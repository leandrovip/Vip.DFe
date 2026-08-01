# Guia de consumo do Vip.Fiscal

Este documento orienta agentes que implementam cálculos tributários em projetos consumidores do pacote `Vip.Fiscal`. A biblioteca calcula valores a partir de dados fiscais já definidos pela aplicação; ela não substitui a validação da legislação, das regras estaduais, do cadastro tributário ou do XML fiscal.

## Instalação e namespaces

```powershell
dotnet add package Vip.Fiscal
```

Nos arquivos de cálculo, importe:

```csharp
using Vip.Fiscal;
using Vip.Fiscal.Enums;
using Vip.Fiscal.Interfaces;
```

Use valores `decimal` para montantes e alíquotas. As alíquotas são informadas em pontos percentuais: use `17.00m` para 17%, e não `0.17m`.

## Escolha da API

| Necessidade | API | Resultado |
| --- | --- | --- |
| Calcular um tributo específico, como ICMS, PIS, COFINS, IPI, DIFAL, FCP, IBS ou CBS | `TributacaoService` | Interface de resultado do tributo, normalmente com `BaseCalculo` e `Valor` |
| Calcular os tributos de um item e preencher campos para uma NF-e/NFC-e | `TributacaoProduto` | A própria instância, preenchida por `Calcular()` com propriedades como `ValorIcms`, `ValorPis` e `ValorCofins` |

Prefira `TributacaoService` quando a aplicação precisa de um cálculo pontual ou trata cada tributo separadamente. Use `TributacaoProduto` quando o contexto do item está completo e o consumidor precisa de um resultado consolidado.

## Modelo de entrada

As duas APIs recebem uma entidade que implemente `IEntidadeTributaria`; `TributacaoProduto` requer `IEntidadeProduto`, que a herda. Não existe uma classe concreta de item distribuída pelo pacote: adapte o modelo do projeto consumidor ou crie um DTO próprio que implemente a interface.

O modelo abaixo contém as propriedades exigidas pelas interfaces atuais e pode servir como adaptador inicial. Mantenha somente uma fonte de verdade: mapeie os dados do produto ou serviço da aplicação para esse objeto antes de calcular.

```csharp
using Vip.Fiscal.Enums;
using Vip.Fiscal.Interfaces;

public class ItemTributavel : IEntidadeProduto
{
    public TipoDocumento TipoDocumento { get; set; } = TipoDocumento.NFe;
    public decimal ValorItem { get; set; }
    public decimal QuantidadeItem { get; set; }
    public bool Servico { get; set; }
    public bool AtivoImobilizadoOuUsoeConsumo { get; set; }
    public decimal Frete { get; set; }
    public decimal Seguro { get; set; }
    public decimal OutrasDespesas { get; set; }
    public decimal Desconto { get; set; }
    public Cst Cst { get; set; }
    public Csosn Csosn { get; set; }
    public decimal PercentualIcms { get; set; }
    public decimal PercentualReducao { get; set; }
    public decimal PercentualCredito { get; set; }
    public decimal PercentualIcmsSt { get; set; }
    public decimal PercentualMva { get; set; }
    public decimal PercentualReducaoSt { get; set; }
    public decimal PercentualDiferimento { get; set; }
    public decimal PercentualDifalInterna { get; set; }
    public decimal PercentualDifalInterestadual { get; set; }
    public decimal PercentualFcp { get; set; }
    public decimal PercentualFcpSt { get; set; }
    public decimal PercentualFcpStRetido { get; set; }
    public decimal ValorUltimaBaseCalculoIcmsStRetido { get; set; }
    public decimal PercentualOriginarioUf { get; set; }
    public decimal QuantidadeBaseCalculoIcmsMonofasico { get; set; }
    public decimal QuantidadeBaseCalculoIcmsMonofasicoRetencao { get; set; }
    public decimal QuantidadeBaseCalculoIcmsMonofasicoRetidoAnteriormente { get; set; }
    public decimal AliquotaAdRemIcms { get; set; }
    public decimal AliquotaAdRemIcmsretencao { get; set; }
    public decimal AliquotaAdRemIcmsRetidoAnteriormente { get; set; }
    public decimal PercentualReducaoAliquotaAdRemIcms { get; set; }
    public decimal PercentualIcmsMonofasicoDiferido { get; set; }
    public bool CalcularIcmsEfetivoeRetencaoParaSt { get; set; }
    public decimal PercentualIcmsEfetivo { get; set; }
    public decimal PercentualReducaoIcmsEfetivo { get; set; }
    public CstPisCofins CstPisCofins { get; set; }
    public decimal PercentualPis { get; set; }
    public decimal PercentualCofins { get; set; }
    public bool DeduzIcmsDaBaseDePisCofins { get; set; }
    public decimal PercentualReducaoPis { get; set; }
    public decimal PercentualReducaoCofins { get; set; }
    public decimal PercentualBiodisel { get; set; }
    public CstIpi CstIpi { get; set; }
    public decimal ValorIpi { get; set; }
    public decimal PercentualIpi { get; set; }
    public decimal PercentualIssqn { get; set; }
    public decimal PercentualRetPis { get; set; }
    public decimal PercentualRetCofins { get; set; }
    public decimal PercentualRetCsll { get; set; }
    public decimal PercentualRetIrrf { get; set; }
    public decimal PercentualRetInss { get; set; }
    public CstIbsCbs CstIbsCbs { get; set; }
    public decimal PercentualIbsUF { get; set; }
    public decimal PercentualIbsMunicipal { get; set; }
    public decimal PercentualCbs { get; set; }
}
```

