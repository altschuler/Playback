using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Playback.Control;

namespace Playback.Helpers
{
    public static class InputHelper
    {
        private static MouseState LastMouseState;

        public static InputState CreateInputState(KeyboardState keyState, MouseState mouseState)
        {
            var state = new InputState();

            if (keyState.IsKeyDown(Keys.Right))
                state.TimeDirection = TimeDirection.Forward;
            else if (keyState.IsKeyDown(Keys.Left))
                state.TimeDirection = TimeDirection.Backward;
            else
                state.TimeDirection = TimeDirection.Stopped;

            state.MousePosition = new Vector2(mouseState.X, mouseState.Y);
            state.LeftMousePressed = mouseState.LeftButton == ButtonState.Pressed;
            state.RightMousePressed = mouseState.RightButton == ButtonState.Pressed;
            state.LeftMouseClicked = mouseState.LeftButton == ButtonState.Released
                && LastMouseState.LeftButton == ButtonState.Pressed;

            // save state for recognizing steps
            LastMouseState = mouseState;

            return state;
        }
    }
}