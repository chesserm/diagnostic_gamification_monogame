﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Org.Apache.Http.Cookies;
using System;
using System.Collections.Specialized;
using System.Linq;
using game_state_enums;
using testing_v2.Screens;

namespace testing_v2
{
    public class Game1 : Game
    {
        // Variables given to us (spritebatch is in charge of drawing ALL visuals)
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // The screen resolution we are designing for (scaling will adjust for variation)
        int screenHeight = 1366;
        int screenWidth = 768;

        // Old textures (one created for each button) that are no longer needed
        #region SpecificTexturesNotNeeded
        //Texture2D play_button;
        //Texture2D shop_button;
        //Texture2D customize_button;
        //Texture2D stats_button;
        //Texture2D back_button;

        //Texture2D background_box;

        //Texture2D chf_button;
        //Texture2D copd_button;
        //Texture2D pneumonia_button;

        //Texture2D diagnose_button;
        //Texture2D investigate_button;

        //Texture2D general_exam_button;
        //Texture2D examine_head_button;
        //Texture2D examine_neck_button;
        //Texture2D examine_abdomen_button;
        //Texture2D examine_extremities_button;
        //Texture2D examine_lungs_button;
        //Texture2D examine_oxygen_button;
        //Texture2D examine_skin_button;
        //Texture2D order_blood_button;
        //Texture2D order_imaging_button;

        //static int menuButtonWidth = 200;
        //static int menuButtonHeight = 100;
        //static int backButtonWidth = 100;
        //static int backButtonHeight = 50;
        #endregion
        

        // Enums to track game state
        #region GameStateEnums

        CoreState currentCorePage = CoreState.Menu;
        PlayState currentPlayPage = PlayState.Initial;
        SymptomState currentSymptom = SymptomState.Nothing;
        #endregion

        // Content used (buttons, sprites, fonts, etc)
        #region ContentVariables
        SpriteFont gameTextFont;
        Texture2D button;
        Texture2D monkey;
        #endregion

        // Screens/Pages for our game
        #region GamePages
        MainMenu mainMenuPage;
        ShopPage shopPage;
        CustomizePage customizePage;
        StatsPage statsPage;

        #endregion

        // Variable that detects touch
        TouchCollection tc;

        #region DeterminingCurrentState

        // Determine which page's draw function to call
        public void DeterminePageToDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch(mainMenuPage.SelectedCorePage)
            {
                case CoreState.Menu:
                    {
                        mainMenuPage.Draw(gameTime, spriteBatch);
                        break;
                    }
                case CoreState.Shop:
                    {
                        // Update state in the shop page if it was toggled from returning to main page
                        if (shopPage.SelectedCorePage == CoreState.Menu)
                        {
                            shopPage.SelectedCorePage = CoreState.Shop;
                            mainMenuPage.SelectedCorePage = CoreState.Menu;
                        }
                        else
                        {
                            // Draw Shop Page
                            shopPage.Draw(gameTime, spriteBatch);
                        }

                        
                        
                        break;
                    }
                case CoreState.Customize:
                    {
                        // Update state in the customize page if it was toggled from returning to main page
                        if (customizePage.SelectedCorePage == CoreState.Menu)
                        {
                            customizePage.SelectedCorePage = CoreState.Customize;
                            mainMenuPage.SelectedCorePage = CoreState.Menu;
                        }
                        else
                        {
                            // Draw Customize Page
                            customizePage.Draw(gameTime, spriteBatch);
                        }

                        
                        break;
                    }
                case CoreState.Stats:
                    {
                        // Update state in the stats page if it was toggled from returning to main page
                        if (statsPage.SelectedCorePage == CoreState.Menu)
                        {
                            statsPage.SelectedCorePage = CoreState.Stats;
                            mainMenuPage.SelectedCorePage = CoreState.Menu;
                        }
                        else
                        {
                            // Draw Stats Page
                            statsPage.Draw(gameTime, spriteBatch);
                        }

                        
                        break;
                    }
                case CoreState.Play:
                    {
                        break;
                    }
            }
        }

