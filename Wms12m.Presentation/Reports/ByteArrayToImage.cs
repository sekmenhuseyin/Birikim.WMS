namespace Wms12m
{
    public static class ByteArrayToImage
    {
        public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.Image returnImage = null;
            try
            {
                var ms = new System.IO.MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                returnImage = System.Drawing.Image.FromStream(ms, true);  //Exception occurs here
            }
            catch { }
            return returnImage;
        }
    }
}