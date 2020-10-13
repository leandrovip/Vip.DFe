using System;
using Vip.DFe.SAT.Enum;

namespace Vip.DFe.SAT.Funcoes
{
    public class SatStatus
    {
        public string NSerie { get; set; }
        public string LanMac { get; set; }
        public StatusLan StatusLan { get; set; }
        public NivelBateria NivelBateria { get; set; }
        public string MTTotal { get; set; }
        public string MTUsada { get; set; }
        public DateTime DhAtual { get; set; }
        public string VerSb { get; set; }
        public string VerLayout { get; set; }
        public string UltimoCFe { get; set; }
        public string ListaInicial { get; set; }
        public string ListaFinal { get; set; }
        public DateTime DhCFe { get; set; }
        public DateTime DhUltima { get; set; }
        public DateTime CertEmissao { get; set; }
        public DateTime CertVencimento { get; set; }
        public EstadoOperacao EstadoOperacao { get; set; }
    }
}