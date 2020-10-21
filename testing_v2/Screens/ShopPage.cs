using System;
using System.Collections.Generic;
using System.Text;
using game_state_enums;
using testing_v2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testing_v2.Screens
{
    class ShopPage
    {
        #region MemberVariables
        // Screen object that abstracts 99% of the work from this object
        Screen _screen = new Screen();
        
        #endregion



        public Dictionary<string, ShopItem> Images { get; set; }
        public SpriteFont MmButtonFont { get; set; }

        // Enum that lets us detect what screen the user has selected
        public CoreState SelectedCorePage { get; set; }

        // Define grid layout for this screen
        private void DesignScreenLayout()
        {
            // 30 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into four rows
            _screen.AddRow(2 * defaultHeight); // row 0  title /back button
            _screen.AddRow(8 * defaultHeight); // row 1  row one of shopitems
            _screen.AddRow(1 * defaultHeight); // row 2  prices
            _screen.AddRow(8 * defaultHeight); // row 3  row two of shop items
            _screen.AddRow(2 * defaultHeight); // row 4  prices
            _screen.AddFinalRow();             // row 5  insturction line


            // Divide screen width within rows into columns (there are 12 column units to divide)

            // Add columns to row #1 (index 0)

            _screen.AddColumn(0, 4); // back button goes here
            _screen.AddColumn(0, 4); 
            _screen.AddColumn(0, 4); 

            // Add columns to row #2 (index 1)
            _screen.AddColumn(1, 4); 
            _screen.AddColumn(1, 4); // Text can go here
            _screen.AddColumn(1, 4);

            // Add columns to row #3 (index 2)
            _screen.AddColumn(2, 4);
            _screen.AddColumn(2, 4); // Text can go here
            _screen.AddColumn(2, 4);

            // Add columns to row #4 (index 3)
            _screen.AddColumn(3, 4);
            _screen.AddColumn(3, 4); // Text can go here
            _screen.AddColumn(3, 4);

            // Add columns to row #5 (index 4)
            _screen.AddColumn(4, 4);
            _screen.AddColumn(4, 4); // Text can go here
            _screen.AddColumn(4, 4);

            // Add columns to row #6 (index 5)
            _screen.AddColumn(5, 12);

        }

        private void CreateAndPlaceElements(Texture2D mmButtonTexture)
        {
            // Create Button Objects
            Controls.Button backButton = new Controls.Button(mmButtonTexture, MmButtonFont) { Text = "Back" };
            Controls.Button shopButton = new Controls.Button(mmButtonTexture, MmButtonFont) { Text = "Shop" };
            Controls.Button InstrButton = new Controls.Button(mmButtonTexture, MmButtonFont) { Text = "Click on price to buy an item!" };


            Controls.Button blackSTprice = new Controls.Button(mmButtonTexture, MmButtonFont) { Text = "$" + Images["blackST"].Price.ToString() };
            Controls.Button silverSTprice = new Controls.Button(mmButtonTexture, MmButtonFont) { Text = "LOCKED!!" };
            //TODO how to unlock - show that to user
            Controls.Button goldSTprice = new Controls.Button(mmButtonTexture, MmButtonFont) { Text = "LOCKED!!" };
            Controls.Button maskprice = new Controls.Button(mmButtonTexture, MmButtonFont) { Text = "$" + Images["mask"].Price.ToString() };
            Controls.Button hatprice = new Controls.Button(mmButtonTexture, MmButtonFont) { Text = "$" + Images["hat"].Price.ToString() };
            Controls.Button monkeyprice = new Controls.Button(mmButtonTexture, MmButtonFont) { Text = "$" + Images["monkey"].Price.ToString() };
            //

            Controls.Button monkey = new Controls.Button(Images["monkey"].ComponentTexture, MmButtonFont);
            Controls.Button blackST = new Controls.Button(Images["blackST"].ComponentTexture, MmButtonFont);
            Controls.Button silverST = new Controls.Button(Images["silverST"].ComponentTexture, MmButtonFont);
            Controls.Button goldST = new Controls.Button(Images["goldST"].ComponentTexture, MmButtonFont);
            Controls.Button mask = new Controls.Button(Images["mask"].ComponentTexture, MmButtonFont);
            Controls.Button hat = new Controls.Button(Images["hat"].ComponentTexture, MmButtonFont);
            // Assign event handlers for the buttons (so they actually do something)
            backButton.Click += BackButton_Click;

            blackSTprice.Click += BlackSTprice_Click;
            silverSTprice.Click += SilverSTprice_Click;
            goldSTprice.Click += GoldSTprice_Click;
            maskprice.Click += Maskprice_Click;
            hatprice.Click += Hatprice_Click;
            monkeyprice.Click += Monkeyprice_Click;

            
            

            // Place button objects (row and col indices gotten from DesignScreenLayout() function above)

            _screen.Place(backButton, 0, 0);
            _screen.Place(shopButton, 0, 1);
            _screen.Place(blackST, 1, 0);
            _screen.Place(silverST, 1, 1);
            _screen.Place(goldST, 1, 2);

            _screen.Place(blackSTprice, 2, 0);
            _screen.Place(silverSTprice, 2, 1);
            _screen.Place(goldSTprice, 2, 2);

            _screen.Place(mask, 3, 0);
            _screen.Place(hat, 3, 1);
            _screen.Place(monkey, 3, 2);

            _screen.Place(maskprice, 4, 0);
            _screen.Place(hatprice, 4, 1);
            _screen.Place(monkeyprice, 4, 2);

            _screen.Place(InstrButton, 5, 0);



        }

        private void Monkeyprice_Click(object sender, EventArgs e)
        {
            //CLICK HANDLING STEPS
            //IF the item id isnt aleady in list / grayed
            //append item id into player item list
            //player class update coins based on item.price
            //change item button color to gray
            //Alert player that they purchased item
        }

        private void Hatprice_Click(object sender, EventArgs e)
        {
            
        }

        private void Maskprice_Click(object sender, EventArgs e)
        {
            
        }

        private void GoldSTprice_Click(object sender, EventArgs e)
        {
            //locked for now
        }

        private void SilverSTprice_Click(object sender, EventArgs e)
        {
            //locked for now
        }

        private void BlackSTprice_Click(object sender, EventArgs e)
        {
            
        }




        private void BackButton_Click(object sender, EventArgs e)
        {
            SelectedCorePage = CoreState.Menu;
        }

        // Constructor
        public ShopPage(Texture2D mmButtonTexture, SpriteFont mmButtonFont, Dictionary<string, ShopItem> images)
        {
            SelectedCorePage = CoreState.Shop;
            Images = images;
            MmButtonFont = mmButtonFont;

            // Divide the grid of the screen into rows and columns
            DesignScreenLayout();

            // Create and place the objects needed for this page
            CreateAndPlaceElements(mmButtonTexture);

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
