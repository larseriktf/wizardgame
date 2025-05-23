﻿using Windows.System;

namespace WizardGame.App.GameFiles.Input
{

    public static class KeyBoard
    {
        public static InputKey NavContinue { get; } = new InputKey();
        public static InputKey NavPause { get; } = new InputKey();
        public static InputKey NavBack { get; } = new InputKey();

        // Movement keys
        // Standard is Left, Up, Right, Down
        public static InputKey MoveLeft { get; } = new InputKey();
        public static InputKey MoveUp { get; } = new InputKey();
        public static InputKey MoveRight { get; } = new InputKey();
        public static InputKey MoveDown { get; } = new InputKey();

        // Action keys
        // Standard is Left, Up, Right, Down
        public static InputKey Action1 { get; } = new InputKey();
        public static InputKey Action2 { get; } = new InputKey();
        public static InputKey Action3 { get; } = new InputKey();
        public static InputKey Action4 { get; } = new InputKey();

        // Testing purposes
        public static InputKey Interact1 { get; } = new InputKey();
        public static InputKey Interact2 { get; } = new InputKey();
        public static InputKey Interact3 { get; } = new InputKey();

        public static void ConfigureInputKey(VirtualKey virtualKey, bool state)
        {
            switch (virtualKey)
            {
                case VirtualKey.Enter               : NavContinue.Pressed = state; break;
                case VirtualKey.Escape              : NavPause.Pressed = state; break;
                case VirtualKey.Back                : NavBack.Pressed = state; break;

                case VirtualKey.A                   : MoveLeft.Pressed = state; break;
                case VirtualKey.W                   : MoveUp.Pressed = state; break;
                case VirtualKey.D                   : MoveRight.Pressed = state; break;
                case VirtualKey.S                   : MoveDown.Pressed = state; break;

                case VirtualKey.GamepadDPadLeft     : MoveLeft.Pressed = state; break;
                case VirtualKey.GamepadDPadUp       : MoveUp.Pressed = state; break;
                case VirtualKey.GamepadDPadRight    : MoveRight.Pressed = state; break;
                case VirtualKey.GamepadDPadDown     : MoveDown.Pressed = state; break;

                case VirtualKey.Left                : Action1.Pressed = state; break;
                case VirtualKey.Up                  : Action2.Pressed = state; break;
                case VirtualKey.Right               : Action3.Pressed = state; break;
                case VirtualKey.Down                : Action4.Pressed = state; break;

                case VirtualKey.GamepadX            : Action1.Pressed = state; break;
                case VirtualKey.GamepadY            : Action2.Pressed = state; break;
                case VirtualKey.GamepadA            : Action3.Pressed = state; break;
                case VirtualKey.GamepadB            : Action4.Pressed = state; break;

                case VirtualKey.F                   : Interact1.Pressed = state; break;
                case VirtualKey.G                   : Interact2.Pressed = state; break;
                case VirtualKey.H                   : Interact3.Pressed = state; break;
            }
        }
    }
}
