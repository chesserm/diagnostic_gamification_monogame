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
        InitialPlayPage initialPlayPage;
        MainPlayPage mainPlayPage;
        DiagnosePlayPage diagnosePlayPage;
        #endregion

        #region Properties

        public PlayState CurrentPlayState { get; set; }
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
