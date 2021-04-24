using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace WizardGame.App.Classes.Input
{

    public static class KeyBoard
    {
        public static Key MoveUp { get; set; } = new Key();
        public static Key MoveRight { get; set; } = new Key();
        public static Key MoveDown { get; set; } = new Key();
        public static Key MoveLeft { get; set; } = new Key();
        
        
        
        public static Key Action1 { get; set; } = new Key();
        public static Key Action2 { get; set; } = new Key();
        public static Key Action3 { get; set; } = new Key();
        public static Key Action4 { get; set; } = new Key();

        public static Windows.Foundation.Point PointerPosition { get; set; }



        public static void UpdateKeys()
        {
            // Movement Keys
            MoveUp.Pressed = AssignKey(VirtualKey.W, CoreVirtualKeyStates.Down);
            MoveLeft.Pressed = AssignKey(VirtualKey.A, CoreVirtualKeyStates.Down);
            MoveDown.Pressed = AssignKey(VirtualKey.S, CoreVirtualKeyStates.Down);
            MoveRight.Pressed = AssignKey(VirtualKey.D, CoreVirtualKeyStates.Down);

            // Arrow Keys
            Action1.Pressed = AssignKey(VirtualKey.Up, CoreVirtualKeyStates.Down);
            Action2.Pressed = AssignKey(VirtualKey.Right, CoreVirtualKeyStates.Down);
            Action3.Pressed = AssignKey(VirtualKey.Down, CoreVirtualKeyStates.Down);
            Action4.Pressed = AssignKey(VirtualKey.Left, CoreVirtualKeyStates.Down);

            // Mouse
            PointerPosition = Window.Current.CoreWindow.PointerPosition;
        }

        private static bool AssignKey(VirtualKey key, CoreVirtualKeyStates keyState)
        {
            return Window.Current.CoreWindow.GetKeyState(key).HasFlag(keyState);
        }
    }
}
