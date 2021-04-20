using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace WizardGame.App.Classes
{

    public static class KeyBoard
    {
        public static bool KeyLeft { get; set; }
        public static bool KeyRight { get; set; }
        public static bool KeyUp { get; set; }
        public static bool KeyDown { get; set; }
        public static bool KeyIncrementVector { get; set; }
        public static bool KeyDecrementVector { get; set; }
        public static Windows.Foundation.Point PointerPosition { get; set; }
        public static bool MouseOnePressed { get; set; }
        public static bool ToggleTarget { get; set; }

        public static bool ArrowLeft { get; set; }
        public static bool ArrowRight { get; set; }
        public static bool ArrowUp { get; set; }
        public static bool ArrowDown { get; set; }


        

        public static void UpdateKeys()
        {
            // Movement Keys
            KeyLeft = Window.Current.CoreWindow.GetKeyState(VirtualKey.A).HasFlag(CoreVirtualKeyStates.Down);
            KeyRight = Window.Current.CoreWindow.GetKeyState(VirtualKey.D).HasFlag(CoreVirtualKeyStates.Down);
            KeyUp = Window.Current.CoreWindow.GetKeyState(VirtualKey.W).HasFlag(CoreVirtualKeyStates.Down);
            KeyDown = Window.Current.CoreWindow.GetKeyState(VirtualKey.S).HasFlag(CoreVirtualKeyStates.Down);

            // Arrow Keys
            ArrowLeft = Window.Current.CoreWindow.GetKeyState(VirtualKey.Left).HasFlag(CoreVirtualKeyStates.Down);
            ArrowRight = Window.Current.CoreWindow.GetKeyState(VirtualKey.Right).HasFlag(CoreVirtualKeyStates.Down);
            ArrowUp = Window.Current.CoreWindow.GetKeyState(VirtualKey.Up).HasFlag(CoreVirtualKeyStates.Down);
            ArrowDown = Window.Current.CoreWindow.GetKeyState(VirtualKey.Down).HasFlag(CoreVirtualKeyStates.Down);

            // Other Keys
            KeyIncrementVector = Window.Current.CoreWindow.GetKeyState(VirtualKey.P).HasFlag(CoreVirtualKeyStates.Down);
            KeyDecrementVector = Window.Current.CoreWindow.GetKeyState(VirtualKey.O).HasFlag(CoreVirtualKeyStates.Down);

            ToggleTarget = Window.Current.CoreWindow.GetKeyState(VirtualKey.K).HasFlag(CoreVirtualKeyStates.Locked);

            // Mouse
            PointerPosition = Window.Current.CoreWindow.PointerPosition;

        }

        public static bool state = false;

        public static bool CheckPressedOnce(bool pressed)
        {
            if (pressed != state)
            {
                state = pressed;
                if (state == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
