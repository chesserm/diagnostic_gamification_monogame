using game_state_enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testing_v2.Screens.PlayScreens
{
    class DiagnosePlayPage
    {
        #region MemberVariables
        Screen _screen = new Screen();

        #endregion

        #region Properties
        public DiagnosisState PatientDiagnosis { get; set; }
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
            _screen.AddRow(2 * defaultHeight); // 2 row for Instruction Text
            _screen.AddRow(2 * defaultHeight); // 3 row for spacing

            _screen.AddRow(4 * defaultHeight); // 4 row for CHF Button
            _screen.AddRow(1 * defaultHeight); // 5 row for spacing
            _screen.AddRow(4 * defaultHeight); // 6 row for COPD Button
            _screen.AddRow(1 * defaultHeight); // 7 row for spacing
            _screen.AddRow(4 * defaultHeight); // 8 row for Pneumonia Button

            _screen.AddFinalRow(); // 9 final row for spacing

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns

            // Add columns to row 0 for backbutton
            _screen.AddColumn(0, 2); // back button goes here
            _screen.AddColumn(0, 10);

            // Add columns to row 2,4,6 for buttons
            _screen.AddColumn(4, 2);
            _screen.AddColumn(4, 8); // CHF button goes here
            _screen.AddColumn(4, 2);

            _screen.AddColumn(6, 2);
            _screen.AddColumn(6, 8); // COPD button goes here
            _screen.AddColumn(6, 2);

            _screen.AddColumn(8, 2);
            _screen.AddColumn(8, 8); // Pneumonia button goes here
            _screen.AddColumn(8, 2);

            #endregion
        }

        private void CreateAndPlaceElements(Texture2D buttonTexture, SpriteFont font)
        {
            // Add elements
            #region CreateElements

            // Create Button Objects
            Controls.Button backButton = new Controls.Button(buttonTexture, font) { Text = "Back" };
            Controls.Button pneumoniaButton = new Controls.Button(buttonTexture, font) { Text = "Pneumonia" };
            Controls.Button copdButton = new Controls.Button(buttonTexture, font) { Text = "COPD" };
            Controls.Button chfButton = new Controls.Button(buttonTexture, font) { Text = "CHF" };


            // Text describing what to do
            Controls.Textbox instructionText = new Controls.Textbox(font, "Diagnose the underlying cause of the patient's Acute Respiratory Failure");

            #endregion

            // Assign event handlers for the buttons (so they actually do something)
            #region AssignEventHandlers
            chfButton.Click += ChfButton_Click;
            copdButton.Click += CopdButton_Click;
            pneumoniaButton.Click += PneumoniaButton_Click;

            #endregion

            // Place button objects (row and col indices gotten from DesignScreenLayout() function above)
            #region PlaceElements

            _screen.Place(backButton, 0, 0);
            _screen.Place(instructionText, 2, 0);
            _screen.Place(chfButton, 4, 1);
            _screen.Place(copdButton, 6, 1);
            _screen.Place(pneumoniaButton, 8, 1);

            #endregion
        }

        private void PneumoniaButton_Click(object sender, EventArgs e)
        {
            PatientDiagnosis = DiagnosisState.Pneumonia;
        }

        private void CopdButton_Click(object sender, EventArgs e)
        {
            PatientDiagnosis = DiagnosisState.COPD;
        }

        private void ChfButton_Click(object sender, EventArgs e)
        {
            PatientDiagnosis = DiagnosisState.Back;
        }

        #endregion


        #region Functions

        public DiagnosePlayPage(Texture2D buttonTexture, SpriteFont font)
        {
            // Initialize diagnosis as undiagnosed
            PatientDiagnosis = DiagnosisState.Undiagnosed;

            DesignScreenLayout();

            CreateAndPlaceElements(buttonTexture, font);
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
