using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testing_v2.Controls
{
    class Sprite : Component
    {
        #region MemberVariables

        private Rectangle _rectangle = new Rectangle(0, 0, 100, 100);
        private Vector2 _position = new Vector2(0, 0);

        #endregion

        #region Properties
        public Vector2 Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
            }
        }

        public override Rectangle Rectangle
        {
            get
            {
                return _rectangle;
            }
            set
            {
                _rectangle = value;
                _position = new Vector2(_rectangle.X, _rectangle.Y);
            }
        }

        #endregion

        
        public Sprite(Texture2D texture)
        {
            // Save texture in the inherited texture value from Component
            ComponentTexture = texture;
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // You can change the color of this to Gray if you want to darken the shading
            spriteBatch.Draw(ComponentTexture, Rectangle, Color.White);
            
        }

        public override void Update(GameTime gameTime)
        {
            // Do nothing (this function needs to be overrided bc it inherits from Component)
            // In the future we can add animations here
            return;
        }
    }
}
