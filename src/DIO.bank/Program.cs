using System;
using System.Collections.Generic;

namespace DIO.Bank
{
    class CLI
    {
        static private List<Conta> Contas = new List<Conta>();
        static void Main(string[] args)
        {
            string opcaoUsuario;
            do
            {
                ListarOperacoes();
                opcaoUsuario = LerOpcao();
                ExecutarOperacao(opcaoUsuario);
            }
            while(opcaoUsuario != "E");

        }
        static private void ListarOperacoes()
        {
            Console.Clear();
            ExibirCabecalho("Operações");
            Console.WriteLine("  (L)  Listar contas");
            Console.WriteLine("  (C)  Criar nova conta");
            Console.WriteLine("  (T)  Transferir");
            Console.WriteLine("  (S)  Sacar");
            Console.WriteLine("  (D)  Depositar");
            Console.WriteLine("  (E)  Encerrar");
        }

        private static void ExibirCabecalho(string operacao)
        {
            const int TamanhoSeparador = 48;
            const int AlinhamentoEsquerdo = 3;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine();
            Console.WriteLine("".PadLeft(AlinhamentoEsquerdo)+"Banco Digital");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("".PadLeft(AlinhamentoEsquerdo) + operacao);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n".PadLeft(TamanhoSeparador,'-'));
        }

        static private string LerOpcao()
        {
            Console.WriteLine();
            Console.WriteLine("Informe a operação desejada:");
            return Console.ReadLine().ToString().ToUpper();
        }
        static private void ExecutarOperacao(string opcao)
        {
            Console.Clear();
            int NumeroConta;
            Conta ContaSelecionada;
            switch(opcao)
            {
                case "L":
                    if(ListarContas())
                    {
                        ExibirMensagem(TipoMensagem.Sucesso);
                    }
                    break;
                case "C":
                    CriarConta();
                    break;
                case "T":
                    ExibirCabecalho("Transferência");
                    
                    NumeroConta = RequisitarConta();
                    ContaSelecionada = SelecionarConta(NumeroConta);
                    NumeroConta = RequisitarConta();
                    Conta ContaDestino = SelecionarConta(NumeroConta);
                    TransferirRecursos(contaOrigem:ContaSelecionada, contaDestino:ContaDestino);
                    break;
                case "S":
                    ExibirCabecalho("Saque");

                    NumeroConta = RequisitarConta();
                    ContaSelecionada = SelecionarConta(NumeroConta);

                    if(SacarRecursos(ContaSelecionada))
                    {
                        ExibirMensagem(TipoMensagem.Sucesso);
                    }
                    else
                    {
                        ExibirMensagem(TipoMensagem.Falha);
                    }
                    break;

                case "D":
                    ExibirCabecalho("Depósito");

                    NumeroConta = RequisitarConta();
                    ContaSelecionada = SelecionarConta(NumeroConta);

                    if(DepositarRecursos(ContaSelecionada))
                    {
                        ExibirMensagem(TipoMensagem.Sucesso);
                    }
                    else
                    {
                        ExibirMensagem(TipoMensagem.Falha);
                    }
                    break;
            }
        }

        private static void ExibirMensagem(TipoMensagem mensagem)
        {
            switch (mensagem)
            {
                case TipoMensagem.Sucesso:
                    Console.WriteLine("\nOperação realizada com sucesso.");
                    break;
                case TipoMensagem.Falha:
                    Console.WriteLine("\nOperação não pôde ser concluída.");
                    break;
            }
            Console.WriteLine("Pressione [ENTER] para continuar...");
            Console.ReadLine();
            Console.Clear();
        }

        private static int RequisitarConta()
        {
            Console.WriteLine("Informe o número da conta:");
            return int.Parse(Console.ReadLine());
        }
        private static Conta SelecionarConta(int numeroConta)
        {
            try
            {
                return Contas[numeroConta];
            }
            catch (System.IndexOutOfRangeException)
            {
                throw;   
            }
        }
        private static bool DepositarRecursos(Conta conta)
        {
            bool OperacaoValida = true;

            decimal ValorDeposito;
            Console.WriteLine("Informe o valor:");
            OperacaoValida &= decimal.TryParse(Console.ReadLine(), out ValorDeposito);
            
            if (OperacaoValida)
            {
                return conta.Depositar(ValorDeposito);
            }
            return false;
        }

        private static bool SacarRecursos(Conta conta)
        {
            Console.WriteLine("Informe o valor do saque:");
            decimal Valor = decimal.Parse(Console.ReadLine());
           
            return conta.Sacar(Valor);
        }

        private static void TransferirRecursos(Conta contaOrigem, Conta contaDestino)
        {
            Console.WriteLine("Informe o valor a ser transferido:");
            decimal ValorTransferencia = decimal.Parse(Console.ReadLine());

            contaOrigem.Transferir(valorTransferencia: ValorTransferencia, contaDestinataria:contaDestino);
        }

        private static void CriarConta()
        {
            ExibirCabecalho("Abertura de Conta");
            
            Console.WriteLine("Informe o tipo de conta:");
            Console.WriteLine("1 - Pessoa Física");
            Console.WriteLine("2 - Pessoa Jurídica");
            int TipoConta = int.Parse(Console.ReadLine());
            
            Console.WriteLine("Informe o Nome do Cliente:");
            string NomeCliente = Console.ReadLine();
            
            Conta NovaConta = new Conta(
                tipoConta: (TipoConta) TipoConta,
                nome: NomeCliente
                );
            
            if (NovaConta != null)
            {
                Contas.Add(NovaConta);
                ExibirMensagem(TipoMensagem.Sucesso);
            }
        }

        private static bool ListarContas()
        {
            ExibirCabecalho("Relatório de Contas");
            if(Contas?.Count == 0)
            {
                Console.WriteLine("Nenhuma conta cadastrada");
                Console.ReadLine();
                return false;
            }
            Console.WriteLine("{0,-15} | {1,-30} | {2,16} | {3,16} |","Tipo de Conta","Nome do Cliente","Saldo em Conta","Limite de Crédito");
            foreach (var conta in Contas)
            {
                Console.WriteLine(conta);
            }
            return true;
        }
    }
}
