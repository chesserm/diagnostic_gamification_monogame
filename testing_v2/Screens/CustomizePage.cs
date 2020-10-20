using System;
using System.Collections.Generic;
using System.Text;
using game_state_enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testing_v2.Screens
{
    class CustomizePage
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
            _screen.AddRow(2 * defaultHeight); // row 0
            _screen.AddRow(16 * defaultHeight); // row 1 sprite
            _screen.AddRow(4 * defaultHeight); // row 2 items
            _screen.AddFinalRow(); // row 3 instructions


            // Divide screen width within rows into columns (there are 12 column units to divide)

            // Add columns to row #1 (index 0)
            _screen.AddColumn(0, 4); // back button goes here
            _screen.AddColumn(0, 8); //title
            
            // Add columns to row #2 (index 1) sprite
            _screen.AddColumn(1, 12);

            // Add columns to row #3 (index 2) items
            _screen.AddColumn(2, 4); //mask
            _screen.AddColumn(2, 4); //hat
            _screen.AddColumn(2, 4); //stethoscope

            // Add columns to row #4 (index 3) instructions
            _screen.AddColumn(3, 12);



        }

        private void CreateAndPlaceElements(Texture2D mmButtonTexture, SpriteFont mmButtonFont, Dictionary<string, ShopItem> storeitems, Texture2D sprite)
        {
            // Create Button Objects
            Controls.Button backButton = new Controls.Button(mmButtonTexture, mmButtonFont) { Text = "Back" };
            Controls.Button titleButton = new Controls.Button(mmButtonTexture, mmButtonFont) { Text = "Customize" };
            Controls.Button instaButton = new Controls.Button(mmButtonTexture, mmButtonFont) { Text = "Click on an item to add to your avatar" };

            Controls.Button sprite1 = new Controls.Button(sprite, mmButtonFont);

            //Controls.Button monkey = new Controls.Button(storeitems["monkey"].ComponentTexture, mmButtonFont);
            Controls.Button blackST = new Controls.Button(storeitems["blackST"].ComponentTexture, mmButtonFont);
            //Controls.Button silverST = new Controls.Button(storeitems["silverST"].ComponentTexture, mmButtonFont);
            //Controls.Button goldST = new Controls.Button(storeitems["goldST"].ComponentTexture, mmButtonFont);
            Controls.Button mask = new Controls.Button(storeitems["mask"].ComponentTexture, mmButtonFont);
            Controls.Button hat = new Controls.Button(storeitems["hat"].ComponentTexture, mmButtonFont);

            Controls.Button layover_blackST = new Controls.Button(storeitems["layoverBST"].ComponentTexture, mmButtonFont);
            Controls.Button layover_mask = new Controls.Button(storeitems["layovermask"].ComponentTexture, mmButtonFont);
            Controls.Button layover_hat = new Controls.Button(storeitems["hat"].ComponentTexture, mmButtonFont);
            // Assign event handlers for the buttons (so they actually do something)
            backButton.Click += BackButton_Click;

            blackST.Click += (sender, EventArgs) => { ButtonBlackST_Click(sender, EventArgs, layover_mask); };
            
            hat.Click += (sender, EventArgs) => { ButtonHat_Click(sender, EventArgs, layover_hat); };

            mask.Click += (sender, EventArgs) => { ButtonMask_Click(sender, EventArgs, layover_blackST); };

            
            // Place button objects (row and col indices gotten from DesignScreenLayout() function above)

            _screen.Place(backButton, 0, 0);
            _screen.Place(titleButton, 0, 1);

            _screen.Place(sprite1, 1, 0);

            _screen.Place(blackST, 2, 0);
            _screen.Place(mask, 2, 1);
            _screen.Place(hat, 2, 2);

            _screen.Place(instaButton, 3, 0);

            //_screen.Place(layover_hat, 1, 0);
            //_screen.Place(layover_blackST, 1, 0);
            //_screen.Place(layover_mask, 1, 0);

        }

        void ButtonMask_Click(object sender, EventArgs e, Controls.Button layover_mask)
        {
            _screen.Place(layover_mask, 1, 0);
        }

        void ButtonHat_Click(object sender, EventArgs e, Controls.Button layover_hat)
        {
            _screen.Place(layover_hat, 1, 0);
        }

        void ButtonBlackST_Click(object sender, EventArgs e, Controls.Button layover_blackST)
        {
            _screen.Place(layover_blackST, 1, 0);
        }


        private void BackButton_Click(object sender, EventArgs e)
        {
            SelectedCorePage = CorePage.Menu;
        }

        // Constructor
        public CustomizePage(Texture2D mmButtonTexture, SpriteFont mmButtonFont, Dictionary<string, ShopItem> playeritems, Texture2D sprite)
        {
            SelectedCorePage = CorePage.Customize;

            // Divide the grid of the screen into rows and columns
            DesignScreenLayout();

            // Create and place the objects needed for this page
            CreateAndPlaceElements(mmButtonTexture, mmButtonFont, playeritems, sprite);

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
