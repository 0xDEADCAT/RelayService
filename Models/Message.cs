namespace RelayService.Models
{
    public record Message
    {
        public String MessId { get; init; }
        public String User { get; init; }
        public String Text { get; init; }
        public string image { get; init; }
        public string mid { get; init; }
        public UInt64 Timestamp { get; init; }
    }
}