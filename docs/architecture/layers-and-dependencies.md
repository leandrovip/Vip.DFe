# Camadas e Dependências

> Código-fonte é a fonte de verdade. Itens incertos marcados como **Hipótese a validar**.

## Camadas internas da biblioteca (`src/Vip.DFe`)

| Camada | Pasta | Depende de | Responsabilidade |
|---|---|---|---|
| Domínio | `0-Domain` | `2-Core` (atributos, `DFeDocument`, `GenericClone`) | Modelos fiscais (NFe, SAT/CFe, DANFE, Shared) com atributos de serialização |
| Serviços | `1-Services` | `0-Domain`, `2-Core`, `3-Infra` | Orquestradores públicos + servs SEFAZ + configuração |
| Núcleo | `2-Core` | `3-Infra`, `0-Domain` (parcial, ex.: enums Shared) | Serializer, criptografia, WCF, documentos, chave, certificado |
| Infra | `3-Infra` | — (tipos base) | Exceções/Guard, gráficos, interops nativos, writer |
| Schemas | `schemas/` (raiz) | — | XSDs de validação (copiados/mapeados em runtime via `NFeConfigArquivo.ObterSchema`) |

## Dependências observadas

- `0-Domain/NFe` e `0-Domain/SAT` herdam de `DFeSignDocument<T>`/`DFeDocument<T>` (`2-Core/Document`) e usam atributos de `2-Core/Attributes`.
- `1-Services` usa `VipComponent` (`2-Core/Controls`) como base dos orquestradores, `Guard`/`VipException` (`3-Infra/Exception`), `Extensions` e `ServiceClientBase`/`Soap12ServiceClientBase` (`2-Core/Service`).
- `NFeService` depende dos servs `1-Services/NFe/Serv*` (autorização, retorno, consulta, inutilização, evento, status), cada um expondo `XxxServXxx` + `Request/Response`.
- A comunicação SOAP herda de `ClientBase<T>` (WCF) via `ServiceClientBase<T>` (`2-Core/Service`) — **Hipótese a validar**: alguns servs usam `Soap12ServiceClientBase`.
- DANFE depende de `Vip.Pdf` (que empacota `org.pdfclown`) para geração de PDF.
- SAT depende de DLL nativa do fabricante (`C:\SAT\sat.dll` default) via P/Invoke dinâmico (`1-Services/SAT/Manager`).
- `Vip.DFe.Tests` e `Vip.DFe.Demo` referenciam a biblioteca; a Demo usa ainda `Vip.Extensions`, `Vip.Fiscal`, `Newtonsoft.Json` e `FastColoredTextBox`.

## Acoplamentos / suspeitas

- **Domínio conhece a serialização**: as entidades de `0-Domain` carregam atributos `[DFeElement]`/`[DFeRoot]` com `Id` (código do manual), `Ocorrencia`, `Ordem`, Min/Max — o mapeamento fiscal está acoplado ao modelo (intencional no desenho atual, mas é a fonte principal de complexidade).
- **`2-Core` referencia enums de `0-Domain/Shared`** (ex.: `TipoAmbiente`), criando dependência do núcleo para o domínio (sentido inverso ao usual) — `Hipótese a validar`.
- **`ServicePointManager.SecurityProtocol` global**: `NFeService` altera o protocolo TLS globalmente e restaura depois (não thread-safe por natureza) — risco em uso concorrente.
- **`Vip.Fiscal` só na Demo**, enquanto `Vip.DFe` tem seus próprios modelos de imposto em `0-Domain`. A Demo usa classes de imposto de `Vip.Fiscal` (ex.: `Icms00`) — duplicidade de fontes de verdade para tributação.
- **Sem camada de abstração de IO/armazenamento na lib**: persistência via arquivos XML é feita diretamente pelos modelos (`NFeProc.Gravar`) e configuração — não há repositórios/injeção de dependência.

## Regras inferidas

- **Dependência unidirecional por convenção**: `0-Domain → 2-Core → 3-Infra`; serviços acima de tudo. Evitar que `3-Infra` dependa de `0-Domain`/`1-Services`.
- **Não criar dependência da lib para a Demo/Tests**.
- **Manter `netstandard2.0`** na lib para compatibilidade com .NET Framework e .NET Core/5+.
- **Não incluir `3-Infra/Notification`** na compilação (removido no csproj).
