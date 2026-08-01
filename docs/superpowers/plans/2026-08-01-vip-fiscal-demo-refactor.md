# Vip Fiscal Demo Refactor Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Refatorar o `Vip.DFe.Demo` para consumir a API nova do `Vip.Fiscal` com `IEntidadeProduto`, `TributacaoProduto`/`TributacaoService`, e preencher as tags/propriedades IBS/CBS na NF-e/NFC-e gerada.

**Architecture:** Manter a alteração mínima no WinForms: o `Item` do grid continua simples, um adaptador fiscal cria a entrada exigida pelo `Vip.Fiscal`, e um serviço pequeno concentra cálculo e mapeamento para o XML. O formulário passa a pedir um resultado fiscal pronto, reduzindo uso direto de classes antigas como `Vip.Fiscal.Imposto.Icms.Icms00`.

**Tech Stack:** .NET Framework 4.8 WinForms, projeto legado `packages.config`, `Vip.Fiscal 1.2.7-beta`, `Vip.DFe` domínio NF-e, C#.

---

## Files

- Create: `src/Vip.DFe.Demo/Models/ItemTributavelDemo.cs`
  - Adapter implementing `Vip.Fiscal.Interfaces.IEntidadeProduto` using current `Item` commercial data plus fixed demo tax defaults.
- Create: `src/Vip.DFe.Demo/Services/ResultadoTributacaoDemo.cs`
  - DTO carrying the original item, fiscal input, `TributacaoProduto` consolidated result, isolated IBS/CBS results, and helper totals.
- Create: `src/Vip.DFe.Demo/Services/TributacaoDemoService.cs`
  - Builds `ItemTributavelDemo`, calls `TributacaoProduto.Calcular()`, calls `TributacaoService.ObterIbs()`, `ObterIbsMunicipal()`, and `ObterCbs()`, then returns `ResultadoTributacaoDemo`.
- Modify: `src/Vip.DFe.Demo/frmPrincipal.cs`
  - Remove old `Vip.Fiscal.Imposto.Icms.Icms00` alias.
  - Add service field.
  - Generate item taxation once per item.
  - Use new result to fill ICMS/PIS/COFINS and IBS/CBS detail tags.
  - Fill total `IBSCBSTot` from item results.
- Modify: `src/Vip.DFe.Demo/Vip.DFe.Demo.csproj`
  - Include new source files.
- Verification: `msbuild .\src\Vip.DFe.Demo\Vip.DFe.Demo.csproj /p:Configuration=Debug`

## Evidence path

Claim: The demo no longer uses the old direct `Vip.Fiscal.Imposto.Icms.Icms00` API and generated NF-e/NFC-e objects include IBS/CBS detail and total groups when demo IBS/CBS rates are configured.

Preferred evidence:
1. Compile the Demo project with MSBuild.
2. Search source to confirm no active `Vip.Fiscal.Imposto.Icms.Icms00` usage remains.
3. Generate XML manually from the Demo, or run the generation path if an existing automation exists, and inspect XML for `<IBSCBS>` and `<IBSCBSTot>`.

Limitation: The Demo is WinForms and certificate/service operations are interactive; compilation plus XML generation is enough for this refactor, but SEFAZ authorization is outside scope.

---

### Task 1: Add fiscal adapter for Vip.Fiscal

**Files:**
- Create: `src/Vip.DFe.Demo/Models/ItemTributavelDemo.cs`
- Modify: `src/Vip.DFe.Demo/Vip.DFe.Demo.csproj`

- [ ] **Step 1: Create adapter file**

Create `src/Vip.DFe.Demo/Models/ItemTributavelDemo.cs`:

