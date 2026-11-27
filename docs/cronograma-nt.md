# Cronograma das Notas Técnicas NF-e/NFC-e

> Datas de homologação e produção das NTs que afetam o **Vip.DFe**. Complementa [`notas-tecnicas.md`](notas-tecnicas.md), que registra o **status de implementação** de cada NT.
> Atualize junto com a captura de NTs (skill [`nt-referencias`](../.claude/skills/nt-referencias/SKILL.md)). Datas **a confirmar** ainda não foram conferidas no PDF oficial em `referencias/nt/`.

## Próximos marcos

| Data | Ambiente | NT | Marco | Obrigatório? | Status na lib | Fonte |
|---|---|---|---|---|---|---|
| **14/12/2026** | Produção | 2026.005 (v1.00) | Leiaute IBS/CBS no Simples Nacional (`tpRegimeIBSCBS`, `cAtivSN`, `gTribSN`) | Não (campos opcionais) | Em implementação (`feat/nt-2026-005-simples-nacional`) | NT: homologação até 26/10/2026, produção 16/11/2026 — **adiada para 14/12/2026** pelo Aviso SVRS 3022 (12/11/2026) |
| **14/12/2026** | Produção | 2026.008 (v1.01) | Campos `vUnComLiq`, `vProdLiq`, `vProdLiqTot`, grupo `ICMSPrevistoPagtoAntecip` | Não (opcionais) | Em implementação (idem) | NT v1.01: produção 16/11/2026 — adiada para 14/12/2026 (Aviso SVRS 3022) |
| **14/12/2026** | Produção | 2026.006 (v1.01) | Split payment (`gPgtoVinc`, evento 110300) | Não (facultativo) | Pendente (fora de escopo; tag já no XSD do PL_010h) | Aviso SVRS 3022 |
| **14/12/2026** | Produção | 2026.002 (v1.11) | RV BA02-35, VC02-40, I08-180: NF-e de saída não pode referenciar NFC-e (mod. 65), CF-e (mod. 59) ou NF-e com `tpImp=6`, salvo NF-e complementar | Sim | Validação da SEFAZ (sem impacto no leiaute) | NT 2026.002 v1.11, histórico |
| **01/12/2026** | Produção | 2026.010 (v1.00) | DANFE com tributos da RTC (quadro IBS/CBS/IS, CRT no emitente, IBS/CBS/IS por item) | Leiaute de impressão | Em implementação (`feat/danfe-rtc`; QR Code mod. 55 aguarda NT) | NT 2026.010, cronograma |
| 01/02/2027 | Homologação | 2026.008 (v1.01) | Regra NB01-30: `ICMSPrevistoPagtoAntecip` obrigatório se `tpNFDebito = 06` ou `tpOperGov = 4` | Condicional | Em implementação (grupo) | NT 2026.008 v1.01, observação 1 |
| 01/03/2027 | Produção | 2026.008 (v1.01) | Regra NB01-30 (idem) | Condicional | Em implementação (grupo) | NT 2026.008 v1.01, observação 2 |
| **01/03/2027** | Produção | 2026.005 | Simples Nacional passa a emitir NF-e com IBS/CBS (venda de mercadorias) | Sim (optantes do SN) | — | Ato Conjunto RFB/CGIBS nº 7/2026 (DOU 13/11/2026; antes 01/01/2027) |

## Marcos já vigentes

| Data | Ambiente | NT | Marco | Status na lib |
|---|---|---|---|---|
| 01/09/2025 | Produção | 2025.001 (v1.01) | QR Code v3 da NFC-e disponível (opcional para PJ; orientado para produtor rural PF, exceto PR) | Implementado como opção (`NFeConfig.NFCeVersaoQrCode`, padrão v2) |
| 13/10/2025 | Produção | 2025.001 (v1.02) | Rej. 452: resposta assíncrona proibida para lote com uma única NF-e (GAP03a-3) | Implementado (`NFeConfig.EnviarModoSincrono = true` por padrão) |
| até 15/06/2026 | Homologação | 2026.004 (v1.01) + NT Conjunta DF-e 2025.001 | CNPJ alfanumérico (schemas de CNPJ/chave/Id, DV da chave com ASCII−48, código de barras CODE-128 C/A) | Implementado (NT 2026.004 v1.01, cronograma; a NTCJ 2025.001 v1.00 previa 06/04/2026) |
| **01/07/2026** | Produção | 2026.004 (v1.01) + NT Conjunta DF-e 2025.001 | CNPJ alfanumérico (idem) | Implementado (NT 2026.004 v1.01, cronograma; a NTCJ 2025.001 v1.00 previa 06/07/2026) |
| **16/11/2026** | Produção | 2025.002-RTC (v1.53) | Referência em devolução: `NFref` no cabeçalho **ou** `DFeReferenciado` no item (VC02-14/VC02-20; homologação até 09/11/2026) — não adiada pelo Aviso SVRS 3022 | Suportado (modelo aceita as duas formas; validação com o consumidor) |
| **16/11/2026** | Produção | 2026.007 (v1.00/v1.10) | Emissão por contribuinte sem IE (facultativa) e validações LCC-RFB/CCC; em homologação, CNPJ alfanumérico não rejeita 181/182–186 | Pendente (`emit/IE` opcional só no XSD do PL_010h; domínio exige IE) — não foi adiada pelo Aviso SVRS 3022 |

## Pacotes de schema publicados

| Pacote | NTs | Publicado em | No repositório (`schemas/`) |
|---|---|---|---|
| PL_010h v1.03 | 2026.005 v1.00, 2026.006 v1.00, 2026.008 v1.01 | 13/11/2026 | Em implementação: `leiauteNFe` e `DFeTiposBasicos` (inclui o acumulado de 010f/010g) |
| PL_010g v1.03 | 2026.006 v1.00, 2026.008 v1.00 | 26/10/2026 | Não |
| PL_010f v1.04 | 2025.002 v1.50, 2026.007 v1.00 | 31/08/2026 | Não |
| PL_010e v1.02 | 2025.002 v1.40, 2026.002 v1.0, 2026.003 v1.0 | 10/07/2026 | Não |
| PL_010d v1.03 | 2026.004 v1.01 (CNPJ alfanumérico) | 10/07/2026 | Parcial: `leiauteInutNFe` e `leiauteConsSitNFe` copiados; padrões de CNPJ/chave/Id de eventos aplicados em `tiposBasico_v1.03` e nos leiautes de CC-e/cancelamento/substituição |

> Fonte dos pacotes: página "Esquemas XML" do Portal Nacional da NF-e, consultada em 2026-11-26. Antes da NT 2026.005, `leiauteNFe`/`DFeTiposBasicos`/`tiposBasico_v4.00` de `schemas/` eram idênticos (ignorando fim de linha) ao **PL_010e v1.02** — conferido em 2026-11-26.
