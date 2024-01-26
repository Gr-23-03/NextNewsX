using Newtonsoft.Json;

namespace NextNews.Models.Database
{

    public class Stock
    {
        public List<Top10Item> top10 { get; set; }
    }

    public class Top10Item
    {
        public string name { get; set; }
        public string symbol { get; set; }
        public double close { get; set; }
        public double prevClose { get; set; }
        public double percentChange { get; set; }
    }

}
