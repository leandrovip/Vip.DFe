<h2 align="center"><strong>Vip.DFe</strong> - Documentos Fiscais Eletrônicos (BETA)</h2>

<p align="center">
  <a href="https://github.com/leandrovip/Vip.DFe/blob/master/LICENSE">
    <img alt="Licença" src="https://img.shields.io/github/license/leandrovip/Vip.DFe" />
  </a>

  <a href="https://www.nuget.org/packages/Vip.DFe/">
    <img alt="NuGet downloads" src="https://img.shields.io/nuget/dt/Vip.DFe?label=NuGet%20downloads&style=flat-square">
  </a>

  <a href="https://www.nuget.org/packages/Vip.DFe/">
    <img alt="NuGet" src="https://img.shields.io/nuget/v/Vip.DFe.svg">
  </a>
</p>

Biblioteca .NET para emissão e gestão de Documentos Fiscais Eletrônicos (DFe) brasileiros: NF-e, NFC-e, CF-e SAT e geração de DANFE em PDF.

> **Em desenvolvimento (beta).** A API pode mudar entre versões. Use por sua conta e risco e valide sempre em homologação antes de operar em produção.

## Funcionalidades

- **NF-e (modelo 55) e NFC-e (modelo 65)**: montagem do documento, assinatura XMLDSig, validação por XSD (leiaute 4.00) e comunicação com os webservices da SEFAZ (SOAP 1.2).
  - Autorização (síncrona e em lote), consulta de recibo, consulta por chave, status do serviço e inutilização.
  - Eventos: cancelamento, cancelamento por substituição e carta de correção (CC-e).
  - Reforma tributária: grupos de **IBS/CBS/IS** no item (`NFeDetImposto.IbsCbs` e `NFeDetImposto.IS`) e nos totais (`NFeTotal.IbsCbsTot` e `NFeTotal.IsTot`), além dos campos novos de identificação, referências e observações do item.
- **CF-e SAT**: comunicação com a DLL do fabricante (convenções `Cdecl` e `StdCall`) para ativação, venda, cancelamento, consultas, configuração de rede e extração de logs.
- **DANFE (PDF)**: DANFE de NF-e modelo 55 e DANFE de evento. Quando o XML informado não possui protocolo de autorização, o DANFE é gerado em **modo espelho / pré-visualização** com os avisos "DANFE SEM VALOR FISCAL" e "SEM AUTORIZAÇÃO DE USO".

> O DANFE suporta apenas NF-e modelo 55. Para NFC-e, `DanfeViewModelCreator` lança `NotSupportedException`.

## Instalação

```powershell
dotnet add package Vip.DFe
```

## Requisitos

- Biblioteca: `netstandard2.0` (.NET Framework 4.6.1+, .NET Core 2.0+, .NET 5+).
- Certificado digital ICP-Brasil **A1** (arquivo `.pfx` / bytes) ou **A3** (token/cartão, selecionado pelo número de série) para NF-e/NFC-e.
- Arquivos XSD de validação acessíveis em `NFeConfigArquivo.DiretorioSchemas` (a pasta `schemas/` do repositório).
- CF-e SAT: DLL do fabricante do equipamento SAT (caminho informado em `CFeService.PathDll`).

## Como utilizar

Os exemplos abaixo são esqueletos mínimos. Preencha os documentos conforme o leiaute oficial e valide sempre em homologação.

### Configuração do `NFeService`

```csharp
using System;
using Vip.DFe.NFe;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Shared.Enum;

var nfeService = new NFeService();
var cfg = nfeService.Configuracoes;

cfg.Ambiente = TipoAmbiente.Homologacao;
cfg.Modelo = NFeModelo.NFe;                 // NFeModelo.NFCe para NFC-e
cfg.Versao = NFeVersao.v400;
cfg.CNPJ = "00000000000000";
cfg.EnviarModoSincrono = true;              // usado por Autorizacao()

cfg.Webservices.UF = CodigoUF.SP;
cfg.Webservices.Timeout = TimeSpan.FromSeconds(60);   // TimeSpan?

// Certificado: caminho do arquivo ou número de série (A3/instalado)
cfg.Certificado.Certificado = @"C:\certificados\empresa.pfx";
cfg.Certificado.Senha = "senha";
cfg.Certificado.ManterEmCache = false;
// Alternativa: cfg.Certificado.CertificadoBytes = bytesDoPfx;

// Arquivos
cfg.Arquivos.Salvar = true;
cfg.Arquivos.Diretorio = @"C:\DFe\NFe";
cfg.Arquivos.DiretorioSchemas = @"C:\DFe\Schemas";
cfg.Arquivos.DiretorioAutorizadasBackup = @"D:\Backup\NFe";   // opcional
```

### Montagem da NF-e

