﻿using System;
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
        VideoCapture _capture { get; set; }
        private Stopwatch _stopwatch = new Stopwatch();

        int _xBallPosition { get; set; }
        int _timeElapsed { get; set; }
        private int _tempX;

        public string _aText { get; set; }
        public string _bText { get; set; }

        public static bool ballGoingRight = false;

        public GoalsChecker(Stopwatch stopwatch)
        {
            this._stopwatch = stopwatch;
        }

        public string CheckForScoreA(string text)  // goal'as Team A  vartuose, A++
        {
            Console.WriteLine(_stopwatch.ToString());
            int temp;
            TimeSpan ts = _stopwatch.Elapsed;
            _timeElapsed = ts.Seconds;
            if (_timeElapsed >= 3 && VideoScreen.isATeamScored && !ballGoingRight)
            {
                temp = int.Parse(text);
                temp = temp + 1;
                text = temp.ToString();
                _stopwatch.Reset();
                VideoScreen.isATeamScored = false;
                VideoScreen.isBTeamScored = false;
            }
            return text;
        }

        public string CheckForScoreB(string text)  // goal'as Team B vartuose, B++
        {
            int temp;
            TimeSpan ts = _stopwatch.Elapsed;
            _timeElapsed = ts.Seconds;
            if (_timeElapsed >= 3 && VideoScreen.isBTeamScored && ballGoingRight)
            {
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

        //public void Direction(int x, int i, int[] xCoords)
        public void Direction(int x, int i, List<int> xCoords)
        {
            xCoords.Add(x);

            if (i >= 2)
            {
                _tempX = xCoords[i - 1] - xCoords[i - 2]; // kamuoliuko greitis X-asimi (px/frame)

                if (_tempX >= 0)
                {
                    ballGoingRight = true;    // kamuolys juda link B vartu
                }
                else
                {
                    ballGoingRight = false;   // kamuolys juda link A vartu
                }
            }
            else
            {
                // else detect more balls to work with and do ^
            }
        }
    }
}
