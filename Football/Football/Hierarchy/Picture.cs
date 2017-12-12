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
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using Emgu.CV.CvEnum;

namespace Football
{
    public class Picture
    {
        //laukai
        private Image<Bgr, byte> _imgInput;

        //properties
        public Image<Bgr, byte> GetImage => _imgInput;

        //***
        public bool TakeASource()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = @"Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _imgInput = new Image<Bgr, byte>(ofd.FileName);
                return true;
            }
            else return false;

        }
        //--------------------------------------------------
        public Image<Gray, byte> ConvertToGray()
        {
            try
            {
                return _imgInput.Convert<Gray, byte>();
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
        //--------------------------------------------------
        public Image<Gray, byte> ConvertToCanny(int x, int y)
        {
            try
            {
                Image<Gray, byte> imgCanny = new Image<Gray, byte>(_imgInput.Width, _imgInput.Height, new Gray(0));
                imgCanny = _imgInput.Canny(x, y);
                return imgCanny;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
        //-----------------------------------------------------
        public Image<Gray, float> ConvertToSobel(int x, int y, int z)
        {
            try
            {
                Image<Gray, byte> imgGray = _imgInput.Convert<Gray, byte>();
                Image<Gray, float> imgSobel = new Image<Gray, float>(_imgInput.Width, _imgInput.Height, new Gray(0));
                imgSobel = imgGray.Sobel(x, y, z);
                return imgSobel;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
        //-----------------------------------------------------
        public Image<Gray, float> ConvertToLaplase(int x)
        {
            try
            {
                Image<Gray, byte> imgGray = _imgInput.Convert<Gray, byte>();
                Image<Gray, float> imgLaplasian = new Image<Gray, float>(_imgInput.Width, _imgInput.Height, new Gray(0));
                imgLaplasian = imgGray.Laplace(3);
                return imgLaplasian;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
        //------------------------------------------------------
        public Image<Gray, byte> ColorRange(int lowBlue, int lowGreen, int lowRed, int highBlue, int highGreen, int highRed)
        {
            try
            {
                Image<Gray, Byte> imgRange = _imgInput.InRange(new Bgr(lowBlue, lowGreen, lowRed), new Bgr(highBlue, highGreen, highRed));
                return imgRange;
            }
            catch (NullReferenceException)
            {
                return null;
            }

        }
    }
}