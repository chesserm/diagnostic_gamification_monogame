using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using game_state_enums;
using System.Linq;
using Microsoft.Xna.Framework;

namespace testing_v2.Screens.PlayScreens
{
    class SymptomInfoPlayPage
    {

        #region MemberVariables
        Screen _screen = new Screen();
        SymptomState _symptomSelected; 

        #endregion

        #region Properties

        public SymptomState SymptomInfoStatus { get; set; }

        public bool IsUserFinishedReviewing { get; set; }

        public PatientData PatientData { get; set; }

        #endregion



        #region HelperFunctions


        #region FormattingFunctions

        #region ExamGeneral
        public void DesignScreenExamGeneral()
        {
            // 24 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into five rows
            #region Rows
            _screen.AddRow(2 * defaultHeight); // 0 row at the top of screen for title
            _screen.AddRow(2 * defaultHeight); // 1 row for spacing

            _screen.AddRow(2 * defaultHeight); // 2 first data value
            _screen.AddRow(1 * defaultHeight); // 3 row for spacing
            _screen.AddRow(2 * defaultHeight); // 4 Second data value
            _screen.AddRow(1 * defaultHeight); // 5 row for spacing
            _screen.AddRow(2 * defaultHeight); // 6 Third data value
            _screen.AddRow(1 * defaultHeight); // 7 row for spacing
            _screen.AddRow(2 * defaultHeight); // 8 Fourth data value
            _screen.AddRow(1 * defaultHeight); // 9 row for spacing

            _screen.AddRow(2 * defaultHeight); // 10 Fifth data value
            _screen.AddRow(2 * defaultHeight); // 11 row for spacing

            _screen.AddRow(2 * defaultHeight); // 12 row for button
            _screen.AddFinalRow(); // 13 final row for spacing

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns

            // Add columns to rows with data
            _screen.AddColumn(2, 4); // First Data value goes here
            _screen.AddColumn(2, 8);

            _screen.AddColumn(4, 4); // Second Data value goes here
            _screen.AddColumn(4, 8);

            _screen.AddColumn(6, 4); // Third Data value goes here
            _screen.AddColumn(6, 8);

            _screen.AddColumn(8, 4); // Fourth Data value goes here
            _screen.AddColumn(8, 8);

            _screen.AddColumn(10, 4); // Fifth Data value goes here
            _screen.AddColumn(10, 8);

            // Add columns to row with button
            _screen.AddColumn(12, 4);
            _screen.AddColumn(12, 4); // button goes here
            _screen.AddColumn(12, 4);

            #endregion

        }

        public void PlaceElementsExamGeneral(Texture2D buttonTexture, SpriteFont font)
        {
            Controls.Button continueButton = new Controls.Button(buttonTexture, font) { Text = "Continue" };
            Controls.Textbox textTitle = new Controls.Textbox(font, "Results of General Exam");

            Controls.Textbox textTemp = new Controls.Textbox(font, $"Temperature: {PatientData.GeneralExamData[0]}");
            Controls.Textbox textHeartRate = new Controls.Textbox(font, $"Heart Rate: {PatientData.GeneralExamData[1]}");
            Controls.Textbox textRespiratoryRate = new Controls.Textbox(font, $"Respiratory Rate: {PatientData.GeneralExamData[2]}");
            Controls.Textbox textBloodPressure = new Controls.Textbox(font, $"Blood Pressure: {PatientData.GeneralExamData[3]}");
            Controls.Textbox textObservations = new Controls.Textbox(font, $"Observations: {PatientData.GeneralExamData[4]}");

            // Add event handler
            continueButton.Click += ContinueButton_Click;

            // Place elements
            _screen.Place(textTitle, 0, 0);
            _screen.Place(textTemp, 2, 0);
            _screen.Place(textHeartRate, 4, 0);
            _screen.Place(textRespiratoryRate, 6, 0);
            _screen.Place(textBloodPressure, 8, 0);
            _screen.Place(textObservations, 10, 0);
            _screen.Place(continueButton, 12, 1);
        }


        #endregion


        #region ExamHead

