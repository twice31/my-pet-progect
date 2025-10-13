namespace Domain.ExchangeRequest.VO
{
    public record RequestedBookId
    {
        public Guid Value { get; }

        private RequestedBookId(Guid value)
        {
            Value = value;
        }

        public static RequestedBookId Create(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Идентификатор запрашиваемой книги не может быть пустым.", nameof(value));

            return new RequestedBookId(value);
        }

        public override string ToString() => Value.ToString();
    }
}
