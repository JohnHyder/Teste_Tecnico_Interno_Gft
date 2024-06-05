using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria {
        public ContaBancaria(int numero, string titular, double depositoInicial)
        {
            this.numeroDaConta = numero;
            this.titular = titular;
            this.saldo = depositoInicial;
        }

        public int numeroDaConta { get; set; }
        public string titular { get; set; }
        public double saldo { get; set; }

        public void Deposito(double valor) 
        {
            saldo += valor;
        }

        public void Saque(double valor) {
            if(saldo > 0) 
            {
                saldo -= (valor + 3.50);
            }
            else
            {
                Console.WriteLine("Saldo insuficiente para saque.");
            }
        }
    }
}
