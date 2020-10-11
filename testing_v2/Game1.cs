using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Specialized;
using System.Linq;



namespace testing_v2
{
    public class Game1 : Game
    {
        // Variables given to us (spritebatch is in charge of drawing ALL visuals)
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Monkey
        Texture2D monkey;
        Texture2D play_button;
        Texture2D shop_button;
        Texture2D customize_button;
        Texture2D stats_button;
        Texture2D back_button;


        SpriteFont gameTextFont;

        static int menuButtonWidth = 200;
        static int menuButtonHeight = 100;
        static int backButtonWidth = 100;
        static int backButtonHeight = 50;


        // Variable that detects touch
        TouchCollection tc;


        // Enum for us to know which screen the user is on
        enum Screen
        {
            Menu,
            Stats,
            Shop,
            Play,
            Customize
        }
        // Variable that tracks whether or not the initial information has been read
        Screen currentScreen = Screen.Menu;



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
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        // This function is used to load assets, sounds, etc.
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            monkey = Content.Load<Texture2D>("monkey");
            
            play_button = Content.Load<Texture2D>("play_button");
            shop_button = Content.Load<Texture2D>("shop_button");
            customize_button = Content.Load<Texture2D>("customize_button");
            back_button = Content.Load<Texture2D>("back_button");
            stats_button = Content.Load<Texture2D>("stats_button");

            gameTextFont = Content.Load<SpriteFont>("gameTextFont");
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

            // Check which button is being pressed
            switch(currentScreen)
            {
                case Screen.Play:
                    {
                        if (tc.Count > 0)
                        {
                            if (tc[0].Position.X < menuButtonWidth && tc[0].Position.Y < menuButtonHeight)
                            {
                                currentScreen = Screen.Menu;
                            }
                        }
                        break;
                    }
                case Screen.Menu:
                    {

                        // Check if the user is touching anywhere on the screen
                        if (tc.Count > 0)
                        {
                            // The buttons on the top row of the 2x2 grid
                            bool is_shop_button = tc[0].Position.X > 200 && tc[0].Position.X < (200 + menuButtonWidth) && tc[0].Position.Y > 500 && tc[0].Position.Y < (500 + menuButtonHeight);
                            bool is_customize_button = tc[0].Position.X > 600 && tc[0].Position.X < (600 + menuButtonWidth) && tc[0].Position.Y > 500 && tc[0].Position.Y < (500 + menuButtonHeight);

                            // The buttons on the bottom row of the 2x2 grid
                            bool is_stats_button = tc[0].Position.X > 200 && tc[0].Position.X < (200 + menuButtonWidth) && tc[0].Position.Y > 800 && tc[0].Position.Y < (800 + menuButtonHeight);
                            bool is_play_button = tc[0].Position.X > 600 && tc[0].Position.X < (600 + menuButtonWidth) && tc[0].Position.Y > 800 && tc[0].Position.Y < (800 + menuButtonHeight);
                            
                            
                            if (is_shop_button)
                            {
                                currentScreen = Screen.Shop;
                            }
                            else if (is_customize_button)
                            {
                                currentScreen = Screen.Customize;
                            }
                            else if (is_stats_button)
                            {
                                currentScreen = Screen.Stats;
                            }
                            else if (is_play_button)
                            {
                                currentScreen = Screen.Play;
                            }
                        } //if
                        break;
                    }
                case Screen.Shop:
                    {
                        if (tc.Count > 0)
                        {
                            if (tc[0].Position.X < menuButtonWidth && tc[0].Position.Y < menuButtonHeight )
                            {
                                currentScreen = Screen.Menu;
                            }
                        }
                        break;
                    }
                case Screen.Customize:
                    {
                        if (tc.Count > 0)
                        {
                            if (tc[0].Position.X < menuButtonWidth && tc[0].Position.Y < menuButtonHeight)
                            {
                                currentScreen = Screen.Menu;
                            }
                        }
                        break;
                    }
                case Screen.Stats:
                    {
                        if (tc.Count > 0)
                        {
                            if (tc[0].Position.X < menuButtonWidth && tc[0].Position.Y < menuButtonHeight)
                            {
                                currentScreen = Screen.Menu;
                            }
                        }
                        break;
                    }
                default:
                    break;
            }

            base.Update(gameTime);
        }


        // This function runs once each time the game executes, but strictly handles visual changes on the screen
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here inside of the Begin() and End() function calls
            _spriteBatch.Begin();
            //_spriteBatch.Draw(monkey, new Vector2(0, 0), Color.White);

            // Main menu
            // Check which button is being pressed
            switch (currentScreen)
            {
                case Screen.Menu:
                    {
                        
                        _spriteBatch.Draw(shop_button, new Vector2(200, 500), Color.White);
                        _spriteBatch.Draw(customize_button, new Vector2(600, 500), Color.White);
                        _spriteBatch.Draw(stats_button, new Vector2(200, 800), Color.White);
                        _spriteBatch.Draw(play_button, new Vector2(600, 800), Color.White);
                        
                        break;
                    }
                case Screen.Shop:
                    {
                        _spriteBatch.Draw(back_button, new Vector2(0, 0), Color.White);
                        break;
                    }
                case Screen.Customize:
                    {
                        _spriteBatch.Draw(back_button, new Vector2(0, 0), Color.White);
                        break;
                    }
                case Screen.Stats:
                    {
                        _spriteBatch.Draw(back_button, new Vector2(0, 0), Color.White);
                        break;
                    }
                case Screen.Play:
                    {
                        _spriteBatch.Draw(back_button, new Vector2(0, 0), Color.White);
                        break;
                    }
                default:
                    break;
            }


            


            //_spriteBatch.DrawString(gameTextFont, "Hello World!", new Vector2(100, 100), Color.Black);
            
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
