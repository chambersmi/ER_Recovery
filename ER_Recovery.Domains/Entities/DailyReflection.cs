namespace ER_Recovery.Domains.Entities
{
    public class DailyReflection
    {
        public DateTime Date { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