        public void DesignScreenExamHead()
        {
            // 24 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into five rows
            #region Rows
            _screen.AddRow(2 * defaultHeight); // 0 row at the top of screen for title
            _screen.AddRow(10 * defaultHeight); // 1 row for spacing

            _screen.AddRow(2 * defaultHeight); // 2 data value
            _screen.AddRow(6 * defaultHeight); // 3 row for spacing
            _screen.AddRow(2 * defaultHeight); // 4 row for button
            _screen.AddFinalRow(); // 5 final row for spacing

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns

            // Add columns to rows with data
            _screen.AddColumn(2, 4); //  Data value goes here
            _screen.AddColumn(2, 8);

            // Add columns to row with button
            _screen.AddColumn(4, 4);
            _screen.AddColumn(4, 4); // button goes here
            _screen.AddColumn(4, 4);

            #endregion
        }

        public void PlaceElementsExamHead(Texture2D buttonTexture, SpriteFont font)
        {
            Controls.Textbox textTitle = new Controls.Textbox(font, "Results from Head Exam");
            Controls.Textbox examResults = new Controls.Textbox(font, $"Observations: {PatientData.HeadData}");

            Controls.Button continueButton = new Controls.Button(buttonTexture, font) { Text = "Continue"};

            continueButton.Click += ContinueButton_Click;

            _screen.Place(textTitle, 0, 0);
            _screen.Place(examResults, 2, 0);
            _screen.Place(continueButton, 4, 1);
        }


        #endregion


        #region ExamNeck

        public void DesignScreenExamNeck()
        {
            // 24 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into five rows
            #region Rows
            _screen.AddRow(2 * defaultHeight); // 0 row at the top of screen for title
            _screen.AddRow(10 * defaultHeight); // 1 row for spacing

            _screen.AddRow(2 * defaultHeight); // 2 data value
            _screen.AddRow(6 * defaultHeight); // 3 row for spacing
            _screen.AddRow(2 * defaultHeight); // 4 row for button
            _screen.AddFinalRow(); // 5 final row for spacing

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns

            // Add columns to rows with data
            _screen.AddColumn(2, 4); //  Data value goes here
            _screen.AddColumn(2, 8);

            // Add columns to row with button
            _screen.AddColumn(4, 4);
            _screen.AddColumn(4, 4); // button goes here
            _screen.AddColumn(4, 4);

            #endregion
        }

        public void PlaceElementsExamNeck(Texture2D buttonTexture, SpriteFont font)
        {
            Controls.Textbox textTitle = new Controls.Textbox(font, "Results from Neck Exam");
            Controls.Textbox examResults = new Controls.Textbox(font, $"Observations: {PatientData.NeckData}");

            Controls.Button continueButton = new Controls.Button(buttonTexture, font) { Text = "Continue" };

            continueButton.Click += ContinueButton_Click;

            _screen.Place(textTitle, 0, 0);
            _screen.Place(examResults, 2, 0);
            _screen.Place(continueButton, 4, 1);
        }

        #endregion


        #region ExamLungs

        public void DesignScreenExamLungs()
        {
            // 24 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into five rows
            #region Rows
            _screen.AddRow(2 * defaultHeight); // 0 row at the top of screen for title
            _screen.AddRow(10 * defaultHeight); // 1 row for spacing

            _screen.AddRow(2 * defaultHeight); // 2 data value
            _screen.AddRow(6 * defaultHeight); // 3 row for spacing
            _screen.AddRow(2 * defaultHeight); // 4 row for button
            _screen.AddFinalRow(); // 5 final row for spacing

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns

            // Add columns to rows with data
            _screen.AddColumn(2, 4); //  Data value goes here
            _screen.AddColumn(2, 8);

            // Add columns to row with button
            _screen.AddColumn(4, 4);
            _screen.AddColumn(4, 4); // button goes here
            _screen.AddColumn(4, 4);

            #endregion
        }

