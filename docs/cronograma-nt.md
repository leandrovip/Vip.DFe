# Cronograma das Notas Técnicas NF-e/NFC-e

> Datas de homologação e produção das NTs que afetam o **Vip.DFe**. Complementa [`notas-tecnicas.md`](notas-tecnicas.md), que registra o **status de implementação** de cada NT.
> Atualize junto com a captura de NTs (skill [`nt-referencias`](../.claude/skills/nt-referencias/SKILL.md)). Datas **a confirmar** ainda não foram conferidas no PDF oficial em `referencias/nt/`.

## Próximos marcos

| Data | Ambiente | NT | Marco | Obrigatório? | Status na lib | Fonte |
|---|---|---|---|---|---|---|
| **14/12/2026** | Produção | 2026.005 (v1.00) | IBS/CBS no Simples Nacional (`tpRegimeIBSCBS`, `cAtivSN`, `gTribSN`) | a confirmar | Pendente | Aviso de 12/11/2026 (resumo de fornecedor) — confirmar no PDF |
| **14/12/2026** | Produção | 2026.008 (v1.01) | Campos `vUnComLiq`, `vProdLiq`, `vProdLiqTot` | Não (opcionais) | Pendente | Resumo de fornecedor — confirmar no PDF |
| 01/02/2027 | Homologação | 2026.008 (v1.01) | Regra NB01-30: grupo `ICMSPrevistoPagtoAntecip` obrigatório se `tpNFDebito = 06` ou `tpOperGov = 4` | Condicional | Pendente | Resumo de fornecedor — confirmar no PDF |
| 01/03/2027 | Produção | 2026.008 (v1.01) | Regra NB01-30 (idem) | Condicional | Pendente | Resumo de fornecedor — confirmar no PDF |
| 2027 (a confirmar) | Produção | 2026.006 (v1.01) | Split payment (`gPgtoVinc`, evento 110300) | Não (facultativo) | Pendente | Resumo de fornecedor |
| a confirmar | Produção | 2026.004 (v1.01) | CNPJ alfanumérico | Sim (emitentes com CNPJ novo) | Parcial | — |
| a confirmar | — | 2025.002-RTC (v1.53) | Regras de referência em devolução/complemento (NFref ou `DFeReferenciado`) | a confirmar | Parcial | Publicada em 30/10/2026 |
| a confirmar | — | 2026.007 (v1.11) | Emissão por contribuinte sem IE | Não (facultativa) | Pendente | — |

## Marcos já vigentes

| Data | Ambiente | NT | Marco | Status na lib |
|---|---|---|---|---|
| 01/09/2025 | Produção | 2025.001 | Envio síncrono obrigatório para NF-e única | Parcial (síncrono não é padrão: `NFeConfig.EnviarModoSincrono = false`) |
| 15/06/2026 | Homologação | 2026.004 (v1.01) | CNPJ alfanumérico | Parcial | 

## Pacotes de schema publicados

| Pacote | NTs | Publicado em | No repositório (`schemas/`) |
|---|---|---|---|
| PL_010h v1.03 | 2026.005 v1.00, 2026.006 v1.00, 2026.008 v1.01 | 13/11/2026 | Não |
| PL_010g v1.03 | 2026.006 v1.00, 2026.008 v1.00 | 26/10/2026 | Não |
| PL_010f v1.04 | 2025.002 v1.50, 2026.007 v1.00 | 31/08/2026 | Não |
| PL_010e v1.02 | 2025.002 v1.40, 2026.002 v1.0, 2026.003 v1.0 | 10/07/2026 | Não |
| PL_010d v1.03 | 2026.004 v1.01 (CNPJ alfanumérico) | 10/07/2026 | Não (a conferir: o XSD local já aceita CNPJ alfanumérico) |

> Fonte dos pacotes: página "Esquemas XML" do Portal Nacional da NF-e, consultada em 2026-11-26. Qual PL deu origem aos XSD atuais de `schemas/` não está registrado — **a conferir** comparando com `referencias/schemas/`.
