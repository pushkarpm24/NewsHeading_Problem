using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NewsHeading
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void NewsHeadingTest()
        {
            Dictionary<int, string> dictionaryNews = new Dictionary<int, string>();
            IWebDriver driver = new ChromeDriver();

            driver = new ChromeDriver();
            // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
            driver.Url = "https://news.ycombinator.com/";

            IList<IWebElement> newsElement = driver.FindElements(By.ClassName("storylink"));
            IList<IWebElement> pointsElement = driver.FindElements(By.ClassName("score"));

            List<string> news = new List<string>();
            List<int> point = new List<int>();

            for (int i = 0; i < pointsElement.Count; i++)
            {
                string value = newsElement[i].Text;
                news.Add(value);
                Console.WriteLine(value);

                string key = pointsElement[i].Text;
                string trimkey = key.Replace("points", "");
                point.Add(int.Parse(trimkey));
                Console.WriteLine(trimkey);
            }

            for (int i = 0; i < point.Count; i++)
            {
                if (!dictionaryNews.ContainsKey(point[i]))
                {
                    dictionaryNews.Add(point[i], news[i]);
                }
            }

            foreach (var data in dictionaryNews)
            {
                Console.WriteLine(data);

            }

            string joinStr = string.Join(Environment.NewLine,
                   dictionaryNews.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value));

            var results = joinStr.Split(' ').Where(x => x.Length > 3)
                              .GroupBy(x => x)
                              .Select(x => new { Count = x.Count(), Word = x.Key })
                              .OrderByDescending(x => x.Count);

            Console.WriteLine("=================================================================================");

            Console.WriteLine("Most Repeated Word is:::::" + results.First().Word);

            Console.WriteLine("=================================================================================");
            Console.WriteLine("Heading With Highest Points is:::::" + dictionaryNews.OrderBy(key => key.Key).Last());
            dictionaryNews.Clear();

            driver.Quit();
        }
    }
}
