using System;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using Vip.DFe.Attributes;
using Vip.DFe.Cryptography.Models;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;
using KeyInfo = System.Security.Cryptography.Xml.KeyInfo;

namespace Vip.DFe.Cryptography
{
    public static class SigningManager
    {
        #region Methods

        /// <summary>
        ///     Assina a XML usando o certificado informado.
        /// </summary>
        /// <param name="xml">O Xml.</param>
        /// <param name="docElement">O elemento principal do xml a ser assinado.</param>
        /// <param name="infoElement">O elemento a ser assinado.</param>
        /// <param name="certificado">O certificado.</param>
        /// <param name="comments">Se for <c>true</c> vai inserir a tag #withcomments no transform.</param>
        /// <param name="identado">Se for <c>true</c> irá retornar o xml identado.</param>
        /// <param name="showDeclaration">Se for <c>true</c> irá incluir a declaração no xml</param>
        /// <param name="digest">Algoritmo usando para gerar o hash por padrão SHA1.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="VipException">Erro ao efetuar assinatura digital.</exception>
        public static string AssinarXml(string xml, string docElement, string infoElement, X509Certificate2 certificado, bool comments = false, bool identado = false, bool showDeclaration = true, SignDigest digest = SignDigest.SHA1)
        {
            try
            {
                var xmlDoc = new XmlDocument {PreserveWhitespace = true};
                xmlDoc.LoadXml(xml);
                AssinarDocumento(xmlDoc, docElement, infoElement, "Id", certificado, comments, digest);
                return xmlDoc.AsString(identado, showDeclaration);
            }
            catch (System.Exception ex)
            {
                throw new VipException("Erro ao efetuar assinatura digital.", ex);
            }
        }

        /// <summary>
        ///     Assina Multiplos elementos dentro da Xml.
        /// </summary>
        /// <param name="xml">Xml desejado</param>
        /// <param name="docElement">O elemento principal do xml a ser assinado.</param>
        /// <param name="infoElement">O elemento a ser assinado.</param>
        /// <param name="certificado">O certificado utilizado.</param>
        /// <param name="comments">Se for <c>true</c> vai inserir a tag #withcomments no transform.</param>
        /// <param name="identado">Se for <c>true</c> irá retornar o xml identado.</param>
        /// <param name="showDeclaration">Se for <c>true</c> irá incluir a declaração no xml</param>
        /// <param name="digest">Algoritmo usando para gerar o hash por padrão SHA1.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="VipException">Erro ao efetuar assinatura digital.</exception>
        public static string AssinarXmlTodos(string xml, string docElement, string infoElement, X509Certificate2 certificado, bool comments = false, bool identado = false, bool showDeclaration = false, SignDigest digest = SignDigest.SHA1)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);

                var xmlElements = doc.GetElementsByTagName(docElement).Cast<XmlElement>().Where(x => x.GetElementsByTagName(infoElement).Count == 1).ToArray();
                Guard.Against<VipException>(!xmlElements.Any(), "Nome do elemento de assinatura incorreto");

                foreach (var element in xmlElements)
                {
                    var xmlDoc = new XmlDocument {PreserveWhitespace = true};
                    xmlDoc.LoadXml(element.OuterXml);
                    AssinarDocumento(xmlDoc, docElement, infoElement, "Id", certificado, comments, digest);

                    var signedElement = doc.ImportNode(xmlDoc.DocumentElement, true);
                    element.ParentNode?.ReplaceChild(signedElement, element);
                }

