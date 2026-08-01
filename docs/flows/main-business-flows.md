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
  7. Se `EnviarModoSincrono` (ou NFCe): `GerarNFeProc` monta `NFeProc` + confere `DigVal` e grava `procNFe` no disco.
  8. Status do componente `EmEspera`; `StatusChanged` disparado.
- **Persistência**: pastas `Enviado`/`Assinado`/`Autorizado` (padrão `NFe/CNPJ/Modelo/<fase>/yyyyMM`).

## Fluxo NFe — Consulta de processamento (recibo)

- **Entrada**: `ConsultaAutorizacao(string recibo)` após lote.
- **Passos**: `NFeServRetAutorizacao.RetAutorizacao(recibo)`; tenta novamente até `CStat == 104` (lote processado) respeitando `NumeroTentativas`/`IntervaloTentativas`; ao final monta e grava `procNFe` das autorizadas.

## Fluxo NFe — Consulta por chave e status do serviço

- `Consultar(chave)` → `NFeServConsultaProtocolo.Consulta`; gera `procNFe` se `CStat` 100/150.
- `ConsultaSituacaoServico()` → `NFeServStatusServico.StatusServico`.

## Fluxo NFe — Eventos (cancelamento, cancelamento por substituição, CCe)

- **Entrada**: `Cancelar(...)`, `CancelarSubstituicao(...)`, `CartaoCorrecao(...)`.
- **Passos**:
  1. Validações `Guard.Against` (justificativa ≥15 e ≤255 caracteres; CCe ≤1000).
  2. Monta `NFeEvento`/`NFeInfEvento`/`NFeDetEvento` conforme `TpEvento`.
  3. `NFeServRecepcaoEvento.RecepcaoEvento(evento)` (SOAP).
  4. Se `CStat == 128` (evento registrado): monta `NFeProcEvento` e grava `procEvento`; resultado disponível também como `ProcEvento`.
- **Saída**: `NFeRecepcaoEventoResposta`.

## Fluxo NFe — Inutilização

- `Inutilizar(cnpj, justificativa, modelo, serie, numeroInicial, numeroFinal)` → `NFeServInutilizacao`; arquivo na pasta `Inutilizado`.

## Fluxo SAT — Venda e cancelamento

- **Entrada**: `CFeService` com `CFe` montado (`NewCFe()`), código de ativação e DLL configurados.
- **Passos** (venda): monta XML do CFe → assina (`SignAC`) → `EnviarDadosVenda` chama a DLL (via `SatManager`) → parse da resposta `VendaResponse` (`CodigoDeRetorno == 6000` = sucesso) → salva XMLs conforme `SatArquivos`.
- **Cancelamento**: `CancelarUltimaVenda(CFe/CFeCanc/chave)` → DLL → `CancelamentoResponse` (`7000` = sucesso).
- Suporte a sessão: `ConsultarNumeroSessao`/`ConsultarUltimaSessaoFiscal`; validação de sessão configurável (`SatConfig.ValidarNumeroSessaoResposta`).

## Fluxo SAT — Status, configuração e gestão

- `ConsultarStatusOperacional()` → `StatusOperacionalResponse` (número de série, versão, bateria, etc.).
- `ConfigurarInterfaceDeRede(SatRede)`, `AtivarSAT`, `AssociarAssinatura`, `BloquearSAT`/`DesbloquearSAT`, `AtualizarSoftwareSAT`, `TrocarCodigoDeAtivacao`, `ExtrairLogs` — comandos diretos da DLL.
- Eventos `On*` (ex.: `OnGetCodigoDeAtivacao`, `OnGetNumeroSessao`) permitem capturar entradas no runtime.

## Fluxo DANFE (documento e evento)

1. Criar ViewModel a partir do XML/modelo:
   - `DanfeViewModel.CriarDeArquivoXml/CriarDoConteudoXml` (NFeProc/NFe) ou `DanfeEventoViewModel.*` (procEvento).
2. `new DanfeService(viewModel)` → `Gerar()` → `Salvar(path/stream)` ou `ObterPdfBytes(stream)`.
3. Logomarca opcional via `AdicionarLogoImagem`/`AdicionarLogoPdf`.

## Regras transversais

- Todas as operações NFe passam por `Configuracoes.Validar()` e ajuste/restauração de `SecurityProtocol`.
- Arquivos só são gravados em sucesso confirmado da SEFAZ (códigos `CStat`).
- Códigos de retorno: verificar `NFeStatus`/`Response` para cada operação (valores exemplificados acima são os observados nos serviços).
