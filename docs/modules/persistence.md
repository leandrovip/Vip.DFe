# Persistência

> Fonte de verdade: código em `src/Vip.DFe` (configuração de arquivos) e `src/Vip.DFe.Demo` (JSON). Itens incertos marcados como **Hipótese a validar**.

## Visão geral

**Não existe banco de dados relacional.** A persistência é:

1. **XML em disco** — na biblioteca, para documentos fiscais (envio/assinado/retorno/autorizado/inutilizado/eventos).
2. **JSON** — na aplicação Demo, para a configuração do usuário.

## Biblioteca (XML em disco)

### Organização

`NFeConfigArquivo` (`1-Services/NFe/Configuration/NFeConfigArquivo.cs`) monta caminhos no padrão:

```
<Diretorio>/<CNPJ>/<Modelo>/<Fase>[/<yyyyMM>]/
```

- Fases: `Enviado`, `Assinado`, `Retorno`, `Autorizado`, `Inutilizado` (métodos `ObterCaminhoEnviado/Assinado/Retorno/Autorizado/Inutilizado`).
- **A subpasta `yyyyMM` existe apenas na fase `Autorizado`** (`ObterCaminhoAutorizado(DateTime data, ...)` passa a data; as demais fases chamam `ObterCaminho` sem data — `NFeConfigArquivo.cs:~43-67` e `:97`). `procNFe`/`procEvento` usam a data de emissão (`DhEmi`) / de registro do evento (`DhRegEvento`).
- `CNPJ` (de `NFeConfig.CNPJ`) e `Modelo` (descrição de `NFeModelo`) entram no caminho quando não vazios; os diretórios são criados se não existirem.
- `Diretorio` default: `<pasta do assembly>\NFe` (`Assembly.GetExecutingAssembly().GetPath()` + `\NFe`); `DiretorioSchemas` default: `<pasta do assembly>\NFe\Schemas`. Se `Diretorio` ficar vazio, `ObterCaminho` usa `AppDomain.CurrentDomain.BaseDirectory\NFe`.
- `DiretorioAutorizadasBackup` é opcional: quando preenchido, cópia do `procNFe`/`procEvento` (e do XML de inutilização) é gravada no mesmo padrão sob esse diretório. No `NFeProc.Gravar` a cópia só ocorre se o diretório de backup já existir (`Directory.Exists`).
- Separação por CNPJ/mês no SAT é configurada em `SatArquivos` (flags `SepararPorCNPJ`/`SepararPorMes`).

### Fluxo de gravação

- Antes do envio: o XML assinado é gravado em `Assinado` por `NFeServBase.GravarXmlAssinado` (`NFeServBase.cs:~47`), se `Arquivos.Salvar`.
- Durante a chamada SOAP: `ServiceClient.GravarSoap` (`ServiceClient.cs:~94-112`) grava o envelope enviado em `Enviado` e a resposta em `Retorno` (nome `yyyyMMddHHmmssfff-<ArquivoEnvio|ArquivoResposta>.xml`), se `Arquivos.Salvar`.
- Após sucesso (SEFAZ): `NFeService.GerarNFeProc` monta `NFeProc` e chama `NFeProc.Gravar(config)` (pasta `Autorizado`); eventos montam `NFeProcEvento` e gravam via `procEvento.Gravar(config)`. A inutilização é gravada em `Inutilizado` por `GravarXmlInutilizacao`. Exemplos de arquivos gerados: `procNFe`, `procEvento`.
- SAT: `SatArquivos` define pastas de venda/cancelamento e prefixos dos arquivos gravados pelo `CFeService`.

### Schemas

- XSDs ficam em `schemas/` na raiz; `NFeConfigArquivo.ObterSchema(NFeSchema)` resolve o nome do arquivo (ex.: `nfe_v4.00.xsd`, `procNFe_v4.00.xsd`, `enviNFe_v4.00.xsd`, `envEventoCancNFe_v1.00.xsd`) com cache em `SchemasCache`.
- Validação dos documentos antes do envio: `NFeCollection.Validar()`.

### Padrões / transações

- **Sem transações**: cada arquivo é gravado de forma atômica individual. Não há garantia transacional entre múltiplos arquivos (ex.: XML de envio + procNFe).
- **Sem repositórios/DI na lib**: os modelos de domínio gravam diretamente (`NFeProc.Gravar`) e os serviços conhecem os caminhos — acoplamento direto a `System.IO`.

## Demo (JSON)

| Arquivo | Responsabilidade |
|---|---|
| `Data/JsonRepository.cs` | Repositório genérico `JsonRepository<T>`: `Save`/`Load` serializando com `Newtonsoft.Json` em arquivo no `BaseDirectory` |
| `Data/ConfiguracaoService.cs` | Fachada sobre `JsonRepository<Configuracao>` persistindo `configuracao.json` |
| `Models/Configuracao.cs` | DTO de configuração (NFeService, certificado, arquivos, emitente/destinatário/transporte/volume) |

## Riscos e acoplamentos

- **Ausência de camada de persistência abstraída**: mudanças em caminhos/padrões de arquivo impactam serviços e domínio.
- **Sem transação/rollback** entre "gravar envio" e "gravar procNFe"; recuperação depende do operador.
- **Schemas apontados para disco do executável**: `DiretorioSchemas` default aponta para a pasta do assembly; em deploy, os XSDs precisam acompanhar o produto.
- **Criptografia/senha**: `configuracao.json` da Demo armazena `CertificadoSenha` em texto puro (`Configuracao.cs`; `JsonRepository<T>` apenas serializa com `Newtonsoft.Json`, sem criptografia) — fato confirmado; não reutilize essa abordagem em produção.
