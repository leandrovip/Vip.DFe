using System;

namespace Vip.DFe.Demo.Models
{
    public class CertificadoDigital
    {
        #region Propriedades

        public string NumeroSerie { get; private set; }
        public string Sujeito { get; private set; }
        public DateTime DataValidade { get; private set; }

        #endregion

        #region Construtores

        public CertificadoDigital(string numeroSerie, string sujeito, DateTime dataValidade)
        {
            NumeroSerie = numeroSerie.Trim();
            Sujeito = sujeito.Trim();
            DataValidade = dataValidade;
        }

        protected CertificadoDigital() { }

        #endregion

        #region Métodos

        public bool SeVencido()
        {
            var result = DateTime.Compare(DataValidade, DateTime.Now);
            return result < 0;
        }

        #endregion

    }
}