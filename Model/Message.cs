namespace RelayService.Model
{
    public record Message
    {
        public String MessId { get; init; }
        public String User { get; init; }
        public String Text { get; init; }
        public string image { get; init; }
        public UInt64 Timestamp { get; init; }
    }
}