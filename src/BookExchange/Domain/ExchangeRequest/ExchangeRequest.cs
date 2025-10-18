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

        // Заменяем ExchangeRequestStatusValue на ExchangeRequestStatus (Умное перечисление)
        public ExchangeRequestStatus Status { get; private set; }
        public ExchangeHistory History { get; private set; }

        // ЗАКРЫТЫЙ КОНСТРУКТОР
        private ExchangeRequest(
            RequestId id,
            RequestedBookId requestedBookId,
            RecipientId recipientId,
            BookOwnerId bookOwnerId,
            ExchangeMethod method,
            ExchangeRequestStatus status, // Принимает ExchangeRequestStatus
            ExchangeHistory history)
        {
            Id = id;
            RequestedBookId = requestedBookId;
            RecipientId = recipientId;
            BookOwnerId = bookOwnerId;
            Method = method;
            Status = status ?? throw new ArgumentNullException(nameof(status), "Статус запроса не может быть null.");
            History = history;
        }

        // ФАБРИЧНЫЙ МЕТОД: для загрузки существующего запроса (с валидацией)
        public static ExchangeRequest Create(
            RequestId id,
            RequestedBookId requestedBookId,
            RecipientId recipientId,
            BookOwnerId bookOwnerId,
            ExchangeMethod method,
            ExchangeRequestStatus status, // Принимает ExchangeRequestStatus
            ExchangeHistory history)
        {
            if (id == null) throw new ArgumentNullException(nameof(id), "Идентификатор запроса не может быть пустым.");
            if (history == null) throw new ArgumentNullException(nameof(history), "История обмена не может быть пустой.");

            return new ExchangeRequest(id, requestedBookId, recipientId, bookOwnerId, method, status, history);
        }

        // ФАБРИЧНЫЙ МЕТОД: для создания НОВОГО запроса
        public static ExchangeRequest New(
            RequestedBookId requestedBookId,
            RecipientId recipientId,
            BookOwnerId bookOwnerId,
            ExchangeMethod method)
        {
            var id = RequestId.Create(Guid.NewGuid());
            var status = ExchangeRequestStatus.Requested;
            var history = ExchangeHistory.Create();

            return Create(id, requestedBookId, recipientId, bookOwnerId, method, status, history);
        }


        // МЕТОД, ИЗМЕНЯЮЩИЙ СОСТОЯНИЕ: Изменить статус запроса обмена
        public void UpdateStatus(ExchangeRequestStatus newStatus)
        {
            // Используем логику поведения из "Умного перечисления"
            if (newStatus == ExchangeRequestStatus.Completed && !Status.CanBeCompleted())
            {
                throw new InvalidOperationException($"Запрос в статусе '{Status.Name}' не может быть завершен.");
            }

            if (newStatus == ExchangeRequestStatus.Cancelled && !Status.CanBeCancelled())
            {
                throw new InvalidOperationException($"Запрос в статусе '{Status.Name}' не может быть отменен.");
            }

            // Используем унаследованный оператор сравнения
            if (Status == newStatus)
            {
                throw new InvalidOperationException("Статус запроса уже установлен на выбранное значение.");
            }

            Status = newStatus;

            // Обновляем историю, используя Status.Name
            AddEventToHistory($"Статус изменен на: {Status.Name}");
        }

        // Метод теперь переприсваивает НОВЫЙ неизменяемый объект
        public void AddEventToHistory(string eventDescription)
        {
            History = History.AddEvent(eventDescription);
        }

        public override string ToString()
        {
            // Используем Status.Name для понятного названия
            return $"Запрос обмена: {Id}, Книга: {RequestedBookId}, Статус: {Status.Name}";
        }
    }
}