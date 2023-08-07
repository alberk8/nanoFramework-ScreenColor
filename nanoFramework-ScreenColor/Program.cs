using nanoFramework.UI;
using nanoFramework_ScreenColor.M5Core2;
using System.Diagnostics;
using System.Threading;

namespace nanoFramework_ScreenColor
{
    public class Program
    {
        public static void Main()
        {
            Debug.WriteLine("Hello from nanoFramework!");
            Thread.Sleep(1_000);
            
            // Set for M5Core2
            var deviceType = DeviceType.M5Core2;
            
            //Set for ST7789V
            //var deviceType = DeviceType.ST7789V;
            
            switch (deviceType)
            {
                case DeviceType.M5Core2:
                    // This is needed for M5Core2
                    MCore2.PowerOn();
                    Display.Initialize(DeviceType.M5Core2);
                    break;
                 
                case DeviceType.ST7789V:
                    Display.Initialize(DeviceType.ST7789V);
                    break;
                    
            }

            Debug.WriteLine("** Clear Screen");
            DisplayControl.Clear();

             // BMP Image 
            Bitmap bmp = Resource.GetBitmap(Resource.BitmapResources.rgb_bar_bmp);
            bmp.Flush(0, 0, bmp.Width, bmp.Height);

            //Thread.Sleep(2_000);
            bmp.Dispose();

            // Jpeg/Jpg Image
            bmp = Resource.GetBitmap(Resource.BitmapResources.rgb_bar_jpg);
            bmp.Flush(0, 0, bmp.Width, bmp.Height, 0, 35);

            bmp.Dispose();

          
            // Gif Image
            bmp = Resource.GetBitmap(Resource.BitmapResources.rgb_bar_gif);
            bmp.Flush(0, 0, bmp.Width, bmp.Height, 0, 70);

            bmp.Dispose();

             //Draw Text Red
            Display.DrawText("RED ", System.Drawing.Color.Red, 0, 105, false);

            //Draw Text Green
            Display.DrawText("GREEN ", System.Drawing.Color.Green, 60, 105, false);

            //Draw Text Red
            Display.DrawText("BLUE ", System.Drawing.Color.Blue, 125, 105, false);

            //Draw Rectangle Red
            Bitmap bmp1 = new Bitmap(50, 50);
            bmp1.DrawRectangle(0, 0, 50, 50, 5, System.Drawing.Color.Red);
            bmp1.Flush(0, 0, 50, 50, 0, 135);

            //Draw Rectangle Green
            bmp1.DrawRectangle(0, 0, 50, 50, 5, System.Drawing.Color.Green);
            bmp1.Flush(0, 0, 50, 50, 60, 135);

            //Draw Rectangle Blue
            bmp1.DrawRectangle(0, 0, 50, 50, 5, System.Drawing.Color.Blue);
            bmp1.Flush(0, 0, 50, 50, 120, 135);

            //Draw Rectangle Blue
            bmp1.DrawRectangle(0, 0, 50, 50, 5, System.Drawing.Color.CornflowerBlue);
            bmp1.Flush(0, 0, 50, 50, 180, 135);

            Thread.Sleep(Timeout.Infinite);


        }
    }
}
