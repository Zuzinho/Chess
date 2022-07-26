namespace Chess
{
    class King : Figure
    {
        public King(byte Coordinate, bool colour) : base(Coordinate, "King", colour) { }

        protected override void setCTM()
        {//King can move to neighboring coordinates
            coordinatesToMove.Clear();
            byte x_coor = (byte)(coordinate / 10), y_coor = (byte)(coordinate % 10);
            for(int i = x_coor - 1; i <= x_coor + 1; i++)
            {
                for(int j=y_coor - 1; j <= y_coor + 1; j++)
                {
                    if (i * 10 + j == coordinate) continue;
                    coordinatesToMove.Add((byte)(i*10 + j));
                }
            }
            coordinatesToMove.RemoveAll(x => x % 10 > 8 || x / 10 > 8 || x % 10 < 1 || x / 10 < 1);
        }
        protected override void setCTB()
        {//Rook can beat the same coordinates as coordinates it can move along
            coordinatesToMove.Clear();
            byte x_coor = (byte)(coordinate / 10), y_coor = (byte)(coordinate % 10);
            for (int i = x_coor - 1; i <= x_coor + 1; i++)
            {
                for (int j = y_coor - 1; j <= y_coor + 1; j++)
                {
                    if (i * 10 + j == coordinate) continue;
                    coordinatesToMove.Add((byte)(i * 10 + j));
                }
            }
            coordinatesToMove.RemoveAll(x => x % 10 > 8 || x / 10 > 8 || x % 10 < 1 || x / 10 < 1);
        }
    }
}
