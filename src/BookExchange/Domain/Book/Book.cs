using Domain.Book.VO;

public class Book
{
    public BookId Id { get; }
    public Title BookTitle { get; }
    public Author Author { get; }
    public BookStatusValue Status { get; private set; }

    public Book(BookId id, Title title, Author author, BookStatusValue status)
    {
        Id = id;
        BookTitle = title;
        Author = author;
        Status = status ?? throw new ArgumentNullException(nameof(status), "Статус книги не может быть null.");
    }

    // Метод для изменения статуса книги
    public void UpdateStatus(BookStatus newStatus)
    {
        // Можно добавить проверку например что книга не может быть изменена на тот же статусс
        if (Status.Status == newStatus)
        {
            throw new InvalidOperationException("Статус книги уже установлен на выбранное значение.");
        }

        // Допустим, только "Available" можно изменять на "Reserved" или "Exchanged"
        if (Status.Status == BookStatus.Available && (newStatus == BookStatus.Reserved || newStatus == BookStatus.Exchanged))
        {
            Status = new BookStatusValue(newStatus);
        }
        else if (Status.Status == BookStatus.Reserved && newStatus == BookStatus.Exchanged)
        {
            Status = new BookStatusValue(newStatus);
        }
        else
        {
            throw new InvalidOperationException("Невозможно изменить статус книги в текущем состоянии.");
        }
    }

    // Метод для отображения информации о книге
    public override string ToString()
    {
        return $"Книга: {BookTitle} автор: {Author} - Статус: {Status}";
    }
}
