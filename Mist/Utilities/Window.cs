using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mist.GameObjects;
using System;
using MonoGame.Extended;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace Mist.Utilities
{
    public class Window
    {

        protected Texture2D windowTopLeft;
        protected Texture2D windowTopCenter;
        protected Texture2D windowTopRight;
        protected Texture2D windowSideLeft;
        protected Texture2D windowSideRight; 
        protected Texture2D windowBottomLeft;
        protected Texture2D windowBottomCenter;
        protected Texture2D windowBottomRight;
        protected Texture2D windowFill;

        protected Vector2 position;
        protected float scaleX;
        protected float scaleY;

        public Window()
        {
        }

        public virtual void Initialize()
        { }

        public virtual void Load(ContentManager Content)
        {

        }

        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw(SpriteBatch spritebatch)
        { }

        protected void WindowSize(float width, float height)
        {

        }
    }
}
