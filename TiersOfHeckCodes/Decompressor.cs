using System.IO.Compression;
using System.Text;

namespace TiersOfHeckCodes;

public class Decompressor
{
    public static string Decompress(string inp)
    {
        var text = Unzip(inp);
        text = text.Replace("!", ",\"w\":[");
        text = text.Replace("@", "],\"v\":[");
        text = text.Replace("$", "],\"e\":[");
        text = text.Replace("%", "],\"f\":[");
        text = text.Replace("&", "],\"g\":[");
        text = text.Replace("*", "],\"m\":[");
        text = text.Replace("#", "],\"n\":[");
        text = text.Replace("^", "],\"i\":");
        text = text.Replace("+", "{\"l\":");
        return text;
    }

    public static string Compress(string inp)
    {
        var text = Zip(inp);
        text = text.Replace(",\"w\":[", "!");
        text = text.Replace("],\"v\":[", "@");
        text = text.Replace("],\"e\":[", "$");
        text = text.Replace("],\"f\":[", "%");
        text = text.Replace("],\"g\":[", "&");
        text = text.Replace("],\"m\":[", "*");
        text = text.Replace("],\"n\":[", "#");
        text = text.Replace("],\"i\":", "^");
        text = text.Replace("{\"l\":", "+");
        return text;
    }
    
    public static string Zip(string str)
    {
        string result;
        using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(str)))
        {
            using (MemoryStream memoryStream2 = new MemoryStream())
            {
                using (DeflateStream deflateStream = new DeflateStream(memoryStream2, CompressionMode.Compress))
                {
                    CopyMemStream(memoryStream, deflateStream);
                }
                result = Z85.ToZ85String(memoryStream2.ToArray(), true);
            }
        }
        return result;
    }

    private static string Unzip(string toUnzip)
    {
        string @string;
        using (MemoryStream memoryStream = new MemoryStream(Z85.FromZ85String(toUnzip)))
        {
            using (MemoryStream memoryStream2 = new MemoryStream())
            {
                using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
                {
                    CopyMemStream(deflateStream, memoryStream2);
                }
                @string = Encoding.UTF8.GetString(memoryStream2.ToArray());
            }
        }
        return @string;
    }

    private static void CopyMemStream(Stream src, Stream dest)
    {
        var array = new byte[4096];
        int count;
        while ((count = src.Read(array, 0, array.Length)) != 0)
        {
            dest.Write(array, 0, count);
        }
    }
}