```csharp
using Vip.DFe.NFe.NotaFiscal;

var nfe = nfeService.Documentos.AddNewNFe();   // já define Versao e Modelo conforme a configuração
// Alternativa: nfeService.Documentos.Add(nfeExistente);

var ide = nfe.InfNFe.Ide;
ide.CodigoUF = CodigoUF.SP;
ide.Serie = 1;
ide.NumeroNFe = 1;
ide.TipoNFe = NFeTipo.Saida;
ide.TipoEmissao = TipoEmissao.Normal;
ide.TipoImpressao = TipoImpressao.NormalRetrato;
ide.TpAmb = TipoAmbiente.Homologacao;

nfe.InfNFe.Emitente.CNPJ = "00000000000000";
nfe.InfNFe.Emitente.XNome = "EMPRESA EMITENTE LTDA";
nfe.InfNFe.Emitente.Endereco.Logradouro = "RUA EXEMPLO";

nfe.InfNFe.Destinatario.Nome = "CLIENTE EXEMPLO";

var item = nfe.InfNFe.Detalhe.AddNew();
item.NItem = 1;
item.Produto.XProd = "PRODUTO EXEMPLO";
// item.Imposto.Imposto = ...  (ICMS: Icms00, IcmsSn102, etc.)
// item.Imposto.IbsCbs = ...   (reforma tributária)

// ... preencha os demais grupos (totais, transporte, pagamento) conforme o leiaute.
```

### Autorização

```csharp
// Modo síncrono (Configuracoes.EnviarModoSincrono = true): envia a primeira NFe da coleção
var resposta = nfeService.Autorizacao();

// cStat 104 = lote processado; o protocolo da nota fica em Resultado.ProtNFe.InfProt
var prot = resposta.Resultado.ProtNFe.InfProt;
if (resposta.Resultado.CStat == 104 && resposta.NFeAutorizada != null)
{
    // NFeAutorizada (NFeProc) só é preenchida quando o cStat da nota
    // está entre os processados (100, 110, 150, 301, 302, 303); o procNFe já foi gravado em disco.
    Console.WriteLine($"Protocolo {prot.NProt} - {prot.CStat} {prot.Motivo}");
}

// Modo assíncrono (lote): o retorno traz o recibo; consulte-o depois
var lote = nfeService.AutorizacaoLote("1");
var recibo = lote.Resultado.InfRec.NRec;                       // preenchido apenas se o lote foi aceito
var retorno = nfeService.ConsultaAutorizacao(recibo);          // repete até cStat 104 (NumeroTentativas)
foreach (var procNFe in retorno.NFeAutorizadas)               // procNFe das notas processadas
    Console.WriteLine(procNFe.ProtNFe.InfProt.NProt);
```

### Consultas, eventos e inutilização

```csharp
var status = nfeService.ConsultaSituacaoServico();
var consulta = nfeService.Consultar(chave);              // consulta por chave de 44 dígitos
var inut = nfeService.Inutilizar(cnpj, "Justificativa com mais de 15 caracteres", NFeModelo.NFe, 1, 10, 12);

// Eventos: Resultado.CStat 128 = lote de eventos processado.
// O resultado de cada evento está em Resultado.RetEvento[i].InfEvento.CStat (135 = evento registrado).
var cancelamento = nfeService.Cancelar(cnpj, chave, numeroProtocolo, 1, "Justificativa com mais de 15 caracteres");
var cce = nfeService.CartaoCorrecao(cnpj, chave, 1, "Texto da correção com mais de 15 caracteres");

if (cancelamento.Resultado.CStat == 128)
{
    var evento = cancelamento.Resultado.RetEvento[0].InfEvento;
    // evento.CStat == 135 -> evento registrado e vinculado à NFe
    // cancelamento.ProcEvento é gravado em disco quando o evento é processado
}
```

Existe ainda `CancelarSubstituicao(...)`, cuja justificativa é limitada a 255 caracteres.

### CF-e SAT

```csharp
using Vip.DFe.SAT;
using Vip.DFe.SAT.Enum;

using var sat = new CFeService();
sat.Modelo = ModeloSat.StdCall;            // ou ModeloSat.Cdecl, conforme o fabricante
sat.PathDll = @"C:\SAT\sat.dll";
sat.CodigoAtivacao = "12345678";
sat.SignAC = "ASSINATURA_AC";              // apenas preenchida em CFe.InfCFe.Ide.SignAC; a assinatura do AC é gerada fora da lib
sat.Configuracoes.IdeCNPJ = "00000000000000";
sat.Configuracoes.EmitCNPJ = "00000000000000";

sat.Ativar();                              // obrigatório antes de qualquer comando

var status = sat.ConsultarStatusOperacional();

var cfe = sat.NewCFe();                    // já traz Ide/Emit preenchidos a partir de Configuracoes
cfe.InfCFe.Dest.Nome = "CLIENTE EXEMPLO";
var det = cfe.InfCFe.Det.AddNew();
// det.Prod / det.Imposto ...
var pgto = cfe.InfCFe.Pagto.Pagamentos.AddNew();   // CFePgtoMp: CMp, VMp, CAdmC
// ... preencha os demais grupos conforme o leiaute do SAT.

var venda = sat.EnviarDadosVenda(cfe);
if (venda.CodigoDeRetorno == 6000)         // 6000 = emitido com sucesso
{
    var chave = venda.ChaveConsulta;
}

var cancelamentoSat = sat.CancelarUltimaVenda(venda.Venda);   // 7000 = cancelado com sucesso
sat.Desativar();
```

