using Domain.Book.VO;

namespace Domain.User.VO
{
    public class WishList
    {
        public List<BookId> Books { get; }  

        public WishList(List<BookId> books = null)
        {
            Books = books ?? new List<BookId>();
        }

        public void AddBook(BookId book)
        {
            if (!Books.Contains(book))  
            {
                Books.Add(book);
            }
        }

        public void RemoveBook(BookId book)
        {
            Books.Remove(book);
        }

        public override string ToString()
        {
            return string.Join(", ", Books.Select(b => b.ToString()));
        }
    }
}
