# Fluxos de Negócio Principais

> Fonte de verdade: código em `src/Vip.DFe`. Itens incertos marcados como **Hipótese a validar**.

## Fluxo NFe/NFCe — Autorização (síncrona e lote)

- **Entrada**: `NFeService` com NFes preenchidas em `Documentos.NFe`.
- **Camadas**: `1-Services/NFe/NFeService` → `ServAutorizacao` → `2-Core` (Service/WCF, Cryptography, Serializer) → `0-Domain` (modelos).
- **Passos**:
  1. `Configuracoes.Validar()` (resolve endpoint/webservice).
  2. Ajusta `ServicePointManager.SecurityProtocol` (TLS).
  3. Obtém certificado (`NFeConfigCertificado.ObterCertificado`).
  4. `Assinar()` — `SigningManager` (XMLDSig) em cada NFe.
  5. `Validar()` — validação contra XSD (via `NFeCollection`/`DFeSerializer`).
  6. `NFeServAutorizacao.Autorizacao`/`AutorizacaoLote` (SOAP).
  7. Se `Configuracoes.EnviarModoSincrono` (único critério; o código não testa o modelo NFCe — `NFeService.cs:~145`): em `Autorizacao()`, `GerarNFeProc` monta `NFeProc` + confere `DigVal` e grava `procNFe` no disco. Só ocorre quando `Resultado.CStat == 104` e o `cStat` da nota (`ProtNFe.InfProt.CStat`) está entre os processados (100, 110, 150, 301, 302, 303 — `DFeConstantes.NFeProcessada`). `AutorizacaoLote` não gera `procNFe`; isso ocorre em `ConsultaAutorizacao(recibo)`.
  - O mesmo `EnviarModoSincrono` define `indSinc` (0/1) no XML `enviNFe` (`NFeServAutorizacao`). **Padrão: `true`** (NT 2025.001: lote com uma única NF-e/NFC-e não pode ser assíncrono, rej. 452). Para `AutorizacaoLote` com mais de uma NF-e, o consumidor deve configurar `false` (a SEFAZ só aceita síncrono para lote de uma nota).
  - QR Code da NFC-e (`NFe.GerarQrCode`, chamado após a assinatura em `NFeCollection.Assinar`): versão definida por `NFeConfig.NFCeVersaoQrCode` (padrão v2, com CSC; v3 sem CSC e, em contingência offline, com assinatura RSA SHA-1 do certificado). NF-e mod. 55 com `tpImp=6` também gera `infNFeSupl`, sempre em v3 e com as URLs da NFC-e da UF.
  8. Status do componente `EmEspera`; `StatusChanged` disparado.
- **Persistência**: pastas `Assinado`/`Enviado`/`Retorno`/`Autorizado` (padrão `NFe/CNPJ/Modelo/<fase>`; a subpasta `yyyyMM` existe só em `Autorizado`). Ver `docs/modules/persistence.md`.

## Fluxo NFe — Consulta de processamento (recibo)

- **Entrada**: `ConsultaAutorizacao(string recibo)` após lote.
- **Passos**: `NFeServRetAutorizacao.RetAutorizacao(recibo)`; tenta novamente até `CStat == 104` (lote processado) respeitando `NumeroTentativas`/`IntervaloTentativas`; ao final monta e grava `procNFe` das autorizadas.

## Fluxo NFe — Consulta por chave e status do serviço

- `Consultar(chave)` → `NFeServConsultaProtocolo.Consulta`; gera `procNFe` se `CStat` 100/150.
- `ConsultaSituacaoServico()` → `NFeServStatusServico.StatusServico`.

## Fluxo NFe — Eventos (cancelamento, cancelamento por substituição, CCe)

- **Entrada**: `Cancelar(...)`, `CancelarSubstituicao(...)`, `CartaCorrecao(...)`.
- **Passos**:
  1. Validações `Guard.Against`: justificativa com no mínimo 15 caracteres em `Cancelar`, `CancelarSubstituicao` e `CartaCorrecao`; o **limite máximo de 255 caracteres vale só em `CancelarSubstituicao`**; a CCe tem limite máximo de 1000 caracteres; `Cancelar` não valida tamanho máximo.
  2. Monta `NFeEvento`/`NFeInfEvento`/`NFeDetEvento` conforme `TpEvento`.
  3. `NFeServRecepcaoEvento.RecepcaoEvento(evento)` (SOAP).
  4. `Resultado.CStat == 128` significa **lote de eventos processado** (`NFeService.cs:~555`); o status de cada evento vem em `Resultado.RetEvento[i].InfEvento.CStat` (ex.: `135` = evento registrado e vinculado à NFe; processados: 135, 136, 101, 151, 155 — `DFeConstantes.EventoProcessado`). Com `128`, monta `NFeProcEvento` (usa o primeiro `RetEvento`) e só grava `procEvento` se `Processado`; resultado disponível também como `ProcEvento`.
