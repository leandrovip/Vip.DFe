using System;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.NFe.NotaFiscal.Detalhe;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.Demo
{
    public class DadosProduto
    {
        private readonly TipoAmbiente _tipoAmbiente;
        private NFeModelo _modelo;

        public DadosProduto(TipoAmbiente tipoAmbiente, NFeModelo modelo)
        {
            _tipoAmbiente = tipoAmbiente;
            _modelo = modelo;
        }

        public NFeDetProduto GetProduto()
        {
            //int numeroItemBanco = 1;
            bool produtoIncideEscala = false;

            var prod = new NFeDetProduto();

            prod.Codigo = "1";

            /*
             * Em ambiente de Homolog,
             * APENAS O PRIMEIRO ITEM DA NF deve
             * estar descrito como NOTA EMITIDA EM HOMOLOGACAO
             * */
            //prod.xProd = (tipoAmbiente == TipoAmbiente.Homologacao && numeroItemBanco == 1
            //            ? "NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL"
            //            : "DESCRICAO DO PRODUTO");

            prod.XProd = "PRODUTO DE TESTEPRODUTO DE TESTEPRODUTO DE TESTEPRODUTO DE TESTEPRODUTO DE TESTEPRODUTO DE";

            prod.CFOP = 5102;
            prod.NCM = "22030000";
            prod.UCom = "UN";
            prod.UTrib = "UN";
            prod.QCom = 1;
            prod.QTrib = 1;
            prod.VUnCom = 100;
            prod.VUnTrib = 100;

            //RECOMENDAVEL QUE SEJA EXATAMENTE IGUAL ESTA LINHA
            prod.VProd = Math.Round(prod.VUnCom * prod.QCom, 3);
            prod.VDesc = 0;
            prod.VFrete = 0;
            prod.VOutro = 0;
            prod.VSeg = 0;
            prod.IndTot = NFeIndTotal.ValorItemCompoeTotalNota;
            prod.CEAN = "SEM GTIN";
            prod.CEANTrib = "SEM GTIN";
            prod.CBenef = "";

            if (produtoIncideEscala)
            {
                int indicadorEscala = 83; //vindo do banco
                prod.IndEscala = indicadorEscala == 83 ? NFeIndEscala.Sim : NFeIndEscala.Nao;

                string cnpjFabricante = "12345678900014"; //vindo do banco
                prod.CNPJFab = prod.IndEscala == NFeIndEscala.Nao ? cnpjFabricante : string.Empty;
            }

            string cest = ""; // vindo do banco;
            if (!string.IsNullOrEmpty(cest))
                prod.CEST = cest;

            return prod;
        }

        public NFeDetImposto GetImposto()
        {
            var imposto = new NFeDetImposto
            {
                Imposto = GetICMS(),
                Pis = GetPIS(),
                Cofins = GetCOFINS()
            };

            //if (_modelo == NFeModelo.NFe) imposto.Ipi = GetIPI();

            return imposto;
        }

        private Ipi GetIPI()
        {
            var ipi = new Ipi();
            ipi.Imposto = new IpiTrib
            {
                Cst = "53",
                VBc = 0,
                PIpi = 0,
                VIpi = 0
            };

            /*
             * Num cenário real, os dados abaixo
             * virão do banco de dados
             * 
             * Esta regra serve apenas de 
             * ilustração
             * */
            //if (cst == CSTIPI.ipi99)
            //    ipi.cEnq = 999;
            //else if (cst == CSTIPI.ipi50)
            //    ipi.cEnq = 999;
            //else if (cst == CSTIPI.ipi51)
            //    ipi.cEnq = 999;
            //else if (cst == CSTIPI.ipi53)
            //    ipi.cEnq = 999;

            return ipi;
        }

        private Cofins GetCOFINS()
        {
            var cofins = new CofinsNt {Cst = "09"};
            return new Cofins {Imposto = cofins};
        }

        private Pis GetPIS()
        {
            var pis = new PisNt {Cst = "09"};
            return new Pis {Imposto = pis};
        }

        private INFeImposto GetICMS()
        {
            bool isSimplesNacional = true;
            INFeIcms icms = null;

            if (isSimplesNacional)
                icms = GetIcmsSimplesNacional();
            else
                icms = GetIcmsRegimeNormal();

            return new Icms {Tipo = icms};
        }

        private INFeIcms GetIcmsRegimeNormal()
        {
            var icms = new Icms00();
            icms.Origem = OrigemMercadoria.Nacional;
            icms.ModBC = NFeModalidadeBC.MVA;
            icms.VBc = 100;
            icms.PIcms = 17.50m;
            icms.VIcms = 17.50m;
            return icms;
        }

        private INFeIcms GetIcmsSimplesNacional()
        {
            var icms = new IcmsSn102();
            icms.Csosn = "102";
            icms.Origem = OrigemMercadoria.Nacional;
            return icms;
        }
    }
}