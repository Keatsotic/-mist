using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mist.Data;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Animations.SpriteSheets;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using Mist.Utilities;
using Mist.Manager;

namespace Mist.GameObjects
{
    public class Player : GameObject
    {

        protected static float[] posX = new float[64];
        protected static float[] posY = new float[64];


        public Player()
        { }

        public Player(Vector2 _position)
        {
            position = _position;
            depth = 0;
        }

        public override void Initialize()
        {
            speed = 1;

            for (var i = 0; i < 64; i++)
            {
                posX[i] = position.X;
                posY[i] = position.Y;
            }

            spriteSheetData = new SpriteSheetData(16, 24,
                                                  new List<string> { "Down", "DownRight", "Right", "UpRight", "Up", "UpLeft", "Left", "DownLeft" },
                                                  new List<int[]> { new[] { 0, 1 }, new[] { 2, 3 }, new[] { 4, 5 }, new[] { 6, 7 }, new[] { 8, 9 }, new[] { 10, 11 }, new[] { 12, 13 }, new[] { 14, 15 } },
                                                  new List<float> { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f },
                                                  new List<bool> { true, true, true, true, true, true, true, true });
            base.Initialize();
        }

        public override void Load(ContentManager content, MapManager _mapManager)
        {
            image = TextureLoader.Load("Textures/Ness", content);

            var spriteWidth = spriteSheetData.Width;
            var spriteHeight = spriteSheetData.Height;
            var objectTexture = image;
            var objectAtlas = TextureAtlas.Create("objectAtlas", objectTexture, spriteWidth, spriteHeight);

            var animationFactory = new SpriteSheetAnimationFactory(objectAtlas);
            for (int i = 0; i < spriteSheetData.AnimationName.Count; i++)
            {
                animationFactory.Add(spriteSheetData.AnimationName[i], new SpriteSheetAnimationData(spriteSheetData.FrameArray[i], spriteSheetData.FrameDuration[i], spriteSheetData.IsLooping[i]));
            }

            animatedSprite = new AnimatedSprite(animationFactory, "Down");

            sprite = animatedSprite;

            animatedSprite.Origin = Vector2.Zero;
            base.Load(content, _mapManager);
        }

        public override void Update(GameTime gameTime, List<GameObject> objects)
        {
            RecordPreviousPosition();

            switch (directionFacing)
            {
                case (int)DirFacing.Right:
                    animatedSprite.Play("Right");
                    break;
                case (int)DirFacing.UpRight:
                    animatedSprite.Play("UpRight");
                    break;
                case (int)DirFacing.Up:
                    animatedSprite.Play("Up");
                    break;
                case (int)DirFacing.UpLeft:
                    animatedSprite.Play("UpLeft");
                    break;
                case (int)DirFacing.Left:
                    animatedSprite.Play("Left");
                    break;
                case (int)DirFacing.DownLeft:
                    animatedSprite.Play("DownLeft");
                    break;
                case (int)DirFacing.Down:
                    animatedSprite.Play("Down");
                    break;
                case (int)DirFacing.DownRight:
                    animatedSprite.Play("DownRight");
                    break;
                default:
                    isMoving = false;
                    break;
            }

            if (directionFacing != 9)
            {
                isMoving = true;
                Collisions(dirUnitVector, 0, (spriteSheetData.Height / 2)+1);
                CheckCollisionDoor();
            }

            sprite.Depth = position.Y / 10000;

            if (isMoving)
            {
                animatedSprite.Update(gameTime);
            }

            RecordPosition();

            Camera2D.Update(position);

            isMoving = false;

            GetDirectionFacing();
            BoundingBoxSprite = new Rectangle((int)position.X, (int)position.Y + (spriteSheetData.Height / 2), 16, 12);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
#if DEBUG
            if (boundingBoxTexture != null)
            {
                spriteBatch.Draw(boundingBoxTexture, BoundingBoxSprite, Color.Red);
            }
            
#endif
            base.Draw(spriteBatch);
        }

        protected void RecordPosition()
        {
            if (position.X != posX[0] || position.Y != posY[0])
            {
                
                //update record of positions
                //shift records down by one position
                for (var i = 64 - 1; i > 0; i--)
                {
                    posX[i] = posX[i - 1];
                    posY[i] = posY[i - 1];
                }

                posX[0] = position.X;
                posY[0] = position.Y;
            }
        }

        public void CheckCollisionDoor()
        {
            var doorRect = new Rectangle((int)position.X,
                                         (int)position.Y + (spriteSheetData.Height / 2),
                                         BoundingBoxSprite.Width,
                                         BoundingBoxSprite.Height);

            mapManager.CheckCollisionDoor(doorRect);
        }
    }
}
