using System;

namespace DIO.Bank
{
    public class Conta
    {
        internal TipoConta TipoConta { get; set; }
        private decimal Saldo { get; set; }
        internal decimal Credito { get; set; }
        internal String Nome { get; set;}
        const decimal LimiteMinimoCredito = 300;
        const decimal LimiteMinimoDeposito = (decimal) 0.01;
        public Conta(TipoConta tipoConta, String nome)
        {
            this.TipoConta = tipoConta;
            this.Nome = nome;
            this.Saldo = 0;
            this.Credito = LimiteMinimoCredito;
        }
        public override string ToString()
        {
            return string.Format($"{this.TipoConta,-15} | {this.Nome,-30} | {this.Saldo,16:C2} | {this.Credito,16:C2} |");
        }
        public bool Depositar(decimal valorDeposito)
        {
            if(valorDeposito < LimiteMinimoDeposito)
            {
                return false;
            }
            else
            {
                this.Saldo += valorDeposito;
                return true;
            }
        }
        public bool Transferir(decimal valorTransferencia, Conta contaDestinataria)
        {
            return (this.Sacar(valorTransferencia)) ? contaDestinataria.Depositar(valorTransferencia) : false;
        }
        public bool Sacar(decimal valorSaque)
        {
            if(((this.Saldo + this.Credito) - valorSaque) < 0) 
            {
                return false;
            }
            else
            {
                this.Saldo -= valorSaque;
                return true;
            }
        }
    }
}