```csharp
using Vip.Fiscal.Enums;
using Vip.Fiscal.Interfaces;

namespace Vip.DFe.Demo.Models
{
    public sealed class ItemTributavelDemo : IEntidadeProduto
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

        public static ItemTributavelDemo From(Item item)
        {
            return new ItemTributavelDemo
            {
                TipoDocumento = TipoDocumento.NFe,
                ValorItem = item.ValorItem,
                QuantidadeItem = item.Quantidade,
                Frete = item.Frete,
                Seguro = item.Seguro,
                OutrasDespesas = item.Outros,
                Desconto = item.Desconto,
                Servico = false,
                AtivoImobilizadoOuUsoeConsumo = false,

                Cst = Cst.Cst00,
                Csosn = Csosn.Csosn102,
                PercentualIcms = 18.00m,

                CstPisCofins = CstPisCofins.Cst01,
                PercentualPis = 1.65m,
                PercentualCofins = 7.60m,
                DeduzIcmsDaBaseDePisCofins = false,

                CstIbsCbs = CstIbsCbs.Cst000,
                PercentualIbsUF = 0.10m,
                PercentualIbsMunicipal = 0.00m,
                PercentualCbs = 0.90m
            };
        }
    }
}
```

- [ ] **Step 2: Include adapter in csproj**

In `src/Vip.DFe.Demo/Vip.DFe.Demo.csproj`, add the compile item near the other `Models` entries:

```xml
<Compile Include="Models\ItemTributavelDemo.cs" />
```

- [ ] **Step 3: Build check**

Run:

```powershell
msbuild .\src\Vip.DFe.Demo\Vip.DFe.Demo.csproj /p:Configuration=Debug
```

Expected: build succeeds, or only existing environment/dependency errors unrelated to the new type. If `CstIpi` requires a non-zero default to compile/use, keep the property default and do not emit IPI in this task.

---

### Task 2: Add taxation service for Demo

**Files:**
- Create: `src/Vip.DFe.Demo/Services/ResultadoTributacaoDemo.cs`
- Create: `src/Vip.DFe.Demo/Services/TributacaoDemoService.cs`
- Modify: `src/Vip.DFe.Demo/Vip.DFe.Demo.csproj`

- [ ] **Step 1: Create result DTO**

Create `src/Vip.DFe.Demo/Services/ResultadoTributacaoDemo.cs`:

```csharp
using Vip.DFe.Demo.Models;
using Vip.Fiscal.Interfaces.Resultados;

namespace Vip.DFe.Demo.Services
{
    public sealed class ResultadoTributacaoDemo
    {
        public Item Item { get; set; }
        public ItemTributavelDemo Entidade { get; set; }
        public Vip.Fiscal.TributacaoProduto Produto { get; set; }
        public IResultadoIbs IbsUf { get; set; }
        public IResultadoIbsMunicipal IbsMunicipal { get; set; }
        public IResultadoCbs Cbs { get; set; }

        public decimal BaseIbsCbs => IbsUf?.BaseCalculo ?? IbsMunicipal?.BaseCalculo ?? Cbs?.BaseCalculo ?? 0;
        public decimal ValorIbsUf => IbsUf?.Valor ?? 0;
        public decimal ValorIbsMunicipal => IbsMunicipal?.Valor ?? 0;
        public decimal ValorIbs => ValorIbsUf + ValorIbsMunicipal;
        public decimal ValorCbs => Cbs?.Valor ?? 0;
    }
}
```

- [ ] **Step 2: Create service**

Create `src/Vip.DFe.Demo/Services/TributacaoDemoService.cs`:

```csharp
using Vip.DFe.Demo.Models;
using Vip.DFe.Shared.Enum;
using Vip.Fiscal;
using Vip.Fiscal.Enums;

namespace Vip.DFe.Demo.Services
{
    public sealed class TributacaoDemoService
    {
        public ResultadoTributacaoDemo Calcular(Item item, RegimeTributario regimeTributario, NFeDestinoOperacao destinoOperacao)
        {
            var entidade = ItemTributavelDemo.From(item);
            var crt = regimeTributario == RegimeTributario.SimplesNacional ? Crt.SimplesNacional : Crt.RegimeNormal;
            var tipoOperacao = destinoOperacao == NFeDestinoOperacao.Interestadual ? TipoOperacao.OperacaoInterestadual : TipoOperacao.OperacaoInterna;

            var produto = new TributacaoProduto(
                entidade,
                crt,
                tipoOperacao,
                TipoPessoa.Juridica,
                TipoDesconto.Incondicional,
                null).Calcular();

            var service = new TributacaoService(entidade, TipoDesconto.Incondicional, null);

            return new ResultadoTributacaoDemo
            {
                Item = item,
                Entidade = entidade,
                Produto = produto,
                IbsUf = service.ObterIbs(),
                IbsMunicipal = service.ObterIbsMunicipal(),
                Cbs = service.ObterCbs()
            };
        }
    }
}
```

