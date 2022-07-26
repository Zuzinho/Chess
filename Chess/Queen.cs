namespace Chess
{
    class Queen : Figure
    {
        public Queen(byte Coordinate, bool colour) : base(Coordinate, "Queen", colour) { }

        protected override void setCTM()
        {//Queen can move like rook and elephant. Because of it we can copy CTM from rook and elephant
            coordinatesToMove.Clear();
            byte x_coor = (byte)(coordinate / 10), y_coor = (byte)(coordinate % 10);
            int dif = x_coor - y_coor, sum = x_coor + y_coor;
            //Rook movings
            for (byte i = 1; i < 9; i++) {
                byte coorVert = (byte)(i * 10 + y_coor), coorHorr = (byte)(x_coor * 10 + i);
                if (i == x_coor){
                    coordinatesToMove.Add(coorHorr);
                    continue;
                }
                if (i == y_coor){
                    coordinatesToMove.Add(coorVert);
                    continue;
                }
                coordinatesToMove.Add(coorVert);
                coordinatesToMove.Add(coorHorr);
            }
            //Elephant movings
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (i - j == dif || i + j == sum)
                    {
                        if (i * 10 + j != coordinate)
                        {
                            coordinatesToMove.Add((byte)(i * 10 + j));
                        }
                    }
                }
            }
        }
        protected override void setCTB()//Queen can beat the same coordinates as coordinates it can move along
        {
            coordinatesToBeat.Clear();
            byte x_coor = (byte)(coordinate / 10), y_coor = (byte)(coordinate % 10);
            int dif = x_coor - y_coor, sum = x_coor + y_coor;
            for (byte i = 1; i < 9; i++)
            {
                byte coorVert = (byte)(i * 10 + y_coor), coorHorr = (byte)(x_coor * 10 + i);
                if (i == x_coor)
                {
                    coordinatesToBeat.Add(coorHorr);
                    continue;
                }
                if (i == y_coor)
                {
                    coordinatesToBeat.Add(coorVert);
                    continue;
                }
                coordinatesToBeat.Add(coorVert);
                coordinatesToBeat.Add(coorHorr);
            }
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (i - j == dif || i + j == sum)
                    {
                        if (i * 10 + j != coordinate)
                        {
                            coordinatesToBeat.Add((byte)(i * 10 + j));
                        }
                    }
                }
            }
        }
    }
}
