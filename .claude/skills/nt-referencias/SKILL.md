---
name: nt-referencias
description: Use when the user asks to check, download or update NF-e/NFC-e Technical Notes (NT), schema packages (PL_xxx) or deadlines from the Portal Nacional da NF-e, or asks "saiu NT nova?", "o que mudou nas NTs", "atualizar referências/cronograma" in Vip.DFe.
---

# nt-referencias

Captura e triagem das Notas Técnicas e pacotes de schemas oficiais da NF-e/NFC-e para o `Vip.DFe`.
Esta skill **não implementa** leiaute: para isso use `nfe-xml-nota-tecnica`.

## Fonte

- **Portal Nacional da NF-e** (`https://www.nfe.fazenda.gov.br/portal/`) é a fonte oficial e a mais completa.
- O espelho SVRS (`dfe-portal.svrs.rs.gov.br/NFe/Documentos`) **não** tem todos os pacotes de schema (em 2026-11-26 parava no PL_010c, enquanto o portal já tinha o PL_010h). Não use como fonte primária.

## Submódulo `referencias/`

O acervo fica no repositório **`dfe-referencia`** (`ssh://git@dev.vipsolucoes.com:3022/vipsolucoes/dfe-referencia.git`), montado como submódulo em `referencias/`.

- **Antes de tudo**, confira se está inicializado: se `referencias/manifesto.json` não existir, rode `git submodule update --init referencias` (ou peça ao usuário, se não houver acesso SSH).
- O submódulo fica em *detached HEAD*: antes de alterar algo nele, `git -C referencias checkout master && git -C referencias pull`.
- O CI (`actions/checkout`) **não** baixa o submódulo; a lib não depende dele para compilar.
- Na máquina do Leandro, a cópia principal do acervo é `E:\dev\dfe-referencia` (compartilhada com o `Vip.Seed`; é onde ficam os `_originais/`). Prefira rodar o script lá e, no Vip.DFe, só atualizar o ponteiro: `git -C referencias pull` (ou `git submodule update --remote referencias`) e commit. No submódulo, `_originais/` pode não existir: o PDF original pode ser baixado pela URL do cabeçalho de cada `.md`.

## Onde fica cada coisa

| Caminho | Conteúdo | Repositório |
|---|---|---|
| `referencias/tools/Baixar-Referencias.ps1` | Script de download incremental | dfe-referencia |
| `referencias/manifesto.json` | Índice: seção, título, `conteudoId`, arquivo, sha256, data | dfe-referencia |
| `referencias/nt/*.md` | Texto extraído das NTs (pdftotext) | dfe-referencia |
| `referencias/schemas/<pacote>/` | XSD extraídos dos pacotes, sem alteração | dfe-referencia |
| `referencias/_originais/` | PDFs e ZIPs originais | ignorado (local) |
| `docs/notas-tecnicas.md` | Situação da lib frente a cada NT | Vip.DFe |
| `docs/cronograma-nt.md` | Datas de homologação/produção | Vip.DFe |
| `schemas/` | XSD usados pela lib em runtime | Vip.DFe |

## Fluxo

1. **Listar o que é novo** (sem baixar):
   `powershell -NoProfile -ExecutionPolicy Bypass -File referencias\tools\Baixar-Referencias.ps1 -SomenteListar`
2. **Baixar** (incremental; o manifesto evita baixar de novo):
   `powershell -NoProfile -ExecutionPolicy Bypass -File referencias\tools\Baixar-Referencias.ps1`
   Parâmetros úteis: `-Secoes NotasTecnicas|Esquemas`, `-DesdeAno <ano>` (padrão 0 = tudo), `-Maximo N`.
3. **Triar cada item novo** lendo o `.md` em `referencias/nt/` (ou os XSD em `referencias/schemas/`):
   - Tema, versão, e se substitui versão anterior.
   - **Datas** de homologação e produção → atualizar `docs/cronograma-nt.md`.
   - Impacto no XML gerado pela lib (tags, grupos, eventos, obrigatoriedade) → atualizar a linha da NT em `docs/notas-tecnicas.md` (status Implementado/Parcial/Pendente/N/A/A analisar).
   - Novo pacote de schema: comparar com `schemas/` (`git diff --no-index schemas/leiauteNFe_v4.00.xsd referencias/schemas/PL_xxx/NFe/leiauteNFe_v4.00.xsd`) e listar o que muda. **Não** copie para `schemas/` sem pedido explícito: isso faz parte da implementação (`nfe-xml-nota-tecnica`).
4. **Reportar** ao usuário: o que saiu, o que afeta a lib, prazos próximos (≤ 60 dias) e sugestão de prioridade.
5. **Commits (dois, cada um com confirmação explícita do usuário)**:
   1. No submódulo: `git -C referencias add -A && git -C referencias commit ...` e push para o `dfe-referencia`.
   2. No Vip.DFe: commit do novo ponteiro (`git add referencias`) junto com as docs atualizadas.
   Nunca faça commit do ponteiro apontando para um commit que não foi enviado ao `dfe-referencia`.

## Regras

- O texto extraído do PDF **perde colunas de tabelas** (Ele/Pai/Tipo/Ocorrência/Tamanho). Tipo, tamanho, ocorrência e elemento pai vêm do **XSD**; do PDF use regras, datas e contexto. Na dúvida, abra o original em `referencias/_originais/`.
- O manifesto é indexado pelo `conteudoId` do portal. Itens distintos podem ter o **mesmo nome de arquivo** com conteúdo diferente (ex.: três `Eventos_RTC.zip`): o que chega depois ganha sufixo `_<sha256[0..8]>`. Confira no manifesto (`titulo`) qual versão é qual antes de usar.
- Datas e obrigatoriedades sem confirmação no PDF oficial ficam marcadas como **a confirmar**.
- Se o portal mudar o HTML e o script parar de achar itens, corrija o parser no script (no `dfe-referencia`) e registre a mudança aqui e no `README.md` dele.
