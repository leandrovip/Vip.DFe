# Modelo de Domínio

> Código-fonte é a fonte de verdade (`src/Vip.DFe/0-Domain`). Não é catálogo de classes; apenas entidades principais, regras e relacionamentos. Itens incertos marcados como **Hipótese a validar**.

## Visão geral

As entidades são POCOs com atributos de serialização (`[DFeRoot]`, `[DFeElement]` com `Id` do manual, `Ocorrencia`, `Ordem`, Min/Max), herdam `GenericClone<T>` (clone profundo) e implementam `INotifyPropertyChanged` (injetado por `PropertyChanged.Fody`). Modelos que vão à SEFAZ herdam `DFeSignDocument<T>` (possuem `Signature` XMLDSig); modelos de resposta/processados herdam `DFeDocument<T>`.

```
0-Domain/
├── NFe/      → nota, eventos, protocolo, enums (43), interfaces de imposto
├── SAT/      → CFe/CFeCanc, funções SatRede/SatStatus, enums, interfaces de imposto
├── Danfe/    → ViewModels, Blocos, Elementos (renderização), enums
└── Shared/   → enums comuns (CodigoUF, TipoAmbiente, MeioPagamento, etc.)
```

## Módulo NFe

### Entidades principais

| Entidade | Pasta | Responsabilidade | Relacionamentos / regras |
|---|---|---|---|
| `NFe` | `NotaFiscal/NFe.cs` | Raiz da nota (`[DFeRoot("NFe")]`); assinável (`DFeSignDocument`) | `InfNFe` obrigatório; `InfNFeSupl` opcional (QRCode NFCe); `Assinado` derivado da assinatura |
| `InfNFe` | `NotaFiscal/infNFe.cs` | Corpo da nota: `ide`, `emit`, `dest`, `det`, `total`, `transp`, `pag`, etc. | Grupos na ordem do leiaute (Ids A→Z); `Id` da nota = chave de 44 dígitos (ver `ChaveDFe`) |
| `NFeIde` | `Identificacao/` | Identificação: UF, natOp, modelo, série, número, emissão, operação, consumidor final, presença | `cUF` (CodigoUF), `mod` (NFeModelo), `tpEmis`, `tpAmb`, `finNFe`, `indPres` |
| `NFeEmit` / `NFeEmitEndereco` | `Emitente/` | Emitente: CNPJ/CPF, razão social, endereço, IE, CRT | CRT ligado ao regime tributário; validações Min/Max por atributo |
| `NFeDest` / `NFeDestEndereco` | `Destinatario/` | Destinatário: CNPJ/CPF, endereço, `indIEDest` | `indIEDest` (NFeIndIeDest): contribuinte/não contribuinte/isentos |
| `NFeDetalhe` | `Detalhe/` | Item da nota (`det`) | `NItem`, `Produto` (`NFeDetProduto`) e `Imposto` (`NFeDetImposto`: ICMS/PIS/COFINS/IPI/II/ISSQN e, na reforma tributária, `IS`/`IbsCbs`); também `ObsItem`, `DFeReferenciado`, `InfAdProd`, `VItem` |
| `NFeDetImposto` | `Detalhe/` | Tributação do item | Usa interfaces `INFeIcms`, `INFePis`, `INFeCofins`, `INFeIpi`, `INFeImposto` (`NFe/Interfaces`) para suportar grupos por CST/CSOSN — `Hipótese a validar`: mapeamento completo dos grupos |
| `NFeTotal` | `Total/` | Totais: `IcmsTot`, `IssqnTot`, `RetTrib`, `IsTot`, `IbsCbsTot`, `VNfTot` | Reflete o grupo `total` do leiaute; `IsTot` só é serializado se `IsTot.VIS > 0` |
| `NFeTransp`, `NFeVolTransp`, `NFeVeicTransp` | `Transporte/` | Transporte/volume/veículo | `modFrete`, volumes `pesoL/pesoB` |
| `NFePgto` | `Pagamento/` | Formas de pagamento | `MP` + `MeioPagamento` (Shared); regras de pagamento do manual |
| `NFeCobranca` | `Cobranca/` | Fatura/duplicatas | Grupo `cobr` |
| `NFeCana`, `NFeCompra`, `NFeExporta`, `NFeIntermediador`, `NFeResponsavelTecnico`, `NFeRetirada`, `NFeEntrega`, `NFeAvulsa`, `NFeAutXml` | `NotaFiscal/` | Grupos opcionais específicos do leiaute (cana, compra, exportação, intermediador, resp. técnico, retirada/entrega, avulsa, autorização p/ XML) | Condicionados ao cenário fiscal |
| `NFeProc` | `NotaFiscal/NFeProc.cs` | Documento processado: `NFe` + `ProtNFe` (`[DFeRoot("nfeProc")]`) | `Processado` indica autorização; `Gravar(config)` persiste XML |
| `NFeProtNFe` / `NFeInfProt` | `Protocolo/` | Protocolo de autorização/consulta | `NProt`, `DigVal` (validado em `NFeService` se `ValidarDigest`) |
| `NFeEvento` / `NFeInfEvento` / `NFeDetEvento` | `Evento/` | Evento genérico (cancelamento, cancelamento por substituição, CCe) | `TpEvento` (NFeTipoEvento); `DetEvento` varia por tipo |
| `NFeProcEvento` | `Evento/NFeProcEvento.cs` | Evento processado (para DANFE de evento e gravação) | `Evento` + `RetEvento` |
| `NFeCollection` | `NotaFiscal/NFeCollection.cs` | Coleção de NFes do serviço | `Assinar(cert, options)`, `Validar()` |

