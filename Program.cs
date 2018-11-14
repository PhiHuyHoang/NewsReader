using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadandXDoc
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] sources =
            {
                "https://dantri.com.vn/trangchu.rss",
                "https://www.24h.com.vn/upload/rss/trangchu24h.rss",
                "http://vietnamnet.vn/rss/home.rss",
                "http://gamek.vn/trang-chu.rss"
            };

            string[] keywords = { "tình" };

            List<RssAnalyzer> analyzers = new List<RssAnalyzer>();

            foreach (string url in sources)
            {
                analyzers.Add(new RssAnalyzer(url, keywords));
            }

            //WAIT
            foreach (var item in analyzers)
            {
                item.Join();
            }
            //WRITE
            foreach (var item in analyzers)
            {
                foreach (string url in item.Links)
                {
                    Console.WriteLine(url);
                    Process.Start(url);
                    Thread.Sleep(500);
                }
            }
            Console.WriteLine("END");
            Console.ReadLine();
        }
    }
}
