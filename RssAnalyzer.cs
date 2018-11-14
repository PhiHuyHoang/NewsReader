using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ThreadandXDoc
{
    class RssAnalyzer
    {
        string[] keywords;
        string url;
        Thread thread;

        public List<string> Links { get; private set; }

        public RssAnalyzer(string url, string[] keywords)
        {
            this.url = url;
            this.keywords = keywords;
            thread = new Thread(GetLink);
            thread.Start();
        }

        public void Join()
        {
            thread.Join(); // Blocking method - Wait until the previous thread fullfill
        }

        public void GetLink()
        {
            XDocument document = XDocument.Load(url);
            var items = document.Element("rss").Element("channel").Elements("item");
            var neededItem = items.Where(
                item => keywords.All(
                    word => item.Element("description").Value.ToLower().Contains(word)));

            Links = neededItem.Select(node => node.Element("link").Value).ToList();
        }
    }
}