        // Determine which page to update, and update it
        public void DeterminePageToUpdate(GameTime gameTime)
        {
            switch (mainMenuPage.SelectedCorePage)
            {
                case CoreState.Menu:
                    {
                        mainMenuPage.Update(gameTime);
                        break;
                    }
                case CoreState.Shop:
                    {
                        // Update state in the shop page if it was toggled from returning to main page
                        if (shopPage.SelectedCorePage == CoreState.Menu)
                        {
                            shopPage.SelectedCorePage = CoreState.Shop;
                            mainMenuPage.SelectedCorePage = CoreState.Menu;

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
                            // Update Shop Page
                            shopPage.Update(gameTime);
                        }

                        

                        break;
                    }
                case CoreState.Customize:
                    {
                        // Update state in the customize page if it was toggled from returning to main page
                        if (customizePage.SelectedCorePage == CoreState.Menu)
                        {
                            customizePage.SelectedCorePage = CoreState.Customize;
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
                            statsPage.SelectedCorePage = CoreState.Stats;
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
                        break;
                    }
            }
        }

        #endregion


        // This is the constructor for the game class and is where we can set some parameters
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        // This function is called once when the application opens to initialize certain values (e.g. database connections)
        protected override void Initialize()
        {

            base.Initialize();
        }


        // This function is used to load assets, sounds, etc.
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            monkey = Content.Load<Texture2D>("monkey");

            #region NoLongerNeeded
            //background_box = Content.Load<Texture2D>("background_box");

            //play_button = Content.Load<Texture2D>("play_button");
            //shop_button = Content.Load<Texture2D>("shop_button");
            //customize_button = Content.Load<Texture2D>("customize_button");
            //back_button = Content.Load<Texture2D>("back_button");
            //stats_button = Content.Load<Texture2D>("stats_button");

            //diagnose_button = Content.Load<Texture2D>("diagnose_button");
            //investigate_button = Content.Load<Texture2D>("investigate_button");

            //chf_button = Content.Load<Texture2D>("CHF");
            //copd_button = Content.Load<Texture2D>("COPD");
            //pneumonia_button = Content.Load<Texture2D>("pneumonia");

            //general_exam_button = Content.Load<Texture2D>("general_exam_button");
            //examine_abdomen_button = Content.Load<Texture2D>("examine_abdomen");
            //examine_extremities_button = Content.Load<Texture2D>("extreme_extremities");
            //examine_head_button = Content.Load<Texture2D>("examine_head");
            //examine_lungs_button = Content.Load<Texture2D>("examine_lungs");
            //examine_neck_button = Content.Load<Texture2D>("examine_neck");
            //examine_oxygen_button = Content.Load<Texture2D>("examine_oxygen");
            //examine_skin_button = Content.Load<Texture2D>("examine_skin");

            //order_blood_button = Content.Load<Texture2D>("order_blood_work");
            //order_imaging_button = Content.Load<Texture2D>("ordering_imaging");
            #endregion

            button = Content.Load<Texture2D>("button");

            gameTextFont = Content.Load<SpriteFont>("gameTextFont");

            // TODO: Add your initialization logic here
            mainMenuPage = new MainMenu(button, gameTextFont);
            shopPage = new ShopPage(button, gameTextFont);
            customizePage = new CustomizePage(button, gameTextFont);
            statsPage = new StatsPage(button, gameTextFont);
        }


        // This function runs once each time the game executes (e.g. 60 times per second if game is running at 60 FPS/Hz)
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here
            tc = TouchPanel.GetState();

            // Automatically determine and update correct page
            DeterminePageToUpdate(gameTime);
            //mainMenuPage.Update(gameTime);

            // Check which button is being pressed
            

