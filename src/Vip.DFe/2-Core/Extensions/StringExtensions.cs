using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Vip.DFe.Exception;

namespace Vip.DFe.Extensions
{
    internal static class StringExtensions
    {
        public static string TrimVip(this string value)
        {
            return value?.Trim() ?? string.Empty;
        }

        /// <summary>
        ///     Verifica se a string é nula ou vazia
        /// </summary>
        /// <param name="value">string para verificar</param>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        ///     Verifica se a string NÃO é nula ou vazia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !value.IsNullOrEmpty();
        }

        /// <summary>
        ///     Verifica se a string é numerica.
        /// </summary>
        /// <param name="strNum">The string number.</param>
        /// <returns>Retorna true/false se a string é numerica</returns>
        /// <exception cref="VipException">Erro ao validar string</exception>
        public static bool IsNumeric(this string strNum)
        {
            try
            {
                var reNum = new Regex(@"^\d+$");
                return reNum.IsMatch(strNum);
            }
            catch (System.Exception ex)
            {
                throw new VipException("Erro ao validar string", ex);
            }
        }

        /// <summary>
        ///     Verifica se a string é um e-mail válido
        /// </summary>
        /// <param name="value">String</param>
        /// <returns><b>true</b> caso for e-mail válido</returns>
        public static bool IsEmail(this string value)
        {
            if (value.IsNullOrEmpty()) return false;
            
            try
            {
                const string regex = @"^(([^<>()[\]\\.,;áàãâäéèêëíìîïóòõôöúùûüç:\s@\""]+"
                                     + @"(\.[^<>()[\]\\.,;áàãâäéèêëíìîïóòõôöúùûüç:\s@\""]+)*)|(\"".+\""))@"
                                     + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|"
                                     + @"(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";

                var rx = new Regex(regex);
                return rx.IsMatch(value);
            }
            catch (System.Exception ex)
            {
                throw new VipException($"Erro ao validar o e-mail. Mensagem: {ex.Message}");
            }
        }

        /// <summary>
        ///     Retorna apenas os numeros da string.
        /// </summary>
        /// <param name="value">String para processar.</param>
        /// <returns>System.String.</returns>
        public static string OnlyNumbers(this string value)
        {
            return value.TrimVip().IsNullOrEmpty()
                ? string.Empty
                : new string(value.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        ///     Get string Between the specified values
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>System.String.</returns>
        public static string GetStrBetween(this string value, int start, int end)
        {
            if (value.IsNullOrEmpty()) return string.Empty;

            var len = value.Length;

            if (start < 0) start += len;

            if (end < 0) end += len;

            if (len == 0 || start > len - 1 || end < start) return string.Empty;

            if (start < 0) start = 0;

            if (end >= len) end = len - 1;

            return value.Substring(start, end - start + 1);
        }

        /// <summary>
        ///     Preenche uma string com zero a direita ate ficar do tamanho especificado.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="length">Tamanho final desejado</param>
        /// <returns>String do tamanho especificado e se menor complementada com zero a direita/esquerda</returns>
        public static string ZeroFill(this string text, int length)
        {
            return text.OnlyNumbers().StringFill(length, '0');
        }

        /// <summary>
        ///     Alinha a string a direita/esquerda e preenche com caractere informado ate ficar do tamanho especificado.
        /// </summary>
        /// <param name="text">O texto</param>
        /// <param name="length">Tamanho final desejado</param>
        /// <param name="with">Caractere para preencher</param>
        /// <param name="esquerda">Direção do preenchimento</param>
        /// <returns>String do tamanho especificado e se menor complementada com o caractere informado a direita/esquerda</returns>
        public static string StringFill(this string text, int length, char with = ' ', bool esquerda = true)
        {
            if (text.IsNullOrEmpty()) text = string.Empty;

            if (text.Length > length)
            {
                text = text.Remove(length);
            }
            else
            {
                length -= text.Length;

                if (esquerda)
                    text = new string(with, length) + text;
                else
                    text += new string(with, length);
            }

            return text;
        }

        /// <summary>
        ///     Remove acentos da string
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string RemoveAccent(this string texto)
        {
            if (texto.IsNullOrEmpty())
                return string.Empty;

            const string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç&º°ª";
            const string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCcE   ";

            for (var i = 0; i < comAcentos.Length; i++)
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());

            return texto;
        }

        /// <summary>
        ///     Reverse content.
        /// </summary>
        /// <param name="value">value to reverse.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="VipException">Erro ao reverter string</exception>
        public static string Reverse(this string value)
        {
            try
            {
                if (value.IsNullOrEmpty() || value.Length == 1) return value;
                return new string(value.ToCharArray().Reverse().ToArray());
            }
            catch
            {
                throw new VipException("Erro ao reverter string");
            }
        }

        /// <summary>
        ///     Converte string para int32.
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        /// <param name="def">Definição</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="VipException">Erro ao converter string</exception>
        public static int ToInt32(this string value, int def = -1)
        {
            return ToInt32(value, def, CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///     Converte string para int32.
        /// </summary>
        /// <param name="format">Format Provider</param>
        /// <returns>System.Int32.</returns>
        public static int ToInt32(this string value, IFormatProvider format)
        {
            return ToInt32(value, -1, format);
        }

        /// <summary>
        ///     Converte string para int32.
        /// </summary>
        /// <param name="format">Format Provider</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="VipException">Erro ao converter string</exception>
        public static int ToInt32(this string value, int def, IFormatProvider format)
        {
            try
            {
                if (!int.TryParse(value, NumberStyles.Any, format, out var converted))
                    converted = def;

                return converted;
            }
            catch (System.Exception ex)
            {
                throw new VipException("Erro ao converter string", ex);
            }
        }

        /// <summary>
        ///     Converte string para int64
        /// </summary>
        /// <param name="def">Definição</param>
        /// <returns>System.Int64.</returns>
        public static long ToInt64(this string value, long def = -1)
        {
            return ToInt64(value, def, CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///     Converte string para int64
        /// </summary>
        /// <param name="format">Format Provider</param>
        /// <returns>System.Int64.</returns>
        public static long ToInt64(this string value, IFormatProvider format)
        {
            return ToInt64(value, -1, format);
        }

        /// <summary>
        ///     To the int64.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <param name="def">The definition.</param>
        /// <param name="format">The format.</param>
        /// <returns>Int64.</returns>
        /// <exception cref="VipException">Erro ao converter string</exception>
        /// <exception cref="VipException">Erro ao converter string</exception>
        public static long ToInt64(this string dados, long def, IFormatProvider format)
        {
            try
            {
                if (!long.TryParse(dados, NumberStyles.Any, format, out var converted))
                    converted = def;

                return converted;
            }
            catch (System.Exception ex)
            {
                throw new VipException("Erro ao converter string", ex);
            }
        }

        /// <summary>
        ///     Converte string para DateTime
        /// </summary>
        /// <param name="value">The dados.</param>
        /// <returns>DateTime.</returns>
        /// <exception cref="VipException">Erro ao converter string</exception>
        public static DateTime ToData(this string value)
        {
            try
            {
                if (!DateTime.TryParse(value, out var converted))
                    converted = default;

                return converted;
            }
            catch (System.Exception ex)
            {
                throw new VipException("Erro ao converter string", ex);
            }
        }

        /// <summary>
        ///     Converte string para DateTimeOffset.
        /// </summary>
        /// <param name="dados">The dados.</param>
        /// <returns>DateTime.</returns>
        /// <exception cref="VipException">Erro ao converter string</exception>
        public static DateTimeOffset ToDataOffset(this string dados)
        {
            try
            {
                if (!DateTimeOffset.TryParse(dados, out var converted))
                    converted = default;

                return converted;
            }
            catch (System.Exception ex)
            {
                throw new VipException("Erro ao converter string", ex);
            }
        }

        /// <summary>
        ///     Verifica se um código é um GTIN (8,12,13,14) válido
        /// </summary>
        /// <param name="value">Código</param>
        /// <returns><b>true</b> caso for válido</returns>
        public static bool IsGtin(this string value)
        {
            var code = value.OnlyNumbers();
            if (code.IsNullOrEmpty()) return false;

            code = code.ZeroFill(14);
            var digit = code[13] - '0';
            code = code.Substring(0, 13);

            var sum = 0;
            var factor = 3;

            foreach (var dig in code)
            {
                sum += (dig - '0') * factor;
                factor = factor == 3 ? 1 : 3;
            }

            var check = (10 - sum % 10) % 10;
            return check == digit;
        }

        public static string RemoverDeclaracaoXml(this string xml)
        {
            if (xml.IsNullOrEmpty()) return xml;

            var posIni = xml.IndexOf("<?", StringComparison.Ordinal);
            if (posIni < 0) return xml;

            var posFinal = xml.IndexOf("?>", StringComparison.Ordinal);
            return posFinal < 0 ? xml : xml.Remove(posIni, posFinal + 2 - posIni);
        }
    }
}