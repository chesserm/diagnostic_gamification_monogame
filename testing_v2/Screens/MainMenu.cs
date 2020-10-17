using Android.Nfc.CardEmulators;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using testing_v2.Controls;
using game_state_enums;

namespace testing_v2.Screens
{

    class MainMenu
    {
        #region MemberVariables
        // Screen object that abstracts 99% of the work from this object
        Screen _screen = new Screen();



        #endregion

        // Enum that lets us detect what screen the user has selected
        public CorePage SelectedCorePage { get; set; }

        #region HelperFunctions
        // Define grid layout for this screen
        private void DesignScreenLayout()
        {
            // 30 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into four rows
            _screen.AddRow(2 * defaultHeight); // row 0
            _screen.AddRow(8 * defaultHeight); // row 1
            _screen.AddRow(1 * defaultHeight); // row 2
            _screen.AddRow(8 * defaultHeight); // row 3
            _screen.AddRow(2 * defaultHeight); // row 4
            _screen.AddFinalRow();             // row 5


            // Divide screen width within rows into columns (there are 12 column units to divide)

            // Add columns to row #2 (index 1)
            _screen.AddColumn(1, 2);
            _screen.AddColumn(1, 4); // One button goes here (col 1)
            _screen.AddColumn(1, 4); // one button goes here (col 2)
            _screen.AddColumn(1, 2);

            // Add columns to row #3 (index 2)
            _screen.AddColumn(3, 2);
            _screen.AddColumn(3, 4); // one button goes here (col 1)
            _screen.AddColumn(3, 4); // one button goes here (col 2)
            _screen.AddColumn(3, 2); 

        }

        private void CreateAndPlaceElements(Texture2D mmButtonTexture, SpriteFont mmButtonFont)
        {
            // Create Button Objects
            Controls.Button playButton = new Controls.Button(mmButtonTexture, mmButtonFont) { Text = "Play" };
            Controls.Button shopButton = new Controls.Button(mmButtonTexture, mmButtonFont) { Text = "Shop" };
            Controls.Button customizeButton = new Controls.Button(mmButtonTexture, mmButtonFont) { Text = "Customize" };
            Controls.Button statsButton = new Controls.Button(mmButtonTexture, mmButtonFont) {Text = "Statistics"};

            // Assign event handlers for the buttons (so they actually do something)
            playButton.Click += PlayButton_Click;
            shopButton.Click += ShopButton_Click;
            customizeButton.Click += CustomizeButton_Click;
            statsButton.Click += StatsButton_Click;

            // Place button objects (row and col indices gotten from DesignScreenLayout() function above)
            _screen.Place(shopButton, 1, 1);
            _screen.Place(customizeButton, 1, 2);
            _screen.Place(statsButton, 3, 1);
            _screen.Place(playButton, 3, 2);

        }

        #region ButtonEventHandlers
        private void StatsButton_Click(object sender, EventArgs e)
        {
            SelectedCorePage = CorePage.Stats;
        }

        private void CustomizeButton_Click(object sender, EventArgs e)
        {
            SelectedCorePage = CorePage.Customize;
        }

        private void ShopButton_Click(object sender, EventArgs e)
        {
            SelectedCorePage = CorePage.Shop;
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            SelectedCorePage = CorePage.Play;
        }
        #endregion

        #endregion

        #region Functions
        // Constructor
        public MainMenu(Texture2D mmButtonTexture, SpriteFont mmButtonFont)
        {
            SelectedCorePage = CorePage.Menu;

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

        #endregion
    }
}
