using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.Linq;

namespace Data.Converters
{
    public class StringListConverter : ValueConverter<IReadOnlyList<string>, string>
    {
        private const string Separator = "|";

        public StringListConverter()
            : base(
                v => string.Join(Separator, v),

                v => v.Split(new[] { Separator }, System.StringSplitOptions.RemoveEmptyEntries)
                      .ToList()
                      .AsReadOnly()
            )
        {
        }
    }
}