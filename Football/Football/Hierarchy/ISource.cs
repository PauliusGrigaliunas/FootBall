using Emgu.CV;
using Emgu.CV.Structure;
using static Football.ColourPalet;

namespace Football
{
    public interface ISource
    {
        VideoCapture Capture { get; set; }
        Image<Bgr, byte> ImgOriginal { get; set; }
        Image<Gray, byte> ImgFiltered { get; set; }
        bool TakeASource();
        bool StartVideo();
        void Pause();
        void Stop();        
        bool StartLastUsedVideo();
        bool Check();
        Image<Gray, byte> GetFilteredImageZones(ColourStruct clr);
        Image<Gray, byte> GetFilteredImage(ColourStruct colour);

    }
}