### Enums relevantes (`NFe/Enum`)

São **43** enums em `0-Domain/NFe/Enum`.

- `NFeVersao` (v400), `NFeModelo` (NFe/NFCe), `NFeStatus` (estados do componente), `NFeSchema` (nomes de XSD).
- Fiscais: `NFeFinalidade`, `NFeTipo` (entrada/saída), `NFeDestinoOperacao`, `NFeConsumidorFinal`, `NFePresencaComprador`, `NFeIndIeDest`, `NFeIndTotal`, `NFeModalidadeBC/BCST`, `NFeMotivoDesoneracao`, `NFeTipoEvento`, `NFeBandeiraCartao`, etc.
- Reforma tributária (novos): `NFeTipoCompraGov`, `NFeTipoCredPresIbsZFM`, `NFeTipoNotaCredito`, `NFeTipoNotaDebito`, `NFeTipoOperacaoGov`.

### Reforma tributária (IBS/CBS/IS)

Campos e schemas atualizados para a reforma tributária (commit `13e783b`). Resumo dos pontos de entrada:

| Local | Tipo | Observação |
|---|---|---|
| `NFeDetImposto.IbsCbs` | `IbsCbs` (`Detalhe/Imposto/IbsCbs`) | IBS/CBS do item: `Cst`, `CClassTrib`, `IndDoacao` e um dos grupos abaixo |
| `NFeDetImposto.IS` | `ImpostoSeletivo` | Imposto Seletivo: `CstIS`, `CClassTribIS`, `VBcIS`, `PIs`, `AdRemIS`, `UTrib`, `QTrib`, `VIS` |
| `NFeTotal.IsTot` | `NFeIsTot` | Total do IS (serializado se `VIS > 0`) |
| `NFeTotal.IbsCbsTot` | `NFeIbsCbsTot` | `VBcIbsCbs`, `Ibs` (`NFeIbsTot` → `NFeIbsUfTot`/`NFeIbsMunicipioTot`), `Cbs` (`NFeCbsTot`), `Mono` (`NFeMonoTot`), `EstornoCredito` (`NFeEstornoCreditoTot`) |
| `NFeIde` | `CMunFGIBS`, `TpNFDebito` (`NFeTipoNotaDebito`), `TpNFCredito` (`NFeTipoNotaCredito`), `GCompraGov` (`NFeGCompraGov`), `GPagAntecipado` (`NFeGPagAntecipado`) | Cada um só é serializado conforme `ShouldSerializeXxx()` (ex.: `TpNFDebito` apenas se `FinNFe == Debito`; `CMunFGIBS` apenas se diferente de 0) |
| `NFeDetalhe.DFeReferenciado` | `NFeDFeReferenciado` | `ChaveAcesso` e `NItem` do DF-e referenciado |
| `NFeDetalhe.ObsItem` | `NFeDetObsItem` | `ObsCont` e `ObsFisco` (`NFeDetObs`: `XCampo`, `XTexto`) |
| `NFeDetProduto.TpCredPresIBSZFM` | `NFeTipoCredPresIbsZFM?` | Crédito presumido IBS na ZFM |
| `NFeIssqnTot.DCompet` | `DateTime` | Tipo alterado para `DateTime`, com remoção de `ShouldSerializeDCompet` (commit `2b78f14`) |

