using System.Reflection;
using System.Linq;

namespace Domain.Enumerations
{
    // Абстрактный базовый класс для "Умных перечислений"
    public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
        where TEnum : Enumeration<TEnum>
    {
        // Свойства, которые будут унаследованы всеми "Умными перечислениями"
        public int Key { get; }
        public string Name { get; }

        // ЗАЩИЩЕННЫЙ КОНСТРУКТОР
        protected Enumeration(int key, string name)
        {
            Key = key;
            Name = name;
        }

        // КЭШ: Список всех экземпляров конкретного Enumeration
        private static readonly Dictionary<int, TEnum> _enums = GetAllEnums().ToDictionary(e => e.Key);

        // МЕТОД: Получить все экземпляры (для кэша)
        private static IEnumerable<TEnum> GetAllEnums()
        {
            // Получаем тип наследника (например, BookStatus)
            Type enumType = typeof(TEnum);

            // Получаем все публичные статические поля этого типа
            FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            // Возвращаем значения этих полей
            return fields.Select(f => f.GetValue(null)).Cast<TEnum>();
        }

        // ФАБРИЧНЫЙ МЕТОД: Получить экземпляр по Ключу
        public static TEnum FromKey(int key)
        {
            if (!_enums.TryGetValue(key, out TEnum? value))
            {
                throw new InvalidOperationException($"Ключ '{key}' не поддерживается типом {typeof(TEnum).Name}.");
            }

            return value;
        }

        // ФАБРИЧНЫЙ МЕТОД: Получить экземпляр по Имени
        public static TEnum FromName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Имя не может быть пустым.", nameof(name));
            }

            // Поиск без учета регистра
            TEnum? value = _enums.Values.SingleOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (value == null)
            {
                throw new InvalidOperationException($"Имя '{name}' не поддерживается типом {typeof(TEnum).Name}.");
            }

            return value;
        }


        // Сравнение по ключу (для быстрой работы)
        public bool Equals(Enumeration<TEnum>? other)
        {
            if (other is null) return false;
            if (GetType() != other.GetType()) return false;
            return Key.Equals(other.Key);
        }

        public override bool Equals(object? obj) => obj is Enumeration<TEnum> other && Equals(other);

        // Хеширование по ключу
        public override int GetHashCode() => Key.GetHashCode();

        // Отображение имени для читабельности
        public override string ToString() => Name;

        // Операторы сравнения
        public static bool operator ==(Enumeration<TEnum> a, Enumeration<TEnum> b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Enumeration<TEnum> a, Enumeration<TEnum> b) => !(a == b);
    }
}