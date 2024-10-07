using System;
using System.Security.Cryptography.X509Certificates;

namespace Vip.DFe.Demo
{
    public static class CertificadoHelper
    {
        #region Métodos Estáticos

        public static CertificadoDigital Obter()
        {
            var certificadoSelecionado = Selecionar();
            if (certificadoSelecionado == null) return null;

            return new CertificadoDigital(certificadoSelecionado.SerialNumber, certificadoSelecionado.Subject, certificadoSelecionado.NotAfter);
        }

        /// <summary>
        ///     Executa tela com os certificados digitais instalados para seleção do usuário
        /// </summary>
        /// <returns>
        ///     Retorna o certificado digital (null se nenhum certificado foi selecionado ou se o certificado selecionado está
        ///     com alguma falha)
        /// </returns>
        private static X509Certificate2 Selecionar()
        {
            var collection = AbrirTelaSelecao();

            if (collection.Count > 0) return collection[0];
            return null;
        }

        /// <summary>
        ///     Abre a tela de dialogo do windows para seleção do certificado digital
        /// </summary>
        /// <returns>Retorna a coleção de certificados digitais</returns>
        private static X509Certificate2Collection AbrirTelaSelecao()
        {
            var store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            var collection = store.Certificates;
            var collection1 = collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            var collection2 = collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);
            var scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) digital(is) disponível(is)",
                "Selecione o certificado digital para uso no aplicativo", X509SelectionFlag.SingleSelection);

            return scollection;
        }

        #endregion
    }
}