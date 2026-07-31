# UI WinForms — Vip.DFe.Demo

> Fonte de verdade: `src/Vip.DFe.Demo`. Itens incertos marcados como **Hipótese a validar**.

## Papel

Aplicação de demonstração/operação manual da biblioteca. Não faz parte do pacote NuGet. É um **projeto legado MSBuild** (`.csproj` não-SDK, WinExe) para **.NET Framework 4.8**, com `packages.config`.

> Nota: o `README.md` sugere `dotnet run` para a Demo; isso não funciona diretamente — o projeto é .NET Framework legado e exige `msbuild` + `nuget restore` (ver `docs/conventions/build-and-validation.md`).

## Arquivos centrais

| Arquivo | Responsabilidade |
|---|---|
| `Program.cs` | Entry point (`Application.Run(new frmPrincipal())`) |
| `frmPrincipal.cs` / `frmPrincipal.Designer.cs` / `frmPrincipal.resx` | Form principal (~1559 linhas): montagem de NFe, operação NFeService, SAT, DANFE, configuração |
| `Data/JsonRepository.cs` | Persistência genérica JSON (ver `docs/modules/persistence.md`) |
| `Data/ConfiguracaoService.cs` | Carrega/salva `configuracao.json` |
| `Models/Configuracao.cs` | Configuração persistida (versão, ambiente, estado, emitente, destinatário, transporte, certificado, arquivos) |
| `Models/CertificadoDigital.cs`, `Models/Item.cs` | Modelos de apoio da UI (item de nota, certificado) |
| `Helpers/CertificadoHelper.cs` | Seleção/tratamento de certificado digital (repositório Windows/arquivo) |
| `Helpers/ControlExtensions.cs`, `Helpers/EnumHelper.cs`, `Extensions/*` | Suporte a UI (bindings, combobox por enum, máscaras) |
| `Enums/FuncaoServico.cs` | Enum usado no form para selecionar a função a executar |

## Operação típica na UI

1. Carregar/editar configuração (persistida em `configuracao.json`).
2. Selecionar certificado digital.
3. Montar NFe (emitente, destinatário, itens, pagamentos) — itens usam classes de imposto de **Vip.Fiscal** (ex.: `Icms00`).
4. Executar funções da biblioteca: autorização, consulta, cancelamento, CCe, inutilização, status; operações SAT; geração de DANFE/evento.
5. Visualizar XMLs de envio/retorno (via `FastColoredTextBox`).

## Observações

- **Duplicidade de modelos de imposto**: a Demo usa `Vip.Fiscal` para tributação, enquanto a lib possui modelos próprios em `0-Domain`. Para testar o imposto da lib, os itens precisam ser preenchidos via `NFeDetImposto` — `Hipótese a validar` (ver `docs/architecture/layers-and-dependencies.md`).
- **Sem testes automatizados** para a UI.
- **Não é referência de arquitetura de produção**: é exemplo operacional; padrões (componente com `Configuracoes`/`Documentos`/eventos) vêm da biblioteca, não da Demo.
