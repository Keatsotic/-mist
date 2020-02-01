using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mist.Manager;
using Mist.Utilities;
using Mist.GameObjects;
using System;
using MonoGame.Extended.Screens;
using System.Collections.Generic;
using MonoGame.Extended.Screens.Transitions;

namespace Mist.Screens
{
    public class TitleScreen : GameScreen
    {

        private SpriteBatch _spriteBatch;
        private Texture2D _background;

        public TitleScreen(Game game)
            : base(game)
        {

        }

        public override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = Content.Load<Texture2D>("Textures/TitleScreen");
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Magenta);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyDown(Keys.Enter))
            {

                ScreenManager.LoadScreen(new MainGameScreen(Game), new FadeTransition(GraphicsDevice, Color.Black, 2.5f));
            }
        }
    }
}
