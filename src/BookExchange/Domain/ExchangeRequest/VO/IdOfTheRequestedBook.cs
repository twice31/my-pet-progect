namespace Domain.ExchangeRequest.VO
{
    public record RequestedBookId
    {
        public Guid Value { get; }

        public RequestedBookId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор запрашиваемой книги не может быть пустым.", nameof(value));
            }

            Value = value;
        }

        public override string ToString() => Value.ToString();
    }
}