                return doc.AsString(identado, showDeclaration);
            }
            catch (System.Exception ex)
            {
                throw new VipException("Erro ao efetuar assinatura digital.", ex);
            }
        }

        /// <summary>
        ///     Assina o xml informado.
        /// </summary>
        /// <param name="doc">O Xml</param>
        /// <param name="docElement">O elemento principal do xml a ser assinado.</param>
        /// <param name="infoElement">O elemento a ser assinado.</param>
        /// <param name="signAtribute">O atributo identificador do elemento a ser assinado.</param>
        /// <param name="certificado">O certificado.</param>
        /// <param name="comments">Se for <c>true</c> vai inserir a tag #withcomments no transform.</param>
        /// <param name="digest">Algoritmo usando para gerar o hash por padrão SHA1.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="VipException">Erro ao efetuar assinatura digital.</exception>
        public static void AssinarDocumento(XmlDocument doc, string docElement, string infoElement, string signAtribute, X509Certificate2 certificado, bool comments = false, SignDigest digest = SignDigest.SHA1)
        {
            Guard.Against<ArgumentNullException>(doc == null, "XmlDocument não pode ser nulo.");
            Guard.Against<ArgumentException>(docElement.IsNullOrEmpty(), "docElement não pode ser nulo ou vazio.");

            var xmlDigitalSignature = GerarAssinatura(doc, infoElement, signAtribute, certificado, comments, digest);
            var xmlElement = doc.GetElementsByTagName(docElement).Cast<XmlElement>().FirstOrDefault();

            Guard.Against<VipException>(xmlElement == null, "Elemento principal não encontrado.");

            var element = doc.ImportNode(xmlDigitalSignature, true);
            xmlElement.AppendChild(element);
        }

        /// <summary>
        ///     Gera a assinatura do xml e retorna uma instancia da classe <see cref="DFeSignature" />.
        /// </summary>
        public static DFeSignature AssinarDocumento<TDocument>(this DFeSignDocument<TDocument> document, X509Certificate2 certificado, bool comments, SignDigest digest, SaveOptions options, out string signedXml) where TDocument : class
        {
            Guard.Against<ArgumentException>(!typeof(TDocument).HasAttribute<DFeSignInfoElement>(), "Atributo [DFeSignInfoElement] não encontrado.");

            var xml = document.GetXml(options, Encoding.UTF8);
            var xmlDoc = new XmlDocument {PreserveWhitespace = true};
            xmlDoc.LoadXml(xml);

            var signatureInfo = typeof(TDocument).GetAttribute<DFeSignInfoElement>();
            var xmlSignature = GerarAssinatura(xmlDoc, signatureInfo.SignElement, signatureInfo.SignAtribute, certificado, comments, digest);

            // Adiciona a assinatura no documento e retorna o xml assinado no parametro signedXml
            var element = xmlDoc.ImportNode(xmlSignature, true);
            xmlDoc.DocumentElement?.AppendChild(element);
            signedXml = xmlDoc.AsString(!options.HasFlag(SaveOptions.DisableFormatting), !options.HasFlag(SaveOptions.OmitDeclaration));

            return DFeSignature.Load(xmlSignature.OuterXml);
        }

        public static bool ValidarAssinatura<TDocument>(DFeSignDocument<TDocument> document, bool gerarXml) where TDocument : class
        {
            var xml = document.Xml.IsNullOrEmpty() || gerarXml ? document.GetXml(SaveOptions.DisableFormatting, Encoding.UTF8) : document.Xml;
            var xmlDoc = new XmlDocument {PreserveWhitespace = true};
            xmlDoc.LoadXml(xml);
            return ValidarAssinatura(xmlDoc);
        }

        private static XmlElement GerarAssinatura(XmlDocument doc, string infoElement, string signAtribute, X509Certificate2 certificado, bool comments, SignDigest digest)
        {
            Guard.Against<ArgumentException>(!infoElement.IsNullOrEmpty() && doc.GetElementsByTagName(infoElement).Count != 1, "Referencia invalida ou não é unica.");

            //Adiciona Certificado ao Key Info
            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(certificado));

            //Seta chaves
            var signedDocument = new SignedXml(doc)
            {
                SigningKey = certificado.PrivateKey,
                KeyInfo = keyInfo,
                SignedInfo =
                {
                    SignatureMethod = GetSignatureMethod(digest)
                }
            };

            var uri = infoElement.IsNullOrEmpty() || signAtribute.IsNullOrEmpty() ? "" : $"#{doc.GetElementsByTagName(infoElement)[0].Attributes?[signAtribute]?.InnerText}";

            // Cria referencia
            var reference = new System.Security.Cryptography.Xml.Reference
            {
                Uri = uri,
                DigestMethod = GetDigestMethod(digest)
            };

            // Adiciona transformação a referencia
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            reference.AddTransform(new XmlDsigC14NTransform(comments));

            // Adiciona referencia ao xml
            signedDocument.AddReference(reference);

            // Calcula Assinatura
            signedDocument.ComputeSignature();

            // Pega representação da assinatura
            return signedDocument.GetXml();
        }

        public static RSACryptoServiceProvider LerDispositivo(RSACryptoServiceProvider key, string PIN)
        {
            CspParameters csp = new CspParameters(key.CspKeyContainerInfo.ProviderType, key.CspKeyContainerInfo.ProviderName);
            SecureString ss = new SecureString();
            foreach (char a in PIN) ss.AppendChar(a);
            csp.ProviderName = key.CspKeyContainerInfo.ProviderName;
            csp.ProviderType = key.CspKeyContainerInfo.ProviderType;
            csp.KeyNumber = key.CspKeyContainerInfo.KeyNumber == KeyNumber.Exchange ? 1 : 2;
            csp.KeyContainerName = key.CspKeyContainerInfo.KeyContainerName;
            csp.KeyPassword = ss;
            csp.Flags = CspProviderFlags.NoPrompt | CspProviderFlags.UseDefaultKeyContainer;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);
            return rsa;
        }

        private static string GetSignatureMethod(SignDigest digest)
        {
            return digest switch
            {
                SignDigest.SHA1 => "http://www.w3.org/2000/09/xmldsig#rsa-sha1",
                SignDigest.SHA256 => "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256",
                _ => throw new ArgumentOutOfRangeException(nameof(digest), digest, null)
            };
        }

        private static string GetDigestMethod(SignDigest digest)
        {
            return digest switch
            {
                SignDigest.SHA1 => "http://www.w3.org/2000/09/xmldsig#sha1",
                SignDigest.SHA256 => "http://www.w3.org/2001/04/xmlenc#sha256",
                _ => throw new ArgumentOutOfRangeException(nameof(digest), digest, null)
            };
        }

        /// <summary>
        ///     Validar a assinatura do Xml
        /// </summary>
        /// <param name="doc">o documento xml</param>
        /// <param name="docElement">O elemento principal do xml onde esta a tag Signature.</param>
        /// <returns></returns>
        public static bool ValidarAssinatura(XmlDocument doc)
        {
            try
            {
                var signElement = doc.GetElementsByTagName("Signature");
                Guard.Against<VipException>(signElement.Count < 1, "Verificação falhou: Elemento [Signature] não encontrado no documento.");
                Guard.Against<VipException>(signElement.Count > 1, "Verificação falhou: Mais de um elemento [Signature] encontrado no documento.");

                var certificateElement = doc.GetElementsByTagName("X509Certificate");
                Guard.Against<VipException>(certificateElement.Count < 1, "Verificação falhou: Elemento [X509Certificate] não encontrado no documento.");
                Guard.Against<VipException>(certificateElement.Count > 1, "Verificação falhou: Mais de um elemento [X509Certificate] encontrado no documento.");

                var signedXml = new SignedXml(doc);
                signedXml.LoadXml((XmlElement) signElement[0]);

                var certificate = new X509Certificate2(Convert.FromBase64String(certificateElement[0].InnerText));

                return signedXml.CheckSignature(certificate, true);
            }
            catch
            {
                return false;
            }
        }

        #endregion Methods
    }
}