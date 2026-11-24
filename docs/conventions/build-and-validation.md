# Compilação e Validação

> Fonte de verdade: arquivos `.csproj`, `.gitea/workflows/nuget.yml`, `.gitea/workflows/ci.yml` e `src/nuget.config`. Itens incertos marcados como **Hipótese a validar**.

## Repositório e CI

- **Origem**: Gitea `ssh://git@dev.vipsolucoes.com:3022/vipsolucoes/vip-dfe.git` (remote `origin`). Pushes, PRs e Actions acontecem aqui.
- **GitHub** (`github.com/leandrovip/Vip.DFe`) é **mirror** público (push mirror configurado no Gitea). Não faça push direto nele. URLs públicas (README, `PackageProjectUrl`/`RepositoryUrl` do csproj) continuam apontando para o GitHub.
- **Gitea Actions** com runners próprios: `vip-linux` (CI) e `vip-windows` (publicação). Os workflows ficam em `.gitea/workflows/`.

## Requisitos

- **Visual Studio 2022** (Windows) ou **.NET SDK 10.0.x**; **MSBuild** + **NuGet CLI** só para a Demo.
- A Demo é projeto **.NET Framework 4.8 legado** (MSBuild não-SDK, `packages.config`) — precisa de `nuget restore` e `msbuild`; **não use `dotnet build` para ela** (não é projeto SDK).
- Biblioteca e testes usam SDK style: `dotnet build`/`dotnet test` funcionam.

## Como compilar

### CI (Gitea Actions `ci.yml`, runner `vip-linux`)

Em `push`/`pull_request` para `master`: `dotnet restore`/`build`/`test` do projeto de testes (que compila a lib). **A Demo não é compilada no CI** (exige Windows/MSBuild). Não publica nada.

> **Hipótese a validar**: os testes de DANFE geram PDF via `Vip.Pdf` → `System.Drawing.Common`, suportado só no Windows a partir do .NET 7. Se falharem no `vip-linux`, mover o `ci.yml` para `vip-windows`.

### Pipeline de publicação (Gitea Actions `nuget.yml`, runner `vip-windows`, manual) — ordem de referência

```bash
dotnet restore src/Vip.DFe.Tests/Vip.DFe.Tests.csproj
dotnet build src/Vip.DFe.Tests/Vip.DFe.Tests.csproj -c Release --no-restore -p:Version=<versão>
dotnet test src/Vip.DFe.Tests/Vip.DFe.Tests.csproj -c Release --no-build
dotnet pack src/Vip.DFe/Vip.DFe.csproj -c Release -o artifacts -p:Version=<versão> -p:PackageVersion=<versão[-beta]>
dotnet nuget push "artifacts/*.nupkg" --source https://api.nuget.org/v3/index.json --api-key <NUGET_TOKEN> --skip-duplicate
```

> Versão publicada é definida no workflow (`1.2.<run_number>`, sufixo `-beta` opcional via input `typeBuild`), não no csproj (`1.0.21`). A série `1.1.x` (até `1.1.46`) foi publicada pelo antigo GitHub Actions; a `1.2` começou com a migração para o Gitea (contador de runs zerado) e com a quebra `CartaoCorrecao` → `CartaCorrecao`. Não edite o csproj para "publicar versão".
>
> Requer o secret `NUGET_TOKEN` configurado no repositório do Gitea.
>
> O pacote inclui `lib/netstandard2.0/Vip.DFe.xml` (`GenerateDocumentationFile`; `CS1591` suprimido) e não expõe `Fody`/`PropertyChanged.Fody`/`ExtraConstraints.Fody` como dependências (`PrivateAssets="all"`).

### Manual (desenvolvimento)

```bash
# Restaurar dependências (Demo legado)
nuget restore .\src\Vip.DFe.sln

# Build completo da solução
msbuild .\src\Vip.DFe.sln /p:Configuration=Debug

# Somente biblioteca
dotnet build .\src\Vip.DFe\Vip.DFe.csproj

# Somente testes e executá-los
dotnet test .\src\Vip.DFe.Tests\Vip.DFe.Tests.csproj
```

## O que validar por tipo de alteração

| Tipo de alteração | Projeto a validar | Validação |
|---|---|---|
| Modelo/domínio NFe, SAT ou serialização | `Vip.DFe` + `Vip.DFe.Tests` | Build + testes + conferência de XML gerado (salvar/`GetXml`) |
| Atributos/serializer/criptografia/chave | `Vip.DFe` + `Vip.DFe.Tests` | Build + testes (`Core/`, `NFe/`) |
| Serviço SEFAZ (URLs, contrato SOAP) | `Vip.DFe` | Build; testes manuais em homologação (não há testes automatizados de integração) |
| DANFE | `Vip.DFe` + `Vip.DFe.Tests` | Build + testes (`Danfe/`); validar visual do PDF |
| SAT | `Vip.DFe` | Build; teste com SAT real/homologado (DLL) |
| Configuração de arquivos/caminhos | `Vip.DFe` | Build; validar criação das pastas/arquivos em execução |
| UI/Demo | `Vip.DFe.Demo` | Build com `msbuild` (não SDK); testes manuais |
| Pacote NuGet | `Vip.DFe` | `dotnet pack` + inspeção do `.nupkg` |

## Sequência recomendada

1. `nuget restore .\src\Vip.DFe.sln`
2. Build da biblioteca (`msbuild`/`dotnet build`).
3. `dotnet test` no projeto de testes.
4. Se alterou contrato/API: revisar `docs/modules/api.md` e consumidores (`Demo`).
5. Se alterou dependência de pacote: rodar `nuget locals all -clear` e restaurar (como o pipeline).

## Incertezas

- **Instalação do .NET Framework 4.8 targeting pack / VS Build Tools** pode ser exigida para compilar a Demo em máquinas sem VS 2022 completo.
- **`dotnet run` para a Demo** não funciona — projeto legado; usar `nuget restore` + `msbuild` (é o que o `README.md` da raiz descreve).
- **Cobertura de testes**: endereços SEFAZ por UF, chave/DV, DANFE e serialização/XSD da NFe (round-trip, opcionais, ordem de tags, assinatura). Não há testes de integração com a SEFAZ nem de SAT.
- **Restore offline**: `src/nuget.config` define apenas `repositoryPath = ..\packages`; as fontes do NuGet são as padrão — a Demo depende de `packages/` local restaurado.
- **`.gitignore`**: os conflitos de merge (`<<<<<<< HEAD`) que existiam foram **resolvidos** (não há mais marcadores no arquivo).
