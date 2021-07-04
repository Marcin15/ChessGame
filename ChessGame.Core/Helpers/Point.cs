using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame.Core
{
    public class Point
    {
        public int RowIndex { get; private set; }
        public int ColumnIndex { get; private set; }
        public Point(int rowIndex, int columnIndex)
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
        }
    }
}
