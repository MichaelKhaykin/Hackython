#include <AccelStepper.h>
#include <MultiStepper.h>
#include "Command.h"

//#define _DEBUG_

const int zLim = 11;
const int enPin = 8;
const int magnetPin = 12;
const int stepsPerRev = 9600;

AccelStepper stepperS(AccelStepper::DRIVER, 2, 5); //x - shoulder
AccelStepper stepperE(AccelStepper::DRIVER, 3, 6); //y - elbow
AccelStepper stepperZ(AccelStepper::DRIVER, 4, 7); //z - screw
MultiStepper arm;

void initMotor(AccelStepper* stepper, bool invertDir, int maxAccel = 4000, int maxSpd = 1000) {
  stepper->setEnablePin(enPin);
  stepper->setPinsInverted(invertDir, false, true);
  stepper->setAcceleration(maxAccel);
  stepper->setMaxSpeed(maxSpd);
  stepper->enableOutputs();
}

void lowerToSwitch() { //Blocking
  digitalWrite(enPin, LOW);
  stepperZ.moveTo(-100000); //Use instead of setSpeed for acceleration
  while(stepperZ.run() & digitalRead(zLim) == HIGH) {
  }
  stepperZ.stop();
  stepperZ.setCurrentPosition(0);
  digitalWrite(enPin, HIGH);
}

void setup() {
  pinMode(magnetPin, OUTPUT);
  pinMode(zLim, INPUT_PULLUP);
  initMotor(&stepperS, false);
  initMotor(&stepperE, true);
  initMotor(&stepperZ, true, 4000, 1000);
  arm.addStepper(stepperS);
  arm.addStepper(stepperE);
  lowerToSwitch();
  Serial.begin(115200);
}

Command command;
String commandStr;

CommandType getNewPosition() {
  if (Serial.available() > 0) {
    byte readByte;
    do {
      if (Serial.available() == 0) {
        return NoCommand;
      }
      readByte = Serial.read();
      commandStr += (char)readByte;
    } while (readByte != '\n'); //Change this to newline when serial monitor testing

    if (commandStr.length() < 2) {
      commandStr = "";
      return NoCommand;
    }

    #ifdef _DEBUG_
    Serial.println("Command: " + commandStr);
    #endif
    
    if (commandStr.charAt(0) == 'm' || commandStr.charAt(0) == 'M') {
      char secondChar = commandStr.charAt(1);
      commandStr = "";
      if (secondChar == 'n' || secondChar == 'N') {
        command.magnetSetting = true;
        return MagnetizeCommand;
      }
      else if (secondChar == 'f' || secondChar == 'F') {
        command.magnetSetting = false;
        return MagnetizeCommand;
      }
      else {
        return NoCommand;
      }
    }
    else {
      int firstComma = commandStr.indexOf(',');

      if(firstComma == -1) {
        commandStr = "";
        return NoCommand;
      }
      
      int secondComma = commandStr.indexOf(',', firstComma + 1);
      int angleS = commandStr.substring(0, firstComma).toInt();
      int angleE = commandStr.substring(firstComma + 1, secondComma == -1 ? commandStr.length() : secondComma).toInt();
      int posZ;
      if (secondComma == -1) {
        command.positions[2] = -1;
      }
      else {
        posZ = commandStr.substring(secondComma + 1, commandStr.length()).toInt();
        posZ = posZ > 9500 ? 9500 : posZ < 0 ? 0 : posZ;
        command.positions[2] = posZ;
      }
  
      command.positions[0] = angleS / 360.0 * stepsPerRev;
      command.positions[1] = angleE / 360.0 * stepsPerRev;
      commandStr = "";
      return MoveCommand;
    }
  }
  else {
    return NoCommand;
  }
}

bool moving = false;
bool beginning = true;
long stopTime;
void loop() {
  switch(getNewPosition()) {
    case MoveCommand:
      if(!moving) {
        arm.moveTo(command.positions);
        if(command.positions[2] >= 0) {
          stepperZ.moveTo(command.positions[2]);
        }
      }
      break;
    case MagnetizeCommand:
      digitalWrite(magnetPin, command.magnetSetting ? HIGH : LOW);
      #ifdef _DEBUG_
      Serial.println(command.magnetSetting ? "Magnet on" : "Magnet off");
      #endif
      break;
  }

  if (arm.run() | stepperZ.run()) {
    digitalWrite(enPin, LOW);
    moving = true;
    beginning = false;
  }
  else {
    if(!beginning) {
      Serial.println("reached");
    }
    if (moving)
    {
      #ifdef _DEBUG_
      Serial.println("Curr S Angle: " + String(stepperS.currentPosition() * 360.0 / stepsPerRev));
      Serial.println("Curr E Angle: " + String(stepperE.currentPosition() * 360.0 / stepsPerRev));
      Serial.println("Curr Z: " + String(stepperZ.currentPosition()));
      Serial.println();
      #endif
      stopTime = millis();
    }
    moving = false;
    if (millis() - stopTime > 500) {
      //digitalWrite(enPin, HIGH);
    }
  }
}
