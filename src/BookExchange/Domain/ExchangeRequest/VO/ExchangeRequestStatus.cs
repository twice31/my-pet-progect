namespace Domain.ExchangeRequest.VO
{
    // Перечисление для Статуса запроса обмена
    public enum ExchangeRequestStatus
    {
        Requested, // Запрашивается
        InProgress, // В процессе
        Completed // завершено
    }

    // Объект значения для статуса запроса обмена
    public record ExchangeRequestStatusValue
    {
        public ExchangeRequestStatus Status { get; }

        public ExchangeRequestStatusValue(ExchangeRequestStatus status)
        {
            Status = status;
        }

        // Переопределяем метод ToString для вывода на русском языке
        public override string ToString() => Status switch
        {
            ExchangeRequestStatus.Requested => "Запрашивается",
            ExchangeRequestStatus.InProgress => "В процессе",
            ExchangeRequestStatus.Completed => "Завершено",
            _ => "Неизвестно" // Для обработки неизвестных значений
        };
    }
}
