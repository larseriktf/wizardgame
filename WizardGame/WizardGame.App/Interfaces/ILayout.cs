using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardGame.App.Interfaces
{
    interface ILayout
    {
        int[][] Layout { get; set; }
        void GenerateLayout();
    }
}
