using System.ComponentModel;
using System.IO;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Protocolo;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal
{
    [DFeRoot("nfeProc", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class NFeProc : DFeDocument<NFeProc>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public NFeProc()
        {
            NFe = new NFe();
            ProtNFe = new NFeProtNFe();
            Versao = NFeVersao.v400;
        }

        #endregion Constructors

        #region Propriedades

        [DFeAttribute(TipoCampo.Enum, "versao", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeVersao Versao { get; set; }

        [DFeElement("NFe", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFe NFe { get; set; }

        [DFeElement("protNFe", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeProtNFe ProtNFe { get; set; }

        [DFeIgnore] public bool Processado => ProtNFe?.InfProt?.CStat.IsIn(DFeConstantes.NFeProcessada) ?? false;

        #endregion

        #region Method

        public void Gravar(NFeConfig configuracoes)
        {
            if (!configuracoes.Arquivos.Salvar) return;

            var nomeArquivo = $"{ProtNFe.InfProt.ChNFe}";
            var caminho = configuracoes.Arquivos.ObterCaminhoAutorizado(NFe.InfNFe.Ide.DhEmi.DateTime);
            nomeArquivo += ProtNFe.InfProt.CStat.IsIn(DFeConstantes.NFeAutorizada) ? "-procNFe.xml" : "-den.xml";
            Save(Path.Combine(caminho, nomeArquivo));
        }

        #endregion
    }
}