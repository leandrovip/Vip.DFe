using System;
using System.IO;
using Vip.DFe.Danfe;
using Vip.DFe.Danfe.Modelo;
using Xunit;

namespace Vip.DFe.Tests.Danfe
{
    public class DanfeViewModelCreatorTests
    {
        private const string ChaveAcesso = "35160810873538000245550010000000111504749126";

        [Fact]
        public void DanfeViewModelCreator_CriarDoConteudoXml_ComNFeProc_DeveManterFluxoAtual()
        {
            var model = DanfeViewModel.CriarDoConteudoXml(ObterNFeProcXml());

            Assert.False(model.ModoEspelho);
            Assert.Equal("135260000000000 - 26/03/2026 10:15:00", model.ProtocoloAutorizacao);
            Assert.Equal(100, model.CodigoStatusReposta);
            Assert.Equal("Autorizado o uso da NF-e", model.DescricaoStatusReposta);
            Assert.Equal(ChaveAcesso, model.ChaveAcesso);
        }

        [Fact]
        public void DanfeViewModelCreator_CriarDoConteudoXml_ComNFe_DeveGerarModoEspelho()
        {
            var model = DanfeViewModel.CriarDoConteudoXml(ObterNFeXml());

            Assert.True(model.ModoEspelho);
            Assert.Null(model.ProtocoloAutorizacao);
            Assert.Null(model.CodigoStatusReposta);
            Assert.Null(model.DescricaoStatusReposta);
            Assert.Equal(ChaveAcesso, model.ChaveAcesso);
            Assert.Equal("EMITENTE TESTE LTDA", model.Emitente.RazaoSocial);
            Assert.Equal("DESTINATARIO TESTE LTDA", model.Destinatario.RazaoSocial);
            Assert.Single(model.Produtos);
            Assert.Equal(100D, model.CalculoImposto.ValorTotalNota);
        }

        [Fact]
        public void DanfeService_Gerar_ComModeloEspelho_DeveConcluirSemErro()
        {
            var model = DanfeViewModel.CriarDoConteudoXml(ObterNFeXml());

            using var danfe = new DanfeService(model);
            using var stream = new MemoryStream();

            danfe.Gerar();
            danfe.Salvar(stream);

            Assert.True(stream.Length > 0);
        }

        [Fact]
        public void DanfeViewModelCreator_CriarDoConteudoXml_ComRaizNaoSuportada_DeveRetornarMensagemClara()
        {
            var exception = Assert.Throws<System.Exception>(() => DanfeViewModel.CriarDoConteudoXml(@"<foo />"));

            Assert.Contains("Documento raiz 'foo' não suportado para geração da DANFE.", exception.Message);
        }

