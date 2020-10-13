using System;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.Attributes
{
    /// <summary>
    ///     Interface IDFeElement
    /// </summary>
    public abstract class DFeBaseAttribute : Attribute
    {
        #region Constructors

        protected DFeBaseAttribute()
        {
            Tipo = TipoCampo.Str;
            Id = "";
            Name = string.Empty;
            Min = 0;
            Max = 0;
            Ocorrencia = 0;
            Ordem = 0;
            Descricao = string.Empty;
        }

        #endregion Constructors

        #region Properties

        public TipoCampo Tipo { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Descricao { get; set; }

        public int Ordem { get; set; }

        public int Max { get; set; }

        public int Min { get; set; }

        public Ocorrencia Ocorrencia { get; set; }

        #endregion Properties
    }
}