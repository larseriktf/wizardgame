using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace WizardGame.App.Classes.Input
{

    public static class KeyBoard
    {
        public static Key NavContinue { get; } = new Key();
        public static Key NavPause { get; } = new Key();
        public static Key NavBack { get; } = new Key();

        public static Key MoveUp { get; } = new Key();
        public static Key MoveRight { get; } = new Key();
        public static Key MoveDown { get; } = new Key();
        public static Key MoveLeft { get; } = new Key();

        public static Key Action1 { get; } = new Key();
        public static Key Action2 { get; } = new Key();
        public static Key Action3 { get; } = new Key();
        public static Key Action4 { get; } = new Key();

        // Testing purposes
        public static Key Interact1 { get; } = new Key();
        public static Key Interact2 { get; } = new Key();
        public static Key Interact3 { get; } = new Key();


        public static void UpdateKeys()
        {
            // Navigation
            NavContinue.Pressed = AssignKey(VirtualKey.Enter, CoreVirtualKeyStates.Down);
            NavPause.Pressed = AssignKey(VirtualKey.Escape, CoreVirtualKeyStates.Down);
            NavBack.Pressed = AssignKey(VirtualKey.Back, CoreVirtualKeyStates.Down);

            // Movement
            MoveUp.Pressed = AssignKey(VirtualKey.W, CoreVirtualKeyStates.Down);
            MoveLeft.Pressed = AssignKey(VirtualKey.A, CoreVirtualKeyStates.Down);
            MoveDown.Pressed = AssignKey(VirtualKey.S, CoreVirtualKeyStates.Down);
            MoveRight.Pressed = AssignKey(VirtualKey.D, CoreVirtualKeyStates.Down);

            // Spellcasting
            Action1.Pressed = AssignKey(VirtualKey.Up, CoreVirtualKeyStates.Down);
            Action2.Pressed = AssignKey(VirtualKey.Right, CoreVirtualKeyStates.Down);
            Action3.Pressed = AssignKey(VirtualKey.Down, CoreVirtualKeyStates.Down);
            Action4.Pressed = AssignKey(VirtualKey.Left, CoreVirtualKeyStates.Down);

            // Debugging
            Interact1.Pressed = AssignKey(VirtualKey.F, CoreVirtualKeyStates.Down);
            Interact2.Pressed = AssignKey(VirtualKey.G, CoreVirtualKeyStates.Down);
            Interact3.Pressed = AssignKey(VirtualKey.H, CoreVirtualKeyStates.Down);
        }

        private static bool AssignKey(VirtualKey key, CoreVirtualKeyStates keyState)
        {
            return Window.Current.CoreWindow.GetKeyState(key).HasFlag(keyState);
        }
    }
}
