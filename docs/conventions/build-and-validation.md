# Compilação e Validação

> Fonte de verdade: arquivos `.csproj`, `.gitea/workflows/ci.yml`, `.github/workflows/nuget.yml` e `src/nuget.config`. Itens incertos marcados como **Hipótese a validar**.

## Repositório e CI

- **Origem**: Gitea `ssh://git@dev.vipsolucoes.com:3022/vipsolucoes/vip-dfe.git` (remote `origin`). Pushes, PRs e CI acontecem aqui.
- **GitHub** (`github.com/leandrovip/Vip.DFe`) é **mirror** público (push mirror configurado no Gitea). Não faça push direto nele. URLs públicas (README, `PackageProjectUrl`/`RepositoryUrl` do csproj) continuam apontando para o GitHub.
- **CI**: Gitea Actions, `.gitea/workflows/ci.yml`, runner `vip-linux`. O Gitea lê só `.gitea/workflows/` (ignora `.github/workflows/` quando aquela existe).
- **Publicação**: GitHub Actions **no mirror**, `.github/workflows/nuget.yml`, **somente manual** (`workflow_dispatch`), com **Trusted Publishing** do nuget.org (OIDC, sem API key). Motivo: o Trusted Publishing só aceita GitHub Actions e GitLab CI, não Gitea.
- **Submódulo `referencias/`**: repo privado `ssh://git@dev.vipsolucoes.com:3022/vipsolucoes/dfe-referencia.git` (NTs e schemas oficiais, ver skill `nt-referencias`). Clone com `git clone --recurse-submodules` ou rode `git submodule update --init referencias`. **Não é necessário para compilar**: `ci.yml` e `nuget.yml` usam `actions/checkout@v4` sem `submodules`, então não o baixam (no mirror público do GitHub o link do submódulo aparece, mas é inacessível).

## Requisitos

- **Visual Studio 2022** (Windows) ou **.NET SDK 10.0.x**; **MSBuild** + **NuGet CLI** só para a Demo.
- A Demo é projeto **.NET Framework 4.8 legado** (MSBuild não-SDK, `packages.config`) — precisa de `nuget restore` e `msbuild`; **não use `dotnet build` para ela** (não é projeto SDK).
- Biblioteca e testes usam SDK style: `dotnet build`/`dotnet test` funcionam.

## Como compilar

### CI (Gitea Actions `ci.yml`, runner `vip-linux`)

Em `push` para `master` (não roda em pull request): `dotnet restore`/`build`/`test` do projeto de testes (que compila a lib). **A Demo não é compilada no CI** (exige Windows/MSBuild). Não publica nada.

> Testes que geram PDF do DANFE usam `[WindowsFact]` (`src/Vip.DFe.Tests/WindowsFactAttribute.cs`) e são **ignorados no Linux**: o `Vip.Pdf` usa `System.Drawing.Common`, que no .NET 7+ lança `PlatformNotSupportedException` fora do Windows (confirmado no `vip-linux`). Eles rodam localmente no Windows e no `nuget.yml` (`windows-latest`). O mesmo vale para quem consome a lib: **geração de PDF do DANFE só funciona no Windows**.

### Pipeline de publicação (GitHub Actions no mirror, `nuget.yml`, `windows-latest`, manual) — ordem de referência

```bash
dotnet restore src/Vip.DFe.Tests/Vip.DFe.Tests.csproj
dotnet build src/Vip.DFe.Tests/Vip.DFe.Tests.csproj -c Release --no-restore -p:Version=<versão>
dotnet test src/Vip.DFe.Tests/Vip.DFe.Tests.csproj -c Release --no-build
dotnet pack src/Vip.DFe/Vip.DFe.csproj -c Release -o artifacts -p:Version=<versão> -p:PackageVersion=<versão[-beta]>
# NuGet/login@v1 troca o token OIDC do GitHub por uma API key temporária (~1 h)
dotnet nuget push "artifacts/*.nupkg" --source https://api.nuget.org/v3/index.json --api-key <chave temporária do NuGet/login> --skip-duplicate
```

> Versão publicada é definida no workflow (`1.2.<run_number>`, sufixo `-beta` opcional via input `typeBuild`), não no csproj (`1.0.21`). O `run_number` do `nuget.yml` no GitHub continua a contagem da série `1.1.x` (até `1.1.46`); a `1.2` marca as quebras `CartaoCorrecao` → `CartaCorrecao` e `DPrevEntrega` → `DateTime?`. Não edite o csproj para "publicar versão".
>
> Pré-requisitos: política de **Trusted Publishing** no nuget.org (Owner `leandrovip`, Repository `Vip.DFe`, Workflow File `nuget.yml`) e secret `NUGET_USER` (nome do perfil nuget.org) no repositório GitHub. O `workflow_dispatch` só fica disponível quando o arquivo está na branch padrão (`master`) do GitHub.
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
