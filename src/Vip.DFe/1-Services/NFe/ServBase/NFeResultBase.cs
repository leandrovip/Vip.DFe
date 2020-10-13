using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.ServBase
{
    public class NFeResultBase<T> : DFeDocument<T> where T : class
    {
        #region Properties

        [DFeAttribute(TipoCampo.Enum, "versao", Min = 1, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeVersao Versao { get; set; }

        [DFeElement(TipoCampo.Enum, "tpAmb", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public TipoAmbiente Ambiente { get; set; }

        [DFeElement(TipoCampo.Str, "verAplic", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string VersaoAplicacao { get; set; }

        [DFeElement(TipoCampo.Int, "cStat", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CStat { get; set; }

        [DFeElement(TipoCampo.Str, "xMotivo", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Motivo { get; set; }

        [DFeElement(TipoCampo.Enum, "cUF", Min = 1, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CodigoUF UF { get; set; }

        #endregion Properties
    }
}