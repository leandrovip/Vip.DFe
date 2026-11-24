# UI WinForms — Vip.DFe.Demo

> Fonte de verdade: `src/Vip.DFe.Demo`. Itens incertos marcados como **Hipótese a validar**.

## Papel

Aplicação de demonstração/operação manual da biblioteca. Não faz parte do pacote NuGet. É um **projeto legado MSBuild** (`.csproj` não-SDK, WinExe) para **.NET Framework 4.8**, com `packages.config`.

> Nota: a Demo **não** é executada com `dotnet run` — o projeto é .NET Framework legado e exige `nuget restore .\src\Vip.DFe.sln` + `msbuild .\src\Vip.DFe.sln` (ver `README.md` na raiz e `docs/conventions/build-and-validation.md`). O antigo `src/README.md` foi removido.

## Arquivos centrais

| Arquivo | Responsabilidade |
|---|---|
| `Program.cs` | Entry point (`Application.Run(new frmPrincipal())`) |
| `frmPrincipal.cs` / `frmPrincipal.Designer.cs` / `frmPrincipal.resx` | Form principal (~1634 linhas em `frmPrincipal.cs`): montagem de NFe, operação NFeService, SAT, DANFE, configuração |
| `Data/JsonRepository.cs` | Persistência genérica JSON (ver `docs/modules/persistence.md`) |
| `Data/ConfiguracaoService.cs` | Carrega/salva `configuracao.json` |
| `Models/Configuracao.cs` | Configuração persistida (versão, ambiente, estado, emitente, destinatário, transporte, certificado, arquivos) |
| `Models/CertificadoDigital.cs`, `Models/Item.cs` | Modelos de apoio da UI (item de nota, certificado) |
| `Models/ItemTributavelDemo.cs` | Adaptador de `Item` para o `Vip.Fiscal` (`IEntidadeProduto`) com percentuais de demonstração (ICMS, PIS/COFINS, IBS/CBS) |
| `Services/TributacaoDemoService.cs` | Calcula a tributação de um item com `Vip.Fiscal` (`TributacaoProduto`/`TributacaoService`) e devolve `ResultadoTributacaoDemo` |
| `Services/ResultadoTributacaoDemo.cs` | DTO com o item, a entidade fiscal, o resultado consolidado e os resultados de IBS UF/município e CBS |
| `Helpers/CertificadoHelper.cs` | Seleção/tratamento de certificado digital (repositório Windows/arquivo) |
| `Helpers/ControlExtensions.cs`, `Helpers/EnumHelper.cs`, `Extensions/*` | Suporte a UI (bindings, combobox por enum, máscaras) |
| `Enums/FuncaoServico.cs` | Enum usado no form para selecionar a função a executar |

## Operação típica na UI

1. Carregar/editar configuração (persistida em `configuracao.json`).
2. Selecionar certificado digital.
3. Montar NFe (emitente, destinatário, itens, pagamentos) — a tributação de cada item é **calculada** pelo `Vip.Fiscal` (`TributacaoDemoService`) e o formulário preenche as classes de imposto da própria lib (`NFeDetImposto`, `IbsCbs`, `NFeIbsCbsTot`).
4. Executar funções da biblioteca: autorização, consulta, cancelamento, CCe, inutilização, status; operações SAT; geração de DANFE/evento.
5. Visualizar XMLs de envio/retorno (via `FastColoredTextBox`).

## Observações

- **`Vip.Fiscal` apenas para cálculo**: a Demo calcula valores com `Vip.Fiscal` e preenche `NFeDetImposto` (ICMS/PIS/COFINS), `IbsCbs` por item e `NFeTotal.IbsCbsTot` com os modelos da lib (`frmPrincipal.cs:~941` monta `new NFeTotal {IcmsTot = icmsTot, IbsCbsTot = GerarIbsCbsTotal(tributacoes)}`). O refactor foi concluído no commit `13e783b` (plano em `docs/superpowers/plans/2026-08-01-vip-fiscal-demo-refactor.md`); guia de consumo do pacote em `docs/GUIA-CONSUMO-VIP-FISCAL.md`.
- **Valores provisórios**: `CClassTrib = "000001"` e `Cst = "000"` do IBS/CBS são placeholders de demonstração (`frmPrincipal.cs:~1384`); não use em produção sem classificação fiscal validada.
- **Sem testes automatizados** para a UI.
- **Não é referência de arquitetura de produção**: é exemplo operacional; padrões (componente com `Configuracoes`/`Documentos`/eventos) vêm da biblioteca, não da Demo.
