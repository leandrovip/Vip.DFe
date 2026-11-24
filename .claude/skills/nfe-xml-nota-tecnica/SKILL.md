---
name: nfe-xml-nota-tecnica
description: Use when implementing NF-e, NFC-e, SAT, event, or other DF-e XML layout changes from SEFAZ/ENCAT Technical Notes, official schemas, or user-provided fiscal field specifications in Vip.DFe.
---

# nfe-xml-nota-tecnica

Esta skill orienta a implementação de novos campos, grupos e alterações de leiaute XML de documentos fiscais no projeto `Vip.DFe`.

Use-a quando houver necessidade de implementar alterações em NF-e, NFC-e, SAT, eventos ou outros DF-e com base em Notas Técnicas da SEFAZ/ENCAT, schemas XSD oficiais ou em uma solicitação que descreva os campos alterados.

## Contexto do projeto

O projeto `Vip.DFe` é uma biblioteca .NET `netstandard2.0` para emissão de NF-e/NFC-e e CFe-SAT.

A estrutura principal é:

```text
src/Vip.DFe/
├── 0-Domain/   # modelos XML fiscais
├── 1-Services/ # serviços NF-e/SAT, configurações e integrações
├── 2-Core/     # serializer, atributos, assinatura, XML, schemas
└── 3-Infra/    # exceções, helpers, writer, graphics
```

Modelos NF-e ficam principalmente em:

```text
src/Vip.DFe/0-Domain/NFe/NotaFiscal/
```

Exemplos importantes:

```text
Detalhe/NFeDetalhe.cs
Detalhe/NFeDetProduto.cs
Detalhe/NFeDetImposto.cs
Detalhe/Imposto/
Total/NFeTotal.cs
Total/NFeIcmsTot.cs
```

Schemas ficam em:

```text
schemas/
```

A serialização XML é baseada em atributos:

```csharp
[DFeElement]
[DFeAttribute]
[DFeCollection]
[DFeItem]
```

Com tipos e ocorrência:

```csharp
TipoCampo.De2
TipoCampo.De4
TipoCampo.Str
TipoCampo.StrNumberFill
TipoCampo.Int
TipoCampo.Enum

Ocorrencia.Obrigatoria
Ocorrencia.NaoObrigatoria
Ocorrencia.MaiorQueZero
```

Classes de domínio seguem o padrão:

```csharp
public sealed class MinhaClasse : GenericClone<MinhaClasse>, INotifyPropertyChanged
{
    #region Events

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Properties

    #endregion
}
```

## Objetivo da skill

Com esta skill, o agente deve:

- adicionar novo campo ao XML de NF-e/NFC-e/SAT/eventos;
- alterar campo existente;
- criar grupos e subgrupos;
- ajustar obrigatoriedade;
- ajustar tipo, tamanho, casas decimais e enumerações;
- incluir tags condicionais;
- atualizar schemas XSD somente quando solicitado;
- implementar alterações de Notas Técnicas;
- preservar compatibilidade retroativa;
- seguir os padrões de modelagem XML já existentes no `Vip.DFe`.

## Processo obrigatório

### 1. Entender a origem da alteração

Antes de editar código, identificar:

- número e versão da Nota Técnica, quando disponível;
- versão do leiaute afetada;
- documento afetado: NF-e, NFC-e, SAT, evento, inutilização, consulta etc.;
- campos, grupos e regras alteradas;
- schemas XSD relacionados;
- tabelas oficiais ou enums afetados;
- regras de validação citadas.

Se a Nota Técnica não estiver disponível, usar apenas as informações fornecidas pelo usuário e marcar explicitamente o que é hipótese.

Não implementar com base apenas no nome do campo.

### 2. Confirmar no schema quando possível

Procurar nos schemas locais antes de decidir onde implementar:

```text
schemas/leiauteNFe_v4.00.xsd
schemas/DFeTiposBasicos_v1.00.xsd
schemas/prod_leiauteNFe_v4.00.xsd
```

Buscar por:

- nome da tag;
- tipo XSD;
- `minOccurs`;
- `maxOccurs`;
- `sequence`;
- `choice`;
- elemento pai;
- posição entre irmãos.

Exemplo de aprendizado importante:

`vItem` poderia parecer campo de `prod`, mas o XSD mostra que é filho direto de `det`, depois de `obsItem` e antes de `DFeReferenciado`.

Portanto foi implementado em:

```text
NFeDetalhe.cs
```

e não em:

```text
NFeDetProduto.cs
```

### 3. Mapear o caminho XML completo

Para cada campo ou grupo, levantar:

```text
NFe/infNFe/det/imposto/IBSCBS/CST
```

