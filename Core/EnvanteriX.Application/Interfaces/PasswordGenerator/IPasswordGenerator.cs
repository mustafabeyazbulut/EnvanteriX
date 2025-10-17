namespace EnvanteriX.Application.Interfaces.PasswordGenerator
{
    public interface IPasswordGenerator
    {
        /// <summary>
        /// Güçlü rastgele parola üretir.
        /// </summary>
        /// <param name="length">Parolanın uzunluğu (default 16)</param>
        /// <returns>Oluşturulan parola</returns>
        string Generate(int length = 16);
    }
}
