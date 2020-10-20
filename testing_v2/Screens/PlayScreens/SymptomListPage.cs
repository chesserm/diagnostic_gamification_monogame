using System;
using System.Collections.Generic;
using System.Text;
using game_state_enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testing_v2.Screens.PlayScreens
{
    class SymptomListPage
    {
        #region MemberVariables

        Screen _screen = new Screen();

        #endregion

        #region Properties
        public SymptomState SelectedSymptom { get; set; }

        PatientData patientData { get; set; }

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
            _screen.AddRow((int)(0.5 * defaultHeight)); // 1 row for spacing
            _screen.AddRow(2 * defaultHeight); // 2 row for Instruction Text
            _screen.AddRow(1 * defaultHeight); // 3 row for spacing

            _screen.AddRow(1 * defaultHeight); // 4 row for General Examination Button
            _screen.AddRow((int)(0.5 * defaultHeight)); // 5 row for spacing
            _screen.AddRow(1 * defaultHeight); // 6 row for Head Examination Button
            _screen.AddRow((int)(0.5 * defaultHeight)); // 7 row for spacing
            _screen.AddRow(1 * defaultHeight); // 8 row for Neck Examination Button
            _screen.AddRow((int)(0.5 * defaultHeight)); // 9 row for spacing
            _screen.AddRow(1 * defaultHeight); // 10 row for Lungs Examination Button
            _screen.AddRow((int)(0.5 * defaultHeight)); // 11 row for spacing
            _screen.AddRow(1 * defaultHeight); // 12 row for Extremities Examination Button
            _screen.AddRow((int)(0.5 * defaultHeight)); // 13 row for spacing
            _screen.AddRow(1 * defaultHeight); // 14 row for Skin Examination Button
            _screen.AddRow((int)(0.5 * defaultHeight)); // 15 row for spacing
            _screen.AddRow(1 * defaultHeight); // 16 row for Abdomen Examination Button
            _screen.AddRow((int)(0.5 * defaultHeight)); // 17 row for spacing
            _screen.AddRow(1 * defaultHeight); // 18 row for Oxygen Examination Button
            _screen.AddRow((int)(0.5 * defaultHeight)); // 19 row for spacing
            _screen.AddRow(1 * defaultHeight); // 20 row for Imaging Examination Button
            _screen.AddRow((int)(0.5 * defaultHeight)); // 21 row for spacing
            _screen.AddRow(1 * defaultHeight); // 22 row for Imaging Examination Button

            _screen.AddFinalRow(); // 23 final row for spacing

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns

            // Add columns to row 0 for backbutton
            _screen.AddColumn(0, 2); // back button goes here
            _screen.AddColumn(0, 10);

            // Add columns to row 4-18 for buttons
            for(int row = 4; row < 23; row += 2)
            {
                _screen.AddColumn(row, 2);
                _screen.AddColumn(row, 8); // button goes here
                _screen.AddColumn(row, 2);
            }
            

            #endregion
        }

        private void CreateAndPlaceElements(Texture2D buttonTexture, SpriteFont font)
        {
            // Add elements
            #region CreateElements

            // Create Button Objects
            Controls.Button backButton = new Controls.Button(buttonTexture, font) { Text = "Back" };
            Controls.Button examGeneral = new Controls.Button(buttonTexture, font) { Text = "Perform General Examination" };
            Controls.Button examHead = new Controls.Button(buttonTexture, font) { Text = "Examine Head" };
            Controls.Button examNeck = new Controls.Button(buttonTexture, font) { Text = "Examine Neck" };
            Controls.Button examLungs = new Controls.Button(buttonTexture, font) { Text = "Examine Lungs" };
            Controls.Button examExtremities = new Controls.Button(buttonTexture, font) { Text = "Examine Extremities" };
            Controls.Button examSkin = new Controls.Button(buttonTexture, font) { Text = "Examine Skin" };
            Controls.Button examAbdomen = new Controls.Button(buttonTexture, font) { Text = "Examine Abdomen" };
            Controls.Button examOxygen = new Controls.Button(buttonTexture, font) { Text = "Review Oxygen Information" };
            Controls.Button examBloodwork = new Controls.Button(buttonTexture, font) { Text = "Request Bloodwork" };
            Controls.Button examImaging = new Controls.Button(buttonTexture, font) { Text = "Request Imaging" };


            // Text describing what to do
            Controls.Textbox instructionText = new Controls.Textbox(font, "Select an action to further investigate the patient's condition");

            #endregion

            // Assign event handlers for the buttons (so they actually do something)
            #region AssignEventHandlers

            backButton.Click += BackButton_Click;
            examGeneral.Click += ExamGeneral_Click;
            examHead.Click += ExamHead_Click;
            examNeck.Click += ExamNeck_Click;
            examLungs.Click += ExamLungs_Click;
            examExtremities.Click += ExamExtremities_Click;
            examSkin.Click += ExamSkin_Click;
            examAbdomen.Click += ExamAbdomen_Click;
            examOxygen.Click += ExamOxygen_Click;
            examBloodwork.Click += ExamBloodwork_Click;
            examImaging.Click += ExamImaging_Click;

            #endregion

            // Place button objects (row and col indices gotten from DesignScreenLayout() function above)
            #region PlaceElements

            _screen.Place(backButton, 0, 0);
            _screen.Place(instructionText, 2, 0);

            _screen.Place(examGeneral, 4, 1);
            _screen.Place(examHead, 6, 1);
            _screen.Place(examNeck, 8, 1);
            _screen.Place(examLungs, 10, 1);
            _screen.Place(examExtremities, 12, 1);
            _screen.Place(examSkin, 14, 1);
            _screen.Place(examAbdomen, 16, 1);
            _screen.Place(examOxygen, 18, 1);
            _screen.Place(examBloodwork, 20, 1);
            _screen.Place(examImaging, 22, 1);

            #endregion
        }

        #region EventHandlers

        private void ExamImaging_Click(object sender, EventArgs e)
        {
            SelectedSymptom = SymptomState.Imaging;
        }

        private void ExamBloodwork_Click(object sender, EventArgs e)
        {
            SelectedSymptom = SymptomState.Bloodwork;
        }

        private void ExamOxygen_Click(object sender, EventArgs e)
        {
            SelectedSymptom = SymptomState.Oxygen;
        }

        private void ExamAbdomen_Click(object sender, EventArgs e)
        {
            SelectedSymptom = SymptomState.Abdomen;
        }

        private void ExamSkin_Click(object sender, EventArgs e)
        {
            SelectedSymptom = SymptomState.Skin;
        }

        private void ExamExtremities_Click(object sender, EventArgs e)
        {
            SelectedSymptom = SymptomState.Extremities;
        }

        private void ExamLungs_Click(object sender, EventArgs e)
        {
            SelectedSymptom = SymptomState.Lungs;
        }

        private void ExamNeck_Click(object sender, EventArgs e)
        {
            SelectedSymptom = SymptomState.Neck;
        }

        private void ExamHead_Click(object sender, EventArgs e)
        {
            SelectedSymptom = SymptomState.Head;
        }

        private void ExamGeneral_Click(object sender, EventArgs e)
        {
            SelectedSymptom = SymptomState.General;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            SelectedSymptom = SymptomState.MainMenu;
        }

        #endregion

        #endregion


        #region Functions
        public SymptomListPage(Texture2D buttonTexture, SpriteFont font)
        {
            SelectedSymptom = SymptomState.Nothing;

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