### DANFE

```csharp
using Vip.DFe.Danfe;
using Vip.DFe.Danfe.Modelo;

// XML nfeProc (autorizada) ou NFe (sem protocolo)
var viewModel = DanfeViewModelCreator.CriarDeArquivoXml(@"C:\DFe\NFe\procNFe.xml");
// Alternativas: CriarDeStringXml(xml), CriarDeArquivoXml(Stream) e CreateFromXml(NFeProc)

using var danfe = new DanfeService(viewModel);
// danfe.AdicionarLogoImagem(@"C:\DFe\logo.jpg");   // opcional (JPEG não progressivo)
danfe.Gerar();
danfe.Salvar(@"C:\DFe\danfe.pdf");       // também aceita Stream; ObterPdfBytes(stream) retorna byte[]
```

- Se o XML for uma `NFe` sem `protNFe`, `viewModel.ModoEspelho` fica `true` e o PDF sai com os avisos "DANFE SEM VALOR FISCAL" e "SEM AUTORIZAÇÃO DE USO" (mais "AMBIENTE DE HOMOLOGAÇÃO" quando aplicável) e "SEM PROTOCOLO - PRÉ VISUALIZAÇÃO" no campo de protocolo.
- Somente modelo 55; formas de emissão suportadas: normal, SVC-AN e SVC-RS.
- DANFE de evento: `DanfeEventoViewModel` + `DanfeEventoService`.

## Demo

`src/Vip.DFe.Demo` é um aplicativo WinForms em **.NET Framework 4.8** (projeto MSBuild legado com `packages.config`). Não é executado com `dotnet run`:

```powershell
nuget restore .\src\Vip.DFe.sln
msbuild .\src\Vip.DFe.sln /p:Configuration=Debug
```

Depois, abra `src\Vip.DFe.Demo\bin\Debug\Vip.DFe.Demo.exe`. A Demo usa o pacote `Vip.Fiscal` apenas para calcular tributos e preenche os grupos de imposto, IBS e CBS da biblioteca (ver `docs/GUIA-CONSUMO-VIP-FISCAL.md`).

## Testes

```powershell
dotnet test .\src\Vip.DFe.Tests\Vip.DFe.Tests.csproj
```

A cobertura de testes ainda é pequena (chave, dígito verificador, configuração NFe e DANFE).

## Estrutura do projeto

```text
Vip.DFe/
├── AGENTS.md           # Instruções para agentes/contribuidores
├── LICENSE
├── README.md
├── docs/               # Documentação técnica (arquitetura, fluxos, módulos, convenções)
├── schemas/            # XSDs NF-e/NFC-e 4.00, eventos e XMLDSig usados na validação
└── src/
    ├── Vip.DFe.sln
    ├── Vip.DFe/        # Biblioteca (netstandard2.0)
    │   ├── 0-Domain/   # Modelos fiscais: NFe, SAT, DANFE, Shared
    │   ├── 1-Services/ # NFeService, CFeService, DanfeService, serviços SEFAZ, configuração
    │   ├── 2-Core/     # Serialização, assinatura, WCF/SOAP, chave, certificado
    │   └── 3-Infra/    # Exceções/Guard, gráficos, interops, writer
    ├── Vip.DFe.Demo/   # Aplicativo WinForms (.NET Framework 4.8)
    └── Vip.DFe.Tests/  # Testes xUnit
```

Documentação detalhada em [`docs/`](docs/) e diretrizes de contribuição em [`AGENTS.md`](AGENTS.md).

## Avisos importantes

- Use sempre o ambiente de **homologação** antes de emitir em produção.
- A validação por XSD exige que os schemas estejam acessíveis em `Configuracoes.Arquivos.DiretorioSchemas`.
- Consulte a legislação e o manual de integração vigentes (Portal da NF-e e especificações do SAT) para regras específicas de cada UF.

## Contribuindo

Contribuições são bem-vindas: faça um fork, crie uma branch, envie suas alterações e abra um Pull Request. Siga as convenções descritas em [`AGENTS.md`](AGENTS.md) e em [`docs/conventions/`](docs/conventions/). Problemas e sugestões: [Issues](https://github.com/leandrovip/Vip.DFe/issues).

## Créditos

Repositórios utilizados como base:

- [ACBr.Net](https://github.com/ACBrNet)
- [DFe.NET Zeus](https://github.com/ZeusAutomacao/DFe.NET)

## Licença

[MIT](LICENSE)
