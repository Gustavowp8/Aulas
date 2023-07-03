using System;
using System.Globalization;

namespace Estoque
{ 
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Estoque");
            Console.WriteLine();

            Produto p = new Produto();

            Console.WriteLine("Entre com os dados do produto");

            Console.Write("Nome: ");
            p.Nome = Console.ReadLine();

            Console.Write("Preço: ");
            p.Preco = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.Write("Quantidade: ");
            p.Quantidade = int.Parse(Console.ReadLine());

            Console.WriteLine("Dados do Produto: " + p);
            Console.WriteLine();

           Console.Write("Quantidade a adicionar: ");
            int qte = int.Parse(Console.ReadLine());
            p.AdicionarProduto(qte);
            Console.WriteLine();

            Console.WriteLine("Dados atualizados do Produto: " + p);
            Console.WriteLine();

            Console.Write("Quantidade a ser retirado: ");
            qte = int.Parse(Console.ReadLine());
            p.RetirarProdutos(qte);
            Console.WriteLine();

            Console.WriteLine("Dados atualizados do Produto: " + p);
            Console.WriteLine();
        }
    }
}