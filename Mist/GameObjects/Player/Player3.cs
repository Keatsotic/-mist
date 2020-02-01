﻿using System.Collections.Generic;
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
    public class Player3 : Player
    {
        public Player3()
        {
        }

        public Player3(Vector2 _position)
        {
            position = _position;
        }

        public override void Initialize()
        {
            spriteSheetData = new SpriteSheetData(16, 24,
                                                  new List<string> { "Down", "DownRight", "Right", "UpRight", "Up", "UpLeft", "Left", "DownLeft" },
                                                  new List<int[]> { new[] { 0, 1 }, new[] { 2, 3 }, new[] { 4, 5 }, new[] { 6, 7 }, new[] { 8, 9 }, new[] { 10, 11 }, new[] { 12, 13 }, new[] { 14, 15 } },
                                                  new List<float> { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f },
                                                  new List<bool> { true, true, true, true, true, true, true, true });

        }

        public override void Load(ContentManager content, MapManager _mapManager)
        {
            image = TextureLoader.Load("Textures/Jeff", content);

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
            
        }

        public override void Update(GameTime gameTime, List<GameObject> objects)
        {
            GetDirectionFacingParty();
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
            }

            sprite.Depth = position.Y / 10000;

            if (isMoving)
            {
                animatedSprite.Update(gameTime);
            }

            isMoving = false;

            position.X = posX[33];
            position.Y = posY[33];
        }
    }
}
