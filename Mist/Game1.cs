using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mist.Manager;
using Mist.Utilities;
using Mist.GameObjects;
using System;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using System.Collections.Generic;
using MonoGame.Extended.Screens.Transitions;
using Mist.Screens;

namespace Mist
{

    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        private readonly ScreenManager _screenManager;

        public List<GameObject> objects = new List<GameObject>();
        

        private TimeSpan _counterElapsed = TimeSpan.Zero;

        private int _fpsCounter;
        public int FpsCounter { get => _fpsCounter; set => _fpsCounter = value; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1f / 60f);

            ResolutionManager.Init(ref graphics);
            ResolutionManager.SetVirtualResolution(400, 240);
            ResolutionManager.SetResolution(1920, 1080, false);

            _screenManager = Components.Add<ScreenManager>();

        }

        protected override void Initialize()
        {
            Camera2D.Initialize();
            _screenManager.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            var transition = new FadeTransition(GraphicsDevice, Color.Black, 0.5f);
            _screenManager.LoadScreen(new TitleScreen(this), transition);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            FpsCounter++;
            _counterElapsed += gameTime.ElapsedGameTime;

            if (_counterElapsed >= TimeSpan.FromSeconds(1))
            {
#if DEBUG
                Window.Title = "MIST " + _fpsCounter.ToString() + "fps - " +
                                (GC.GetTotalMemory(false) / 1048576f).ToString("F") + "MB";
#endif
                FpsCounter = 0;
                _counterElapsed -= TimeSpan.FromSeconds(1);
            }

            base.Draw(gameTime);
        }
    }
}
