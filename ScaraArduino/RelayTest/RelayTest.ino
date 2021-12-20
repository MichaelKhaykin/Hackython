int relayPin = 12;
void setup() {
  pinMode(relayPin, OUTPUT);

}

void loop() {
  digitalWrite(relayPin, LOW);
  delay(1000);
  digitalWrite(relayPin, HIGH);
  delay(1000);
}
