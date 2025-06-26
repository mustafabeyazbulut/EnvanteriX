namespace EnvanteriX.Application.Interfaces.Enums
{
    public interface IEnumHelpers
    {
        string GetDescription(Enum value);
        IEnumerable<(int Value, string Name, string Description)> GetEnumList<T>() where T : Enum;
        T ParseEnum<T>(string value) where T : Enum;
    }
}
