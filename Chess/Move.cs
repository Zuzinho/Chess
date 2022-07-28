namespace Chess
{
    class Move
    {
        private static Board _board;
        private Board _currentBoard;
        public Move(ref bool turn, ref bool isOver)
        {
            if (turn)
            {
                PrintClass.Print("White move");
            }
            else
            {
                PrintClass.Print("Black move");
            }
            _board.PrintBoard(turn);
            string coor_str = "", newCoor_str = "";
            _input(ref coor_str, "Enter coordinate of figure: ");
            byte coor = _convertCoordinate(coor_str);
            _input(ref newCoor_str, "Enter coordinate you want to place this figure to: ");
            byte newCoor = _convertCoordinate(newCoor_str);
            if (_board.Move(coor, newCoor, turn, ref isOver))
            {
                PrintClass.Print("Success");
                turn = !turn;
            }
            PrintClass.Print("Try again");
            _currentBoard = _board;
        }
        public static void SetBoard(Board board)
        {
            _board = board;
        }
        public Board GetBoard()
        {
            return _currentBoard;
        }
        private byte _convertCoordinate(string Coor)
        {
            char coor_vert_ch = Coor[0], coor_horiz_ch = Coor[1];
            int coor_vert = (int)coor_vert_ch - 64, coor_horiz = (int)coor_horiz_ch - 48;
            return (byte)(coor_vert * 10 + coor_horiz);
        }
        private void _input(ref string Coor,string inputMessage = "")
        {
            while (true)
            {
                Coor = InputClass.Input<string>(inputMessage);
                if (Coor.Length == 2) break;
                PrintClass.Print("This coordinate is uncorrect,try again");
            }
        }
    }
}