### Dados compartilhados

Preencha antes de qualquer cálculo:

- `ValorItem` e `QuantidadeItem` representam o valor unitário e a quantidade do item.
- `Frete`, `Seguro`, `OutrasDespesas` e `Desconto` alteram a base conforme o tributo e o tipo de desconto.
- `TipoDocumento` deve refletir o documento que será gerado, como `TipoDocumento.NFe`.
- Defina `Servico = true` para serviços. No fluxo consolidado, isso direciona o cálculo para ISSQN e retenções em vez de ICMS, DIFAL, FCP e IPI.

### Dados por grupo tributário

| Grupo | Principais campos |
| --- | --- |
| ICMS normal | `Cst`, `PercentualIcms`, `PercentualReducao`, `PercentualDiferimento` |
| Simples Nacional | `Csosn`, `PercentualCredito` |
| ICMS-ST | `PercentualIcmsSt`, `PercentualMva`, `PercentualReducaoSt`, `PercentualFcpSt` |
| DIFAL/FCP | `PercentualDifalInterna`, `PercentualDifalInterestadual`, `PercentualFcp` |
| PIS/COFINS | `CstPisCofins`, `PercentualPis`, `PercentualCofins`, `DeduzIcmsDaBaseDePisCofins`, percentuais de redução |
| IPI | `CstIpi`, `PercentualIpi` ou `ValorIpi` |
| ISS e retenções | `PercentualIssqn`, `PercentualRetPis`, `PercentualRetCofins`, `PercentualRetCsll`, `PercentualRetIrrf`, `PercentualRetInss` |
| IBS/CBS | `CstIbsCbs`, `PercentualIbsUF`, `PercentualIbsMunicipal`, `PercentualCbs` |

Preencha códigos fiscais e alíquotas a partir da regra tributária da operação. Não deduza CST, CSOSN, regime ou alíquota somente a partir do tipo de documento.

## Cálculos isolados com TributacaoService

`TributacaoService` recebe `IEntidadeTributaria`. O construtor permite informar o tipo de desconto e, quando necessário, o tipo de cálculo de ICMS desonerado:

```csharp
var service = new TributacaoService(
    entidade,
    TipoDesconto.Incondicional,
    tipoCalculoIcmsDesonerado: null);
```

Os métodos disponíveis são `ObterIcms`, `ObterIpi`, `ObterIcmsCredito`, `ObterCofins`, `ObterPis`, `ObterDifal`, `ObterIcmsSt`, `ObterFcp`, `ObterFcpSt`, `ObterIssqn`, `ObterIcmsDesonerado`, `ObterIcmsMonofasico`, `ObterIcmsEfetivo`, `ObterIbs`, `ObterIbsMunicipal` e `ObterCbs`.

### ICMS básico

```csharp
var item = new ItemTributavel
{
    ValorItem = 1000.00m,
    QuantidadeItem = 1.000m,
    Cst = Cst.Cst00,
    PercentualIcms = 17.00m
};

var service = new TributacaoService(item);
var icms = service.ObterIcms();

// icms.BaseCalculo == 1000.00m
// icms.Valor == 170.00m
```

Os resultados isolados expõem, conforme o tributo, propriedades específicas. Para os resultados que seguem `IResultadoBase`, leia `BaseCalculo` e `Valor`; não recalcule esses valores no consumidor.

### PIS e COFINS com composição de base

