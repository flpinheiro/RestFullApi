using System.Text;

namespace RestFull.Domain.Core.Helpers
{
    internal static class StreamHelper
    {

        public static Stream GetStreamWithGetBytes(string sampleString, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;

            var byteArray = encoding.GetBytes(sampleString);
            var memoryStream = new MemoryStream(byteArray);

            return memoryStream;
        }

        public static Stream GetStreamWithStreamWriter(string sampleString, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;

            var stream = new MemoryStream(encoding.GetByteCount(sampleString));
            using var writer = new StreamWriter(stream, encoding);
            writer.Write(sampleString);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}