using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;

namespace Vip.DFe.Serializer
{
    internal static class PrimitiveSerializer
    {
        #region Serialize

        /// <summary>
        ///     Serializes a fundamental primitive object (e.g. string, int etc.) into a XElement using options.
        /// </summary>
        /// <param name="tag">The name of the primitive to serialize.</param>
        /// <param name="item">The item.</param>
        /// <param name="prop">The property.</param>
        /// <param name="options">Indicates how the output is formatted or serialized.</param>
        /// <param name="idx">a</param>
        /// <returns>The XElement representation of the primitive.</returns>
        public static XObject Serialize(DFeBaseAttribute tag, object item, PropertyInfo prop, SerializerOptions options, int idx = -1)
        {
            try
            {
                var value = prop.GetValueOrIndex(item, idx);
                var estaVazio = value == null || value.ToString().IsNullOrEmpty();
                var conteudoProcessado = ProcessValue(ref estaVazio, tag.Tipo, value, tag.Ocorrencia, tag.Min, prop, item);

                return ProcessContent(tag, conteudoProcessado, estaVazio, options);
            }
            catch (System.Exception ex)
            {
                options.AddAlerta(tag.Id, tag.Name, tag.Descricao, ex.ToString());
                return null;
            }
        }

        public static XObject Serialize(DFeBaseAttribute tag, object value, SerializerOptions options)
        {
            try
            {
                var estaVazio = value == null || value.ToString().IsNullOrEmpty();
                var conteudoProcessado = ProcessValue(ref estaVazio, tag.Tipo, value, tag.Ocorrencia, tag.Min, null, null);
                return ProcessContent(tag, conteudoProcessado, estaVazio, options);
            }
            catch (System.Exception ex)
            {
                options.AddAlerta(tag.Id, tag.Name, tag.Descricao, ex.ToString());
                return null;
            }
        }

