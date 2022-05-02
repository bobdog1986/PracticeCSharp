using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///1472. Design Browser History
    public class BrowserHistory
    {
        public class UrlItem
        {
            public UrlItem(string url, UrlItem prev = null, UrlItem next = null)
            {
                this.url = url;
                this.prev = prev;
                this.next = next;
            }
            public string url;
            public UrlItem next;
            public UrlItem prev;
        }

        private UrlItem curr;

        public BrowserHistory(string homepage)
        {
            curr=new UrlItem(homepage);
        }

        public void Visit(string url)
        {
            curr.next = new UrlItem(url, curr, null);
            curr = curr.next;
        }

        public string Back(int steps)
        {
            while(steps-- > 0)
            {
                if (curr.prev == null) break;
                curr = curr.prev;
            }
            return curr.url;
        }

        public string Forward(int steps)
        {
            while (steps-- > 0)
            {
                if (curr.next == null) break;
                curr = curr.next;
            }
            return curr.url;
        }
    }
}
