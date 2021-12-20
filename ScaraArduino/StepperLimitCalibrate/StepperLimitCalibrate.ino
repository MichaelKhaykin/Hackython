#include <AccelStepper.h>

const int enPin = 8;
const int limPin = 11;

AccelStepper stepper(AccelStepper::DRIVER, 4, 7);

void lowerToSwitch() //Blocking
{
  digitalWrite(enPin, LOW);
  while(digitalRead(limPin) == HIGH)
  {
    stepper.setSpeed(-500);
    stepper.runSpeed();
  }
  stepper.setSpeed(0);
  stepper.runSpeed();
  stepper.setCurrentPosition(0);
  digitalWrite(enPin, HIGH);
  //stepper.moveTo(0);
}

void setup()
{
  pinMode(limPin, INPUT_PULLUP);
  stepper.setEnablePin(enPin);
  stepper.setPinsInverted(true, false, true);
  stepper.setAcceleration(3000);
  stepper.setMaxSpeed(1000);
  stepper.enableOutputs();
  lowerToSwitch();
  stepper.moveTo(2000);
  digitalWrite(enPin, LOW);
}

void loop()
{
    if(stepper.run())
    {
      //digitalWrite(enPin, !digitalRead(limPin));
    }
    else
    {
      digitalWrite(enPin, HIGH);
    }
}
