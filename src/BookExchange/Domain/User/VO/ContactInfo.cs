namespace Domain.User.VO
{
    // Объект значения для контактных данных пользователя
    public record ContactInfo
    {
        public string Phone { get; }
        public string Email { get; }

        public ContactInfo(string phone, string email)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Телефон не может быть пустым или состоять только из пробелов.", nameof(phone));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Электронная почта не может быть пустой или состоять только из пробелов.", nameof(email));
            }

            Phone = phone;
            Email = email;
        }

        // Метод для отображения контактных данных
        public override string ToString() => $"{Phone}, {Email}";
    }
}