        public void PlaceElementsExamLungs(Texture2D buttonTexture, SpriteFont font)
        {
            Controls.Textbox textTitle = new Controls.Textbox(font, "Results from Lung Exam");
            Controls.Textbox examResults = new Controls.Textbox(font, $"Observations: {PatientData.LungData}");

            Controls.Button continueButton = new Controls.Button(buttonTexture, font) { Text = "Continue" };

            continueButton.Click += ContinueButton_Click;

            _screen.Place(textTitle, 0, 0);
            _screen.Place(examResults, 2, 0);
            _screen.Place(continueButton, 4, 1);
        }

        #endregion


        #region ExamExtremities

        public void DesignScreenExamExtremities()
        {
            // 24 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into five rows
            #region Rows
            _screen.AddRow(2 * defaultHeight); // 0 row at the top of screen for title
            _screen.AddRow(10 * defaultHeight); // 1 row for spacing

            _screen.AddRow(2 * defaultHeight); // 2 data value
            _screen.AddRow(6 * defaultHeight); // 3 row for spacing
            _screen.AddRow(2 * defaultHeight); // 4 row for button
            _screen.AddFinalRow(); // 5 final row for spacing

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns

            // Add columns to rows with data
            _screen.AddColumn(2, 4); //  Data value goes here
            _screen.AddColumn(2, 8);

            // Add columns to row with button
            _screen.AddColumn(4, 4);
            _screen.AddColumn(4, 4); // button goes here
            _screen.AddColumn(4, 4);

            #endregion
        }

        public void PlaceElementsExamExtremities(Texture2D buttonTexture, SpriteFont font)
        {
            Controls.Textbox textTitle = new Controls.Textbox(font, "Results from Examining Extremities");
            Controls.Textbox examResults = new Controls.Textbox(font, $"Observations: {PatientData.ExtremitiesData}");

            Controls.Button continueButton = new Controls.Button(buttonTexture, font) { Text = "Continue" };

            continueButton.Click += ContinueButton_Click;

            _screen.Place(textTitle, 0, 0);
            _screen.Place(examResults, 2, 0);
            _screen.Place(continueButton, 4, 1);
        }

        #endregion


        #region ExamSkin

        public void DesignScreenExamSkin()
        {
            // 24 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into five rows
            #region Rows
            _screen.AddRow(2 * defaultHeight); // 0 row at the top of screen for title
            _screen.AddRow(10 * defaultHeight); // 1 row for spacing

            _screen.AddRow(2 * defaultHeight); // 2 data value
            _screen.AddRow(6 * defaultHeight); // 3 row for spacing
            _screen.AddRow(2 * defaultHeight); // 4 row for button
            _screen.AddFinalRow(); // 5 final row for spacing

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns

            // Add columns to rows with data
            _screen.AddColumn(2, 4); //  Data value goes here
            _screen.AddColumn(2, 8);

            // Add columns to row with button
            _screen.AddColumn(4, 4);
            _screen.AddColumn(4, 4); // button goes here
            _screen.AddColumn(4, 4);

            #endregion
        }

        public void PlaceElementsExamSkin(Texture2D buttonTexture, SpriteFont font)
        {
            Controls.Textbox textTitle = new Controls.Textbox(font, "Results from Skin Exam");
            Controls.Textbox examResults = new Controls.Textbox(font, $"Observations: {PatientData.SkinData}");

            Controls.Button continueButton = new Controls.Button(buttonTexture, font) { Text = "Continue" };

            continueButton.Click += ContinueButton_Click;

            _screen.Place(textTitle, 0, 0);
            _screen.Place(examResults, 2, 0);
            _screen.Place(continueButton, 4, 1);
        }

        #endregion


        #region ExamAbdomen

        public void DesignScreenExamAbdomen()
        {
            // 24 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into five rows
            #region Rows
            _screen.AddRow(2 * defaultHeight); // 0 row at the top of screen for title
            _screen.AddRow(10 * defaultHeight); // 1 row for spacing

            _screen.AddRow(2 * defaultHeight); // 2 data value
            _screen.AddRow(6 * defaultHeight); // 3 row for spacing
            _screen.AddRow(2 * defaultHeight); // 4 row for button
            _screen.AddFinalRow(); // 5 final row for spacing

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns

            // Add columns to rows with data
            _screen.AddColumn(2, 4); //  Data value goes here
            _screen.AddColumn(2, 8);

            // Add columns to row with button
            _screen.AddColumn(4, 4);
            _screen.AddColumn(4, 4); // button goes here
            _screen.AddColumn(4, 4);

            #endregion
        }

