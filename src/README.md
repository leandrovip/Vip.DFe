# Vip.DFe

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![NuGet](https://img.shields.io/badge/nuget-v1.0.21-blue)](https://www.nuget.org/packages/Vip.DFe/)
[![.NET Standard 2.0](https://img.shields.io/badge/.NET%20Standard-2.0-green.svg)](https://docs.microsoft.com/pt-br/dotnet/standard/net-standard)

Biblioteca .NET para emissão e gerenciamento de Documentos Fiscais Eletrônicos (DFe) brasileiros, incluindo **NFe** (Nota Fiscal Eletrônica) e **CFe-SAT** (Cupom Fiscal Eletrônico - Sistema Autenticador e Transmissor).

## ?? Descrição

**Vip.DFe** é uma biblioteca completa e robusta desenvolvida em C# que facilita a integração de sistemas comerciais com os webservices da SEFAZ (Secretaria da Fazenda) para emissão de documentos fiscais eletrônicos. A biblioteca abstrai toda a complexidade de comunicação SOAP, serialização XML, assinatura digital e validação de schemas, permitindo que desenvolvedores foquem na lógica de negócio.

### Principais Funcionalidades

#### NFe (Nota Fiscal Eletrônica)
- ? **Autorização de NFe** - Envio síncrono e assíncrono (lote)
- ? **Consulta de Protocolo** - Verificação de situação da NFe
- ? **Cancelamento** - Cancelamento de NFe autorizada
- ? **Cancelamento por Substituição** - Cancelamento com nota substituta
- ? **Carta de Correção Eletrônica (CCe)** - Correção de informações da NFe
- ? **Inutilização de Numeração** - Inutilizar faixa de números
- ? **Consulta Status do Serviço** - Verificar disponibilidade da SEFAZ
- ? **Geração de DANFE** - Documento Auxiliar da Nota Fiscal Eletrônica
- ? **Assinatura Digital** - Suporte completo a certificados digitais A1 e A3
- ? **Validação de Schema** - Validação automática contra XSD da SEFAZ

#### CFe-SAT (Cupom Fiscal Eletrônico)
- ? **Envio de Venda** - Transmissão de cupom fiscal
- ? **Cancelamento de Venda** - Cancelamento de cupom
- ? **Consulta de Sessão** - Verificação de cupons emitidos
- ? **Testes Fim-a-Fim** - Validação de comunicação com SAT
- ? **Extração de Logs** - Obtenção de logs do equipamento SAT
- ? **Gerenciamento do SAT** - Ativação, bloqueio, desbloqueio e configuração
- ? **Suporte Multi-fabricante** - Compatível com diferentes modelos de SAT

## ?? Instalação

### Via NuGet Package Manager
```bash
Install-Package Vip.DFe
```

### Via .NET CLI
```bash
dotnet add package Vip.DFe
```

### Via PackageReference (arquivo .csproj)
```xml
<PackageReference Include="Vip.DFe" Version="1.0.21" />
```

## ?? Requisitos

- **.NET Standard 2.0** ou superior
- **.NET Framework 4.6.1+** ou **.NET Core 2.0+** ou **.NET 5/6/7/8+**
- **Certificado Digital A1 ou A3** (para NFe)
- **Equipamento SAT homologado** (para CFe-SAT)

## ?? Guia de Uso

### NFe - Exemplo Básico

#### 1. Configuração Inicial

```csharp
using Vip.DFe.NFe;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.Shared.Enum;
using Vip.DFe.NFe.Enum;

// Criar instância do serviço NFe
var nfeService = new NFeService();

// Configurar
nfeService.Configuracoes.CNPJ = "12345678000195";
nfeService.Configuracoes.Ambiente = TipoAmbiente.Homologacao;
nfeService.Configuracoes.Modelo = NFeModelo.NFe;
nfeService.Configuracoes.TipoEmissao = TipoEmissao.Normal;
nfeService.Configuracoes.Versao = NFeVersao.v400;

// Configurar certificado digital
nfeService.Configuracoes.Certificado.Arquivo = @"C:\certificado.pfx";
nfeService.Configuracoes.Certificado.Senha = "senha_do_certificado";

// Configurar webservices
nfeService.Configuracoes.Webservices.UF = CodigoUF.SP;
nfeService.Configuracoes.Webservices.TimeOut = 30000;

// Configurar arquivos
nfeService.Configuracoes.Arquivos.Salvar = true;
nfeService.Configuracoes.Arquivos.PathNFe = @"C:\NFe\Autorizadas";
nfeService.Configuracoes.Arquivos.PathEvento = @"C:\NFe\Eventos";
```

#### 2. Criar e Enviar NFe

```csharp
using Vip.DFe.NFe.NotaFiscal;

// Criar nova NFe
var nfe = new NFe
{
    InfNFe = new InfNFe
    {
        Versao = NFeVersao.v400,
        Ide = new NFeIde
        {
            CUF = CodigoUF.SP,
            NatOp = "VENDA DE MERCADORIA",
            Mod = NFeModelo.NFe,
            Serie = 1,
            NNF = 123,
            DhEmi = DateTime.Now,
            TpNF = NFeTipo.Saida,
            IdDest = NFeDestinoOperacao.Interna,
            CMunFG = 3550308, // Código IBGE de São Paulo
            TpImp = TipoImpressao.Retrato,
            TpEmis = TipoEmissao.Normal,
            TpAmb = TipoAmbiente.Homologacao,
            FinNFe = NFeFinalidade.Normal,
            IndFinal = NFeConsumidorFinal.Normal,
            IndPres = NFePresencaComprador.Presencial,
            ProcEmi = ProcessoEmissao.AplicativoContribuinte,
            VerProc = "1.0.0"
        },
        Emit = new NFeEmit
        {
            CNPJ = "12345678000195",
            XNome = "EMPRESA EXEMPLO LTDA",
            XFant = "EMPRESA EXEMPLO",
            EnderEmit = new NFeEmitEndereco
            {
                XLgr = "RUA EXEMPLO",
                Nro = "123",
                XBairro = "CENTRO",
                CMun = 3550308,
                XMun = "SAO PAULO",
                UF = CodigoUF.SP,
                CEP = "01234567"
            },
            IE = "123456789",
            CRT = RegimeTributario.SimplesNacional
        },
        Dest = new NFeDest
        {
            CNPJ = "98765432000100",
            XNome = "CLIENTE EXEMPLO",
            EnderDest = new NFeDestEndereco
            {
                XLgr = "AV EXEMPLO",
                Nro = "456",
                XBairro = "JARDIM EXEMPLO",
                CMun = 3550308,
                XMun = "SAO PAULO",
                UF = CodigoUF.SP,
                CEP = "09876543"
            },
            IndIEDest = NFeIndIeDest.ContribuinteICMS,
            IE = "987654321"
        }
    }
};

// Adicionar produtos
nfe.InfNFe.Det.Add(new NFeDetalhe
{
    NItem = 1,
    Prod = new NFeDetProduto
    {
        CProd = "001",
        CEAN = "SEM GTIN",
        XProd = "PRODUTO EXEMPLO",
        NCM = "12345678",
        CFOP = 5102,
        UCom = "UN",
        QCom = 10,
        VUnCom = 100.00m,
        VProd = 1000.00m,
        CEANTrib = "SEM GTIN",
        UTrib = "UN",
        QTrib = 10,
        VUnTrib = 100.00m,
        IndTot = NFeIndTotal.ValorItemCompoeTotalNF
    },
    Imposto = new NFeDetImposto
    {
        VTotTrib = 0.00m,
        Icms = new Icms
        {
            // Configurar impostos conforme necessidade
        }
    }
});

// Adicionar NFe na coleção
nfeService.Documentos.NFe.Add(nfe);

// Enviar para autorização
var resultado = nfeService.Autorizacao();

if (resultado.Resultado.CStat == 100) // Autorizada
{
    Console.WriteLine("NFe autorizada com sucesso!");
    Console.WriteLine($"Chave: {resultado.NFeAutorizada.ProtNFe.InfProt.ChNFe}");
    Console.WriteLine($"Protocolo: {resultado.NFeAutorizada.ProtNFe.InfProt.NProt}");
}
else
{
    Console.WriteLine($"Erro: {resultado.Resultado.CStat} - {resultado.Resultado.XMotivo}");
}
```

#### 3. Consultar NFe

```csharp
var chaveNFe = "35210912345678000195550010000001231000000001";
var consulta = nfeService.Consultar(chaveNFe);

if (consulta.Resultado.CStat == 100)
{
    Console.WriteLine($"NFe Autorizada - Protocolo: {consulta.Resultado.ProtNFe.InfProt.NProt}");
}
```

#### 4. Cancelar NFe

```csharp
var cnpj = "12345678000195";
var chave = "35210912345678000195550010000001231000000001";
var protocolo = "135210000000001";
var justificativa = "Cancelamento por erro de digitação no valor da mercadoria";

var cancelamento = nfeService.Cancelar(
    cnpj: cnpj,
    chave: chave,
    numeroProtocolo: protocolo,
    sequencialEvento: 1,
    justificativa: justificativa
);

if (cancelamento.Resultado.CStat == 135)
{
    Console.WriteLine("NFe cancelada com sucesso!");
}
```

#### 5. Emitir Carta de Correção Eletrônica (CCe)

```csharp
var cnpj = "12345678000195";
var chave = "35210912345678000195550010000001231000000001";
var correcao = "Corrigir a razão social do destinatário de EMPRESA X para EMPRESA Y";

var cce = nfeService.CartaoCorrecao(
    cnpj: cnpj,
    chave: chave,
    sequencialEvento: 1,
    correcao: correcao
);

if (cce.Resultado.CStat == 135)
{
    Console.WriteLine("CCe registrada com sucesso!");
}
```

### CFe-SAT - Exemplo Básico

#### 1. Configuração Inicial

```csharp
using Vip.DFe.SAT;
using Vip.DFe.SAT.Configuration;
using Vip.DFe.SAT.CupomFiscal;

// Criar instância do serviço SAT
var satService = new CFeService();

// Configurar
satService.Modelo = ModeloSat.DarthVader;
satService.CodigoAtivacao = "12345678";
satService.PathDll = @"C:\SAT\sat.dll";

// Configurar dados do estabelecimento
satService.Configuracoes.IdeCNPJ = "12345678000195";
satService.Configuracoes.IdeNumeroCaixa = 1;
satService.Configuracoes.EmitCNPJ = "12345678000195";
satService.Configuracoes.EmitIE = "123456789";
satService.Configuracoes.EmitIM = "987654";

// Configurar arquivos
satService.Arquivos.SalvarCFe = true;
satService.Arquivos.PastaCFeVenda = @"C:\SAT\Vendas";
satService.Arquivos.SepararPorCNPJ = true;
satService.Arquivos.SepararPorMes = true;

// Ativar o serviço
satService.Ativar();
```

#### 2. Consultar Status do SAT

```csharp
var status = satService.ConsultarStatusOperacional();

Console.WriteLine($"Código: {status.CodigoDeRetorno}");
Console.WriteLine($"Mensagem: {status.MensagemRetorno}");
Console.WriteLine($"Número de Serie: {status.NumeroSerie}");
Console.WriteLine($"Versão do Software: {status.VersaoSoftware}");
```

#### 3. Criar e Enviar Cupom Fiscal

```csharp
// Criar novo CFe
var cfe = satService.NewCFe();

// Configurar dados do cupom
cfe.InfCFe.Ide.NumeroCaixa = 1;
cfe.InfCFe.Ide.SignAC = "SGR-SAT";

// Adicionar destinatário (opcional)
cfe.InfCFe.Dest.CNPJ = "98765432000100";
cfe.InfCFe.Dest.XNome = "CLIENTE EXEMPLO";

// Adicionar produtos
cfe.InfCFe.Det.Add(new CFeDet
{
    NItem = 1,
    Prod = new CFeDetProd
    {
        CProd = "001",
        CEAN = "7891234567890",
        XProd = "PRODUTO EXEMPLO",
        NCM = "12345678",
        CFOP = "5102",
        UCom = "UN",
        QCom = 2,
        VUnCom = 10.00m,
        IndRegra = IndRegra.Truncamento
    },
    Imposto = new CFeDetImposto
    {
        Icms = new ImpostoIcms
        {
            Icms00 = new ImpostoIcms00
            {
                Orig = OrigemMercadoria.Nacional,
                CST = "00",
                PRedBC = 0,
                VBC = 20.00m,
                PICMS = 18.00m,
                VICMS = 3.60m
            }
        }
    }
});

// Configurar totais
cfe.InfCFe.Total.VCFe = 20.00m;

// Adicionar pagamentos
cfe.InfCFe.Pgto.MP.Add(new CFePgtoMP
{
    CMp = MeioPagamento.Dinheiro,
    VMp = 20.00m
});

// Enviar para o SAT
var resultado = satService.EnviarDadosVenda(cfe);

if (resultado.CodigoDeRetorno == 6000)
{
    Console.WriteLine("Cupom emitido com sucesso!");
    Console.WriteLine($"Chave: {resultado.Venda.InfCFe.Id}");
    Console.WriteLine($"Número: {resultado.Venda.InfCFe.Ide.NCFe}");
}
else
{
    Console.WriteLine($"Erro: {resultado.MensagemRetorno}");
}
```

#### 4. Cancelar Cupom Fiscal

```csharp
var cupomParaCancelar = resultado.Venda;
var cancelamento = satService.CancelarUltimaVenda(cupomParaCancelar);

if (cancelamento.CodigoDeRetorno == 7000)
{
    Console.WriteLine("Cupom cancelado com sucesso!");
}
```

### Geração de DANFE (PDF)

```csharp
using Vip.DFe.Danfe;

var danfeService = new DanfeService();

// Carregar NFe processada (XML autorizado)
var xmlNFe = File.ReadAllText(@"C:\NFe\nfe_autorizada.xml");
var nfeProc = NFeProc.LoadFromXml(xmlNFe);

// Configurar DANFE
danfeService.Configuracoes.Logomarca = @"C:\logo_empresa.png";
danfeService.Configuracoes.DocumentoCancelado = false;
danfeService.Configuracoes.Orientacao = Orientacao.Retrato;

// Gerar PDF
danfeService.GerarDanfe(nfeProc, @"C:\NFe\danfe.pdf");

Console.WriteLine("DANFE gerado com sucesso!");
```

## ?? Configurações Avançadas

### Certificado Digital

```csharp
// Certificado A1 (arquivo PFX)
nfeService.Configuracoes.Certificado.Arquivo = @"C:\certificado.pfx";
nfeService.Configuracoes.Certificado.Senha = "senha123";

// Certificado A1/A3 do repositório do Windows
nfeService.Configuracoes.Certificado.Serial = "1234567890ABCDEF";
nfeService.Configuracoes.Certificado.Tipo = TipoCertificado.A1UsoCertificado;

// Manter certificado em cache (melhor performance)
nfeService.Configuracoes.Certificado.ManterEmCache = true;
```

### Ambientes e Timeout

```csharp
// Ambiente
nfeService.Configuracoes.Ambiente = TipoAmbiente.Producao; // ou Homologacao

// Timeout para webservices (em milissegundos)
nfeService.Configuracoes.Webservices.TimeOut = 60000; // 60 segundos

// Tentativas de consulta após envio
nfeService.Configuracoes.Webservices.NumeroTentativas = 5;
nfeService.Configuracoes.Webservices.IntervaloTentativas = 2000; // 2 segundos
```

### Salvamento de Arquivos

```csharp
// Configurar salvamento automático
nfeService.Configuracoes.Arquivos.Salvar = true;
nfeService.Configuracoes.Arquivos.PathNFe = @"C:\NFe\Autorizadas";
nfeService.Configuracoes.Arquivos.PathEvento = @"C:\NFe\Eventos";
nfeService.Configuracoes.Arquivos.PathInutilizacao = @"C:\NFe\Inutilizadas";

// Separar por CNPJ e mês
nfeService.Configuracoes.Arquivos.SepararPorCNPJ = true;
nfeService.Configuracoes.Arquivos.SepararPorMes = true;
```

## ?? Testes

O projeto inclui uma aplicação Demo em Windows Forms (`Vip.DFe.Demo`) que demonstra todas as funcionalidades da biblioteca:

```bash
# Compilar e executar o projeto de demonstração
cd src/Vip.DFe.Demo
dotnet run
```

## ?? Estrutura do Projeto

```
Vip.DFe/
??? 0-Domain/               # Modelos de domínio (NFe, SAT, DANFE)
?   ??? NFe/               # Entidades da NFe
?   ??? SAT/               # Entidades do SAT
?   ??? Danfe/             # Modelos para geração de DANFE
?   ??? Shared/            # Entidades compartilhadas
??? 1-Services/            # Serviços de comunicação
?   ??? NFe/              # Serviços NFe (Autorização, Consulta, etc)
?   ??? SAT/              # Serviços SAT
?   ??? Danfe/            # Serviço de geração de DANFE
??? 2-Core/               # Núcleo da biblioteca
?   ??? Serializer/       # Serialização XML
?   ??? Cryptography/     # Assinatura digital
?   ??? Extensions/       # Extensões auxiliares
??? 3-Infra/              # Infraestrutura
    ??? Graphics/         # Recursos gráficos
    ??? Interops/         # Interoperabilidade nativa
```

## ?? Contribuindo

Contribuições são bem-vindas! Para contribuir:

1. Faça um **Fork** do projeto
2. Crie uma **branch** para sua feature (`git checkout -b feature/MinhaFeature`)
3. **Commit** suas mudanças (`git commit -m 'Adiciona nova funcionalidade'`)
4. Faça **Push** para a branch (`git push origin feature/MinhaFeature`)
5. Abra um **Pull Request**

### Padrões de Código

- Seguir convenções de código C#
- Adicionar testes unitários para novas funcionalidades
- Manter documentação XML atualizada
- Comentários em português

## ?? Licença

Este projeto está licenciado sob a **Licença MIT** - veja o arquivo [LICENSE](LICENSE) para detalhes.

```
MIT License

Copyright (c) 2024 Leandro Ferreira - VIP Soluções

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

## ?? Suporte e Contato

- **Autor**: Leandro Ferreira
- **Organização**: VIP Soluções
- **Repositório**: [https://github.com/leandrovip/Vip.DFe](https://github.com/leandrovip/Vip.DFe)
- **Issues**: [https://github.com/leandrovip/Vip.DFe/issues](https://github.com/leandrovip/Vip.DFe/issues)

## ?? Roadmap

- [ ] Suporte a NFC-e (Nota Fiscal de Consumidor Eletrônica) completo
- [ ] Implementação de CTe (Conhecimento de Transporte Eletrônico)
- [ ] Implementação de MDFe (Manifesto de Documentos Fiscais Eletrônicos)
- [ ] Melhorias na geração de DANFE com templates customizáveis
- [ ] Suporte a eventos adicionais da NFe 4.0
- [ ] Bibliotecas de exemplo em Blazor e ASP.NET Core

## ?? Avisos Importantes

- **Certificado Digital**: É obrigatório o uso de certificado digital válido (A1 ou A3) para operações com NFe
- **Homologação**: Sempre teste em ambiente de homologação antes de usar em produção
- **SEFAZ Virtual**: Alguns estados utilizam SEFAZ Virtual, a biblioteca detecta automaticamente
- **Schemas**: A biblioteca valida automaticamente contra os schemas da SEFAZ versão 4.0
- **Legislação**: Consulte a legislação vigente do seu estado para regras específicas

## ?? Documentação Adicional

- [Portal da NFe](http://www.nfe.fazenda.gov.br/)
- [Manual de Integração NFe](http://www.nfe.fazenda.gov.br/portal/listaConteudo.aspx?tipoConteudo=/fObHhLNwWM=)
- [Especificações SAT](https://portal.fazenda.sp.gov.br/servicos/sat/)

---

**Desenvolvido com ?? por [Leandro Ferreira](https://github.com/leandrovip) - VIP Soluções**