        public static string ProcessValue(ref bool estaVazio, TipoCampo tipo, object valor, Ocorrencia ocorrencia, int min, PropertyInfo prop, object item)
        {
            var conteudoProcessado = string.Empty;

            if (estaVazio) return conteudoProcessado;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (tipo)
            {
                case TipoCampo.Str:
                    conteudoProcessado = valor.ToString().TrimVip();
                    break;

                case TipoCampo.Dat:
                case TipoCampo.DatCFe:
                    if (valor is DateTime data)
                        conteudoProcessado = data.ToString(tipo == TipoCampo.DatCFe ? "yyyyMMdd" : "yyyy-MM-dd");
                    else
                        estaVazio = true;
                    break;

                case TipoCampo.Hor:
                case TipoCampo.HorCFe:
                    if (valor is DateTime hora)
                        conteudoProcessado = hora.ToString(tipo == TipoCampo.HorCFe ? "HHmmss" : "HH:mm:ss");
                    else
                        estaVazio = true;
                    break;

                case TipoCampo.DatHor:
                    if (valor is DateTime dthora)
                        conteudoProcessado = dthora.ToString("s");
                    else
                        estaVazio = true;
                    break;

                case TipoCampo.DatHorTz:
                    switch (valor)
                    {
                        case DateTimeOffset dateTime:
                            conteudoProcessado = dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszzz");
                            break;

                        case DateTime dateTime:
                            conteudoProcessado = dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszzz");
                            break;

                        default:
                            estaVazio = true;
                            break;
                    }

                    break;
                case TipoCampo.De2:
                case TipoCampo.De3:
                case TipoCampo.De4:
                case TipoCampo.De6:
                case TipoCampo.De10:
                    if (valor is decimal vDecimal)
                    {
                        if (ocorrencia == Ocorrencia.MaiorQueZero && vDecimal == 0)
                            estaVazio = true;
                        else if (ocorrencia == Ocorrencia.NaoObrigatoria && vDecimal == 0)
                            estaVazio = true;
                        else
                            // ReSharper disable once SwitchStatementMissingSomeCases
                            switch (tipo)
                            {
                                case TipoCampo.De2:
                                    conteudoProcessado = string.Format(CultureInfo.InvariantCulture, "{0:0.00}", vDecimal);
                                    break;

                                case TipoCampo.De3:
                                    conteudoProcessado = string.Format(CultureInfo.InvariantCulture, "{0:0.000}", vDecimal);
                                    break;

                                case TipoCampo.De4:
                                    conteudoProcessado = string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", vDecimal);
                                    break;

                                case TipoCampo.De6:
                                    conteudoProcessado = string.Format(CultureInfo.InvariantCulture, "{0:0.000000}", vDecimal);
                                    break;

                                default:
                                    conteudoProcessado = string.Format(CultureInfo.InvariantCulture, "{0:0.0000000000}", vDecimal);
                                    break;
                            }
                    }
                    else
                    {
                        estaVazio = true;
                    }

                    break;
                case TipoCampo.Int:
                case TipoCampo.Long:
                    switch (valor)
                    {
                        case long vLong when ocorrencia == Ocorrencia.MaiorQueZero && vLong == 0:
                        case int vInt when ocorrencia == Ocorrencia.MaiorQueZero && vInt == 0:
                            estaVazio = true;
                            break;

                        case long _:
                        case int _:
                            conteudoProcessado = valor.ToString();
                            if (conteudoProcessado.Length < min)
                                conteudoProcessado = conteudoProcessado.ZeroFill(min);
                            break;

                        default:
                            estaVazio = true;
                            break;
                    }

                    break;
                case TipoCampo.StrNumberFill:
                    conteudoProcessado = valor.ToString();
                    if (conteudoProcessado.Length < min) conteudoProcessado = conteudoProcessado.ZeroFill(min);
                    break;

                case TipoCampo.StrNumber:
                    conteudoProcessado = valor.ToString().OnlyNumbers();
                    break;
                case TipoCampo.Enum:
                    var member = valor.GetType().GetMember(valor.ToString()).FirstOrDefault();
                    var enumAttribute = member?.GetCustomAttributes(false).OfType<DFeEnumAttribute>().FirstOrDefault();
                    var enumValue = enumAttribute?.Value;
                    conteudoProcessado = enumValue ?? valor.ToString();
                    break;
                case TipoCampo.Custom:
                    var serialize = prop.GetSerializer(item);
                    conteudoProcessado = serialize();
                    break;
                default:
                    conteudoProcessado = valor.ToString();
                    break;
            }

            return conteudoProcessado;
        }

        private static XObject ProcessContent(DFeBaseAttribute tag, string conteudoProcessado, bool estaVazio, SerializerOptions options)
        {
            string alerta;
            if (tag.Ocorrencia == Ocorrencia.Obrigatoria && estaVazio && tag.Min > 0)
                alerta = DFeSerializer.ErrMsgVazio;
            else
                alerta = string.Empty;

            if (conteudoProcessado.IsNullOrEmpty() && conteudoProcessado.Length < tag.Min && alerta.IsNullOrEmpty() && conteudoProcessado.Length > 1)
                alerta = DFeSerializer.ErrMsgMenor;

            if (!string.IsNullOrEmpty(conteudoProcessado.Trim()) && conteudoProcessado.Length > tag.Max)
                alerta = DFeSerializer.ErrMsgMaior;

            if (!string.IsNullOrEmpty(alerta.Trim()) && DFeSerializer.ErrMsgVazio.Equals(alerta) && !estaVazio)
                alerta += $" [{tag.Name}]";

            options.AddAlerta(tag.Id, tag.Name, tag.Descricao, alerta);

            XObject xmlTag = null;
            if (tag.Ocorrencia == Ocorrencia.Obrigatoria && estaVazio)
                xmlTag = tag is DFeElementAttribute ? (XObject) new XElement(tag.Name) : new XAttribute(tag.Name, "");

            if (estaVazio) return xmlTag;

            var elementValue = options.RemoverAcentos ? conteudoProcessado.RemoveAccent() : conteudoProcessado;
            elementValue = options.RemoverEspacos ? elementValue.TrimVip() : elementValue;

            switch (tag)
            {
                case DFeAttributeAttribute _: return new XAttribute(tag.Name, elementValue);
                case DFeDictionaryKeyAttribute keyAtt when keyAtt.AsAttribute: return new XAttribute(keyAtt.Name, elementValue);
                case DFeElementAttribute elementAtt:
                    if (elementValue.IsCData())
                    {
                        elementValue = elementValue.RemoveCData();
                        return new XElement(tag.Name, new XCData(elementValue));
                    }
                    else
                    {
                        return elementAtt.UseCData ? new XElement(tag.Name, new XCData(elementValue)) : new XElement(tag.Name, elementValue);
                    }

                default:
                    return new XElement(tag.Name, elementValue);
            }
        }

