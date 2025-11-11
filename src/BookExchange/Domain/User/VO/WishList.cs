using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Book.VO;

namespace Domain.User.VO
{
    public record WishList
    {
        public IReadOnlyList<BookId> Books { get; private set; }

        private WishList()
        {
            Books = new List<BookId>().AsReadOnly();
        }

        public static WishList Create(IEnumerable<BookId>? books = null)
        {
            var wishList = new WishList();

            var list = (books ?? Enumerable.Empty<BookId>()).ToList().AsReadOnly();
            wishList.Books = list;

            return wishList;
        }

        public WishList Add(BookId book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (Books.Contains(book)) return this;

            var newList = Books.ToList();
            newList.Add(book);
            return new WishList() { Books = newList.AsReadOnly() }; 
        }

        public WishList Remove(BookId book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (!Books.Contains(book)) return this;

            var newList = Books.ToList();
            newList.RemoveAll(b => b.Value == book.Value);
            return new WishList() { Books = newList.AsReadOnly() }; 
        }

        public override string ToString() => string.Join(", ", Books.Select(b => b.ToString()));
    }
}