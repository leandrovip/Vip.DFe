using System.Xml.Linq;
using SaveOptions = Vip.DFe.Enum.SaveOptions;
using Xunit;
using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;

namespace Vip.DFe.Tests.Serializer
{
    public class NFeLoadFormatadoTests
    {
        [Fact]
        public void NFe_Load_XmlFormatado_DevePreservarItens()
        {
            var original = SerializerTestHelper.CarregarNFeModelo();
            var formatado = XDocument.Parse(original.GetXml()).ToString();

            var nfe = NFeDoc.Load(formatado);

            Assert.Equal(original.InfNFe.Detalhe.Count, nfe.InfNFe.Detalhe.Count);
            Assert.True(SerializerTestHelper.ContemTag(nfe.GetXml(SaveOptions.DisableFormatting | SaveOptions.OmitDeclaration), "det"));
        }

        [Fact]
        public void NFe_Load_XmlNaoFormatado_DevePreservarItens()
        {
            var original = SerializerTestHelper.CarregarNFeModelo();

            var nfe = NFeDoc.Load(original.GetXml());

            Assert.Equal(original.InfNFe.Detalhe.Count, nfe.InfNFe.Detalhe.Count);
        }
    }
}
