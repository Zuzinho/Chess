namespace Chess
{
    class Move
    {
        private Board? _currentBoard;
        private bool _turn;
        private static Board _board;

        public Move(ref bool turn, ref bool isOver)
        {
            _turn = turn;
            _printTurn();
            _board.PrintBoard(turn);
            byte coor = 0, newCoor = 0;
            Input:
            if (!_getInput(ref coor, ref newCoor)) return;
            while (!_board.Move(coor, newCoor, turn, ref isOver)) { 
                PrintClass.Print("Try again");
                goto Input;
            }
            PrintClass.Print("Success");
            turn = !turn;
            _currentBoard = _board;
        }
        public Board GetBoard()
        {
            return _currentBoard;
        }
        public static void SetBoard(Board board)
        {
            _board = board;
        }

        private static byte _convertCoordinate(string Coor)
        {
            char coor_vert_ch = Coor[0], coor_horiz_ch = Coor[1];
            int coor_vert = (int)coor_vert_ch - 64, coor_horiz = (int)coor_horiz_ch - 48;
            return (byte)(coor_vert * 10 + coor_horiz);
        }

        private bool _getInput(ref byte coor,ref byte newCoor)
        {
            if (!_getCorrectInput(ref coor, "Enter coordinate of figure, or back if you want to go back: ")) return false;
            _board.PrintBoard(_turn,coor);
            if (!_getCorrectInput(ref newCoor, "Enter coordinate you want to place this figure to,or back if you want to go back: ")) return false;
            return true;
        }
        private static void _input(ref string Coor,string inputMessage = "")
        {
            while (true)
            {
                Coor = InputClass.Input<string>(inputMessage);
                if (Coor.Length == 2 || Coor == "back") break;
                PrintClass.Print("This coordinate is uncorrect,try again");
            }
        }
        private static bool _getCorrectInput(ref byte coor, string inputMessage = "")
        {
            string coor_str = "";
            do
            {
                _input(ref coor_str, inputMessage);
                if (coor_str == "back") return false;
                coor = _convertCoordinate(coor_str);
                if (!CoordinatesChecker.CheckCoordinate(coor)) break;
                PrintClass.Print("No such coordinate as this one,try again");
            }
            while (true);
            return true;
        }

        private void _printTurn()
        {
            if (_turn)
            {
                PrintClass.Print("White move");
            }
            else
            {
                PrintClass.Print("Black move");
            }
        }
    }
}
