using Domain.ExchangeRequest.VO;

namespace Domain.ExchangeRequest
{
    public class ExchangeRequest
    {
        public RequestId Id { get; }
        public RequestedBookId RequestedBookId { get; }
        public RecipientId RecipientId { get; }
        public BookOwnerId BookOwnerId { get; }
        public ExchangeMethod Method { get; }
        public ExchangeRequestStatusValue Status { get; private set; } // Сделаем set приватным
        public ExchangeHistory History { get; }

        public ExchangeRequest(
            RequestId id,
            RequestedBookId requestedBookId,
            RecipientId recipientId,
            BookOwnerId bookOwnerId,
            ExchangeMethod method,
            ExchangeRequestStatusValue status,
            ExchangeHistory history)
        {
            Id = id;
            RequestedBookId = requestedBookId;
            RecipientId = recipientId;
            BookOwnerId = bookOwnerId;
            Method = method;
            Status = status;
            History = history ?? new ExchangeHistory(new List<string>());
        }

        // Метод для изменения статуса запроса обмена
        public void UpdateStatus(ExchangeRequestStatus newStatus)
        {
            Status = new ExchangeRequestStatusValue(newStatus); // создаем нвый объект
        }

        // Метод для добавления события в историю обмена
        public void AddEventToHistory(string eventDescription)
        {
            History.AddEvent(eventDescription);
        }

        public override string ToString()
        {
            return $"ExchangeRequest: {Id}, Book: {RequestedBookId}, Status: {Status}";
        }
    }
}