        #endregion Serialize

        #region Deserialize

        /// <summary>
        ///     Deserializes the XElement to the fundamental primitive (e.g. string, int etc.) of a specified type using options.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="parentElement">The parent XElement used to deserialize the fundamental primitive.</param>
        /// <param name="item">The item.</param>
        /// <param name="prop">The property.</param>
        /// <param name="options">The options.</param>
        /// <returns>The deserialized fundamental primitive from the XElement.</returns>
        public static object Deserialize(DFeBaseAttribute tag, XObject parentElement, object item, PropertyInfo prop, SerializerOptions options, int idx = -1)
        {
            if (parentElement == null) return null;

            var element = parentElement as XElement;
            var value = element?.Value ?? ((XAttribute) parentElement).Value;
            return GetValue(tag.Tipo, value, item, prop);
        }

        public static object GetValue(TipoCampo tipo, string valor, object item, PropertyInfo prop)
        {
            if (valor.IsNullOrEmpty()) return null;

            object ret;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (tipo)
            {
                case TipoCampo.Int:
                    ret = valor.ToInt32();
                    break;
                case TipoCampo.Long:
                    ret = valor.ToInt64();
                    break;
                case TipoCampo.DatHor:
                    ret = valor.ToData();
                    break;

                case TipoCampo.DatHorTz:
                    ret = valor.ToDataOffset();
                    break;

                case TipoCampo.Dat:
                case TipoCampo.DatCFe:
                    ret = DateTime.ParseExact(valor, tipo == TipoCampo.DatCFe ? "yyyyMMdd" : "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    break;

                case TipoCampo.Hor:
                case TipoCampo.HorCFe:
                    ret = DateTime.ParseExact(valor, tipo == TipoCampo.HorCFe ? "HHmmss" : "HH:mm:ss", CultureInfo.InvariantCulture);
                    break;

                case TipoCampo.De2:
                case TipoCampo.De3:
                case TipoCampo.De4:
                case TipoCampo.De10:
                case TipoCampo.De6:
                    var numberFormat = CultureInfo.InvariantCulture.NumberFormat;
                    ret = decimal.Parse(valor, numberFormat);
                    break;

                case TipoCampo.Enum:
                    var type = prop.PropertyType.IsGenericType ? prop.PropertyType.GetGenericArguments()[0] : prop.PropertyType;
                    object value1 = type.GetMembers().Where(x => x.HasAttribute<DFeEnumAttribute>())
                        .SingleOrDefault(x => x.GetAttribute<DFeEnumAttribute>().Value == valor)?.Name ?? valor;

                    ret = System.Enum.Parse(type, value1.ToString());
                    break;

                case TipoCampo.Custom:
                    var deserialize = prop.GetDeserializer(item);
                    ret = deserialize(valor);
                    break;

                default:
                    ret = valor.RemoveCData();
                    break;
            }

            return ret;
        }

        #endregion Deserialize
    }
}