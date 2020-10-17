using game_state_enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testing_v2.Screens
{
    class StatsPage
    {
        #region MemberVariables
        // Screen object that abstracts 99% of the work from this object
        Screen _screen = new Screen();

        #endregion

        // Enum that lets us detect what screen the user has selected
        public CorePage SelectedCorePage { get; set; }

        // Define grid layout for this screen
        private void DesignScreenLayout()
        {
            // 30 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into four rows
            _screen.AddRow(8 * defaultHeight); // row 0
            _screen.AddRow(8 * defaultHeight); // row 1
            _screen.AddFinalRow(); // row 2


            // Divide screen width within rows into columns (there are 12 column units to divide)

            // Add columns to row #1 (index 0)

            _screen.AddColumn(0, 4); // back button goes here
            _screen.AddColumn(0, 4);
            _screen.AddColumn(0, 4);

            // Add columns to row #2 (index 1)
            _screen.AddColumn(1, 4);
            _screen.AddColumn(1, 4); // Text can go here
            _screen.AddColumn(1, 4);

        }

        private void CreateAndPlaceElements(Texture2D mmButtonTexture, SpriteFont mmButtonFont)
        {
            // Create Button Objects
            Controls.Button backButton = new Controls.Button(mmButtonTexture, mmButtonFont) { Text = "Back" };


            // Assign event handlers for the buttons (so they actually do something)
            backButton.Click += BackButton_Click;

            // Place button objects (row and col indices gotten from DesignScreenLayout() function above)

            _screen.Place(backButton, 0, 0);

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            SelectedCorePage = CorePage.Menu;
        }

        // Constructor
        public StatsPage(Texture2D mmButtonTexture, SpriteFont mmButtonFont)
        {
            SelectedCorePage = CorePage.Stats;

            // Divide the grid of the screen into rows and columns
            DesignScreenLayout();

            // Create and place the objects needed for this page
            CreateAndPlaceElements(mmButtonTexture, mmButtonFont);

        }

        // Draw for Game
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // The screen object takes care of drawing everything 
            _screen.Draw(gameTime, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            // The screen object takes care of updating everything 
            _screen.Update(gameTime);
        }
    }
}
