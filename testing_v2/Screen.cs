using Android.Bluetooth;
using Android.Widget;
using Java.Security;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testing_v2
{
    class Screen
    {
        #region Dimensions
        // Dimensions (hardcoded to this resolution, scaling should take care of the rest)
        public static int screenHeight = 1366;
        public static int screenWidth = 768;

        public static int MAX_NUM_COLS = 12;
        public static int MAX_NUM_ROWS = 24;

        public static int COLUMN_WIDTH = screenWidth / MAX_NUM_COLS;
        
        public int numColumns = 1;
        public int numRows = 1;

        public static int defaultRowHeight = screenHeight / MAX_NUM_ROWS;
        #endregion

        #region MemberVariables
        // List of components that need to be drawn
        List<Component> _elements = new List<Component>();
        
        // List of row objects
        List<Row> _rows = new List<Row>();

        // Y position of the next row's starting Y coordinate  
        int _nextRowY = 0;

        #endregion

        #region CustomExceptions
        [Serializable]
        class MaximumColumnNumberExceeded : Exception
        {
            public MaximumColumnNumberExceeded()
                : base("Attempted to add columns excceding MAX_COLUMNS")
            {

            }

        }

        [Serializable]
        class ScreenHeightExceedingMaxHeight : Exception
        {
            public ScreenHeightExceedingMaxHeight()
                : base("Attempted to add row excceding height of device screen")
            {

            }

        }
        #endregion

        #region Rows
        public void AddRow()
        { 
            // Check if trying to add stuff off screen
            if (_nextRowY + defaultRowHeight > screenHeight)
            {
                throw new ScreenHeightExceedingMaxHeight();
            }


            // Add row object
            _rows.Add(new Row(_nextRowY, defaultRowHeight));

            // Update Row count
            numRows += 1;

            // Update next row position
            _nextRowY += defaultRowHeight;
        }

        public void AddRow(int height)
        {
            // Check if trying to add stuff off screen
            if (_nextRowY + height > screenHeight)
            {
                throw new ScreenHeightExceedingMaxHeight();
            }

            // Add new row object to list
            _rows.Add(new Row(_nextRowY, height));

            // Increment number of rows
            numRows += 1;

            // Update position of next row's starting Y coordinate 
            _nextRowY += height;

        }

        public void AddFinalRow()
        {
            int remainingHeight = screenHeight - _nextRowY;

            // Check if trying to add stuff off screen or invisible
            if (remainingHeight <= 0)
            {
                throw new ScreenHeightExceedingMaxHeight();
            }

            // Add row object
            _rows.Add(new Row(_nextRowY, remainingHeight));

            // Update Row count
            numRows += 1;

            // Update next row position
            _nextRowY += remainingHeight;
        }

        #endregion

        #region Columns
        // Adds a column to row "row" that spans "numColSpan" columns [1,12]
        public void AddColumn(int row, int numColSpan)
        {
            // Check for validity of adding another column 
            if (_rows[row].ColumnsSpanned + numColSpan > MAX_NUM_COLS)
            {
                throw new MaximumColumnNumberExceeded();
            }

            _rows[row].AddCol(numColSpan);
        }
        #endregion

        #region Functions

        // Default constructor
        public Screen()
        {

        }

        // Do the calculations to determine position and area of component's rectangle on screen
        public Rectangle GetPositionRectangle(int row, int col)
        {
            int x = _rows[row].getWidthBeforeCol(col) * COLUMN_WIDTH;
            int y = _rows[row].StartY;

            int height = _rows[row].Height;
            int width;

            int widthColumn = _rows[row].getWidthColumn(col);
            if (widthColumn == -1)
            {
                width = screenWidth;
            }
            else
            {
                width = _rows[row].getWidthColumn(col) * COLUMN_WIDTH;
            }
            

            return new Rectangle(x, y, width, height);
        }

        // Place an element starting at grid position (row, column) 
        // and spanning spanRow rows and spanCol columns
        public void Place(Component element, int row, int col)
        {
            // Set the Rectangle property of the component to correspond to the
            // block in the grid determined by the grid system in Screen
            element.Rectangle = GetPositionRectangle(row, col);
            
            // Add element to the _elements array of components
            _elements.Add(element);
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw each element in the _elements list of Components
            int num_elements = _elements.Count;
            for (int i = 0; i < num_elements; ++i)
            {
                _elements[i].Draw(gameTime, spriteBatch);
            }
        }


        public void Update(GameTime gameTime)
        {
            // Draw each element in the _elements list of Components
            int num_elements = _elements.Count;
            for (int i = 0; i < num_elements; ++i)
            {
                _elements[i].Update(gameTime);
            }
        }

        #endregion

    }
}
