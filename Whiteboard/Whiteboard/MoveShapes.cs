using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Whiteboard
{
    class MoveShapes
    {
        public class Line
        {
            public Point startPoint { get; set; }
            public Point endPoint { get; set; }
            public int width { get; set; }
            public Color color { get; set; }
        }

        public class LineControl : Panel
        {

        }
    }
}
