using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.User;
using Domain.Book.VO;
using System;

namespace BookExchange.Infrastructure.Data.Configurations
{
    public sealed class WishListConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.OwnsOne(u => u.WishList, wishListBuilder =>
            {
                wishListBuilder.OwnsMany(w => w.Books, bookIdBuilder =>
                {
                    bookIdBuilder.Property<Guid>("Id");
                    bookIdBuilder.HasKey("Id");

                    bookIdBuilder.Property(b => b.Value)
                        .HasColumnName("BookId")
                        .IsRequired();


                    bookIdBuilder.WithOwner().HasForeignKey("UserId");

                    bookIdBuilder.ToTable("WishListItems");
                });

            });
        }
    }
}