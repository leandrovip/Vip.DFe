# Notas Técnicas NF-e/NFC-e — changelog e compatibilidade

> Situação do **Vip.DFe** frente às Notas Técnicas (NT) do leiaute **NF-e/NFC-e 4.00** (modelos 55/65).
> Levantamento de **2026-11-24** (branch `fix/varredura-bugs-docs`). Atualize este arquivo a cada NT implementada.
>
> **Fontes**: lista de NTs do espelho oficial SVRS (`dfe-portal.svrs.rs.gov.br/Nfe/Documentos`) e PDFs do Portal Nacional quando acessíveis; versões/datas **não foram conferidas PDF a PDF** — confirme no [Portal Nacional da NF-e](https://www.nfe.fazenda.gov.br/portal/listaConteudo.aspx?tipoConteudo=04BIflQt1aY=) antes de planejar.
> **Evidência de código**: schemas em `schemas/` e modelos em `src/Vip.DFe/0-Domain/NFe` (busca pelo nome da tag nos atributos `[DFeElement]`/`[DFeCollection]`).
> Para implementar uma NT use a skill [`.claude/skills/nfe-xml-nota-tecnica`](../.claude/skills/nfe-xml-nota-tecnica/SKILL.md).

## Legenda

| Status | Significado |
|---|---|
| **Implementado** | Tags/eventos/regras da NT presentes no domínio, serializados e cobertos pelo XSD local |
| **Parcial** | Parte da NT implementada; itens faltantes listados |
| **Pendente** | Nada (ou quase nada) implementado |
| **N/A** | NT só altera regras de validação/processamento da SEFAZ, prazos ou impressão sem impacto no XML gerado |
| **A analisar** | NT recente cujo conteúdo ainda não foi mapeado contra o código |

## Situação por NT

| NT (versão de referência) | Tema | Status | Evidência / o que falta |
|---|---|---|---|
| 2016.002 (v1.61) | Leiaute 4.00 (FCP, `pag`/`detPag`, `rastro`, `cBenef`, `indEscala`/`CNPJFab`) | **Implementado** | `NFeDetProduto.cs` (`indEscala`, `CNPJFab`, `cBenef`, `rastro`), `NFeIcmsTot.cs` (`vFCP`, `vFCPST`, `vFCPUFDest`), `NFePagamento.cs` |
| 2018.005 (v1.52) | Responsável técnico (`infRespTec`, CSRT) | **Implementado** | `infNFe.cs:224` (`infRespTec`) |
| 2019.001 (v1.62) | Regras de validação | **N/A** | Só regras SEFAZ |
| 2020.001 (v1.50) | Manifestação do destinatário (eventos 2102xx) | **Pendente** | Sem eventos 210200/210210/210220/210240 nem serviço de recepção no Ambiente Nacional |
| 2020.005 (v1.20) | Regras de validação | **N/A** | — |
| 2020.006 (v1.30) | Intermediador (`indIntermed` B25c, `infIntermed` YB01) | **Implementado** | `NFeIde.cs:182`, `infNFe.cs:194` |
| 2020.007 (v1.01) | Evento de informação do transportador | **Pendente** | Evento não modelado (código do evento a confirmar na NT) |
| 2021.001 (v1.01) | Comprovante de entrega (eventos 110130/110131) | **Pendente** | Eventos não modelados |
| 2021.002 (v1.12) | Nota Fiscal Fácil – NFF | **Parcial** | `tpEmis = 3` existe no XSD; faltam `infSolicNFF/xSolic`, `infProdNFF` (`cProdFisco`, `cOperNFF`) e `infProdEmb` (`xEmb`, `qVolEmb`, `uEmb`). Baixa prioridade (uso por app NFF) |
| 2021.004 (v1.30) | Prazos de implantação | **N/A** | — |
| 2022.001 | Consulta GTIN | **N/A** | Serviço externo; não há cliente na lib |
| 2022.002 (v1.30) | Equiparação à exportação e outras alterações | **A analisar** | — |
| 2022.003 (v1.10) | Novos campos e regras | **A analisar** | — |
| 2022.004 / 2022.005 | Regras ISSQN / DIFAL | **N/A** | Regras; grupo `ICMSUFDest` já existe (`NFeDetImposto.cs:94`) |
| 2023.001 (v1.5x) | ICMS monofásico combustíveis (ICMS02/15/53/61, `vICMSMono*`) | **Parcial** | Grupos `Icms02/15/53/61` e totais `qBCMono*`/`vICMSMono*` presentes (`NFeIcmsTot.cs:90-120`); faltam `pRedAdRem`/`motRedAdRem` (ICMS15) e `vICMSMonoOp` (ICMS53) |
| 2023.002 (v1.02) | Fim do envio assíncrono / inutilização em AC, BA, SP | **Parcial** | Lib suporta síncrono (`NFeConfig.EnviarModoSincrono`), mas o **padrão é `false`** (`NFeConfig.cs:28`) |
| 2023.003 (v1.4) | Regras NFC-e exclusivas AM | **N/A** | — |
| 2023.004 (v1.11) | Conciliação financeira (ECONF 110750/110751) e campos de pagamento | **Pendente** | Faltam `dPag`, `CNPJPag`, `UFPag` (detPag) e `CNPJReceb`, `idTermPag` (card) — já existem no XSD local; eventos 110750/110751 não modelados. (`xPag` já implementado, `NFeDetPag.cs:46`) |
| 2023.005 (v1.02) | Insucesso na entrega (110192/110193) | **Pendente** | Eventos não modelados |
| 2024.001 (v1.10) | CRT 4 – MEI | **Implementado** | `RegimeTributario.MEI = 4` (`Shared/Enum/RegimeTributario.cs:26`), commit `0283fb6` |
| 2024.002 (v1.10) | IBS/CBS/IS (início RTC) | **Implementado** | Absorvida pela 2025.002-RTC (abaixo) |
| 2024.003 | Produtos agropecuários (`agropecuario`) | **Pendente** | Faltam `agropecuario`, `defensivo` (`nReceituario`, `CPFRespTec`), `guiaTransito` (`tpGuia`, `UFGuia`, `serieGuia`, `nGuia`) — já no XSD local |
| 2025.001 (v1.03) | Simplificação; **QR Code v3 NFC-e**; síncrono obrigatório p/ NF-e única (01/09/2025) | **Pendente** | QR Code gerado na **versão 2** (`NFe.cs:161`, `versaoQrCode = 2`); síncrono não é padrão (ver 2023.002) |
| 2025.002-RTC (v1.53) | Reforma Tributária do Consumo: IBS, CBS, IS | **Parcial** | Implementada em `13e783b` (2026-08-01): `IBSCBS` (`Detalhe/Imposto/IbsCbs`), `IS`, `ISTot`, `IBSCBSTot`, `vNFTot`, `cMunFGIBS`, `tpNFDebito/Credito`, `gCompraGov`, `gPagAntecipado`, `DFeReferenciado`, `dPrevEntrega` (corrigido em `42b41fc`). **Versão da NT usada não foi registrada**; a v1.53 (30/10/2026) mudou regras de referência em devolução/complemento (NFref no cabeçalho **ou** DFeReferenciado no item). Conferir diferenças v1.3x→v1.53 e atualizar XSD se houver |
| 2026.001 (v1.02b) | Provedor de Assinatura e Autorização – PAA | **Pendente** | Falta `infPAA/CNPJPAA` (no XSD local). Baixa prioridade |
| 2026.002 / 2026.003 | DANFE simplificado tipo 2 | **Pendente** | DANFE atual não tem layout simplificado |
| 2026.004 (v1.01) | **CNPJ alfanumérico** | **Parcial** | XSD local já aceita `[0-9A-Z]{12}[0-9]{2}` (`tiposBasico_v4.00.xsd:96`), mas **`DigitoVerificador.Obter` remove letras** (`OnlyNumbers()`, `2-Core/DigitoVerificador.cs:16`) → chave de acesso com CNPJ alfanumérico falha. Revisar também `ChaveDFe`, validações e campos CNPJ tipados como numéricos |
| 2026.005 (v1.00) | RTC: Simples Nacional | **A analisar** | — |
| 2026.006 (v1.01) | RTC: vinculação de pagamento (split payment) | **A analisar** | — |
| 2026.007 (v1.11) | Emissão por contribuinte sem IE (LCC-RFB/CCC) | **A analisar** | — |
| 2026.008 (v1.01) | Valor líquido do produto (novos campos) | **A analisar** | — |
| 2026.009 (v1.00) | Correção de regra de validação | **N/A** | — |
| 2026.010 (v1.00) | DANFE com tributos da RTC | **Pendente** | DANFE não imprime IBS/CBS/IS (sem referência em `0-Domain/Danfe` / `1-Services/Danfe`) |

## Eventos e serviços

| Item | Status | Observação |
|---|---|---|
| 110110 CC-e | **Implementado** | `NFeService.CartaCorrecao` |
| 110111 Cancelamento | **Implementado** | `NFeService.Cancelar` |
| 110112 Cancelamento por substituição (NFC-e) | **Implementado** | `NFeService.CancelarSubstituicao` (commit `3f43cf7`) |
| EPEC | **Pendente** | Classe comentada (`NFeDetEvento.cs:101-120`). O enum declara EPEC como `110113`; o código usual do EPEC da NF-e é **110140** — conferir na NT antes de implementar |
| 110130/110131 Comprovante de entrega | **Pendente** | NT 2021.001 |
| 110150 Ator interessado | **Pendente** | Código a confirmar na NT |
| 110192/110193 Insucesso na entrega | **Pendente** | NT 2023.005 |
| 110750/110751 Conciliação financeira | **Pendente** | NT 2023.004 |
| 2102xx Manifestação do destinatário | **Pendente** | NT 2020.001; exige Recepção de Evento do Ambiente Nacional |
| Distribuição DF-e (`NFeDistribuicaoDFe`) | **Pendente** | Só endpoints (`NFeEnderecoCollection.cs:182, 356`); sem serviço nem schema `distDFeInt` |
| Consulta Cadastro (`NfeConsultaCadastro`) | **Pendente** | Endpoints e `consCad_v2.00.xsd` existem; sem classe de serviço |

## Campos do XSD local ainda sem modelo (NT de origem a identificar)

O `leiauteNFe_v4.00.xsd` local já contém estes elementos, mas o domínio não. Mapear a NT de origem antes de implementar:

- `emit/ISUFEmit`
- `prod/gCred` (`cCredPresumido`, `pCredPresumido`, `vCredPresumido`) — crédito presumido
- `ICMS10/vICMSSTDeson`, `motDesICMSST`
- `ICMS51` e `ICMS90`: `cBenefRBC`, `pFCPDif`, `vFCPDif`, `vFCPEfet`
- `PISST/indSomaPISST`, `COFINSST/indSomaCOFINSST`
- `procRef/tpAto`

## Histórico de implementação no Vip.DFe

| Data | Commit | NT / mudança |
|---|---|---|
| 2023-11-01 | `59eb7b3`, `98481ee`, `8ff5826` | NT 2023.001 – campos de ICMS monofásico |
| 2024-10-05 | `0283fb6` | NT 2024.001 – CRT 4 (MEI) e schemas atualizados |
| 2025-02-12 | `5383bb5` | QR Code NFC-e (versão 2) |
| 2025-02-21 | `8d8b572` | `xPag` (descrição do pagamento) |
| 2025-12-11 | `3f43cf7` | Evento 110112 – cancelamento por substituição |
| 2026-06-11 | `2b78f14` | ISSQN `dCompet` como `DateTime` (TData) |
| 2026-08-01 | `13e783b` | NT 2025.002-RTC – IBS/CBS/IS, schemas atualizados |
| 2026-08-07 | `aaa6093` | Serialização de opcionais (`ShouldSerialize*`) da RTC |
| 2026-11-24 | `42b41fc` | `dPrevEntrega` como `DateTime?` (TData) |

## Prioridades sugeridas

1. **NT 2026.004 – CNPJ alfanumérico**: corrigir cálculo do DV/chave e validações (quebra emissão para emitentes com CNPJ novo).
2. **NT 2025.002-RTC v1.53**: diff com a versão implementada; atualizar XSD e regras de referência em devolução/complemento.
3. **NT 2025.001**: QR Code v3 da NFC-e e avaliar `EnviarModoSincrono = true` como padrão.
4. **NT 2023.004**: campos `dPag`, `CNPJPag`, `UFPag`, `CNPJReceb`, `idTermPag` (+ evento ECONF, se usado).
5. **NT 2026.010 / 2026.002-003**: DANFE com tributos RTC e DANFE simplificado.
6. Restante do ICMS monofásico (NT 2023.001), agropecuário (NT 2024.003), campos sem modelo e eventos pós-autorização conforme demanda do VipERP.
7. Analisar NTs 2022.002, 2022.003 e 2026.005–2026.008.

> Observação: `schemas/prod_leiauteNFe_v4.00.xsd` (cópia antiga do leiaute, até a NT 2018.005, sem uso) foi removido em 2026-11-24; a referência é `leiauteNFe_v4.00.xsd`.
