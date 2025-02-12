using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;
using Vip.DFe.Helpers;
using Vip.DFe.NFe.Enum;

namespace Vip.DFe.NFe.NotaFiscal
{
    public sealed class NFeCollection
    {
        #region Fields

        private readonly DFeCollection<NFe> _nfes;

        #endregion Fields

        #region Constructors

        internal NFeCollection(NFeService parent)
        {
            Parent = parent;
            _nfes = new DFeCollection<NFe>();
        }

        #endregion Constructors

        #region Properties

        public NFeService Parent { get; }
        public NFe[] NFe => _nfes.ToArray();

        #endregion Properties

        #region Methods

        /// <summary>
        ///     Adiciona um objeto ao final do <see cref="T:System.Collections.Generic.List`1" />.
        /// </summary>
        /// <returns>
        ///     <see cref="T:NFe" />
        /// </returns>
        public NFe AddNewNFe()
        {
            var instance = _nfes.AddNew();
            instance.InfNFe.Versao = Parent.Configuracoes.Versao;
            instance.InfNFe.Ide.Modelo = Parent.Configuracoes.Modelo;

            return instance;
        }

        /// <summary>
        ///     Adiciona um objeto ao final do <see cref="T:System.Collections.Generic.List`1" />.
        /// </summary>
        /// <param name="item">
        ///     O objeto a ser adicionado ao final do <see cref="T:System.Collections.Generic.List`1" />.
        ///     O valor pode ser <see langword="null" /> para tipos de referência.
        /// </param>
        public void Add(NFe item)
        {
            _nfes.Add(item);
        }

        /// <summary>
        ///     Adiciona os elementos da coleção especificada ao final do <see cref="T:System.Collections.Generic.List`1" />.
        /// </summary>
        /// <param name="collection">
        ///     A coleção cujos elementos devem ser adicionados ao final do <see cref="T:System.Collections.Generic.List`1" />.
        ///     A coleção em si não pode ser <see langword="null" />, mas pode conter elementos que são <see langword="null" />, se
        ///     o tipo <paramref name="T" /> é um tipo de referência.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="collection" /> é <see langword="null" />.
        /// </exception>
        public void AddRange(IEnumerable<NFe> collection)
        {
            _nfes.AddRange(collection);
        }

        /// <summary>
        ///     Determina se um elemento está no <see cref="T:System.Collections.Generic.List`1" />.
        /// </summary>
        /// <param name="item">
        ///     O objeto a ser localizado no <see cref="T:System.Collections.Generic.List`1" />.
        ///     O valor pode ser <see langword="null" /> para tipos de referência.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> se <paramref name="item" /> for encontrado no
        ///     <see cref="T:System.Collections.Generic.List`1" />; caso contrário, <see langword="false" />.
        /// </returns>
        public bool Contains(NFe item) => _nfes.Contains(item);

        /// <summary>
        ///     Pesquisa o objeto especificado e retorna o índice baseado em zero da primeira ocorrência dentro de todo o
        ///     <see cref="T:System.Collections.Generic.List`1" />.
        /// </summary>
        /// <param name="item">
        ///     O objeto a ser localizado no <see cref="T:System.Collections.Generic.List`1" />.
        ///     O valor pode ser <see langword="null" /> para tipos de referência.
        /// </param>
        /// <returns>
        ///     O índice baseado em zero da primeira ocorrência de <paramref name="item" /> em todo o
        ///     <see cref="T:System.Collections.Generic.List`1" />, se encontrado; caso contrário, -1.
        /// </returns>
        public int IndexOf(NFe item) => _nfes.IndexOf(item);

        /// <summary>
        ///     Insere um elemento no <see cref="T:System.Collections.Generic.List`1" />, no índice especificado.
        /// </summary>
        /// <param name="index">
        ///     O índice de base zero no qual o <paramref name="item" /> deve ser inserido.
        /// </param>
        /// <param name="item">
        ///     O objeto a ser inserido.
        ///     O valor pode ser <see langword="null" /> para tipos de referência.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> é menor que 0.
        ///     -ou-
        ///     <paramref name="index" /> é maior que <see cref="P:System.Collections.Generic.List`1.Count" />.
        /// </exception>
        public void Insert(int index, NFe item)
        {
            _nfes.Insert(index, item);
        }

