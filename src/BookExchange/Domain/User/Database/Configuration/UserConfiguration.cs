using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.User;
using Domain.User.VO;
using Domain.Book.VO;
using System.Linq;

namespace Data.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // 1. НАСТРОЙКА ТАБЛИЦЫ
            builder.ToTable("Users");

            // 2. ПЕРВИЧНЫЙ КЛЮЧ И ПРОСТОЙ VO (UserId)
            builder.HasKey(u => u.Id);

            // Настройка конвертера: UserId <--> Guid (для хранения в БД)
            builder.Property(u => u.Id)
                .HasConversion(
                    userId => userId.Value,          // Преобразование UserId в Guid
                    value => UserId.Create(value)    // Преобразование Guid в UserId
                )
                .HasColumnName("UserId")
                .IsRequired();

            // 3. СЛОЖНЫЙ VO: ContactInfo
            // Свойства ContactInfo будут отображены в таблицу Users
            builder.OwnsOne(u => u.Contact, contactBuilder =>
            {
                // Настройка конвертера для UserPhone
                contactBuilder.Property(c => c.Phone)
                    .HasConversion(
                        phone => phone.Value,
                        value => UserPhone.Create(value)
                    )
                    .HasColumnName("Phone")
                    .IsRequired();

                // Настройка конвертера для UserEmail
                contactBuilder.Property(c => c.Email)
                    .HasConversion(
                        email => email.Value,
                        value => UserEmail.Create(value)
                    )
                    .HasColumnName("Email")
                    .IsRequired()
                    // Указываем максимальную длину, используя константу из VO
                    .HasMaxLength(UserEmail.MAX_EMAIL_LENGTH);
            });

            // 4. СЛОЖНЫЙ VO: UserRating
            builder.OwnsOne(u => u.Rating, ratingBuilder =>
            {
                // Конфигурация внутреннего VO Rating
                ratingBuilder.Property(r => r.Rating)
                    .HasConversion(
                        rating => rating.Value,
                        value => Rating.Create(value)
                    )
                    .HasColumnName("RatingValue")
                    .IsRequired();

                // 5. ВЛОЖЕННАЯ КОЛЛЕКЦИЯ VO: Reviews
                ratingBuilder.OwnsMany(r => r.Reviews, reviewBuilder =>
                {
                    reviewBuilder.HasKey("Id");

                    reviewBuilder.WithOwner().HasForeignKey("UserId");
                    reviewBuilder.ToTable("UserReviews");

                    reviewBuilder.Property(r => r.Text)
                        .HasColumnName("ReviewText")
                        .IsRequired()
                        .HasMaxLength(500); // Ограничение длины отзыва

                    reviewBuilder.Property(r => r.Date)
                        .HasColumnName("ReviewDate")
                        .IsRequired();
                });
            });

            // 6. СЛОЖНЫЙ VO: WishList
            builder.OwnsMany(u => u.WishList.Books, wishListBuilder =>
            {
                wishListBuilder.HasKey("Id");

                // Настройка BookId
                wishListBuilder.Property(b => b)
                    .HasConversion(
                        bookId => bookId.Value,
                        value => BookId.Create(value)
                    )
                    .HasColumnName("BookId")
                    .IsRequired();

                wishListBuilder.WithOwner().HasForeignKey("UserId");
                wishListBuilder.ToTable("WishListItems");
            });
        }
    }
}