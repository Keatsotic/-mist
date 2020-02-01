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
using Microsoft.Xna.Framework.Content;
using Mist.MenuSystem;

namespace Mist.Manager
{
    public class WindowManager
    {
        private List<Window> activeWindows = new List<Window>();

        private BaseMenu baseMenu;
        private InventoryMenu inventoryMenu;
        private StatusMenu statusMenu;
        private EquipMenu equipMenu;

        private Window dialogueBox;

        public WindowManager()
        { }

        public void Initialize()
        {
            
        }

        public void Load(ContentManager content)
        { }

        public void Update(GameTime gameTime)
        { }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i=0; i<activeWindows.Count; i++ )
            {
                activeWindows[i].Draw(spriteBatch);
            }
        }
    }
}
