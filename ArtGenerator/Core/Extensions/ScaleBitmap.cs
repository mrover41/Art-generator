using System.Drawing;

namespace ArtGenerator.Core.Extensions {
    public static class ScaleBitmap {
        public static Bitmap Scale(this Bitmap bitmap, int scale) { 
            return new Bitmap(bitmap, new Size(bitmap.Width / scale, bitmap.Height / scale));
        }
    }
}
