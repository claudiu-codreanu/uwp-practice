using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Model
{
    public class NewsItem
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Headline { get; set; }
        public string Subhead { get; set; }
        public string Dateline { get; set; }
        public string Image { get; set; }
    }


    public class NewsItemsFeed
    {
        public static List<NewsItem> GetNews()
        {
            List<NewsItem> list = new List<NewsItem>();

			list.Add(new NewsItem()
			{
				Id = 1,
				Category = "Financial",
				Headline = "Stocks lower on terror attack in Nice, France",
				Subhead = "DJIA, S&P500 close day lower",
				Dateline = "Thursday, July 14 2016",
				Image = "Assets/Financial1.png"
			});

			list.Add(new NewsItem()
			{
				Id = 2,
				Category = "Financial",
				Headline = "Gold higher amid global uncertainty in financial markets",
				Subhead = "Gold spot highest in six weeks",
				Dateline = "Friday, July 15 2016",
				Image = "Assets/Financial2.png"
			});

			list.Add(new NewsItem()
			{
				Id = 3,
				Category = "Financial",
				Headline = "Apple stock price flat on so-so earnings report",
				Subhead = "Apple sales barely meet forecast",
				Dateline = "Saturday, July 16 2016",
				Image = "Assets/Financial3.png"
			});

			list.Add(new NewsItem()
			{
				Id = 4,
				Category = "Financial",
				Headline = "Job market remains relatively strong despite global turmoil",
				Subhead = "500K jobs created in Q2 alone",
				Dateline = "Sunday, July 17 2016",
				Image = "Assets/Financial4.png"
			});

			list.Add(new NewsItem()
			{
				Id = 5,
				Category = "Financial",
				Headline = "Dollar gains agains Euro as other nations likely to follow Brexit example",
				Subhead = "Brexit unfolds, EU put to test",
				Dateline = "Monday, July 18 2016",
				Image = "Assets/Financial5.png"
			});


			list.Add(new NewsItem()
			{
				Id = 6,
				Category = "Food",
				Headline = "Food truck event in downtown Hollywood, FL",
				Subhead = "Food trucks near you",
				Dateline = "Tuesday, July 19 2016",
				Image = "Assets/Food1.png"
			});

			list.Add(new NewsItem()
			{
				Id = 7,
				Category = "Food",
				Headline = "Intermittent fasting good for your health, study shows",
				Subhead = "Science confirms millenia old religious practice",
				Dateline = "Tuesday, July 19 2016",
				Image = "Assets/Food2.png"
			});

			list.Add(new NewsItem()
			{
				Id = 8,
				Category = "Food",
				Headline = "Cutthroat Kitchen working on season five, producer says",
				Subhead = "Food Network strikes deal on new season",
				Dateline = "Tuesday, July 19 2016",
				Image = "Assets/Food3.png"
			});

			list.Add(new NewsItem()
			{
				Id = 9,
				Category = "Food",
				Headline = "Fish meat contains Omega3 oils, good for brain and nervous system",
				Subhead = "Eat more fish, less meat",
				Dateline = "Tuesday, July 19 2016",
				Image = "Assets/Food4.png"
			});

			list.Add(new NewsItem()
			{
				Id = 10,
				Category = "Food",
				Headline = "Walmart to open neighbourhood friendly, fresh grocery market",
				Subhead = "Cooper City residents have new shopping option",
				Dateline = "Tuesday, July 19 2016",
				Image = "Assets/Food5.png"
			});

			return list;
        }
    }
}
