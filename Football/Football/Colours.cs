using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    public struct Colour
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public Ball.ColourName ColourName { get; set; }
        public Hsv Low { get; set; }
        public Hsv High { get; set; }


    }


}
