using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Book;
using Domain.Book.VO;

namespace Data.Configurations
{
    /// Конфигурация для маппинга агрегата Book на таблицу базы данных.
    public sealed class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // 1. Настройка таблицы
            builder.ToTable("Books");

            // 2. Первичный ключ (BookId)
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    bookId => bookId.Value,
                    value => BookId.Create(value)
                )
                .HasColumnName("BookId")
                .IsRequired();

            // 3. Объекты-значения 

            builder.Property(b => b.BookTitle)
                .HasConversion(
                    title => title.Value,
                    value => Title.Create(value)
                )
                .HasColumnName("Title")
                // Используем константу из доменного объекта Title
                .HasMaxLength(Title.MAX_LENGTH)
                .IsRequired();

            builder.Property(b => b.Author)
                .HasConversion(
                    author => author.Name,
                    value => Author.Create(value)
                )
                .HasColumnName("Author")
                // Используем константу из доменного объекта Author
                .HasMaxLength(Author.MAX_LENGTH)
                .IsRequired();

            builder.Property(b => b.Isbn)
                .HasConversion(
                    isbn => isbn.Value,
                    value => ISBN.Create(value)
                )
                .HasColumnName("ISBN")
                // Используем константу из доменного объекта ISBN
                .HasMaxLength(ISBN.MAX_LENGTH)
                .IsRequired();

            // 4. Умное перечисление
            builder.Property(b => b.Status)
                .HasConversion(
                    status => status.Key,
                    key => BookStatus.FromKey(key)
                )
                .HasColumnName("StatusKey")
                .IsRequired();
        }
    }
}