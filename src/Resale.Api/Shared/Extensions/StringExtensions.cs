using System.Text.RegularExpressions;

namespace Resales.Api.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool ValideCnpjString(this string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            cnpj = Regex.Replace(cnpj, @"\D", "");

            if (cnpj.Length != 14)
                return false;

            if (cnpj.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj[..12];
            int soma = tempCnpj
                .Select((t, i) => (t - '0') * multiplicador1[i])
                .Sum();

            int resto = soma % 11;
            int primeiroDigito = resto < 2 ? 0 : 11 - resto;

            tempCnpj += primeiroDigito;

            soma = tempCnpj
                .Select((t, i) => (t - '0') * multiplicador2[i])
                .Sum();

            resto = soma % 11;
            int segundoDigito = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith($"{primeiroDigito}{segundoDigito}");
        }
    }
}