        public void PlaceElementsExamAbdomen(Texture2D buttonTexture, SpriteFont font)
        {
            Controls.Textbox textTitle = new Controls.Textbox(font, "Results from Abdomen Exam");
            Controls.Textbox examResults = new Controls.Textbox(font, $"Observations: {PatientData.AbdomenData}");

            Controls.Button continueButton = new Controls.Button(buttonTexture, font) { Text = "Continue" };

            continueButton.Click += ContinueButton_Click;

            _screen.Place(textTitle, 0, 0);
            _screen.Place(examResults, 2, 0);
            _screen.Place(continueButton, 4, 1);
        }

        #endregion


        #region ExamOxygen

        public void DesignScreenExamOxygen()
        {
            // 24 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into five rows
            #region Rows
            _screen.AddRow(2 * defaultHeight); // 0 row at the top of screen for title
            _screen.AddRow(8 * defaultHeight); // 1 row for spacing

            _screen.AddRow(2 * defaultHeight); // 2 first data value
            _screen.AddRow(2 * defaultHeight); // 3 row for spacing
            _screen.AddRow(2 * defaultHeight); // 4 Second data value
            _screen.AddRow(2 * defaultHeight); // 5 row for spacing
            _screen.AddRow(2 * defaultHeight); // 6 row for button
            _screen.AddFinalRow(); // final row for spacing

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns

            // Add columns to rows with data
            _screen.AddColumn(2, 4); // First Data value goes here
            _screen.AddColumn(2, 8);

            _screen.AddColumn(4, 4); // Second Data value goes here
            _screen.AddColumn(4, 8);

            // Add columns to row with button
            _screen.AddColumn(6, 4);
            _screen.AddColumn(6, 4); // button goes here
            _screen.AddColumn(6, 4);

            #endregion
        }

        public void PlaceElementsExamOxygen(Texture2D buttonTexture, SpriteFont font)
        {
            Controls.Button continueButton = new Controls.Button(buttonTexture, font) { Text = "Continue" };
            Controls.Textbox textTitle = new Controls.Textbox(font, "Review of Oxygen Information");

            Controls.Textbox dataValue1 = new Controls.Textbox(font, $"Oxygen Saturation: {PatientData.OxygenData[0]}");
            Controls.Textbox dataValue2 = new Controls.Textbox(font, $"Amount of Oxygen Given: {PatientData.OxygenData[1]}");

            // Add event handler
            continueButton.Click += ContinueButton_Click;

            // Place elements
            _screen.Place(textTitle, 0, 0);
            _screen.Place(dataValue1, 2, 0);
            _screen.Place(dataValue1, 4, 0);
            _screen.Place(continueButton, 6, 1);

        }

        #endregion


        #region ExamBloodwork

