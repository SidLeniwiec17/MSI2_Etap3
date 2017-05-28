using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MSI_Etap3.ReportHelper
{
    public class ConfusionMatrix
    {
        public Point [][] Matrix { get; set; }
        public int Classes { get; set; }

        public ConfusionMatrix(int classes)
        {
            Classes = classes;
            Matrix = new Point[Classes][];
            for(int i = 0; i < Classes; i++)
            {
                Matrix[i] = new Point[Classes];
            }
        }
    }
}
