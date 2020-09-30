namespace Application.Auxiliary
{
    public class GridPosition
    {
        private readonly int row;
        private readonly int col;

        public int Row
        {
            get
            {
                return row;
            }
        }
        public int Col
        {
            get
            {
                return col;
            }
        }

        public GridPosition(int row, int column)
        {
            this.row = column;
            this.col = row;
        }
    }
}
