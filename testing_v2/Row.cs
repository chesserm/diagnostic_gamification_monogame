using Android.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testing_v2
{
    // Row class for use in the grid implemented in Screen
    class Row
    {
        #region MemberVariables

        // List whose length corresponds to Columns and each value indicates number
        // of columns being spanned
        List<int> _columns = new List<int>();
        #endregion

        #region Properties
        // Number of columns in the row
        public int NumColumns { get; private set; }

        // Total Number of columns spanned
        public int ColumnsSpanned { get; private set; }
        
        // Height of row
        public int Height { get; private set; }

        // Starting Y position of row
        public int StartY { get; private set; }


        #endregion 

        #region Constructors
        

        public Row(int posY,  int height)
        {
            StartY = posY;
            NumColumns = 0;
            Height = height;
        }
        #endregion

        #region CustomException
        [Serializable]
        class ColumnDoesNotExist : Exception
        {
            public ColumnDoesNotExist()
                : base("Attempted to access a column that does not exist")
            {

            }

        }
        #endregion

        #region Functions
        public void AddCol(int colSpan)
        {
            NumColumns += 1;
            ColumnsSpanned += colSpan;
            _columns.Add(colSpan);
        }

        // Returns the number of column units spanned by the column
        public int getWidthColumn(int col)
        {
            if (col < 0 || col > NumColumns)
            {
                throw new ColumnDoesNotExist();
            }

            // If there is no column yet, the only column spans the whole row
            if (NumColumns == 0)
            {
                return -1;
            }

            // Normal case, return columns spanned by col
            return _columns[col];
        }

        public int getWidthBeforeCol(int col)
        {
            if (col < 0 || col > NumColumns)
            {
                throw new ColumnDoesNotExist();
            }

            int width = 0;
            for (int i = 0; i < col; ++i)
            {
                width += _columns[i];
            }

            return width;
        }
        #endregion

    }
}
