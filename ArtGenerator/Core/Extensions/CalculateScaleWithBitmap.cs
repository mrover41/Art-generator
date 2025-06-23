using System.Drawing;

namespace ArtGenerator.Core.Extensions {
    public static class CalculateScaleWithBitmap {
        public static float CalculateScale(this Bitmap bitmap) {
            return 1f / bitmap.Width;
        }
    }
}
