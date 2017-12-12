using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Diagnostics;
using System.Windows.Forms;
using Emgu.CV.CvEnum;
using System.Drawing;
using static Football.ColourPalet;

namespace Football
{
    public abstract class Source : ISource
    {
        public VideoCapture Capture { get; set; }
        public Image<Bgr, byte> ImgOriginal { get; set; }
        public Image<Gray, byte> ImgFiltered { get; set; }
        public static VideoScreen _home { get; set; }
        internal Stopwatch _stopwatch = new Stopwatch();
        internal System.Windows.Forms.Timer _timer;
        internal Ball _ball = new Ball();

        public Source()
        {
        }

        public virtual bool TakeASource() {
            return false;
        }

        internal bool Starter()
        {

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000 / 30;
            _timer.Tick += new EventHandler(_home.TimeTick);
            _timer.Start();
            return true;
        }

        public Image<Gray, byte> GetFilteredImage(ColourStruct colour)
        {
            Image<Gray, byte> imgSmoothed = ImgOriginal.Convert<Hsv, byte>().InRange(colour.Low, colour.High);

            var erodeImage = CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(5, 5), new Point(-1, -1));
            CvInvoke.Erode(imgSmoothed, imgSmoothed, erodeImage, new Point(-1, -1), 1, BorderType.Reflect, default(MCvScalar));
            var dilateImage = CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(6, 6), new Point(-1, -1));
            CvInvoke.Dilate(imgSmoothed, imgSmoothed, dilateImage, new Point(-1, -1), 1, BorderType.Reflect, default(MCvScalar));
            return imgSmoothed;
        }
        public Image<Gray, byte> GetFilteredImageZones(ColourStruct colour)
        {
            Image<Gray, byte> imgSmoothed = ImgOriginal.Convert<Hsv, byte>().InRange(colour.Low, colour.High);
            return imgSmoothed;
        }

        public void SaveUserSettings(String filename)
        {
            //https://msdn.microsoft.com/en-us/library/a65txexh.aspx
            Properties.Settings.Default.lastfilepath = filename;
            Properties.Settings.Default.Save();
        }

        public bool StartVideo()
        {
            if (_timer != null)
            {
                _timer.Tick += new EventHandler(_home.TimeTick);
                _timer.Start();
                return true;
            }

            else return TakeASource();
        }

        public bool StartLastUsedVideo()
        {
            if (_timer != null)
            {
                _timer.Tick += new EventHandler(_home.TimeTick);
                _timer.Start();
                return true;
            }
            else return StartLastUsedVid();
        }

        public bool StartLastUsedVid()
        {
            if (Properties.Settings.Default.lastfilepath != "Here is stored the path to the lastest user's used video file")
            {
                Capture = new Emgu.CV.VideoCapture(Properties.Settings.Default.lastfilepath);
                _timer = new System.Windows.Forms.Timer();
                _timer.Interval = 1000 / 30;
                _timer.Tick += new EventHandler(_home.TimeTick);
                _timer.Start();
                return true;
            }
            else
                return TakeASource();
        }

        public void Pause()
        {
            if (_timer != null)
            {
                _timer.Tick -= new EventHandler(_home.TimeTick);
                _timer.Stop();
            }
        }
        public bool Check()
        {
            if (_timer == null)
            {
                DialogResult dialogResult = MessageBox.Show("Would you like to reset" +
                    "points to 0 : 0?", "Adding another video", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) return true;
                else return false;
            }
            return false;
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Tick -= new EventHandler(_home.TimeTick);
                _timer.Stop();
                _timer = null;
                Capture = null;
            }
        }
    }
}
