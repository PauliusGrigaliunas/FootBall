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
using static Football.ColourPalet;

namespace Football
{
    public class Ball
    {
        public struct BallPosition
        {
            public static int X { get; set; }
            public static int Y { get; set; }
            public static bool goingRight { get; set; }
        }

        //objects
        public GoalsChecker Gcheck { get; set; }

        //variables   
        public int Index { get; set; }
        public List<int> xCoordList = new List<int>();
        public Image<Bgr, byte> ImgOriginal { get; set; }
        public Image<Gray, byte> ImgFiltered { get; set; }
        //public Colour[] Colour { get; set; }
        public string PositionComment;
        delegate int Distance(int A, int B);
        delegate void Print(List<int> list, int AGATES, int BGATES, int ABdistance);
        Gates _gates = new Gates();

        public ColourPalet colourPalet = new ColourPalet();
        public ChooseColour chooseColour = new ChooseColour();
        private const int customColorIndex = 2;


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


        public void BallPositionDraw(Image<Bgr, byte> imgCircles, Image<Gray, byte> ImgGates, string ATeam, string BTeam, GoalsChecker gch, List<int> list, int counter)
        {
            Gcheck = gch; xCoordList = list; Index = counter;

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


                int AGATES = _gates.FindAGates(ImgGates); // O <--
                int BGATES = _gates.FindBGates(ImgGates); // --> O
                int ABdistance = _gates.dist(AGATES, BGATES, ImgFiltered);
                
                //print(xCoordList, AGATES, BGATES, ABdistance); // Diagnostic info

                PositionComment = getBallStatus(ABdistance, AGATES, ATeam, BTeam); // commentator logics
            }
            catch (Exception)
            {
                return;
            }
        }


        Print print = delegate (List<int> list, int AGATES, int BGATES, int ABdistance)  
        {
            IEnumerator<int> enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                int item = enumerator.Current;
                Debug.WriteLine(item);
            }
            Debug.WriteLine(AGATES + "   <--->   " + BGATES + "   dist = " + ABdistance + " ballpos: " + BallPosition.X);
        };

        private string getBallStatus(int dist, int diff, string at, string bt)
        {
            if (BallPosition.X <= (dist * 4 / 20 + diff))
            {
                return at + " Team Defenders";
            }
            else if (BallPosition.X >= (dist * 4 / 20 + diff) && BallPosition.X < (dist * 7 / 20 + diff))
            {
                return bt + " Team Attackers";
            }
            else if (BallPosition.X >= (dist * 7 / 20 + diff) && BallPosition.X < (dist * 10 / 20 + diff))
            {
                return at + " Team Middle 5";
            }
            else if (BallPosition.X >= (dist * 10 / 20 + diff) && BallPosition.X < (dist * 13 / 20 + diff))
            {
                return bt + " Team Middle 5";
            }
            else if (BallPosition.X >= (dist * 13 / 20 + diff) && BallPosition.X < (dist * 16 / 20 + diff))
            {
                return at + " Team Attackers";
            }
            else if (BallPosition.X >= (dist * 16 / 20 + diff) && BallPosition.X < (dist + diff))
            {
                return bt + " Team Defenders";
            }
            else
            {
                return "Unknown";
            }
        }

        public void setCustomColor(Hsv low, Hsv high)
        {
            chooseColour.colourPalet.Colour[customColorIndex].Low = low;
            chooseColour.colourPalet.Colour[customColorIndex].High = high;
        }


        public int GetIndex()
        {
            return Index;
        }

        public List<int> GetList()
        {
            return xCoordList;
        }

        public GoalsChecker GetGCH()
        {
            return Gcheck;
        }
    }
}