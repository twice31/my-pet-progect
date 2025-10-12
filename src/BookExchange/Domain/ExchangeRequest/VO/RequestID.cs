namespace Domain.ExchangeRequest.VO
{
    public record RequestId
    {
        public Guid Value { get; }

        public RequestId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор запроса не может быть пустым.", nameof(value));
            }

            Value = value;
        }

        public override string ToString() => Value.ToString();
    }
}
