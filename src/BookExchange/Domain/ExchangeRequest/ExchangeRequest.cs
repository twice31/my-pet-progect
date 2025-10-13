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

        public ExchangeRequestStatusValue Status { get; private set; }
        public ExchangeHistory History { get; private set; } // Приватный сеттер для контроля изменений

        // ЗАКРЫТЫЙ КОНСТРУКТОР
        private ExchangeRequest(
            RequestId id,
            RequestedBookId requestedBookId,
            RecipientId recipientId,
            BookOwnerId bookOwnerId,
            ExchangeMethod method,
            ExchangeRequestStatusValue status,
            ExchangeHistory history)
        {
            // ЗДесь предполагается, что валидация null-значений уже проведена в Create/New
            Id = id;
            RequestedBookId = requestedBookId;
            RecipientId = recipientId;
            BookOwnerId = bookOwnerId;
            Method = method;
            Status = status;
            History = history;
        }

        // ФАБРИЧНЫЙ МЕТОД: для загрузки существующего запроса (с валидацией)
        public static ExchangeRequest Create(
            RequestId id,
            RequestedBookId requestedBookId,
            RecipientId recipientId,
            BookOwnerId bookOwnerId,
            ExchangeMethod method,
            ExchangeRequestStatusValue status,
            ExchangeHistory history)
        {
            if (id == null) throw new ArgumentNullException(nameof(id), "Идентификатор запроса не может быть пустым.");
            if (requestedBookId == null) throw new ArgumentNullException(nameof(requestedBookId), "Идентификатор запрашиваемой книги не может быть пустым.");
            if (recipientId == null) throw new ArgumentNullException(nameof(recipientId), "Идентификатор получателя не может быть пустым.");
            if (bookOwnerId == null) throw new ArgumentNullException(nameof(bookOwnerId), "Идентификатор владельца книги не может быть пустым.");
            if (method == null) throw new ArgumentNullException(nameof(method), "Метод обмена не может быть пустым.");
            if (status == null) throw new ArgumentNullException(nameof(status), "Статус не может быть пустым.");
            if (history == null) throw new ArgumentNullException(nameof(history), "История обмена не может быть пустой.");

            return new ExchangeRequest(id, requestedBookId, recipientId, bookOwnerId, method, status, history);
        }

        // ФАБРИЧНЫЙ МЕТОД: для создания НОВОГо запроса
        public static ExchangeRequest New(
            RequestedBookId requestedBookId,
            RecipientId recipientId,
            BookOwnerId bookOwnerId,
            ExchangeMethod method)
        {
            var id = RequestId.Create(Guid.NewGuid());
            var status = ExchangeRequestStatusValue.Create(ExchangeRequestStatus.Requested);
            var history = ExchangeHistory.Create(); // Создаем пустую историю

            return Create(id, requestedBookId, recipientId, bookOwnerId, method, status, history);
        }


        // МЕТОД, ИЗМЕНЯЮЩИЙ СОСТОЯНИЕ: Изменить статус запроса обмена
        public void UpdateStatus(ExchangeRequestStatus newStatus)
        {
            Status = ExchangeRequestStatusValue.Create(newStatus);

            // Обновляем историю
            AddEventToHistory($"Статус изменен на: {Status.ToString()}");
        }

        // Метод теперь переприсваивает НОВЫЙ неизменяемый объект
        public void AddEventToHistory(string eventDescription)
        {
            History = History.AddEvent(eventDescription);
        }

        public override string ToString()
        {
            return $"Запрос обмена: {Id}, Книга: {RequestedBookId}, Статус: {Status}";
        }
    }
}