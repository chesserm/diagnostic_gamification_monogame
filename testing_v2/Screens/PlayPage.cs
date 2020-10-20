using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using game_state_enums;
using testing_v2.Screens.PlayScreens;

namespace testing_v2.Screens
{
    class PlayPage
    {
        #region MemberVariables

        PatientData patientData;
        
        InitialPlayPage initialPlayPage;
        MainPlayPage mainPlayPage;
        DiagnosePlayPage diagnosePlayPage;
        SymptomListPage symptomListPlayPage;
        SymptomInfoPlayPage symptomInfoPlayPage;

        #endregion


        #region Properties

        public PlayState CurrentPlayState { get; set; }
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
            CurrentPlayState = PlayState.Initial;

            // Initialize child page objects
            initialPlayPage = new InitialPlayPage(buttonTexture, font);
            mainPlayPage = new MainPlayPage(patientTexture, buttonTexture, font);
            diagnosePlayPage = new DiagnosePlayPage(buttonTexture, font);
            symptomListPlayPage = new SymptomListPage(buttonTexture, font);

            // Get data before passing it into info page
            getData();


            symptomInfoPlayPage = new SymptomInfoPlayPage(patientData, SymptomState.Nothing, buttonTexture, font);


        }


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
                                    break;
                                }
                            case PlayState.SymptomList:
                                {
                                    // Player wants to investigate a symptom
                                    break;
                                }
                            case PlayState.Back:
                                {
                                    // Player wants to return to main menu
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        // Reset the MainPlayPage's state tracking variable
                        mainPlayPage.CurrentMainPlayState = PlayState.Main;
                        break;
                    }
                case PlayState.SymptomList:
                    {
                        break;
                    }
                case PlayState.SymptomInfo:
                    {
                        break;
                    }
                case PlayState.Reasoning:
                    {
                        break;
                    }
                case PlayState.Diagnose:
                    {
                        switch(diagnosePlayPage.PatientDiagnosis)
                        {
                            case DiagnosisState.CHF:
                                {
                                    break;
                                }
                            case DiagnosisState.COPD:
                                {
                                    break;
                                }
                            case DiagnosisState.Pneumonia:
                                {
                                    break;
                                }
                            case DiagnosisState.Back:
                                {
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        // Reset DiagnosisState variable for next time page is visited
                        diagnosePlayPage.PatientDiagnosis = DiagnosisState.Undiagnosed;
                        break;
                    }
                case PlayState.Summary:
                    {
                        break;
                    }
            }
        }

        #endregion
    }
}
