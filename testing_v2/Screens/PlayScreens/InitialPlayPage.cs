using Android.App.Backup;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using testing_v2.Controls;
using game_state_enums;

namespace testing_v2.Screens.PlayScreens
{
    class InitialPlayPage
    {
        #region MemberVariables
        Screen _screen = new Screen();

        Texture2D _buttonTexture;
        SpriteFont _font;
        #endregion

        #region Properties
        // Flag for determining when the user is finished with this page (PlayPage checks this)
        public bool IsUserFinishedWithPage { get; set; }

        public int Age { get; set; }
        public string Gender { get; set; }

        public string PastMedicalHistory1 { get; set; }
        public string PastMedicalHistory2 { get; set; }
        public string PastMedicalHistory3 { get; set; }
        public string TobaccoUse { get; set; }


        public string SymptomOnset { get; set; }
        public string SymptomSeverity { get; set; }
        public string SymptomDescription { get; set; }
        public string SymptomDuration { get; set; }

        public string ProvocatingFactors { get; set; }
        public string RelievingFactors { get; set; }

        #endregion


        #region HelperFunctions
        // Define grid layout for this screen
        private void DesignScreenLayout()
        {
            // 24 rows, where height = deafultRowHeight, exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into four rows
            #region Rows

            _screen.AddRow(1 * defaultHeight); // 0 title
            _screen.AddRow(1 * defaultHeight); // 1 space after title

            _screen.AddRow(1 * defaultHeight); // 2 Age
            _screen.AddRow(1 * defaultHeight); // 3 Gender
            _screen.AddRow(1 * defaultHeight); // 4 space after age + gender

            _screen.AddRow(1 * defaultHeight); // 5 past medical history title
            _screen.AddRow(1 * defaultHeight); // 6 past medical history 1
            _screen.AddRow(1 * defaultHeight); // 7 past medical history 2
            _screen.AddRow(1 * defaultHeight); // 8 past medical history 3
            _screen.AddRow(1 * defaultHeight); // 9 past medical history Tobacco
            _screen.AddRow(1 * defaultHeight); // 10 Space after past medical history

            _screen.AddRow(1 * defaultHeight); // 11 Symptom title
            _screen.AddRow(1 * defaultHeight); // 12 Symptom Description
            _screen.AddRow(1 * defaultHeight); // 13 Symptom Severity
            _screen.AddRow(1 * defaultHeight); // 14 Provocating Factors
            _screen.AddRow(1 * defaultHeight); // 15 Relieving Factors
            _screen.AddRow(1 * defaultHeight); // 16 Symptom Onset
            _screen.AddRow(1 * defaultHeight); // 17 Symptom Duration
            _screen.AddRow(1 * defaultHeight); // 18 Space after factors

            _screen.AddRow(1 * defaultHeight); // 19 Button
            _screen.AddFinalRow(); // 20 Space after button at bottom

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns

            // Add columns to rows 2,3,5, and 11 to force Approximate left justification
            #region LeftJustifyTitles
            _screen.AddColumn(2,4); // Text goes here
            _screen.AddColumn(2,8);

            _screen.AddColumn(3, 4); // Text goes here
            _screen.AddColumn(3, 8);

            _screen.AddColumn(5, 4); // Text goes here
            _screen.AddColumn(5, 8);

            _screen.AddColumn(11, 4); // Text goes here
            _screen.AddColumn(11, 8);
            #endregion

            // Add columns to rows 6-9 and 12-17 to show indentation
            #region indentation2Units
            _screen.AddColumn(6, 2);
            _screen.AddColumn(6, 4); // Text goes here
            _screen.AddColumn(6, 6);

            _screen.AddColumn(7, 2);
            _screen.AddColumn(7, 4); // Text goes here
            _screen.AddColumn(7, 6);

            _screen.AddColumn(8, 2);
            _screen.AddColumn(8, 4); // Text goes here
            _screen.AddColumn(8, 6);

            _screen.AddColumn(9, 2);
            _screen.AddColumn(9, 4); // Text goes here
            _screen.AddColumn(9, 6);

            _screen.AddColumn(12, 2);
            _screen.AddColumn(12, 4); // Text goes here
            _screen.AddColumn(12, 6);

            _screen.AddColumn(13, 2);
            _screen.AddColumn(13, 4); // Text goes here
            _screen.AddColumn(13, 6);

            _screen.AddColumn(14, 2);
            _screen.AddColumn(14, 4); // Text goes here
            _screen.AddColumn(14, 6);

            _screen.AddColumn(15, 2);
            _screen.AddColumn(15, 4); // Text goes here
            _screen.AddColumn(15, 6);

            _screen.AddColumn(16, 2);
            _screen.AddColumn(16, 4); // Text goes here
            _screen.AddColumn(16, 6);

            _screen.AddColumn(17, 2);
            _screen.AddColumn(17, 4); // Text goes here
            _screen.AddColumn(17, 6);
            #endregion

            // Center button
            _screen.AddColumn(19, 4);
            _screen.AddColumn(19, 4); // Button goes here
            _screen.AddColumn(19, 4);

            #endregion
        }

