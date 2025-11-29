namespace BookExchange.Presenters.Envelope
{
    /// <summary>
    /// Унифицированный контейнер (Envelope) для всех ответов API.
    /// </summary>
    /// <typeparam name="T">Тип возвращаемых данных.</typeparam>
    public class Envelope<T>
    {
        /// <summary>
        /// Полезная нагрузка (данные), возвращаемая клиенту. Может быть null.
        /// </summary>
        public T? Data { get; }

        /// <summary>
        /// Флаг, указывающий на успешное выполнение запроса.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Сообщение об ошибке, если запрос не удался.
        /// </summary>
        public string? ErrorMessage { get; }

        /// <summary>
        /// Конструктор для успешного ответа.
        /// </summary>
        /// <param name="data">Возвращаемые данные.</param>
        public Envelope(T data)
        {
            Data = data;
            IsSuccess = true;
            ErrorMessage = null;
        }

        /// <summary>
        /// Конструктор для ответа с ошибкой.
        /// </summary>
        /// <param name="errorMessage">Сообщение об ошибке.</param>
        public Envelope(string errorMessage)
        {
            Data = default;
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }
    }
}