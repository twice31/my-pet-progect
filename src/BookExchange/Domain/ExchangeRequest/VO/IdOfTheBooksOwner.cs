namespace Domain.ExchangeRequest.VO
{
    public record BookOwnerId
    {
        public Guid Value { get; }

        public BookOwnerId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор владельца книги не может быть пустым.", nameof(value));
            }

            Value = value;
        }

        public override string ToString() => Value.ToString();
    }
}
