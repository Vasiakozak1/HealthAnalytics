namespace HealthAnalytics.BusinessLogic.Data.ViewModels
{
    public class MessageViewModel
    {
        public string Title { get; }
        public string Subtitle { get; }
        public string Text { get; }
        public MessageType MessageType { get; }
        public MessageViewModel(string text, string title, MessageType messageType, string subtitle = "")
        {
            Text = text;
            Title = title;
            Subtitle = subtitle;
            MessageType = messageType;
        }
    }

    public enum MessageType
    {
        Dialog = 1,
        Toast = 2
    }
}
