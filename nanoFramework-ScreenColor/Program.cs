using nanoFramework.UI;
using System.Diagnostics;
using System.Threading;

namespace nanoFramework_ScreenColor
{
    public class Program
    {
        public static void Main()
        {
            Debug.WriteLine("Hello from nanoFramework!");

            var x = Display.Initialize();

            Bitmap bmp = Resource.GetBitmap(Resource.BitmapResources.rgb_bar);

            bmp.Flush(0, 0, bmp.Width, bmp.Height);


            Display.DrawText("RED RED", System.Drawing.Color.Red, 1, 31, false);

            Display.DrawText("GREEN GREEN", System.Drawing.Color.Green, 1, 31 + 30, false);

            Display.DrawText("BLUE BLUE", System.Drawing.Color.Blue, 1, 31 + 30 + 30, false);

            Debug.WriteLine("Done");

            Thread.Sleep(Timeout.Infinite);


        }
    }
}
