using System.IO;
using System.IO.Compression;

namespace SimpleFramework.Utils.Compress
{
    public static class CompressHelper
    {
        public static byte[] Compress(byte[] data)
        {
            using (var compressedStream = new MemoryStream())
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                zipStream.Write(data, 0, data.Length);
                return compressedStream.ToArray();
            }
        }

        public static byte[] Decompress(byte[] compressedData)
        {
            using (var ms = new MemoryStream())
            {
                using (var compressedStream = new MemoryStream(compressedData))
                using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress, false))
                {
                    gzipStream.CopyTo(ms);
                }
                return ms.ToArray();
            }
        }
    }
}
