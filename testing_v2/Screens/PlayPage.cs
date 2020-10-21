using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using game_state_enums;
using testing_v2.Screens.PlayScreens;
using Org.Apache.Http.Cookies;
using System.IO;

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

        // Variable for tracking the recently selected symptom
        SymptomState lastSelectedSymptom = SymptomState.Nothing;

        // Maps for tracking reasoning
        Dictionary<SymptomState, String> userReasoning = new Dictionary<SymptomState, String>();
        Dictionary<SymptomState, String> correctReasoning = new Dictionary<SymptomState, String>();
        Dictionary<ReasoningState, String> reasoningChoices;

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

        // Function for updating the reasoning page for the user to select from
        public void UpdateReasoningPage(SymptomState selectedSymptom)
        {
            // Reset the reasoningChoices dictionary
            reasoningChoices = new Dictionary<ReasoningState, String>();

            // Populate reasoningChoices with corresponding reasoning choices
            switch (selectedSymptom)
            {
                case SymptomState.General:
                    {
                        reasoningChoices[ReasoningState.Correct] = "General Exam Correct Reasoning";
                        reasoningChoices[ReasoningState.Incorrect1] = "General Exam Incorrect Reasoning 1";
                        reasoningChoices[ReasoningState.Incorrect2] = "General Exam Incorrect Reasoning 2";
                        reasoningChoices[ReasoningState.Incorrect3] = "General Exam Incorrect Reasoning 3";
                        break;
                    }
                case SymptomState.Head:
                    {
                        reasoningChoices[ReasoningState.Correct] = "Head Correct Reasoning";
                        reasoningChoices[ReasoningState.Incorrect1] = "Head Incorrect Reasoning 1";
                        reasoningChoices[ReasoningState.Incorrect2] = "Head Incorrect Reasoning 2";
                        reasoningChoices[ReasoningState.Incorrect3] = "Head Incorrect Reasoning 3";
                        break;
                    }
                case SymptomState.Neck:
                    {
                        reasoningChoices[ReasoningState.Correct] = "Neck Correct Reasoning";
                        reasoningChoices[ReasoningState.Incorrect1] = "Neck Incorrect Reasoning 1";
                        reasoningChoices[ReasoningState.Incorrect2] = "Neck Incorrect Reasoning 2";
                        reasoningChoices[ReasoningState.Incorrect3] = "Neck Incorrect Reasoning 3";
                        break;
                    }
                case SymptomState.Lungs:
                    {
                        reasoningChoices[ReasoningState.Correct] = "Lungs Correct Reasoning";
                        reasoningChoices[ReasoningState.Incorrect1] = "Lungs Incorrect Reasoning 1";
                        reasoningChoices[ReasoningState.Incorrect2] = "Lungs Incorrect Reasoning 2";
                        reasoningChoices[ReasoningState.Incorrect3] = "Lungs Incorrect Reasoning 3";
                        break;
                    }
                case SymptomState.Extremities:
                    {
                        reasoningChoices[ReasoningState.Correct] = "Extremities Correct Reasoning";
                        reasoningChoices[ReasoningState.Incorrect1] = "Extremities Incorrect Reasoning 1";
                        reasoningChoices[ReasoningState.Incorrect2] = "Extremities Incorrect Reasoning 2";
                        reasoningChoices[ReasoningState.Incorrect3] = "Extremities Incorrect Reasoning 3";
                        break;
                    }
                case SymptomState.Skin:
                    {
                        reasoningChoices[ReasoningState.Correct] = "Skin Correct Reasoning";
                        reasoningChoices[ReasoningState.Incorrect1] = "Skin Incorrect Reasoning 1";
                        reasoningChoices[ReasoningState.Incorrect2] = "Skin Incorrect Reasoning 2";
                        reasoningChoices[ReasoningState.Incorrect3] = "Skin Incorrect Reasoning 3";
                        break;
                    }
                case SymptomState.Abdomen:
                    {
                        reasoningChoices[ReasoningState.Correct] = "Abdomen Correct Reasoning";
                        reasoningChoices[ReasoningState.Incorrect1] = "Abdomen Incorrect Reasoning 1";
                        reasoningChoices[ReasoningState.Incorrect2] = "Abdomen Incorrect Reasoning 2";
                        reasoningChoices[ReasoningState.Incorrect3] = "Abdomen Incorrect Reasoning 3";
                        break;
                    }
                case SymptomState.Oxygen:
                    {
                        reasoningChoices[ReasoningState.Correct] = "Oxygen Correct Reasoning";
                        reasoningChoices[ReasoningState.Incorrect1] = "Oxygen Incorrect Reasoning 1";
                        reasoningChoices[ReasoningState.Incorrect2] = "Oxygen Incorrect Reasoning 2";
                        reasoningChoices[ReasoningState.Incorrect3] = "Oxygen Incorrect Reasoning 3";
                        break;
                    }
                case SymptomState.Bloodwork:
                    {
                        reasoningChoices[ReasoningState.Correct] = "Bloodwork Correct Reasoning";
                        reasoningChoices[ReasoningState.Incorrect1] = "Bloodwork Incorrect Reasoning 1";
                        reasoningChoices[ReasoningState.Incorrect2] = "Bloodwork Incorrect Reasoning 2";
                        reasoningChoices[ReasoningState.Incorrect3] = "Bloodwork Incorrect Reasoning 3";
                        break;
                    }
                case SymptomState.Imaging:
                    {
                        reasoningChoices[ReasoningState.Correct] = "Imaging Correct Reasoning";
                        reasoningChoices[ReasoningState.Incorrect1] = "Imaging Incorrect Reasoning 1";
                        reasoningChoices[ReasoningState.Incorrect2] = "Imaging Incorrect Reasoning 2";
                        reasoningChoices[ReasoningState.Incorrect3] = "Imaging Incorrect Reasoning 3";
                        break;
                    }
            }

            // Force the page to redraw with the new reasoning values
            reasoningPlayPage.ReasoningChoices = reasoningChoices;
            reasoningPlayPage.UpdateReasoningPage();


            return;
        }

        // Update the dictionary containing user selected reasoning
        public void UpdateUserReasoning(SymptomState selectedSymptom, ReasoningState userChoice)
        {
            // Update the dictionaries tracking the reasoning for user and correct choices
            // Only use first reasoning choice
            if (!userReasoning.ContainsKey(selectedSymptom))
            {
                userReasoning[selectedSymptom] = reasoningChoices[userChoice];
                correctReasoning[selectedSymptom] = reasoningChoices[ReasoningState.Correct];
            }
            
            return;
        }

        // Ensure the summary page has the reasoning data it needs
        public void SendSummaryPageReasoning()
        {
            summaryPlayPage.UserReasoning = userReasoning;
            summaryPlayPage.CorrectReasoning = correctReasoning;
            summaryPlayPage.PlayerDiagnosis = _playerDiagnosis;
            summaryPlayPage.CorrectDiagnosis = patientData.Diagnosis;
            summaryPlayPage.UpdateSummaryPage();

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

            // Initialize variables


            // Initialize child page objects
            initialPlayPage = new InitialPlayPage(buttonTexture, font);
            mainPlayPage = new MainPlayPage(patientTexture, buttonTexture, font);
            diagnosePlayPage = new DiagnosePlayPage(buttonTexture, font);
            symptomListPlayPage = new SymptomListPage(buttonTexture, font);
            reasoningPlayPage = new ReasoningPlayPage(buttonTexture, font);
            summaryPlayPage = new SummaryPlayPage(buttonTexture, font);

            // Get data before passing it into info page (and set data in initialPlayPage)
            getData();

            // Required to ensure screen is properly updated
            initialPlayPage.UpdateInitialPlayPage();


            symptomInfoPlayPage = new SymptomInfoPlayPage(patientData, SymptomState.Nothing, buttonTexture, font);
        }

        // Function that can be called by Game's state manager to reset the play loop thoroughly
        public void ResetPlayLoop()
        {
            // Set state variables for the PlayPage object
            CurrentPlayState = PlayState.Initial;
            IsUserDoneWithPlay = false;
            userReasoning = new Dictionary<SymptomState, string>();
            correctReasoning = new Dictionary<SymptomState, string>();
            _playerDiagnosis = DiagnosisState.Undiagnosed;

            // Reset state variables for all pages
            initialPlayPage.IsUserFinishedWithPage = false;
            mainPlayPage.CurrentMainPlayState = PlayState.Main;
            symptomListPlayPage.SelectedSymptom = SymptomState.Nothing;
            symptomInfoPlayPage.IsUserFinishedReviewing = false;
            symptomInfoPlayPage.SymptomInfoStatus = SymptomState.Nothing;
            reasoningPlayPage.IsUserFinishedWithPage = false;
            diagnosePlayPage.PatientDiagnosis = DiagnosisState.Undiagnosed;
            summaryPlayPage.IsUserFinishedWithPage = false;


        }


        #region DrawAndUpdate

        // Draw for Game
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            switch(CurrentPlayState)
            {
                case PlayState.Initial:
                    {
                        initialPlayPage.Draw(gameTime, spriteBatch);
                        break;
                    }
                case PlayState.Main:
                    {
                        mainPlayPage.Draw(gameTime, spriteBatch);
                        break;
                    }
                case PlayState.SymptomList:
                    {
                        symptomListPlayPage.Draw(gameTime, spriteBatch);
                        break;
                    }
                case PlayState.SymptomInfo:
                    {
                        symptomInfoPlayPage.Draw(gameTime, spriteBatch);
                        break;
                    }
                case PlayState.Reasoning:
                    {
                        reasoningPlayPage.Draw(gameTime, spriteBatch);
                        break;
                    }
                case PlayState.Diagnose:
                    {
                        diagnosePlayPage.Draw(gameTime, spriteBatch);
                        break;
                    }
                case PlayState.Summary:
                    {
                        summaryPlayPage.Draw(gameTime, spriteBatch);
                        break;
                    }
                default:
                    {
                        // No other cases
                        break;
                    }
            }

            return;
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
                            // Reset Initial's state tracker
                            initialPlayPage.IsUserFinishedWithPage = false;

                            // Update state variable for next state in Play loop
                            CurrentPlayState = PlayState.Main;
                        }
                        {
                            // User is not done with the initial page yet, so update this page
                            initialPlayPage.Update(gameTime);
                        }


                        break;
                    }
                case PlayState.Main:
                    {
                        switch(mainPlayPage.CurrentMainPlayState)
                        {
                            case PlayState.Diagnose:
                                {
                                    // Reset state tracker of main game play page
                                    mainPlayPage.CurrentMainPlayState = PlayState.Main;

                                    // Player is ready to diagnose the patient's ARF (switches screens)
                                    CurrentPlayState = PlayState.Diagnose;
                                    break;
                                }
                            case PlayState.SymptomList:
                                {
                                    // Reset state tracker of main game play page
                                    mainPlayPage.CurrentMainPlayState = PlayState.Main;

                                    // Player wants to investigate a symptom (switches screens)
                                    CurrentPlayState = PlayState.SymptomList;

                                    break;
                                }
                            case PlayState.Back:
                                {
                                    // Player wants to return to main menu of the whole app
                                    IsUserDoneWithPlay = true;

                                    // Reset the state tracker of the main game loop page
                                    mainPlayPage.CurrentMainPlayState = PlayState.Main;
                                    break;
                                }
                            default:
                                {
                                    // User hasn't done anything on main play page yet, so update
                                    mainPlayPage.Update(gameTime);
                                    break;
                                }
                        }

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
                                    // The user wants to return to main page of play loop (switches screens)
                                    CurrentPlayState = PlayState.Main;

                                    // Reset state tracker of symptom list for next visit
                                    symptomListPlayPage.SelectedSymptom = SymptomState.Nothing;
                                    break;
                                }
                            default:
                                {
                                    // For all other cases, the user wants to investigate a symptom

                                    // Re-create the symptom info page to display correct info
                                    symptomInfoPlayPage.ChangeSymptomInfo(symptomListPlayPage.SelectedSymptom, _buttonTexture, _font);

                                    // Update the PlayPage variable tracking last selected symptom (needed for reasoning page)
                                    lastSelectedSymptom = symptomListPlayPage.SelectedSymptom;

                                    // Update state to point to info page (switches screens)
                                    CurrentPlayState = PlayState.SymptomInfo;


                                    // Reset state tracker of symptom list for next visit
                                    // NOTE THIS UPDATE MUST BE THE LAST THING IN THIS CASE BLOCK
                                    symptomListPlayPage.SelectedSymptom = SymptomState.Nothing;
                                    break;
                                }
                        }

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
                                UpdateReasoningPage(lastSelectedSymptom);
                                CurrentPlayState = PlayState.Reasoning;
                            }

                            // Reset state tracker within System Info page for next visit
                            symptomInfoPlayPage.IsUserFinishedReviewing = false;
                            symptomInfoPlayPage.SymptomInfoStatus = SymptomState.Nothing;
                        }
                        else
                        {
                            // User is not finished reviewing symptom info, so update page
                            symptomInfoPlayPage.Update(gameTime);
                        }

                        break;
                    }
                case PlayState.Reasoning:
                    {
                        // Check if the user is finished reviewing summary page
                        if (reasoningPlayPage.IsUserFinishedWithPage)
                        {
                            // Update the dictionaries that track user reasoning choices
                            UpdateUserReasoning(lastSelectedSymptom, reasoningPlayPage.SelectedReasoning);

                            // Reset reasoning page's state tracker
                            reasoningPlayPage.IsUserFinishedWithPage = false;
                            reasoningPlayPage.SelectedReasoning = ReasoningState.Undecided;

                            // User is finished with reasoning page (selected a reasoning), return to main page of play
                            CurrentPlayState = PlayState.Main;
                        }
                        else
                        {
                            // User is not yet finished, update page
                            reasoningPlayPage.Update(gameTime);
                        }

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
                                    // Update diagnosePlayPage's game state
                                    diagnosePlayPage.PatientDiagnosis = DiagnosisState.Undiagnosed;

                                    // Player mistakenly chose diagnose and wants to return to main play page
                                    CurrentPlayState = PlayState.Main;

                                    
                                    break;
                                }
                            default:
                                {
                                    // In all other cases, Player has made a diagnosis

                                    // Update diagnosis variable
                                    _playerDiagnosis = diagnosePlayPage.PatientDiagnosis;

                                    // Update diagnosePlayPage's game state (reset for next visit)
                                    diagnosePlayPage.PatientDiagnosis = DiagnosisState.Undiagnosed;

                                    // Make sure summary page is up to date with reasoning values
                                    SendSummaryPageReasoning();

                                    // Change state to go to summary
                                    CurrentPlayState = PlayState.Summary;
                                    break;
                                }
                        }

                        break;
                    }
                case PlayState.Summary:
                    {
                        // Check if the user is finished reviewing summary page
                        if (summaryPlayPage.IsUserFinishedWithPage)
                        {
                            // Reset summary page's state tracker
                            summaryPlayPage.IsUserFinishedWithPage = false;

                            // DO NOT call resetPlayLoop() here. There will be an infinite loop 

                            // User is finished with summary page, return to main menu of app
                            IsUserDoneWithPlay = true;
                            CurrentPlayState = PlayState.Main;
                        }
                        else
                        {
                            // User is not yet finished, update page
                            summaryPlayPage.Update(gameTime);
                        }

                        break;
                    }
            }
            return;
        }
        
        #endregion

        #endregion
    }
}
