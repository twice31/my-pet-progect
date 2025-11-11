using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.User.VO
{
    public record UserRating
    {
        public Rating Rating { get; }
        public IReadOnlyList<Review> Reviews { get; private set; }

        private UserRating(Rating rating)
        {
            Rating = rating;
            Reviews = new List<Review>().AsReadOnly();
        }

        public static UserRating Create(Rating rating, IEnumerable<Review>? reviews = null)
        {
            if (rating == null) throw new ArgumentNullException(nameof(rating));

            var userRating = new UserRating(rating);

            var list = (reviews ?? Enumerable.Empty<Review>()).ToList().AsReadOnly();
            userRating.Reviews = list;

            return userRating;
        }

        public override string ToString() => $"Рейтинг: {Rating}, Отзывы: {string.Join("; ", Reviews)}";
    }
}