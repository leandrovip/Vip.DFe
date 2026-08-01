# AGENTS.md

Guia operacional para agentes que trabalham neste repositório. Leia `docs/` antes de editar código.

## O que é

`Vip.DFe` — biblioteca .NET (`netstandard2.0`) para emissão de NFe/NFCe e CFe-SAT, com geração de DANFE. Distribuída via NuGet. Documentação técnica em `docs/` (PT-BR).

## Arquitetura em resumo

```
src/
├── Vip.DFe/        # lib principal
│   ├── 0-Domain/   # modelos fiscais NFe/SAT/DANFE + enums Shared
│   ├── 1-Services/ # NFeService, CFeService, DanfeService, DanfeEventoService + servs SEFAZ + config
│   ├── 2-Core/     # atributos, DFeSerializer, SigningManager, DFeDocument, WCF, ChaveDFe, certificado
│   └── 3-Infra/    # VipException/Guard, Gfx/Fonte, interops nativos, Writer
├── Vip.DFe.Demo/   # WinForms .NET Framework 4.8 (MSBuild legado, packages.config) — EXEMPLO, não lib
├── Vip.DFe.Tests/  # xUnit net6.0 (cobertura mínima)
└── Vip.DFe.sln
schemas/            # XSDs NFe 4.00 / eventos 1.00 / xmldsig (validação em runtime)
packages/           # dependências locais da Demo (nuget.config aponta para cá)
```

- Pasta numerada = camada; namespace espelha a pasta.
- Entidades de domínio = POCOs com `[DFeRoot]`/`[DFeElement]` (Id/Ocorrencia/Ordem/Min/Max) + `INotifyPropertyChanged` via Fody.
- API pública = `NFeService`, `CFeService`, `DanfeService`, `DanfeEventoService` + modelos `DFeDocument<T>`.
- Sem banco: persistência em XML no disco (NFe/SAT) e JSON na Demo.
- Sem Web API; integração SEFAZ via SOAP/WCF; SAT via DLL nativa; PDF via Vip.Pdf.

## Regras de navegação

- `docs/architecture/*` → visão geral, camadas, projetos.
- `docs/modules/*` → domínio, serviços, persistência, API, UI.
- `docs/flows/*` → fluxos de negócio e integrações.
- `docs/conventions/*` → nomenclatura/padrões e build.
- Incertos estão marcados como **Hipótese a validar** — não trate como fato.
- Código-fonte é a fonte de verdade: confirme no código antes de afirmar.

## Antes de editar

- **Mapeie o fluxo**: identifique pontos de entrada (serviços), camadas envolvidas e onde o XML/arquivos são gerados.
- **Alterações mínimas**: não faça refatorações amplas; faça a mudança mais local possível.
- **Preserve contratos públicos**: assinaturas e namespaces de métodos públicos são consumidos por clientes do NuGet; não quebre sem necessidade explícita.
- Se algo estiver incerto no código, marque como `Hipótese a validar` na documentação, não invente.
- Para alterações de leiaute XML de NF-e/NFC-e/SAT/eventos baseadas em Nota Técnica, schemas ou especificação de campos, use a skill `nfe-xml-nota-tecnica` antes de implementar.

## Tarefas multi-camada (exemplos)

| Mudança | Onde mexer |
|---|---|
| Campo novo no leiaute NFe | Modelo em `0-Domain/NFe/NotaFiscal/...` + atributos; validar XSD em `schemas/`; testes |
| Nova operação SEFAZ | Serviço em `1-Services/NFe/Serv*` (Request/Response/Resposta + interface `INFeXxx`) + endpoint em `NFeEnderecoCollection` + método em `NFeService` |
| Mudança em serialização/assinatura | `2-Core/Serializer` / `2-Core/Cryptography` — impacto em toda a lib |
| DANFE | `0-Domain/Danfe` (Modelo/Blocos/Elementos) + `1-Services/Danfe` |
| Configuração de arquivos | `1-Services/NFe/Configuration/NFeConfigArquivo.cs` ou `SatArquivos` |
| Demo | `Vip.DFe.Demo` — projeto legado, compilar com `msbuild`, não `dotnet run` |

## Compilação e validação

```bash
nuget restore .\src\Vip.DFe.sln
msbuild .\src\Vip.DFe.sln /p:Configuration=Debug        # solução completa (inclui Demo)
dotnet test .\src\Vip.DFe.Tests\Vip.DFe.Tests.csproj    # testes (só Vip.DFe)
dotnet build .\src\Vip.DFe\Vip.DFe.csproj               # só a lib
```

- Compile os projetos impactados primeiro; rode testes ao tocar em `0-Domain`, `2-Core`, DANFE ou testes.
- Não altere a versão no csproj: a versão publicada é definida pelo workflow `nuget.yml` (`1.1.<run_number>`).

## Ignorar

- `bin/`, `obj/`, `packages/`, `.vs/`, `.user`, `.DotSettings.user`, artefatos de build.
- Documentação temporária/gerada não solicitada — não crie `.md` além do necessário.
- `3-Infra/Notification` está excluído do csproj — não restaure/reintroduza sem aval.

## Observações WinForms/API

- `Vip.DFe.Demo` é WinForms .NET Framework 4.8 legado (packages.config, MSBuild antigo); usa `Vip.Fiscal`/`Vip.Extensions` — modelos de imposto duplicam os da lib.
- `NFeService`/`CFeService` são `sealed`, herdam `VipComponent` (configurações/documentos/eventos), mensagens PT-BR, `Guard.Against<VipException>`.
- Respostas SEFAZ seguem o padrão `XxxResposta` com `XmlEnvio`/`XmlRetorno`/`EnvelopeSoap`/`RetornoWS`/`Resultado`.
