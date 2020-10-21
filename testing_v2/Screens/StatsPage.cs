using game_state_enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using testing_v2.Managers;

namespace testing_v2.Screens
{
    class StatsPage
    {
        #region MemberVariables
        // Screen object that abstracts 99% of the work from this object
        Screen _screen = new Screen();
        PlayerManager _playerManager;

        Texture2D _buttonTexture;
        SpriteFont _font;
        #endregion

        // Enum that lets us detect what screen the user has selected
        public CoreState SelectedCorePage { get; set; }

        // Define grid layout for this screen
        private void DesignScreenLayout()
        {
            // 30 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into four rows
            _screen.AddRow(8 * defaultHeight); // row 0
            _screen.AddRow(8 * defaultHeight); // row 1
            _screen.AddRow(8 * defaultHeight); // row 2
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

            _screen.AddColumn(2, 4);
            _screen.AddColumn(2, 4); // Text can go here
            _screen.AddColumn(2, 4);

        }

        private void CreateAndPlaceElements(Texture2D mmButtonTexture, SpriteFont mmButtonFont)
        {
            // Create Button Objects
            Controls.Button backButton = new Controls.Button(mmButtonTexture, mmButtonFont) { Text = "Back" };
            Controls.Button addcorrectcopd = new Controls.Button(mmButtonTexture, mmButtonFont) { Text = "Correct COPD" };
            Controls.Button addcorrectpneumonia = new Controls.Button(mmButtonTexture, mmButtonFont) { Text = "Correct Pnuemonia" };
            Controls.Button addcorrectchf = new Controls.Button(mmButtonTexture, mmButtonFont) { Text = "Correct CHF" };
            Controls.Button addincorrectcopd = new Controls.Button(mmButtonTexture, mmButtonFont) { Text = "Incorrect COPD" };
            Controls.Button addincorrectpneumonia = new Controls.Button(mmButtonTexture, mmButtonFont) { Text = "Incorrect Pnuemonia" };
            Controls.Button addincorrectchf = new Controls.Button(mmButtonTexture, mmButtonFont) { Text = "Incorrect CHF" };

            Controls.Textbox displayCoins = new Controls.Textbox(mmButtonFont, $"User coin balance: {_playerManager.Player.NumCoins}");

            _screen.Place(displayCoins, 0, 2);

            // Assign event handlers for the buttons (so they actually do something)
            backButton.Click += BackButton_Click;
            addcorrectcopd.Click += addcorrectcopd_Click;
            addcorrectpneumonia.Click += addcorrectpneumonia_Click;
            addcorrectchf.Click += addcorrectchf_Click;
            addincorrectcopd.Click += addincorrectcopd_Click;
            addincorrectpneumonia.Click += addincorrectpneumonia_Click;
            addincorrectchf.Click += addincorrectchf_Click;
            // Place button objects (row and col indices gotten from DesignScreenLayout() function above)

            _screen.Place(backButton, 0, 0);
            _screen.Place(addcorrectcopd, 1, 0);
            _screen.Place(addcorrectchf, 1, 1);
            _screen.Place(addcorrectpneumonia, 1, 2);
            _screen.Place(addincorrectcopd, 2, 0);
            _screen.Place(addincorrectchf, 2, 1);
            _screen.Place(addincorrectpneumonia, 2, 2);


        }

        

        private void BackButton_Click(object sender, EventArgs e)
        {
            SelectedCorePage = CoreState.Menu;

        }

        private void addcorrectcopd_Click(object sender, EventArgs e)
        {
            _playerManager.caseComplete(true, 'c');
            UpdatePage();
        }
        private void addincorrectcopd_Click(object sender, EventArgs e)
        {
            _playerManager.caseComplete(false, 'c');
        }
        private void addcorrectchf_Click(object sender, EventArgs e)
        {
            _playerManager.caseComplete(true, 'h');
        }
        private void addincorrectchf_Click(object sender, EventArgs e)
        {
            _playerManager.caseComplete(false, 'h');
        }
        private void addcorrectpneumonia_Click(object sender, EventArgs e)
        {
            _playerManager.caseComplete(true, 'p');
        }
        private void addincorrectpneumonia_Click(object sender, EventArgs e)
        {
            _playerManager.caseComplete(false, 'p');
        }

        // Constructor
        public StatsPage(Texture2D mmButtonTexture, SpriteFont mmButtonFont, PlayerManager playerManagerin)
        {
            SelectedCorePage = CoreState.Stats;
            _playerManager = playerManagerin;

            _buttonTexture = mmButtonTexture;
            _font = mmButtonFont;

            // Divide the grid of the screen into rows and columns
            DesignScreenLayout();

            // Create and place the objects needed for this page
            CreateAndPlaceElements(mmButtonTexture, mmButtonFont);

        }

        public void UpdatePage()
        {
            _screen = new Screen();
            DesignScreenLayout();
            CreateAndPlaceElements(_buttonTexture, _font);
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
