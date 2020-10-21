using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Org.Apache.Http.Cookies;
using System;
using System.Collections.Specialized;
using System.Linq;
using game_state_enums;
using testing_v2.Screens;
using testing_v2.Managers;
using Javax.Security.Auth;
using System.Collections.Generic;

namespace testing_v2
{
    public class Game1 : Game
    {
        // Variables given to us (spritebatch is in charge of drawing ALL visuals)
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public PlayerManager _playerManager;

        // The screen resolution we are designing for (scaling will adjust for variation)
        int screenHeight = 1366;
        int screenWidth = 768;

        // Content used (buttons, sprites, fonts, etc)
        #region ContentVariables
        SpriteFont gameTextFont;
        
        Texture2D button;
        Texture2D monkey1;
        Texture2D blackST1;
        Texture2D silverST1;
        Texture2D goldST1;
        Texture2D mask1;
        Texture2D hat1;
        Texture2D sprite;

        Texture2D layover_blackST;
        Texture2D layover_mask1;
        #endregion


        GameDriver gameDriver;


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


            Dictionary<string, ShopItem> images = new Dictionary<string, ShopItem>();
            // TODO: use this.Content to load your game content here
            monkey1 = Content.Load<Texture2D>("monkey");
            blackST1 = Content.Load<Texture2D>("shopBlackST");
            silverST1 = Content.Load<Texture2D>("shopsilverST");
            goldST1 = Content.Load<Texture2D>("shopgoldST");
            mask1 = Content.Load<Texture2D>("mask");
            hat1 = Content.Load<Texture2D>("hat");

            sprite = Content.Load<Texture2D>("guy-noST");
            _playerManager = PlayerManager.Load();

            //background_box = Content.Load<Texture2D>("background_box");

            layover_blackST = Content.Load<Texture2D>("layover_blackST");
            layover_mask1 = Content.Load<Texture2D>("layover_mask");

            ShopItem monkey = new ShopItem(monkey1, ItemType.Labcoat, 5, 100);
            ShopItem blackST = new ShopItem(blackST1, ItemType.Stethescope, 0, 100);
            ShopItem silverST = new ShopItem(silverST1, ItemType.Stethescope, 1, 200);
            ShopItem goldST = new ShopItem(goldST1, ItemType.Stethescope, 2, 300);
            ShopItem mask = new ShopItem(mask1, ItemType.Mask, 3, 200);
            ShopItem hat = new ShopItem(hat1, ItemType.Hat, 4, 150);

            ShopItem layover_BST = new ShopItem(layover_blackST, ItemType.Stethescope, 100, 0);
            ShopItem layover_mask = new ShopItem(layover_mask1, ItemType.Mask, 100, 0);

            images["monkey"] = monkey;
            images["blackST"] = blackST;
            images["silverST"] = silverST;
            images["goldST"] = goldST;
            images["mask"] = mask;
            images["hat"] = hat;

            images["layoverBST"] = layover_BST;
            images["layovermask"] = layover_mask;

            //sprite
            //Dictionary<string, ShopItem> owneditems = new Dictionary<string, ShopItem>();


            button = Content.Load<Texture2D>("button");
            gameTextFont = Content.Load<SpriteFont>("gameTextFont");

            // TODO: Add your initialization logic here

            // Initialize game driver, which manages the whole game
            gameDriver = new GameDriver(button, gameTextFont, monkey1, monkey1, images, sprite, _playerManager);

            
            /*
            * Update GameDriver to have images argument and sprite argument from customizepage and shop page
            */
            //shopPage = new ShopPage(button, gameTextFont, images);
            //TODO change customize page third arguement to player purchased items list
            //customizePage = new CustomizePage(button, gameTextFont, images, sprite);
            
        }


        // This function runs once each time the game executes (e.g. 60 times per second if game is running at 60 FPS/Hz)
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here
            
            // Automatically determine and update correct page

            gameDriver.Update(gameTime);

            base.Update(gameTime);
        }


        // This function runs once each time the game executes, but strictly handles visual changes on the screen
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here inside of the Begin() and End() function calls
            _spriteBatch.Begin();
            
           
            //Automatically determine which page to draw

            gameDriver.Draw(gameTime, _spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
