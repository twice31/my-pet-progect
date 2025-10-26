using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.User;
using Domain.User.VO;
using Domain.Book.VO;

namespace Data.Configurations
{
    // Конфигурация для маппинга агрегата User на таблицу базы данных.
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // 1. НАСТРОЙКА ТАБЛИЦЫ
            builder.ToTable("Users");

            // 2. ПЕРВИЧНЫЙ КЛЮЧ И ПРОСТОЙ VO (UserId)
            builder.HasKey(u => u.Id);

            // Настройка конвертера: UserId <--> Guid
            builder.Property(u => u.Id)
                .HasConversion(
                    userId => userId.Value,
                    value => UserId.Create(value)
                )
                .HasColumnName("UserId")
                .IsRequired();

            // 3. СЛОЖНЫЙ VO: ContactInfo
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
                    .HasMaxLength(UserEmail.MAX_EMAIL_LENGTH)
                    .IsRequired();
            });
            builder.Navigation(u => u.Contact).IsRequired();


            // 4. СЛОЖНЫЙ VO: UserRating
            builder.OwnsOne(u => u.Rating, ratingBuilder =>
            {
                // Конвертер для Rating
                ratingBuilder.Property(r => r.Rating)
                    .HasConversion(
                        rating => rating.Value,
                        value => Rating.Create(value)
                    )
                    .HasColumnName("RatingValue")
                    .IsRequired();


                // 5. КОЛЛЕКЦИЯ VO
                ratingBuilder.OwnsMany(r => r.Reviews, reviewBuilder =>
                {
                    reviewBuilder.HasKey("Id");

                    reviewBuilder.Property(r => r.Text)
                        .HasColumnName("ReviewText")
                        .IsRequired()
                        // Используем константу MAX_LENGTH из Review.cs
                        .HasMaxLength(Review.MAX_LENGTH);

                    reviewBuilder.Property(r => r.Date)
                        .HasColumnName("ReviewDate")
                        .IsRequired();

                    // Связываем отзыв с пользователем
                    reviewBuilder.WithOwner().HasForeignKey("UserId");
                    reviewBuilder.ToTable("UserReviews");
                });
                ratingBuilder.Navigation(r => r.Reviews).IsRequired();
            });
            builder.Navigation(u => u.Rating).IsRequired();

            // 6. СЛОЖНЫЙ VO: WishList 
            // Конфигурация WishList ВЫНЕСЕНА в WishListConfiguration.cs
            builder.Navigation(u => u.WishList).IsRequired();
        }
    }
}