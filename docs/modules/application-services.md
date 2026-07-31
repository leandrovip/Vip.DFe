# Serviços de Aplicação

> Fonte de verdade: `src/Vip.DFe/1-Services`. Itens incertos marcados como **Hipótese a validar**.

## Orquestradores públicos (API)

| Service | Arquivo | Responsabilidade | Dependências principais | Exemplos de entrada/saída |
|---|---|---|---|---|
| `NFeService` | `1-Services/NFe/NFeService.cs` | Orquestra NFe/NFCe: autorização (síncrona e lote), consultas, cancelamentos, CCe, inutilização, status do serviço | `NFeConfig`, `NFeCollection`, servs `NFeServ*`, `VipComponent`, certificado | `Autorizacao()`, `AutorizacaoLote(id)`, `ConsultaAutorizacao(recibo)`, `Consultar(chave)`, `ConsultaSituacaoServico()`, `Inutilizar(cnpj, justificativa, modelo, serie, ini, fim)`, `Cancelar(...)`, `CancelarSubstituicao(...)`, `CartaoCorrecao(...)`; retornos `NFeXxxResposta` |
| `CFeService` | `1-Services/SAT/CFeService.cs` | Comunicação com o SAT via DLL nativa | `SatConfig`, `SatArquivos`, `ISatLibrary`/`SatManager`, `SatRede`, eventos | `EnviarDadosVenda(CFe)`, `CancelarUltimaVenda(CFe/CFeCanc/chave)`, `ConsultarSAT()`, `ConsultarStatusOperacional()`, `ConsultarNumeroSessao(n)`, `TesteFimAFim(CFe)`, `AtivarSAT(...)`, `AssociarAssinatura(...)`, `ConfigurarInterfaceDeRede(SatRede)`, `ExtrairLogs()`, `AtualizarSoftwareSAT()`, `BloquearSAT()`, `DesbloquearSAT()`, `TrocarCodigoDeAtivacao(...)`; retornos `SatResponse`/`VendaResponse`/`CancelamentoResponse`/`StatusOperacionalResponse`/`ConsultaSessaoResponse`/`LogResponse`/`TesteResponse` |
| `DanfeService` | `1-Services/Danfe/DanfeService.cs` | Gera DANFE em PDF a partir de `DanfeViewModel` | `Vip.Pdf`/`org.pdfclown`, blocos/elementos de `0-Domain/Danfe` | `Gerar()`, `Salvar(path/stream)`, `ObterPdfBytes(stream)`, `AdicionarLogoImagem/AdicionarLogoPdf` |
| `DanfeEventoService` | `1-Services/Danfe/DanfeEventoService.cs` | Gera DANFE de evento (cancelamento/CCe) em PDF a partir de `DanfeEventoViewModel` | `Vip.Pdf`/`org.pdfclown`, `BlocosEvento` | Mesmo padrão de `DanfeService` |

**Fluxo típico (NFeService)**: `Configuracoes.Validar()` → certificado → `Assinar()` → `Validar()` (XSD) → status `NFeStatus.X` → serv `NFeServXxx` (SOAP) → status `EmEspera` → monta `procNFe`/`procEvento` e grava XML em disco.

## Serviços SEFAZ (`1-Services/NFe/Serv*`)

Cada operação tem: `XxxServXxx` (serviço SOAP) + `XxxRequest`/`XxxResponse` (contrato) + `XxxResposta` (resultado com `XmlEnvio`/`XmlRetorno`/`EnvelopeSoap`/`RetornoWS`/`Resultado`).

| Serviço | Operação | Responsabilidade |
|---|---|---|
| `ServAutorizacao` | `Autorizacao` / `AutorizacaoLote` | Envia NFe(s) assinadas (NFCe síncrono / lote) |
| `ServRetAutorizacao` | `RetAutorizacao(recibo)` | Consulta processamento do lote pelo recibo |
| `ServConsultaProtocolo` | `Consulta(chave)` | Situação da NFe pela chave |
| `ServStatusServico` | `StatusServico()` | Status do serviço da SEFAZ |
| `ServInutilizacao` | `Inutilizar(...)` | Inutilização de faixa de numeração |
| `ServRecepcaoEvento` | `RecepcaoEvento(evento)` | Cancelamento, cancelamento por substituição e CCe |

- Interfaces: `INFeAutorizacao`, `INFeRetAutorizacao`, `INFeConsultaProtocolo`, `INFeStatusServico`, `INFeInutilizacao`, `INFeRecepcaoEvento` (`1-Services/NFe/Interfaces`).
- Base comum: `NFeServBase`, `NFeResultBase`, `RequestBase`, `ResponseBase` (`ServBase/`).

## Configuração

| Classe | Pasta | Papel |
|---|---|---|
| `NFeConfig` | `1-Services/NFe/Configuration` | Ambiente, modelo, versão, CNPJ, NFCe token/CSC, flags (síncrono, remover acentos/espaços, validar digest), `Webservices`, `Arquivos`, `Certificado` |
| `NFeConfigWebService` | idem | UF, timeout, tentativas/intervalo; resolve endpoint via `NFeEnderecoCollection` |
| `NFeConfigArquivo` | idem | Diretórios (`NFe/CNPJ/Modelo/{Enviado,Assinado,Retorno,Autorizado,Inutilizado}/yyyyMM`) e resolução de XSDs |
| `NFeConfigCertificado` | idem | Arquivo/senha ou serial/repositório, cache |
| `NFeEnderecoCollection` | idem | Catálogo de URLs por UF × serviço × modelo × versão × ambiente (27 UFs + SVAN/SVRS/SVC) |
| `SatConfig` | `1-Services/SAT/Configuration` | CNPJ/caixa, ambiente, dados do emitente, ISSQN, flags (UTF-8, validação de sessão) |
| `SatArquivos` | idem | Pastas de venda/cancelamento, separação por CNPJ/mês, prefixos dos arquivos |

## Infraestrutura de SAT

- `Manager/SatManager` + `SatLibrary` (abstract) + `SatCdecl`/`SatStdCall` (delegates com as duas calling conventions) + `ISatLibrary` (`SAT/Interfaces`).
- Carrega a DLL (`C:\SAT\sat.dll` default) e resolve as funções dinamicamente (`GetProcAddress`).
- `Events/` — eventos de ciclo de vida (`OnGetCodigoDeAtivacao`, `OnGetNumeroSessao`, `OnEnviarDadosVenda`, `OnCalcPath`, etc.).
- `Response/` — classes de resposta com `CodigoDeRetorno`/`MensagemRetorno` + dados específicos (`VendaResponse.Venda`, etc.).

## Comportamento transversal

- **`VipComponent`** (base dos orquestradores): ciclo `OnInitialize`/`OnDisposing`, `StatusChanged`, `Dispose`.
- **Eventos**: `NFeService.StatusChanged`; `CFeService` com vários eventos `On*`.
- **Erros**: `Guard.Against<T>` lança `VipException` (ou `ArgumentException`/`ArgumentNullException`) com mensagens PT-BR.
