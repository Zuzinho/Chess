namespace Chess
{
    class Pawn : Figure
    {
        public Pawn(byte Coordinate, bool colour) : base(Coordinate, "Pawn", colour) { }
        protected override void setCTM()
        {
            coordinatesToMove.Clear();
            int direction = colour ? 1 : -1;
            if (coordinate % 10 == 4.5 + direction*3.5) return;// If it is on upper horizontal, it cann`t move
            coordinatesToMove.Add((byte)(coordinate + 1*direction)); // else it can move only to up, it means that
            // it can move to y+1 and x+0 coordinate, hence we should plus 1 and plus 0 to coordinate. 1 + 0 = 1
            if (movingStory.Count == 1) coordinatesToMove.Add((byte)(coordinate + 2 * direction));// If this move is the first for it
            // it can move to y+2 and x+0 coordinate 
        }
        protected override void setCTB()
        {
            int direction = colour ? 1 : -1;
            coordinatesToBeat.Clear();
            if (coordinate/10 == 8) // If x = 8, it can beat only left figure
            { // It can beat y+1 and x - 1, it means that we should plus 1 and minus 10 to coordinate.
                coordinatesToBeat.Add((byte)(coordinate - (10-direction)));// 1 - 10 = -9
                return;
            }
            if( coordinate / 10 == 1) // If x = 1, it can beat only right figure
            { // It can beat y+1 and x + 1, it means that we should plus 1 and plus 10 to coordinate.
                coordinatesToBeat.Add((byte)(coordinate + (10+direction)));// 1 + 10 = 11
                return;
            } // If it isn`t on left or right vertical, it can beat left and right figure
            coordinatesToBeat.Add((byte)(coordinate + (10 + direction)));
            coordinatesToBeat.Add((byte)(coordinate - (10-direction)));
        }
    }
}
