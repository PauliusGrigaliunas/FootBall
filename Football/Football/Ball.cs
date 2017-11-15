using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading;
using Emgu.CV.UI;
using System.Drawing;
using System.Diagnostics;

namespace Football
{
    public class Ball
    {
        /* public struct BallColor
         {
             public string Name { get; set; }
             public int LowBlue { get; set; }
             public int LowGreen { get; set; }
             public int LowRed { get; set; }
             public int HighBlue { get; set; }
             public int HighGreen { get; set; }
             public int HighRed { get; set; }

         }*/


        public struct BallPosition
        {
            public static int X { get; set; }
            public static int Y { get; set; }
            public static bool GoingRight { get; set; }
        }


        //objects
        public GoalsChecker Gcheck { get; set; }

        //variables   
        public int Index { get; set; }
        public List<int> xCoordList = new List<int>();
        public Image<Bgr, byte> ImgOriginal { get; set; }
        public Image<Gray, byte> ImgFiltered { get; set; }
        public Colour[] colour;


        public Ball() {
            BallColorQuery();
        }

        public enum ColourName { Default,  Orange }//dar pilnai neįgyvendintas

        public Colour Col(ColourName colourName) {
            switch (colourName)
            {
                case ColourName.Default:
                    return colour[0];
                case ColourName.Orange:
                    return colour[1];
                default:
                    return colour[0];
            }
        }

        private void BallColorQuery()
        {
            colour = new[] {
                new Colour
                {
                    Number = 0,
                    Name = "Default",
                    ColourName = ColourName.Default,
                    Low = new Hsv(0, 0, 0),
                    High = new Hsv(10, 10, 10),
                },

                new Colour
                {
                    Number = 1,
                    Name = "Orange",
                    ColourName = ColourName.Orange,
                    Low = new Hsv(0, 140, 150),
                    High = new Hsv(180, 255, 255),
                }
            };
        }

        internal void Detection(Video _video, ColourName coloursName = ColourName.Default, string colourName = "Default", int colourNumber = 0 )
        {
            Colour _colour;
            //! pritaikyti protingai galime Enum
            if (colourName != "Default")
                _colour = colour.First(x => x.Name == colourName);
            else if(colourNumber != 0)
                _colour = colour.First(x => x.Number == colourNumber);
            else
                _colour = colour.First(x => x.ColourName == coloursName);

            Image<Bgr, byte> imgCircles = _video.ImgOriginal.CopyBlank();     //copy parameters of original frame image

            ImgFiltered = _video.GetFilteredImage(_colour);
            ImgOriginal = _video.ImgOriginal;

            BallPositionDraw(imgCircles);
        }

        private CircleF[] GetCircles(Image<Gray, byte> imgGray)
        {
            Gray grayCircle = new Gray(12);
            Gray cannyThreshold = new Gray(26);
            double lAccumResolution = 2.0;
            double minDistanceBtwCircles = 1.0;
            int minRadius = 0;
            int maxRadius = 10;
            return imgGray.HoughCircles(grayCircle, cannyThreshold, lAccumResolution, minDistanceBtwCircles, minRadius, maxRadius)[0];
        }

        private void BallPositionDraw(Image<Bgr, byte> imgCircles)
        {
            try
            {
                foreach (CircleF circle in GetCircles(ImgFiltered))
                {
                    BallPosition.X = (int)circle.Center.X;
                    Gcheck.StartStopwatch(BallPosition.X, ImgOriginal.Width);
                    Gcheck.Direction(BallPosition.X, Index, xCoordList); Index++;
                    imgCircles.Draw(circle, new Bgr(Color.Red), 3);
                }

                if (Index >= 5)
                {
                    xCoordList = xCoordList.Skip(Index - 4).ToList();
                    Index = 4;
                }

                Display(xCoordList);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void Display(List<int> list)
        {
            IEnumerator<int> enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                int item = enumerator.Current;
                Debug.WriteLine(item);
            }
        }
    }
}
