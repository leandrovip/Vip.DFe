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
| Carta de correção | `CartaoCorrecao(cnpj, chave, seq, correcao)` | `NFeRecepcaoEventoResposta` |
| Inutilização | `Inutilizar(cnpj, justificativa, modelo, serie, ini, fim)` | `NFeInutilizacaoResposta` |
| Assinatura/validação | `Assinar(cert)`, `Validar()` | void |

### SAT (`CFeService`)

`AtivarSAT`, `AssociarAssinatura`, `EnviarDadosVenda`, `CancelarUltimaVenda`, `ConsultarSAT`, `ConsultarStatusOperacional`, `ConsultarNumeroSessao`, `ConsultarUltimaSessaoFiscal`, `TesteFimAFim`, `ExtrairLogs`, `ComunicarCertificadoIcpBrasil`, `ConfigurarInterfaceDeRede`, `AtualizarSoftwareSAT`, `BloquearSAT`, `DesbloquearSAT`, `TrocarCodigoDeAtivacao`, `Ativar`, `Desativar` — retornos em `Vip.DFe.SAT.Response`.

### DANFE

```text
DanfeViewModel.CriarDeArquivoXml/CriarDoConteudoXml (ou CriarDeArquivoXml(Stream))
  → new DanfeService(viewModel) → Gerar() → Salvar(path/stream)/ObterPdfBytes(stream)

DanfeEventoViewModel.CriarDeArquivoXml/CriarDoConteudoXml
  → new DanfeEventoService(viewModel) → Gerar() → Salvar(...)
```

## Contratos de resposta

Padrão nas respostas SEFAZ: objeto `XxxResposta` com propriedades `XmlEnvio`, `XmlRetorno`, `EnvelopeSoap`, `RetornoWS` e `Resultado` (com `CStat`/`XMotivo` e dados específicos). Verifique `CStat` para decidir o desfecho (ex.: `100` autorizada, `104` lote em processamento, `135` evento registrado, `128`... — validar códigos no domínio `NFe/Enum/NFeStatus.cs`/`Response`).

> Exemplos de uso completos: `src/README.md` (autorização NFe, SAT, DANFE).

## Observações de contrato público

- **Preserve a assinatura dos métodos públicos e namespaces** — são o contrato consumido pelos clientes (ver `AGENTS.md`).
- `NFeService` e `CFeService` são `sealed` e herdam `VipComponent`.
- Mensagens de erro/validação em PT-BR.
