namespace Domain.ExchangeRequest.VO
{
    public record RecipientId
    {
        public Guid Value { get; }

        public RecipientId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор получателя не может быть пустым.", nameof(value));
            }

            Value = value;
        }

        public override string ToString() => Value.ToString();
    }
}
