using System;
using System.Timers;

namespace WizardGame.App.Classes.Input
{
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