        public void DesignScreenExamBloodwork()
        {
            // 24 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into five rows
            #region Rows

            _screen.AddRow(2 * defaultHeight); // 0 row at the top of screen for title
            _screen.AddRow(4 * defaultHeight); // 1 row for spacing

            _screen.AddRow(1 * defaultHeight);          // 2 row for two values
            _screen.AddRow((int)(0.5 * defaultHeight)); // 3 row for spacing
            _screen.AddRow(1 * defaultHeight);          // 4 row for two values
            _screen.AddRow((int)(0.5 * defaultHeight)); // 5 row for spacing
            _screen.AddRow(1 * defaultHeight);          // 6 row for two values
            _screen.AddRow((int)(0.5 * defaultHeight)); // 7 row for spacing
            _screen.AddRow(1 * defaultHeight);          // 8 row for two values
            _screen.AddRow((int)(0.5 * defaultHeight)); // 9 row for spacing
            _screen.AddRow(1 * defaultHeight);          // 10 row for two values
            _screen.AddRow((int)(0.5 * defaultHeight)); // 11 row for spacing
            _screen.AddRow(1 * defaultHeight);          // 12 row for two values
            _screen.AddRow((int)(0.5 * defaultHeight)); // 13 row for spacing
            _screen.AddRow(1 * defaultHeight);          // 14 row for two values
            _screen.AddRow((int)(0.5 * defaultHeight)); // 15 row for spacing
            _screen.AddRow(1 * defaultHeight);          // 16 row for two values

            _screen.AddRow(3 * defaultHeight);          // 17 row for spacing
            _screen.AddRow(1 * defaultHeight); // 17 row for Continue Button
            _screen.AddFinalRow(); // final row for spacing

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns

            // Add columns to row 17 for Continue button
            _screen.AddColumn(17, 4); 
            _screen.AddColumn(17, 4); // Continue button goes here
            _screen.AddColumn(17, 4);

            // Add columns to rows containing content
            for (int i = 2; i < 17; i += 2)
            {
                _screen.AddColumn(i, 1);
                _screen.AddColumn(i, 4); // Info goes here
                _screen.AddColumn(i, 2);
                _screen.AddColumn(i, 4); // Info goes here
                _screen.AddColumn(i, 1);

            }
            

            #endregion
        }

        public void PlaceElementsExamBloodwork(Texture2D buttonTexture, SpriteFont font)
        {
            Controls.Textbox titleText = new Controls.Textbox(font, "Bloodwork Results");
            Controls.Button continueButton = new Controls.Button(buttonTexture, font) { Text = "Continue" };

            // Add event handler
            continueButton.Click += ContinueButton_Click;

            //Place title Text and button
            _screen.Place(titleText, 0, 0);
            _screen.Place(continueButton, 17, 1);

            #region PlacingDataInfo

            List<String> dataValueTitles = new List<String>();
            #region DefiningPossibleValues

            dataValueTitles.Add("White blood cells");
            dataValueTitles.Add("Hemoglobin");
            dataValueTitles.Add("Hematocrit");
            dataValueTitles.Add("Platelets");
            dataValueTitles.Add("Sodium");
            dataValueTitles.Add("Potassium");
            dataValueTitles.Add("Chloride");
            dataValueTitles.Add("Bicarbonate");
            dataValueTitles.Add("BUN");
            dataValueTitles.Add("Creatinine");
            dataValueTitles.Add("Glucose");
            dataValueTitles.Add("BNP");
            dataValueTitles.Add("ABG - pH");
            dataValueTitles.Add("ABG - pcO2");
            dataValueTitles.Add("ABG - pO2");
            dataValueTitles.Add("Lactate");

            #endregion

            int numberBloodworkValues = dataValueTitles.Count;

            

            int rowIndex = 2;
            int colIndex = 1;
            for (int i = 0; i < numberBloodworkValues; ++i)
            {
                _screen.Place((new Controls.Textbox(font, $"{dataValueTitles[i]}: {PatientData.BloodworkData[i]}")), rowIndex, colIndex);

                // Every other iteration, increase row value
                // Every iteration, swap between column index 1 and 3
                if (i % 2 == 1)
                {
                    rowIndex += 2;
                    colIndex = 1;
                }
                else
                {
                    colIndex = 3;
                }
                
            }

            #endregion

        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            IsUserFinishedReviewing = true;
        }

        #endregion


        #region ExamImaging

        public void DesignScreenExamImaging()
        {
            // 24 rows exist in the deafult grid, so get the default height
            int defaultHeight = Screen.defaultRowHeight;

            // Divide screen height (24 rows of size defaultHeight pixels) into five rows
            #region Rows

            _screen.AddRow(4 * defaultHeight); // 0 row at the top of screen for title
            _screen.AddRow(8 * defaultHeight); // 1 row for spacing
            _screen.AddRow(2 * defaultHeight); // 2 row for Text explaining lack of database
            _screen.AddRow(4 * defaultHeight); // 3 row for spacing
            _screen.AddRow(2 * defaultHeight); // 4 row for Button to return to main diagnosis page

            _screen.AddFinalRow(); // final row for spacing

            #endregion


            // Divide screen width within rows into columns (there are 12 column units to divide)
            #region Columns

            // Add columns to button row
            _screen.AddColumn(4, 4);
            _screen.AddColumn(4, 4); // Okay button goes here
            _screen.AddColumn(4, 4);

            #endregion
        }

