using EnvanteriX.Application.Interfaces.Enums;
using System.ComponentModel;
using System.Reflection;

namespace EnvanteriX.Persistence.Enums
{
    public class EnumHelpers : IEnumHelpers
    {
        public string GetDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? value.ToString();
        }

        public IEnumerable<(int Value, string Name, string Description)> GetEnumList<T>() where T : Enum
        {
            var type = typeof(T);
            foreach (var value in Enum.GetValues(type))
            {
                var name = value.ToString();
                var field = type.GetField(name);
                var attr = field?.GetCustomAttribute<DescriptionAttribute>();
                yield return ((int)value, name, attr?.Description ?? name);
            }
        }

        public T ParseEnum<T>(string value) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }
    }
}
