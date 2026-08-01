# Visão Geral da Solução — Vip.DFe

> Código-fonte é a fonte de verdade. Documentação inicial; itens incertos marcados como **Hipótese a validar**.

## Objetivo

Biblioteca .NET para emissão e gestão de Documentos Fiscais Eletrônicos (DFe) brasileiros:

- **NFe / NFCe** — montagem, assinatura, envio/consulta/cancelamento via webservices SEFAZ e geração de DANFE.
- **CFe-SAT** — comunicação com o SAT (DLL nativa) para emissão/cancelamento de cupons e gestão do equipamento.

## Stack

| Item | Valor |
|---|---|
| Solução | `src/Vip.DFe.sln` (VS 2022) |
| Biblioteca | `Vip.DFe` — `netstandard2.0`, `LangVersion latest`, versão csproj `1.0.21`, MIT |
| Demo | `Vip.DFe.Demo` — WinForms .NET Framework 4.8 (projeto MSBuild legado, `packages.config`) |
| Testes | `Vip.DFe.Tests` — xUnit `net6.0` |
| Publicação | NuGet (`Vip.DFe`), pipeline GitHub Actions em `.github/workflows/nuget.yml` |
| Idiomas | Mensagens de erro/validação em PT-BR |

## Projetos

| Projeto | Tipo | Papel |
|---|---|---|
| `src/Vip.DFe` | Class library (netstandard2.0) | Toda a lógica fiscal: domínio, serviços, serialização XML, criptografia, comunicação, DANFE |
| `src/Vip.DFe.Demo` | WinForms (.NET Framework 4.8) | Aplicação de demonstração operacional da biblioteca |
| `src/Vip.DFe.Tests` | xUnit (net6.0) | Testes de unidade (cobertura mínima — ver abaixo) |

## Resumo da arquitetura da biblioteca

A lib `Vip.DFe` é organizada em pastas numeradas (espelho dos namespaces) com as seguintes responsabilidades:

| Pasta | Responsabilidade |
|---|---|
| `0-Domain` | Modelos fiscais: NFe, SAT (CFe), DANFE (ViewModels/Blocos/Elementos) e enums compartilhados |
| `1-Services` | Orquestradores (`NFeService`, `CFeService`, `DanfeService`, `DanfeEventoService`), servs SEFAZ (SOAP) e configuração |
| `2-Core` | Infraestrutura de núcleo: atributos, `DFeSerializer`, assinatura (`SigningManager`), `DFeDocument<T>`, cliente WCF, `ChaveDFe`, certificado |
| `3-Infra` | Suporte: exceções (`VipException`/`Guard`), gráficos (Gfx/Fonte), interops nativos, Writer |
| `schemas/` (raiz) | XSDs NFe/NFCe 4.00, eventos 1.00 e XMLDSig usados na validação |

Ponto de entrada da API: os 4 orquestradores de `1-Services` (ver `docs/modules/api.md`).

## Como navegar

1. **Entender a API pública** → `docs/modules/api.md`.
2. **Entender o domínio** → `docs/modules/domain-model.md` e código em `0-Domain/`.
3. **Entender orquestração/fluxos** → `docs/modules/application-services.md` e `docs/flows/main-business-flows.md`.
4. **Integrações externas (SEFAZ/SAT/PDF)** → `docs/flows/integration-flows.md`.
5. **Convenções de código** → `docs/conventions/naming-and-patterns.md`.
6. **Compilar/validar** → `docs/conventions/build-and-validation.md`.
7. **Operar com a Demo** → `docs/modules/ui-winforms.md`.

## Hipóteses a validar

- **Versão publicada vs. versão do código**: csproj marca `1.0.21`, mas o workflow `nuget.yml` publica `1.1.<run_number>`. A versão real no NuGet pode divergir do csproj.
- **Vip.Pdf / PDFClown**: `Vip.Pdf` (NuGet, MIT do projeto) empacota `org.pdfclown`; origem/licença do PDFClown embutido a confirmar.
- **Cobertura de endpoints SEFAZ**: `NFeEnderecoCollection.cs` cobre os 27 estados + SVAN/SVRS/SVC (homologação e produção), mas a efetividade operacional de cada endpoint precisa ser validada em ambiente real.
- **`3-Infra/Notification`**: removido do csproj e sem pasta no disco — é legado a limpar.
- **Assinatura padrão SHA-1**: `SigningManager` usa `SignDigest.SHA1` como default (`Hipótese a validar` se é adequado à legislação/SEFAZ atual).
- **Cobertura de testes mínima**: apenas 4 arquivos de teste (ChaveDFe, DigitoVerificador, NFeConfig, DanfeViewModelCreator).
