using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Football
{
    public class Camera : Source
    {

        public override bool TakeASource()
        {
            Capture?.Dispose();
            Capture = new Emgu.CV.VideoCapture(0);
            return Starter();
        }

    }
}
