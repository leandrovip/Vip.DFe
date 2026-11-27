# Notas Técnicas NF-e/NFC-e — changelog e compatibilidade

> Situação do **Vip.DFe** frente às Notas Técnicas (NT) do leiaute **NF-e/NFC-e 4.00** (modelos 55/65).
> Levantamento de **2026-11-24** (branch `fix/varredura-bugs-docs`). Atualize este arquivo a cada NT implementada.
>
> **Fontes**: lista de NTs do espelho oficial SVRS (`dfe-portal.svrs.rs.gov.br/Nfe/Documentos`) e PDFs do Portal Nacional quando acessíveis; versões/datas **não foram conferidas PDF a PDF** — confirme no [Portal Nacional da NF-e](https://www.nfe.fazenda.gov.br/portal/listaConteudo.aspx?tipoConteudo=04BIflQt1aY=) antes de planejar.
> **Evidência de código**: schemas em `schemas/` e modelos em `src/Vip.DFe/0-Domain/NFe` (busca pelo nome da tag nos atributos `[DFeElement]`/`[DFeCollection]`).
> Para implementar uma NT use a skill [`.claude/skills/nfe-xml-nota-tecnica`](../.claude/skills/nfe-xml-nota-tecnica/SKILL.md); para baixar/triar NTs e schemas novos, a skill [`.claude/skills/nt-referencias`](../.claude/skills/nt-referencias/SKILL.md). Datas de homologação/produção: [`cronograma-nt.md`](cronograma-nt.md).

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
| 2022.002 (v1.30a) | Equiparação à exportação (Conv. ICMS 55/2021), Ajuste SINIEF 40/25 (PcD) | **N/A** | Só regras SEFAZ (X04-10: CFOP 7667 com UF "EX"/equiparada; E03a-10, E12-10, E14-10, E16a-20 flexibilizadas p/ CFOP 7501). Sem tag nova no XML |
| 2022.003 (v1.10/1.11) | Referência com código numérico zerado (`refNFeSig`) + regras BA02* | **Implementado** | `refNFeSig` em `NFeNfRef.cs` e no XSD local; demais itens são regras SEFAZ (BA02-60/70/80, BA02a-74/90/110) |
| 2022.004 / 2022.005 | Regras ISSQN / DIFAL | **N/A** | Regras; grupo `ICMSUFDest` já existe (`NFeDetImposto.cs:94`) |
| 2023.001 (v1.5x) | ICMS monofásico combustíveis (ICMS02/15/53/61, `vICMSMono*`) | **Parcial** | Grupos `Icms02/15/53/61` e totais `qBCMono*`/`vICMSMono*` presentes (`NFeIcmsTot.cs:90-120`); faltam `pRedAdRem`/`motRedAdRem` (ICMS15) e `vICMSMonoOp` (ICMS53) |
| 2023.002 (v1.02) | Fim do envio assíncrono / inutilização em AC, BA, SP | **Implementado** | Síncrono por padrão (`NFeConfig.EnviarModoSincrono = true`, ver NT 2025.001) |
| 2023.003 (v1.4) | Regras NFC-e exclusivas AM | **N/A** | — |
| 2023.004 (v1.20) | Conciliação financeira (ECONF 110750/110751) e campos de pagamento | **Parcial** (campos implementados; ECONF pendente) | `NFeDetPag`: `dPag` (YA03a), par `CNPJPag`/`UFPag` (YA03c/d, os dois ou nenhum); `NFeCardPag`: `CNPJReceb` (YA07a), `idTermPag` (YA07b), `cAut` até 128; `indDeduzDeson` (N28b, enum `NFeIndDeduzDeson`) em ICMS20/30/40/70/90, emitido só com `vICMSDeson`; `MeioPagamento` com os códigos 14, 20, 21, 22, 23, 24 e 91 (Tabela de Meios de Pagamento SVRS de 04/03/2026 e IT 2024.002). `tBand` mantido (MOC 7.0; tabela oficial de operadoras não localizada). **Evento ECONF não modelado** (sem demanda no VipERP); schema `Evento_Econf_v1.00` no acervo |
| 2023.005 (v1.02) | Insucesso na entrega (110192/110193) | **Pendente** | Eventos não modelados |
| 2024.001 (v1.10) | CRT 4 – MEI | **Implementado** | `RegimeTributario.MEI = 4` (`Shared/Enum/RegimeTributario.cs:26`), commit `0283fb6` |
| 2024.002 (v1.10) | IBS/CBS/IS (início RTC) | **Implementado** | Absorvida pela 2025.002-RTC (abaixo) |
| 2024.003 | Produtos agropecuários (`agropecuario`) | **Pendente** | Faltam `agropecuario`, `defensivo` (`nReceituario`, `CPFRespTec`), `guiaTransito` (`tpGuia`, `UFGuia`, `serieGuia`, `nGuia`) — já no XSD local |
| 2025.001 (v1.03) | Simplificação; **QR Code v3 NFC-e**; síncrono obrigatório p/ NF-e única | **Implementado** | **Síncrono:** `NFeConfig.EnviarModoSincrono` agora é `true` por padrão (rej. 452 "resposta assíncrona para lote com 1 NF-e", GAP03a-3, produção 13/10/2025); lote com mais de uma NF-e deve usar `false` (a lib não troca sozinha). **QR Code v3 (opcional):** `NFeConfig.NFCeVersaoQrCode` (`NFeVersaoQrCode.V2` padrão / `V3`); v3 online `?p=chave\|3\|tpAmb` sem CSC; contingência offline com dia, vNF, tipo/identificação do destinatário e assinatura RSA SHA-1 (Base64) dos parâmetros 1–7 com o certificado da NFC-e (Manual de Especificações Técnicas do DANFE NFC-e e QR Code, 24/03/2025, itens 4.4.1/4.4.2). v2 inalterado. Emitente **pessoa física** (produtor rural) deve usar v3 (rej. 444), exceto PR. Demais regras da NT (atraso de emissão, `indIEDest`, cobrança, pagamento) são da SEFAZ |
| 2025.002-RTC (v1.53) | Reforma Tributária do Consumo: IBS, CBS, IS | **Parcial** | Implementada em `13e783b` (2026-08-01): `IBSCBS` (`Detalhe/Imposto/IbsCbs`), `IS`, `ISTot`, `IBSCBSTot`, `vNFTot`, `cMunFGIBS`, `tpNFDebito/Credito`, `gCompraGov`, `gPagAntecipado`, `DFeReferenciado`, `dPrevEntrega` (corrigido em `42b41fc`). **Alinhada à v1.53** (branch `feat/nt-2025-002-v153-rtc`, conferência elemento a elemento contra o XSD do PL_010h): monofasia `gIBSCBSMono` reescrita conforme `TMonofasia` da v1.50 (`pAliqMonoUF/Mun`, `pAliqMonoCBS`, `pAliqMonoReten`, `gMonoRet` só com o valor, `gpBioDiferenca` com `qBCBioComb`); `IS` emite o bloco base/alíquota/valor junto ou nada; crédito presumido (`TCredPres`) emite exatamente um entre `vCredPres`/`vCredPresCondSus`; `NFref` até 999; `emit/ISUFEmit` (C22, v1.40). Referência em devolução/complemento (v1.53: `NFref` no cabeçalho **ou** `DFeReferenciado` no item, nunca os dois — rej. 1010; uma única `NFref` na devolução — rej. 255): o modelo suporta as duas formas; a escolha e as validações ficam com o consumidor. Não impostos pelo domínio: choices AdRem/AdValorem e `gCredPresOper`/`gCredPresIBSZFM`; `gIBSCBS` com `vBC` = 0 continua omitido |
| 2026.001 (v1.02b) | Provedor de Assinatura e Autorização – PAA | **Pendente** | Falta `infPAA/CNPJPAA` (no XSD local). Baixa prioridade |
| 2026.002 (v1.11) / 2026.003 (v1.00) | Emissão offline da NF-e, autorização com alerta e DANFE Simplificado Tipo 2 | **Parcial** | **Feito:** cStat **120** "Autorizado o uso da NF-e, com alerta" tratado como autorizado (`DFeConstantes.NFeProcessada`/`NFeAutorizada`; grava `-procNFe.xml`); `infProt/cMsg`+`xMsg` (um par — a NT permite até 5, a lib preserva o primeiro porque o serializer não repete pares intercalados; hoje só a RV 5E17-65 gera alerta); `TipoImpressao.SimplificadoTipo2 = 6` (por enquanto o DANFE com `tpImp=6` sai no leiaute retrato normal); orientação do DANFE: **paisagem só com `tpImp=2`**, os demais valores saem em retrato (antes, qualquer valor ≠ 1 saía em paisagem). **Pendente:** QR Code v3 na NF-e mod. 55 com `tpImp=6` (sem ele, rej. 394/395), `tpEmis=9` na NF-e e o leiaute do DANFE Simplificado Tipo 2 em bobina (NT 2026.003). Regras BA02-35/VC02-40/I08-180 (NF-e de saída não pode referenciar NFC-e/CF-e, salvo complementar) em produção em **14/12/2026** — validação da SEFAZ, não da lib |
| 2026.004 (v1.01) + NT Conjunta DF-e 2025.001 | **CNPJ alfanumérico** | **Implementado** | `TipoCampo.StrAlfaNumFill` (remove máscara, maiúsculas, zero à esquerda) nos campos de CNPJ e chave; DV da chave com ASCII−48 (`DigitoVerificador`, `ChaveDFe`); `NFeConfig.CNPJ`, `NFeService` (consulta, cancelamento, substituição, CC-e) e inutilização preservam letras; DANFE em CODE-128 híbrido C/A (`Barcode128C`); máscara `12.ABC.345/01DE-35` (`DanfeHelper`). XSD: inutilização e consulta situação do PL_010d v1.03; eventos (`tiposBasico_v1.03`, CC-e, cancelamento, substituição) com os padrões oficiais aplicados à mão (o portal não republicou os específicos). CPF continua numérico. SAT não alterado (legado) |
| 2026.005 (v1.00) | RTC: Simples Nacional (IBS/CBS no SN) | **Implementado** (leiaute) | Todos opcionais: `tpRegimeIBSCBS` C21a (`NFeEmit.cs:112`, enum `TipoRegimeIbsCbs`), `cAtivSN` I17d (`NFeDetProduto.cs:232`, enum `CodigoAtividadeSimplesNacional`), `gTribSN` UB115a com `gIBSSN`/`gCBSSN` (`IbsCbs.cs:68`, `Detalhe/Imposto/IbsCbs/TributacaoSimplesNacional.cs`), total `IBSCBSTot/gTribSN` W59h (`NFeIbsCbsTot.cs:53`, `NFeTribSnTot.cs`; `NFeTotal.ShouldSerializeIbsCbsTot` emite o grupo também quando só há `gTribSN`, rej. 1275). XSD PL_010h. **Fora da lib** (regra de preenchimento, não de leiaute): MEI (CRT 4) não informa IBSCBS; CST 620 não leva `gTribSN`; tabela 2.3 de preenchimento por CRT/`tpRegimeIBSCBS`; totais `vIBSSN`/`vCBSSN` não são calculados (rej. 1277/1284). Evento 412140 (gerado pelo Fisco) não modelado. Produção **14/12/2026** (Aviso SVRS 3022); obrigatório para o SN em 01/03/2027 |
| 2026.006 (v1.01) | RTC: vinculação de pagamento (split payment) | **Pendente** | Grupo `gPgtoVinc` (YC01) / `gPgto` 1-99 (YC02) com atributos `nPag`, `idTransacao`, e `tpMeioPgto`, `CNPJReceb`, `CNPJBasePSP`; evento **110300** (cancelamento via 110001). Ausente no XSD local e no domínio. Facultativo; split payment previsto para 2027. Pacote PL_010g/PL_010h (divergente entre fontes) |
| 2026.007 (v1.11) | Emissão por contribuinte sem IE (exclusivo IBS/CBS); validações LCC-RFB e CCC | **Pendente** | Sem tags novas, mas exige `emit/IE` opcional: hoje é obrigatório no XSD local (`leiauteNFe_v4.00.xsd:564`) e no domínio (`NFeEmit.cs:74`). Facultativa; só NF-e na SVRS (NFC-e vedada, rej. 156). Sem IE: CNPJ obrigatório, `IEST` proibido, grupo ICMS proibido (salvo exceções), IBSCBS obrigatório (rej. 157-163). Demais regras (LCC-RFB/CCC, rej. 164, 170, 178-188) são da SEFAZ |
| 2026.008 (v1.01) | Valor líquido do produto; ICMS previsto (pagamento antecipado) | **Implementado** (leiaute) | Opcionais: `vUnComLiq` I11b (De10) e `vProdLiq` I11c (De2) em `prod` (`NFeDetProduto.cs:158`; só emitidos se **ambos** informados, sequence do XSD), `vProdLiqTot` W01a primeiro filho de `total` (`NFeTotal.cs:37`), grupo `ICMSPrevistoPagtoAntecip/vICMSPrevisto` NB01/NB02 no choice de ICMS (`Icms.cs:68`, `IcmsPrevistoPagtoAntecip.cs`). XSD PL_010h. Sem cálculo automático (rej. 1279: vProdLiq = vUnComLiq × qCom ± 0,01; rej. 1283: soma dos vProdLiq). NB01-30 (grupo obrigatório se `tpNFDebito = 06` ou `tpOperGov = 4`): homologação 01/02/2027, produção 01/03/2027; só mod. 55 (rej. 1280). Produção dos campos: **14/12/2026** (Aviso SVRS 3022) |
| 2026.009 (v1.00) | Correção de regra de validação | **N/A** | — |
| 2026.010 (v1.00) | DANFE com tributos da RTC | **Implementado** (exceto QR Code) | Novo leiaute é o **padrão** do `DanfeService`, retrato e paisagem (`tpImp=2`): CRT no quadro do emitente e área reservada (vazia) para o tipo de regime de apuração do IBS/CBS; quadros "Total dos produtos e total da nota", "Total do ICMS / IPI" (linhas de FCP/DIFAL e ICMS monofásico só quando algum valor > 0) e "Total do IBS / CBS / IS" (linha da monofasia só quando > 0); tabela de itens com 8 colunas (bases, alíquotas e valores de ICMS/IPI/IBS UF/IBS Mun/CBS/IS; alíquota efetiva `pAliqEfet` quando há `gRed`; `[NCM] [cClassTrib]` na descrição). Paisagem com canhoto na borda direita. Quadro "Cálculo do ISSQN" só quando há valor de ISSQN (`vServ`/`vBC`/`vISS` > 0; o domínio sempre instancia `ISSQNtot`). `DanfeViewModel.UsarLeiauteAnterior = true` gera o leiaute antigo (saída temporária de transição). **Limitação conhecida:** o desserializador cria `gRed` vazio quando a tag não existe, por isso `gRed` com `pRedAliq = 0` e `pAliqEfet = 0` é tratado como ausente e o DANFE imprime a alíquota cheia (`pIBSUF`/`pIBSMun`/`pCBS`). **Hipótese a validar:** bases, alíquotas e valores por item (item 4.3) não estão na lista de facultativos do item 4.4, por isso são impressos com 0,00 quando o grupo não existe no XML. **Pendente:** QR Code no DANFE mod. 55 (item 4.5, aguarda NT) |

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
| 110750/110751 Conciliação financeira | **Pendente** | NT 2023.004; sem demanda no VipERP (2026-11-26). Schema `Evento_Econf_v1.00` em `referencias/schemas/` |
| 2102xx Manifestação do destinatário | **Pendente** | NT 2020.001; exige Recepção de Evento do Ambiente Nacional |
| Distribuição DF-e (`NFeDistribuicaoDFe`) | **Pendente** | Só endpoints (`NFeEnderecoCollection.cs:182, 356`); sem serviço nem schema `distDFeInt` |
| Consulta Cadastro (`NfeConsultaCadastro`) | **Pendente** | Endpoints e `consCad_v2.00.xsd` existem; sem classe de serviço |

## Campos do XSD local ainda sem modelo (NT de origem a identificar)

O `leiauteNFe_v4.00.xsd` local já contém estes elementos, mas o domínio não. Mapear a NT de origem antes de implementar:

- `emit/ISUFEmit`
- `prod/gCred` (`cCredPresumido`, `pCredPresumido`, `vCredPresumido`) — crédito presumido
- `ICMS10/vICMSSTDeson`, `motDesICMSST` (também em ICMS70 e ICMS90)
- `ICMSPart`: `vICMSDeson`, `motDesICMS`, `indDeduzDeson` (desoneração nunca modelada nesse grupo)
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

## Pacote de schemas

`schemas/leiauteNFe_v4.00.xsd` e `schemas/DFeTiposBasicos_v1.00.xsd` são cópias do **PL_010h v1.03** (antes eram do PL_010e v1.02). Com isso o XSD local já contém as tags das NTs 2026.005, 2026.006 (`gPgtoVinc`, não modelado no domínio) e 2026.008, e `emit/IE` opcional (NT 2026.007; o domínio ainda exige IE). Confirmado no Portal Nacional (2026-11-26): o **PL_010h v1.03** (13/11/2026) cobre as NTs 2026.005 v1.00, 2026.006 v1.00 e 2026.008 v1.01; o PL_010f v1.04 cobre a 2026.007. Lista completa em [`cronograma-nt.md`](cronograma-nt.md#pacotes-de-schema-publicados).

Antes de implementar qualquer uma dessas NTs, baixe o pacote com `referencias/tools/Baixar-Referencias.ps1` (submódulo `dfe-referencia`, skill `nt-referencias`), compare `referencias/schemas/PL_xxx/` com `schemas/` e só então atualize `schemas/`.

> O espelho SVRS não tem todos os pacotes (parava no PL_010c em 2026-11-26); use o Portal Nacional como fonte.

> As informações das NTs 2022.002 e 2022.003 vieram de **resumos de fornecedores**. As NTs 2026.004, 2026.005 e 2026.008 já foram conferidas no texto oficial e no XSD do acervo (`referencias/`). Para as demais, confirme Ids, tipos, tamanhos e elemento pai no PDF/XSD oficial (`referencias/`) antes da implementação.

## Prioridades sugeridas

1. ~~**NT 2026.004 – CNPJ alfanumérico**~~: implementado (branch `feat/cnpj-alfanumerico`).
2. ~~**Atualizar o pacote de schemas** e implementar a **NT 2026.005 (Simples Nacional na RTC)**~~: implementado (PR #6, schemas do PL_010h; testes em homologação pendentes). Produção em 14/12/2026 (Aviso SVRS 3022) e afeta emitentes CRT 1/2 e CRT 3 com opção pendente pelo SN (o **MEI, CRT 4, não informa** o grupo IBSCBS); obrigatoriedade para o SN em 01/03/2027 (Ato Conjunto RFB/CGIBS nº 7/2026); junto, os campos opcionais da **NT 2026.008** (`vUnComLiq`, `vProdLiq`, `vProdLiqTot`).
3. ~~**NT 2025.002-RTC v1.53**~~: domínio alinhado ao XSD (monofasia, IS, crédito presumido, `ISUFEmit`); referências em devolução/complemento já suportadas.
4. ~~**NT 2025.001**~~: síncrono por padrão e QR Code v3 opcional (branch `feat/nt-2025-001-qrcode-v3`).
5. ~~**NT 2023.004**~~: campos de pagamento, `indDeduzDeson` e códigos novos de `tPag` (branch `feat/nt-2023-004-pagamento`); evento ECONF só quando houver demanda.
6. ~~**NT 2026.010**~~: DANFE com tributos RTC (branch `feat/danfe-rtc`; QR Code mod. 55 aguarda NT). **NT 2026.002-003**: DANFE simplificado.
7. Facultativas/futuras: **NT 2026.007** (emitente sem IE, só se houver cliente exclusivo de IBS/CBS), **NT 2026.006** (split payment, 2027).
8. Restante do ICMS monofásico (NT 2023.001), agropecuário (NT 2024.003), campos sem modelo e eventos pós-autorização conforme demanda do VipERP.

> Observação: `schemas/prod_leiauteNFe_v4.00.xsd` (cópia antiga do leiaute, até a NT 2018.005, sem uso) foi removido em 2026-11-24; a referência é `leiauteNFe_v4.00.xsd`.
