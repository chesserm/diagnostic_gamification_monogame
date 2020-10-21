using game_state_enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testing_v2.Screens.PlayScreens
{
    class ReasoningPlayPage
    {
        #region MemberVariables

        Screen _screen = new Screen();

        Texture2D _buttonTexture;
        SpriteFont _font;

        #endregion


        #region Properties

        public bool IsUserFinishedWithPage { get; set; }

   
        public ReasoningState SelectedReasoning { get; set; }

        public Dictionary<ReasoningState, String> ReasoningChoices { get; set; }

        #endregion

        #region HelperFunctions
        // Define grid layout for this screen
        private void DesignScreenLayout()
        {
            // 30 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into five rows
            #region Rows

            _screen.AddRow(2 * defaultHeight); // 0 row for instruction
            _screen.AddRow(4 * defaultHeight); // 1 row for spacing
            _screen.AddRow(2 * defaultHeight); // 2 row for first button
            _screen.AddRow(1 * defaultHeight); // 3 row for spacing
            _screen.AddRow(2 * defaultHeight); // 4 row for second button
            _screen.AddRow(1 * defaultHeight); // 5 row for spacing
            _screen.AddRow(2 * defaultHeight); // 6 row for third button
            _screen.AddRow(1 * defaultHeight); // 7 row for spacing
            _screen.AddRow(2 * defaultHeight); // 8 row for fourth button
            _screen.AddRow(1 * defaultHeight); // 9 row for spacing
            _screen.AddFinalRow(); // 6 final row for spacing

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns
            
            // Add columns to rows with buttons
            _screen.AddColumn(2, 2);
            _screen.AddColumn(2, 8); // First button goes here
            _screen.AddColumn(2, 2);

            _screen.AddColumn(4, 2);
            _screen.AddColumn(4, 8); // Second button goes here
            _screen.AddColumn(4, 2);

            _screen.AddColumn(6, 2);
            _screen.AddColumn(6, 8); // Third button goes here
            _screen.AddColumn(6, 2);

            _screen.AddColumn(8, 2);
            _screen.AddColumn(8, 8); // Third button goes here
            _screen.AddColumn(8, 2);

            #endregion
        }

        private void CreateAndPlaceElements(Texture2D buttonTexture, SpriteFont font)
        {
            // Add elements
            #region CreateElements

            // Create Button Objects
            Controls.Button reasoningButtonCorrect = new Controls.Button(buttonTexture, font) { Text = ReasoningChoices[ReasoningState.Correct] };

            List<Controls.Button> incorrectButtons = new List<Controls.Button>();
            incorrectButtons.Add(new Controls.Button(buttonTexture, font) { Text = ReasoningChoices[ReasoningState.Incorrect1] });
            incorrectButtons.Add(new Controls.Button(buttonTexture, font) { Text = ReasoningChoices[ReasoningState.Incorrect2] });
            incorrectButtons.Add(new Controls.Button(buttonTexture, font) { Text = ReasoningChoices[ReasoningState.Incorrect3] });
            

            // Text describing what to do
            Controls.Textbox instructionText = new Controls.Textbox(font, "Please select the correct conclusion from the symptom information");

            #endregion

            // Assign event handlers for the buttons (so they actually do something)
            #region AssignEventHandlers
            reasoningButtonCorrect.Click += ReasoningButtonCorrect_Click;
            incorrectButtons[0].Click += ReasoningButtonIncorrect1_Click;
            incorrectButtons[1].Click += ReasoningButtonIncorrect2_Click;
            incorrectButtons[2].Click += ReasoningButtonIncorrect3_Click;

            #endregion

            // Place button objects (row and col indices gotten from DesignScreenLayout() function above)
            #region PlaceElements
            _screen.Place(instructionText, 0, 0);

            // Randomly assign correctButton's position
            Random rnd = new Random();
            int correctRowIndex = rnd.Next(1, 5);

            _screen.Place(reasoningButtonCorrect, correctRowIndex * 2, 1);

            // Determine what indices remain for the other three buttons
            List<int> incorrectRowIndices = new List<int>();
            for (int i = 1; i < 5; ++i)
            {
                if (i != correctRowIndex)
                {
                    incorrectRowIndices.Add(i * 2);
                }
            }

            // Add incorrect buttons at the remaining indices
            for (int i = 0; i < 3; ++i)
            {
                _screen.Place(incorrectButtons[i], incorrectRowIndices[i], 1);
            }

            

            #endregion
        }

        #region EventHandlers
        private void ReasoningButtonIncorrect3_Click(object sender, EventArgs e)
        {
            
            SelectedReasoning = ReasoningState.Incorrect3;
            IsUserFinishedWithPage = true;
        }

        private void ReasoningButtonIncorrect2_Click(object sender, EventArgs e)
        {
            
            SelectedReasoning = ReasoningState.Incorrect2;
            IsUserFinishedWithPage = true;
        }

        private void ReasoningButtonIncorrect1_Click(object sender, EventArgs e)
        {
            
            SelectedReasoning = ReasoningState.Incorrect1;
            IsUserFinishedWithPage = true;
        }

        private void ReasoningButtonCorrect_Click(object sender, EventArgs e)
        {
            
            SelectedReasoning = ReasoningState.Correct;
            IsUserFinishedWithPage = true;
        }

        #endregion

        #endregion




        #region Functions

        public ReasoningPlayPage(Texture2D buttonTexture, SpriteFont font)
        {
            IsUserFinishedWithPage = false;
            SelectedReasoning = ReasoningState.Undecided;

            _buttonTexture = buttonTexture;
            _font = font;
            

        }

        // This function can be called to re-draw the page
        public void UpdateReasoningPage()
        {
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
