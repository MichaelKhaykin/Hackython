int limPin = 11;

void setup() {
  Serial.begin(115200);
  pinMode(limPin, INPUT_PULLUP);
  
}

int count = 0;
void loop() {
  delay(500);
  Serial.println(digitalRead(limPin));
 // Serial.println(count);
  count++;
}
