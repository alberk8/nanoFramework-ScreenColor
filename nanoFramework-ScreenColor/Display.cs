using nanoFramework.Hardware.Esp32;
using nanoFramework.UI;
using System.Drawing;

namespace nanoFramework_ScreenColor
{
    public enum DeviceType
    {
        ST7789V,
        M5Core2
    }


    internal static class Display
    {
 

       private static int ChipSelect=0;
       private static int DataCommand=0;
       private static int Reset=-1;

       private const int BackLightPin = -1;
       private static Bitmap drawTextBmp;
       private static ushort _screenWidth;
       private static ushort _screenHeight;
       private static Font _font = Resource.GetFont(Resource.FontResources.franklin_20);
       private static object _object = new object();

       public static uint Initialize(DeviceType deviceType)
       {
          
           if(deviceType == DeviceType.ST7789V)
           {
               // Module ST7789 Display
               _screenWidth = 240;
               _screenHeight = 320;

               ChipSelect = 5;
               DataCommand = 26;
               Reset = 4;
              
               Configuration.SetPinFunction(19, DeviceFunction.SPI1_MOSI);
               Configuration.SetPinFunction(23, DeviceFunction.SPI1_MISO);

               Configuration.SetPinFunction(18, DeviceFunction.SPI1_CLOCK);
           }
           else
           {
               //M5Core2
               _screenWidth = 320;
               _screenHeight = 240;

               ChipSelect = 5;
               DataCommand = 15;
               Reset = -1;



               Configuration.SetPinFunction(23, DeviceFunction.SPI1_MOSI);
               Configuration.SetPinFunction(19, DeviceFunction.SPI1_MISO);
               Configuration.SetPinFunction(18, DeviceFunction.SPI1_CLOCK);

           }





           var displaySpiConfig = new SpiConfiguration(
              1,
              ChipSelect,
              DataCommand,
              Reset,
              BackLightPin);

           var screenConfig = new ScreenConfiguration(
               0,
               0,
               _screenWidth,
               _screenHeight);


           var ret = DisplayControl.Initialize(
               displaySpiConfig,
               screenConfig,
               (uint)(320 * 240 * 4) * 3);

           if(deviceType == DeviceType.ST7789V )
           {
               DisplayControl.ChangeOrientation(DisplayOrientation.Landscape);
           }

           DisplayControl.Clear();

           drawTextBmp = new Bitmap(ScreenWidth, 40);



          return ret;
       }

       public static ushort ScreenWidth
       {
           get
           {
               if (DisplayControl.Orientation == DisplayOrientation.Portrait || DisplayControl.Orientation == DisplayOrientation.Portrait180)
               {
                   return _screenWidth;
               }
               else
               {
                   return _screenHeight;
               }
           }

       } // => _screenWidth;

       public static ushort ScreenHeight
       {
           get
           {
               if (DisplayControl.Orientation == DisplayOrientation.Portrait || DisplayControl.Orientation == DisplayOrientation.Portrait180)
               {
                   return _screenHeight;
               }
               else
               {
                   return _screenWidth;
               }
           }
       }


       public static void DrawText(string text, Color color, int x, int y, bool center = true)
       {

           int textWidth;
           int textHeight;

           _font.ComputeExtent(text, out textWidth, out textHeight);
           lock (_object)
           {
               //Debug.WriteLine(color.ToString());
               //Debug.WriteLine("ARGB: "+ (uint)color.ToArgb());

               drawTextBmp.Clear();
               //drawTextBmp.DrawText(text, _font, color, (drawTextBmp.Width - textWidth) / 2, 0);
               drawTextBmp.DrawText(text, _font, color, 0, 0);
               if (center)
               {
                   drawTextBmp.Flush(0, 0, drawTextBmp.Width, drawTextBmp.Height, 0, (drawTextBmp.Height - textHeight) / 2);
               }
               else
               {
                   drawTextBmp.Flush(0, 0, drawTextBmp.Width, drawTextBmp.Height, x, y);
               }

           }

       }

    }
}
