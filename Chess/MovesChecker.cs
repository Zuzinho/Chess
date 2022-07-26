namespace Chess
{
    class MovesChecker
    {
        public static bool CheckMove(Board board, Figure figure, byte NewCoordinate)
        {
            string name = figure.GetName();
            if (name == "Pawn") return _checkMove(board, figure as Pawn, NewCoordinate);
            if (name == "Rook") return _checkMove(board, figure as Rook, NewCoordinate);
            if (name == "Horse") return _checkMove(figure as Horse, NewCoordinate);
            if (name == "Elephant") return _checkMove(board, figure as Elephant, NewCoordinate);
            if (name == "Queen") return _checkMove(board, figure as Queen, NewCoordinate);
            if (name == "King") return _checkMove(figure as King, NewCoordinate);
            return false;
        }
        public static bool CheckHit(Board board, Figure figure, byte NewCoordinate)
        {
            string name = figure.GetName();
            if (name == "Pawn") return _checkHit(figure as Pawn, NewCoordinate);
            if (name == "Rook") return _checkHit(board, figure as Rook, NewCoordinate);
            if (name == "Horse") return _checkHit(figure as Horse, NewCoordinate);
            if (name == "Elephant") return _checkHit(board, figure as Elephant, NewCoordinate);
            if (name == "Queen") return _checkHit(board, figure as Queen, NewCoordinate);
            if (name == "King") return _checkHit(figure as King, NewCoordinate);
            return false;
        }
        private static bool _checkMove(Board board, Pawn pawn, byte NewCoordinate)
        {
            var CTM = pawn.GetCoordinatesToMove();
            if (!CTM.Contains(NewCoordinate)) return false;
            if (CTM.Count == 1) return true;
            int x_coor = CTM[0] %10,y_coor = CTM[0] /10;
            Cell cell = board.GetCell(x_coor-1,y_coor-1);
            return cell.IsEmpty();
        }
        private static bool _checkHit(Pawn pawn, byte BeatCoordinate)
        {
            var CTB = pawn.GetCoordinatesToBeat();
            return CTB.Contains(BeatCoordinate);
        }
        private static bool _checkMove(Board board,Rook rook,byte NewCoordinate) {
            var CTM = rook.GetCoordinatesToMove();
            int coordinate = rook.GetCoordinate();
            if (!CTM.Contains(NewCoordinate)) return false;
            int diff = NewCoordinate - coordinate;
            //13*(31**) - 11*(11**) = 2*(20**) Up
            //31*(13**) - 11*(11**) = 20*(2**) Right
            //51*(15**) - 81*(18**) = -30*(-3**) Left
            //15*(51**) - 18*(81**) = -3*(-30**) Down
            //* - My coordinates or diff,** - real coordinates or diff
            int plus;
            if (diff >= 10) plus = 10;// Right 
            else if (diff > 0) plus = 1;// Up
            else if (diff<=-10) plus = -10;// Left
            else plus = -1;// Down
            return _check(board, coordinate, NewCoordinate, plus);
        }
        private static bool _checkHit(Board board, Rook rook, byte NewCoordinate)
        {
            return _checkMove(board,rook,NewCoordinate);//Rook can beat the same coordinates as coordinates it can move along
        }
        private static bool _checkMove(Horse horse, byte NewCoordinate)
        {
            var CTM = horse.GetCoordinatesToMove();
            return CTM.Contains(NewCoordinate);
        }
        private static bool _checkHit(Horse horse, byte NewCoordinate)
        {
            var CTB = horse.GetCoordinatesToMove();
            return CTB.Contains(NewCoordinate);
        }
        private static bool _checkMove(Board board,Elephant elephant,byte NewCoordinate)
        {
            var CTM = elephant.GetCoordinatesToMove();
            int coordinate = elephant.GetCoordinate();
            if (!CTM.Contains(NewCoordinate)) return false;
            int diff = NewCoordinate - coordinate;
            //53*(35**) - 31*(13**) = 22*(22**) Up Right
            //13*(31**) - 31*(13**) = -18*(18**) Up Left
            //56*(65**) - 38*(83**) = 18*(-18**) Down Right
            //16*(61**) - 38*(83**) = -22*(-22**) Down Left
            //* - My coordinates or diff,** - real coordinates or diff
            int plus;
            if(diff>0)// Right
            {
                if (diff % 11 == 0) plus = 11; // Up Right 
                else plus = 9;// Down Right
            }
            else // Left
            {
                if (diff % 9 == 0) plus = -9; // Up Left
                else plus = -11;// Down Left
            }
            return _check(board, coordinate, NewCoordinate, plus);
        }
        private static bool _checkHit(Board board, Elephant elephant, byte NewCoordinate)
        {
            return _checkMove(board, elephant, NewCoordinate);//Elephant can beat the same coordinates as coordinates it can move along
        }
        private static bool _checkMove(Board board, Queen queen, byte NewCoordinate)
        {
            var CTM = queen.GetCoordinatesToMove();
            int coordinate = queen.GetCoordinate();
            if (!CTM.Contains(NewCoordinate)) return false;
            int diff = NewCoordinate - coordinate;
            //53*(35**) - 31*(13**) = 22*(22**) Up Right
            //13*(31**) - 31*(13**) = -18*(18**) Up Left
            //56*(65**) - 38*(83**) = 18*(-18**) Down Right
            //16*(61**) - 38*(83**) = -22*(-22**) Down Left
            //13*(31**) - 11*(11**) = 2*(20**) Up
            //31*(13**) - 11*(11**) = 20*(2**) Right
            //51*(15**) - 81*(18**) = -30*(-3**) Left
            //15*(51**) - 18*(81**) = -3*(-30**) Down
            //* - My coordinates or diff,** - real coordinates or diff
            int plus;
            if (diff >= 10)// Right
            {
                plus = 10;// Right
                if (diff % 11 == 0) plus += 1;// Up Right
                else if (diff % 9 == 0) plus -= 1;// Down Right
            }
            else if (diff > 0) plus = 1;// Up
            else if (diff <= -10)// Left
            {
                plus = -10;// Left
                if (diff % 11 == 0) plus -= 1;//Down left
                else if (diff % 9 == 0) plus += 1;// Down Right
            }
            else plus = -1;
            return _check(board, coordinate, NewCoordinate, plus);
        }
        private static bool _checkHit(Board board, Queen queen, byte NewCoordinate)
        {
            return _checkMove(board, queen, NewCoordinate);//Queen can beat the same coordinates as coordinates it can move along
        }
        private static bool _checkMove(King king,byte NewCoordinate)
        {
            var CTM = king.GetCoordinatesToMove();
            return CTM.Contains(NewCoordinate);
        }
        private static bool _checkHit(King king, byte NewCoordinate)
        {
            var CTB = king.GetCoordinatesToMove();
            return CTB.Contains(NewCoordinate);
        }
        private static bool _check(Board board,int coordinate,byte NewCoordinate,int plus)
        {
            do
            {
                coordinate += plus;
                int x_coor = coordinate % 10, y_coor = coordinate / 10;
                Cell cell = board.GetCell(x_coor - 1, y_coor - 1);
                if (!cell.IsEmpty()) return false;
            }
            while (coordinate != NewCoordinate - plus);
            return true;
        }
    }
}
