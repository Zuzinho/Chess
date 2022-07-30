namespace Chess
{
    class Horse : Figure
    {
        public Horse(byte Coordinate, bool colour) : base(Coordinate, "Horse", colour) { }

        protected override void setCTM() { // Horse can move like Г letter, x+-2 and y+-1, or x+-1 and y+-2
            coordinatesToMove.Clear();
            byte x_coor = (byte)(coordinate / 10), y_coor = (byte)(coordinate % 10);
            coordinatesToMove.Add((byte)((x_coor + 1) * 10 + y_coor + 2));
            coordinatesToMove.Add((byte)((x_coor + 2) * 10 + y_coor + 1));
            coordinatesToMove.Add((byte)((x_coor - 1) * 10 + y_coor - 2));
            coordinatesToMove.Add((byte)((x_coor - 2) * 10 + y_coor - 1));
            coordinatesToMove.Add((byte)((x_coor + 1) * 10 + y_coor - 2));
            coordinatesToMove.Add((byte)((x_coor - 1) * 10 + y_coor + 2));
            coordinatesToMove.Add((byte)((x_coor + 2) * 10 + y_coor - 1));
            coordinatesToMove.Add((byte)((x_coor - 2) * 10 + y_coor + 1));
            coordinatesToMove.RemoveAll(x => x % 10 > 8 || x / 10 > 8 || x % 10 < 1 || x / 10 < 1);
        }
        protected override void setCTB()
        {//Horse can beat the same coordinates as coordinates it can move along
            coordinatesToBeat.Clear();
            byte x_coor = (byte)(coordinate / 10), y_coor = (byte)(coordinate % 10);
            coordinatesToBeat.Add((byte)((x_coor + 1) * 10 + y_coor + 2));
            coordinatesToBeat.Add((byte)((x_coor + 2) * 10 + y_coor + 1));
            coordinatesToBeat.Add((byte)((x_coor - 1) * 10 + y_coor - 2));
            coordinatesToBeat.Add((byte)((x_coor - 2) * 10 + y_coor - 1));
            coordinatesToBeat.Add((byte)((x_coor + 1) * 10 + y_coor - 2));
            coordinatesToBeat.Add((byte)((x_coor - 1) * 10 + y_coor + 2));
            coordinatesToBeat.Add((byte)((x_coor + 2) * 10 + y_coor - 1));
            coordinatesToBeat.Add((byte)((x_coor - 2) * 10 + y_coor + 1));
            coordinatesToBeat.RemoveAll(x => x % 10 > 8 || x / 10 > 8 || x % 10 < 1 || x / 10 < 1);
        }
    }
}
