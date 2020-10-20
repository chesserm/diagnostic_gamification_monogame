using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using game_state_enums;
using testing_v2.Screens.PlayScreens;
using Org.Apache.Http.Cookies;

namespace testing_v2.Screens
{
    class PlayPage
    {
        #region MemberVariables

        // Instance of PatientData (object used to parse database records)
        PatientData patientData;
        
        // Page objects
        InitialPlayPage initialPlayPage;
        MainPlayPage mainPlayPage;
        DiagnosePlayPage diagnosePlayPage;
        SymptomListPage symptomListPlayPage;
        SymptomInfoPlayPage symptomInfoPlayPage;
        ReasoningPlayPage reasoningPlayPage;
        SummaryPlayPage summaryPlayPage;

        // Assets that need to be passed to various functions
        Texture2D _patientTexture;
        Texture2D _buttonTexture;
        SpriteFont _font;

        // Maps for reasoning
        Dictionary<SymptomState, String> userReasoning;
        Dictionary<SymptomState, String> correctReasoning;

        // User's Diagnosis
        DiagnosisState _playerDiagnosis;

        #endregion


        #region Properties

        // Enum to track what the current state is within the game play loop
        public PlayState CurrentPlayState { get; set; }

        // Boolean for state manager to determine if user is done with PlayPage
        // Set to true after one diagnosis and if the user wants to return to main menu from play
        public bool IsUserDoneWithPlay { get; set; }
        #endregion

        #region HelperFunctions

        // This is the function where the database access will happen
        public void getData()
        {
            // Hardcoding values based upon one data record given
            patientData = new PatientData();

            #region InitialData
            patientData.InitialData = new List<String>();
            patientData.InitialData.Add("74"); // Age
            patientData.InitialData.Add("male"); // Gender
            patientData.InitialData.Add("heart failure"); // Past Med History 1
            patientData.InitialData.Add("coronary artery disease"); // Past Med History 2
            patientData.InitialData.Add("COPD"); // Past Med History 3
            patientData.InitialData.Add("current"); // Tobacco Use
            patientData.InitialData.Add("3 days"); // Onset of symptoms
            patientData.InitialData.Add("constant"); // Duration of Symptoms
            patientData.InitialData.Add("exertion"); // Provocating Factors
            patientData.InitialData.Add("chest heaviness"); // Description of Symptoms
            patientData.InitialData.Add("severe"); // Severity of Symptoms
            patientData.InitialData.Add("none"); // Relieving Factors

            initialPlayPage.Age = 74;
            initialPlayPage.Gender = patientData.InitialData[1];
            initialPlayPage.PastMedicalHistory1 = patientData.InitialData[2];
            initialPlayPage.PastMedicalHistory2 = patientData.InitialData[3];
            initialPlayPage.PastMedicalHistory3 = patientData.InitialData[4];
            initialPlayPage.TobaccoUse = patientData.InitialData[5];
            initialPlayPage.SymptomOnset = patientData.InitialData[6];
            initialPlayPage.SymptomDuration = patientData.InitialData[7];
            initialPlayPage.ProvocatingFactors = patientData.InitialData[8];
            initialPlayPage.SymptomDescription = patientData.InitialData[9];
            initialPlayPage.SymptomSeverity = patientData.InitialData[10];
            initialPlayPage.RelievingFactors = patientData.InitialData[11];


            #endregion

            #region GeneralExamInfo

            patientData.GeneralExamData = new List<String>();
            patientData.GeneralExamData.Add("38.4"); // Temperature
            patientData.GeneralExamData.Add("121"); // Heart Rate
            patientData.GeneralExamData.Add("24"); // Respiratory Rate
            patientData.GeneralExamData.Add("104/53"); // Blood Pressure
            patientData.GeneralExamData.Add("awake, alert, oriented x 2"); // Observations

            #endregion

            #region OtherExamInfo
            
            patientData.HeadData = "pupils equal and reactive, dry mucous membranes";
            patientData.NeckData = "No jugular venous distention";
            patientData.LungData = "Crackles in the right lung";
            patientData.ExtremitiesData = "no edema";
            patientData.SkinData = "warm, dry";
            patientData.AbdomenData = "soft, nontender, nondistended";

            #endregion

            #region OxygenInfo
            patientData.OxygenData = new List<String>();
            patientData.OxygenData.Add("91%"); // oxygen saturation
            patientData.OxygenData.Add("4 liters per minute"); // Amount of Oxygen received

            #endregion

            #region BloodworkData

            patientData.BloodworkData = new List<double>();
            patientData.BloodworkData.Add(14.2); // White blood cells
            patientData.BloodworkData.Add(13.6); // Hemoglobin
            patientData.BloodworkData.Add(40.1); // Hematocrit
            patientData.BloodworkData.Add(247); // Platelets
            patientData.BloodworkData.Add(137); // Sodium
            patientData.BloodworkData.Add(4.2); // Potassium
            patientData.BloodworkData.Add(104); // Chloride
            patientData.BloodworkData.Add(21); // Bicarbonate
            patientData.BloodworkData.Add(24); // BUN
            patientData.BloodworkData.Add(1.6); // Creatinine
            patientData.BloodworkData.Add(137); // Glucose
            patientData.BloodworkData.Add(37); // BNP
            patientData.BloodworkData.Add(7.35); // ABG - pH
            patientData.BloodworkData.Add(39); // ABG - pCO2
            patientData.BloodworkData.Add(71); // ABG - pO2
            patientData.BloodworkData.Add(2.4); // Lactate

            #endregion

            // No image given for sample record
            patientData.XRayImage = null;

            // Making up diagnosis since one was not given
            patientData.Diagnosis = DiagnosisState.Pneumonia;

            return;
        }

