using System;
using System.Globalization;
using System.Text;
using Vip.DFe.Extensions;
using Vip.DFe.SAT.Enum;
using Vip.DFe.SAT.Funcoes;

namespace Vip.DFe.SAT.Response
{
    public sealed class StatusOperacionalResponse : SatResponse
    {
        #region Constructors

        public StatusOperacionalResponse(string retorno, Encoding encoding) : base(retorno, encoding)
        {
            ConfigRede = new SatRede();
            Status = new SatStatus();

            if (CodigoDeRetorno != 10000) return;

            ConfigRede.TipoLan = RetornoLst[6] switch
            {
                "PPPoE" => TipoLan.PPPoE,
                "IPFIX" => TipoLan.IPFIX,
                _ => TipoLan.DHCP
            };

            ConfigRede.LanIp = RetornoLst[7];
            ConfigRede.LanMask = RetornoLst[9];
            ConfigRede.LanGateway = RetornoLst[10];
            ConfigRede.LanDNS1 = RetornoLst[11];
            ConfigRede.LanDNS2 = RetornoLst[12];

            Status.NSerie = RetornoLst[5];
            Status.LanMac = RetornoLst[8];
            Status.StatusLan = RetornoLst[13] switch
            {
                "NAO_CONECTADO" => StatusLan.NaoConectado,
                _ => StatusLan.Conectado,
            };
            Status.NivelBateria = RetornoLst[14] switch
            {
                "ALTO" => NivelBateria.Alto,
                "MEDIO" => NivelBateria.Medio,
                _ => NivelBateria.Baixo,
            };
            Status.MTTotal = RetornoLst[15];
            Status.MTUsada = RetornoLst[16];
            Status.DhAtual = DateTime.ParseExact(RetornoLst[17], "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            Status.VerSb = RetornoLst[18];
            Status.VerLayout = RetornoLst[19];
            Status.UltimoCFe = RetornoLst[20];
            Status.ListaInicial = RetornoLst[21];

            var i = 22;
            //Workaround para leitura de Status do Emulador do Fisco, que não retorna o campo: LISTA_FINAL
            if (RetornoLst.Count > 27)
            {
                Status.ListaFinal = RetornoLst[i];
                i++;
            }

            Status.DhCFe = DateTime.ParseExact(RetornoLst[i], "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            i++;
            Status.DhUltima = DateTime.ParseExact(RetornoLst[i], "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            i++;
            Status.CertEmissao = DateTime.ParseExact(RetornoLst[i], "yyyyMMdd", CultureInfo.InvariantCulture);
            i++;
            Status.CertVencimento = DateTime.ParseExact(RetornoLst[i], "yyyyMMdd", CultureInfo.InvariantCulture);
            i++;
            var retStr = RetornoLst[i];
            if (retStr.IsNumeric())
                Status.EstadoOperacao = (EstadoOperacao) retStr.ToInt32();
            else
                Status.EstadoOperacao = retStr switch
                {
                    "BLOQUEIO_SEFAZ" => EstadoOperacao.BloqueioSEFAZ,
                    "BLOQUEIO_CONTRIBUINTE" => EstadoOperacao.BloqueioContribuinte,
                    "BLOQUEIO_AUTONOMO" => EstadoOperacao.BloqueioAutonomo,
                    "BLOQUEIO_DESATIVACAO" => EstadoOperacao.BloqueioDesativacao,
                    _ => EstadoOperacao.Desbloqueado
                };
        }

        #endregion Constructors

        #region Propriedades

        public SatRede ConfigRede { get; set; }

        public SatStatus Status { get; set; }

        #endregion Propriedades
    }
}