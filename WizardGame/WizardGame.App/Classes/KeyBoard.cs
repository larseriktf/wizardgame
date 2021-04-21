using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace WizardGame.App.Classes
{

    public static class KeyBoard
    {
        public static Key KeyLeft { get; set; } = new Key();
        public static Key KeyRight { get; set; } = new Key();
        public static Key KeyUp { get; set; } = new Key();
        public static Key KeyDown { get; set; } = new Key();
        public static Key KeyIncrementVector { get; set; } = new Key();
        public static Key KeyDecrementVector { get; set; } = new Key();
        public static Windows.Foundation.Point PointerPosition { get; set; }
        public static Key MouseOnePressed { get; set; } = new Key();
        public static Key ToggleTarget { get; set; } = new Key();
        public static Key ArrowLeft { get; set; } = new Key();
        public static Key ArrowRight { get; set; } = new Key();
        public static Key ArrowUp { get; set; } = new Key();
        public static Key ArrowDown { get; set; } = new Key();


        

        public static void UpdateKeys()
        {
            // Movement Keys
            KeyLeft.Pressed = Window.Current.CoreWindow.GetKeyState(VirtualKey.A).HasFlag(CoreVirtualKeyStates.Down);
            KeyRight.Pressed = Window.Current.CoreWindow.GetKeyState(VirtualKey.D).HasFlag(CoreVirtualKeyStates.Down);
            KeyUp.Pressed = Window.Current.CoreWindow.GetKeyState(VirtualKey.W).HasFlag(CoreVirtualKeyStates.Down);
            KeyDown.Pressed = Window.Current.CoreWindow.GetKeyState(VirtualKey.S).HasFlag(CoreVirtualKeyStates.Down);

            // Arrow Keys
            ArrowLeft.Pressed = Window.Current.CoreWindow.GetKeyState(VirtualKey.Left).HasFlag(CoreVirtualKeyStates.Down);
            ArrowRight.Pressed = Window.Current.CoreWindow.GetKeyState(VirtualKey.Right).HasFlag(CoreVirtualKeyStates.Down);
            ArrowUp.Pressed = Window.Current.CoreWindow.GetKeyState(VirtualKey.Up).HasFlag(CoreVirtualKeyStates.Down);
            ArrowDown.Pressed = Window.Current.CoreWindow.GetKeyState(VirtualKey.Down).HasFlag(CoreVirtualKeyStates.Down);

            // Other Keys
            KeyIncrementVector.Pressed = Window.Current.CoreWindow.GetKeyState(VirtualKey.P).HasFlag(CoreVirtualKeyStates.Down);
            KeyDecrementVector.Pressed = Window.Current.CoreWindow.GetKeyState(VirtualKey.O).HasFlag(CoreVirtualKeyStates.Down);

            ToggleTarget.Pressed = Window.Current.CoreWindow.GetKeyState(VirtualKey.K).HasFlag(CoreVirtualKeyStates.Locked);

            // Mouse
            PointerPosition = Window.Current.CoreWindow.PointerPosition;

        }

        public class Key
        {
            private bool pressed = false;
            public bool Pressed
            {
                get
                {
                    return pressed;
                }
                set
                {   // Key has updated
                    if (pressed != value)
                    {   // If value has changed
                        tapped = value;
                    }
                    else
                    {
                        tapped = false;
                    }

                    pressed = value;
                }
            }
            private bool tapped = false;
            public bool Tapped
            {
                get
                {
                    return tapped;
                }
            }

            // Ensure Tapped variables
            private readonly Timer delayTimer;
            private bool isReady = true;

            public Key()
            {
                delayTimer = new Timer();
                delayTimer.Elapsed += delegate (object source, ElapsedEventArgs e)
                {   // Every tick update "delayed" boolean
                    isReady = true;
                };
                delayTimer.Start();
            }

            public void ResetDelay()
            {
                isReady = false;
                delayTimer.Interval = 20;
            }

            public void EnsureTapped(Action method)
            {
                if (isReady)
                {
                    if (tapped)
                    {
                        method();
                        ResetDelay();
                    }
                }
            }
        }
    }
}
