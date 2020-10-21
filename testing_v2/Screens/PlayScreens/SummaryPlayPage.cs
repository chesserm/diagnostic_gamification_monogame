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

        #endregion

        #region HelperFunctions

        private void DesignScreenLayout()
        {

        }

        private void CreateAndPlaceElements(Texture2D buttonTexture, SpriteFont font)
        {
        }
        #endregion

        #region Functions

        public SummaryPlayPage(Texture2D buttonTexture, SpriteFont font)
        {
            IsUserFinishedWithPage = false;

            _buttonTexture = buttonTexture;
            _font = font;

            DesignScreenLayout();
            CreateAndPlaceElements(buttonTexture, font);

        }

        // Redraw summary page
        public void UpdateSummaryPage()
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
