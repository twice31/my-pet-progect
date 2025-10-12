namespace Domain.ExchangeRequest.VO
{
    public record ExchangeHistory
    {
        public List<string> Events { get; }

        public ExchangeHistory(List<string> events)
        {
            Events = events ?? new List<string>();
        }

        public void AddEvent(string eventDescription)
        {
            Events.Add(eventDescription);
        }

        public override string ToString() => string.Join(", ", Events);
    }
}
