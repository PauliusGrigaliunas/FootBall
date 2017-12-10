using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading;
using Emgu.CV.UI;
using System.Diagnostics;
using Emgu.CV.CvEnum;
using System.Drawing;
using System.Configuration;

namespace Football
{

    public class Video : Source
    {

        public override bool TakeASource()
        {

            if (Capture != null) { Capture.Dispose(); } 

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Video Files |*.mp4";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SaveUserSettings(ofd.FileName);
                Capture = new Emgu.CV.VideoCapture(ofd.FileName);
                return Starter();
            }
            else return false;
        }
    }
}
