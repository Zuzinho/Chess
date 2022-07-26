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
            Console.BackgroundColor = ConsoleColor.Black;
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
                PrintClass.Print("Enter coordinate of figure and coordinate you want to place this figure to",pause: 200);
                _board.PrintBoard();
                byte coor, newCoor;
                coor = Convert.ToByte(Console.ReadLine());
                if (coor == 0) break;
                newCoor = Convert.ToByte(Console.ReadLine());
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
    }
}
