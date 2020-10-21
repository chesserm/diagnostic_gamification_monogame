using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using game_state_enums;
using Microsoft.Xna.Framework;

namespace testing_v2.Screens.PlayScreens
{
    class MainPlayPage
    {

        #region MemberVariables
        Screen _screen = new Screen();

        #endregion

        #region Properties
        // Enum for determining when the user is finished with this page 
        // and determining where the user wants to go (PlayPage checks this)
        public PlayState CurrentMainPlayState { get; set; }
        #endregion


        #region HelperFunctions
        // Define grid layout for this screen
        private void DesignScreenLayout()
        {
            // 30 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into five rows
            #region Rows

            _screen.AddRow(2 * defaultHeight); // 0 row with backbutton in top left
            _screen.AddRow(1 * defaultHeight); // 1 row for spacing
            _screen.AddRow(12 * defaultHeight); // 2 row for sprite
            _screen.AddRow(2 * defaultHeight); // 3 row for spacing
            _screen.AddRow(2 * defaultHeight); // 4 row for text describing what to do
            _screen.AddRow(4 * defaultHeight); // 5 row for diagnose/investigate buttons
            _screen.AddFinalRow(); // 6 final row for spacing

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns
            // Add columns to row 0 for backbutton
            _screen.AddColumn(0, 10); 
            _screen.AddColumn(0, 2); // back button goes here

            // Add columns to row 2 for sprite
            _screen.AddColumn(2, 4);
            _screen.AddColumn(2, 4); // Text can go here
            _screen.AddColumn(2, 4);

            // Add columns to row 5 for diagnose/investigate buttons
            _screen.AddColumn(5, 2);
            _screen.AddColumn(5, 3);
            _screen.AddColumn(5, 2);
            _screen.AddColumn(5, 3);
            _screen.AddColumn(5, 2);

            #endregion
        }

        private void CreateAndPlaceElements(Texture2D spriteTexture, Texture2D buttonTexture, SpriteFont font)
        {
            // Add elements
            #region CreateElements

            // Create Button Objects
            Controls.Button backButton = new Controls.Button(buttonTexture, font) { Text = "Main Menu" };
            Controls.Button diagnoseButton = new Controls.Button(buttonTexture, font) { Text = "Diagnose Patient" };
            Controls.Button investigateButton = new Controls.Button(buttonTexture, font) { Text = "Investigate Symptom" };

            // Patient sprite
            Controls.Sprite patientSprite = new Controls.Sprite(spriteTexture);

            // Text describing what to do
            Controls.Textbox instructionText = new Controls.Textbox(font, "Diagnose the patient's cause of ARF or select a symptom to investigate");

            #endregion

            // Assign event handlers for the buttons (so they actually do something)
            #region AssignEventHandlers

            backButton.Click += BackButton_Click;
            diagnoseButton.Click += DiagnoseButton_Click;
            investigateButton.Click += InvestigateButton_Click;

            #endregion

            // Place button objects (row and col indices gotten from DesignScreenLayout() function above)
            #region PlaceElements

            _screen.Place(backButton, 0, 1);
            _screen.Place(patientSprite, 2, 1);
            _screen.Place(instructionText, 4, 0);
            _screen.Place(diagnoseButton, 5, 1);
            _screen.Place(investigateButton, 5, 3);

            #endregion
        }

        #region DefineEventHandlers

        private void BackButton_Click(object sender, EventArgs e)
        {
            CurrentMainPlayState = PlayState.Back;
        }

        private void InvestigateButton_Click(object sender, EventArgs e)
        {
            CurrentMainPlayState = PlayState.SymptomList;
        }

        private void DiagnoseButton_Click(object sender, EventArgs e)
        {
            CurrentMainPlayState = PlayState.Diagnose;
        }
        #endregion


        #endregion

        #region Functions

        public MainPlayPage(Texture2D spriteTexture, Texture2D buttonTexture, SpriteFont font)
        {
            // Assign this so PlayPage knows when to change the state being used by the player
            CurrentMainPlayState = PlayState.Main;

            DesignScreenLayout();

            CreateAndPlaceElements(spriteTexture, buttonTexture, font);

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