```csharp
var item = new ItemTributavel
{
    ValorItem = 15.99m,
    QuantidadeItem = 1.000m,
    Cst = Cst.Cst00,
    CstPisCofins = CstPisCofins.Cst01,
    PercentualIcms = 12.00m,
    PercentualPis = 1.65m,
    PercentualCofins = 7.60m,
    Frete = 1.02m,
    OutrasDespesas = 0.67m,
    Desconto = 0.85m,
    DeduzIcmsDaBaseDePisCofins = true
};

var service = new TributacaoService(item, TipoDesconto.Incondicional);
var pis = service.ObterPis();
var cofins = service.ObterCofins();

decimal basePis = pis.BaseCalculo;
decimal valorPis = pis.Valor;
decimal baseCofins = cofins.BaseCalculo;
decimal valorCofins = cofins.Valor;
```

Use `DeduzIcmsDaBaseDePisCofins` apenas quando a regra fiscal aplicável exigir a dedução. As reduções específicas usam `PercentualReducaoPis` e `PercentualReducaoCofins`.

## Cálculo consolidado com TributacaoProduto

`TributacaoProduto` recebe o item e o contexto da operação: regime tributário do emitente, tipo de operação, tipo de destinatário e, opcionalmente, tipo de desconto e cálculo de desoneração.

```csharp
var item = new ItemTributavel
{
    ValorItem = 1000.00m,
    QuantidadeItem = 1.000m,
    Servico = false,
    Cst = Cst.Cst00,
    PercentualIcms = 17.00m,
    CstPisCofins = CstPisCofins.Cst01,
    PercentualPis = 1.65m,
    PercentualCofins = 3.00m,
    PercentualIpi = 5.00m,
    PercentualFcp = 2.00m,
    PercentualDifalInterna = 18.00m,
    PercentualDifalInterestadual = 12.00m
};

var tributacao = new TributacaoProduto(
    item,
    Crt.RegimeNormal,
    TipoOperacao.OperacaoInterestadual,
    TipoPessoa.Fisica,
    TipoDesconto.Incondicional);

var resultado = tributacao.Calcular();

decimal baseIcms = resultado.BaseCalculoIcms;
decimal valorIcms = resultado.ValorIcms;
decimal valorIpi = resultado.ValorIpi;
decimal valorPis = resultado.ValorPis;
decimal valorCofins = resultado.ValorCofins;
decimal valorFcp = resultado.Fcp;
decimal valorDifal = resultado.ValorDifal;
decimal valorIcmsOrigem = resultado.ValorIcmsOrigem;
decimal valorIcmsDestino = resultado.ValorIcmsDestino;
```

Para ICMS-ST, preencha `Cst`, `PercentualIcms`, `PercentualIcmsSt`, `PercentualMva` e eventuais reduções antes de chamar `Calcular()`. Leia `BaseCalculoIcmsSt` e `ValorIcmsSt` no resultado.

Para Simples Nacional, escolha o `Csosn` aplicável, informe `PercentualCredito` quando houver crédito e instancie a classe com `Crt.SimplesNacional`. O resultado consolidado expõe `PercentualCredito` e `ValorCredito`.

## Descontos, operação e arredondamento

- Use `TipoDesconto.Incondicional` quando o desconto reduzir a base conforme a regra fiscal aplicável.
- O identificador público para desconto condicional é `TipoDesconto.Condincional` — a grafia faz parte da API e deve ser usada exatamente assim.
- Escolha `TipoOperacao.OperacaoInterna` ou `TipoOperacao.OperacaoInterestadual` a partir da operação real; esse contexto afeta, entre outros, DIFAL e ICMS.
- Informe `TipoPessoa.Fisica` ou `TipoPessoa.Juridica` corretamente; no cálculo consolidado de serviços isso influencia as retenções.
- Preserve os valores retornados pela biblioteca e aplique arredondamentos adicionais somente conforme a regra de emissão do documento. Não altere bases ou valores manualmente após o cálculo sem uma regra fiscal explícita.

## Regras para agentes consumidores

1. Crie testes no projeto consumidor para cada combinação fiscal suportada, com valores esperados de base e imposto.
2. Mapeie os dados fiscais para `IEntidadeProduto` antes de chamar a biblioteca; não misture regras de apresentação, XML ou persistência com o cálculo.
3. Trate CST, CSOSN, CRT, alíquotas e benefícios como dados tributários configuráveis e auditáveis.
4. Revise legislação federal, estadual e municipal, além de regras de arredondamento do documento, antes de liberar uma operação em produção.
5. Valide o resultado com o responsável fiscal da empresa. A biblioteca está em evolução e os cálculos devem ser homologados no contexto da operação atendida.
