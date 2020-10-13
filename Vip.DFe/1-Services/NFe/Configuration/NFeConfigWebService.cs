using System;
using System.Collections.Generic;
using System.Linq;
using Vip.DFe.Extensions;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.Configuration
{
    public sealed class NFeConfigWebService
    {
        #region Properties

        internal NFeConfig Parent { get; }
        public CodigoUF UF { get; set; }
        public List<NFeEndereco> Enderecos { get; set; }
        public TimeSpan? Timeout { get; set; }
        public int NumeroTentativas { get; set; }
        public int IntervaloTentativas { get; set; }

        #endregion

        #region Constructors

        internal NFeConfigWebService(NFeConfig parent)
        {
            Parent = parent;
            UF = CodigoUF.SP;
            Enderecos = new List<NFeEndereco>();
            Timeout = new TimeSpan(0, 5, 0);
            NumeroTentativas = 3;
            IntervaloTentativas = 1000;
        }

        #endregion

        #region Methods

        public void Configurar()
        {
            var valor = NFeEnderecoCollection.listaAutorizadora.FirstOrDefault(x => x.Item1 == UF.GetDescription() && x.Item4 == Parent.Modelo);
            var autorizadora = Parent.TipoEmissao != TipoEmissao.Normal ? valor?.Item3 : valor?.Item2;

            Enderecos = NFeEnderecoCollection.listaEnderecos
                .Where(x => x.Autorizadora == autorizadora &&
                            x.Modelo == Parent.Modelo &&
                            x.Versao == Parent.Versao &&
                            x.Ambiente == Parent.Ambiente)
                .ToList();
        }

        #endregion
    }
}