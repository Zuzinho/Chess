namespace Chess
{ //Coordinate = (xy). For example, coordinate 56 means x = 5, y = 6
    // In real coordinates x = 5, y = 4

    class Figure
    {
        protected byte coordinate;
        protected List<byte> coordinatesToMove;
        protected List<byte> coordinatesToBeat;
        protected string name;
        protected List<byte> movingStory;
        protected bool colour;
        public Figure(byte Coordinate, string Name, bool Colour)
        {
            coordinate = Coordinate;
            name = Name;
            colour = Colour;
            movingStory = new()
            {
                coordinate
            };
            coordinatesToBeat = new();
            coordinatesToMove = new();
            setCTM();
            setCTB();
        }
        public byte GetCoordinate() { return coordinate; }
        public void SetCoordinate(byte Coordinate) { coordinate = Coordinate; }
        public string GetName() { return name; }
        public void SetName(string Name) { name = Name; }
        public bool GetColour() { return colour; }
        public List<byte> GetMovingStory() { return movingStory; }

        protected virtual void setCTM() { }
        protected virtual void setCTB() { }
        public List<byte> GetCoordinatesToBeat() { return coordinatesToBeat; }
        public List<byte> GetCoordinatesToMove() { return coordinatesToMove; }
        public void Move(byte NewCoordinate)
        {
            coordinate = NewCoordinate;
            movingStory.Add(coordinate);
            setCTM();
            setCTB();
        }
        public bool Defeat()
        {
            coordinatesToMove.Clear();
            coordinatesToBeat.Clear();
            movingStory.Clear();
            if (name == "King")
            {
                return true;
            }
            return false;
        }

    }
}