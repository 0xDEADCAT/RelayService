namespace Contracts
{
    public interface DeleteMessage
    {
        public Guid CommandId { get; }
        public DateTime Timestamp { get; }
        public String MessId { get; }
    }
}