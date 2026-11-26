using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using Vip.DFe.Graphics;

namespace Vip.DFe.Danfe.Elementos
{
    internal class Barcode128C : ElementoBase
    {
        #region Fields

        private static readonly byte[][] Dic;

        #endregion

        #region Constructors

        static Barcode128C()
        {
            Dic = new[]
            {
                new byte[] {2, 1, 2, 2, 2, 2},
                new byte[] {2, 2, 2, 1, 2, 2},
                new byte[] {2, 2, 2, 2, 2, 1},
                new byte[] {1, 2, 1, 2, 2, 3},
                new byte[] {1, 2, 1, 3, 2, 2},
                new byte[] {1, 3, 1, 2, 2, 2},
                new byte[] {1, 2, 2, 2, 1, 3},
                new byte[] {1, 2, 2, 3, 1, 2},
                new byte[] {1, 3, 2, 2, 1, 2},
                new byte[] {2, 2, 1, 2, 1, 3},
                new byte[] {2, 2, 1, 3, 1, 2},
                new byte[] {2, 3, 1, 2, 1, 2},
                new byte[] {1, 1, 2, 2, 3, 2},
                new byte[] {1, 2, 2, 1, 3, 2},
                new byte[] {1, 2, 2, 2, 3, 1},
                new byte[] {1, 1, 3, 2, 2, 2},
                new byte[] {1, 2, 3, 1, 2, 2},
                new byte[] {1, 2, 3, 2, 2, 1},
                new byte[] {2, 2, 3, 2, 1, 1},
                new byte[] {2, 2, 1, 1, 3, 2},
                new byte[] {2, 2, 1, 2, 3, 1},
                new byte[] {2, 1, 3, 2, 1, 2},
                new byte[] {2, 2, 3, 1, 1, 2},
                new byte[] {3, 1, 2, 1, 3, 1},
                new byte[] {3, 1, 1, 2, 2, 2},
                new byte[] {3, 2, 1, 1, 2, 2},
                new byte[] {3, 2, 1, 2, 2, 1},
                new byte[] {3, 1, 2, 2, 1, 2},
                new byte[] {3, 2, 2, 1, 1, 2},
                new byte[] {3, 2, 2, 2, 1, 1},
                new byte[] {2, 1, 2, 1, 2, 3},
                new byte[] {2, 1, 2, 3, 2, 1},
                new byte[] {2, 3, 2, 1, 2, 1},
                new byte[] {1, 1, 1, 3, 2, 3},
                new byte[] {1, 3, 1, 1, 2, 3},
                new byte[] {1, 3, 1, 3, 2, 1},
                new byte[] {1, 1, 2, 3, 1, 3},
                new byte[] {1, 3, 2, 1, 1, 3},
                new byte[] {1, 3, 2, 3, 1, 1},
                new byte[] {2, 1, 1, 3, 1, 3},
                new byte[] {2, 3, 1, 1, 1, 3},
                new byte[] {2, 3, 1, 3, 1, 1},
                new byte[] {1, 1, 2, 1, 3, 3},
                new byte[] {1, 1, 2, 3, 3, 1},
                new byte[] {1, 3, 2, 1, 3, 1},
                new byte[] {1, 1, 3, 1, 2, 3},
                new byte[] {1, 1, 3, 3, 2, 1},
                new byte[] {1, 3, 3, 1, 2, 1},
                new byte[] {3, 1, 3, 1, 2, 1},
                new byte[] {2, 1, 1, 3, 3, 1},
                new byte[] {2, 3, 1, 1, 3, 1},
                new byte[] {2, 1, 3, 1, 1, 3},
                new byte[] {2, 1, 3, 3, 1, 1},
                new byte[] {2, 1, 3, 1, 3, 1},
                new byte[] {3, 1, 1, 1, 2, 3},
                new byte[] {3, 1, 1, 3, 2, 1},
                new byte[] {3, 3, 1, 1, 2, 1},
                new byte[] {3, 1, 2, 1, 1, 3},
                new byte[] {3, 1, 2, 3, 1, 1},
                new byte[] {3, 3, 2, 1, 1, 1},
                new byte[] {3, 1, 4, 1, 1, 1},
                new byte[] {2, 2, 1, 4, 1, 1},
                new byte[] {4, 3, 1, 1, 1, 1},
                new byte[] {1, 1, 1, 2, 2, 4},
                new byte[] {1, 1, 1, 4, 2, 2},
                new byte[] {1, 2, 1, 1, 2, 4},
                new byte[] {1, 2, 1, 4, 2, 1},
                new byte[] {1, 4, 1, 1, 2, 2},
                new byte[] {1, 4, 1, 2, 2, 1},
                new byte[] {1, 1, 2, 2, 1, 4},
                new byte[] {1, 1, 2, 4, 1, 2},
                new byte[] {1, 2, 2, 1, 1, 4},
                new byte[] {1, 2, 2, 4, 1, 1},
                new byte[] {1, 4, 2, 1, 1, 2},
                new byte[] {1, 4, 2, 2, 1, 1},
                new byte[] {2, 4, 1, 2, 1, 1},
                new byte[] {2, 2, 1, 1, 1, 4},
                new byte[] {4, 1, 3, 1, 1, 1},
                new byte[] {2, 4, 1, 1, 1, 2},
                new byte[] {1, 3, 4, 1, 1, 1},
                new byte[] {1, 1, 1, 2, 4, 2},
                new byte[] {1, 2, 1, 1, 4, 2},
                new byte[] {1, 2, 1, 2, 4, 1},
                new byte[] {1, 1, 4, 2, 1, 2},
                new byte[] {1, 2, 4, 1, 1, 2},
                new byte[] {1, 2, 4, 2, 1, 1},
                new byte[] {4, 1, 1, 2, 1, 2},
                new byte[] {4, 2, 1, 1, 1, 2},
                new byte[] {4, 2, 1, 2, 1, 1},
                new byte[] {2, 1, 2, 1, 4, 1},
                new byte[] {2, 1, 4, 1, 2, 1},
                new byte[] {4, 1, 2, 1, 2, 1},
                new byte[] {1, 1, 1, 1, 4, 3},
                new byte[] {1, 1, 1, 3, 4, 1},
                new byte[] {1, 3, 1, 1, 4, 1},
                new byte[] {1, 1, 4, 1, 1, 3},
                new byte[] {1, 1, 4, 3, 1, 1},
                new byte[] {4, 1, 1, 1, 1, 3},
                new byte[] {4, 1, 1, 3, 1, 1},
                new byte[] {1, 1, 3, 1, 4, 1},
                new byte[] {1, 1, 4, 1, 3, 1},
                new byte[] {3, 1, 1, 1, 4, 1},
                new byte[] {4, 1, 1, 1, 3, 1},
                new byte[] {2, 1, 1, 4, 1, 2},
                new byte[] {2, 1, 1, 2, 1, 4},
                new byte[] {2, 1, 1, 2, 3, 2},
                new byte[] {2, 3, 3, 1, 1, 1, 2}
            };
        }

