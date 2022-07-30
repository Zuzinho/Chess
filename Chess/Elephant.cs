namespace Chess
{
    class Elephant : Figure
    {
        public Elephant(byte Coordinate, bool colour) : base(Coordinate, "Elephant", colour) { }

        protected override void setCTM()
        {// Elephant can move only diagonally,it means that it can move along coordinate
            coordinatesToMove.Clear();// which x coordinate and y coordinate x - y = x initial - y initial
            byte x_coor = (byte)(coordinate / 10), y_coor = (byte)(coordinate % 10);// or x + y = x initial + y initial
            int dif = x_coor - y_coor,sum = x_coor + y_coor;
            for(int i = 1; i < 9; i++)
            {
                for(int j =1; j < 9; j++)
                {
                    if(i - j == dif || i + j == sum)
                    {
                        if(i*10 + j != coordinate)
                        {
                            coordinatesToMove.Add((byte)(i * 10 + j));
                        }
                    }
                }
            }
        }
        protected override void setCTB()
        {//Elephant can beat the same coordinates as coordinates it can move along
            coordinatesToBeat.Clear();
            byte x_coor = (byte)(coordinate / 10), y_coor = (byte)(coordinate % 10);
            int dif = x_coor - y_coor, sum = x_coor + y_coor;
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