Grupos em `Detalhe/Imposto/IbsCbs` (pasta com 37 arquivos de classe), pelos nomes reais:

- **Raiz e regimes**: `IbsCbs` aponta para `GrupoIbsCbs` (base `VBc`, `IbsUf`, `IbsMunicipio`, `VIbs`, `Cbs`, `TributacaoRegular`, `TributacaoCompraGov`), `IbsCbsMono` (monofásico), `TransferenciaCredito`, `AjusteCompetencia`, `EstornoCredito`, `CreditoPresumidoOperacao` e `CreditoPresumidoIbsZfm`.
- **Alíquotas e partilha**: `IbsUf`, `IbsMunicipio`, `Cbs`, `Diferimento`, `ReducaoAliquota`, `DevolucaoTributo`, `AlcZfmCbs`, `CreditoPresumidoTributo`.
- **Monofásico/ad rem e ad valorem** (`gIBSCBSMono`, tipo `TMonofasia`; alinhado ao XSD na NT 2025.002 v1.53): `IbsCbsMono` com `IbsMonoAdRem`, `IbsMonoAdValorem`, `CbsMonoAdRem`, `CbsMonoAdValorem`; cada um com `gMonoPadrao` (`IbsAdRemPadrao`/`IbsAdValoremPadrao`/`CbsAdRemPadrao`/`CbsAdValoremPadrao`), `gMonoReten` (`*Reten`), `gMonoRet` (`IbsMonoRet`/`CbsMonoRet`, só o valor retido) e `gpBioDiferenca` (`IbsBioDiferenca`/`CbsBioDiferenca`: `qBCBioComb` + `vIBSDiferenca`/`vCBSDiferenca`). No ad valorem do IBS, `gMonoPadrao` separa UF e município (`pAliqMonoUF`/`vIBSMonoUF`, `pAliqMonoMun`/`vIBSMonoMun`). Os choices AdRem/AdValorem do XSD não são impostos: o consumidor preenche um lado por tributo.
- **Imposto Seletivo**: `ImpostoSeletivo` (na mesma pasta; usado em `NFeDetImposto.IS`).

No DANFE, `DanfeViewModel.ModoEspelho` indica pré-visualização sem protocolo (ver `docs/modules/api.md`). A Demo preenche `NFeDetImposto.IbsCbs` e `NFeTotal.IbsCbsTot` com valores calculados pelo `Vip.Fiscal` (ver `docs/modules/ui-winforms.md`); o `CClassTrib` usado na Demo é um placeholder.

### Regras de domínio (exemplos)

- **Chave de 44 dígitos** gerada por `ChaveDFe.Gerar` (2-Core); dígito verificador por `DigitoVerificador`.
- **Validação de campos** ocorre em runtime pela serialização (Min/Max/Ocorrencia nos atributos) e via XSD (`NFeCollection.Validar`), com mensagens PT-BR do `DFeSerializer`.
- **Digest da assinatura conferido** no retorno da SEFAZ (`NFeService`), se `ValidarDigest`.

## Módulo SAT (CFe)