        #endregion


        #region Functions

        // Constructor
        public PlayPage(Texture2D patientTexture, Texture2D buttonTexture, SpriteFont font)
        {
            // Set state variables
            CurrentPlayState = PlayState.Initial;
            IsUserDoneWithPlay = false;
            _playerDiagnosis = DiagnosisState.Undiagnosed;

            // Set asset variables
            _patientTexture = patientTexture;
            _buttonTexture = buttonTexture;
            _font = font;

            // Initialize child page objects
            initialPlayPage = new InitialPlayPage(buttonTexture, font);
            mainPlayPage = new MainPlayPage(patientTexture, buttonTexture, font);
            diagnosePlayPage = new DiagnosePlayPage(buttonTexture, font);
            symptomListPlayPage = new SymptomListPage(buttonTexture, font);
            reasoningPlayPage = new ReasoningPlayPage();
            summaryPlayPage = new SummaryPlayPage();

            // Get data before passing it into info page (and set data in initialPlayPage)
            getData();


            symptomInfoPlayPage = new SymptomInfoPlayPage(patientData, SymptomState.Nothing, buttonTexture, font);
        }

        // Function that can be called by Game's state manager to reset the play loop
        public void ResetPlayLoop()
        {
            // Set state variables
            CurrentPlayState = PlayState.Initial;
            IsUserDoneWithPlay = false;
        }


        #region DrawAndUpdate

        // Draw for Game
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            switch(CurrentPlayState)
            {
                case PlayState.Initial:
                    {
                        
                        
                        break;
                    }
            }
        }