E identificar:

- tag XML;
- pai XML;
- classe C# que representa o pai;
- ordem correta;
- ocorrência;
- tipo;
- tamanho mínimo e máximo;
- casas decimais;
- regra de preenchimento;
- regra de validação;
- modelo aplicável;
- versão do leiaute.

### 4. Inspecionar a arquitetura existente

Antes de criar nova classe, procurar padrões próximos.

Exemplos:

- campo de produto: `NFeDetProduto.cs`
- campo direto de item: `NFeDetalhe.cs`
- imposto do item: `NFeDetImposto.cs`
- grupos de ICMS/PIS/COFINS/IPI: `Detalhe/Imposto/`
- IBS/CBS/IS do item: `Detalhe/Imposto/IbsCbs/`
- totais: `Total/NFeTotal.cs` e classes em `Total/`

Não criar uma arquitetura nova se já existir um padrão equivalente.

### 5. Implementar no ponto correto

Regras práticas para o `Vip.DFe`:

- Se o campo é filho de `det`, implementar em `NFeDetalhe`.
- Se é filho de `prod`, implementar em `NFeDetProduto`.
- Se é filho de `imposto`, implementar em `NFeDetImposto` ou subgrupo em `Detalhe/Imposto`.
- Se é filho de `total`, implementar em `NFeTotal` ou nova classe em `Total`.
- Se é grupo novo de reforma tributária do item, preferir `Detalhe/Imposto/IbsCbs/`.
- Se é grupo de totais IBS/CBS/IS, preferir `NotaFiscal/Total/`.

### 6. Preservar ordem XML

O serializer ordena por `Ordem` quando informado. Sem `Ordem`, a ordem prática segue a declaração das propriedades.

Portanto:

- inserir propriedades na mesma sequência do XSD;
- não colocar campo em classe errada apenas por semelhança de nome;
- não assumir que a ordem será corrigida automaticamente.

Exemplo:

```text
infAdProd
obsItem
vItem
DFeReferenciado
```

### 7. Escolher tipos conforme padrão do projeto

Usar:

```csharp
decimal + TipoCampo.De2
```

para valores monetários `13v2`.

Usar:

```csharp
decimal + TipoCampo.De4
```

para percentuais.

Usar:

```csharp
string + TipoCampo.StrNumberFill
```

para códigos numéricos de tamanho fixo, como CST, cClassTrib, chave com 44 dígitos quando fizer sentido.

Usar:

```csharp
string + TipoCampo.Str
```

para textos.

Usar:

```csharp
TipoCampo.Enum
```

quando já existir enum fiscal no projeto.

Não usar `string` genericamente quando o projeto já tiver enum ou padrão específico.

### 8. Tratar campos opcionais corretamente

Não emitir tags opcionais vazias.

Para grupos opcionais:

- não inicializar no construtor por padrão;
- serializar apenas se preenchidos;
- criar `ShouldSerialize...()` quando necessário.

Exemplo:

```csharp
private bool ShouldSerializeDFeReferenciado()
{
    return DFeReferenciado?.ChaveAcesso.IsNotNullOrEmpty() == true;
}
```

Para `int` opcional:

```csharp
private bool ShouldSerializeNItem()
{
    return NItem > 0;
}
```

Para `decimal` opcional, usar `Ocorrencia.NaoObrigatoria` ou `MaiorQueZero`, conforme padrão existente.

### 9. Consolidar classes repetidas com critério

Pode consolidar classes quando:

- a estrutura interna é igual;
- as tags XML internas são iguais;
- só mudam os IDs por contexto.

Nesse caso:

- criar uma classe única;
- remover `Id` dos atributos internos se os IDs variam;
- documentar IDs nos comentários XML;
- manter os IDs nos grupos externos.

Exemplo seguro:

```csharp
public class Diferimento
{
    /// UB22 (IBS UF), UB41 (IBS Município), UB60 (CBS)
    [DFeElement(TipoCampo.De4, "pDif", ...)]
    public decimal PDif { get; set; }

    /// UB23 (IBS UF), UB42 (IBS Município), UB61 (CBS)
    [DFeElement(TipoCampo.De2, "vDif", ...)]
    public decimal VDif { get; set; }
}
```

Não consolidar quando as tags internas diferem.

Exemplo de não consolidação:

```text
adRemIBS  vs adRemCBS
vIBSMono  vs vCBSMono
pIBSMono  vs pCBSMono
```

Consolidar esses casos exigiria propriedades opcionais demais e deixaria a API confusa.

### 10. Atualizar schemas somente se solicitado

Se o usuário pedir atualização de XSD:

