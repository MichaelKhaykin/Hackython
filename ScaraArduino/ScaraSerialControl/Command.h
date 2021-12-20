enum CommandType {
  NoCommand,
  MoveCommand,
  MagnetizeCommand
};

union Command {
  long positions[3]; //Shoulder, elbow, height (optional)
  bool magnetSetting; //True = on
};
