using Domain.ExchangeRequest.VO;
using System;
using System.Collections.Generic;

namespace Domain.ExchangeRequest
{
    public class ExchangeRequest
    {
        public RequestId Id { get; }
        public RequestedBookId RequestedBookId { get; }
        public RecipientId RecipientId { get; }
        public BookOwnerId BookOwnerId { get; }
        public ExchangeMethod Method { get; }

        public ExchangeRequestStatus Status { get; private set; }

        public ExchangeHistory History { get; private set; }

        private ExchangeRequest(
            RequestId id,
            RequestedBookId requestedBookId,
            RecipientId recipientId,
            BookOwnerId bookOwnerId,
            ExchangeMethod method,
            ExchangeRequestStatus status)
        {
            Id = id;
            RequestedBookId = requestedBookId;
            RecipientId = recipientId;
            BookOwnerId = bookOwnerId;
            Method = method;
            Status = status ?? throw new ArgumentNullException(nameof(status), "Статус запроса не может быть null.");

            History = ExchangeHistory.Create();
        }

        public static ExchangeRequest Create(
            RequestId id,
            RequestedBookId requestedBookId,
            RecipientId recipientId,
            BookOwnerId bookOwnerId,
            ExchangeMethod method,
            ExchangeRequestStatus status,
            ExchangeHistory history)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (requestedBookId == null) throw new ArgumentNullException(nameof(requestedBookId));
            if (recipientId == null) throw new ArgumentNullException(nameof(recipientId));
            if (bookOwnerId == null) throw new ArgumentNullException(nameof(bookOwnerId));
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (status == null) throw new ArgumentNullException(nameof(status));
            if (history == null) throw new ArgumentNullException(nameof(history));

            var request = new ExchangeRequest(id, requestedBookId, recipientId, bookOwnerId, method, status)
            {
                History = history
            };

            return request;
        }

        public static ExchangeRequest New(RequestedBookId requestedBookId, RecipientId recipientId, BookOwnerId bookOwnerId, ExchangeMethod method)
        {
            var id = RequestId.Create(Guid.NewGuid());
            var status = ExchangeRequestStatus.Requested;
            var history = ExchangeHistory.Create();

            var request = Create(id, requestedBookId, recipientId, bookOwnerId, method, status, history);

            request.AddEventToHistory($"Создан новый запрос обмена. Метод: {method.Method}");

            return request;
        }


        public void UpdateStatus(ExchangeRequestStatus newStatus)
        {
            if (newStatus == ExchangeRequestStatus.Completed && !Status.CanBeCompleted())
            {
                throw new InvalidOperationException($"Запрос в статусе '{Status.Name}' не может быть завершен.");
            }

            if (newStatus == ExchangeRequestStatus.Cancelled && !Status.CanBeCancelled())
            {
                throw new InvalidOperationException($"Запрос в статусе '{Status.Name}' не может быть отменен.");
            }

            if (Status == newStatus)
            {
                throw new InvalidOperationException("Статус запроса уже установлен на выбранное значение.");
            }

            Status = newStatus;

            AddEventToHistory($"Статус изменен на: {Status.Name}");
        }

        public void AddEventToHistory(string eventDescription)
        {
            History = History.AddEvent(eventDescription);
        }

        public override string ToString()
        {
            return $"Запрос обмена: {Id}, Книга: {RequestedBookId}, Статус: {Status.Name}";
        }
    }
}