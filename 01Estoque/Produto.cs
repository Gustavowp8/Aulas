using System;
using System.Globalization;

namespace Estoque
{
    class Produto
    {
        public string Nome;
        public double Preco;
        public int Quantidade;

        public double TotalEmEstoque()
        {
            return Preco * Quantidade;
        }

        public void AdicionarProduto(int quantidade)
        {
             Quantidade += quantidade;
        }

        public void RetirarProdutos(int quantidade){
             Quantidade -= quantidade;
        }

        public override string ToString()
        {
            return Nome
            + " Preço R$ "
            + Preco.ToString("F2", CultureInfo.InvariantCulture)
            + " Quantidade: "
            +Quantidade
            + " Total em estoque R$ "
            + TotalEmEstoque().ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}