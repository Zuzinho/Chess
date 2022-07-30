namespace Chess
{
    class Rook : Figure
    {
        public Rook(byte Coordinate, bool colour) : base(Coordinate, "Rook", colour) { }

        protected override void setCTM()
        {
            coordinatesToMove.Clear();
            byte x_coor = (byte)(coordinate / 10), y_coor = (byte)(coordinate % 10);
            for(byte i = 1; i< 9; i++)//Rook can move only vertically or horizontally
            {//It can move x+0 and y+a,a in [1;8], or y+0 and x+a,a in [1;8]
                byte coorVert = (byte)(i * 10 + y_coor), coorHorr = (byte)(x_coor * 10 + i);
                if(i == x_coor)// If i is x coordinate, coorVert = x_coor*10 + y_coor,and it is _coordinate
                {// It cann`t move to coordinate, on which it located now
                    coordinatesToMove.Add(coorHorr);// And we should add only coorHorr
                    continue;
                }
                if(i == y_coor)// If i is y coordinate, coorVert = x_coor*10 + y_coor,and it is _coordinate
                {// It cann`t move to coordinate, on which it located now
                    coordinatesToMove.Add(coorVert);// And we should add only coorVert
                    continue;
                }// Else we should add both of variable
                coordinatesToMove.Add(coorVert);
                coordinatesToMove.Add(coorHorr);
            }
        }
        protected override void setCTB()//Rook can beat the same coordinates as coordinates it can move along
        {
            coordinatesToBeat.Clear();
            byte x_coor = (byte)(coordinate / 10), y_coor = (byte)(coordinate % 10);
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
        }
    }
}
