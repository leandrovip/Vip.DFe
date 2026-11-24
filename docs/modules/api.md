# API Pública

> Fonte de verdade: assemblies/código de `src/Vip.DFe` e contratos em `docs/architecture/projects-map.md`.

## Forma de distribuição

- **Não há Web API HTTP.** Não existe projeto de API REST/ASP.NET Core no repositório.
- A API pública é a **biblioteca NuGet `Vip.DFe`** (`netstandard2.0`), consumida por aplicações .NET Framework / .NET Core / .NET 5+.
- A `Vip.DFe.Demo` (WinForms) é o cliente de referência.

## Superfície principal

| Componente | Namespace | Papel |
|---|---|---|
| `NFeService` | `Vip.DFe.NFe` | Operações NFe/NFCe |
| `CFeService` | `Vip.DFe.SAT` | Operações CFe-SAT |
| `DanfeService` | `Vip.DFe.Danfe` | Geração de DANFE (PDF) |
| `DanfeEventoService` | `Vip.DFe.Danfe` | DANFE de evento (cancelamento/CCe) |
| `DFeDocument<T>` / `DFeSignDocument<T>` / `DFeCollection` | `Vip.DFe.Document` | Base para modelos: load/save/serialize/assinatura |
| `NFeConfig`, `SatConfig`, `SatArquivos` | `Vip.DFe.NFe.Configuration` / `Vip.DFe.SAT.Configuration` | Configuração dos serviços |
| `DFeSerializer<T>` | `Vip.DFe.Serializer` | Serialização XML por atributos |
| `ChaveDFe`, `DigitoVerificador`, `CertificadoDigital` | `Vip.DFe` | Utilitários de chave/certificado |
| `VipComponent` | `Vip.DFe.Controls` | Base de componentes com `Configuracoes`/`Documentos`/eventos |
| Enums | `Vip.DFe.Shared.Enum`, `Vip.DFe.NFe.Enum`, `Vip.DFe.SAT.Enum`, `Vip.DFe.Enum` | Tipos de domínio/config |

## Métodos de entrada típicos

### NFe/NFCe (`NFeService`)

| Operação | Método | Saída |
|---|---|---|
| Autorização síncrona | `Autorizacao()` | `NFeAutorizacaoResposta` |
| Autorização em lote | `AutorizacaoLote(int/string loteId)` | `NFeAutorizacaoResposta` |
| Consulta por recibo | `ConsultaAutorizacao(string recibo)` | `NFeRetAutorizacaoResposta` |
| Consulta por chave | `Consultar(string chave)` | `NFeConsultaProtocoloResposta` |
| Status do serviço | `ConsultaSituacaoServico()` | `NFeStatusServicoResposta` |
| Cancelamento | `Cancelar(cnpj, chave, protocolo, seq, justificativa)` | `NFeRecepcaoEventoResposta` |
| Cancelamento por substituição | `CancelarSubstituicao(...)` | `NFeRecepcaoEventoResposta` |
| Carta de correção | `CartaCorrecao(cnpj, chave, seq, correcao)` | `NFeRecepcaoEventoResposta` |
| Inutilização | `Inutilizar(cnpj, justificativa, modelo, serie, ini, fim)` | `NFeInutilizacaoResposta` |
| Assinatura/validação | `Assinar(cert)`, `Validar()` | void |

### SAT (`CFeService`)

`AtivarSAT`, `AssociarAssinatura`, `EnviarDadosVenda`, `CancelarUltimaVenda`, `ConsultarSAT`, `ConsultarStatusOperacional`, `ConsultarNumeroSessao`, `ConsultarUltimaSessaoFiscal`, `TesteFimAFim`, `ExtrairLogs`, `ComunicarCertificadoIcpBrasil`, `ConfigurarInterfaceDeRede`, `AtualizarSoftwareSAT`, `BloquearSAT`, `DesbloquearSAT`, `TrocarCodigoDeAtivacao`, `Ativar`, `Desativar` — retornos em `Vip.DFe.SAT.Response`.

### DANFE

```text
DanfeViewModelCreator.CriarDeArquivoXml(string caminho | Stream) / CriarDeStringXml(string) / CreateFromXml(NFeProc)
  → new DanfeService(viewModel) → Gerar() → Salvar(path/stream)/ObterPdfBytes(stream)

DanfeEventoViewModel.CriarDeArquivoXml/CriarDoConteudoXml
  → new DanfeEventoService(viewModel) → Gerar() → Salvar(...)
```