        public void PlaceElementsExamImaging(Texture2D buttonTexture, SpriteFont font)
        {
            Controls.Textbox titleText = new Controls.Textbox(font, "Imaging Results");
            Controls.Button okayButton = new Controls.Button(buttonTexture, font) { Text = "Okay" };
            Controls.Textbox infoText = new Controls.Textbox(font, "At the time of the alpha release, no imaging results are available. Stay tuned for imaging results in the Beta release.");

            okayButton.Click += OkayButton_Click;

            _screen.Place(titleText, 0, 0);
            _screen.Place(infoText, 2, 0);
            _screen.Place(okayButton, 4, 1);
        }

        private void OkayButton_Click(object sender, EventArgs e)
        {
            IsUserFinishedReviewing = true;
        }

        #endregion


        #endregion



        #endregion


        #region Functions
        public SymptomInfoPlayPage()
        {
            IsUserFinishedReviewing = false;
        }

        public SymptomInfoPlayPage(PatientData patientInfo, SymptomState selectedSymptom, Texture2D buttonTexture, SpriteFont font)
        {
            PatientData = patientInfo;
            IsUserFinishedReviewing = false;
            SymptomInfoStatus = selectedSymptom;
        }

        // Updating the screen according to the selectedSymptom
        public void ChangeSymptomInfo(SymptomState selectedSymptom, Texture2D buttonTexture, SpriteFont font)
        {
            // Reset screen object
            _screen = new Screen();
            SymptomInfoStatus = selectedSymptom;

            switch (selectedSymptom)
            {
                case SymptomState.General:
                    {
                        // Design screen and place elements according to design for Extremities
                        DesignScreenExamGeneral();
                        PlaceElementsExamGeneral(buttonTexture, font);
                        break;
                    }
                case SymptomState.Head:
                    {
                        // Design screen and place elements according to design for Extremities
                        DesignScreenExamHead();
                        PlaceElementsExamHead(buttonTexture, font);
                        break;
                    }
                case SymptomState.Neck:
                    {
                        // Design screen and place elements according to design for Extremities
                        DesignScreenExamNeck();
                        PlaceElementsExamNeck(buttonTexture, font);
                        break;
                    }
                case SymptomState.Lungs:
                    {
                        // Design screen and place elements according to design for Extremities
                        DesignScreenExamLungs();
                        PlaceElementsExamLungs(buttonTexture, font);
                        break;
                    }
                case SymptomState.Extremities:
                    {
                        // Design screen and place elements according to design for Extremities
                        DesignScreenExamExtremities();
                        PlaceElementsExamExtremities(buttonTexture, font);
                        break;
                    }
                case SymptomState.Skin:
                    {
                        // Design screen and place elements according to design for Skin
                        DesignScreenExamSkin();
                        PlaceElementsExamSkin(buttonTexture, font);
                        break;
                    }
                case SymptomState.Abdomen:
                    {
                        // Design screen and place elements according to design for Abdomen
                        DesignScreenExamAbdomen();
                        PlaceElementsExamAbdomen(buttonTexture, font);
                        break;
                    }
                case SymptomState.Oxygen:
                    {
                        // Design screen and place elements according to design for Oxygen
                        DesignScreenExamOxygen();
                        PlaceElementsExamOxygen(buttonTexture, font);
                        break;
                    }
                case SymptomState.Bloodwork:
                    {
                        // Design screen and place elements according to design for Bloodwork
                        DesignScreenExamBloodwork();
                        PlaceElementsExamBloodwork(buttonTexture, font);
                        break;
                    }
                case SymptomState.Imaging:
                    {
                        // Design screen and place elements according to design for Imaging
                        DesignScreenExamImaging();
                        PlaceElementsExamImaging(buttonTexture, font);
                        break;
                    }
            }
            return;
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
