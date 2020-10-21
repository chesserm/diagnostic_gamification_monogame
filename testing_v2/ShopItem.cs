using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Text;
using game_state_enums;

namespace testing_v2
{
    class ShopItem : Component
    {
        #region MemberVariables
        private TouchCollection _tc;

        // Unsure about these
        private Rectangle _rectangle = new Rectangle(0, 0, 100, 100);
        private Vector2 _position = new Vector2(0, 0);
        #endregion
        #region Properties

        public int Id { get; set; }

        public int Price { get; set; }

        public ItemType Type { get; set; }

        #endregion

        #region Methods
        public ShopItem(Texture2D texture, ItemType type, int id, int price)
        {
            ComponentTexture = texture;
            Type = type;
            Id = id;
            Price = price;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = Color.White;

            spriteBatch.Draw(ComponentTexture, Rectangle, color);

        }

        public override void Update(GameTime gameTime)
        {
            // Update location based upon touch
            _tc = TouchPanel.GetState();

            if (_tc.Count == 0)
            {
                return;
            }

        }
        #endregion
    }
}
