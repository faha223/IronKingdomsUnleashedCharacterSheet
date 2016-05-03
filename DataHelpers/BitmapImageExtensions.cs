using System.IO;
using System.Windows.Media.Imaging;

namespace IronKingdomsUnleashedCharacterSheet.DataHelpers
{
    public static class BitmapImageExtensions
    {
        public static BitmapImage GetBitmapImage(this byte[] data)
        {
            if (data == null)
                return null;
            using (var ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                ms.Seek(0, SeekOrigin.Begin);
                var img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = ms;
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                img.Freeze();
                return img;
            }
        }

        public static byte[] GetBytes(this BitmapImage img)
        {
            if (img == null)
                return null;
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(img));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }
    }
}
