using Domain.Book.VO;
using Domain.User.VO;
using System;

namespace Domain.User
{
    public class User
    {
        public UserId Id { get; }
        public ContactInfo Contact { get; }
        public UserRating Rating { get; }

        // public get; и private set; для контроля изменений только изнутри класса
        public WishList WishList { get; private set; }

        // ЗАКРЫТЫЙ КОНСТРУКТОР: используется только для инициализации полей.
        private User(UserId id, ContactInfo contact, UserRating rating, WishList wishList)
        {
            Id = id;
            Contact = contact;
            Rating = rating;
            WishList = wishList;
        }

        // ФАБРИЧНЫЙ СТАТИЧЕСКИЙ МЕТОД: для создания существующего пользователя (с валидацией null)
        public static User Create(UserId id, ContactInfo contact, UserRating rating, WishList wishList)
        {
            // Валидация входных объектов
            if (id == null) throw new ArgumentNullException(nameof(id), "Идентификатор пользователя не может быть пустым.");
            if (contact == null) throw new ArgumentNullException(nameof(contact), "Контактная информация не может быть пустой.");
            if (rating == null) throw new ArgumentNullException(nameof(rating), "Рейтинг пользователя не может быть пустым.");
            if (wishList == null) throw new ArgumentNullException(nameof(wishList), "Список желаемого не может быть пустым.");

            return new User(id, contact, rating, wishList);
        }

        // ФАБРИЧНЫЙ СТАТИЧЕСКИЙ МЕТОД: для создания НОВОГО пользователя
        public static User New(ContactInfo contact)
        {
            if (contact == null) throw new ArgumentNullException(nameof(contact), "Контактная информация необходима для создания нового пользователя.");

            // Создаем Id
            var id = UserId.Create(Guid.NewGuid());

            // Явно указываем полный путь к классу Domain.User.VO.Rating для избежания конфликта имен.
            var initialRatingValue = Domain.User.VO.Rating.Create(0.0);
            var initialRating = UserRating.Create(initialRatingValue);

            // Создаем пустой список желаемого
            var emptyWishList = WishList.Create();

            return Create(id, contact, initialRating, emptyWishList);
        }

        // МЕТОД, ИЗМЕНЯЮЩИЙ СОСТОЯНИЕ: Добавить книгу в список желаемого
        public void AddToWishList(BookId bookId) 
        {
            WishList = WishList.Add(bookId);
        }

        // МЕТОД, ИЗМЕНЯЮЩИЙ СОСТОЯНИЕ: Удалить книгу из списка желаемого
        public void RemoveFromWishList(BookId bookId)
        {
            WishList = WishList.Remove(bookId);
        }

        public override string ToString()
        {
            return $"Пользователь {Id} - Контакты: {Contact}, Рейтинг: {Rating}";
        }
    }
}