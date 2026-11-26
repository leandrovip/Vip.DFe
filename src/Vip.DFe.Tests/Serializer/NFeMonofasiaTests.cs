using System;
using System.IO;
using System.Reflection;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Helpers;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs;
using Vip.DFe.NFe.NotaFiscal.Identificacao;
using Vip.DFe.NFe.NotaFiscal.Total;
using Xunit;
using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;

namespace Vip.DFe.Tests.Serializer
{
    /// <summary>
    ///     NT 2025.002-RTC v1.53 (PL_010h): monofasia (TMonofasia), Imposto Seletivo (TIS), crédito presumido (TCredPres),
    ///     ISUFEmit e NFeIbsCbsTot sem nulos.
    /// </summary>
    public class NFeMonofasiaTests
    {
        private static readonly string[] CaminhoMono = {"infNFe", "det", "imposto", "IBSCBS", "gIBSCBSMono"};

        private static NFeDoc NFeComMono(IbsCbsMono mono)
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].Imposto.IbsCbs = new IbsCbs {Cst = "620", CClassTrib = "620001", IbsCbsMono = mono};
            return nfe;
        }

        private static IbsCbsMono MonoIbsAdRemCbsAdValorem() => new IbsCbsMono
        {
            IbsMonoAdRem = new IbsMonoAdRem
            {
                MonoPadrao = new IbsAdRemPadrao {QbcMono = 10m, AdRemIbs = 1.5m, VIbsMono = 15m},
                MonoReten = new IbsAdRemReten {QbcMonoReten = 2m, AdRemIbsReten = 1.5m, VIbsMonoReten = 3m},
                MonoRet = new IbsMonoRet {VIbsMonoRet = 4m},
                BioDiferenca = new IbsBioDiferenca {QBcBioComb = 5m, VIbsDiferenca = 6m}
            },
            CbsMonoAdValorem = new CbsMonoAdValorem
            {
                MonoPadrao = new CbsAdValoremPadrao {VBcMono = 100m, PAliqMonoCbs = 0.9m, VCbsMono = 0.9m},
                MonoReten = new CbsAdValoremReten {VBcMonoReten = 50m, PAliqMonoReten = 0.9m, VCbsMonoReten = 0.45m},
                MonoRet = new CbsMonoRet {VCbsMonoRet = 7m},
                BioDiferenca = new CbsBioDiferenca {QBcBioComb = 8m, VCbsDiferenca = 9m}
            },
            VTotIbsMonoItem = 15m,
            VTotCbsMonoItem = 0.9m
        };

        private static IbsCbsMono MonoIbsAdValoremCbsAdRem() => new IbsCbsMono
        {
            IbsMonoAdValorem = new IbsMonoAdValorem
            {
                MonoPadrao = new IbsAdValoremPadrao
                {
                    VBcMono = 100m, PAliqMonoUF = 0.1m, VIbsMonoUF = 0.1m, PAliqMonoMun = 0.2m, VIbsMonoMun = 0.2m, VIbsMono = 0.3m
                },
                MonoReten = new IbsAdValoremReten {VBcMonoReten = 50m, PAliqMonoReten = 0.3m, VIbsMonoReten = 0.15m},
                MonoRet = new IbsMonoRet {VIbsMonoRet = 4m},
                BioDiferenca = new IbsBioDiferenca {QBcBioComb = 5m, VIbsDiferenca = 6m}
            },
            CbsMonoAdRem = new CbsMonoAdRem
            {
                MonoPadrao = new CbsAdRemPadrao {QbcMono = 10m, AdRemCbs = 1m, VCbsMono = 10m},
                MonoReten = new CbsAdRemReten {QbcMonoReten = 2m, AdRemCbsReten = 1m, VCbsMonoReten = 2m},
                MonoRet = new CbsMonoRet {VCbsMonoRet = 7m},
                BioDiferenca = new CbsBioDiferenca {QBcBioComb = 8m, VCbsDiferenca = 9m}
            },
            VTotIbsMonoItem = 0.3m,
            VTotCbsMonoItem = 10m
        };

        private static string[] Filhos(string xml, params string[] caminho) => SerializerTestHelper.NomesFilhos(SerializerTestHelper.Elemento(xml, caminho));

        private static string[] Caminho(params string[] extra)
        {
            var c = new string[CaminhoMono.Length + extra.Length];
            CaminhoMono.CopyTo(c, 0);
            extra.CopyTo(c, CaminhoMono.Length);
            return c;
        }

        private static string[] ValidarAssinada(NFeDoc nfe)
        {
#pragma warning disable CS0618
            using var cert = NFeValidacaoXsdTests.CriarCertificadoAutoassinado();
#pragma warning restore CS0618
            nfe.Assinar(cert, SaveOptions.DisableFormatting);
            SchemaHelper.ValidarXml(nfe.GetXml(), Path.Combine(SerializerTestHelper.CaminhoBase, "schemas", "nfe_v4.00.xsd"), out var erros, out _);
            return erros;
        }

        private static void AssertValido(NFeDoc nfe)
        {
            var erros = ValidarAssinada(nfe);

            Assert.True(erros.Length == 0, string.Join(Environment.NewLine, erros));
        }

        #region Monofasia

        [Fact]
        public void Monofasia_IbsAdRem_CbsAdValorem_DeveSerializarGruposNaOrdemDoXsd()
        {
            var xml = NFeComMono(MonoIbsAdRemCbsAdValorem()).GetXml();

            Assert.Equal(new[] {"gIBSMonoAdRem", "gCBSMonoAdValorem", "vTotIBSMonoItem", "vTotCBSMonoItem"}, Filhos(xml, CaminhoMono));
            Assert.Equal(new[] {"gMonoPadrao", "gMonoReten", "gMonoRet", "gpBioDiferenca"}, Filhos(xml, Caminho("gIBSMonoAdRem")));
            Assert.Equal(new[] {"qBCMono", "adRemIBS", "vIBSMono"}, Filhos(xml, Caminho("gIBSMonoAdRem", "gMonoPadrao")));
            Assert.Equal(new[] {"qBCMonoReten", "adRemIBSReten", "vIBSMonoReten"}, Filhos(xml, Caminho("gIBSMonoAdRem", "gMonoReten")));
            Assert.Equal(new[] {"vIBSMonoRet"}, Filhos(xml, Caminho("gIBSMonoAdRem", "gMonoRet")));
            Assert.Equal(new[] {"qBCBioComb", "vIBSDiferenca"}, Filhos(xml, Caminho("gIBSMonoAdRem", "gpBioDiferenca")));

            Assert.Equal(new[] {"gMonoPadrao", "gMonoReten", "gMonoRet", "gpBioDiferenca"}, Filhos(xml, Caminho("gCBSMonoAdValorem")));
            Assert.Equal(new[] {"vBCMono", "pAliqMonoCBS", "vCBSMono"}, Filhos(xml, Caminho("gCBSMonoAdValorem", "gMonoPadrao")));
            Assert.Equal(new[] {"vBCMonoReten", "pAliqMonoReten", "vCBSMonoReten"}, Filhos(xml, Caminho("gCBSMonoAdValorem", "gMonoReten")));
            Assert.Equal(new[] {"vCBSMonoRet"}, Filhos(xml, Caminho("gCBSMonoAdValorem", "gMonoRet")));
            Assert.Equal(new[] {"qBCBioComb", "vCBSDiferenca"}, Filhos(xml, Caminho("gCBSMonoAdValorem", "gpBioDiferenca")));
        }

        [Fact]
        public void Monofasia_IbsAdValorem_CbsAdRem_DeveSerializarGruposNaOrdemDoXsd()
        {
            var xml = NFeComMono(MonoIbsAdValoremCbsAdRem()).GetXml();

            Assert.Equal(new[] {"gIBSMonoAdValorem", "gCBSMonoAdRem", "vTotIBSMonoItem", "vTotCBSMonoItem"}, Filhos(xml, CaminhoMono));
            Assert.Equal(new[] {"vBCMono", "pAliqMonoUF", "vIBSMonoUF", "pAliqMonoMun", "vIBSMonoMun", "vIBSMono"},
                Filhos(xml, Caminho("gIBSMonoAdValorem", "gMonoPadrao")));
            Assert.Equal(new[] {"vBCMonoReten", "pAliqMonoReten", "vIBSMonoReten"}, Filhos(xml, Caminho("gIBSMonoAdValorem", "gMonoReten")));
            Assert.Equal(new[] {"vIBSMonoRet"}, Filhos(xml, Caminho("gIBSMonoAdValorem", "gMonoRet")));
            Assert.Equal(new[] {"qBCBioComb", "vIBSDiferenca"}, Filhos(xml, Caminho("gIBSMonoAdValorem", "gpBioDiferenca")));

            Assert.Equal(new[] {"qBCMono", "adRemCBS", "vCBSMono"}, Filhos(xml, Caminho("gCBSMonoAdRem", "gMonoPadrao")));
            Assert.Equal(new[] {"qBCMonoReten", "adRemCBSReten", "vCBSMonoReten"}, Filhos(xml, Caminho("gCBSMonoAdRem", "gMonoReten")));
            Assert.Equal(new[] {"vCBSMonoRet"}, Filhos(xml, Caminho("gCBSMonoAdRem", "gMonoRet")));
            Assert.Equal(new[] {"qBCBioComb", "vCBSDiferenca"}, Filhos(xml, Caminho("gCBSMonoAdRem", "gpBioDiferenca")));
        }

        [Fact]
        public void Monofasia_Valores_DevemUsarCasasDecimaisDoXsd()
        {
            var xml = NFeComMono(MonoIbsAdValoremCbsAdRem()).GetXml();

            Assert.Equal("0.1000", SerializerTestHelper.Elemento(xml, Caminho("gIBSMonoAdValorem", "gMonoPadrao", "pAliqMonoUF")).Value);
            Assert.Equal("0.30", SerializerTestHelper.Elemento(xml, Caminho("gIBSMonoAdValorem", "gMonoPadrao", "vIBSMono")).Value);
            Assert.Equal("5.0000", SerializerTestHelper.Elemento(xml, Caminho("gIBSMonoAdValorem", "gpBioDiferenca", "qBCBioComb")).Value);
            Assert.Equal("9.00", SerializerTestHelper.Elemento(xml, Caminho("gCBSMonoAdRem", "gpBioDiferenca", "vCBSDiferenca")).Value);
        }

        [Fact]
        public void Monofasia_IbsAdRem_CbsAdValorem_DeveSerValidaContraXsd() => AssertValido(NFeComMono(MonoIbsAdRemCbsAdValorem()));

        [Fact]
        public void Monofasia_IbsAdValorem_CbsAdRem_DeveSerValidaContraXsd() => AssertValido(NFeComMono(MonoIbsAdValoremCbsAdRem()));

        [Fact]
        public void Monofasia_DeveSerRecarregadaDoXml()
        {
            var nfe = NFeDoc.Load(NFeComMono(MonoIbsAdValoremCbsAdRem()).GetXml());

            var mono = nfe.InfNFe.Detalhe[0].Imposto.IbsCbs.IbsCbsMono;
            Assert.Equal(0.2m, mono.IbsMonoAdValorem.MonoPadrao.PAliqMonoMun);
            Assert.Equal(0.3m, mono.IbsMonoAdValorem.MonoReten.PAliqMonoReten);
            Assert.Equal(4m, mono.IbsMonoAdValorem.MonoRet.VIbsMonoRet);
            Assert.Equal(6m, mono.IbsMonoAdValorem.BioDiferenca.VIbsDiferenca);
            Assert.Equal(7m, mono.CbsMonoAdRem.MonoRet.VCbsMonoRet);
            Assert.Equal(8m, mono.CbsMonoAdRem.BioDiferenca.QBcBioComb);
        }

        [Fact]
        public void Monofasia_SemGruposOpcionais_NaoDeveEmitirGruposFilhos()
        {
            var mono = new IbsCbsMono
            {
                CbsMonoAdValorem = new CbsMonoAdValorem {MonoPadrao = new CbsAdValoremPadrao {VBcMono = 100m, PAliqMonoCbs = 0.9m, VCbsMono = 0.9m}},
                VTotCbsMonoItem = 0.9m
            };

            var xml = NFeComMono(mono).GetXml();

            Assert.Equal(new[] {"gCBSMonoAdValorem", "vTotIBSMonoItem", "vTotCBSMonoItem"}, Filhos(xml, CaminhoMono));
            Assert.Equal(new[] {"gMonoPadrao"}, Filhos(xml, Caminho("gCBSMonoAdValorem")));
            Assert.False(SerializerTestHelper.ContemTag(xml, "gpBioDiferenca"));
            Assert.False(SerializerTestHelper.ContemTag(xml, "gMonoRet"));
        }

        #endregion

        #region Imposto Seletivo

        private static NFeDoc NFeComIs(ImpostoSeletivo imposto)
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].Imposto.IS = imposto;
            return nfe;
        }

        [Fact]
        public void ImpostoSeletivo_SoComCstEClassTrib_NaoDeveEmitirBlocoNemVIS()
        {
            var xml = NFeComIs(new ImpostoSeletivo {CstIS = "000", CClassTribIS = "000001"}).GetXml();

            Assert.Equal(new[] {"CSTIS", "cClassTribIS"}, Filhos(xml, "infNFe", "det", "imposto", "IS"));
        }

        [Fact]
        public void ImpostoSeletivo_SoComCstEClassTrib_DeveSerValidoContraXsd() => AssertValido(NFeComIs(new ImpostoSeletivo {CstIS = "000", CClassTribIS = "000001"}));

        [Fact]
        public void ImpostoSeletivo_ComBase_DeveEmitirBlocoCompleto()
        {
            var xml = NFeComIs(new ImpostoSeletivo {CstIS = "000", CClassTribIS = "000001", VBcIS = 100m, PIs = 1m, VIS = 1m}).GetXml();

            Assert.Equal(new[] {"CSTIS", "cClassTribIS", "vBCIS", "pIS", "vIS"}, Filhos(xml, "infNFe", "det", "imposto", "IS"));
        }

        [Fact]
        public void ImpostoSeletivo_ComTodosOsOpcionais_DeveEmitirNaOrdemDoXsdEValidar()
        {
            var imposto = new ImpostoSeletivo
            {
                CstIS = "000", CClassTribIS = "000001", VBcIS = 100m, PIs = 1m, AdRemIS = 2m, UTrib = "KG", QTrib = 3m, VIS = 5m
            };
            var nfe = NFeComIs(imposto);

            Assert.Equal(new[] {"CSTIS", "cClassTribIS", "vBCIS", "pIS", "adRemIS", "uTrib", "qTrib", "vIS"},
                Filhos(nfe.GetXml(), "infNFe", "det", "imposto", "IS"));
            AssertValido(nfe);
        }

        [Fact]
        public void ImpostoSeletivo_ComBase_DeveSerValidoContraXsd() => AssertValido(NFeComIs(new ImpostoSeletivo {CstIS = "000", CClassTribIS = "000001", VBcIS = 100m, PIs = 1m, VIS = 1m}));

        [Fact]
        public void ImpostoSeletivo_UTribSemQTrib_NaoDeveEmitirNenhumDosDois()
        {
            var xml = NFeComIs(new ImpostoSeletivo {CstIS = "000", CClassTribIS = "000001", VBcIS = 100m, PIs = 1m, UTrib = "KG", VIS = 1m}).GetXml();

            Assert.Equal(new[] {"CSTIS", "cClassTribIS", "vBCIS", "pIS", "vIS"}, Filhos(xml, "infNFe", "det", "imposto", "IS"));
        }

        [Fact]
        public void ImpostoSeletivo_QTribSemUTrib_NaoDeveEmitirNenhumDosDois()
        {
            var xml = NFeComIs(new ImpostoSeletivo {CstIS = "000", CClassTribIS = "000001", VBcIS = 100m, PIs = 1m, QTrib = 3m, VIS = 1m}).GetXml();

            Assert.Equal(new[] {"CSTIS", "cClassTribIS", "vBCIS", "pIS", "vIS"}, Filhos(xml, "infNFe", "det", "imposto", "IS"));
        }

        #endregion

        #region Crédito presumido

        private static NFeDoc NFeComCredPres(CreditoPresumidoTributo ibs)
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].Imposto.IbsCbs.CreditoPresumidoOperacao = new CreditoPresumidoOperacao
            {
                VBcCredPres = 100m, CCredPres = "01", IbsCredPres = ibs
            };
            return nfe;
        }

        private static readonly string[] CaminhoCredPres = {"infNFe", "det", "imposto", "IBSCBS", "gCredPresOper", "gIBSCredPres"};

        [Fact]
        public void CreditoPresumido_ComCondSus_DeveEmitirSomenteVCredPresCondSus()
        {
            var xml = NFeComCredPres(new CreditoPresumidoTributo {PCredPres = 1m, VCredPres = 1m, VCredPresCondSus = 2m}).GetXml();

            Assert.Equal(new[] {"pCredPres", "vCredPresCondSus"}, Filhos(xml, CaminhoCredPres));
            Assert.Equal("2.00", SerializerTestHelper.Elemento(xml, CaminhoCredPres[0], CaminhoCredPres[1], CaminhoCredPres[2], CaminhoCredPres[3], CaminhoCredPres[4], CaminhoCredPres[5], "vCredPresCondSus").Value);
        }

        [Fact]
        public void CreditoPresumido_SemCondSus_DeveEmitirSomenteVCredPres()
        {
            var xml = NFeComCredPres(new CreditoPresumidoTributo {PCredPres = 1m, VCredPres = 1m}).GetXml();

            Assert.Equal(new[] {"pCredPres", "vCredPres"}, Filhos(xml, CaminhoCredPres));
        }

        [Fact]
        public void CreditoPresumido_ValorZero_DeveEmitirVCredPresZerado()
        {
            var xml = NFeComCredPres(new CreditoPresumidoTributo {PCredPres = 1m}).GetXml();

            Assert.Equal(new[] {"pCredPres", "vCredPres"}, Filhos(xml, CaminhoCredPres));
            Assert.Equal("0.00", SerializerTestHelper.Elemento(xml, CaminhoCredPres[0], CaminhoCredPres[1], CaminhoCredPres[2], CaminhoCredPres[3], CaminhoCredPres[4], CaminhoCredPres[5], "vCredPres").Value);
        }

        #endregion

        #region ISUFEmit e totais

        [Fact]
        public void Emit_ISUFEmit_DeveSerializarAposTpRegimeIbsCbs()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Emitente.TpRegimeIbsCbs = TipoRegimeIbsCbs.SimplesNacionalComRegimeRegular;
            nfe.InfNFe.Emitente.ISUFEmit = "123456789";

            var xml = nfe.GetXml();

            var filhos = Filhos(xml, "infNFe", "emit");
            Assert.Equal("tpRegimeIBSCBS", filhos[^2]);
            Assert.Equal("ISUFEmit", filhos[^1]);
            Assert.Equal("123456789", SerializerTestHelper.Elemento(xml, "infNFe", "emit", "ISUFEmit").Value);
            AssertValido(nfe);
        }

        [Fact]
        public void Emit_SemISUFEmit_NaoDeveEmitirTag()
        {
            var xml = SerializerTestHelper.CarregarNFeModelo().GetXml();

            Assert.False(SerializerTestHelper.ContemTag(xml, "ISUFEmit"));
        }

        [Fact]
        public void Ide_NFref_DevePermitirAte999Itens()
        {
            var attr = typeof(NFeIde).GetProperty("NFref")
                .GetCustomAttributes(typeof(DFeCollectionAttribute), false);

            Assert.Equal(999, ((DFeCollectionAttribute) attr[0]).MaxSize);
        }

        [Fact]
        public void IbsCbsTot_IbsEmitirComFilhosNulos_NaoDeveLancar()
        {
            var tot = new NFeIbsCbsTot {Ibs = new NFeIbsTot {IbsUf = null, IbsMunicipio = null, VIbs = 1m}};
            var m = typeof(NFeIbsCbsTot).GetMethod("ShouldSerializeIbs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.True((bool) m.Invoke(tot, null));

            tot.Ibs.VIbs = 0m;
            Assert.False((bool) m.Invoke(tot, null));
        }

        [Fact]
        public void IbsCbsTot_IbsSemObjetos_NaoDeveLancar()
        {
            var tot = new NFeIbsCbsTot {Ibs = new NFeIbsTot {IbsUf = null, IbsMunicipio = new NFeIbsMunicipioTot {VIbsMun = 2m}}};

            var m = typeof(NFeIbsCbsTot).GetMethod("ShouldSerializeIbs", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.True((bool) m.Invoke(tot, null));
        }

        [Fact]
        public void IbsCbsTot_Nulo_NaoDeveSerializarIbsNemCbs()
        {
            var tot = new NFeIbsCbsTot {Ibs = null, Cbs = null};
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;

            Assert.False((bool) typeof(NFeIbsCbsTot).GetMethod("ShouldSerializeIbs", flags).Invoke(tot, null));
            Assert.False((bool) typeof(NFeIbsCbsTot).GetMethod("ShouldSerializeCbs", flags).Invoke(tot, null));
        }

        #endregion
    }
}