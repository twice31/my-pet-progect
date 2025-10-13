namespace Domain.ExchangeRequest.VO
{
    public record RecipientId
    {
        public Guid Value { get; }

        private RecipientId(Guid value)
        {
            Value = value;
        }

        public static RecipientId Create(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Идентификатор получателя не может быть пустым.", nameof(value));

            return new RecipientId(value);
        }

        public override string ToString() => Value.ToString();
    }
}
