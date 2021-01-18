using iTextSharp.text;

namespace Agridea.Pdf
{
    public class Colors
    {
        public static BaseColor Black = Rgb(0, 0, 0);
        public static BaseColor White = Rgb(255, 255, 255);
        public static BaseColor LightGray = Rgb(0xf0, 0xf0, 0xf0);
        public static BaseColor Gray = Rgb(0xb0, 0xb0, 0xb0);
        public static BaseColor Ok = Rgb(0xb8, 0xfc, 0xc7);
        public static BaseColor Ko = Rgb(0xfa, 0x9d, 0x8e);
        public static BaseColor Pok = Rgb(0xfb, 0xec, 0xa2);
        public static BaseColor AutoSetNa = Rgb(0xd2, 0xe6, 0xf9);
        public static BaseColor AutoSetNc = Rgb(0xa6, 0xc4, 0xef);

        public static BaseColor Rgb(byte r, byte g, byte b)
        {
            return new BaseColor(r, g, b);
        }
    }
}