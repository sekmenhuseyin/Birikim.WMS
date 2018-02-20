using System.Drawing;

namespace Wms12m
{
    public static class ByteArrayToImage
    {
        public static Image ToImage(byte[] byteArrayIn)
        {
            Image returnImage = null;
            try
            {
                var ms = new System.IO.MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                returnImage = Image.FromStream(ms, true);  //Exception occurs here
            }
            catch
            {
                // ignored
            }

            return returnImage;
        }
    }
}