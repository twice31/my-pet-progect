using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Book.VO;

namespace Domain.User.VO
{
    public record WishList
    {
        public IReadOnlyList<BookId> Books { get; }

        private WishList(IReadOnlyList<BookId> books)
        {
            Books = books;
        }

        public static WishList Create(IEnumerable<BookId> books = null)
        {
            var list = (books ?? Enumerable.Empty<BookId>()).ToList().AsReadOnly();
            return new WishList(list);
        }

        // Возвращает новый WishList с добавленной книгой
        public WishList Add(BookId book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (Books.Contains(book)) return this;

            var newList = Books.ToList();
            newList.Add(book);
            return new WishList(newList.AsReadOnly());
        }

        // Возвращает новый WishList с удалённой книгой
        public WishList Remove(BookId book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (!Books.Contains(book)) return this;

            var newList = Books.ToList();
            newList.RemoveAll(b => b == book);
            return new WishList(newList.AsReadOnly());
        }

        public override string ToString() => string.Join(", ", Books.Select(b => b.ToString()));
    }
}
