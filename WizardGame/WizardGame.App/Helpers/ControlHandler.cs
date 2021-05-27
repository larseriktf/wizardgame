using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace WizardGame.App.Helpers
{
    public static class ControlHandler
    {
        public static void ToggleVisibility(UIElement control) =>
            control.Visibility =
            control.Visibility == Visibility.Visible ?
            Visibility.Collapsed : Visibility.Visible;
    }
}