        /// <summary>
        ///     Remove a primeira ocorrência de um objeto específico do <see cref="T:System.Collections.Generic.List`1" />.
        /// </summary>
        /// <param name="item">
        ///     O objeto a remover do <see cref="T:System.Collections.Generic.List`1" />.
        ///     O valor pode ser <see langword="null" /> para tipos de referência.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> se <paramref name="item" /> for removido com êxito; caso contrário,
        ///     <see langword="false" />.
        ///     Esse método também retornará <see langword="false" /> se <paramref name="item" /> não tiver sido encontrado no
        ///     <see cref="T:System.Collections.Generic.List`1" />.
        /// </returns>
        public bool Remove(NFe item) => _nfes.Remove(item);

        /// <summary>
        ///     Remove o elemento no índice especificado do <see cref="T:System.Collections.Generic.List`1" />.
        /// </summary>
        /// <param name="index">
        ///     O índice de base zero do elemento a ser removido.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> é menor que 0.
        ///     -ou-
        ///     <paramref name="index" /> é igual a ou maior que <see cref="P:System.Collections.Generic.List`1.Count" />.
        /// </exception>
        public void RemoveAt(int index)
        {
            _nfes.RemoveAt(index);
        }

        /// <summary>
        ///     Remove todos os elementos do <see cref="T:System.Collections.Generic.List`1" />.
        /// </summary>
        public void Clear()
        {
            _nfes.Clear();
        }

        /// <summary>
        ///     Carrega a NFe informada
        /// </summary>
        /// <param name="path">Caminho da NFe</param>
        public void Load(string path)
        {
            Guard.Against<ArgumentNullException>(path.IsNullOrEmpty(), nameof(path));

            var xml = File.Exists(path) ? File.ReadAllText(path, Encoding.UTF8) : path;
            LoadXml(xml);
        }

        /// <summary>
        ///     Carrega a NFe informada.
        /// </summary>
        /// <param name="stream">Stream da NFe</param>
        public void Load(Stream stream)
        {
            Guard.Against<ArgumentNullException>(stream == null, nameof(stream));
            Guard.Against<ArgumentException>(stream?.Length == 0, "Stream vazio");

            using var reader = new StreamReader(stream);
            LoadXml(reader.ReadToEnd());
        }

        /// <summary>
        ///     Carrega a NFe informada.
        /// </summary>
        public void LoadXml(string xml)
        {
            Guard.Against<VipException>(xml.IsNullOrEmpty(), "Carregamento falhou: Não foi possivel ler o conteudo.");
            Guard.Against<VipException>(!xml.Contains("</NFe>"), "Carregamento falhou: Arquivo xml incorreto.");

            Add(NotaFiscal.NFe.Load(xml));
        }

        /// <summary>
        ///     Assina os documentos da Coleção
        /// </summary>
        public void Assinar()
        {
            var cert = Parent.Configuracoes.Certificado.ObterCertificado();
            var saveOptions = Parent.Configuracoes.ObterOptions();

            try
            {
                Assinar(cert, saveOptions);
            }
            finally
            {
                cert.Reset();
                cert.Dispose();
            }
        }

        /// <summary>
        ///     Assina os documentos da Coleção
        /// </summary>
        /// <param name="certificado">O certificado.</param>
        /// <param name="options"></param>
        public void Assinar(X509Certificate2 certificado, SaveOptions options)
        {
            foreach (var nfe in NFe)
            {
                if (nfe.Assinado) continue;
                nfe.Assinar(certificado, options);
                nfe.GerarQrCode(Parent.Configuracoes);
            }
        }

        /// <summary>
        ///     Valida a NFe de acordo com o Schema.
        /// </summary>
        public void Validar()
        {
            var listaErros = new List<string>();
            var pathSchemas = Parent.Configuracoes.Arquivos.ObterSchema(NFeSchema.NFe);

            foreach (var nfe in NFe)
            {
                var xml = nfe.GetXml();
                SchemaHelper.ValidarXml(xml, pathSchemas, out var erros, out _);
                listaErros.AddRange(erros);
            }

            Guard.Against<VipException>(listaErros.Any(), $"{(Parent.Configuracoes.ExibeErrosSchema ? listaErros.AsString() : "Erros na validação do arquivo xml (Schema NF-e)")}");
        }

        /// <summary>
        ///     Verifica se existem documentos no objeto
        /// </summary>
        public bool IsEmpty() => !_nfes.Any();

        #endregion Methods
    }
}