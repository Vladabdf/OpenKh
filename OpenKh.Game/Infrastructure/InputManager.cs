﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace OpenKh.Game.Infrastructure
{
    public class InputManager
    {
        private GamePadState pad;
        private KeyboardState keyboard;
        private KeyboardState prevKeyboard;
        private RepeatableKeyboard repeatableKeyboard = new RepeatableKeyboard();

        public bool IsDebug => repeatableKeyboard.IsKeyRepeat(Keys.Tab);
        public bool IsShift => repeatableKeyboard.IsKeyRepeat(Keys.RightShift);
        public bool IsDebugRight => repeatableKeyboard.IsKeyRepeat(Keys.Right);
        public bool IsDebugLeft => repeatableKeyboard.IsKeyRepeat(Keys.Left);
        public bool IsDebugUp => repeatableKeyboard.IsKeyRepeat(Keys.Up);
        public bool IsDebugDown => repeatableKeyboard.IsKeyRepeat(Keys.Down);

        public bool IsExit => pad.Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape);
        public bool IsUp => Up && !prevKeyboard.IsKeyDown(Keys.Up);
        public bool IsDown => Down && !prevKeyboard.IsKeyDown(Keys.Down);
        public bool IsLeft => Left && !prevKeyboard.IsKeyDown(Keys.Left);
        public bool IsRight => Right && !prevKeyboard.IsKeyDown(Keys.Right);
        public bool IsCircle => repeatableKeyboard.IsKeyRepeat(Keys.K);
        public bool IsCross => repeatableKeyboard.IsKeyRepeat(Keys.L);

        public bool Up => keyboard.IsKeyDown(Keys.Up);
        public bool Down => keyboard.IsKeyDown(Keys.Down);
        public bool Left => keyboard.IsKeyDown(Keys.Left);
        public bool Right => keyboard.IsKeyDown(Keys.Right);
        public bool A => keyboard.IsKeyDown(Keys.A);
        public bool D => keyboard.IsKeyDown(Keys.D);
        public bool S => keyboard.IsKeyDown(Keys.S);
        public bool W => keyboard.IsKeyDown(Keys.W);

        public void Update(GameTime gameTime)
        {
            pad = GamePad.GetState(PlayerIndex.One);
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            var pressedKeys = prevKeyboard.GetPressedKeys();
            var pressingKeys = keyboard.GetPressedKeys();

            foreach (var keyDown in pressingKeys.Except(pressedKeys))
            {
                repeatableKeyboard.PressKey(keyDown);
            }

            foreach (var keyUp in pressedKeys.Except(pressingKeys))
            {
                repeatableKeyboard.ReleaseKey(keyUp);
            }

            var seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var keepDown in pressedKeys.Intersect(pressingKeys))
            {
                repeatableKeyboard.UpdateKey(keepDown, seconds);
            }
        }
    }
}