| Entidade | Pasta | Responsabilidade | Regras |
|---|---|---|---|
| `CFe` | `CupomFiscal/CFe.cs` | Cupom fiscal eletrônico (raiz `CFe`) | `InfCFe` (identificação, emitente, dest, itens, total, pgto); versão de dados de entrada em `SatConfig.InfCFeVersaoDadosEnt` |
| `InfCFe` | `CupomFiscal/InfCFe.cs` | Corpo do cupom | `Ide` (CNPJ, número caixa, `SignAC`), `Emit`, `Dest` (opcional), `Det` (itens), `Total`, `Pgto` |
| `CFeDet` / `CFeDetProd` / `CFeDetImposto` | `CupomFiscal/` | Item do cupom | `IndRegra` (arredondamento/truncamento); impostos via interfaces `ICFeIcms/ICFePis/ICFeCofins/ICFeImposto` (`SAT/Interfaces`) |
| `CFePgto` / `CFePgtoMP` | `CupomFiscal/` | Pagamentos | `CMp` = `MeioPagamento` (Shared) |
| `CFeCanc` + `CancInfCFe`, `CFeCancIde`, `CFeCancEmit`, `CFeCancDest`, `CFeCancTotal`, `CFeCancInfAdic` | `Eventos/` | Cupom de cancelamento | Estrutura própria do cancelamento SAT; usado em `CancelarUltimaVenda` |
| `SatRede` | `Funcoes/SatRede.cs` | Configuração de rede do SAT | Usado em `ConfigurarInterfaceDeRede` |
| `SatStatus` | `Funcoes/SatStatus.cs` | Status operacional retornado pelo SAT | Preenchido a partir da resposta da DLL |

Enums: `ModeloSat` (fabricante/versão da DLL), `IndRegra`, `EstadoOperacao`, `NivelBateria`, `StatusLan`, `TipoInterface`, `TipoLan`, `SegSemFio`, `TipoProxy`, `RegTribIssqn`, `RatIssqn`.

## Módulo DANFE

| Área | Pasta | Responsabilidade |
|---|---|---|
| `DanfeViewModel` / `DanfeEventoViewModel` | `Danfe/Modelo/` | Dados de exibição (desacoplados do modelo NFe); criados a partir de XML/NFeProc/NFeProcEvento via `DanfeViewModelCreator`/`DanfeEventoViewModelCreator` (`CriarDeArquivoXml`, `CriarDoConteudoXml`) |
| ViewModels auxiliares | `Danfe/Modelo/` | `EmpresaViewModel`, `ProdutoViewModel`, `ImpostoViewModel`, `CalculoImpostoViewModel`, `CalculoIssqnViewModel`, `DuplicataViewModel`, `TransportadoraViewModel`, `LocalEntregaRetiradaViewModel` |
| Blocos | `Danfe/Blocos/` + `BlocosEvento/` | Seções da página (canhoto, emitente, destinatário, itens, cálculo imposto, transportador, dados adicionais; e blocos de evento) |
| Elementos | `Danfe/Elementos/` | Primitivas de desenho (`Campo`, `TextBlock`, `Tabela`, `Barcode128C`, `LinhaTracejada`, etc.) |
| Enums | `Danfe/Enum/` | `Orientacao`, `AlinhamentoHorizontal/Vertical`, `PosicaoBloco` |

## Módulo Shared

Enums comuns usados por NFe e SAT: `CodigoUF`, `TipoAmbiente`, `TipoEmissao`, `TipoImpressao`, `MeioPagamento`, `OrigemMercadoria`, `ProcessoEmissao`, `RegimeTributario`.

## Dúvidas / Hipóteses a validar

- Cobertura exata dos grupos de ICMS por CST/CSOSN em `NFeDetImposto` (inclui CSOSN 900 sem desoneração, conforme commits recentes).
- Cobertura exata dos demais grupos de imposto de `Detalhe/Imposto/IbsCbs` (qual combinação de grupos cada `CST`/`CClassTrib` exige no leiaute) — validar com o manual.

### Fatos confirmados (antes hipóteses)

- **Distribuição DF-e / Consulta Cadastro**: existem apenas como endereço/enum (`NFeTipoServico.NFeDistribuicaoDFe`, `NFeTipoServico.NfeConsultaCadastro`, `NFeStatus.DistDFeInt`, entradas em `NFeEnderecoCollection`) e schemas em `schemas/`; **não há serviço implementado** (`NFeService` não expõe esses métodos).
- **`3-Infra/Notification`**: não existe em disco e está excluído no csproj — resíduo de configuração.
- **`NFeIndIntermed`, `NFeTpIntermedio`, `NFeIndProcesso` existem e têm usos distintos**: `NFeIde.IndIntermed` (indicador de intermediador), `NFeDetImportacao.TpIntermedio` (via de intermediação na importação) e `NFeProcRef.IndProc` (origem do processo referenciado) — não há sobreposição de finalidade.
