# Fluxos de Integração

> Fonte de verdade: código em `src/Vip.DFe`. Itens incertos marcados como **Hipótese a validar**.

## Mapa de integrações

| Integração | Origem → Destino | Projeto responsável | Finalidade | Riscos |
|---|---|---|---|---|
| SEFAZ — webservices NFe/NFCe (SOAP) | Aplicação → SEFAZ (estadual/SEFAZ Virtual) | `Vip.DFe` (`1-Services/NFe/Serv*`, `2-Core/Service`) | Autorização, consulta, status, inutilização, eventos | Endpoints/URLs por UF; mudanças de leiaute; indisponibilidade; certificado |
| Certificado digital ICP-Brasil (X.509) | `NFeConfigCertificado` → `SigningManager` | `Vip.DFe` (`2-Core/Cryptography`) | Autenticação TLS no SOAP e assinatura XMLDSig | Validade/renovação; A1 vs A3; cache (`ManterEmCache`) |
| SAT — DLL nativa do fabricante | Aplicação → `C:\SAT\sat.dll` (default) | `Vip.DFe` (`1-Services/SAT/Manager`) | Venda, cancelamento, status, ativação, configuração de rede, logs | P/Invoke dinâmico; calling convention (Cdecl/StdCall); DLL ausente/desatualizada; `Hipótese a validar` comportamento multi-fabricante |
| Serialização XML + XSD | Modelos `0-Domain` ↔ XML | `Vip.DFe` (`2-Core/Serializer`, `schemas/`) | Montar/validar XMLs fiscais (leiaute 4.00, eventos 1.00) | Desalinhamento com leiaute; validação por XSD exige XSDs disponíveis em runtime |
| PDF — DANFE/DANFE de evento | `DanfeService`/`DanfeEventoService` → PDF | `Vip.DFe` (`1-Services/Danfe`, `0-Domain/Danfe`), `Vip.Pdf` (`org.pdfclown`) | Documento auxiliar de impressão | `Vip.Pdf`/PDFClown: origem/licença a validar; fidelidade visual do leiaute |
| Arquivos XML em disco | `NFeService`/`CFeService` → filesystem | `Vip.DFe` (`NFeConfigArquivo`, `SatArquivos`) | Persistência de envio/retorno/procNFe/procEvento | Sem transação; caminhos por CNPJ/mês; ver `docs/modules/persistence.md` |
| JSON de configuração | Demo ↔ `configuracao.json` | `Vip.DFe.Demo` (`Data/JsonRepository.cs`) | Persistência da configuração da UI | Dados de certificado sem criptografia aparente (`Hipótese a validar`) |

## Detalhes relevantes

### SEFAZ (SOAP/WCF)

- Base: `ServiceClientBase<T>` (`2-Core/Service`) herda de WCF `ClientBase<T>` com `BasicHttpBinding`; `HTTPS → Security.Mode.Transport` e certificado de cliente (`HttpClientCredentialType.Certificate`).
- `InspectorBehavior`/`MessageInspector` capturam mensagens de requisição/resposta (expostos como `XmlEnvio`/`XmlRetorno`/`EnvelopeSoap`/`RetornoWS` nas respostas).
- Endpoints: `NFeEnderecoCollection` (27 UFs + SVAN/SVRS/SVC, homologação e produção) por serviço × modelo × versão × ambiente. **Hipótese a validar**: efetividade de todas as URLs.
- TLS: `NFeService` força `Tls11 | Tls12` globalmente durante a operação.

### SAT (DLL nativa)

- Carregamento dinâmico da DLL (default `C:\SAT\sat.dll`), resolução de funções por nome com delegates `SatCdecl`/`SatStdCall` (duas calling conventions).
- Retornos em texto parseados para `Vip.DFe.SAT.Response` (`CodigoDeRetorno`, `MensagemRetorno`).
- Requer `SatConfig` (CNPJ/caixa/ambiente) e código de ativação.

### Criptografia

- `SigningManager` usa `SignedXml` (`System.Security.Cryptography.Xml`); digest default `SignDigest.SHA1` — **Hipótese a validar** quanto à adequação/legislação atual.
- Suporte `SHA256` disponível via `SignDigest` (mapeamento de URIs em `SigningManager`).

## Riscos gerais

- **Dependências de ambiente**: SEFAZ (rede/certificado), SAT (DLL nativa no cliente), PDF (Vip.Pdf/PDFClown).
- **Dependência de dados externos em runtime**: URLs da SEFAZ e XSDs precisam estar corretos/atuais; `NFeConfigArquivo.ObterSchema` resolve XSDs pelo nome a partir de `DiretorioSchemas`.
- **Alterações de leiaute/manual** exigem atualização coordenada de modelos (atributos), XSDs e endpoints.
