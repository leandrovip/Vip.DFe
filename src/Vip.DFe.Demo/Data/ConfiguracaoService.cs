using Vip.DFe.Demo.Models;

namespace Vip.DFe.Demo.Data
{
    public class ConfiguracaoService
    {
        #region Propriedades

        private const string _nomeArquivo = "configuracao.json";
        private readonly JsonRepository<Configuracao> _repository = new JsonRepository<Configuracao>(_nomeArquivo);

        #endregion

        #region Métodos Públicos

        public void Salvar(Configuracao configuracao)
        {
            _repository.Save(configuracao);
        }

        public Configuracao Obter()
        {
            return _repository.Load();
        }

        #endregion
    }
}