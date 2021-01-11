using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using org.pdfclown.documents.contents.entities;
using org.pdfclown.documents.contents.fonts;
using org.pdfclown.documents.contents.xObjects;
using org.pdfclown.files;
using org.pdfclown.objects;
using Vip.DFe.Danfe.Blocos;
using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using File = org.pdfclown.files.File;

namespace Vip.DFe.Danfe
{
    public class DanfeService : IDisposable
    {
        #region Fields

        private readonly File _file;
        private bool _FoiGerado;
        private XObject _LogoObject;
        internal BlocoIdentificacaoEmitente _identificacaoEmitente;

        private readonly StandardType1Font _fonteRegular;
        private readonly StandardType1Font _fonteNegrito;
        private readonly StandardType1Font _fonteItalico;

        #endregion

        #region Constructors

        public DanfeService(DanfeViewModel viewModel)
        {
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            Blocos = new List<BlocoBase>();
            _file = new File();
            PdfDocument = _file.Document;

            // De acordo com o item 7.7, a fonte deve ser Times New Roman ou Courier New.
            var fonteFamilia = StandardType1Font.FamilyEnum.Times;
            _fonteRegular = new StandardType1Font(PdfDocument, fonteFamilia, false, false);
            _fonteNegrito = new StandardType1Font(PdfDocument, fonteFamilia, true, false);
            _fonteItalico = new StandardType1Font(PdfDocument, fonteFamilia, false, true);

            EstiloPadrao = CriarEstilo();

            Paginas = new List<DanfePagina>();
            Canhoto = CriarBloco<BlocoCanhoto>();
            _identificacaoEmitente = AdicionarBloco<BlocoIdentificacaoEmitente>();
            AdicionarBloco<BlocoDestinatarioRemetente>();

            if (ViewModel.LocalRetirada != null && ViewModel.ExibirBlocoLocalRetirada)
                AdicionarBloco<BlocoLocalRetirada>();

            if (ViewModel.LocalEntrega != null && ViewModel.ExibirBlocoLocalEntrega)
                AdicionarBloco<BlocoLocalEntrega>();

            if (ViewModel.Duplicatas.Count > 0)
                AdicionarBloco<BlocoDuplicataFatura>();

            AdicionarBloco<BlocoCalculoImposto>(ViewModel.Orientacao == Orientacao.Paisagem ? EstiloPadrao : CriarEstilo(4.75F));
            AdicionarBloco<BlocoTransportador>();
            AdicionarBloco<BlocoDadosAdicionais>(CriarEstilo(tFonteCampoConteudo: 8));

            if (ViewModel.CalculoIssqn.Mostrar)
                AdicionarBloco<BlocoCalculoIssqn>();

            AdicionarMetadata();

            _FoiGerado = false;
        }

        #endregion

        #region Properties

        internal org.pdfclown.documents.Document PdfDocument { get; private set; }
        internal BlocoCanhoto Canhoto { get; private set; }
        internal List<BlocoBase> Blocos { get; private set; }
        internal Estilo EstiloPadrao { get; private set; }
        internal List<DanfePagina> Paginas { get; private set; }

        public DanfeViewModel ViewModel { get; private set; }

        #endregion

        #region Methods

        public void Gerar()
        {
            if (_FoiGerado) throw new InvalidOperationException("O Danfe já foi gerado.");

            _identificacaoEmitente.Logo = _LogoObject;
            var tabela = new TabelaProdutosServicos(ViewModel, EstiloPadrao);

            while (true)
            {
                var page = CriarPagina();

                tabela.SetPosition(page.RetanguloCorpo.Location);
                tabela.SetSize(page.RetanguloCorpo.Size);
                tabela.Draw(page.Gfx);

                page.Gfx.Stroke();
                page.Gfx.Flush();

                if (tabela.CompletamenteDesenhada) break;
            }

            PreencherNumeroFolhas();
            _FoiGerado = true;
        }

        public void Salvar(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException(nameof(path));

            _file.Save(path, SerializationModeEnum.Incremental);
        }

        public void Salvar(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            _file.Save(new org.pdfclown.bytes.Stream(stream), SerializationModeEnum.Incremental);
        }

        public byte[] ObterPdfBytes(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            var pdfStrean = new org.pdfclown.bytes.Stream(stream);
            _file.Save(pdfStrean, SerializationModeEnum.Incremental);
            return pdfStrean.ToByteArray();
        }

        public void AdicionarLogoImagem(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var img = Image.Get(stream);
            if (img == null) throw new InvalidOperationException("O logotipo não pode ser carregado, certifique-se que a imagem esteja no formato JPEG não progressivo.");
            _LogoObject = img.ToXObject(PdfDocument);
        }

        public void AdicionarLogoPdf(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            using var pdfFile = new File(new org.pdfclown.bytes.Stream(stream));
            _LogoObject = pdfFile.Document.Pages[0].ToXObject(PdfDocument);
        }

        public void AdicionarLogoImagem(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException(nameof(path));

            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            AdicionarLogoImagem(fs);
        }

        public void AdicionarLogoPdf(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException(nameof(path));

            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            AdicionarLogoPdf(fs);
        }

        #endregion

        #region Private Methods

        private DanfePagina CriarPagina()
        {
            var page = new DanfePagina(this);
            Paginas.Add(page);
            page.DesenharBlocos(Paginas.Count == 1);
            page.DesenharCreditos();

            if (ViewModel.TipoAmbiente == 2)
                page.DesenharAvisoHomologacao();

            return page;
        }

        private void AdicionarMetadata()
        {
            var info = PdfDocument.Information;
            info[new PdfName("ChaveAcesso")] = ViewModel.ChaveAcesso;
            info[new PdfName("TipoDocumento")] = "DANFE";
            info.CreationDate = DateTime.Now;
            info.Creator = $"Vip.DFe {Assembly.GetExecutingAssembly().GetName().Version} - https://github.com/leandrovip/Vip.DFe";
            info.Title = "DANFE (Documento auxiliar da NFe)";
        }

        private Estilo CriarEstilo(float tFonteCampoCabecalho = 6, float tFonteCampoConteudo = 10) => new Estilo(_fonteRegular, _fonteNegrito, _fonteItalico, tFonteCampoCabecalho, tFonteCampoConteudo);

        #endregion

        #region Internal Methods

        internal T CriarBloco<T>() where T : BlocoBase => (T) Activator.CreateInstance(typeof(T), ViewModel, EstiloPadrao);

        internal T CriarBloco<T>(Estilo estilo) where T : BlocoBase => (T) Activator.CreateInstance(typeof(T), ViewModel, estilo);

        internal T AdicionarBloco<T>() where T : BlocoBase
        {
            var bloco = CriarBloco<T>();
            Blocos.Add(bloco);
            return bloco;
        }

        internal T AdicionarBloco<T>(Estilo estilo) where T : BlocoBase
        {
            var bloco = CriarBloco<T>(estilo);
            Blocos.Add(bloco);
            return bloco;
        }

        internal void AdicionarBloco(BlocoBase bloco)
        {
            Blocos.Add(bloco);
        }

        internal void PreencherNumeroFolhas()
        {
            int nFolhas = Paginas.Count;
            for (int i = 0; i < Paginas.Count; i++) Paginas[i].DesenhaNumeroPaginas(i + 1, nFolhas);
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) _file.Dispose();
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}