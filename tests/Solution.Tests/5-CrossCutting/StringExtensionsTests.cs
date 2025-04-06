using CrossCutting.Extensions;

namespace Solution.Tests._5_CrossCutting
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("11.222.333/0001-81")] // CNPJ válido com máscara
        [InlineData("11222333000181")]     // CNPJ válido sem máscara
        public void ValideCnpjString_Should_Return_True_For_Valid_CNPJ(string validCnpj)
        {
            // Act
            var result = validCnpj.ValideCnpjString();

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("11.222.333/0001-00")] // CNPJ com dígitos verificadores inválidos
        [InlineData("11111111111111")]     // CNPJ com todos os dígitos iguais
        [InlineData("1234567890")]         // CNPJ com tamanho incorreto
        [InlineData("")]                   // CNPJ vazio
        [InlineData(null)]                // CNPJ nulo
        [InlineData("abc.def.ghi/jkl-mn")]// CNPJ com letras
        public void ValideCnpjString_Should_Return_False_For_Invalid_CNPJ(string invalidCnpj)
        {
            // Act
            var result = invalidCnpj.ValideCnpjString();

            // Assert
            Assert.False(result);
        }
    }
}