        public Barcode128C(string code, Estilo estilo, float largura = 75F) : base(estilo)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("O código não pode ser vazio.", "code");

            code = code.ToUpperInvariant();

            if (!Regex.IsMatch(code, @"^[0-9A-Z]+$")) throw new ArgumentException("O código deve conter apenas dígitos numéricos e letras maiúsculas (A-Z).", "code");

            // Código só numérico de tamanho ímpar recebe o prefixo "0" (comportamento original em CODE-128C).
            if (code.Length % 2 != 0 && Regex.IsMatch(code, @"^\d+$"))
                Code = "0" + code;
            else
                Code = code;

            Largura = largura;
        }

        #endregion

        #region Properties

        public static readonly float MargemVertical = 2;

        /// <summary>
        ///     Código a ser codificado em barras.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        ///     Largura do código de barras.
        /// </summary>
        public float Largura { get; set; }

        #endregion

        #region Methods

        private const byte CodeA = 101;
        private const byte CodeC = 99;
        private const byte StartC = 105;

        private static bool EhDigito(char c)
        {
            return c >= '0' && c <= '9';
        }

        private static int ContarDigitos(string code, int inicio)
        {
            int n = 0;
            while (inicio + n < code.Length && EhDigito(code[inicio + n])) n++;
            return n;
        }

        /// <summary>
        ///     Converte o código em valores de símbolos Code 128 (Start C + dados), usando o modo híbrido C/A
        ///     da NT Conjunta DF-e 2025.001 (CNPJ alfanumérico). Não inclui checksum nem Stop.
        /// </summary>
        internal static List<byte> Codificar(string code)
        {
            var valores = new List<byte> {StartC};
            bool modoC = true;
            int i = 0;

            while (i < code.Length)
            {
                int n = ContarDigitos(code, i);

                if (modoC)
                {
                    if (n == 0)
                    {
                        valores.Add(CodeA);
                        modoC = false;
                        continue;
                    }

                    int pares = n / 2;
                    for (int k = 0; k < pares; k++, i += 2)
                        valores.Add((byte) ((code[i] - '0') * 10 + (code[i + 1] - '0')));

                    // Dígito ímpar restante vai em A (CODE A antes do último dígito).
                    if (n % 2 != 0)
                    {
                        valores.Add(CodeA);
                        valores.Add((byte) (code[i] - 32));
                        i++;
                        modoC = false;
                    }
                }
                else
                {
                    if (n >= 4)
                    {
                        // Grupo par: CODE C antes do primeiro dígito; ímpar: primeiro dígito em A e CODE C logo após.
                        if (n % 2 != 0)
                        {
                            valores.Add((byte) (code[i] - 32));
                            i++;
                        }

                        valores.Add(CodeC);
                        modoC = true;
                    }
                    else
                    {
                        valores.Add((byte) (code[i] - 32));
                        i++;
                    }
                }
            }

            return valores;
        }

        /// <summary>
        ///     Checksum Code 128: (start + soma(valor * posição)) mod 103. O primeiro item é o Start.
        /// </summary>
        internal static byte CalcularChecksum(IList<byte> valores)
        {
            int cd = valores[0];

            for (int i = 1; i < valores.Count; i++)
                cd += i * valores[i];

            return (byte) (cd % 103);
        }

        private void DrawBarcode(RectangleF rect, Gfx gfx)
        {
            List<byte> codeBytes = Codificar(Code);

            codeBytes.Add(CalcularChecksum(codeBytes));
            codeBytes.Add(106);

            float n = codeBytes.Count * 11 + 2;
            float w = rect.Width / n;

            float x = 0;

            for (int i = 0; i < codeBytes.Count; i++)
            {
                byte[] pt = Dic[codeBytes[i]];

                for (int i2 = 0; i2 < pt.Length; i2++)
                {
                    if (i2 % 2 == 0) gfx.DrawRectangle(rect.X + x, rect.Y, w * pt[i2], rect.Height);

                    x += w * pt[i2];
                }
            }

            gfx.Fill();
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            float w2 = (Width - Largura) / 2F;
            DrawBarcode(new RectangleF(X + w2, Y + MargemVertical, Largura, Height - 2 * MargemVertical), gfx);
        }

        #endregion
    }
}