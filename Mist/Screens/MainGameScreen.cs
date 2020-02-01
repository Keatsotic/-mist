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
    public class MainGameScreen : GameScreen
    {
        readonly MapManager mapManager;
        SpriteBatch spriteBatch;

        private int doorTimer;

        public MainGameScreen(Game game)
            : base(game)
        {
            mapManager = new MapManager(Global.levelName, Game1.graphics, Content);
        }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            mapManager.Update(gameTime);

            if (mapManager.enterDoor)
            {
                Global.canMove = false;
                doorTimer = 30;
                mapManager.enterDoor = false;
                ScreenManager.LoadScreen(new MainGameScreen(Game), new FadeTransition(GraphicsDevice, Color.Black, 1.0f));
            }

            if(doorTimer > 0)
            {
                doorTimer--;
            }
            else
            {
                Global.canMove = true;
            }
            
        }

        public override void Draw(GameTime gameTime)
        {
            ResolutionManager.BeginDraw();

            spriteBatch.Begin(SpriteSortMode.FrontToBack,
                                   BlendState.AlphaBlend,
                                   SamplerState.PointClamp,
                                   null,
                                   null,
                                   null,
                                   Camera2D.GetTransformMatrix());

            mapManager.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
