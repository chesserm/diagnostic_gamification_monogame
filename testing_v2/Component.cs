using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testing_v2
{
    abstract class Component
    {
        public Texture2D ComponentTexture;

        public virtual Rectangle Rectangle { get; set; }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}
