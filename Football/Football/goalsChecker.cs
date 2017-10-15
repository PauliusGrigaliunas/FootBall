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
        int _xBallPosition { get; set; }
        int _timeElapsed;
        VideoCapture _capture { get; set; }
        public string aText;
        public string bText;

        public Stopwatch stopwatch = new Stopwatch();

        public GoalsChecker()
        {
            stopwatch.Start();
        }


        public string CheckForScore(string text, bool isTeamScored )
        {

            Console.WriteLine(stopwatch.ToString());
            int temp;
            //stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            _timeElapsed = ts.Seconds;
            if (_timeElapsed >= 3 && isTeamScored == true)
            {
                temp = int.Parse(text);
                temp = temp + 1;
                text = temp.ToString();
                stopwatch.Reset();
                isTeamScored = false;
            }
            return text;

        }

        public bool StartStopwatch(int x, int xLow, int xHigh)
        {
            bool isTeamScored;
            //A 0 , 45 ; B 440, 500;

            if ( xLow < x && x < xHigh)
            {
                isTeamScored = true;
                stopwatch.Reset();
                stopwatch.Start();
            }
            else
            {
                isTeamScored = false;
                stopwatch.Reset();
            }
            return isTeamScored;
        }
    }
}