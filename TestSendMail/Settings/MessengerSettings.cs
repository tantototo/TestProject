namespace TestSendMail.Settings
{
    public class MessengerSettings
    {
        public string Queue { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public IDictionary<string, object>? Arguments { get; set; }
    }
}
