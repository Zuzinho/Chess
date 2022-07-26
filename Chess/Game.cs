namespace Chess
{
    class Game
    {
        private Board _board;
        private bool _turn;
        private bool _isOver;
        public Game()
        {
            _board = new();
            _turn = true;// True - white,false - black
            _isOver = false;
        }
        public void Play()
        {
            Console.ResetColor();
            while (!_isOver)
            {
                if (_turn)
                {
                    PrintClass.Print("White move");
                }
                else
                {
                    PrintClass.Print("Black move");
                }
                PrintClass.Print("Enter coordinate of figure and coordinate you want to place this figure to,or 'end' if you want to finish the game",pause: 200);
                _board.PrintBoard(_turn);
                string coor_str = "", newCoor_str = "";
                _input(ref coor_str);
                if (coor_str == "end") break;
                byte coor = _convertCoordinate(coor_str);
                _input(ref newCoor_str);
                byte newCoor = _convertCoordinate(newCoor_str);
                if (_board.Move(coor, newCoor, _turn,ref _isOver))
                {
                    PrintClass.Print("Success");
                    _turn = !_turn;
                    continue;
                }
                PrintClass.Print("Try again");
            }
            if (!_isOver)
            {
                PrintClass.Print("Game was interrupted", pause: 500);
            }
            else
            {
                _board.PrintBoard();
                PrintClass.Print("Game over", end: ':');
                if (_turn) PrintClass.Print("Black wins", pause: 500);
                else PrintClass.Print("White wins", pause: 500);
            }
            PrintClass.Print("finishing...");
        }
        private byte _convertCoordinate(string Coor)
        {
            char coor_vert_ch = Coor[0], coor_horiz_ch = Coor[1];
            int coor_vert = (int)coor_vert_ch - 64, coor_horiz = (int)coor_horiz_ch - 48;
            return (byte)(coor_vert * 10 + coor_horiz);
        }
        private void _input(ref string Coor)
        {
            while (true) {
                Coor = Console.ReadLine();
                if (Coor.Length == 2 || Coor=="end") break;
                PrintClass.Print("This coordinate is uncorrect,try again");
            }
        }
    }
}
