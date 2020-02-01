using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mist.GameObjects;
using Mist.Map;
using Mist.Utilities;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace Mist.Manager
{
    public class MapManager
    {
        GraphicsDevice graphicsDevice;
        TiledMap tiledMap;
        TiledMapRenderer tiledRenderer;

        private Texture2D tileTexture;
        private readonly List<TileCollision> tileCollisions;
        private readonly List<TileCollisionDoor> tileCollisionsDoor;
        //private List<TileCollisionLadder> _ladders;

        public List<GameObject> objects = new List<GameObject>();
        public bool enterDoor;

        public static string Level = Global.levelName;

        public string MapName { get; private set; }


        public MapManager(string _mapName, GraphicsDeviceManager _graphics, ContentManager content)
        {
            
            MapName = _mapName;

            tileCollisions = new List<TileCollision>();
            tileCollisionsDoor = new List<TileCollisionDoor>();

            tileTexture = TextureLoader.Load("Textures/pixel", content);

            graphicsDevice = _graphics.GraphicsDevice;
            UnloadMap();
            LoadMap(content);
        }


        public void Update(GameTime gameTime)
        {
            tiledRenderer.Update(gameTime);

            if (Global.canMove)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    objects[i].Update(gameTime, objects);
                }
            }
           
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            tiledRenderer.Draw(Camera2D.GetTransformMatrix(), null, null, 0f);

            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Draw(spriteBatch);
            }
            
#if DEBUG
            for (int i = 0; i < tileCollisionsDoor.Count; i++)
            {
                spriteBatch.Draw(tileTexture, tileCollisionsDoor[i].Rectangle, Color.Green);
            }
#endif
            spriteBatch.End();

            spriteBatch.Begin();
            if (tiledMap.GetLayer("Overlay") != null)
            {
                tiledRenderer.Draw(tiledMap.GetLayer("Overlay"), Camera2D.GetTransformMatrix(), null, null, 1f);
            }
        }

        public void UnloadMap()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects.Remove(objects[i]);
            }
        }


        public void LoadMap(ContentManager content)
        {
            tileTexture = content.Load<Texture2D>("Textures/pixel");

            tiledMap = content.Load<TiledMap>("Maps/" + MapName);
            tiledRenderer = new TiledMapRenderer(graphicsDevice);

            tiledRenderer.LoadMap(tiledMap);

            // access object layer in map
            var _objectLayer = tiledMap.GetLayer<TiledMapObjectLayer>("Objects");



            if (_objectLayer != null)
            {
                for (int i = 0; i < _objectLayer.Objects.Length; i++)
                {

                    if (_objectLayer.Objects[i].Name == "PlayerStart")//add the player
                    {
                        if (_objectLayer.Objects[i].Type == Global.doorNumber)
                        {
                            if (Global.partySize > 1)
                            {
                                objects.Add(new Player2(_objectLayer.Objects[i].Position));

                                if (Global.partySize > 2)
                                {
                                    objects.Add(new Player3(_objectLayer.Objects[i].Position));

                                    if (Global.partySize > 3)
                                    {
                                        objects.Add(new Player4(_objectLayer.Objects[i].Position));
                                    }
                                }
                            }
                            objects.Add(new Player(_objectLayer.Objects[i].Position));
                        }
                        
                    }





                    // add doors
                    if (_objectLayer.Objects[i].Name.Contains("Door")) //add enemies
                    { 
                        tileCollisionsDoor.Add(new TileCollisionDoor((int)_objectLayer.Objects[i].Position.X,
                                                         (int)_objectLayer.Objects[i].Position.Y,
                                                          (int)_objectLayer.Objects[i].Size.Width,
                                                         (int)_objectLayer.Objects[i].Size.Height,
                                                         _objectLayer.Objects[i].Type,
                                                         _objectLayer.Objects[i].Name));
                    }

                    // add doors
                    //if (_objectLayer.Objects[i].Type == "Ladder") //add enemies
                    //{
                    //	_ladders.Add(new TileCollisionLadder((int)_objectLayer.Objects[i].Position.X,
                    //									 (int)_objectLayer.Objects[i].Position.Y,
                    //								 	 (int)_objectLayer.Objects[i].Size.Width,
                    //									 (int)_objectLayer.Objects[i].Size.Height));
                    //}
                    
                }
            }

            //access walls in map
            var tiledMapWallsLayer = tiledMap.GetLayer<TiledMapTileLayer>("Wall");

            for (int i = 0; i < tiledMap.Width; i++)
            {
                for (int j = 0; j < tiledMap.Height; j++)
                {
                    if (tiledMapWallsLayer.TryGetTile((ushort)i, (ushort)j, out TiledMapTile? tile))
                    {
                        if (tile.Value.GlobalIdentifier == 1) // make walls
                        {
                            tileCollisions.Add(new TileCollision(i, j));
                        }
                    }
                }
            }

            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Initialize();
                objects[i].Load(content, this);
            }
           
        }

        public bool CheckCollision(Rectangle rectangle)
        {
            return tileCollisions.Any(tile => tile.Intersect(rectangle));
        }

        public void CheckCollisionDoor(Rectangle rectangle)
        {
            for (var i = 0; i < tileCollisionsDoor.Count; i++)
            {
                if (tileCollisionsDoor[i].Intersect(rectangle))
                {
                    Global.doorNumber = tileCollisionsDoor[i].DoorNumber;
                    Global.levelName = tileCollisionsDoor[i].LevelName;
                    enterDoor = true;
                }
            }
        }
    }
}