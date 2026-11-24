using System;
using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;
using Vip.DFe.NFe.NotaFiscal.Total;
using Xunit;

namespace Vip.DFe.Tests.Serializer
{
    public class NFeIssqnTotDCompetTests
    {
        private static NFeDoc CriarNFeComIssqn(DateTime dCompet)
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Total.IssqnTot = new NFeIssqnTot {VServ = 10m, VBc = 10m, VIss = 0.5m, DCompet = dCompet};
            return nfe;
        }

        [Fact]
        public void NFeIssqnTot_DCompet_DeveSerializarNoFormatoDoXsd()
        {
            var xml = CriarNFeComIssqn(new DateTime(2026, 3, 5)).GetXml();

            var dCompet = SerializerTestHelper.Elemento(xml, "infNFe", "total", "ISSQNtot", "dCompet");

            Assert.NotNull(dCompet);
            Assert.Equal("2026-03-05", dCompet.Value);
        }

        [Fact]
        public void NFeIssqnTot_DCompet_RoundTrip_DeveManterData()
        {
            var xml = CriarNFeComIssqn(new DateTime(2026, 12, 31)).GetXml();

            var recarregada = NFeDoc.Load(xml);

            Assert.Equal(new DateTime(2026, 12, 31), recarregada.InfNFe.Total.IssqnTot.DCompet.Date);
            Assert.Equal(SerializerTestHelper.Normalizar(xml), SerializerTestHelper.Normalizar(recarregada.GetXml()));
        }

        [Fact]
        public void NFeIssqnTot_DCompet_ComHorario_DeveSerializarSomenteData()
        {
            var xml = CriarNFeComIssqn(new DateTime(2026, 3, 5, 14, 30, 0)).GetXml();

            var dCompet = SerializerTestHelper.Elemento(xml, "infNFe", "total", "ISSQNtot", "dCompet");

            Assert.Equal("2026-03-05", dCompet.Value);
        }
    }
}