        private static string ObterNFeXml(bool incluirDeclaracao = true)
        {
            var xml = $@"
<NFe xmlns=""http://www.portalfiscal.inf.br/nfe"">
  <infNFe versao=""4.00"" Id=""NFe{ChaveAcesso}"">
    <ide>
      <cUF>35</cUF>
      <cNF>50474912</cNF>
      <natOp>VENDA DE MERCADORIA</natOp>
      <mod>55</mod>
      <serie>1</serie>
      <nNF>11</nNF>
      <dhEmi>2016-08-01T10:00:00-03:00</dhEmi>
      <tpNF>1</tpNF>
      <idDest>1</idDest>
      <cMunFG>3550308</cMunFG>
      <tpImp>1</tpImp>
      <tpEmis>1</tpEmis>
      <cDV>6</cDV>
      <tpAmb>1</tpAmb>
      <finNFe>1</finNFe>
      <indFinal>1</indFinal>
      <indPres>1</indPres>
      <procEmi>0</procEmi>
      <verProc>1.0.0</verProc>
    </ide>
    <emit>
      <CNPJ>10873538000245</CNPJ>
      <xNome>EMITENTE TESTE LTDA</xNome>
      <xFant>EMITENTE TESTE</xFant>
      <enderEmit>
        <xLgr>RUA TESTE</xLgr>
        <nro>100</nro>
        <xBairro>CENTRO</xBairro>
        <cMun>3550308</cMun>
        <xMun>SAO PAULO</xMun>
        <UF>SP</UF>
        <CEP>01001000</CEP>
        <cPais>1058</cPais>
        <xPais>BRASIL</xPais>
        <fone>1133334444</fone>
      </enderEmit>
      <IE>123456789</IE>
      <CRT>3</CRT>
    </emit>
    <dest>
      <CNPJ>27076697000130</CNPJ>
      <xNome>DESTINATARIO TESTE LTDA</xNome>
      <enderDest>
        <xLgr>AV DESTINO</xLgr>
        <nro>200</nro>
        <xBairro>BAIRRO</xBairro>
        <cMun>3550308</cMun>
        <xMun>SAO PAULO</xMun>
        <UF>SP</UF>
        <CEP>02002000</CEP>
        <cPais>1058</cPais>
        <xPais>BRASIL</xPais>
        <fone>1144445555</fone>
      </enderDest>
      <indIEDest>9</indIEDest>
      <email>destinatario@teste.com.br</email>
    </dest>
    <det nItem=""1"">
      <prod>
        <cProd>001</cProd>
        <cEAN>SEM GTIN</cEAN>
        <xProd>PRODUTO TESTE</xProd>
        <NCM>84713012</NCM>
        <CFOP>5102</CFOP>
        <uCom>UN</uCom>
        <qCom>1.0000</qCom>
        <vUnCom>100.0000</vUnCom>
        <vProd>100.00</vProd>
        <cEANTrib>SEM GTIN</cEANTrib>
        <uTrib>UN</uTrib>
        <qTrib>1.0000</qTrib>
        <vUnTrib>100.0000</vUnTrib>
        <indTot>1</indTot>
      </prod>
      <imposto>
        <vTotTrib>20.00</vTotTrib>
        <ICMS>
          <ICMS00>
            <orig>0</orig>
            <CST>00</CST>
            <modBC>3</modBC>
            <vBC>100.00</vBC>
            <pICMS>18.00</pICMS>
            <vICMS>18.00</vICMS>
          </ICMS00>
        </ICMS>
        <PIS>
          <PISNT>
            <CST>07</CST>
          </PISNT>
        </PIS>
        <COFINS>
          <COFINSNT>
            <CST>07</CST>
          </COFINSNT>
        </COFINS>
      </imposto>
      <infAdProd>DETALHE ADICIONAL</infAdProd>
    </det>
    <total>
      <ICMSTot>
        <vBC>100.00</vBC>
        <vICMS>18.00</vICMS>
        <vICMSDeson>0.00</vICMSDeson>
        <vFCP>0.00</vFCP>
        <vBCST>0.00</vBCST>
        <vST>0.00</vST>
        <vFCPST>0.00</vFCPST>
        <vFCPSTRet>0.00</vFCPSTRet>
        <vProd>100.00</vProd>
        <vFrete>0.00</vFrete>
        <vSeg>0.00</vSeg>
        <vDesc>0.00</vDesc>
        <vII>0.00</vII>
        <vIPI>0.00</vIPI>
        <vIPIDevol>0.00</vIPIDevol>
        <vPIS>0.00</vPIS>
        <vCOFINS>0.00</vCOFINS>
        <vOutro>0.00</vOutro>
        <vNF>100.00</vNF>
        <vTotTrib>20.00</vTotTrib>
      </ICMSTot>
    </total>
    <transp>
      <modFrete>9</modFrete>
    </transp>
    <infAdic>
      <infCpl>OBS TESTE</infCpl>
      <infAdFisco>FISCO TESTE</infAdFisco>
    </infAdic>
  </infNFe>
</NFe>";

            if (incluirDeclaracao)
                return "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + xml;

            return xml;
        }

        private static string ObterNFeProcXml()
        {
            return $@"<?xml version=""1.0"" encoding=""utf-8""?>
<nfeProc xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""4.00"">
  {ObterNFeXml(false)}
  <protNFe versao=""4.00"">
    <infProt>
      <tpAmb>1</tpAmb>
      <verAplic>SVRS202600000000</verAplic>
      <chNFe>{ChaveAcesso}</chNFe>
      <dhRecbto>2026-03-26T10:15:00-03:00</dhRecbto>
      <nProt>135260000000000</nProt>
      <digVal>AAAAAAAAAAAAAAAAAAAAAAAAAAAA</digVal>
      <cStat>100</cStat>
      <xMotivo>Autorizado o uso da NF-e</xMotivo>
    </infProt>
  </protNFe>
</nfeProc>";
        }
    }
}
