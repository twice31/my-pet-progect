using Domain.Enumerations;
using Domain.Enumerations;
using System;

namespace Domain.ExchangeRequest.VO
{
    // Класс ExchangeRequestStatus наследует от базового класса Enumeration<T>
    // и заменяет старые enum ExchangeRequestStatus и record ExchangeRequestStatusValue.
    public sealed class ExchangeRequestStatus : Enumeration<ExchangeRequestStatus>
    {
        // Статические поля, представляющие конкретные экземпляры статусов
        public static readonly ExchangeRequestStatus Requested = new ExchangeRequestStatus(1, "Запрошено");
        public static readonly ExchangeRequestStatus InProgress = new ExchangeRequestStatus(2, "В процессе");
        public static readonly ExchangeRequestStatus Completed = new ExchangeRequestStatus(3, "Завершено");
        // Добавлен новый статус
        public static readonly ExchangeRequestStatus Cancelled = new ExchangeRequestStatus(4, "Отменено");

        public bool CanBeCompleted() => this == InProgress;

        public bool CanBeCancelled() => this == Requested || this == InProgress;

        // Закрытый конструктор вызывает базовый конструктор
        private ExchangeRequestStatus(int key, string name)
            : base(key, name)
        {
        }
    }
}