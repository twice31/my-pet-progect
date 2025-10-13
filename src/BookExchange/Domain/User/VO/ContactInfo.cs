namespace Domain.User.VO
{
    // Объект значения для контактных данных пользователя
    public record ContactInfo
    {
        public string Phone { get; }
        public string Email { get; }

        // Закрытый конструктор
        private ContactInfo(string phone, string email)
        {
            Phone = phone;
            Email = email;
        }

        // Фабричный метод для создания объекта
        public static ContactInfo Create(string phone, string email)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Телефон не может быть пустым или состоять только из пробелов.", nameof(phone));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Электронная почта не может быть пустой или состоять только из пробелов.", nameof(email));
            }

            return new ContactInfo(phone, email);
        }

        public override string ToString() => $"{Phone}, {Email}";
    }
}