`DanfeViewModelCreator` (`Vip.DFe.Danfe.Modelo`, classe estática):

| Método | Entrada | Observação |
|---|---|---|
| `CriarDeArquivoXml(string caminho)` | caminho de arquivo XML | Raiz `nfeProc` ou `NFe` |
| `CriarDeArquivoXml(Stream stream)` | stream com o XML | Sobrecarga existente **somente no creator** (`DanfeViewModel.CriarDeArquivoXml` só aceita `string path`) |
| `CriarDeStringXml(string str)` | conteúdo XML | Equivale a `DanfeViewModel.CriarDoConteudoXml(xml)`; raiz diferente de `nfeProc`/`NFe` gera `System.Exception` com a mensagem "Documento raiz '...' não suportado para geração da DANFE." (coberto em `DanfeViewModelCreatorTests`) |
| `CreateFromXml(NFeProc procNfe)` | objeto já carregado | Usa `ProtNFe.InfProt`; a sobrecarga que recebe `NFe` é `internal` |

- **`DanfeViewModel.ModoEspelho`**: `true` quando o XML não traz protocolo (`NFe` sem `protNFe`). Nesse modo `ProtocoloAutorizacao`, `CodigoStatusReposta` e `DescricaoStatusReposta` ficam nulos e o `DanfeService` imprime "DANFE SEM VALOR FISCAL" / "SEM AUTORIZAÇÃO DE USO" (pré-visualização).
- **Somente modelo 55**: `ide.Modelo != NFeModelo.NFe` lança `NotSupportedException` ("Somente o mod==55 está implementado."); `tpEmis` fora de Normal/SVC-AN/SVC-RS também lança `NotSupportedException`.

## Contratos de resposta

Padrão nas respostas SEFAZ: objeto `XxxResposta` com propriedades `XmlEnvio`, `XmlRetorno`, `EnvelopeSoap`, `RetornoWS` e `Resultado` (com `CStat`/`Motivo` e dados específicos). Verifique `CStat` para decidir o desfecho. Códigos tratados diretamente no código:

| `CStat` | Onde | Significado |
|---|---|---|
| `104` | `Resultado.CStat` de autorização/`ConsultaAutorizacao` | Lote processado; cada nota traz protocolo próprio em `ProtNFe.InfProt` (`CStat` da nota: 100 autorizada, 110, 150, 301, 302, 303 = `NFeProcessada`) |
| `100` / `150` | `Consultar(chave).Resultado.CStat` | Autorizada / autorizada fora do prazo — gera `procNFe` |
| `128` | `Resultado.CStat` de `Cancelar`/`CancelarSubstituicao`/`CartaCorrecao` | **Lote de eventos processado** (`NFeRecepcaoEventoResult`); gatilho para montar `NFeProcEvento` |
| `135` | `Resultado.RetEvento[i].InfEvento.CStat` | **Evento registrado e vinculado** à NFe (outros processados: 136, 101, 151, 155 — `DFeConstantes.EventoProcessado`) |

Ou seja: `128` descreve o lote (mensagem `retEnvEvento`), enquanto `135` descreve o resultado de cada evento individual (`retEvento`); um lote pode ter `128` e o evento ser rejeitado, então confira ambos.

> Exemplos de uso mínimos: `README.md` (raiz) — configuração, autorização, eventos, SAT e DANFE.

## Observações de contrato público

- `CartaoCorrecao` foi renomeado para `CartaCorrecao` (quebra de compatibilidade).
- `NFeIde.DPrevEntrega` passou de `DateTimeOffset?` para `DateTime?` (B10a é `TData`, AAAA-MM-DD). Corrige a falha ao carregar XML com `<dPrevEntrega>` (versões 1.1.46+); quem atribuía `DateTimeOffset` precisa usar `.Date`/`DateTime`.
- **Preserve a assinatura dos métodos públicos e namespaces** — são o contrato consumido pelos clientes (ver `AGENTS.md`).
- `NFeService` e `CFeService` são `sealed` e herdam `VipComponent`.
- Mensagens de erro/validação em PT-BR.
