using Domain.Book.VO;
using Domain.User.VO;
using System;

namespace Domain.User
{
    public class User
    {
        public UserId Id { get; }

        public ContactInfo Contact { get; private set; }
        public UserRating Rating { get; private set; }

        public WishList WishList { get; private set; }

        private User(UserId id)
        {
            Id = id;
        }

        public static User Create(UserId id, ContactInfo contact, UserRating rating, WishList wishList)
        {
            if (id == null) throw new ArgumentNullException(nameof(id), "Идентификатор пользователя не может быть пустым.");
            if (contact == null) throw new ArgumentNullException(nameof(contact), "Контактная информация не может быть пустой.");
            if (rating == null) throw new ArgumentNullException(nameof(rating), "Рейтинг пользователя не может быть пустым.");
            if (wishList == null) throw new ArgumentNullException(nameof(wishList), "Список желаемого не может быть пустым.");

            return new User(id)
            {
                Contact = contact,
                Rating = rating,
                WishList = wishList
            };
        }

        public static User New(ContactInfo contact)
        {
            if (contact == null) throw new ArgumentNullException(nameof(contact), "Контактная информация необходима для создания нового пользователя.");

            var id = UserId.Create(Guid.NewGuid());

            var initialRatingValue = Domain.User.VO.Rating.Create(0.0);
            var initialRating = UserRating.Create(initialRatingValue);

            var emptyWishList = WishList.Create();

            return Create(id, contact, initialRating, emptyWishList);
        }

        public void AddToWishList(BookId bookId)
        {
            WishList = WishList.Add(bookId);
        }

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