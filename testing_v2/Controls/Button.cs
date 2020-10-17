using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Text;

namespace testing_v2.Controls
{
    // NOTE: This class used the following tutorial as a major resource:
    // MonoGame Tutorial 012 - Interface Buttons by Oyyou
    // https://www.youtube.com/watch?v=lcrgj26G5Hg&list=PLV27bZtgVIJqoeHrQq6Mt_S1-Fvq_zzGZ&index=12 

    class Button : Component
    {
        #region MemberVariables
        private TouchCollection _tc;
        //private Texture2D _buttonTexture;
        private SpriteFont _buttonFont;

        private Rectangle _rectangle = new Rectangle(0,0,100, 100);
        private Vector2 _position = new Vector2(0,0);


        #endregion

        #region Properties
        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Vector2 Position { 
            get
            {
                return _position;
            }

            set
            {
                _position = value;
            }
        }

        public Color TextColor { get; set; }

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

        public string Text { get; set; }
        #endregion


        #region Constructor
        public Button(Texture2D texture, SpriteFont font)
        {
            ComponentTexture = texture;
            _buttonFont = font;
            TextColor = Color.Black;
        }
        #endregion

        #region Functions
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = Color.White;

            if (Clicked)
            {
                color = Color.Gray;
            }

            spriteBatch.Draw(ComponentTexture, Rectangle, color);

            // This if statement was directly taken from the tutorial referenced 
            // above and seems to be the standard way of doing this 
            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_buttonFont.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_buttonFont.MeasureString(Text).Y / 2);


                spriteBatch.DrawString(_buttonFont, Text, new Vector2(x, y), TextColor);
            }
            
        }

        public override void Update(GameTime gameTime)
        {
            // Update location based upon touch
            _tc = TouchPanel.GetState();

            if (_tc.Count == 0)
            {
                return;
            }

            // Create rectangle (used for collision) at location user touches
            var touchRectangle = new Rectangle((int)_tc[0].Position.X, (int)_tc[0].Position.Y, 1, 1);

            // Check for touch collision with button (clicking on button)
            if (Rectangle.Intersects(touchRectangle))
            {
                // Check if null, and if not, fire event handler
                Click?.Invoke(this, new EventArgs());
            }
        }

        #endregion
    }
}
