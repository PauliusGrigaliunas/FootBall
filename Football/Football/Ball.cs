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

namespace Football
{
    public class Ball
    {

        public  struct BallPosition
        {
            public static int X { get; set; }
            public static int Y { get; set; }

        }
        //objects
        public GoalsChecker Gcheck { get; set; }

        //variables    
        public int Index { get; set; }
        public List<int> xCoordList = new List<int>();   // "List<T> is a generic collection"
        private int _ix2, _z;

        public Image<Bgr, byte> ImgOriginal { get; set; }
        public Image<Gray, byte> ImgFiltered { get; set; }

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

        public void BallPositionDraw(Image<Bgr, byte> imgCircles)
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

                xCoordList = xCoordList.Skip(Index - 4).ToList();
                // ar čia tas pats?

                if (Index >= 5)   // sarase saugomos paskutines 4 pozicijos, kad taupyt RAM
                {
                    _ix2 = Index - 4;
                    for (_z = 0; _z < _ix2; _z++)
                    {
                        xCoordList.RemoveAt(0);
                    }
                    Index -= _ix2;
                }

                Display(xCoordList);
            }
            catch (Exception)
            {      
                return;
            }
            

            /* foreach (var coord in xCoordList)   // iterating through generic list   // paprastas foreach
             {
                 Console.WriteLine(coord);       // output ball's last 4 positions on the X axis
             }*/
            //Display(xCoordList);
  
            
        }
        /*
        private void Display(IEnumerable<int> argument) // https://www.dotnetperls.com/ienumerable 2nd example // ienumerable
        {
            foreach (int value in argument)
            {
                Console.WriteLine(value);
            }
            argument.GetEnumerator();
        }*/

        private void Display(List<int> list)          //ienumerator
        {
            IEnumerator<int> enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                int item = enumerator.Current;
                Console.WriteLine(item);
            }
        }
    }
}