        public void Update(GameTime gameTime)
        {
            switch (CurrentPlayState)
            {
                case PlayState.Initial:
                    {
                        // Check the flag stored in InitialPlayPage to see if user finished reading initial information
                        if (initialPlayPage.IsUserFinishedWithPage)
                        {
                            // Update state variable for next state in Play loop
                            CurrentPlayState = PlayState.Main;
                        }
                        {
                            // User is not done with the initial page yet, so update this page
                            initialPlayPage.Update(gameTime);
                        }

                        // Reset flag for next time this page is visited
                        // If the user is still on the page, this has no effect
                        // If the user is finished with the page, this resets it for next time
                        initialPlayPage.IsUserFinishedWithPage = false;

                        break;
                    }
                case PlayState.Main:
                    {
                        switch(mainPlayPage.CurrentMainPlayState)
                        {
                            case PlayState.Diagnose:
                                {
                                    // Player is ready to diagnose the patient's ARF
                                    CurrentPlayState = PlayState.Diagnose;
                                    break;
                                }
                            case PlayState.SymptomList:
                                {
                                    // Player wants to investigate a symptom
                                    CurrentPlayState = PlayState.SymptomList;
                                    break;
                                }
                            case PlayState.Back:
                                {
                                    // Player wants to return to main menu of the whole app
                                    IsUserDoneWithPlay = true;
                                    break;
                                }
                            default:
                                {
                                    // User hasn't done anything on main play page yet, so update
                                    mainPlayPage.Update(gameTime);
                                    break;
                                }
                        }

                        // Reset the MainPlayPage's state tracking variable
                        // If the user is still on mainPlayPage, this has no effect
                        // If the user is finished with mainPlayPage (for now), this resets this for next time
                        mainPlayPage.CurrentMainPlayState = PlayState.Main;
                        break;
                    }
                case PlayState.SymptomList:
                    {
                        // Determine which symptom the user wants to investigate
                        // The default value is SymptomState.Nothing
                        switch(symptomListPlayPage.SelectedSymptom)
                        {
                            case SymptomState.Nothing:
                                {
                                    // Default state, just update
                                    // This case MUST be explicitly written since its behavior differs from MainMenu and default
                                    symptomListPlayPage.Update(gameTime);

                                    break;
                                }
                            case SymptomState.MainMenu:
                                {
                                    // The user wants to return to main page of play loop
                                    CurrentPlayState = PlayState.Main;
                                    break;
                                }
                            default:
                                {
                                    // For all other cases, the user wants to investigate a symptom

                                    // Re-create the symptom info page to display correct info
                                    symptomInfoPlayPage.ChangeSymptomInfo(symptomListPlayPage.SelectedSymptom, _buttonTexture, _font);

                                    // Update state to point to info page
                                    CurrentPlayState = PlayState.SymptomInfo;
                                    break;
                                }
                        }

                        // Reset SelectedSymptom variable in symptomListPlayPage
                        // If user hasn't yet selected a page, this has no effect
                        // If user has selected a symptom/returned to main menu, this resets page for next visit
                        symptomListPlayPage.SelectedSymptom = SymptomState.Nothing;

                        break;
                    }
                case PlayState.SymptomInfo:
                    {
                        // Check if user is done reviewing information on the symptom they selected
                        if (symptomInfoPlayPage.IsUserFinishedReviewing)
                        {
                            // User is finished reviewing

                            // Check for edge case (only present during alpha) for imaging
                            if (symptomInfoPlayPage.SymptomInfoStatus == SymptomState.Imaging)
                            {
                                // Set play page back to Main Play page (skip reasoning)
                                CurrentPlayState = PlayState.Main;
                            }
                            else
                            {
                                // Normal case displays reasoning page after symptom info

                                // TODO: NEED TO CHECK WHICH VALUES THE USER HAS ALREADY SEEN AND NOT SEND THEM TO REASONING
                                CurrentPlayState = PlayState.Reasoning;
                            }
                        }
                        else
                        {
                            // User is not finished reviewing symptom info, so update page
                            symptomInfoPlayPage.Update(gameTime);
                        }

                        // Reset symptomInfoPlayPage state tracker
                        // If the user is still on the page, this has no effect
                        // If the user is finished with the page, this resets it for the next visit
                        symptomInfoPlayPage.IsUserFinishedReviewing = false;

                        break;
                    }
                case PlayState.Reasoning:
                    {
                        // Check if the user is finished reviewing summary page
                        if (reasoningPlayPage.IsUserFinishedWithPage)
                        {
                            // User is finished with reasoning page (selected a reasoning), return to main page of play
                            CurrentPlayState = PlayState.Main;
                        }
                        else
                        {
                            // User is not yet finished, update page
                            reasoningPlayPage.Update(gameTime);
                        }

                        // Reset Review page's state variable
                        // If user isn't finished with page, this has no effect
                        // If user is finished with page, this resets it.
                        reasoningPlayPage.IsUserFinishedWithPage = false;
                        break;
                    }
                case PlayState.Diagnose:
                    {
                        switch(diagnosePlayPage.PatientDiagnosis)
                        {
                            case DiagnosisState.Undiagnosed:
                                {
                                    // Undiagnosed (player hasn't selected anything)
                                    diagnosePlayPage.Update(gameTime);
                                    break;
                                }
                            case DiagnosisState.Back:
                                {
                                    // Player mistakenly chose diagnose and wants to return to main play page
                                    CurrentPlayState = PlayState.Main;
                                    break;
                                }
                            default:
                                {
                                    // In all other cases, Player has made a diagnosis

                                    // Update diagnosis variable
                                    _playerDiagnosis = diagnosePlayPage.PatientDiagnosis;

                                    // Change state to go to summary
                                    CurrentPlayState = PlayState.Summary;
                                    break;
                                }
                        }

                        // Reset DiagnosisState variable for next time page is visited
                        // If user hasn't selected a diagnosis yet, this has no effect
                        // If user has selected a diagnosis/gone back, this resets it for next visit
                        diagnosePlayPage.PatientDiagnosis = DiagnosisState.Undiagnosed;
                        break;
                    }
                case PlayState.Summary:
                    {
                        // Check if the user is finished reviewing summary page
                        if (summaryPlayPage.IsUserFinishedWithPage)
                        {
                            // User is finished with summary page, return to main menu of app
                            IsUserDoneWithPlay = true;
                            CurrentPlayState = PlayState.Main;
                        }
                        else
                        {
                            // User is not yet finished, update page
                            summaryPlayPage.Update(gameTime);
                        }

                        // Reset summary page's state variable
                        // If user isn't finished with page, this has no effect
                        // If user is finished with page, this resets it.
                        summaryPlayPage.IsUserFinishedWithPage = false;
                        break;
                    }
            }
        }
        
        #endregion

        #endregion
    }
}
