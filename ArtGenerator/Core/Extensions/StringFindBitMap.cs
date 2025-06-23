using System.Drawing;
using System.IO;

namespace ArtGenerator.Core.Extensions {
    public static class StringFindBitMap {
        public static Bitmap FindBitmap(this string path) {
            if (!File.Exists(path)) return null;
            return new Bitmap(path);
        }
    }
}
