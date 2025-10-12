using Domain.Book.VO;
using Domain.User.VO;

namespace Domain.User
{
    public class User
    {
        public UserId Id { get; }
        public ContactInfo Contact { get; }
        public UserRating Rating { get; }
        public WishList WishList { get; }

        public User(UserId id, ContactInfo contact, UserRating rating, WishList wishList)
        {
            Id = id;
            Contact = contact;
            Rating = rating;
            WishList = wishList;
        }

        // Добавить книгу в список желаемых
        public void AddToWishList(BookId bookId)
        {
            WishList.AddBook(bookId);
        }

        // Удалить книгу из списка желаемых
        public void RemoveFromWishList(BookId bookId)
        {
            WishList.RemoveBook(bookId);
        }

        public override string ToString()
        {
            return $"User {Id} - {Contact}, {Rating}";
        }
    }
}
