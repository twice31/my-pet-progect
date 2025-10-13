namespace Domain.ExchangeRequest.VO
{
    public enum ExchangeRequestStatus
    {
        Requested,   // Запрошено
        InProgress,  // В процессе
        Completed    // Завершено
    }

    public record ExchangeRequestStatusValue
    {
        public ExchangeRequestStatus Status { get; }

        private ExchangeRequestStatusValue(ExchangeRequestStatus status)
        {
            Status = status;
        }

        public static ExchangeRequestStatusValue Create(ExchangeRequestStatus status)
        {
            return new ExchangeRequestStatusValue(status);
        }

        public override string ToString() => Status switch
        {
            ExchangeRequestStatus.Requested => "Запрошено",
            ExchangeRequestStatus.InProgress => "В процессе",
            ExchangeRequestStatus.Completed => "Завершено",
            _ => "Неизвестный статус"
        };
    }
}
