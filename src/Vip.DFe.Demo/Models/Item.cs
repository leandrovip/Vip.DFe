using System;
using System.Collections.Generic;

namespace Vip.DFe.Demo.Models
{
    public class Item
    {
        #region Propriedades

        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string NCM { get; set; }
        public string CEST { get; set; }
        public string Medida { get; set; }
        public bool ItemGLP { get; set; }
        public int CFOP { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorItem { get; set; }
        public decimal Desconto { get; set; }
        public decimal Frete { get; set; }
        public decimal Seguro { get; set; }
        public decimal Outros { get; set; }
        public decimal TotalItem => (Quantidade * ValorItem + Frete + Seguro + Outros - Desconto).Round();

        #endregion

        #region Construtores

        public Item(string codigo, string descricao, string ncm, string cest, string medida, bool itemGLP, int cfop, decimal valorItem, decimal quantidade, decimal desconto, decimal frete, decimal seguro, decimal outros)
        {
            Codigo = codigo;
            Descricao = descricao;
            NCM = ncm;
            CEST = cest;
            Medida = medida;
            ItemGLP = itemGLP;
            CFOP = cfop;
            ValorItem = valorItem;
            Quantidade = quantidade;
            Desconto = desconto;
            Frete = frete;
            Seguro = seguro;
            Outros = outros;
        }

        #endregion

        #region Métodos Estáticos

        private static readonly Random random = new Random();

        public static Item Obter()
        {
            return new Item(
                $"COD{random.Next(1000, 9999)}",
                $"Produto {random.Next(1, 100)}",
                _listaNCM[random.Next(_listaNCM.Count)],
                "",
                "UN",
                false,
                5102,
                Math.Round((decimal) (random.NextDouble() * 100), 4),
                Math.Round((decimal) (random.NextDouble() * 10), 4),
                Math.Round((decimal) (random.NextDouble() * 50), 2),
                Math.Round((decimal) (random.NextDouble() * 20), 2),
                Math.Round((decimal) (random.NextDouble() * 10), 2),
                Math.Round((decimal) (random.NextDouble() * 30), 2)
            );
        }

        public static List<Item> ObterLista(int quantidade = 10)
        {
            var itens = new List<Item>();
            for (var i = 1; i <= quantidade; i++) itens.Add(Obter());
            return itens;
        }

        private static readonly List<string> _listaNCM = new List<string>
        {
            "01012100", "02013000", "03022000", "04011010", "05021000",
            "06019090", "07096000", "08031000", "09012100", "10063021"
        };

        #endregion
    }
}