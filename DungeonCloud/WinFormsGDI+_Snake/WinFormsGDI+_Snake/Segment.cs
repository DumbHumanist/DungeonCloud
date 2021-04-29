using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace WinFormsGDI__Snake
{
    class Segment
    {
        public int x;
        public int y;
        public Segment(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