            base.Update(gameTime);
        }


        // This function runs once each time the game executes, but strictly handles visual changes on the screen
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here inside of the Begin() and End() function calls
            _spriteBatch.Begin();
            //_spriteBatch.Draw(monkey, new Vector2(0, 0), Color.White);
            #region oldcode
            // Main menu
            // Check which button is being pressed
            //switch (currentScreen)
            //{
            //    case Screen.Menu:
            //        {

            //            _spriteBatch.Draw(shop_button, new Vector2(200, 500), Color.White);
            //            _spriteBatch.Draw(customize_button, new Vector2(600, 500), Color.White);
            //            _spriteBatch.Draw(stats_button, new Vector2(200, 800), Color.White);
            //            _spriteBatch.Draw(play_button, new Vector2(600, 800), Color.White);

            //            break;
            //        }
            //    case Screen.Shop:
            //        {
            //            _spriteBatch.Draw(back_button, new Vector2(0, 0), Color.White);
            //            _spriteBatch.DrawString(gameTextFont, "Shop Page", new Vector2(400, 500), Color.Black);
            //            break;
            //        }
            //    case Screen.Customize:
            //        {
            //            _spriteBatch.Draw(back_button, new Vector2(0, 0), Color.White);
            //            _spriteBatch.DrawString(gameTextFont, "Customize Page", new Vector2(400, 500), Color.Black);
            //            break;
            //        }
            //    case Screen.Stats:
            //        {
            //            _spriteBatch.Draw(back_button, new Vector2(0, 0), Color.White);
            //            _spriteBatch.DrawString(gameTextFont, "Stats Page", new Vector2(400, 500), Color.Black);
            //            break;
            //        }
            //    case Screen.Play:
            //        {
            //            // Figure out which step of the diagnostic process the user is in 
            //            switch (diagnosticStep)
            //            {
            //                case PlayScreen.Initial:
            //                    {
            //                        _spriteBatch.Draw(background_box, new Vector2(100, 100), Color.White);
            //                        _spriteBatch.DrawString(gameTextFont, "Initial Presentation Information", new Vector2(275, 150), Color.Black);

            //                        _spriteBatch.DrawString(gameTextFont, "Age: 74", new Vector2(150, 300), Color.Black);
            //                        _spriteBatch.DrawString(gameTextFont, "Gender: Male", new Vector2(150, 350), Color.Black);

            //                        _spriteBatch.DrawString(gameTextFont, "Description of Symptoms:", new Vector2(150, 450), Color.Black);
            //                        _spriteBatch.DrawString(gameTextFont, "Chest Heaviness", new Vector2(200, 500), Color.Black);

            //                        _spriteBatch.DrawString(gameTextFont, "Severity of Symptoms: Severe", new Vector2(150, 600), Color.Black);

            //                        _spriteBatch.DrawString(gameTextFont, "Onset of Symptoms: 3 days", new Vector2(150, 650), Color.Black);

            //                        _spriteBatch.DrawString(gameTextFont, "Duration of Symptoms: Constant", new Vector2(150, 700), Color.Black);

            //                        _spriteBatch.DrawString(gameTextFont, "Provocating Factors:", new Vector2(150, 800), Color.Black);
            //                        _spriteBatch.DrawString(gameTextFont, "Exertion", new Vector2(200, 850), Color.Black);

            //                        _spriteBatch.DrawString(gameTextFont, "Relieving Factors:", new Vector2(150, 900), Color.Black);
            //                        _spriteBatch.DrawString(gameTextFont, "None", new Vector2(200, 950), Color.Black);


            //                        _spriteBatch.DrawString(gameTextFont, "Past Medical History: ", new Vector2(150, 1050), Color.Black);
            //                        _spriteBatch.DrawString(gameTextFont, "Heart Failure", new Vector2(200, 1100), Color.Black);
            //                        _spriteBatch.DrawString(gameTextFont, "Cornary Atery Disease", new Vector2(200, 1150), Color.Black);
            //                        _spriteBatch.DrawString(gameTextFont, "COPD", new Vector2(200, 1200), Color.Black);
            //                        _spriteBatch.DrawString(gameTextFont, "Current Tobacco Use", new Vector2(200, 1250), Color.Black);


            //                        _spriteBatch.Draw(play_button, new Vector2(300, 1400), Color.White);



            //                        break;
            //                    }
            //                case PlayScreen.Main:
            //                    {
            //                        //_spriteBatch.DrawString(gameTextFont, "Main Play Page", new Vector2(400, 500), Color.Black);

            //                        _spriteBatch.Draw(monkey, new Vector2(250, 350), Color.White);

            //                        _spriteBatch.Draw(diagnose_button, new Vector2(200, 1200), Color.White);
            //                        _spriteBatch.Draw(investigate_button, new Vector2(600, 1200), Color.White);


            //                        break;
            //                    }
            //                case PlayScreen.SymptomList:
            //                    {
            //                        // Check which symptom is being investigated
            //                        switch (currentSymptom)
            //                        {
            //                            case Symptom.Nothing:
            //                                {
            //                                    _spriteBatch.Draw(back_button, new Vector2(0, 0), Color.White);

            //                                    _spriteBatch.Draw(background_box, new Vector2(100, 100), Color.White);
            //                                    _spriteBatch.DrawString(gameTextFont, "Select a Symptom to Investigate", new Vector2(275, 150), Color.Black);

            //                                    _spriteBatch.Draw(general_exam_button, new Vector2(200, 400), Color.White);
            //                                    _spriteBatch.Draw(examine_head_button, new Vector2(200, 550), Color.White);
            //                                    _spriteBatch.Draw(examine_neck_button, new Vector2(200, 700), Color.White);
            //                                    _spriteBatch.Draw(examine_lungs_button, new Vector2(200, 850), Color.White);
            //                                    _spriteBatch.Draw(examine_extremities_button, new Vector2(200, 1000), Color.White);
            //                                    _spriteBatch.Draw(examine_abdomen_button, new Vector2(200, 1150), Color.White);
            //                                    _spriteBatch.Draw(order_blood_button, new Vector2(200, 1300), Color.White);
            //                                    _spriteBatch.Draw(order_imaging_button, new Vector2(200, 1450), Color.White);
            //                                    break;
            //                                }
            //                            case Symptom.General:
            //                                {
            //                                    _spriteBatch.Draw(background_box, new Vector2(100, 100), Color.White);
            //                                    _spriteBatch.DrawString(gameTextFont, "Results of General Exam", new Vector2(275, 150), Color.Black);

            //                                    _spriteBatch.DrawString(gameTextFont, "Vitals:", new Vector2(150, 350), Color.Black);
            //                                    _spriteBatch.DrawString(gameTextFont, "Temperature: 38.4", new Vector2(200, 400), Color.Black);
            //                                    _spriteBatch.DrawString(gameTextFont, "Heart Rate: 121", new Vector2(200, 450), Color.Black);
            //                                    _spriteBatch.DrawString(gameTextFont, "Respiratory Rate: 121", new Vector2(200, 500), Color.Black);
            //                                    _spriteBatch.DrawString(gameTextFont, "Blood Pressure: 104/53", new Vector2(200, 550), Color.Black);

            //                                    _spriteBatch.DrawString(gameTextFont, "General Observations:", new Vector2(150, 700), Color.Black);
            //                                    _spriteBatch.DrawString(gameTextFont, "Awake", new Vector2(200, 750), Color.Black);
            //                                    _spriteBatch.DrawString(gameTextFont, "Alert", new Vector2(200, 800), Color.Black);
            //                                    _spriteBatch.DrawString(gameTextFont, "Oriented x2", new Vector2(200, 850), Color.Black);

            //                                    _spriteBatch.Draw(back_button, new Vector2(750, 125), Color.White);

            //                                    break;
            //                                }
            //                            case Symptom.Head:
            //                                {
            //                                    break;
            //                                }
            //                            case Symptom.Neck:
            //                                {
            //                                    break;
            //                                }
            //                            case Symptom.Lungs:
            //                                {
            //                                    break;
            //                                }
            //                            case Symptom.Extremities:
            //                                {
            //                                    break;
            //                                }
            //                            case Symptom.Abdomen:
            //                                {
            //                                    break;
            //                                }
            //                            case Symptom.Oxygen:
            //                                {
            //                                    break;
            //                                }
            //                            case Symptom.Imaging:
            //                                {
            //                                    break;
            //                                }
            //                            default:
            //                                {
            //                                    break;
            //                                }
            //                        }



            //                        break;
            //                    }
            //                case PlayScreen.SymptomInfo:
            //                    {
            //                        _spriteBatch.Draw(back_button, new Vector2(0, 0), Color.White);
            //                        _spriteBatch.DrawString(gameTextFont, "Symtom Info Play Page", new Vector2(400, 500), Color.Black);
            //                        break;
            //                    }
            //                case PlayScreen.Reasoning:
            //                    {
            //                        _spriteBatch.Draw(back_button, new Vector2(0, 0), Color.White);
            //                        _spriteBatch.DrawString(gameTextFont, "Reasoning Play Page", new Vector2(400, 500), Color.Black);
            //                        break;
            //                    }
            //                case PlayScreen.Diagnose:
            //                    {
            //                        _spriteBatch.Draw(back_button, new Vector2(0, 0), Color.White);
            //                        _spriteBatch.DrawString(gameTextFont, "Diagnose Play Page", new Vector2(400, 500), Color.Black);
            //                        break;
            //                    }
            //                case PlayScreen.Summary:
            //                    {
            //                        _spriteBatch.Draw(back_button, new Vector2(0, 0), Color.White);
            //                        _spriteBatch.DrawString(gameTextFont, "Summary Play Page", new Vector2(400, 500), Color.Black);
            //                        break;
            //                    }
            //                default:
            //                    {
            //                        break;
            //                    }
            //            }

            //            break;
            //        }
            //    default:
            //        break;
            //}
            #endregion

            //Automatically determine which page to draw
            DeterminePageToDraw(gameTime, _spriteBatch);

            //mainMenuPage.Draw(gameTime, _spriteBatch);




            //_spriteBatch.DrawString(gameTextFont, "Hello World!", new Vector2(100, 100), Color.Black);


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
