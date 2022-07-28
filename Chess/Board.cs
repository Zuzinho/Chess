namespace Chess
{
    class Board
    {
        private Cell?[,] _board;
        private List<Figure> _figures;
        private Figure? _lastFigure;
        public Board()
        {
            _board = new Cell?[8, 8];
            _lastFigure = null;
            _figures = new List<Figure>();
            PrintClass.Print("Initialization of the board...", pause: 500);
            _initBoard();
            PrintClass.Print("Place pawns...", pause: 500);
            _placePawns();
            PrintClass.Print("Place rooks...", pause: 500);
            _placeRooks();
            PrintClass.Print("Place horses...", pause: 500);
            _placeHorses();
            PrintClass.Print("Place elephants...", pause: 500);
            _placeElephants();
            PrintClass.Print("Place queens...", pause: 500);
            _placeQueens();
            PrintClass.Print("Place kings...", pause: 500);
            _placeKings();
        }
        private void _initBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    _board[i, j] = new((i + j) % 2 == 1, (byte)(j * 10 + i + 11));
                }
            }
        }
        private void _placePawns()
        {
            for (byte i = 0; i < 8; i++)
            {
                Pawn pawnWhite = new((byte)(i * 10 + 12), true);
                Pawn pawnBlack = new((byte)(i * 10 + 17), false);
                _figures.Add(pawnWhite);
                _figures.Add(pawnBlack);
                _board[1, i].SetFigure(pawnWhite);
                _board[6, i].SetFigure(pawnBlack);
            }
        }
        private void _placeRooks()
        {
            Rook rookWhiteLeft = new(11, true);
            Rook rookWhiteRight = new(81, true);
            Rook rookBlackLeft = new(18, false);
            Rook rookBlackRight = new(88, false);
            _figures.Add(rookWhiteLeft);
            _figures.Add(rookWhiteRight);
            _figures.Add(rookBlackLeft);
            _figures.Add(rookBlackRight);
            _board[0, 0].SetFigure(rookWhiteLeft);
            _board[0, 7].SetFigure(rookWhiteRight);
            _board[7, 0].SetFigure(rookBlackLeft);
            _board[7, 7].SetFigure(rookBlackRight);
        }
        private void _placeHorses()
        {
            Horse HorseWhiteLeft = new(21, true);
            Horse HorseWhiteRight = new(71, true);
            Horse HorseBlackLeft = new(28, false);
            Horse HorseBlackRight = new(78, false);
            _figures.Add(HorseWhiteLeft);
            _figures.Add(HorseWhiteRight);
            _figures.Add(HorseBlackLeft);
            _figures.Add(HorseBlackRight);
            _board[0, 1].SetFigure(HorseWhiteLeft);
            _board[0, 6].SetFigure(HorseWhiteRight);
            _board[7, 1].SetFigure(HorseBlackLeft);
            _board[7, 6].SetFigure(HorseBlackRight);
        }
        private void _placeElephants()
        {
            Elephant elephantWhiteLeft = new(31, true);
            Elephant elephantWhiteRight = new(61, true);
            Elephant elephantBlackLeft = new(38, false);
            Elephant elephantBlackRight = new(68, false);
            _figures.Add(elephantWhiteLeft);
            _figures.Add(elephantWhiteRight);
            _figures.Add(elephantBlackLeft);
            _figures.Add(elephantBlackRight);
            _board[0, 2].SetFigure(elephantWhiteLeft);
            _board[0, 5].SetFigure(elephantWhiteRight);
            _board[7, 2].SetFigure(elephantBlackLeft);
            _board[7, 5].SetFigure(elephantBlackRight);
        }
        private void _placeQueens()
        {
            Queen queenWhite = new(41, true);
            Queen queenBlack = new(48, false);
            _figures.Add(queenWhite);
            _figures.Add(queenBlack);
            _board[0, 3].SetFigure(queenWhite);
            _board[7, 3].SetFigure(queenBlack);
        }
        private void _placeKings()
        {
            King kingWhite = new(51, true);
            King kingBlack = new(58, false);
            _figures.Add(kingWhite);
            _figures.Add(kingBlack);
            _board[0, 4].SetFigure(kingWhite);
            _board[7, 4].SetFigure(kingBlack);
        }
        private static bool _checkCoordinate(byte Coordinate, int y)
        {
            return Coordinate < 11 || Coordinate > 88 || y == 0 || y == 9;
        }
        private bool _move(Cell cell, Cell newCell)
        {
            var figure = cell.GetFigure();
            if (MovesChecker.CheckMove(this, figure, newCell.GetCoordinate()))
            {
                figure.Move(newCell.GetCoordinate());
                figure = figure.GetName() == "Pawn"? _turnPawnInto(figure,newCell):figure;
                newCell.SetFigure(figure);
                cell.SetFigure(null);
                _lastFigure = figure;
                return true;
            }
            PrintClass.Print("You cann`t move to this coordinate");
            return false;
        }
        private bool _beat(Cell cell, byte NewCoordinate,Figure beatenFigure, ref bool isOver,Cell DoubleCell = null)
        {
            Figure? figure = cell.GetFigure();
            Cell? newCell = _board[NewCoordinate % 10 - 1, NewCoordinate / 10 - 1];
            if (!MovesChecker.CheckHit(this, figure, NewCoordinate))
            {
                PrintClass.Print("You cann`t beat figure placed this coordinate");
                return false;
            }
            figure.Move(NewCoordinate);
            figure = figure.GetName() == "Pawn" ? _turnPawnInto(figure, newCell) : figure;
            newCell.SetFigure(figure);
            cell.SetFigure(null);
            _figures.Remove(beatenFigure);
            isOver = beatenFigure.Defeat();
            if (DoubleCell != null) DoubleCell.SetFigure(null);
            _lastFigure = figure;
            return true;
        }
        public bool Move(byte Coordinate, byte NewCoordinate, bool colourTurn, ref bool isOver)
        {
            int y_coor = Coordinate / 10, x_coor = Coordinate % 10;
            if (_checkCoordinate(Coordinate, y_coor))
            {
                PrintClass.Print("No such coordinate as the first one");
                return false;
            }
            Cell? cell = _board[x_coor - 1, y_coor - 1];
            Figure? figure = cell.GetFigure();
            if (figure == null)
            {
                PrintClass.Print("Here is no any figure");
                return false;
            }
            if (figure.GetColour() != colourTurn)
            {
                PrintClass.Print("Turn of figures with another colour");
                return false;
            }
            int newY = NewCoordinate / 10, newX = NewCoordinate % 10;
            if (_checkCoordinate(NewCoordinate, newY))
            {
                PrintClass.Print("No such coordinate as the second one");
                return false;
            }
            Cell? newCell = _board[newX - 1, newY - 1];
            Figure? beatenFigure = newCell.GetFigure();
            int dir = colourTurn ? -1 : 1;
            if (beatenFigure == null)
            {
                Cell? doubleCell = _board[newX - 1 + dir, newY -1];
                beatenFigure = doubleCell.GetFigure();
                if (!_checkDoubleMove(beatenFigure)) return _move(cell, newCell);
                return _beat(cell, NewCoordinate,beatenFigure,ref isOver,doubleCell);
            }
            if (colourTurn != beatenFigure.GetColour())
            {
                return _beat(cell, NewCoordinate,beatenFigure, ref isOver);
            }
            if (_checkCastling(figure, beatenFigure))
            {
                return _makeCastling(figure, beatenFigure);
            }
            PrintClass.Print("You cann`t beat figure which has the same colour");
            return false;
        }
        private bool _checkDoubleMove(Figure beatenFigure)
        {
            if (beatenFigure == null) return false;
            if (beatenFigure.GetName() != "Pawn") return false;
            if (beatenFigure.GetMovingStory().Count != 2) return false;
            if (beatenFigure != _lastFigure) return false;
            return true;
        }
        private bool _checkCastling(Figure figure,Figure beatenFigure)
        {
            if (figure.GetName() != "King" || beatenFigure.GetName() != "Rook") return false;
            if (figure.GetMovingStory().Count + beatenFigure.GetMovingStory().Count != 2) return false;
            if (!MovesChecker.CheckMove(this, beatenFigure, figure.GetCoordinate())) return false;
            return true;
        }
        private bool _makeCastling(Figure king,Figure rook)
        {
            int kingCoordinate = king.GetCoordinate(),rookCoordinate = rook.GetCoordinate();
            Cell rookCell = _board[rookCoordinate % 10 - 1, rookCoordinate / 10 - 1], kingCell = _board[kingCoordinate % 10 - 1, kingCoordinate / 10 - 1];
            int diff = kingCoordinate - rookCoordinate;
            int dir = diff>0? 1 : -1;
            int newRookCoor = kingCoordinate - dir*10,newKingCoor = newRookCoor-10*dir;
            Cell newRookCell = _board[newRookCoor % 10 - 1, newRookCoor / 10 - 1],newKingCell = _board[newKingCoor % 10 - 1, newKingCoor / 10 - 1];
            newRookCell.SetFigure(rook);
            newKingCell.SetFigure(king);
            rookCell.SetFigure(null);
            kingCell.SetFigure(null);
            king.Move((byte)newKingCoor);
            rook.Move((byte)newRookCoor);
            return true;
        }
        private Figure _turnPawnInto(Figure figure,Cell newCell) {
            byte newCoor = newCell.GetCoordinate();
            bool figureColour = figure.GetColour();
            int lastHoriz = figureColour ? 8 : 1;
            if(newCoor%10 == lastHoriz)
            {
                while (true)
                {
                    string number = InputClass.Input<string>("Enter number if you want turn pawn into: 1-horse,2-elephant,3-rook,4-queen: ");
                    switch (number)
                    {
                        case "1":
                            return new Horse(newCoor, figureColour);
                            break;
                        case "2":
                            return new Elephant(newCoor, figureColour);
                            break;
                        case "3":
                            return new Rook(newCoor, figureColour);
                            break;
                        case "4":
                            return new Queen(newCoor, figureColour);
                            break;
                        default:
                            PrintClass.Print("Incorrect input,try again");
                            break;
                    }
                }
            }
            return figure;
        }
        public void PrintBoard(bool Turn = true)
        {
            if (_board == null) return;
            int dir = Turn ? 1 : -1;
            for (int i = (int)(3.5+3.5*dir); i !=3.5-4.5*dir; i-=dir, PrintClass.Print('\0'))
            {
                Console.ResetColor();
                PrintClass.Print(i+1, end: ' ');
                for (int j = (int)(3.5-3.5*dir); j != 3.5+4.5*dir; j+=dir, Console.ResetColor())
                {
                    _board[i, j].PrintCell();
                }
            }
            PrintClass.Print("  ", end: '\0');
            for (int i = (int)(4.5-3.5*dir); i != 4.5+4.5*dir; i+=dir)
            {
                PrintClass.Print(" " + (char)(64+i) + " ", end: '\0');
            }
            PrintClass.Print('\0');
        }
        public Cell GetCell(int x, int y)
        {
            return _board[x, y];
        }
    }
}