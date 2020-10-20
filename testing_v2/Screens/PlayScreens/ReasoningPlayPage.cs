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

        #endregion


        #region Properties

        public bool IsUserFinishedWithPage { get; set; }

        #endregion


        #region Functions

        public ReasoningPlayPage()
        {
            IsUserFinishedWithPage = false;
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
