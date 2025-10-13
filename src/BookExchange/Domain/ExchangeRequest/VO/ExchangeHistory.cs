using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.ExchangeRequest.VO
{
    public record ExchangeHistory
    {
        // Используем IReadOnlyList для обеспечения неизменяемости снаружи
        public IReadOnlyList<string> Events { get; }

        // ЗАКРЫТЫЙ КОНСТРУКТОР
        private ExchangeHistory(IReadOnlyList<string> events)
        {
            Events = events ?? throw new ArgumentNullException(nameof(events));
        }

        // ФАБРИЧНЫЙ МЕТОД
        public static ExchangeHistory Create(IEnumerable<string>? events = null)
        {
            var list = (events ?? Enumerable.Empty<string>()).ToList().AsReadOnly();
            return new ExchangeHistory(list);
        }

        // Метод AddEvent теперь возвращает НОВЫЙ объект
        public ExchangeHistory AddEvent(string eventDescription)
        {
            if (string.IsNullOrWhiteSpace(eventDescription))
                throw new ArgumentException("Описание события не может быть пустым.", nameof(eventDescription));

            // Создаем новый список, добавляем событие
            var newList = Events.ToList();
            newList.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm}] {eventDescription}");

            // Возвращаем НОВЫЙ экземпляр 
            return new ExchangeHistory(newList.AsReadOnly());
        }

        public override string ToString() => string.Join(" | ", Events);
    }
}