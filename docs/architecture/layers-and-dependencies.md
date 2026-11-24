# Camadas e Dependências

> Código-fonte é a fonte de verdade. Itens incertos marcados como **Hipótese a validar**.

## Camadas internas da biblioteca (`src/Vip.DFe`)

| Camada | Pasta | Depende de | Responsabilidade |
|---|---|---|---|
| Domínio | `0-Domain` | `2-Core` (atributos, `DFeDocument`, `GenericClone`) | Modelos fiscais (NFe, SAT/CFe, DANFE, Shared) com atributos de serialização |
| Serviços | `1-Services` | `0-Domain`, `2-Core`, `3-Infra` | Orquestradores públicos + servs SEFAZ + configuração |
| Núcleo | `2-Core` | `3-Infra`, `0-Domain` (parcial, ex.: enums Shared), `1-Services` (parcial: `ServiceClient` usa `NFeConfig`) | Serializer, criptografia, WCF, documentos, chave, certificado |
| Infra | `3-Infra` | — (tipos base) | Exceções/Guard, gráficos, interops nativos, writer |
| Schemas | `schemas/` (raiz) | — | XSDs de validação (copiados/mapeados em runtime via `NFeConfigArquivo.ObterSchema`) |

## Dependências observadas

- `0-Domain/NFe` e `0-Domain/SAT` herdam de `DFeSignDocument<T>`/`DFeDocument<T>` (`2-Core/Document`) e usam atributos de `2-Core/Attributes`.
- `1-Services` usa `VipComponent` (`2-Core/Controls`) como base dos orquestradores, `Guard`/`VipException` (`3-Infra/Exception`), `Extensions` e `ServiceClientBase`/`Soap12ServiceClientBase` (`2-Core/Service`).
- `NFeService` depende dos servs `1-Services/NFe/Serv*` (autorização, retorno, consulta, inutilização, evento, status), cada um expondo `XxxServXxx` + `Request/Response`.
- A comunicação SOAP herda de `ClientBase<T>` (WCF) via `ServiceClientBase<T>` (`2-Core/Service`). **Todos os servs SEFAZ usam SOAP 1.2**: `NFeServBase<T>` → `ServiceClient<TConfig, TService>` (`ServiceClient.cs:14`) → `Soap12ServiceClientBase<T>` → `ServiceClientBase<T>`.
- DANFE depende de `Vip.Pdf` (que empacota `org.pdfclown`) para geração de PDF.
- SAT depende de DLL nativa do fabricante (`C:\SAT\sat.dll` default) via P/Invoke dinâmico (`1-Services/SAT/Manager`).
- `Vip.DFe.Tests` e `Vip.DFe.Demo` referenciam a biblioteca; a Demo usa ainda `Vip.Extensions`, `Vip.Fiscal`, `Newtonsoft.Json` e `FastColoredTextBox`.

## Acoplamentos / suspeitas

- **Domínio conhece a serialização**: as entidades de `0-Domain` carregam atributos `[DFeElement]`/`[DFeRoot]` com `Id` (código do manual), `Ocorrencia`, `Ordem`, Min/Max — o mapeamento fiscal está acoplado ao modelo (intencional no desenho atual, mas é a fonte principal de complexidade).
- **`2-Core` referencia enums de `0-Domain/Shared`** (ex.: `TipoAmbiente`), criando dependência do núcleo para o domínio (sentido inverso ao usual) — `Hipótese a validar`.
- **`2-Core` depende de `1-Services` (fato)**: `2-Core/Service/ServiceClient.cs` usa `Vip.DFe.NFe.Configuration` (`NFeConfig`, `NFeConfigArquivo`) e `Vip.DFe.NFe.Enum` (`ServiceClient.cs:9-10`), invertendo o sentido esperado das camadas.
- **`ServicePointManager.SecurityProtocol` global**: `NFeService` altera o protocolo TLS globalmente e restaura depois (não thread-safe por natureza) — risco em uso concorrente.
- **`Vip.Fiscal` só na Demo**, enquanto `Vip.DFe` tem seus próprios modelos de imposto em `0-Domain`. A Demo usa `Vip.Fiscal` apenas para **calcular** (via `TributacaoDemoService`) e preenche os modelos da própria lib (`NFeDetImposto`, `IbsCbs`, `NFeIbsCbsTot`); não há mais uso das classes de imposto do `Vip.Fiscal` (ex.: `Icms00`) para montar o XML.
- **Sem camada de abstração de IO/armazenamento na lib**: persistência via arquivos XML é feita diretamente pelos modelos (`NFeProc.Gravar`) e configuração — não há repositórios/injeção de dependência.

## Regras inferidas

- **Dependência unidirecional por convenção**: `0-Domain → 2-Core → 3-Infra`; serviços acima de tudo. Evitar que `3-Infra` dependa de `0-Domain`/`1-Services`.
- **Não criar dependência da lib para a Demo/Tests**.
- **Manter `netstandard2.0`** na lib para compatibilidade com .NET Framework e .NET Core/5+.
- **Não incluir `3-Infra/Notification`** na compilação (excluído no csproj; a pasta não existe em disco).
