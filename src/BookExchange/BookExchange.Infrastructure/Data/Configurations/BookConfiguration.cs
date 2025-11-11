using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Book;
using Domain.Book.VO;
using Domain.User.VO; 

namespace BookExchange.Infrastructure.Data.Configurations
{
    public sealed class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    bookId => bookId.Value,
                    value => BookId.Create(value)
                )
                .HasColumnName("BookId")
                .IsRequired();


            builder.Property(b => b.Title)
                .HasConversion(
                    title => title.Value,
                    value => Title.Create(value)
                )
                .HasColumnName("Title")
                .HasMaxLength(Title.MAX_LENGTH)
                .IsRequired();

            builder.Property(b => b.Author)
                .HasConversion(
                    author => author.Name,
                    value => Author.Create(value)
                )
                .HasColumnName("Author")
                .HasMaxLength(Author.MAX_LENGTH)
                .IsRequired();

            builder.Property(b => b.Isbn)
                .HasConversion(
                    isbn => isbn.Value,
                    value => ISBN.Create(value)
                )
                .HasColumnName("ISBN")
                .HasMaxLength(ISBN.MAX_LENGTH)
                .IsRequired();

            builder.Property(b => b.OwnerId)
                .HasConversion(
                    ownerId => ownerId.Value,
                    value => UserId.Create(value)
                )
                .HasColumnName("OwnerId")
                .IsRequired();


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