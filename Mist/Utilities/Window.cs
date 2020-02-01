using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mist.GameObjects;
using System;
using MonoGame.Extended;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Mist.Manager;

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

        private Vector2 topRightPosition;
        private Vector2 topCenterPosition;
        private Vector2 leftSidePosition;
        private Vector2 rightSidePosition;
        private Vector2 bottomLeftPosition;
        private Vector2 bottomCenterPosition;
        private Vector2 bottomRightPosition;

        protected int width;
        protected int height;

        protected bool active = false;

        public Window()
        { }

        public Window(int _width, int _height, Vector2 _position)
        {
            width = _width;
            height = _height;
            position = _position;
        }

        public virtual void Initialize()
        {

        }

        public virtual void Load(ContentManager Content)
        {
            windowTopLeft = TextureLoader.Load("Textures/windowTopLeft", Content);
            windowTopCenter = TextureLoader.Load("Textures/windowTopCenter", Content);
            windowTopRight = TextureLoader.Load("Textures/windowTopRight", Content);
            windowSideLeft = TextureLoader.Load("Textures/windowSideLeft", Content);
            windowSideRight = TextureLoader.Load("Textures/windowSideRight", Content);
            windowBottomLeft = TextureLoader.Load("Textures/windowBottomLeft", Content);
            windowBottomCenter = TextureLoader.Load("Textures/windowBottomCenter", Content);
            windowBottomRight = TextureLoader.Load("Textures/windowBottomRight", Content);
            windowFill = TextureLoader.Load("Textures/WindowPixel", Content);
        }

        public virtual void Update(GameTime gameTime)
        {
            WindowSize();
        }

       // [Obsolete]
        public virtual void Draw(SpriteBatch spritebatch)
        {
            //if (active)
            //{
                spritebatch.Draw(windowFill, new Rectangle( (int)position.X + 4, (int)position.Y + 4, width + 4, height + 4), Color.White);
                spritebatch.Draw(windowTopLeft, position, Color.White);

                spritebatch.Draw(windowTopCenter, new Rectangle((int)topCenterPosition.X, (int)topCenterPosition.Y, width, windowTopCenter.Height), Color.White);
                
                spritebatch.Draw(windowTopRight, topRightPosition, Color.White);

                spritebatch.Draw(windowSideLeft, new Rectangle((int)leftSidePosition.X, (int)leftSidePosition.Y, windowSideLeft.Width, height), Color.White);
                spritebatch.Draw(windowSideRight, new Rectangle((int)rightSidePosition.X, (int)rightSidePosition.Y, windowSideRight.Width, height), Color.White);

                spritebatch.Draw(windowBottomLeft, bottomLeftPosition, Color.White);

                spritebatch.Draw(windowBottomCenter, new Rectangle((int)bottomCenterPosition.X, (int)bottomCenterPosition.Y, width, windowBottomCenter.Height), Color.White);


                spritebatch.Draw(windowBottomRight, bottomRightPosition, Color.White);
            ///}
        }

        protected void WindowSize()
        {
            topCenterPosition = position + new Vector2(windowTopLeft.Width, 0);
            topRightPosition = position + new Vector2(windowTopLeft.Width + windowTopCenter.Width * width, 0);

            leftSidePosition = position + new Vector2(0, windowTopLeft.Height);
            rightSidePosition = position + new Vector2(windowTopLeft.Width + windowTopCenter.Width * width, windowTopLeft.Height);

            bottomLeftPosition = position + new Vector2(0, windowTopLeft.Height + windowSideLeft.Height * height);
            bottomCenterPosition = position + new Vector2(windowTopLeft.Width, windowTopLeft.Height + windowSideLeft.Height * height);
            bottomRightPosition = position + new Vector2(windowTopLeft.Width + windowTopCenter.Width * width, windowTopLeft.Height + windowSideLeft.Height * height);
        }
    }
}