        private void CreateAndPlaceElements(Texture2D texture, SpriteFont font)
        {
            #region DefiningElements

            Textbox title = new Textbox(font, "Initial Patient Information");

            // Information being displayed to user
            Textbox age = new Textbox(font, $"Age: {Age}");
            Textbox gender = new Textbox(font, $"Gender: {Gender}");

            Textbox pastMedicalHistoryTitle = new Textbox(font, "Past Medical History");
            Textbox pastMedicalHistory1 = new Textbox(font, PastMedicalHistory1);
            Textbox pastMedicalHistory2 = new Textbox(font, PastMedicalHistory2);
            Textbox pastMedicalHistory3 = new Textbox(font, PastMedicalHistory3);
            Textbox tobaccoUse = new Textbox(font, $"Tobacco use: {TobaccoUse}");

            Textbox symptomTitle = new Textbox(font, "Symptom Information");
            Textbox symptomOnset = new Textbox(font, SymptomOnset);
            Textbox symptomDuration = new Textbox(font, SymptomDuration);
            Textbox symptomDescription = new Textbox(font, SymptomDescription);
            Textbox symptomSeverity = new Textbox(font, SymptomSeverity);

            Textbox provocatingFactors = new Textbox(font, ProvocatingFactors);
            Textbox relievingFactors = new Textbox(font, RelievingFactors);

            // Button to advance to core gameplay loop
            Controls.Button continueButton = new Controls.Button(texture, font) { Text = "Continue"};
            
            #endregion

            // Event handler for button
            continueButton.Click += ContinueButton_Click;


            // Placing all components on screen
            #region PlacingElements

            _screen.Place(title, 0, 0);
            
            _screen.Place(age, 2, 0);
            _screen.Place(gender, 3, 0);

            _screen.Place(pastMedicalHistoryTitle, 5, 0);
            _screen.Place(pastMedicalHistory1, 6, 1);
            _screen.Place(pastMedicalHistory2, 7, 1);
            _screen.Place(pastMedicalHistory3, 8, 1);
            _screen.Place(tobaccoUse, 9, 1);

            _screen.Place(symptomTitle, 11, 0);
            _screen.Place(symptomDescription, 12, 1);
            _screen.Place(symptomSeverity, 13, 1);
            _screen.Place(provocatingFactors, 14, 1);
            _screen.Place(relievingFactors, 15, 1);
            _screen.Place(symptomOnset, 16, 1);
            _screen.Place(symptomDuration, 17, 1);

            _screen.Place(continueButton, 19, 1);

            #endregion

            return;

        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            // Set boolean to true so PlayPage knows to switch state to the next page in the loop
            IsUserFinishedWithPage = true;
        }

        #endregion

        #region Functions

        // Constructor
        public InitialPlayPage(Texture2D texture, SpriteFont font)
        {
            // Initialize flag that the PlayPage checks for
            IsUserFinishedWithPage = false;

            _buttonTexture = texture;
            _font = font;

            // Divide the grid of the screen into rows and columns
            //DesignScreenLayout();

            // Create and place the objects needed for this page
            //CreateAndPlaceElements(texture, font);
        }

        public void UpdateInitialPlayPage()
        {
            // Divide the grid of the screen into rows and columns
            DesignScreenLayout();

            // Create and place the objects needed for this page
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

        #endregion


    }
}
