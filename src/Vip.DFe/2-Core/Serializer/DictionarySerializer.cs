using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;

namespace Vip.DFe.Serializer
{
    internal static class DictionarySerializer
    {
        #region Serialize

        public static XObject[] Serialize(PropertyInfo prop, object parentObject, SerializerOptions options)
        {
            Guard.Against<VipException>(!prop.HasAttribute<DFeDictionaryAttribute>(), $"Atributo necessário não encontrado [{nameof(DFeDictionaryAttribute)}]");
            Guard.Against<VipException>(!prop.HasAttribute<DFeDictionaryKeyAttribute>(), $"Atributo necessário não encontrado [{nameof(DFeDictionaryKeyAttribute)}]");
            Guard.Against<VipException>(!prop.HasAttribute<DFeDictionaryValueAttribute>(), $"Atributo necessário não encontrado [{nameof(DFeDictionaryValueAttribute)}]");

            var tag = prop.GetAttribute<DFeDictionaryAttribute>();
            var keyAtt = prop.GetAttribute<DFeDictionaryKeyAttribute>();
            var valueAtt = prop.GetAttribute<DFeDictionaryValueAttribute>();

            Guard.Against<ArgumentNullException>(!keyAtt.AsAttribute && tag.ItemName.IsNullOrEmpty(), "Se a Key não é um atributo é necessario informar o [ItemName]");

            var dictionary = (IDictionary) prop.GetValue(parentObject, null);
            if (dictionary.Count < tag.MinSize || dictionary.Count > tag.MaxSize && tag.MaxSize > 0)
            {
                var msg = dictionary.Count > tag.MaxSize ? DFeSerializer.ErrMsgMaiorMaximo : DFeSerializer.ErrMsgMenorMinimo;
                options.AddAlerta(tag.Id, tag.Name, tag.Descricao, msg);
            }

            if (dictionary.Count == 0 && tag.MinSize == 0 && tag.Ocorrencia == Ocorrencia.NaoObrigatoria) return null;

            var args = dictionary.GetType().GetGenericArguments();
            Guard.Against<ArgumentException>(args.Length != 2);

            var keyType = ObjectType.From(args[0]);
            var valueType = ObjectType.From(args[1]);

            Guard.Against<VipException>(keyType != ObjectType.PrimitiveType && keyAtt.AsAttribute);

            var list = new List<XElement>();

            var dicENumerator = dictionary.GetEnumerator();

            while (dicENumerator.MoveNext())
            {
                var key = dicENumerator.Entry.Key;
                var value = dicENumerator.Entry.Value;

                var keyElement = keyType == ObjectType.PrimitiveType
                    ? PrimitiveSerializer.Serialize(keyAtt, key, options)
                    : ObjectSerializer.Serialize(key, key.GetType(), keyAtt.Name, keyAtt.Namespace, options);

                var valueElement = valueType == ObjectType.PrimitiveType
                    ? (XElement) PrimitiveSerializer.Serialize(valueAtt, value, options)
                    : ObjectSerializer.Serialize(value, value.GetType(), valueAtt.Name, valueAtt.Namespace, options);

                if (keyAtt.AsAttribute)
                {
                    valueElement.AddAttribute((XAttribute) keyElement);
                    list.Add(valueElement);
                }
                else
                {
                    var itemElement = new XElement(tag.ItemName);
                    itemElement.AddChild((XElement) keyElement);
                    itemElement.AddChild(valueElement);

                    list.Add(itemElement);
                }
            }

            var element = new XElement(tag.Name, tag.Namespace);
            element.AddChild(list.ToArray());

            return new XObject[] {element};
        }

        #endregion Serialize

        #region Deserialize

        public static object Deserialize(PropertyInfo prop, XElement parent, object parentItem, SerializerOptions options)
        {
            Guard.Against<VipException>(!prop.HasAttribute<DFeDictionaryAttribute>(),
                $"Atributo necessário não encontrado [{nameof(DFeDictionaryAttribute)}]");
            Guard.Against<VipException>(!prop.HasAttribute<DFeDictionaryKeyAttribute>(),
                $"Atributo necessário não encontrado [{nameof(DFeDictionaryKeyAttribute)}]");
            Guard.Against<VipException>(!prop.HasAttribute<DFeDictionaryValueAttribute>(),
                $"Atributo necessário não encontrado [{nameof(DFeDictionaryValueAttribute)}]");

            var tag = prop.GetAttribute<DFeDictionaryAttribute>();
            var keyAtt = prop.GetAttribute<DFeDictionaryKeyAttribute>();
            var valueAtt = prop.GetAttribute<DFeDictionaryValueAttribute>();

            var dictionary = (IDictionary) Activator.CreateInstance(prop.PropertyType);
            var args = prop.PropertyType.GetGenericArguments();

            var keyType = ObjectType.From(args[0]);
            var valueType = ObjectType.From(args[1]);

            var elements = parent.ElementsAnyNs(keyAtt.AsAttribute ? valueAtt.Name : tag.ItemName);
            foreach (var element in elements)
            {
                object key;
                object value;
                if (keyAtt.AsAttribute)
                {
                    var keyElement = (XObject) element.Attributes(keyAtt.Name).FirstOrDefault();
                    key = PrimitiveSerializer.Deserialize(keyAtt, keyElement, null, prop, options);
                    value = valueType == ObjectType.PrimitiveType
                        ? PrimitiveSerializer.Deserialize(valueAtt, element, parentItem, prop, options)
                        : ObjectSerializer.Deserialize(args[1], element, options);
                }
                else
                {
                    key = keyType == ObjectType.PrimitiveType
                        ? PrimitiveSerializer.Deserialize(keyAtt, element.ElementAnyNs(keyAtt.Name), parentItem, prop, options)
                        : ObjectSerializer.Deserialize(args[0], element.ElementAnyNs(keyAtt.Name), options);

                    value = valueType == ObjectType.PrimitiveType
                        ? PrimitiveSerializer.Deserialize(valueAtt, element.ElementAnyNs(valueAtt.Name), parentItem, prop, options)
                        : ObjectSerializer.Deserialize(args[1], element.ElementAnyNs(valueAtt.Name), options);
                }

                dictionary.Add(key, value);
            }

            return dictionary;
        }

        #endregion Deserialize
    }
}