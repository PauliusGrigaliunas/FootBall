using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    public class ColourPalet
    {
        public struct ColourStruct
        {
            public int Number { get; set; }
            public string Name { get; set; }
            public Hsv Low { get; set; }
            public Hsv High { get; set; }
        }
        public ColourStruct[] Colour { get; set; }

        public ColourPalet() {
            ColorQuery();
        } 

        private void ColorQuery()
        {
            //colour = lazy.Value;
            Colour = new[] {
                new ColourStruct
                {
                    Number = 1,
                    Name = "Orange",
                    Low = new Hsv(0, 140, 150),
                    High = new Hsv(180, 255, 255),
                },

                new ColourStruct
                {
                    Number = 2,
                    Name = "Yellow",
                    Low = new Hsv(0, 93, 0),
                    High = new Hsv(255, 255, 89),
                },

                new ColourStruct
                {
                    Number = 3,
                    Name = "Custom",
                    Low = new Hsv(0, 140, 150),
                    High = new Hsv(180, 255, 255),
                },

                new ColourStruct
                {
                    Number = 4,
                    Name = "BlackDarkGates",
                    Low = new Hsv(0, 0, 0),
                    High = new Hsv(255, 255, 10),
                }
            };
        }
    }
}
