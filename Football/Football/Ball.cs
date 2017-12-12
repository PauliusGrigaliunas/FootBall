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
            public static bool GoingRight { get; set; }
        }

        //objects
        public GoalsChecker Gcheck { get; set; }
        public int Index { get; set; }
        public List<int> XCoordList = new List<int>();

        public Image<Bgr, byte> ImgOriginal { get; set; }
        public Image<Gray, byte> ImgFiltered { get; set; }
        public string PositionComment;
        delegate void Print(List<int> list, int AGATES, int BGATES, int ABdistance);
        Gates _gates = new Gates();
        private Stopwatch _stopwatch = new Stopwatch();
        public bool AScored = false;
        public bool Bscored = false;

        public ChooseColour ChooseColour = new ChooseColour();
        private const int CustomColorIndex = 2;
        private const int GatesColorIndex = 3;


        private CircleF[] GetCircles(Image<Gray, byte> imgGray) //šitas testavimui buvo naudojamas
        {
            Gray grayCircle = new Gray(12);
            Gray cannyThreshold = new Gray(26);
            double lAccumResolution = 2.0;
            double minDistanceBtwCircles = 1.0;
            int minRadius = 0;
            int maxRadius = 10;
            return imgGray.HoughCircles(grayCircle, cannyThreshold, lAccumResolution, minDistanceBtwCircles, minRadius, maxRadius)[0];
        }


        public void BallPositionDraw(ISource video, string aTeam, string bTeam, int SelectedColorIndex)
        {
            Gcheck = new GoalsChecker(_stopwatch);
            ColourStruct colour = ChooseColour.Controler(SelectedColorIndex);

            ImgFiltered = video.GetFilteredImage(colour);
            ImgOriginal = video.ImgOriginal; 

            Image<Bgr, byte> imgCircles = video.ImgOriginal.CopyBlank();

            ColourStruct clr = _gates.ChooseColour.Controler(GatesColorIndex);
            Image<Gray, byte> imgGates = video.GetFilteredImageZones(clr);

            foreach (CircleF circle in GetCircles(ImgFiltered))
            {
                BallPosition.X = (int)circle.Center.X;
                Gcheck.StartStopwatch(BallPosition.X, ImgOriginal.Width);
                Gcheck.Direction(BallPosition.X, Index, XCoordList); Index++;
                imgCircles.Draw(circle, new Bgr(Color.Red), 3); //čia piešimui
            }


            if (Index >= 5)
            {
                XCoordList = XCoordList.Skip(Index - 4).ToList();
                Index = 4;
            }


            int AGATES = _gates.FindAGates(imgGates); // O <--
            int BGATES = _gates.FindBGates(imgGates); // --> O
            int ABdistance = _gates.Dist(AGATES, BGATES, ImgFiltered);

            PositionComment = GetBallStatus(ABdistance, AGATES, aTeam, bTeam); // commentator logics

        }


        private string GetBallStatus(int dist, int diff, string at, string bt)
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

        public void SetCustomColor(Hsv low, Hsv high)
        {
            ChooseColour.colourPalet.Colour[CustomColorIndex].Low = low;
            ChooseColour.colourPalet.Colour[CustomColorIndex].High = high;
        }
    }
}