using System;
using Xunit;

namespace DIO.Bank.test
{
    public class ContaUnitTest
    {
        String ClienteFicticio;
        TipoConta TipoFicticio;
        Conta ContaFicticia;
        public ContaUnitTest()
        {
            ClienteFicticio = "Cliente Ficticio";
            TipoFicticio = TipoConta.Fisica;
            ContaFicticia = new Conta(TipoFicticio,ClienteFicticio);
        }
        [Fact]
        public void Conta_Criacao_Sucesso()
        {
            Assert.NotNull(ContaFicticia);
        }
        [Fact]
        public void Depositar_ValorPositivo_RetornaVerdadeiro()
        {
            const decimal ValorPositivoMinimo = (decimal) 0.01;

            Assert.True(ContaFicticia.Depositar(ValorPositivoMinimo));
        }
        [Fact]
        public void Depositar_ValorNuloOuNegativo_RetornaNegativo()
        {
            const decimal ValorNulo = (decimal) 0;
            const decimal ValorNegativo = (decimal) -0.01;
            
            Assert.False(ContaFicticia.Depositar(ValorNulo));
            Assert.False(ContaFicticia.Depositar(ValorNegativo));
        }
    }
}
