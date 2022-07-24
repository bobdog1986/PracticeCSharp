using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy.Design
{
    ///1797. Design Authentication Manager
    public class AuthenticationManager
    {
        private readonly int _timeToLive;
        private readonly Dictionary<string, int> dict;

        public AuthenticationManager(int timeToLive)
        {
            _timeToLive = timeToLive;
            dict = new Dictionary<string, int>();
        }

        public void Generate(string tokenId, int currentTime)
        {
            dict.Add(tokenId, currentTime + _timeToLive);
        }

        public void Renew(string tokenId, int currentTime)
        {
            if (dict.ContainsKey(tokenId) && dict[tokenId] > currentTime)
            {
                dict[tokenId] = currentTime + _timeToLive;
            }
        }

        public int CountUnexpiredTokens(int currentTime)
        {
            return dict.Where(x => x.Value > currentTime).Count();
        }
    }
}
