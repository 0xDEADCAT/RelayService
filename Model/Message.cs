namespace RelayService.Model
{
    public class Message
    {
        public UInt64 MessId { get; set; }
        public String User { get; set; }
        public String Text { get; set; }
        public DateTime Timestamp { get; set; }
    }
}