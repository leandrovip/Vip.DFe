# Compilação e Validação

> Fonte de verdade: arquivos `.csproj`, `.github/workflows/nuget.yml` e `src/nuget.config`. Itens incertos marcados como **Hipótese a validar**.

## Requisitos

- **Visual Studio 2022** (Windows) ou **.NET SDK 6.0.x** + **MSBuild** + **NuGet CLI** (usados pelo pipeline).
- A Demo é projeto **.NET Framework 4.8 legado** (MSBuild não-SDK, `packages.config`) — precisa de `nuget restore` e `msbuild`; **não use `dotnet build` para ela** (não é projeto SDK).
- Biblioteca e testes usam SDK style: `dotnet build`/`dotnet test` funcionam.

## Como compilar

### Pipeline oficial (GitHub Actions `nuget.yml`) — ordem de referência

```bash
msbuild .\src\Vip.DFe.sln /t:Clean /p:Configuration=Release
nuget locals all -clear
nuget restore .\src\Vip.DFe.sln
msbuild .\src\Vip.DFe.sln /p:Configuration=Release /p:Version=<versão>        # build da solução
dotnet vstest src\Vip.DFe.Tests\bin\Release\net6.0\Vip.DFe.Tests.dll          # testes
msbuild .\src\Vip.DFe\Vip.DFe.csproj /t:Pack /p:Configuration=Release /p:PackageVersion=<versão>  # pacote
nuget push .\src\Vip.DFe\bin\Release\*.nupkg -Source https://api.nuget.org/v3/index.json -ApiKey <token>
```

> Versão publicada é definida no workflow (`1.1.<run_number>`), não no csproj (`1.0.21`). Não edite o csproj para "publicar versão" — `Hipótese a validar` se este comportamento deve mudar.

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
| Pacote NuGet | `Vip.DFe` | `msbuild /t:Pack` + inspeção do `.nupkg` |

## Sequência recomendada

1. `nuget restore .\src\Vip.DFe.sln`
2. Build da biblioteca (`msbuild`/`dotnet build`).
3. `dotnet test` no projeto de testes.
4. Se alterou contrato/API: revisar `docs/modules/api.md` e consumidores (`Demo`).
5. Se alterou dependência de pacote: rodar `nuget locals all -clear` e restaurar (como o pipeline).

## Incertezas

- **Instalação do .NET Framework 4.8 targeting pack / VS Build Tools** pode ser exigida para compilar a Demo em máquinas sem VS 2022 completo.
- **`dotnet run` para a Demo** (README) não funciona — projeto legado; usar MSBuild.
- **Cobertura de testes é mínima** (4 arquivos); não espere regressão garantida fora das áreas cobertas.
- **Restore offline**: `src/nuget.config` define apenas `repositoryPath = ..\packages`; as fontes do NuGet são as padrão — a Demo depende de `packages/` local restaurado.
- **`.gitignore` contém marcadores de merge não resolvidos** (`<<<<<<< HEAD`) — o arquivo é válido, mas está bagunçado; `Hipótese a validar` antes de editar regras de ignore.
