using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testing_v2.Controls
{
    class Textbox : Component
    {

        #region MemberVariables
        
        private SpriteFont _font;

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

        public string Text { get; set; }

        public Color TextColor { get; set; }

        #endregion

        #region Constructors
        public Textbox(SpriteFont spriteFont, String text)
        {
            _font = spriteFont;
            Text = text;
            TextColor = Color.Black;
        }

        public Textbox(SpriteFont spriteFont, String text, Color textColor)
        {
            _font = spriteFont;
            Text = text;
            TextColor = textColor;
        }

        #endregion

        #region Functions
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // This if statement was directly taken from the tutorial referenced 
            // at the top of Button and seems to be the standard way of doing this 
            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);


                spriteBatch.DrawString(_font, Text, new Vector2(x, y), TextColor);
            }
            return;
        }

        public override void Update(GameTime gameTime)
        {
            // Update not needed for textboxes, but function had to be overridden from Component
            return;
        }

        #endregion
    }
}
