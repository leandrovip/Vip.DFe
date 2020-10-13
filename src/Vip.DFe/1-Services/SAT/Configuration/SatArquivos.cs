using System.ComponentModel;
using System.Reflection;
using Vip.DFe.Conveter;
using Vip.DFe.Extensions;

namespace Vip.DFe.SAT.Configuration
{
    /// <summary>
    ///     Classe de configuração de salvamento dos arquivos CFe
    /// </summary>
    [TypeConverter(typeof(VipExpandableObjectConverter))]
    public sealed class SatArquivos
    {
        #region Constructor

        /// <summary>
        ///     Inicializa uma nova instancia da classe <see cref="SatArquivos" />.
        /// </summary>
        internal SatArquivos()
        {
            PrefixoArqCFe = "AD";
            PrefixoArqCFeCanc = "ADC";
            var path = Assembly.GetExecutingAssembly().GetPath();
            PastaCFeVenda = $@"{path}\Vendas";
            PastaCFeCancelamento = $@"{path}\Cancelamentos";
            PastaEnvio = $@"{path}\Enviado";
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        ///     Se salva os arquivos CFe de venda
        /// </summary>
        public bool SalvarCFe { get; set; }

        /// <summary>
        ///     Se salva os arquivos CFe de cancelamento
        /// </summary>
        public bool SalvarCFeCanc { get; set; }

        /// <summary>
        ///     Se salva os arquivos de envio
        /// </summary>
        public bool SalvarEnvio { get; set; }

        /// <summary>
        ///     Se salva os arquivos separados por CNPJ
        /// </summary>
        public bool SepararPorCNPJ { get; set; }

        /// <summary>
        ///     Se salva os arquivos separados por Mês
        /// </summary>
        public bool SepararPorMes { get; set; }

        /// <summary>
        ///     Caminho da pasta dos arquivos CFe de venda
        /// </summary>
        public string PastaCFeVenda { get; set; }

        /// <summary>
        ///     Caminho da pasta dos arquivos CFe de cancelamento
        /// </summary>
        public string PastaCFeCancelamento { get; set; }

        /// <summary>
        ///     Caminho da pasta dos arquivos enviados
        /// </summary>
        /// <value>The pasta envio.</value>
        public string PastaEnvio { get; set; }

        /// <summary>
        ///     Prefixo utilizado nos arquivos CFe de venda
        /// </summary>
        public string PrefixoArqCFe { get; set; }

        /// <summary>
        ///     Prefixo utilizado nos arquivos CFe de cancelamento
        /// </summary>
        public string PrefixoArqCFeCanc { get; set; }

        #endregion Properties
    }
}