# Mapa de Projetos

> Fonte de verdade: arquivos `.csproj` em `src/`.

| Projeto | Tipo / Framework | Responsabilidade | Dependências | Observações |
|---|---|---|---|---|
| `src/Vip.DFe` (`Vip.DFe.csproj`) | Class library · `netstandard2.0`, `LangVersion latest`, versão `1.0.21`, MIT | Toda a lógica fiscal: domínio NFe/SAT/DANFE, serialização XML, assinatura, comunicação SEFAZ, integração SAT, DANFE PDF | Fody, PropertyChanged.Fody, ExtraConstraints.Fody, System.Drawing.Common, System.Formats.Asn1, System.Security.Cryptography.Pkcs/Xml, System.ServiceModel.Http/Security, Vip.Pdf | Pasta `3-Infra/Notification` excluída do csproj; pacote NuGet público |
| `src/Vip.DFe.Demo` (`Vip.DFe.Demo.csproj`) | WinForms (WinExe) · .NET Framework 4.8 · MSBuild legado, `packages.config` | Aplicação de demonstração: operar NFe/NFCe/SAT/DANFE pela UI e persistir configuração em JSON | Vip.DFe (project ref), Newtonsoft.Json, FastColoredTextBox, Vip.Extensions, Vip.Fiscal, Vip.Pdf, WCF/security, System.Drawing.Common | Projeto não-SDK (`.csproj` antigo); requer `msbuild` + `nuget restore`; arquivos centrais: `Program.cs`, `frmPrincipal(.Designer).cs`, `Data/JsonRepository.cs`, `Data/ConfiguracaoService.cs`, `Models/*`, `Helpers/CertificadoHelper.cs` |
| `src/Vip.DFe.Tests` (`Vip.DFe.Tests.csproj`) | xUnit · `net6.0` | Testes de unidade | Vip.DFe (project ref), Microsoft.NET.Test.Sdk, xunit, Newtonsoft.Json | Cobertura mínima: `Core/ChaveDFeTests`, `Core/DigitoVerificadorTests`, `NFe/Configuration/NFeConfigTests`, `Danfe/DanfeViewModelCreatorTests` |

## Dependências entre projetos (direção)

```
Vip.DFe.Demo ──► Vip.DFe
Vip.DFe.Tests ─► Vip.DFe
```

## Referências externas relevantes

| Dependência | Usada por | Finalidade |
|---|---|---|
| `Vip.Pdf` (NuGet; empacota `org.pdfclown`) | Vip.DFe, Demo | Geração de PDF (DANFE) |
| `Vip.Extensions` | Demo | Extensões utilitárias |
| `Vip.Fiscal` | Demo | Modelos de imposto usados na UI (a lib tem modelos próprios) |
| `Newtonsoft.Json` | Demo, Tests | Serialização JSON (persistência de configuração / assertions) |
| `Fody` + `PropertyChanged.Fody` + `ExtraConstraints.Fody` | Vip.DFe | Injeção de `INotifyPropertyChanged` e constraints em build-time |
| DLL nativa do SAT | Vip.DFe (runtime) | `C:\SAT\sat.dll` default; P/Invoke dinâmico |
