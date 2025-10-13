namespace Domain.ExchangeRequest.VO
{
    public record BookOwnerId
    {
        public Guid Value { get; }

        private BookOwnerId(Guid value)
        {
            Value = value;
        }

        public static BookOwnerId Create(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Идентификатор владельца книги не может быть пустым.", nameof(value));

            return new BookOwnerId(value);
        }

        public override string ToString() => Value.ToString();
    }
}