- **Saída**: `NFeRecepcaoEventoResposta`.

## Fluxo NFe — Inutilização

- `Inutilizar(cnpj, justificativa, modelo, serie, numeroInicial, numeroFinal)` → `NFeServInutilizacao`; arquivo na pasta `Inutilizado`.

## Fluxo SAT — Venda e cancelamento

- **Entrada**: `CFeService` com `CFe` montado (`NewCFe()`), código de ativação e DLL configurados.
- **Passos** (venda): monta XML do CFe → `SignAC` é apenas **preenchido** em `Ide.SignAC` por `NewCFe()`/`PreencherCFe` (`CFeService.cs:~268`); o `CFeService` não assina nada, a assinatura do AC (CNPJ software house + CNPJ emitente) é fornecida pela aplicação → `EnviarDadosVenda` chama a DLL (via `SatManager`) → parse da resposta `VendaResponse` (`CodigoDeRetorno == 6000` = sucesso) → salva XMLs conforme `SatArquivos`.
- **Cancelamento**: `CancelarUltimaVenda(CFe/CFeCanc/chave)` → DLL → `CancelamentoResponse` (`7000` = sucesso).
- Suporte a sessão: `ConsultarNumeroSessao`/`ConsultarUltimaSessaoFiscal`; validação de sessão configurável (`SatConfig.ValidarNumeroSessaoResposta`).

## Fluxo SAT — Status, configuração e gestão

- `ConsultarStatusOperacional()` → `StatusOperacionalResponse` (número de série, versão, bateria, etc.).
- `ConfigurarInterfaceDeRede(SatRede)`, `AtivarSAT`, `AssociarAssinatura`, `BloquearSAT`/`DesbloquearSAT`, `AtualizarSoftwareSAT`, `TrocarCodigoDeAtivacao`, `ExtrairLogs` — comandos diretos da DLL.
- Eventos `On*` (ex.: `OnGetCodigoDeAtivacao`, `OnGetNumeroSessao`) permitem capturar entradas no runtime.

## Fluxo DANFE (documento e evento)

1. Criar ViewModel a partir do XML/modelo:
   - `DanfeViewModelCreator.CriarDeArquivoXml/CriarDeStringXml/CreateFromXml(NFeProc)` ou os atalhos `DanfeViewModel.CriarDeArquivoXml/CriarDoConteudoXml` (raiz `nfeProc` ou `NFe`); `DanfeEventoViewModel.*` para procEvento.
2. `new DanfeService(viewModel)` → `Gerar()` → `Salvar(path/stream)` ou `ObterPdfBytes(stream)`.
3. Logomarca opcional via `AdicionarLogoImagem`/`AdicionarLogoPdf`.

### DANFE em modo espelho (pré-visualização)

- Quando o XML é uma `NFe` **sem** `protNFe` (ex.: nota ainda não autorizada), `DanfeViewModelCreator` define `ModoEspelho = true` (`infProt == null`) e não preenche protocolo/`CStat`.
- `DanfeService.ObterLinhasAviso()` (`DanfeService.cs:~191-205`) desenha o aviso em cada página: `DANFE SEM VALOR FISCAL` + `SEM AUTORIZAÇÃO DE USO` (+ `AMBIENTE DE HOMOLOGAÇÃO` se `TipoAmbiente == 2`); `BlocoIdentificacaoEmitente` exibe `SEM PROTOCOLO - PRÉ VISUALIZAÇÃO` no lugar do protocolo.
- Fora do modo espelho, em homologação (`TipoAmbiente == 2`) o aviso é `SEM VALOR FISCAL` + `AMBIENTE DE HOMOLOGAÇÃO`; em produção com protocolo não há aviso.
- Limitação: somente modelo 55 (`NotSupportedException` para NFCe, `DanfeViewModelCreator.cs:~125`) e tpEmis Normal/SVC-AN/SVC-RS.

## Regras transversais

- Todas as operações NFe passam por `Configuracoes.Validar()` e ajuste/restauração de `SecurityProtocol`.
- XML assinado, envelope SOAP de envio e de retorno são gravados sempre que `Arquivos.Salvar` é `true`; já `procNFe`/`procEvento` só são gravados quando o protocolo/evento está entre os `CStat` processados.
- Códigos de retorno: verificar `NFeStatus`/`Response` para cada operação (valores exemplificados acima são os observados nos serviços).
