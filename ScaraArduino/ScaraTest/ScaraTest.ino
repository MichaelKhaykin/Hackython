#include <AccelStepper.h>

const int enPin = 8;
const int dirPin = 7;
const int stepPin = 4;
const int limPin = 11;

AccelStepper stepper(AccelStepper::DRIVER, stepPin, dirPin);

int goal = 200;
void setup()
{
  pinMode(limPin, INPUT_PULLUP);
  stepper.setEnablePin(enPin);
  stepper.setPinsInverted(false, false, true);
  stepper.setAcceleration(2000);
  stepper.setMaxSpeed(200);
  stepper.enableOutputs();
  stepper.moveTo(goal);
  //stepper.setSpeed(-100);
}

void loop()
{
    digitalWrite(enPin, !digitalRead(limPin));

    int absError = stepper.distanceToGo() < 0 ? -stepper.distanceToGo() : stepper.distanceToGo();
    if (absError < 5)
    {
      goal *= -1;
      stepper.moveTo(goal);
      stepper.setAcceleration(2000);
      delay(500);
    }
    else if(absError < 100) 
    {
      stepper.setAcceleration(2000);
    }

    stepper.run();
}
