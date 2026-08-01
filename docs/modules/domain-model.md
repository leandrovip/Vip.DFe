# Modelo de Domínio

> Código-fonte é a fonte de verdade (`src/Vip.DFe/0-Domain`). Não é catálogo de classes; apenas entidades principais, regras e relacionamentos. Itens incertos marcados como **Hipótese a validar**.

## Visão geral

As entidades são POCOs com atributos de serialização (`[DFeRoot]`, `[DFeElement]` com `Id` do manual, `Ocorrencia`, `Ordem`, Min/Max), herdam `GenericClone<T>` (clone profundo) e implementam `INotifyPropertyChanged` (injetado por `PropertyChanged.Fody`). Modelos que vão à SEFAZ herdam `DFeSignDocument<T>` (possuem `Signature` XMLDSig); modelos de resposta/processados herdam `DFeDocument<T>`.

```
0-Domain/
├── NFe/      → nota, eventos, protocolo, enums (38), interfaces de imposto
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
| `NFeDetalhe` | `Detalhe/` | Item da nota (`det`) | `NItem`, `Prod` (produto) e `Imposto` (grupos ICMS/PIS/COFINS/IPI/II/ISSQN) |
| `NFeDetImposto` | `Detalhe/` | Tributação do item | Usa interfaces `INFeIcms`, `INFePis`, `INFeCofins`, `INFeIpi`, `INFeImposto` (`NFe/Interfaces`) para suportar grupos por CST/CSOSN — `Hipótese a validar`: mapeamento completo dos grupos |
| `NFeTotal` | `Total/` | Totais (ICMS, serviços, valores por UF de destino) | Reflete o grupo `total` do leiaute |
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

- `NFeVersao` (v400), `NFeModelo` (NFe/NFCe), `NFeStatus` (estados do componente), `NFeSchema` (nomes de XSD).
- Fiscais: `NFeFinalidade`, `NFeTipo` (entrada/saída), `NFeDestinoOperacao`, `NFeConsumidorFinal`, `NFePresencaComprador`, `NFeIndIeDest`, `NFeIndTotal`, `NFeModalidadeBC/BCST`, `NFeMotivoDesoneracao`, `NFeTipoEvento`, `NFeBandeiraCartao`, etc.

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
- `DistribuicaoDFe` / `ConsultaCadastro`: schemas `consCad` existem em `schemas/` e `NfeConsultaCadastro` existe em `NFeEnderecoCollection`, mas **não foi encontrado serviço implementado** para Distribuição/Consulta Cadastro na lib.
- `3-Infra/Notification` não existe em disco (removido do csproj) — legado.
- Fonte dos enums `NFeIndProcesso`, `NFeIndIntermed`/`NFeTpIntermedio` (sobreposição aparente de finalidade).
