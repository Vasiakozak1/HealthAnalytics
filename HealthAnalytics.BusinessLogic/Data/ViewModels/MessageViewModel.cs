namespace HealthAnalytics.BusinessLogic.Data.ViewModels
{
    public class MessageViewModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Text { get; set; }
        public MessageViewModel(string text, string title, string subtitle = "")
        {
            Text = text;
            Title = title;
            Subtitle = subtitle;
        }
    }
}
