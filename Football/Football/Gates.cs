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
    public class Gates
    {
         ISource _video;

        public ChooseColour ChooseColour = new ChooseColour();

        public delegate int Distance(int A, int B, Image<Gray, byte> ImgOriginal);

        public Distance Dist = delegate (int AG, int BG, Image<Gray, byte> img)
        {
            int result = AG - BG;
            if (result <= 0)
                return img.Width * 5 / 6;
            else
                return result;
        };

        public int FindAGates(Image<Gray, byte> ImgGates)
        {
            int i, j, red = 0, green = 0, blue = 0, counter = 0, X = 0;
            Color clr;
            int width = ImgGates.Width * 1 / 4;                      // 1/4  | 3/4
            int sheight = ImgGates.Height * 2 / 5;                   // 2/5  | 2/5
            int height = ImgGates.Height * 4 / 5;                    // 4/5  | 4/5
            Bitmap bmp = new Bitmap(ImgGates.Bitmap);

            for (i = 0; i < width; ++i)
            {
                for (j = sheight; j < height; j++)
                {
                    clr = bmp.GetPixel(i, j);
                    red = clr.R;
                    green = clr.G;
                    blue = clr.B;

                    if (red >= 245 && green >= 245 && blue >= 245)
                        counter++;
                    else
                        counter = 0;

                    if (counter >= 5)
                    {
                        X = i - 1;
                        return X;
                    }
                }
            }
            return X;
        }

        public int FindBGates(Image<Gray, byte> imgGates)
        {
            int i, j, red = 0, green = 0, blue = 0, counter = 0, X = 0;
            Color clr;
            int width = imgGates.Width * 3 / 4;
            int sheight = imgGates.Height * 2 / 5;
            int height = imgGates.Height * 4 / 5;
            Bitmap bmp = new Bitmap(imgGates.Bitmap);

            for (i = width; i < imgGates.Width; ++i)
            {
                for (j = sheight; j < height; j++)
                {
                    clr = bmp.GetPixel(i, j);
                    red = clr.R;
                    green = clr.G;
                    blue = clr.B;

                    if (red >= 245 && green >= 245 && blue >= 245)
                        counter++;
                    else
                        counter = 0;

                    if (counter >= 5)
                    {
                        X = i - 1;
                        return X;
                    }
                }
            }
            return X;
        }

    }
}