- localizar o schema afetado;
- usar schema oficial SEFAZ/ENCAT;
- preservar namespaces;
- preservar imports/includes;
- respeitar `sequence`, `choice`, `minOccurs`, `maxOccurs`;
- não inventar estrutura.

Se o usuário não pedir XSD, não alterar schemas.

### 11. Testes

Quando o escopo incluir testes, criar ou atualizar testes para:

- serialização com campo preenchido;
- omissão quando opcional;
- desserialização;
- ordem da tag;
- cardinalidade;
- enum válido/inválido;
- limites de tamanho;
- casas decimais;
- grupos condicionais;
- compatibilidade com XML antigo;
- validação contra XSD oficial.

Se o projeto ainda não tiver testes equivalentes para o ponto alterado, informar isso e propor teste mínimo.

### 11a. Atualizar o changelog de NTs

Antes de começar, consulte `docs/notas-tecnicas.md` para ver o status atual da NT. Ao concluir, atualize a linha da NT (status, evidência arquivo:linha, versão da NT usada) e a tabela "Histórico de implementação" com o commit.

### 12. Verificação obrigatória

Depois de editar código no `Vip.DFe`, executar pelo menos:

```powershell
dotnet build .\src\Vip.DFe\Vip.DFe.csproj
```

Se tocar em `0-Domain`, `2-Core`, DANFE ou testes, considerar:

```powershell
dotnet test .\src\Vip.DFe.Tests\Vip.DFe.Tests.csproj
```

Não afirmar que a tarefa está concluída sem verificação fresca.

Reportar warnings separadamente e indicar se são preexistentes.

## Regras obrigatórias

- Não inventar tag, tipo, tamanho, cardinalidade ou regra.
- Priorizar Nota Técnica, schemas oficiais e schemas locais.
- Informar quando uma informação não estiver disponível.
- Não alterar XSD se não for solicitado.
- Não alterar versão do `.csproj`.
- Não alterar serviços se o escopo for apenas modelo XML.
- Não implementar cálculo automático sem pedido explícito.
- Não remover campos antigos sem confirmação.
- Não quebrar contratos públicos sem necessidade.
- Não fazer refatoração ampla fora do escopo.
- Seguir nomenclatura e organização de pastas do projeto.
- Preservar compatibilidade retroativa.

## Fontes prioritárias

Usar, nesta ordem:

1. Nota Técnica oficial SEFAZ/ENCAT;
2. Portal Nacional da NF-e;
3. Manual de Orientação do Contribuinte;
4. schemas XSD oficiais;
5. tabelas oficiais;
6. informes técnicos;
7. schemas já existentes em `schemas/`;
8. documentação em `docs/`;
9. código-fonte atual.

Blogs e implementações de terceiros só podem ser referência complementar, nunca fonte principal.

## Resposta inicial esperada

Antes de implementar, apresentar plano breve:

```markdown
## Plano de implementação

1. Confirmar campos no schema/Nota Técnica.
2. Mapear caminho XML e classe C# correspondente.
3. Atualizar modelos de domínio.
4. Ajustar grupos opcionais e ShouldSerialize.
5. Criar ou atualizar enums, se necessário.
6. Atualizar XSD somente se solicitado.
7. Executar build/testes relevantes.
```

## Resposta final esperada

Após alterar código, responder:

```markdown
## Alterações realizadas

- Arquivo:
  - Alteração:
  - Motivo:
  - Referência na NT/schema:

## Verificação

- Comando:
- Resultado:

## Validação XML, se aplicável

- Schema utilizado:
- Ordem conferida:
- Tags opcionais omitidas:
- Compatibilidade retroativa:

## Pendências ou incertezas

- ...
```

## Casos que exigem confirmação

Interromper e perguntar ao usuário quando:

- a Nota Técnica não estiver disponível;
- houver divergência entre NT e XSD;
- o repositório não deixar clara a versão do leiaute;
- existirem várias classes possíveis para o mesmo grupo;
- a mudança puder quebrar compatibilidade;
- a regra depender de decisão de negócio;
- o usuário pedir algo que contrarie schema oficial;
- atualizar XSD for necessário mas não tiver sido autorizado.

## Exemplo de solicitação

```text
Implemente o grupo UB da NT 2025.002-RTC no item da NF-e.
```

Resposta esperada do agente:

```markdown
## Plano de implementação

1. Conferir grupo UB no schema/NT.
2. Mapear o pai XML: det/imposto.
3. Criar modelos em Detalhe/Imposto/IbsCbs.
4. Adicionar propriedades opcionais em NFeDetImposto.
5. Garantir que grupos opcionais não serializem vazios.
6. Compilar Vip.DFe.
```
