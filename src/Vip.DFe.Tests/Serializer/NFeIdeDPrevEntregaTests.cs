using System;
using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;
using Xunit;

namespace Vip.DFe.Tests.Serializer
{
    /// <summary>
    ///     B10a dPrevEntrega é TData (AAAA-MM-DD) no leiauteNFe_v4.00.xsd.
    ///     Regressão: com DateTimeOffset? a desserialização lançava ArgumentException (VipERP #896/#935).
    /// </summary>
    public class NFeIdeDPrevEntregaTests
    {
        private static string XmlComDPrevEntrega(string valor)
        {
            var xml = SerializerTestHelper.CarregarNFeModelo().GetXml();
            var pos = xml.IndexOf("<tpNF>", StringComparison.Ordinal);
            Assert.True(pos > 0, "Fixture sem <tpNF>.");
            return xml.Insert(pos, $"<dPrevEntrega>{valor}</dPrevEntrega>");
        }

        [Fact]
        public void NFe_Load_ComDPrevEntrega_DeveCarregarData()
        {
            var nfe = NFeDoc.Load(XmlComDPrevEntrega("2026-09-30"));

            Assert.Equal(new DateTime(2026, 9, 30), nfe.InfNFe.Ide.DPrevEntrega);
        }

        [Fact]
        public void NFeIde_DPrevEntrega_Preenchido_DeveSerializarNoFormatoDoXsd()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Ide.DPrevEntrega = new DateTime(2026, 9, 30);

            var dPrevEntrega = SerializerTestHelper.Elemento(nfe.GetXml(), "infNFe", "ide", "dPrevEntrega");

            Assert.NotNull(dPrevEntrega);
            Assert.Equal("2026-09-30", dPrevEntrega.Value);
        }

        [Fact]
        public void NFeIde_DPrevEntrega_Nulo_NaoDeveGerarTag()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Ide.DPrevEntrega = null;

            Assert.False(SerializerTestHelper.ContemTag(nfe.GetXml(), "dPrevEntrega"));
        }

        [Fact]
        public void NFe_DPrevEntrega_RoundTrip_DeveManterXml()
        {
            var xml = XmlComDPrevEntrega("2026-09-30");

            var recarregada = NFeDoc.Load(xml);

            Assert.Equal(SerializerTestHelper.Normalizar(xml), SerializerTestHelper.Normalizar(recarregada.GetXml()));
        }
    }
}
