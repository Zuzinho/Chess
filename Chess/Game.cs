namespace Chess
{
    class Game
    {
        private Board _board;
        private bool _turn;
        private bool _isOver;
        private List<Move> _moves; 
        public Game()
        {
            _board = new();
            _moves = new List<Move>();
            _turn = true;// True - white,false - black
            _isOver = false;
            Move.SetBoard(_board);
        }
        public void Play()
        {
            Console.ResetColor();
            string command;
            while (!_isOver)
            {
                command = InputClass.Input<string>("Enter number: 1 - if you want to make a next move,0 - if you want to finish: ");
                switch (command)
                {
                    case "1":
                        _moves.Add(new(ref _turn, ref _isOver));
                        break;
                    case "0":
                        goto End;
                }
            }
            End:
            _end(_isOver);
        }
        private void _end(bool isOver)
        {
            if (!isOver)
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
