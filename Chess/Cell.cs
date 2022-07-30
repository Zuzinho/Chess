namespace Chess
{
    class Cell
    {
        private bool _colour;
        private Figure? _figure;
        private byte _coordinate;
        private bool _isEmpty;

        public Cell(bool colour, byte coordinate, Figure? figure = null)
        {
            _colour = colour;
            _coordinate = coordinate;
            _figure = figure;
            _isEmpty = _figure == null;
        }

        public bool GetColour()
        {
            return _colour;
        }

        public Figure? GetFigure()
        {
            return _figure;
        }
        public void SetFigure(Figure? figure)
        {
            _figure = figure;
            _isEmpty = _figure == null;
        }

        public byte GetCoordinate()
        {
            return _coordinate;
        }

        public bool IsEmpty()
        {
            return _isEmpty;
        }

        public void PrintCell(int aim = 0)
        {
            Console.BackgroundColor = _colour ? ConsoleColor.DarkYellow : ConsoleColor.Magenta;
            if (_figure == null)
            {
                if (aim == 0) {PrintClass.Print("   ", end: '\0'); return;}
                PrintClass.Print(" # ", end: '\0');
                return;
            }
            Console.ForegroundColor = _figure.GetColour() ? ConsoleColor.White : ConsoleColor.Black;
            PrintClass.Print(" " + _figure.GetName()[0] + " ", end: '\0');
        }
    }
}
