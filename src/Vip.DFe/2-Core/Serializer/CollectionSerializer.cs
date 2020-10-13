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
    internal static class CollectionSerializer
    {
        #region Serialize

        /// <summary>
        ///     Serializes the specified value.
        /// </summary>
        /// <param name="prop">The property.</param>
        /// <param name="parentObject">The parent object.</param>
        /// <param name="options">The options.</param>
        /// <returns>XElement.</returns>
        public static XObject[] Serialize(PropertyInfo prop, object parentObject, SerializerOptions options)
        {
            var tag = prop.GetAttribute<DFeCollectionAttribute>();
            var list = (ICollection) prop.GetValue(parentObject, null) ?? new ArrayList();

            if (list.Count < tag.MinSize || list.Count > tag.MaxSize && tag.MaxSize > 0)
            {
                var msg = list.Count > tag.MaxSize ? DFeSerializer.ErrMsgMaiorMaximo : DFeSerializer.ErrMsgMenorMinimo;
                options.AddAlerta(tag.Id, tag.Name, tag.Descricao, msg);
            }

            if (list.Count == 0 && tag.MinSize == 0 && tag.Ocorrencia == Ocorrencia.NaoObrigatoria) return null;

            var itemType = GetItemType(prop.PropertyType) ?? GetItemType(list.GetType());
            var objectType = ObjectType.From(itemType);

            if (objectType == ObjectType.PrimitiveType)
                return SerializePrimitive(prop, parentObject, list, tag, options);

            return !prop.HasAttribute<DFeItemAttribute>()
                ? SerializeObjects(list, tag, options)
                : SerializeChild(list, tag, prop.GetAttributes<DFeItemAttribute>(), options);
        }

        public static XObject[] SerializeChild(ICollection values, DFeCollectionAttribute tag, DFeItemAttribute[] itemTags, SerializerOptions options)
        {
            var arrayElement = new XElement(tag.Name);

            foreach (var value in values)
            {
                var itemTag = itemTags.SingleOrDefault(x => x.Tipo == value.GetType());
                Guard.Against<VipException>(itemTag == null, $"Item {value.GetType().Name} não presente na lista de itens.");

                XElement childElement;
                if (itemTag != null && itemTag.IsValue)
                {
                    var properties = value.GetType().GetProperties()
                        .Where(x => !x.ShouldIgnoreProperty() && x.ShouldSerializeProperty(value))
                        .OrderBy(x => x.GetAttribute<DFeBaseAttribute>()?.Ordem ?? 0).ToArray();

                    Guard.Against<VipException>(!properties.All(x => x.HasAttribute<DFeItemValueAttribute>() || x.HasAttribute<DFeAttributeAttribute>()),
                        $"Item {value.GetType().Name} é do tipo [ItemValue] e so pode ter atributo do tipo [DFeAttributeAttribute] ou [DFeItemValueAttribute].");

                    Guard.Against<VipException>(properties.Count(x => x.HasAttribute<DFeItemValueAttribute>()) != 1,
                        $"Item {value.GetType().Name} é do tipo [ItemValue] e não tem presente o atributo [DFeItemValueAttribute] ou possui mais de um atributo.");

                    var valueProp = properties.SingleOrDefault(x => x.HasAttribute<DFeItemValueAttribute>());
                    var valueAtt = valueProp.GetAttribute<DFeItemValueAttribute>();

                    XNamespace aw = itemTag.Namespace ?? string.Empty;
                    childElement = new XElement(aw + itemTag.Name);

                    var childValue = valueProp.GetValueOrIndex(value);
                    var estaVazio = childValue == null || childValue.ToString().IsNullOrEmpty();
                    childElement.Value = PrimitiveSerializer.ProcessValue(ref estaVazio, valueAtt.Tipo, valueProp,
                        valueAtt.Ocorrencia, valueAtt.Min, valueProp, value);

                    foreach (var property in properties.Where(x => x.HasAttribute<DFeAttributeAttribute>()))
                    {
                        var attTag = property.GetAttribute<DFeAttributeAttribute>();
                        var att = (XAttribute) PrimitiveSerializer.Serialize(attTag, value, property, options);
                        childElement.AddAttribute(att);
                    }
                }
                else
                {
                    childElement = ObjectSerializer.Serialize(value, value.GetType(), itemTag?.Name, itemTag?.Namespace, options);
                }

                arrayElement.AddChild(childElement);
            }

            return new XObject[] {arrayElement};
        }

        public static XObject[] SerializeObjects(ICollection values, DFeCollectionAttribute tag, SerializerOptions options)
        {
            return (from object value in values select ObjectSerializer.Serialize(value, value.GetType(), tag.Name, tag.Namespace, options)).Cast<XObject>()
                .ToArray();
        }

        public static XObject[] SerializeObjects(ICollection values, DFeItemAttribute tag, SerializerOptions options)
        {
            return (from object value in values select ObjectSerializer.Serialize(value, value.GetType(), tag.Name, tag.Namespace, options)).Cast<XObject>()
                .ToArray();
        }

        public static XObject[] SerializePrimitive(PropertyInfo prop, object parentObject, ICollection values, DFeCollectionAttribute tag,
            SerializerOptions options)
        {
            var retElements = new List<XObject>();
            for (var i = 0; i < values.Count; i++)
            {
                var ret = PrimitiveSerializer.Serialize(tag, parentObject, prop, options, i);
                retElements.Add(ret);
            }

            return retElements.ToArray();
        }

        #endregion Serialize

        #region Deserialize

        /// <summary>
        ///     Deserializes the specified type.
        /// </summary>
        /// <param name="type">The type of the list to deserialize.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="prop">The property.</param>
        /// <param name="parentItem"></param>
        /// <param name="options">Indicates how the output is deserialized.</param>
        /// <returns>The deserialized list from the XElement.</returns>
        /// <exception cref="System.InvalidOperationException">
        ///     Could not deserialize this non generic dictionary without more type
        ///     information.
        /// </exception>
        /// Deserializes the XElement to the list (e.g. List
        /// <T />
        /// , Array of a specified type using options.
        public static object Deserialize(Type type, XElement[] parent, PropertyInfo prop, object parentItem, SerializerOptions options)
        {
            var listItemType = GetListType(type);
            var objectType = ObjectType.From(GetItemType(prop.PropertyType));

            var list = (IList) Activator.CreateInstance(type);
            var elementAtt = prop.GetAttribute<DFeCollectionAttribute>();

            if (prop.HasAttribute<DFeItemAttribute>())
            {
                var itemTags = prop.GetAttributes<DFeItemAttribute>();
                var elements = parent.All(x => x.Name.LocalName == elementAtt.Name) && parent.Length > 1 ? parent : parent.Elements();

                foreach (var element in elements)
                {
                    var itemTag = itemTags.SingleOrDefault(x => x.Name == element.Name.LocalName);
                    Guard.Against<VipException>(itemTag == null, $"Nenhum atributo [{nameof(DFeItemAttribute)}] encontrado " +
                                                                 $"para o elemento: {element.Name.LocalName}");

                    object obj;
                    if (itemTag != null && itemTag.IsValue)
                    {
                        obj = itemTag.Tipo.HasCreate() ? itemTag.Tipo.GetCreate().Invoke() : Activator.CreateInstance(itemTag.Tipo);

                        var properties = itemTag.Tipo.GetProperties()
                            .Where(x => !x.ShouldIgnoreProperty() && x.ShouldSerializeProperty(obj))
                            .OrderBy(x => x.GetAttribute<DFeBaseAttribute>()?.Ordem ?? 0).ToArray();

                        Guard.Against<VipException>(!properties.All(x => x.HasAttribute<DFeItemValueAttribute>() || x.HasAttribute<DFeAttributeAttribute>()),
                            $"Item {itemTag.Tipo.Name} é do tipo [ItemValue] e so pode ter atributo do tipo [DFeAttributeAttribute] ou [DFeItemValueAttribute].");

                        Guard.Against<VipException>(properties.Count(x => x.HasAttribute<DFeItemValueAttribute>()) != 1,
                            $"Item {itemTag.Tipo.Name} é do tipo [ItemValue] e não tem presente o atributo [DFeItemValueAttribute] ou possui mais de um atributo.");

                        var valueProp = properties.SingleOrDefault(x => x.HasAttribute<DFeItemValueAttribute>());
                        var valueAtt = valueProp.GetAttribute<DFeItemValueAttribute>();

                        var value = PrimitiveSerializer.GetValue(valueAtt.Tipo, element.Value, obj, prop);
                        valueProp?.SetValue(obj, value);

                        foreach (var property in properties.Where(x => x.HasAttribute<DFeAttributeAttribute>()))
                        {
                            var attTag = property.GetAttribute<DFeAttributeAttribute>();
                            value = PrimitiveSerializer.Deserialize(attTag, element, obj, property, options);
                            property.SetValue(obj, value);
                        }
                    }
                    else
                    {
                        obj = ObjectSerializer.Deserialize(itemTag?.Tipo, element, options);
                    }

                    list.Add(obj);
                }
            }
            else
            {
                if (objectType == ObjectType.PrimitiveType)
                {
                    foreach (var element in parent)
                    {
                        var obj = PrimitiveSerializer.Deserialize(elementAtt, element, parentItem, prop, options);
                        list.Add(obj);
                    }
                }
                else
                {
                    if (ObjectType.From(prop.PropertyType).IsIn(ObjectType.ArrayType, ObjectType.EnumerableType))
                        listItemType = GetItemType(prop.PropertyType);

                    foreach (var element in parent)
                    {
                        var obj = ObjectSerializer.Deserialize(listItemType, element, options);
                        list.Add(obj);
                    }
                }
            }

            return list;
        }

        public static object Deserialize(Type type, XElement[] elements, SerializerOptions options)
        {
            var listItemType = GetListType(type);
            var list = (IList) Activator.CreateInstance(type);

            foreach (var element in elements)
            {
                var obj = ObjectSerializer.Deserialize(listItemType, element, options);
                list.Add(obj);
            }

            return list;
        }

        #endregion Deserialize

        #region Methods

        private static Type GetListType(Type type)
        {
            var listItemType = typeof(ArrayList).IsAssignableFrom(type) || type.IsArray ? typeof(ArrayList) :
                type.GetGenericArguments().Any() ? type.GetGenericArguments()[0] : type.BaseType?.GetGenericArguments()[0];

            return listItemType;
        }

        public static Type GetItemType(Type type)
        {
            var listItemType = type.IsArray ? type.GetElementType() :
                type.GetGenericArguments().Any() ? type.GetGenericArguments()[0] : type.BaseType?.GetGenericArguments()[0];

            return listItemType;
        }

        #endregion Methods
    }
}