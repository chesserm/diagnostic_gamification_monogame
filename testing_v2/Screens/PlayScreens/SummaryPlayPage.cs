using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using game_state_enums;

namespace testing_v2.Screens.PlayScreens
{
    class SummaryPlayPage
    {
        #region MemberVariables

        Screen _screen = new Screen();

        Texture2D _buttonTexture;
        SpriteFont _font;
        #endregion


        #region Properties

        public bool IsUserFinishedWithPage { get; set; }

        // Variables for tracking game state
        public Dictionary<SymptomState, String> UserReasoning  { get; set;}
        public Dictionary<SymptomState, String> CorrectReasoning { get; set; }

        // Variable for tracking player diagnosis
        public DiagnosisState PlayerDiagnosis { get; set; }

        #endregion

        #region HelperFunctions

        private void DesignScreenLayout()
        {

            // 24 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into four rows
            _screen.AddRow(2 * defaultHeight); // row 0 title
            _screen.AddRow(2 * defaultHeight); // row 1 Correct vs Incorrect
            _screen.AddRow(2 * defaultHeight); // row 2 spacing
            _screen.AddRow(2 * defaultHeight); // row 3 Reasoning title goes here
            _screen.AddRow(2 * defaultHeight); // row 4 Correct incorrect headings go here


            // Add for loop for space

            _screen.AddRow(2 * defaultHeight); // row 5 for button
            
            _screen.AddFinalRow();             // row 6 + 1


            // Divide screen width within rows into columns (there are 12 column units to divide)

            // Divide correct and incorrect row in half
            _screen.AddColumn(1, 2);
            _screen.AddColumn(1, 3); // Correct diagnosis goes here
            _screen.AddColumn(1, 2);
            _screen.AddColumn(1, 3);
            _screen.AddColumn(1, 2); // Users's diagnosis goes here

            // Divide row for reasoning title
            _screen.AddColumn(3, 1);
            _screen.AddColumn(3, 4); // Reasoning title goes here
            _screen.AddColumn(3, 7);

            // Divide row for correct vs incorrect title
            _screen.AddColumn(4, 1);
            _screen.AddColumn(4, 3); // Reasoning title goes here
            _screen.AddColumn(4, 2);
            _screen.AddColumn(4, 3);
            _screen.AddColumn(4, 1);

            // Divide up row for button
            _screen.AddColumn(5, 4);
            _screen.AddColumn(5, 4); // Button goes here
            _screen.AddColumn(5, 4);


        }

        private void CreateAndPlaceElements(Texture2D buttonTexture, SpriteFont font)
        {
            Controls.Button finishButton = new Controls.Button(buttonTexture, font) { Text = "Finish" };

            Controls.Textbox summaryTitle = new Controls.Textbox(font, "Summary of Diagnostic Process");
            Controls.Textbox reasoningTitle = new Controls.Textbox(font, "Reasoning Steps");
            Controls.Textbox correctTitle = new Controls.Textbox(font, "Correct");
            Controls.Textbox incorrectTitle = new Controls.Textbox(font, "Incorrect");

            // Add event handler for one button
            finishButton.Click += FinishButton_Click;


            #region PlaceElements

            _screen.Place(summaryTitle, 0, 0);

            #endregion
        }

        private void FinishButton_Click(object sender, EventArgs e)
        {
            IsUserFinishedWithPage = true;
        }
        #endregion

        #region Functions

        public SummaryPlayPage(Texture2D buttonTexture, SpriteFont font)
        {
            IsUserFinishedWithPage = false;

            _buttonTexture = buttonTexture;
            _font = font;

        }

        // Redraw summary page
        public void UpdateSummaryPage()
        {
            _screen = new Screen();
            DesignScreenLayout();
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
