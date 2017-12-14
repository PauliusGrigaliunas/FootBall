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
using Emgu.CV.CvEnum;
using Emgu.CV.Cuda;
using System.Drawing;
using System.Diagnostics;

namespace Football
{
    public class GoalsChecker
    {
        Commentator com = new Commentator();
        private Stopwatch _stopwatch = new Stopwatch();

        int TimeElapsed { get; set; }
        private int _tempX;

        public string AText { get; set; }
        public string BText { get; set; }

        public GoalsChecker()
        {
           // _stopwatch = stopwatch;
        }

        public string CheckForScoreA(string text) 
        {
            //Console.WriteLine(_stopwatch.ToString());
            int temp;
            TimeSpan ts = _stopwatch.Elapsed;
            TimeElapsed = ts.Seconds;
            if (TimeElapsed >= 2 && VideoScreen.isATeamScored && !Ball.BallPosition.GoingRight)
            {
                com.StopAllTracks();
                com.PlayRandomSound(0, 9);
                temp = int.Parse(text);
                temp = temp + 1;
                text = temp.ToString();
                _stopwatch.Reset();
                VideoScreen.isATeamScored = false;
                VideoScreen.isBTeamScored = false;
            }
            return text;
        }

        public string CheckForScoreB(string text) 
        {
            int temp;
            TimeSpan ts = _stopwatch.Elapsed;
            TimeElapsed = ts.Seconds;
            if (TimeElapsed >= 2 && VideoScreen.isBTeamScored && Ball.BallPosition.GoingRight)
            {
                com.StopAllTracks();
                com.PlayRandomSound(0, 8);
                temp = int.Parse(text);
                temp = temp + 1;
                text = temp.ToString();
                _stopwatch.Reset();
                VideoScreen.isATeamScored = false;
                VideoScreen.isBTeamScored = false;
            }
            return text;
        }

        public void StartStopwatch(int x, int width)
        {
            if (width * 5 / 8 < x && x < width)
            {
                VideoScreen.isATeamScored = false;
                VideoScreen.isBTeamScored = true;
                _stopwatch.Reset();
                _stopwatch.Start();
            }
            else if (0 < x && x < width * 3 / 8)
            {
                VideoScreen.isBTeamScored = false;
                VideoScreen.isATeamScored = true;
                _stopwatch.Reset();
                _stopwatch.Start();
            }
            else
            {
                VideoScreen.isBTeamScored = false;
                VideoScreen.isATeamScored = false;
                _stopwatch.Reset();
            }
        }

        public void Direction(int x, int i, List<int> xCoords)
        {
            xCoords.Add(x);

            if (i >= 2)
            {
                _tempX = xCoords[i - 1] - xCoords[i - 2]; 

                if (_tempX >= 0)
                {
                    Ball.BallPosition.GoingRight = true;  // o -> B
                }
                else
                {
                    Ball.BallPosition.GoingRight = false; // o -> A
                }
            }
        }

        public void SetStopwatch(bool enabled)
        {
            if (enabled)
                _stopwatch.Start();
            else
                _stopwatch.Stop();
        }
    }
}
