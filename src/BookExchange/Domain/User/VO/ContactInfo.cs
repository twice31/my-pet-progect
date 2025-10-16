using System;

namespace Domain.User.VO
{
    // Объект значения для контактных данных пользователя
    public record ContactInfo
    {
        // Использование новых объектов-значений
        public UserPhone Phone { get; }
        public UserEmail Email { get; }

        // Закрытый конструктор теперь принимает UserPhone и UserEmail
        private ContactInfo(UserPhone phone, UserEmail email)
        {
            // Убрана валидация на null, т.к. она будет в Create
            Phone = phone;
            Email = email;
        }

        public static ContactInfo Create(string phoneValue, string emailValue)
        {
            // ВАЛИДАЦИЯ СТРОК ЧЕРЕЗ РЕГУЛЯРНЫЕ ВЫРАЖЕНИЯ ПРОИСХОДИТ ЗДЕСЬ

            // Создаем UserPhone
            var phone = UserPhone.Create(phoneValue);

            // Создаем UserEmail
            var email = UserEmail.Create(emailValue);

            // После успешного создания объектов-значений мы уверены, что строки валидны
            return new ContactInfo(phone, email);
        }

        public override string ToString() => $"{Phone.Value}, {Email.Value}";
    }
}