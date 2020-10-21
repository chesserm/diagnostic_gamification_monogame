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

        int _amountCoinsAwarded = 0;
        int _amountExpAwarded = 0;
        #endregion


        #region Properties

        public bool IsUserFinishedWithPage { get; set; }

        // Variables for tracking game state
        public Dictionary<SymptomState, String> UserReasoning  { get; set;}
        public Dictionary<SymptomState, String> CorrectReasoning { get; set; }

        // Variable for tracking player diagnosis
        public DiagnosisState PlayerDiagnosis { get; set; }

        public DiagnosisState CorrectDiagnosis { get; set; }

        #endregion

        #region HelperFunctions

        private void determineAwards()
        {
            _amountCoinsAwarded = 0;
            _amountExpAwarded = 0;

            // Check for correct diagnosis
            if (PlayerDiagnosis == CorrectDiagnosis)
            {
                _amountCoinsAwarded += 50;
                _amountExpAwarded += 500;
            }

            int bonusExp = 250;
            int bonusCoins = 25;
            foreach (KeyValuePair<SymptomState, String> kvp in UserReasoning)
            {
                if (kvp.Value != CorrectReasoning[kvp.Key])
                {
                    bonusExp -= 50;
                    bonusCoins -= 5;
                }
            }

            _amountCoinsAwarded += bonusCoins;
            _amountExpAwarded += bonusExp;

            return;
        }


        private String getDiagnosisString(DiagnosisState diagnosis)
        {
            switch(diagnosis)
            {
                case DiagnosisState.CHF:
                    {
                        return "CHF";
                    }
                case DiagnosisState.COPD:
                    {
                        return "COPD";
                    }
                case DiagnosisState.Pneumonia:
                    {
                        return "Pneumonia";
                    }
            }

            return "Diagnosis";
        }


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

            // Five reasoning values
            _screen.AddRow(2 * defaultHeight); // row 5 Correct incorrect headings go here
            _screen.AddRow(2 * defaultHeight); // row 6 Correct incorrect headings go here
            _screen.AddRow(2 * defaultHeight); // row 7 Correct incorrect headings go here
            _screen.AddRow(2 * defaultHeight); // row 8 Correct incorrect headings go here
            _screen.AddRow(2 * defaultHeight); // row 9 Correct incorrect headings go here

            _screen.AddRow(1 * defaultHeight); // row 10 for coins and exp
            _screen.AddRow(1 * defaultHeight); // row 11 for button
            
            _screen.AddFinalRow();             // row 11


            // Divide screen width within rows into columns (there are 12 column units to divide)

            // Divide correct and incorrect row in half
            _screen.AddColumn(1, 2);
            _screen.AddColumn(1, 3); // Correct diagnosis goes here
            _screen.AddColumn(1, 2);
            _screen.AddColumn(1, 3); // Users's diagnosis goes here
            _screen.AddColumn(1, 2); 

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

            // Add slots for reasoning
            for (int i = 5; i < 10; ++i)
            {
                _screen.AddColumn(i, 2);
                _screen.AddColumn(i, 3); // Correct reasoning goes here
                _screen.AddColumn(i, 2);
                _screen.AddColumn(i, 3); // Users's reasoning goes here
                _screen.AddColumn(i, 2);
            }

            // Divide up coin and exp row 
            _screen.AddColumn(10, 2);
            _screen.AddColumn(10, 3); // Correct reasoning goes here
            _screen.AddColumn(10, 2);
            _screen.AddColumn(10, 3); // Users's reasoning goes here
            _screen.AddColumn(10, 2);

            // Divide up row for button
            _screen.AddColumn(11, 4);
            _screen.AddColumn(11, 4); // Button goes here
            _screen.AddColumn(11, 4);


        }

        private void CreateAndPlaceElements(Texture2D buttonTexture, SpriteFont font)
        {
            #region CreateElements
            Controls.Button finishButton = new Controls.Button(buttonTexture, font) { Text = "Finish" };

            Controls.Textbox summaryTitle = new Controls.Textbox(font, "Summary of Diagnostic Process");
            Controls.Textbox reasoningTitle = new Controls.Textbox(font, "Reasoning Steps");
            Controls.Textbox correctTitle = new Controls.Textbox(font, "Correct");
            Controls.Textbox incorrectTitle = new Controls.Textbox(font, "Incorrect");

            Controls.Textbox userDiagnosisText = new Controls.Textbox(font, getDiagnosisString(PlayerDiagnosis));
            Controls.Textbox correctDiagnosisText = new Controls.Textbox(font, getDiagnosisString(CorrectDiagnosis));

            Controls.Textbox coinAwarded = new Controls.Textbox(font, $"Coins Awarded: {_amountCoinsAwarded}");
            Controls.Textbox expAwarded = new Controls.Textbox(font, $"Experience Awarded: {_amountExpAwarded}"); 


            #region CreatingReasoningElements

             List <Controls.Textbox> correctReasoningChoices = new List<Controls.Textbox>();
            List<Controls.Textbox> userReasoningChoices = new List<Controls.Textbox>();

            // Create Textbox UI elements using reasoning choices
            int remainingReasoningSlots = 5;
            foreach(KeyValuePair<SymptomState, String> kvp in UserReasoning)
            {
                // Currently, only 5 reasoning slots can be shown (need to make a UI element with scrolling)
                if (remainingReasoningSlots == 0)
                {
                    break;
                }

                // Create textbox UI elements based upon reasoning choices selected by user
                // This assumes that for every symptom where reasoning is selected by the user, 
                // the corresponding correct reasoning is added properly
                userReasoningChoices.Add(new Controls.Textbox(font, kvp.Value));
                correctReasoningChoices.Add(new Controls.Textbox(font, CorrectReasoning[kvp.Key]));

                // Decrement by one to indicate one less remaining slot
                remainingReasoningSlots -= 1;
            }
            #endregion

            #endregion


            // Add event handler for finish button
            finishButton.Click += FinishButton_Click;


            #region PlaceElements

            // Titles
            _screen.Place(summaryTitle, 0, 0);
            _screen.Place(reasoningTitle, 3, 1);
            _screen.Place(correctTitle, 4, 1);
            _screen.Place(incorrectTitle, 4, 3);
            _screen.Place(userDiagnosisText, 1, 1);
            _screen.Place(correctDiagnosisText, 1, 3);

            // Button
            _screen.Place(finishButton, 11, 1);

            // Exp and coin values
            _screen.Place(coinAwarded, 10, 1);
            _screen.Place(expAwarded, 10, 3);

            #region AddReasoningElements
            int sizeOfLists = userReasoningChoices.Count;
            for (int i = 0; i < sizeOfLists; ++i)
            {
                // The rows where reasoning is placed are [5-9]
                int rowValue = i + 5;
                _screen.Place(correctReasoningChoices[i], rowValue, 1);
                _screen.Place(userReasoningChoices[i], rowValue, 3);
            }

            #endregion

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

        // Redraw summary page (done after reasoning Properties are set/updated)
        public void UpdateSummaryPage()
        {
            determineAwards();

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
