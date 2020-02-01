using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mist.Utilities;

namespace Mist.Manager
{
    public static class InputManager //Static classes can easily be accessed anywhere in our codebase. They always stay in memory so you should only do it for universal things like input.
    {
        private static KeyboardState keyboardState = Keyboard.GetState();
        private static KeyboardState lastKeyboardState;

        private static MouseState mouseState;
        private static MouseState lastMouseState;

        //player controls
        public static bool Left;
        public static bool Right;
        public static bool Up;
        public static bool Down;

        public static bool UpLeft;
        public static bool UpRight;
        public static bool DownLeft;
        public static bool DownRight;

        public static bool LeftPressed;
        public static bool RightPressed;
        public static bool UpPressed;
        public static bool DownPressed;

        public static bool Inspect;
        public static bool Menu;
        public static bool Special;
        public static bool Start;
        public static bool Select;

        public static int horizontal;
        public static int vertical;

        public static void Update()
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            lastMouseState = mouseState;
            mouseState = Mouse.GetState();

            PlayerInputsUpdate();
        }

        /// <summary>
        /// Checks if key is currently pressed.
        /// </summary>
        public static bool IsKeyDown(Keys input)
        {
            return keyboardState.IsKeyDown(input);
        }

        /// <summary>
        /// Checks if key is currently up.
        /// </summary>
        public static bool IsKeyUp(Keys input)
        {
            return keyboardState.IsKeyUp(input);
        }

        /// <summary>
        /// Checks if key was just pressed.
        /// </summary>
        public static bool KeyPressed(Keys input)
        {
            if (keyboardState.IsKeyDown(input) == true && lastKeyboardState.IsKeyDown(input) == false)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns whether or not the left mouse button is being pressed.
        /// </summary>
        public static bool MouseLeftDown()
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns whether or not the right mouse button is being pressed.
        /// </summary>
        public static bool MouseRightDown()
        {
            if (mouseState.RightButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if the left mouse button was clicked.
        /// </summary>
        public static bool MouseLeftClicked()
        {
            if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if the right mouse button was clicked.
        /// </summary>
        public static bool MouseRightClicked()
        {
            if (mouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets mouse coordinates adjusted for virtual resolution and camera position.
        /// </summary>
        public static Vector2 MousePositionCamera()
        {
            Vector2 mousePosition = Vector2.Zero;
            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;

            return ScreenToWorld(mousePosition);
        }

        /// <summary>
        /// Gets the last mouse coordinates adjusted for virtual resolution and camera position.
        /// </summary>
        public static Vector2 LastMousePositionCamera()
        {
            Vector2 mousePosition = Vector2.Zero;
            mousePosition.X = lastMouseState.X;
            mousePosition.Y = lastMouseState.Y;

            return ScreenToWorld(mousePosition);
        }

        /// <summary>
        /// Takes screen coordinates (2D position like where the mouse is on screen) then converts it to world position (where we clicked at in the world). 
        /// </summary>
        private static Vector2 ScreenToWorld(Vector2 input)
        {
            input.X -= ResolutionManager.VirtualViewportX;
            input.Y -= ResolutionManager.VirtualViewportY;

            return Vector2.Transform(input, Matrix.Invert(Camera2D.GetTransformMatrix()));
        }

        private static void PlayerInputsUpdate()
        {
            // check for directional movement
            Left = IsKeyDown(Keys.Left) == true || IsKeyDown(Keys.A) == true || GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed;
            Right = IsKeyDown(Keys.Right) == true || IsKeyDown(Keys.D) == true || GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed;

            Up = IsKeyDown(Keys.Up) == true || IsKeyDown(Keys.W) == true || GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed;
            Down = IsKeyDown(Keys.Down) == true || IsKeyDown(Keys.S) == true || GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed;

            //momentary direction
            LeftPressed = KeyPressed(Keys.Left) == true || GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed;
            RightPressed = KeyPressed(Keys.Right) == true || GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed;

            UpPressed = KeyPressed(Keys.Up) == true || GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed;
            DownPressed = KeyPressed(Keys.Down) == true || GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed;

            //check for button presses
            Inspect = KeyPressed(Keys.V) == true || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed;
            
            Special = KeyPressed(Keys.C) == true || GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed;
            Menu = KeyPressed(Keys.G) == true || GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed;

            Start = KeyPressed(Keys.Enter) == true || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed;
            Select = KeyPressed(Keys.Escape) == true || GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed;

            //Set horizontal and vertical values accordingly based on direction of key presses
            horizontal = Left ? -1 : (Right ? 1 : 0);
            vertical = Up ? -1 : (Down ? 1 : 0);

            
        }
    }
}
