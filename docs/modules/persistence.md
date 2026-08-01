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
<Diretorio>/<CNPJ>/<Modelo>/<Fase>/<yyyyMM>/
```

- Fases: `Enviado`, `Assinado`, `Retorno`, `Autorizado`, `Inutilizado` (métodos `ObterCaminhoEnviado/Assinado/Retorno/Autorizado/Inutilizado`).
- `Diretorio` default: `<BaseDirectory>\NFe`; `DiretorioSchemas` default: `<BaseDirectory>\NFe\Schemas`; `DiretorioAutorizadasBackup` opcional.
- Separação por CNPJ e mês é automática no padrão de caminho (a Demo/SAT também suporta flags `SepararPorCNPJ`/`SepararPorMes` — `SatArquivos`).

### Fluxo de gravação

- Antes do envio: documentos assinados são gravados pelo `NFeService`/modelos via `DFeDocument<T>.Save`/`GetXml`.
- Após sucesso (SEFAZ): `NFeService.GerarNFeProc` monta `NFeProc` e chama `NFeProc.Gravar(config)`; eventos montam `NFeProcEvento` e gravam via `procEvento.Gravar(config)`. Exemplos de arquivos gerados: `procNFe`, `procEvento`.
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
- **Criptografia/senha**: `configuracao.json` da Demo armazena dados de certificado sem criptografia aparente (`Hipótese a validar`).
