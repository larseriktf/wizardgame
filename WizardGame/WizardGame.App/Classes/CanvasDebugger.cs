using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using WizardGame.App.Classes.Entities;
using WizardGame.App.Interfaces;
using static System.Math;

namespace WizardGame.App.Classes
{
    public static class CanvasDebugger
    {
        private static readonly int xIndent = 25;
        private static readonly int yIndent = 25;
        public static Dictionary<object, string> DebugMessages { get; set; } = new Dictionary<object, string>();

        public static void DrawMessages(CanvasDrawingSession ds)
        {
            if (DebugMessages.Count() > 0)
            {
                ds.DrawText("Canvas Debugger", xIndent, yIndent, Colors.White);
                ds.DrawText("---------------", xIndent, yIndent * 2, Colors.White);

                int i = 0;
                foreach (KeyValuePair<object, string> message in DebugMessages)
                {
                    ds.DrawText(message.Value, xIndent, yIndent * (i + 3), Colors.White);
                    i++;
                }
            }
        }

        public static void Debug(object obj, string message)
        {
            if (!DebugMessages.ContainsKey(obj))
            {
                DebugMessages.Add(obj, message);
            }
            else
            {
                DebugMessages[obj] = message;
            }
        }


        public static Entity objA { get; set; } = null;
        public static Entity objB { get; set; } = null;
        private static int length = 128;
        public static void TestDrawing(CanvasDrawingSession ds)
        {
            if (objA != null && objB != null)
            {

                // Card velocity vector
                //ds.DrawLine(
                //    objA.XPos,
                //    objA.YPos,
                //    objA.XPos + ((float)Math.Cos(CardEnemy.Angle) * length),
                //    objA.YPos + ((float)Math.Sin(CardEnemy.Angle) * length),
                //    Colors.Blue);

                // Lagging vector
                //ds.DrawLine(
                //    objA.XPos,
                //    objA.YPos,
                //    objA.XPos + ((float)Math.Cos(CardEnemy.LagAngle) * length),
                //    objA.YPos + ((float)Math.Sin(CardEnemy.LagAngle) * length),
                //    Colors.Green);

                // Vector towards target
                //ds.DrawLine(
                //    objA.XPos,
                //    objA.YPos,
                //    objA.XPos + ((float)Math.Cos(CardEnemy.TargetAngle) * length),
                //    objA.YPos + ((float)Math.Sin(CardEnemy.TargetAngle) * length),
                //    Colors.Red);
            }


        }
    }
}
