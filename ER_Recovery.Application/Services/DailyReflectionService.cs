using ER_Recovery.Application.Services.Interfaces;
using ER_Recovery.Web.Models;
using HtmlAgilityPack;
using System;
using System.Net;

namespace ER_Recovery.Web.Services
{
    public class DailyReflectionService : IDailyReflectionService
    {
        private readonly HttpClient _httpClient;

        public string Title { get; set; } = "";
        public string Body { get; set; } = "";

        public DailyReflectionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DailyReflection> GetTodaysReflectionAsync()
        {
            var url = "https://www.aa.org/daily-reflections";
            var html = await _httpClient.GetStringAsync(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            File.WriteAllText("daily.html", html);

            var article = doc.DocumentNode.SelectSingleNode("//article[contains(@class, 'node--type-daily-reflection')]");

            if (article == null)
                throw new Exception("Unable to find the daily reflection element.");

            var titleNode = article.SelectSingleNode(".//h3/span") ?? article.SelectSingleNode(".//h3");

            var title = WebUtility.HtmlDecode(titleNode?.InnerText.Trim() ?? " Daily Reflection");

            var currentMonth = DateTime.Now.ToString("MMMM");
            var dateNode = article.SelectSingleNode($".//div[contains(text(), '{currentMonth}')]");
            var dateText = dateNode?.InnerText.Trim() ?? DateTime.Today.ToString("MMMM dd");

            var bodyNode = article.SelectSingleNode(".//div[contains(@class, 'field--name-body')]");
            var bodyHtml = bodyNode?.InnerHtml.Trim() ?? "Body not found";

            return new DailyReflection
            {
                Title = title,
                Body = bodyHtml,
                Date = DateTime.Today
            };
        }
    }
}
