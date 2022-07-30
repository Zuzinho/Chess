namespace Chess
{
    class MovesListsGetter
    {
        public static List<byte> GetMovesList(Board board, Figure figure)
        {
            if (figure == null) return new List<byte> { };
            string name = figure.GetName();
            if (name == "Pawn") return _getAllMovesList(board, figure as Pawn);
            if (name == "Rook") return _getAllMovesList(board, figure as Rook);
            if (name == "Horse") return _getAllMovesList(figure as Horse);
            if (name == "Elephant") return _getAllMovesList(board, figure as Elephant);
            if (name == "Queen") return _getAllMovesList(board, figure as Queen);
            if (name == "King") return _getAllMovesList(figure as King);
            return new List<byte> { };
        }
        public static List<byte> GetHitsList(Board board, Figure figure)
        {
            if (figure == null) return new List<byte> { };
            string name = figure.GetName();
            if (name == "Pawn") return _getAllHitsList(figure as Pawn);
            if (name == "Rook") return _getAllMovesList(board, figure as Rook);
            if (name == "Horse") return _getAllMovesList(figure as Horse);
            if (name == "Elephant") return _getAllMovesList(board, figure as Elephant);
            if (name == "Queen") return _getAllMovesList(board, figure as Queen);
            if (name == "King") return _getAllMovesList(figure as King);
            return new List<byte> { };
        }

        private static List<byte> _getAllMovesList(Board board, Pawn pawn)
        {
            List<byte> MovesList = pawn.GetCoordinatesToMove();
            if (MovesList.Count == 1) return MovesList;
            Cell cell = board.GetCell(MovesList[0]);
            if (!cell.IsEmpty()) MovesList.RemoveAt(1);
            return MovesList;
        }
        private static List<byte> _getAllHitsList(Pawn pawn)
        {
            return pawn.GetCoordinatesToBeat();
        }
        private static List<byte> _getAllMovesList(Board board, Rook rook)
        {
            var CTM = rook.GetCoordinatesToMove();
            int coordinate = rook.GetCoordinate();
            //13*(31**) - 11*(11**) = 2*(20**) Up
            //31*(13**) - 11*(11**) = 20*(2**) Right
            //51*(15**) - 81*(18**) = -30*(-3**) Left
            //15*(51**) - 18*(81**) = -3*(-30**) Down
            //* - My coordinates or diff,** - real coordinates or diff
            List<int> directList = new List<int> { 10, 1, -10, -1 };
            // Right - 10
            // Up - 1
            // Left - -10
            // Down - -1
            return _getMovesList(board, coordinate, directList);
        }
        private static List<byte> _getAllMovesList(Horse horse)
        {
            return horse.GetCoordinatesToMove();
        }
        private static List<byte> _getAllMovesList(Board board, Elephant elephant)
        {
            var CTM = elephant.GetCoordinatesToMove();
            int coordinate = elephant.GetCoordinate();
            //53*(35**) - 31*(13**) = 22*(22**) Up Right
            //13*(31**) - 31*(13**) = -18*(18**) Up Left
            //56*(65**) - 38*(83**) = 18*(-18**) Down Right
            //16*(61**) - 38*(83**) = -22*(-22**) Down Left
            //* - My coordinates or diff,** - real coordinates or diff
            List<int> directList = new List<int> { 11, 9, -11, -9 };
            // Up Right - 11
            // Down Right - 9
            // Up Left - -9
            // Down Left - -11
            return _getMovesList(board, coordinate, directList);
        }
        private static List<byte> _getAllMovesList(Board board, Queen queen)
        {
            var CTM = queen.GetCoordinatesToMove();
            int coordinate = queen.GetCoordinate();
            //53*(35**) - 31*(13**) = 22*(22**) Up Right
            //13*(31**) - 31*(13**) = -18*(18**) Up Left
            //56*(65**) - 38*(83**) = 18*(-18**) Down Right
            //16*(61**) - 38*(83**) = -22*(-22**) Down Left
            //13*(31**) - 11*(11**) = 2*(20**) Up
            //31*(13**) - 11*(11**) = 20*(2**) Right
            //51*(15**) - 81*(18**) = -30*(-3**) Left
            //15*(51**) - 18*(81**) = -3*(-30**) Down
            //* - My coordinates or diff,** - real coordinates or diff
            List<int> directList = new List<int> { 10, 11, 9, 1, -10, -11, -9, -1 };
            // Right - 10
            // Up Right - 11
            // Down Right - 9
            // Up - 1
            // Left - -10
            // Down left - -11
            // Up left - -9
            // Down - -1
            return _getMovesList(board, coordinate, directList);
        }
        private static List<byte> _getAllMovesList(King king)
        {
            return king.GetCoordinatesToMove();
        }

        private static List<byte> _getMovesList(Board board, int coordinate, List<int> directList)
        {
            List<byte> allMovesList = new List<byte>();
            foreach (int plus in directList) allMovesList.AddRange(_getMovesList(board, coordinate, plus));
            return allMovesList;
        }
        private static List<byte> _getMovesList(Board board, int coordinate, int plus)
        {
            List<byte> CTM = new List<byte>();
            while (CoordinatesChecker.CheckCoordinate(coordinate))
            {
                coordinate += plus;
                Cell cell = board.GetCell(coordinate);
                if (!cell.IsEmpty()) break;
                CTM.Add((byte)coordinate);
            }
            return CTM;
        }
    }
}