- [ ] **Step 3: Include service files in csproj**

In `src/Vip.DFe.Demo/Vip.DFe.Demo.csproj`, add:

```xml
<Compile Include="Services\ResultadoTributacaoDemo.cs" />
<Compile Include="Services\TributacaoDemoService.cs" />
```

- [ ] **Step 4: Build check**

Run:

```powershell
msbuild .\src\Vip.DFe.Demo\Vip.DFe.Demo.csproj /p:Configuration=Debug
```

Expected: build succeeds. If `NFeDestinoOperacao.Interestadual` has a different enum member name, inspect `src/Vip.DFe/0-Domain/NFe/Enum/NFeDestinoOperacao.cs` and map the existing member for interstate operation.

---

### Task 3: Use new service in frmPrincipal for ICMS/PIS/COFINS

**Files:**
- Modify: `src/Vip.DFe.Demo/frmPrincipal.cs`

- [ ] **Step 1: Adjust usings**

At the top of `frmPrincipal.cs`:

Remove:

```csharp
using Icms00 = Vip.Fiscal.Imposto.Icms.Icms00;
```

Add:

```csharp
using Vip.DFe.Demo.Services;
```

- [ ] **Step 2: Add service field and initialize it**

Near existing fields:

```csharp
private readonly TributacaoDemoService _tributacaoDemoService;
```

In the constructor after `_configuracao = _serviceConfiguracao.Obter();`:

```csharp
_tributacaoDemoService = new TributacaoDemoService();
```

- [ ] **Step 3: Calculate taxation once per item**

In `ObterDocumento()`, inside the item loop before creating `NFeDetalhe`, add:

```csharp
var tributacao = _tributacaoDemoService.Calcular(item, _configuracao.EmitenteRegimeTributario, _configuracao.DestinoOperacao);
```

Change:

```csharp
Imposto = GerarImposto(item),
```

To:

```csharp
Imposto = GerarImposto(tributacao),
```

- [ ] **Step 4: Replace GerarIcms signature and implementation**

Replace:

```csharp
private INFeImposto GerarIcms(Item item)
```

With:

```csharp
private INFeImposto GerarIcms(ResultadoTributacaoDemo tributacao)
```

Inside the method, replace the `case "00"` body with:

```csharp
case "00":
    icms = new NFe.NotaFiscal.Detalhe.Imposto.Estadual.Icms00
    {
        Origem = origem,
        ModBC = NFeModalidadeBC.ValorOperacao,
        VBc = tributacao.Produto.BaseCalculoIcms,
        PIcms = tributacao.Entidade.PercentualIcms,
        VIcms = tributacao.Produto.ValorIcms,
        PFcp = tributacao.Entidade.PercentualFcp,
        VFcp = tributacao.Produto.Fcp
    };
    break;
```

Keep the Simples Nacional branch as `IcmsSn102` for minimum scope.

- [ ] **Step 5: Replace GerarImposto signature**

Find `GerarImposto(Item item)` and change to:

```csharp
private NFeDetImposto GerarImposto(ResultadoTributacaoDemo tributacao)
{
    return new NFeDetImposto
    {
        Imposto = GerarIcms(tributacao),
        Pis = GerarPis(tributacao),
        Cofins = GerarCofins(tributacao),
        IbsCbs = GerarIbsCbs(tributacao)
    };
}
```

- [ ] **Step 6: Replace PIS/COFINS helpers**

Replace current `GerarPis()` with:

```csharp
private Pis GerarPis(ResultadoTributacaoDemo tributacao)
{
    INFePis pis = new PisAliq
    {
        Cst = "01",
        VBc = tributacao.Produto.BaseCalculoPis,
        PPis = tributacao.Entidade.PercentualPis,
        VPis = tributacao.Produto.ValorPis
    };

    return new Pis {Imposto = pis};
}
```

Replace current `GerarCofins()` with:

```csharp
private Cofins GerarCofins(ResultadoTributacaoDemo tributacao)
{
    INFeCofins cofins = new CofinsAliq
    {
        Cst = "01",
        VBc = tributacao.Produto.BaseCalculoCofins,
        PCofins = tributacao.Entidade.PercentualCofins,
        VCofins = tributacao.Produto.ValorCofins
    };

    return new Cofins {Imposto = cofins};
}
```

- [ ] **Step 7: Build check**

Run:

```powershell
msbuild .\src\Vip.DFe.Demo\Vip.DFe.Demo.csproj /p:Configuration=Debug
```

Expected: build succeeds. If `PisAliq`/`CofinsAliq` names differ, inspect `src/Vip.DFe/0-Domain/NFe/NotaFiscal/Detalhe/Imposto/Federal` and use the existing aliquot classes implementing `INFePis`/`INFeCofins`.

---

### Task 4: Fill IBS/CBS detail tags

**Files:**
- Modify: `src/Vip.DFe.Demo/frmPrincipal.cs`

- [ ] **Step 1: Add IBS/CBS domain using**

At the top of `frmPrincipal.cs`, add:

```csharp
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs;
```

- [ ] **Step 2: Add detail mapper helper**

Near the other tax helper methods, add:

```csharp
private IbsCbs GerarIbsCbs(ResultadoTributacaoDemo tributacao)
{
    return new IbsCbs
    {
        Cst = "000",
        CClassTrib = "000001",
        GrupoIbsCbs = new GrupoIbsCbs
        {
            VBc = tributacao.BaseIbsCbs,
            IbsUf = new IbsUf
            {
                PIbsUf = tributacao.Entidade.PercentualIbsUF,
                VIbsUf = tributacao.ValorIbsUf
            },
            IbsMunicipio = new IbsMunicipio
            {
                PIbsMun = tributacao.Entidade.PercentualIbsMunicipal,
                VIbsMun = tributacao.ValorIbsMunicipal
            },
            VIbs = tributacao.ValorIbs,
            Cbs = new Cbs
            {
                PCbs = tributacao.Entidade.PercentualCbs,
                VCbs = tributacao.ValorCbs
            }
        }
    };
}
```

- [ ] **Step 3: Build check**

Run:

```powershell
msbuild .\src\Vip.DFe.Demo\Vip.DFe.Demo.csproj /p:Configuration=Debug
```

Expected: build succeeds and `NFeDetImposto.ShouldSerializeIbsCbs()` will serialize the group because `Cst` is non-empty.

---

### Task 5: Fill IBS/CBS totals

**Files:**
- Modify: `src/Vip.DFe.Demo/frmPrincipal.cs`

- [ ] **Step 1: Preserve per-item taxation results**

Before the item loop in `ObterDocumento()`, add:

```csharp
var tributacoes = new List<ResultadoTributacaoDemo>();
```

After calculating `tributacao` inside the loop, add:

```csharp
tributacoes.Add(tributacao);
```

- [ ] **Step 2: Add total using**

At the top of `frmPrincipal.cs`, ensure this namespace is already present:

```csharp
using Vip.DFe.NFe.NotaFiscal.Total;
```

It is already used by `NFeIcmsTot`; do not add a duplicate if present.

- [ ] **Step 3: Create IBS/CBS total after ICMS total**

After `var total = new NFeTotal {IcmsTot = icmsTot};`, add:

```csharp
total.IbsCbsTot = GerarIbsCbsTotal(tributacoes);
```

- [ ] **Step 4: Add total helper**

Near tax helper methods, add:

```csharp
private NFeIbsCbsTot GerarIbsCbsTotal(IEnumerable<ResultadoTributacaoDemo> tributacoes)
{
    var lista = tributacoes.ToList();

    return new NFeIbsCbsTot
    {
        VBcIbsCbs = lista.Sum(x => x.BaseIbsCbs).Round(),
        Ibs = new NFeIbsTot
        {
            IbsUf = new NFeIbsUfTot
            {
                VIbsUf = lista.Sum(x => x.ValorIbsUf).Round()
            },
            IbsMunicipio = new NFeIbsMunicipioTot
            {
                VIbsMun = lista.Sum(x => x.ValorIbsMunicipal).Round()
            },
            VIbs = lista.Sum(x => x.ValorIbs).Round()
        },
        Cbs = new NFeCbsTot
        {
            VCbs = lista.Sum(x => x.ValorCbs).Round()
        }
    };
}
```

- [ ] **Step 5: Build check**

Run:

```powershell
msbuild .\src\Vip.DFe.Demo\Vip.DFe.Demo.csproj /p:Configuration=Debug
```

Expected: build succeeds. `NFeTotal.ShouldSerializeIbsCbsTot()` serializes totals because `VBcIbsCbs`, `Ibs`, or `Cbs` is present.

---

### Task 6: Verify generated XML contains IBS/CBS groups

**Files:**
- No source changes unless verification exposes a compile/serialization issue.

- [ ] **Step 1: Build Demo**

Run:

```powershell
msbuild .\src\Vip.DFe.Demo\Vip.DFe.Demo.csproj /p:Configuration=Debug
```

Expected: success.

- [ ] **Step 2: Source search**

Run:

```powershell
rg "Vip\.Fiscal\.Imposto\.Icms\.Icms00|new Icms00\(" src\Vip.DFe.Demo
```

Expected: no active old `Vip.Fiscal.Imposto.Icms.Icms00` construction. Domain XML class `NFe.NotaFiscal...Icms00` remains valid.

- [ ] **Step 3: Manual Demo XML generation**

Open `src/Vip.DFe.Demo/bin/Debug/Vip.DFe.Demo.exe`, configure homologation/schemas if needed, generate an XML for NF-e or NFC-e without authorization.

Expected XML fragments in the generated item:

```xml
<IBSCBS>
  <CST>000</CST>
  <cClassTrib>000001</cClassTrib>
  <gIBSCBS>
    <vBC>...</vBC>
    <gIBSUF>
      <pIBSUF>...</pIBSUF>
      <vIBSUF>...</vIBSUF>
    </gIBSUF>
    <gIBSMun>
      <pIBSMun>...</pIBSMun>
      <vIBSMun>...</vIBSMun>
    </gIBSMun>
    <vIBS>...</vIBS>
    <gCBS>
      <pCBS>...</pCBS>
      <vCBS>...</vCBS>
    </gCBS>
  </gIBSCBS>
</IBSCBS>
```

Expected XML fragments in totals:

```xml
<IBSCBSTot>
  <vBCIBSCBS>...</vBCIBSCBS>
  <gIBS>...</gIBS>
  <gCBS>...</gCBS>
</IBSCBSTot>
```

If serialization omits fields with zero values that are mandatory in the model, increase demo percentages to non-zero values in `ItemTributavelDemo.From` for `PercentualIbsMunicipal` as well.

---

## Self-review notes

- Scope matches the approved minimal refactor and treats IBS/CBS as XML/tag filling, not unit-test-only behavior.
- No broad UI/grid changes are included.
- `TributacaoProduto` in the installed DLL does not expose consolidated IBS/CBS properties; plan therefore uses `TributacaoService.ObterIbs()`, `ObterIbsMunicipal()`, and `ObterCbs()` for those values while keeping `TributacaoProduto` for consolidated ICMS/PIS/COFINS.
- `cClassTrib = "000001"` is a demo placeholder classification for serialization. Before production use, replace it with a fiscal classification validated by the responsible fiscal team.
