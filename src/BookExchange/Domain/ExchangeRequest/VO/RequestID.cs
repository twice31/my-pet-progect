namespace Domain.ExchangeRequest.VO
{
    public record RequestId
    {
        public Guid Value { get; }

        private RequestId(Guid value)
        {
            Value = value;
        }

        public static RequestId Create(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Идентификатор запроса не может быть пустым.", nameof(value));

            return new RequestId(value);
        }

        public override string ToString() => Value.ToString();
    }
}
