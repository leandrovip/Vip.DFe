using System;
using System.Collections.Generic;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.Configuration
{
    public static class NFeEnderecoCollection
    {
        #region Endereços URL

        public static HashSet<NFeEndereco> listaEnderecos = new HashSet<NFeEndereco>
        {
            #region NFe

            #region Homologação

            #region AM

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "AM", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homnfe.sefaz.am.gov.br/services2/services/NfeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "AM", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homnfe.sefaz.am.gov.br/services2/services/NfeConsulta4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "AM", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homnfe.sefaz.am.gov.br/services2/services/NfeStatusServico4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "AM", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homnfe.sefaz.am.gov.br/services2/services/RecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "AM", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homnfe.sefaz.am.gov.br/services2/services/NfeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "AM", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao4"),

            #endregion

            #region BA

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "BA", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfe.sefaz.ba.gov.br/webservices/NFeInutilizacao4/NFeInutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "BA", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfe.sefaz.ba.gov.br/webservices/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "BA", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfe.sefaz.ba.gov.br/webservices/NFeStatusServico4/NFeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "BA", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfe.sefaz.ba.gov.br/webservices/CadConsultaCadastro4/CadConsultaCadastro4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "BA", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfe.sefaz.ba.gov.br/webservices/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "BA", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfe.sefaz.ba.gov.br/webservices/NFeAutorizacao4/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "BA", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfe.sefaz.ba.gov.br/webservices/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx"),

            #endregion

            #region GO

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homolog.sefaz.go.gov.br/nfe/services/NFeInutilizacao4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homolog.sefaz.go.gov.br/nfe/services/NFeConsultaProtocolo4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homolog.sefaz.go.gov.br/nfe/services/NFeStatusServico4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homolog.sefaz.go.gov.br/nfe/services/CadConsultaCadastro4?wsdl"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homolog.sefaz.go.gov.br/nfe/services/NFeRecepcaoEvento4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homolog.sefaz.go.gov.br/nfe/services/NFeAutorizacao4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homolog.sefaz.go.gov.br/nfe/services/NFeRetAutorizacao4?wsdl"),

            #endregion

            #region MG

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeConsulta4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeStatusServico4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfe.fazenda.mg.gov.br/nfe2/services/CadConsultaCadastro4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeRecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeRetAutorizacao4"),

            #endregion

            #region MS

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfe.sefaz.ms.gov.br/ws/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfe.sefaz.ms.gov.br/ws/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfe.sefaz.ms.gov.br/ws/NFeStatusServico4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfe.sefaz.ms.gov.br/ws/CadConsultaCadastro4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfe.sefaz.ms.gov.br/ws/NFeRecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfe.sefaz.ms.gov.br/ws/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfe.sefaz.ms.gov.br/ws/NFeRetAutorizacao4"),

            #endregion

            #region MT

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro4?wsdl"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao4?wsdl"),

            #endregion

            #region PE

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "PE", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NFeInutilizacao4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "PE", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NFeConsultaProtocolo4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "PE", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NFeStatusServico4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "PE", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/CadConsultaCadastro4?wsdl"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "PE", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NFeRecepcaoEvento4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "PE", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NFeAutorizacao4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "PE", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NFeRetAutorizacao4?wsdl"),

            #endregion

            #region PR

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeInutilizacao4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeConsultaProtocolo4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeStatusServico4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfe.sefa.pr.gov.br/nfe/CadConsultaCadastro4?wsdl"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeRecepcaoEvento4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeAutorizacao4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeRetAutorizacao4?wsdl"),

            #endregion

            #region RS

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "RS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "RS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "RS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "RS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://cad.sefazrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "RS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "RS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "RS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"),

            #endregion

            #region SP

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeinutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeconsultaprotocolo4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfestatusservico4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfe.fazenda.sp.gov.br/ws/cadconsultacadastro4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferecepcaoevento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeautorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferetautorizacao4.asmx"),

            #endregion

            #region SVAN (Servidor Nacional Emissão NFe)

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "SVAN", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.sefazvirtual.fazenda.gov.br/NFeInutilizacao4/NFeInutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "SVAN", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.sefazvirtual.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "SVAN", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.sefazvirtual.fazenda.gov.br/NFeStatusServico4/NFeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "SVAN", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.sefazvirtual.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "SVAN", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.sefazvirtual.fazenda.gov.br/NFeAutorizacao4/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "SVAN", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.sefazvirtual.fazenda.gov.br/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx"),

            #endregion

            #region SVRS (Servidor Virtual Rio Grande do Sul)

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "SVRS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "SVRS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "SVRS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "SVRS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "SVRS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "SVRS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"),

            #endregion

            #region SVC-AN (Servidor Virtual Nacional de Emissão em Contigência)

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "SVC-AN", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.svc.fazenda.gov.br/NFeInutilizacao4/NFeInutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "SVC-AN", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.svc.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "SVC-AN", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.svc.fazenda.gov.br/NFeStatusServico4/NFeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "SVC-AN", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.svc.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "SVC-AN", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.svc.fazenda.gov.br/NFeAutorizacao4/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "SVC-AN", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.svc.fazenda.gov.br/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx"),

            #endregion

            #region SVC-RS (Servidor Virtual Rio Grande do Sul de Emissão em Contigência)

            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "SVC-RS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "SVC-RS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "SVC-RS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "SVC-RS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "SVC-RS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"),

            #endregion

            #region AN (Servidor Nacional de Manifestação Destinatario(Recepção Evento) e Serviço de Distribuição NFe

            new NFeEndereco(NFeTipoServico.NFeDistribuicaoDFe, NFeModelo.NFe, "AN", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfe.fazenda.gov.br/NFeDistribuicaoDFe/NFeDistribuicaoDFe.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "AN", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfe.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"),

            #endregion

            #endregion

            #region Produção

            #region AM

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "AM", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.am.gov.br/services2/services/NfeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "AM", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.am.gov.br/services2/services/NfeConsulta4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "AM", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.am.gov.br/services2/services/NfeStatusServico4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "AM", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.am.gov.br/services2/services/RecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "AM", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.am.gov.br/services2/services/NfeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "AM", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao4"),

            #endregion

            #region BA

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "BA", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.ba.gov.br/webservices/NFeInutilizacao4/NFeInutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "BA", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.ba.gov.br/webservices/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "BA", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.ba.gov.br/webservices/NFeStatusServico4/NFeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "BA", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.ba.gov.br/webservices/CadConsultaCadastro4/CadConsultaCadastro4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "BA", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.ba.gov.br/webservices/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "BA", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.ba.gov.br/webservices/NFeAutorizacao4/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "BA", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.ba.gov.br/webservices/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx"),

            #endregion

            #region GO

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.go.gov.br/nfe/services/NFeInutilizacao4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.go.gov.br/nfe/services/NFeConsultaProtocolo4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.go.gov.br/nfe/services/NFeStatusServico4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.go.gov.br/nfe/services/CadConsultaCadastro4?wsdl"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.go.gov.br/nfe/services/NFeRecepcaoEvento4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.go.gov.br/nfe/services/NFeAutorizacao4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.go.gov.br/nfe/services/NFeRetAutorizacao4?wsdl"),

            #endregion

            #region MG

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeStatusServico4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.fazenda.mg.gov.br/nfe2/services/CadConsultaCadastro4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeRecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeRetAutorizacao4"),

            #endregion

            #region MS

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.ms.gov.br/ws/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.ms.gov.br/ws/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.ms.gov.br/ws/NFeStatusServico4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.ms.gov.br/ws/CadConsultaCadastro4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.ms.gov.br/ws/NFeRecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.ms.gov.br/ws/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.ms.gov.br/ws/NFeRetAutorizacao4"),

            #endregion

            #region MT

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro4?wsdl"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao4?wsdl"),

            #endregion

            #region PE

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "PE", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "PE", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "PE", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NFeStatusServico4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "PE", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.pe.gov.br/nfe-service/services/CadConsultaCadastro4?wsdl"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "PE", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NFeRecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "PE", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "PE", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NFeRetAutorizacao4"),

            #endregion

            #region PR

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefa.pr.gov.br/nfe/NFeInutilizacao4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefa.pr.gov.br/nfe/NFeConsultaProtocolo4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefa.pr.gov.br/nfe/NFeStatusServico4?wsdl"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefa.pr.gov.br/nfe/CadConsultaCadastro4?wsdl"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefa.pr.gov.br/nfe/NFeRecepcaoEvento4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefa.pr.gov.br/nfe/NFeAutorizacao4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefa.pr.gov.br/nfe/NFeRetAutorizacao4?wsdl"),

            #endregion

            #region RS

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"),

            #endregion

            #region SP

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.fazenda.sp.gov.br/ws/nfeinutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.fazenda.sp.gov.br/ws/nfeconsultaprotocolo4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.fazenda.sp.gov.br/ws/nfestatusservico4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.fazenda.sp.gov.br/ws/cadconsultacadastro4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.fazenda.sp.gov.br/ws/nferecepcaoevento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.fazenda.sp.gov.br/ws/nfeautorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.fazenda.sp.gov.br/ws/nferetautorizacao4.asmx"),

            #endregion

            #region SVAN (Servidor Nacional Emissão NFe)

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "SVAN", NFeVersao.v400, TipoAmbiente.Producao, "https://www.sefazvirtual.fazenda.gov.br/NFeInutilizacao4/NFeInutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "SVAN", NFeVersao.v400, TipoAmbiente.Producao, "https://www.sefazvirtual.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "SVAN", NFeVersao.v400, TipoAmbiente.Producao, "https://www.sefazvirtual.fazenda.gov.br/NFeStatusServico4/NFeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "SVAN", NFeVersao.v400, TipoAmbiente.Producao, "https://www.sefazvirtual.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "SVAN", NFeVersao.v400, TipoAmbiente.Producao, "https://www.sefazvirtual.fazenda.gov.br/NFeAutorizacao4/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "SVAN", NFeVersao.v400, TipoAmbiente.Producao, "https://www.sefazvirtual.fazenda.gov.br/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx"),

            #endregion

            #region SVRS (Servidor Virtual Rio Grande do Sul)

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "SVRS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "SVRS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "SVRS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaCadastro, NFeModelo.NFe, "SVRS", NFeVersao.v400, TipoAmbiente.Producao, "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "SVRS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "SVRS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "SVRS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"),

            #endregion

            #region SVC-AN (Servidor Virtual Nacional de Emissão em Contigência)

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFe, "SVC-AN", NFeVersao.v400, TipoAmbiente.Producao, "https://www.svc.fazenda.gov.br/NFeInutilizacao4/NFeInutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "SVC-AN", NFeVersao.v400, TipoAmbiente.Producao, "https://www.svc.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "SVC-AN", NFeVersao.v400, TipoAmbiente.Producao, "https://www.svc.fazenda.gov.br/NFeStatusServico4/NFeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "SVC-AN", NFeVersao.v400, TipoAmbiente.Producao, "https://www.svc.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "SVC-AN", NFeVersao.v400, TipoAmbiente.Producao, "https://www.svc.fazenda.gov.br/NFeAutorizacao4/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "SVC-AN", NFeVersao.v400, TipoAmbiente.Producao, "https://www.svc.fazenda.gov.br/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx"),

            #endregion

            #region SVC-RS (Servidor Virtual Rio Grande do Sul de Emissão em Contigência)

            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFe, "SVC-RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFe, "SVC-RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "SVC-RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFe, "SVC-RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFe, "SVC-RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"),

            #endregion

            #region AN (Servidor Nacional de Manifestação Destinatario(Recepção Evento) e Serviço de Distribuição NFe

            new NFeEndereco(NFeTipoServico.NFeDistribuicaoDFe, NFeModelo.NFe, "AN", NFeVersao.v400, TipoAmbiente.Producao, "https://www1.nfe.fazenda.gov.br/NFeDistribuicaoDFe/NFeDistribuicaoDFe.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFe, "AN", NFeVersao.v400, TipoAmbiente.Producao, "https://www.nfe.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"),

            #endregion

            #endregion

            #endregion

            #region NFCe

            #region Homologação

            #region AM

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeConsulta4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeStatusServico4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/RecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRetAutorizacao4"),

            #endregion

            #region CE

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfceh.sefaz.ce.gov.br/nfce4/services/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfceh.sefaz.ce.gov.br/nfce4/services/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfceh.sefaz.ce.gov.br/nfce4/services/NFeStatusServico4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfceh.sefaz.ce.gov.br/nfce4/services/NFeRecepcaoEvento4?WSDL"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfceh.sefaz.ce.gov.br/nfce4/services/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfceh.sefaz.ce.gov.br/nfce4/services/NFeRetAutorizacao4"),

            #endregion

            #region GO

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homolog.sefaz.go.gov.br:443/nfe/services/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homolog.sefaz.go.gov.br:443/nfe/services/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homolog.sefaz.go.gov.br:443/nfe/services/NFeStatusServico4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homolog.sefaz.go.gov.br:443/nfe/services/NFeRecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homolog.sefaz.go.gov.br:443/nfe/services/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homolog.sefaz.go.gov.br:443/nfe/services/NFeRetAutorizacao4"),

            #endregion

            #region MG

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfce.fazenda.mg.gov.br/nfce/services/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfce.fazenda.mg.gov.br/nfce/services/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfce.fazenda.mg.gov.br/nfce/services/NFeStatusServico4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfce.fazenda.mg.gov.br/nfce/services/NFeRecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfce.fazenda.mg.gov.br/nfce/services/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hnfce.fazenda.mg.gov.br/nfce/services/NFeRetAutorizacao4"),

            #endregion

            #region MS

            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfce.sefaz.ms.gov.br/ws/NFeStatusServico4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfce.sefaz.ms.gov.br/ws/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfce.sefaz.ms.gov.br/ws/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfce.sefaz.ms.gov.br/ws/NFeRecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfce.sefaz.ms.gov.br/ws/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.nfce.sefaz.ms.gov.br/ws/NFeRetAutorizacao4"),

            #endregion

            #region MT

            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeStatusServico4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeConsulta4"),
            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.sefaz.mt.gov.br/nfcews/services/RecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeRetAutorizacao4?wsdl"),

            #endregion

            #region PR

            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeStatusServico4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeRecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeRetAutorizacao4?wsdl"),

            #endregion

            #region RS

            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"),

            #endregion

            #region SP

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeInutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeConsultaProtocolo4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeRecepcaoEvento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeRetAutorizacao4.asmx"),

            #endregion

            #region SVRS - Servidor Virtual Rio Grande Do Sul (NFC-e)

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "SVRS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfce-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "SVRS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "SVRS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "SVRS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfce-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "SVRS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "SVRS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"),

            #endregion

            #endregion

            #region Produção

            #region AM

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeConsulta4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeStatusServico4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.am.gov.br/nfce-services/services/RecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRetAutorizacao4"),

            #endregion

            #region CE

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.ce.gov.br/nfce4/services/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.ce.gov.br/nfce4/services/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.ce.gov.br/nfce4/services/NFeStatusServico4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.ce.gov.br/nfce4/services/NFeRecepcaoEvento4?wsdl"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.ce.gov.br/nfce4/services/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.ce.gov.br/nfce4/services/NFeRetAutorizacao4"),

            #endregion

            #region GO

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.go.gov.br:443/nfe/services/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.go.gov.br:443/nfe/services/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.go.gov.br:443/nfe/services/NFeStatusServico4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.go.gov.br:443/nfe/services/NFeRecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.go.gov.br:443/nfe/services/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "https://nfe.sefaz.go.gov.br:443/nfe/services/NFeRetAutorizacao4"),

            #endregion

            #region MG

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.fazenda.mg.gov.br/nfce/services/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.fazenda.mg.gov.br/nfce/services/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.fazenda.mg.gov.br/nfce/services/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.fazenda.mg.gov.br/nfce/services/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.fazenda.mg.gov.br/nfce/services/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.fazenda.mg.gov.br/nfce/services/NFeRetAutorizacao4"),

            #endregion

            #region MS

            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.ms.gov.br/ws/NFeStatusServico4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.ms.gov.br/ws/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.ms.gov.br/ws/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.ms.gov.br/ws/NFeRecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.ms.gov.br/ws/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.ms.gov.br/ws/NFeRetAutorizacao4"),

            #endregion

            #region MT

            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeStatusServico4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeConsulta4"),
            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.mt.gov.br/nfcews/services/RecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeRetAutorizacao4?wsdl"),

            #endregion

            #region PR

            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefa.pr.gov.br/nfce/NFeStatusServico4"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefa.pr.gov.br/nfce/NFeConsultaProtocolo4"),
            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefa.pr.gov.br/nfce/NFeInutilizacao4"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefa.pr.gov.br/nfce/NFeRecepcaoEvento4"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefa.pr.gov.br/nfce/NFeAutorizacao4"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefa.pr.gov.br/nfce/NFeRetAutorizacao4?wsdl"),

            #endregion

            #region RS

            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"),

            #endregion

            #region SP

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.fazenda.sp.gov.br/ws/NFeInutilizacao4.asmx "),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.fazenda.sp.gov.br/ws/NFeConsultaProtocolo4.asmx "),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.fazenda.sp.gov.br/ws/NFeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.fazenda.sp.gov.br/ws/NFeRecepcaoEvento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.fazenda.sp.gov.br/ws/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.fazenda.sp.gov.br/ws/NFeRetAutorizacao4.asmx"),

            #endregion

            #region SVRS - Servidor Virtual Rio Grande Do Sul (NFC-e)

            new NFeEndereco(NFeTipoServico.NfeInutilizacao, NFeModelo.NFCe, "SVRS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeConsultaProtocolo, NFeModelo.NFCe, "SVRS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"),
            new NFeEndereco(NFeTipoServico.NfeStatusServico, NFeModelo.NFCe, "SVRS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"),
            new NFeEndereco(NFeTipoServico.RecepcaoEvento, NFeModelo.NFCe, "SVRS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeAutorizacao, NFeModelo.NFCe, "SVRS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"),
            new NFeEndereco(NFeTipoServico.NFeRetAutorizacao, NFeModelo.NFCe, "SVRS", NFeVersao.v400, TipoAmbiente.Producao, "https://nfce.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"),

            #endregion

            #endregion

            #endregion

            #region NFCe QrCode

            #region AC

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "AC", NFeVersao.v400, TipoAmbiente.Homologacao, "http://www.hml.sefaznet.ac.gov.br/nfce/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "AC", NFeVersao.v400, TipoAmbiente.Producao, "http://www.sefaznet.ac.gov.br/nfce/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "AC", NFeVersao.v400, TipoAmbiente.Homologacao, "www.sefaznet.ac.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "AC", NFeVersao.v400, TipoAmbiente.Producao, "www.sefaznet.ac.gov.br/nfce/consulta"),

            #endregion

            #region AL

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "AL", NFeVersao.v400, TipoAmbiente.Homologacao, "http://nfce.sefaz.al.gov.br/QRCode/consultarNFCe.jsp"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "AL", NFeVersao.v400, TipoAmbiente.Producao, "http://nfce.sefaz.al.gov.br/QRCode/consultarNFCe.jsp"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "AL", NFeVersao.v400, TipoAmbiente.Homologacao, "www.sefaz.al.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "AL", NFeVersao.v400, TipoAmbiente.Producao, "www.sefaz.al.gov.br/nfce/consulta"),

            #endregion

            #region AM

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Homologacao, "http://sistemas.sefaz.am.gov.br/nfceweb-hom/consultarNFCe.jsp"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Producao, "http://sistemas.sefaz.am.gov.br/nfceweb/consultarNFCe.jsp"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Homologacao, "www.sefaz.am.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "AM", NFeVersao.v400, TipoAmbiente.Producao, "www.sefaz.am.gov.br/nfce/consulta"),

            #endregion

            #region AP

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "AP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://www.sefaz.ap.gov.br/nfcehml/nfce.php"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "AP", NFeVersao.v400, TipoAmbiente.Producao, "https://www.sefaz.ap.gov.br/nfce/nfcep.php"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "AP", NFeVersao.v400, TipoAmbiente.Homologacao, "www.sefaz.ap.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "AP", NFeVersao.v400, TipoAmbiente.Producao, "www.sefaz.ap.gov.br/nfce/consulta"),

            #endregion

            #region BA

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "BA", NFeVersao.v400, TipoAmbiente.Homologacao, "http://hnfe.sefaz.ba.gov.br/servicos/nfce/qrcode.aspx"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "BA", NFeVersao.v400, TipoAmbiente.Producao, "http://nfe.sefaz.ba.gov.br/servicos/nfce/qrcode.aspx"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "BA", NFeVersao.v400, TipoAmbiente.Homologacao, "http://hinternet.sefaz.ba.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "BA", NFeVersao.v400, TipoAmbiente.Producao, "www.sefaz.ba.gov.br/nfce/consulta"),

            #endregion

            #region CE

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Homologacao, "http://nfceh.sefaz.ce.gov.br/pages/ShowNFCe.html"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Producao, "http://nfce.sefaz.ce.gov.br/pages/ShowNFCe.html"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Homologacao, "www.sefaz.ce.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "CE", NFeVersao.v400, TipoAmbiente.Producao, "www.sefaz.ce.gov.br/nfce/consulta"),

            #endregion

            #region DF

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "DF", NFeVersao.v400, TipoAmbiente.Homologacao, "http://www.fazenda.df.gov.br/nfce/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "DF", NFeVersao.v400, TipoAmbiente.Producao, "http://www.fazenda.df.gov.br/nfce/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "DF", NFeVersao.v400, TipoAmbiente.Homologacao, "www.fazenda.df.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "DF", NFeVersao.v400, TipoAmbiente.Producao, "www.fazenda.df.gov.br/nfce/consulta"),

            #endregion

            #region ES

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "ES", NFeVersao.v400, TipoAmbiente.Homologacao, "http://homologacao.sefaz.es.gov.br/ConsultaNFCe/qrcode.aspx"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "ES", NFeVersao.v400, TipoAmbiente.Producao, "http://app.sefaz.es.gov.br/ConsultaNFCe/qrcode.aspx"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "ES", NFeVersao.v400, TipoAmbiente.Homologacao, "www.sefaz.es.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "ES", NFeVersao.v400, TipoAmbiente.Producao, "www.sefaz.es.gov.br/nfce/consulta"),

            #endregion

            #region GO

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "http://homolog.sefaz.go.gov.br/nfeweb/sites/nfce/danfeNFCe"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "http://nfe.sefaz.go.gov.br/nfeweb/sites/nfce/danfeNFCe"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Homologacao, "http://homolog.sefaz.go.gov.br/nfeweb/sites/nfce/danfeNFCe"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "GO", NFeVersao.v400, TipoAmbiente.Producao, "www.sefaz.go.gov.br/nfce/consulta"),

            #endregion

            #region MA

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "MA", NFeVersao.v400, TipoAmbiente.Homologacao, "http://www.hom.nfce.sefaz.ma.gov.br/portal/consultarNFCe.jsp"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "MA", NFeVersao.v400, TipoAmbiente.Producao, "http://www.nfce.sefaz.ma.gov.br/portal/consultarNFCe.jsp"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "MA", NFeVersao.v400, TipoAmbiente.Homologacao, "www.sefaz.ma.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "MA", NFeVersao.v400, TipoAmbiente.Producao, "www.sefaz.ma.gov.br/nfce/consulta"),

            #endregion

            #region MG

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://portalsped.fazenda.mg.gov.br/portalnfce/sistema/qrcode.xhtml"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://portalsped.fazenda.mg.gov.br/portalnfce/sistema/qrcode.xhtml"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hportalsped.fazenda.mg.gov.br/portalnfce"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "MG", NFeVersao.v400, TipoAmbiente.Producao, "https://portalsped.fazenda.mg.gov.br/portalnfce"),

            #endregion

            #region MS

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "http://www.dfe.ms.gov.br/nfce/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "http://www.dfe.ms.gov.br/nfce/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Homologacao, "http://www.dfe.ms.gov.br/nfce/"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "MS", NFeVersao.v400, TipoAmbiente.Producao, "http://www.dfe.ms.gov.br/nfce/"),

            #endregion

            #region MT

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "http://homologacao.sefaz.mt.gov.br/nfce/consultanfce"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "http://www.sefaz.mt.gov.br/nfce/consultanfce"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Homologacao, "http://homologacao.sefaz.mt.gov.br/nfce/consultanfce"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "MT", NFeVersao.v400, TipoAmbiente.Producao, "http://www.sefaz.mt.gov.br/nfce/consultanfce"),

            #endregion

            #region PA

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "PA", NFeVersao.v400, TipoAmbiente.Homologacao, "https://appnfc.sefa.pa.gov.br/portal-homologacao/view/consultas/nfce/nfceForm.seam"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "PA", NFeVersao.v400, TipoAmbiente.Producao, "https://appnfc.sefa.pa.gov.br/portal/view/consultas/nfce/nfceForm.seam"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "PA", NFeVersao.v400, TipoAmbiente.Homologacao, "www.sefa.pa.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "PA", NFeVersao.v400, TipoAmbiente.Producao, "www.sefa.pa.gov.br/nfce/consulta"),

            #endregion

            #region PB

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "PB", NFeVersao.v400, TipoAmbiente.Homologacao, "http://www.sefaz.pb.gov.br/nfcehom"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "PB", NFeVersao.v400, TipoAmbiente.Producao, "http://www.sefaz.pb.gov.br/nfce"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "PB", NFeVersao.v400, TipoAmbiente.Homologacao, "www.sefaz.pb.gov.br/nfcehom"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "PB", NFeVersao.v400, TipoAmbiente.Producao, "www.sefaz.pb.gov.br/nfce"),

            #endregion

            #region PE

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "PE", NFeVersao.v400, TipoAmbiente.Homologacao, "http://nfcehomolog.sefaz.pe.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "PE", NFeVersao.v400, TipoAmbiente.Producao, "http://nfce.sefaz.pe.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "PE", NFeVersao.v400, TipoAmbiente.Homologacao, "nfce.sefaz.pe.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "PE", NFeVersao.v400, TipoAmbiente.Producao, "nfce.sefaz.pe.gov.br/nfce/consulta"),

            #endregion

            #region PI

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "PI", NFeVersao.v400, TipoAmbiente.Homologacao, "http://www.sefaz.pi.gov.br/nfce/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "PI", NFeVersao.v400, TipoAmbiente.Producao, "http://www.sefaz.pi.gov.br/nfce/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "PI", NFeVersao.v400, TipoAmbiente.Homologacao, "www.sefaz.pi.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "PI", NFeVersao.v400, TipoAmbiente.Producao, "www.sefaz.pi.gov.br/nfce/consulta"),

            #endregion

            #region PR

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "http://www.fazenda.pr.gov.br/nfce/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "http://www.fazenda.pr.gov.br/nfce/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Homologacao, "http://www.fazenda.pr.gov.br"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "PR", NFeVersao.v400, TipoAmbiente.Producao, "http://www.fazenda.pr.gov.br"),

            #endregion

            #region RJ

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "RJ", NFeVersao.v400, TipoAmbiente.Homologacao, "http://www4.fazenda.rj.gov.br/consultaNFCe/QRCode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "RJ", NFeVersao.v400, TipoAmbiente.Producao, "http://www4.fazenda.rj.gov.br/consultaNFCe/QRCode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "RJ", NFeVersao.v400, TipoAmbiente.Homologacao, "www.fazenda.rj.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "RJ", NFeVersao.v400, TipoAmbiente.Producao, "www.fazenda.rj.gov.br/nfce/consulta"),

            #endregion

            #region RN

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "RN", NFeVersao.v400, TipoAmbiente.Homologacao, "http://hom.nfce.set.rn.gov.br/consultarNFCe.aspx"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "RN", NFeVersao.v400, TipoAmbiente.Producao, "http://nfce.set.rn.gov.br/consultarNFCe.aspx"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "RN", NFeVersao.v400, TipoAmbiente.Homologacao, "www.set.rn.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "RN", NFeVersao.v400, TipoAmbiente.Producao, "www.set.rn.gov.br/nfce/consulta"),

            #endregion

            #region RO

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "RO", NFeVersao.v400, TipoAmbiente.Homologacao, "http://www.nfce.sefin.ro.gov.br/consultanfce/consulta.jsp"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "RO", NFeVersao.v400, TipoAmbiente.Producao, "http://www.nfce.sefin.ro.gov.br/consultanfce/consulta.jsp"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "RO", NFeVersao.v400, TipoAmbiente.Homologacao, "www.sefin.ro.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "RO", NFeVersao.v400, TipoAmbiente.Producao, "www.sefin.ro.gov.br/nfce/consulta"),

            #endregion

            #region RR

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "RR", NFeVersao.v400, TipoAmbiente.Homologacao, "http://200.174.88.103:8080/nfce/servlet/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "RR", NFeVersao.v400, TipoAmbiente.Producao, "https://www.sefaz.rr.gov.br/nfce/servlet/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "RR", NFeVersao.v400, TipoAmbiente.Homologacao, "www.sefaz.rr.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "RR", NFeVersao.v400, TipoAmbiente.Producao, "www.sefaz.rr.gov.br/nfce/consulta"),

            #endregion

            #region RS

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "RS", NFeVersao.v400, TipoAmbiente.Homologacao, "https://www.sefaz.rs.gov.br/NFCE/NFCE-COM.aspx"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "https://www.sefaz.rs.gov.br/NFCE/NFCE-COM.aspx"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "RS", NFeVersao.v400, TipoAmbiente.Homologacao, "www.sefaz.rs.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "RS", NFeVersao.v400, TipoAmbiente.Producao, "www.sefaz.rs.gov.br/nfce/consulta"),

            #endregion

            #region SC

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "SC", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.sat.sef.sc.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "SC", NFeVersao.v400, TipoAmbiente.Producao, "https://sat.sef.sc.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "SC", NFeVersao.v400, TipoAmbiente.Homologacao, "https://hom.sat.sef.sc.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "SC", NFeVersao.v400, TipoAmbiente.Producao, "https://sat.sef.sc.gov.br/nfce/consulta"),

            #endregion

            #region SE

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "SE", NFeVersao.v400, TipoAmbiente.Homologacao, "http://www.hom.nfe.se.gov.br/nfce/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "SE", NFeVersao.v400, TipoAmbiente.Producao, "http://www.nfce.se.gov.br/nfce/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "SE", NFeVersao.v400, TipoAmbiente.Homologacao, "http://www.hom.nfe.se.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "SE", NFeVersao.v400, TipoAmbiente.Producao, "http://www.nfce.se.gov.br/nfce/consulta"),

            #endregion

            #region SP

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://www.homologacao.nfce.fazenda.sp.gov.br/NFCeConsultaPublica/Paginas/ConsultaQRCode.aspx"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://www.nfce.fazenda.sp.gov.br/NFCeConsultaPublica/Paginas/ConsultaQRCode.aspx"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Homologacao, "https://www.homologacao.nfce.fazenda.sp.gov.br/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "SP", NFeVersao.v400, TipoAmbiente.Producao, "https://www.nfce.fazenda.sp.gov.br/consulta"),

            #endregion

            #region TO

            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "TO", NFeVersao.v400, TipoAmbiente.Homologacao, "http://homologacao.sefaz.to.gov.br/nfce/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlQrCode, NFeModelo.NFCe, "TO", NFeVersao.v400, TipoAmbiente.Producao, "http://www.sefaz.to.gov.br/nfce/qrcode"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "TO", NFeVersao.v400, TipoAmbiente.Homologacao, "http://homologacao.sefaz.to.gov.br/nfce/consulta"),
            new NFeEndereco(NFeTipoServico.NFCeUrlChave, NFeModelo.NFCe, "TO", NFeVersao.v400, TipoAmbiente.Producao, "http://www.sefaz.to.gov.br/nfce/consulta"),

            #endregion

            #endregion
        };

        #endregion

        #region Estados x Autorizadoras

        // Estado, Autorizadora, Contingencia, Modelo
        public static HashSet<Tuple<string, string, string, NFeModelo>> listaAutorizadora = new HashSet<Tuple<string, string, string, NFeModelo>>
        {
            #region NFe

            new Tuple<string, string, string, NFeModelo>("AC", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("AL", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("AM", "AM", "SVC-RS", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("AP", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("BA", "BA", "SVC-RS", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("CE", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("DF", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("ES", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("GO", "GO", "SVC-RS", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("MA", "SVAN", "SVC-RS", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("MG", "MG", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("MS", "MS", "SVC-RS", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("MT", "MT", "SVC-RS", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("PA", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("PB", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("PE", "PE", "SVC-RS", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("PI", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("PR", "PR", "SVC-RS", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("RJ", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("RN", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("RO", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("RR", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("RS", "RS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("SC", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("SE", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("SP", "SP", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("TO", "SRVS", "SVC-AN", NFeModelo.NFe),
            new Tuple<string, string, string, NFeModelo>("AN", "AN", "SVC-AN", NFeModelo.NFe),

            #endregion

            #region NFCe

            new Tuple<string, string, string, NFeModelo>("AC", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("AL", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("AM", "AM", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("AP", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("BA", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("CE", "CE", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("DF", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("ES", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("GO", "GO", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("MA", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("MG", "MG", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("MS", "MS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("MT", "MT", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("PA", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("PB", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("PE", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("PI", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("PR", "PR", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("RJ", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("RN", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("RO", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("RR", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("RS", "RS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("SC", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("SE", "SVRS", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("SP", "SP", "", NFeModelo.NFCe),
            new Tuple<string, string, string, NFeModelo>("TO", "SVRS", "", NFeModelo.NFCe),

            #endregion
        };

        #endregion
    }
}