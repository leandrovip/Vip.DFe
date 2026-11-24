# Convenções de Nomenclatura e Padrões

> Fonte de verdade: código em `src/Vip.DFe`. Itens incertos marcados como **Hipótese a validar**.

## Estrutura e namespaces

- **Pastas numeradas por camada** espelhadas em namespaces: `0-Domain`, `1-Services`, `2-Core`, `3-Infra` → `Vip.DFe.NFe`, `Vip.DFe.SAT`, `Vip.DFe.Danfe`, `Vip.DFe.Serializer`, `Vip.DFe.Cryptography`, etc.
- **Sub-pastas seguem o leiaute fiscal**: `NotaFiscal/Identificacao`, `Emitente`, `Destinatario`, `Detalhe`, `Total`, `Transporte`, `Pagamento` — refletindo os grupos do XML.

## Entidades de domínio

- POCOs `sealed` por padrão, herdam `GenericClone<T>` e implementam `INotifyPropertyChanged` (via `PropertyChanged.Fody`, não manual).
- Raízes XML: `[DFeRoot("nome", Namespace = ...)]`; grupos: `[DFeElement]` com `Id` (código do manual), `TipoCampo`, `Min`/`Max`, `Ocorrencia` (`Obrigatoria`/`NaoObrigatoria`/`MaiorQueZero`), `Ordem`.
- Documentos processados/assináveis: `DFeDocument<T>` / `DFeSignDocument<T>` (propriedade `Signature`).

## Serviços

- Orquestradores: `XxxService`.
  - `NFeService` e `CFeService`: `sealed`, herdam `VipComponent` (com `Configuracoes`/`Documentos`/eventos `On*`/`StatusChanged`).
  - `DanfeService` e `DanfeEventoService`: `public class ... : IDisposable` (não são `sealed` e não herdam `VipComponent`; ver `DanfeService.cs:18` e `DanfeEventoService.cs:16`).
- Serviços SEFAZ: `ServXxx` (`NFeServAutorizacao`, `NFeServRetAutorizacao`, `NFeServConsultaProtocolo`, `NFeServStatusServico`, `NFeServInutilizacao`, `NFeServRecepcaoEvento`) + interfaces `INFeXxx`.
- SAT: `SatManager`, `SatLibrary`, `SatCdecl`/`SatStdCall` + `ISatLibrary`.

## Padrão de contrato SEFAZ (Request/Response/Resposta)

Cada operação segue:

```text
XxxRequest / XxxResponse   → contrato da mensagem SOAP
XxxResposta                → resultado de alto nível (XmlEnvio, XmlRetorno, EnvelopeSoap, RetornoWS, Resultado)
XxxServXxx                 → serviço que executa a operação
```

- Bases: `RequestBase`/`ResponseBase`/`NFeResultBase`/`NFeServBase` (`1-Services/NFe/ServBase`).
- Respostas SAT: `Vip.DFe.SAT.Response` (`SatResponse` + especializações por operação).

## Configuração

- `XxxConfig` por domínio (`NFeConfig`, `SatConfig`), com sub-objetos `Webservices`, `Arquivos`, `Certificado` (NFe) e `SatArquivos` (SAT).
- Construtores `internal XxxConfig(parent)`; propriedades públicas; `TypeConverter` para uso em designer (`VipExpandableObjectConverter`).

## Elementos opcionais na serialização

- Elementos opcionais usam o padrão `ShouldSerializeXxx()` (método `bool` na própria classe, normalmente `private`, às vezes `public`), consumido pelo serializador para decidir se a tag é emitida. Exemplos em `NFeIde.cs`: `ShouldSerializeCMunFGIBS()`, `ShouldSerializeTpNFDebito()`, `ShouldSerializeGCompraGov()`; em `infNFe.cs`: `ShouldSerializeAvulsa()`.
- **Regra**: não emitir tags opcionais vazias. Se o grupo/campo não tem conteúdo (ou a condição de negócio não se aplica), `ShouldSerializeXxx()` deve retornar `false`.
- Commit de referência: `fix(nfe): Corrige serialização XML de elementos opcionais na NFe`.

## Núcleo

- `DFeSerializer`/`DFeSerializer<T>` (opções em `SerializerOptions`; `SaveOptions` flags).
- `SigningManager` estático para assinatura XMLDSig; `ChaveDFe.Gerar`/`DigitoVerificador` para chave de 44 dígitos.
- `Guard.Against<TException>(condicao, mensagem)` com mensagens PT-BR; exceções de domínio: `VipException`.

## Exceções e validação

- `Guard.Against<ArgumentException/ArgumentNullException/VipException>` nas entradas públicas.
- Mensagens de erro PT-BR, em maiúsculas no estilo `"ERRO: ..."`.
- Validação estrutural via XSD + atributos de serialização (Min/Max/Ocorrencia).

## Inconsistências observadas

- **`0-Domain/Enum` vs `Shared/Enum`**: enums do núcleo ficam em `Vip.DFe.Enum` (`Ocorrencia`, `SaveOptions`, `SignDigest`, `TipoCertificado`), enums fiscais compartilhados em `Vip.DFe.Shared.Enum` — atenção a namespaces duplicados na base (ex.: `Vip.DFe.Enum` e `Vip.DFe.SAT.Enum` coexistem).
- **Uso de `Vip.Fiscal` na Demo vs modelos próprios na lib** (duplicidade de modelos de imposto).
- **`propertyChanged` via Fody** mas comentários/`[DesignerSerializationVisibility]` manuais em propriedades — sem padrão único documentado.
- **`XxxResposta.Resultado` vs `XxxResponse`**: nomes próximos para conceitos diferentes (resultado de alto nível vs mensagem) — `Hipótese a validar` se a distinção está clara para consumidores.
- **Arquivos de teste com sufixo `Tests` e pastas espelhando namespace** (`Core/`, `NFe/`, `Danfe/`) — padrão ok, mas cobertura mínima.
- **Tabs como indentação** no csproj; código usa espaços. Não existe `.editorconfig` no repositório (fato) e não há StyleCop configurado.
