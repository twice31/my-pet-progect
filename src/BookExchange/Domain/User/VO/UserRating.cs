using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.User.VO
{
    public record UserRating
    {
        public Rating Rating { get; }
        public IReadOnlyList<Review> Reviews { get; }

        private UserRating(Rating rating, IReadOnlyList<Review> reviews)
        {
            Rating = rating;
            Reviews = reviews;
        }

        public static UserRating Create(Rating rating, IEnumerable<Review> reviews = null)
        {
            if (rating == null) throw new ArgumentNullException(nameof(rating));

            var list = (reviews ?? Enumerable.Empty<Review>()).ToList().AsReadOnly();
            return new UserRating(rating, list);
        }

        public override string ToString() => $"Рейтинг: {Rating}, Отзывы: {string.Join("; ", Reviews)}";
    }
}
