namespace Chess
{
    class CoordinatesChecker
    {
        public static bool CheckCoordinate(int Coordinate)
        {
            int y = Coordinate % 10;
            return Coordinate < 11 || Coordinate > 88 || y == 0 || y == 9;
        }
    }
}
