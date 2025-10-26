using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.User;
using Domain.Book.VO;

namespace Data.Configurations
{
    // Отдельная конфигурация для WishList
    public sealed class WishListConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.OwnsOne(u => u.WishList, wishListBuilder =>
            {
                wishListBuilder.OwnsMany(w => w.Books, bookIdBuilder =>
                {
                    bookIdBuilder.HasKey("Id");

                    bookIdBuilder.Property(b => b)
                        .HasConversion(
                            bookId => bookId.Value,
                            value => BookId.Create(value)
                        )
                        .HasColumnName("BookId")
                        .IsRequired();

                    bookIdBuilder.WithOwner().HasForeignKey("UserId");

                    bookIdBuilder.ToTable("WishListItems");
                });

                wishListBuilder.Navigation(w => w.Books).IsRequired();

            });
        }
    }
}