using System;
using System.Collections.Generic;
using System.Text;
using testing_v2.Screens;
using game_state_enums;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace testing_v2
{
    class GameDriver
    {

        // Page objects
        #region Pages

        MainMenu mainMenuPage;
        ShopPage shopPage;
        CustomizePage customizePage;
        StatsPage statsPage;
        PlayPage playPage;



        #endregion


        #region Assets

        //Dictionary<string, ShopItem> images;

        #endregion

        // Properties
        #region Properties

        // Button Texture
        Texture2D ButtonTexture { get; set; }

        // Texture for the user's avatar
        Texture2D UserAvatarTexture { get; set; }
        
        // Patient Texture
        Texture2D PatientTexture { get; set; }

        // Font for text on the screen
        SpriteFont Font { get; set; }

        // List of shop item images
        List<Texture2D> shopItems;

        #endregion


        // Constructor
        #region Constructor
        public GameDriver(Texture2D buttonTexture, SpriteFont font, Texture2D patientSprite, Texture2D userAvatarSprite, Dictionary<string, ShopItem> imagesDictionary, Texture2D sprite)
        {
            ButtonTexture = buttonTexture;
            Font = font;
            PatientTexture = patientSprite;
            UserAvatarTexture = userAvatarSprite;

            // Initialize all pages using assets provided
            mainMenuPage = new MainMenu(ButtonTexture, Font);
            shopPage = new ShopPage(ButtonTexture, Font, imagesDictionary);
            customizePage = new CustomizePage(ButtonTexture, Font, imagesDictionary, sprite);
            statsPage = new StatsPage(ButtonTexture, Font);
            playPage = new PlayPage(PatientTexture, ButtonTexture, Font);



        }

        #endregion


        // The logic to determine how to switch states is handled in Update()
        // The Draw() function is simple and just relies on Update() working properly. Check there for bugs first
        #region DrawAndUpdate


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (mainMenuPage.SelectedCorePage)
            {
                case CoreState.Menu:
                    {
                        mainMenuPage.Draw(gameTime, spriteBatch);
                        break;
                    }
                case CoreState.Shop:
                    {
                        shopPage.Draw(gameTime, spriteBatch);
                        break;
                    }
                case CoreState.Customize:
                    {
                        customizePage.Draw(gameTime, spriteBatch);
                        break;
                    }
                case CoreState.Stats:
                    {
                        statsPage.Draw(gameTime, spriteBatch);
                        break;
                    }
                case CoreState.Play:
                    {
                        playPage.Draw(gameTime, spriteBatch);
                        break;
                    }
            }

        }

        public void Update(GameTime gameTime)
        {
            switch (mainMenuPage.SelectedCorePage)
            {
                case CoreState.Menu:
                    {
                        // Update this screen (the main menu screen)
                        mainMenuPage.Update(gameTime);
                        break;
                    }
                case CoreState.Shop:
                    {
                        // Update state in the shop page if it was toggled from returning to main page
                        if (shopPage.SelectedCorePage == CoreState.Menu)
                        {
                            // Change state variable to return to main menu
                            mainMenuPage.SelectedCorePage = CoreState.Menu;

                            // Reset shop's state tracker
                            shopPage.SelectedCorePage = CoreState.Shop;

                            /*
                             * Here you would check shopPage.ItemsPurchased and add each item to the player class's list of ids
                             * of purchased items and you would also check the shopPage.TotalCoinsSpent and subtract this value from the player's
                             * balance of coins.
                             * 
                             * Then you'd reset the shopPage.ItemsPurchased to an empty list and shopPage.TotalCoinsSpent to 0 for the next time
                             * the player goes into the shop
                             * 
                             * These variables have to be created. I just made them up for conceptual notes.
                             * 
                             * An alternative, and possibly better way to do this, is to instead set a property in the shopPage (e.g. playercoins) to be
                             * equal to the player's coins before entering the page. This lets you check to make sure they don't spend more than they are
                             * allowed to in the shop and makes the update easier when you return (you just set the player's coins equal to the variable when
                             * you return instead of adding). you would still need to reset this to 0 in the shop page after returning.
                             * 
                             */

                        }
                        else
                        {
                            // User is not done in shop, so Update Shop Page
                            shopPage.Update(gameTime);
                        }

                        break;
                    }
                case CoreState.Customize:
                    {
                        // Update state in the customize page if it was toggled from returning to main page
                        if (customizePage.SelectedCorePage == CoreState.Menu)
                        {
                            // Reset Customize page's state tracker
                            customizePage.SelectedCorePage = CoreState.Customize;

                            // Update global state tracker
                            mainMenuPage.SelectedCorePage = CoreState.Menu;
                        }
                        else
                        {
                            // Update Customize Page
                            customizePage.Update(gameTime);
                        }
                        break;
                    }
                case CoreState.Stats:
                    {
                        // Update state in the stats page if it was toggled from returning to main page
                        if (statsPage.SelectedCorePage == CoreState.Menu)
                        {
                            // Update stats page's state tracker
                            statsPage.SelectedCorePage = CoreState.Stats;

                            // Update global state tracker
                            mainMenuPage.SelectedCorePage = CoreState.Menu;

                            /*
                             * Here you would check statsPage.NumCorrect properties (there would need to be one for each cause)
                             * and set each variable in the corresponding player class to the new values
                             * 
                             * Then you'd just have to make sure the variables in the statsPage class get re-assigned to before 
                             * re-entering the stats page
                             * 
                             * 
                             * These variables have to be created. I just made them up for conceptual notes.
                             * 
                             */
                        }
                        else
                        {
                            // Update Stats Page
                            statsPage.Update(gameTime);
                        }

                        break;
                    }
                case CoreState.Play:
                    {
                        // Check to see if user is done with play loop
                        if (playPage.IsUserDoneWithPlay)
                        {
                            // Reset game loop and state
                            playPage.ResetPlayLoop();

                            // Update global state variable
                            mainMenuPage.SelectedCorePage = CoreState.Menu;
                        }
                        else
                        {
                            playPage.Update(gameTime);
                        }
                        break;
                    }
            }
        }

        #endregion
    }


}
