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
using Mist.Manager;
using System;
using Mist.Utilities;

namespace Mist.GameObjects
{
    public class GameObject
    {
        protected Texture2D image;
        protected Texture2D boundingBoxTexture;
        public Vector2 position;
        public Color drawColor = Color.White;
        public float scale = 1f, rotation = 0f;

        protected float speed;

        protected Vector2 previousPos;
        protected float depth;
        protected int directionFacing = 6;
        protected Vector2 dirUnitVector;

        protected SpriteSheetData spriteSheetData;
        protected TextureAtlas textureAtlas;
        protected AnimatedSprite animatedSprite;
        protected Sprite sprite;

        protected MapManager mapManager;

        public Rectangle BoundingBoxSprite { get; set; }

        protected bool isMoving;

        public bool active = true;
        protected Vector2 center;

        public GameObject()
        { }

        public virtual void Initialize()
        {
            CalculateCenter();
        }

        public virtual void Load(ContentManager Content, MapManager _mapManager)
        {
            mapManager = _mapManager;
            boundingBoxTexture = TextureLoader.Load("Textures/Pixel", Content);
        }

        public virtual void Update(GameTime gameTime, List<GameObject> objects)
        {
            GetDirectionFacing();
            BoundingBoxSprite = (Rectangle)sprite.GetBoundingRectangle(position, rotation, new Vector2(1, 1));
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (sprite != null && active)
            {
                spriteBatch.Draw(sprite, position);
            }
        }

        private void CalculateCenter()
        {
            if (image == null)
                return;
            center.X = image.Width / 2;
            center.Y = image.Height / 2;
        }

        protected void GetDirectionFacing()
        {
            if (InputManager.horizontal != 0 || InputManager.vertical != 0)
            {
                dirUnitVector = new Vector2(InputManager.horizontal, InputManager.vertical);
                dirUnitVector = Vector2.Normalize(dirUnitVector);

                var facingAngle = Math.Atan2(-dirUnitVector.Y, dirUnitVector.X) * 180 / Math.PI;

                if (facingAngle < 0)
                {
                    facingAngle += 360;
                }

                directionFacing = (int)Math.Abs(Math.Round(facingAngle / 45));
            }
            else
            {
                directionFacing = 9;
            }

        }

        protected void GetDirectionFacingParty()
        {
            if (previousPos != position)
            {
                dirUnitVector = (position - previousPos);
                dirUnitVector = Vector2.Normalize(dirUnitVector);

                var facingAngle = Math.Atan2(-dirUnitVector.Y, dirUnitVector.X) * 180 / Math.PI;

                if (facingAngle < 0)
                {
                    facingAngle += 360;
                }

                directionFacing = (int)Math.Abs(Math.Round(facingAngle / 45));
            }
            else
            {
                directionFacing = 9;
            }

        }


        protected void RecordPreviousPosition()
        {
            previousPos = position;
        }

        public void Collisions(Vector2 direction, int xOffset, int yOffset)
        {

            //check horizontal collisions
            var velocity = (direction * speed);
            var _hspd = velocity.X;

            if (CheckCollision(new Rectangle((int)(position.X + xOffset + _hspd),
                                             ((int)position.Y + yOffset),
                                             BoundingBoxSprite.Width,
                                             BoundingBoxSprite.Height)))
            {

                while (_hspd != 0 && !CheckCollision(new Rectangle(((int)position.X + xOffset + Math.Sign(_hspd)),
                                                                   ((int)position.Y + yOffset),
                                                                    BoundingBoxSprite.Width,
                                                                    BoundingBoxSprite.Height)))
                {
                    position.X += Math.Sign(_hspd);
                }
                _hspd = 0;
            }

            position.X += _hspd;



            //check vertical collisions
            var _vspd = velocity.Y;

            if (CheckCollision(new Rectangle((int)(position.X + xOffset),
                                             (int)(position.Y + yOffset + _vspd),
                                             BoundingBoxSprite.Width,
                                             BoundingBoxSprite.Height)))
            {
                while (_vspd != 0 && !CheckCollision(new Rectangle((int)(position.X + xOffset),
                                                                   ((int)position.Y + yOffset + Math.Sign(_vspd)),
                                                                    BoundingBoxSprite.Width,
                                                                    BoundingBoxSprite.Height)))
                {
                    position.Y += Math.Sign(_vspd);
                }
                _vspd = 0;
            }
            position.Y += _vspd;

        }

        public bool CheckCollision(Rectangle rectangle)
        {
            return mapManager.CheckCollision(rectangle);
        }
    }
}