using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design.Codec_535
{
    ///535. Encode and Decode TinyURL
    public class Codec
    {
        private readonly Dictionary<string, string> dict = new Dictionary<string, string>();
        private readonly string str = @"http://tinyurl.com/";

        // Encodes a URL to a shortened URL
        public string encode(string longUrl)
        {
            string guidKey = string.Empty;
            while (string.IsNullOrEmpty(guidKey))
            {
                string currKey = Guid.NewGuid().ToString().Substring(0, 6);
                if (!dict.ContainsKey(currKey))//in case of duplicate guid
                    guidKey = currKey;
            }
            dict.Add(guidKey, longUrl);
            return str + guidKey;
        }

        // Decodes a shortened URL to its original URL.
        public string decode(string shortUrl)
        {
            var key = shortUrl.Substring(shortUrl.Length - 6);
            return dict[key];
        }
    }

    // Your Codec object will be instantiated and called as such:
    // Codec codec = new Codec();
    // codec.decode(codec.encode(url));
}
