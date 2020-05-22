namespace Models
{
    public class Tile
    {
        private readonly int column;
        private readonly int row;

        public Tile(int row, int column)
        {
            this.column = column;
            this.row = row;
        }

        public int Column => column;

        public int Row => row;
    }
}