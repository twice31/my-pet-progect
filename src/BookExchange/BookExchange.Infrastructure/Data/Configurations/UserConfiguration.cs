using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.User;
using Domain.User.VO;
using Domain.Book.VO;
using System;

namespace BookExchange.Infrastructure.Data.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasConversion(
                    userId => userId.Value,
                    value => UserId.Create(value)
                )
                .HasColumnName("UserId")
                .IsRequired();

            builder.OwnsOne(u => u.Contact, contactBuilder =>
            {
                contactBuilder.Property(c => c.Phone)
                    .HasConversion(
                        phone => phone.Value,
                        value => UserPhone.Create(value)
                    )
                    .HasColumnName("Phone")
                    .IsRequired();

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


            builder.OwnsOne(u => u.Rating, ratingBuilder =>
            {
                ratingBuilder.Property(r => r.Rating)
                    .HasConversion(
                        rating => rating.Value,
                        value => Rating.Create(value)
                    )
                    .HasColumnName("RatingValue")
                    .IsRequired();


                ratingBuilder.OwnsMany(r => r.Reviews, reviewBuilder =>
                {
                    reviewBuilder.Property<Guid>("Id");
                    reviewBuilder.HasKey("Id");

                    reviewBuilder.Property(r => r.Text)
                        .HasColumnName("ReviewText")
                        .IsRequired()
                        .HasMaxLength(Review.MAX_LENGTH);

                    reviewBuilder.Property(r => r.Date)
                        .HasColumnName("ReviewDate")
                        .IsRequired();

                    reviewBuilder.WithOwner().HasForeignKey("UserId");
                    reviewBuilder.ToTable("UserReviews");
                });
            });
            builder.Navigation(u => u.Rating).IsRequired();

        }
    }
}