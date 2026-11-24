using System.Collections.Generic;
using System.Linq;
using Vip.DFe.NFe;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Shared.Enum;
using Xunit;

namespace Vip.DFe.Tests.NFe.Configuration
{
    public class NFeConfigTests
    {
        [Fact]
        public void NFeConfigTests_ConfiguracaoWebservice_DeveRetornarRegistrosValidosParaProducaoEHomologacao()
        {
            // Arrange
            var serviceNFe = new NFeService();
            serviceNFe.Configuracoes.Modelo = NFeModelo.NFe;
            serviceNFe.Configuracoes.TipoEmissao = TipoEmissao.Normal;
            serviceNFe.Configuracoes.Versao = NFeVersao.v400;

            // Act
            serviceNFe.Configuracoes.Validar();

            // Assert
            Assert.NotEmpty(serviceNFe.Configuracoes.Webservices.Enderecos);
        }

        public static IEnumerable<object[]> CombinacoesUfModeloAmbiente()
        {
            // AN, EX e SU nao sao UFs autorizadoras de NFe/NFCe no cadastro de enderecos.
            var naoUfs = new[] {CodigoUF.AN, CodigoUF.EX, CodigoUF.SU};

            foreach (var uf in System.Enum.GetValues(typeof(CodigoUF)).Cast<CodigoUF>().Except(naoUfs))
            foreach (var modelo in new[] {NFeModelo.NFe, NFeModelo.NFCe})
            foreach (var ambiente in new[] {TipoAmbiente.Homologacao, TipoAmbiente.Producao})
            {
                // RS NFCe em homologacao nao possui enderecos de servico cadastrados (somente producao).
                if (uf == CodigoUF.RS && modelo == NFeModelo.NFCe && ambiente == TipoAmbiente.Homologacao) continue;

                yield return new object[] {uf, modelo, ambiente};
            }
        }

        [Theory]
        [MemberData(nameof(CombinacoesUfModeloAmbiente))]
        public void NFeConfigTests_Validar_DeveRetornarServicosEssenciaisParaUfModeloEAmbiente(CodigoUF uf, NFeModelo modelo, TipoAmbiente ambiente)
        {
            // Arrange
            var serviceNFe = new NFeService();
            serviceNFe.Configuracoes.Modelo = modelo;
            serviceNFe.Configuracoes.Ambiente = ambiente;
            serviceNFe.Configuracoes.TipoEmissao = TipoEmissao.Normal;
            serviceNFe.Configuracoes.Versao = NFeVersao.v400;
            serviceNFe.Configuracoes.Webservices.UF = uf;

            // Act
            serviceNFe.Configuracoes.Validar();

            // Assert
            var servicos = serviceNFe.Configuracoes.Webservices.Enderecos.Select(x => x.TipoServico).ToList();
            Assert.Contains(NFeTipoServico.NFeAutorizacao, servicos);
            Assert.Contains(NFeTipoServico.NFeRetAutorizacao, servicos);
            Assert.Contains(NFeTipoServico.NfeConsultaProtocolo, servicos);
            Assert.Contains(NFeTipoServico.NfeStatusServico, servicos);
            Assert.Contains(NFeTipoServico.RecepcaoEvento, servicos);
        }
    }
}