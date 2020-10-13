using System;
using System.Collections;
using Vip.DFe.Attributes;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;

namespace Vip.DFe.Serializer
{
    internal struct ObjectType
    {
        #region ObjectsTypes

        public static ObjectType ClassType => new ObjectType(0);

        public static ObjectType PrimitiveType => new ObjectType(1);

        public static ObjectType ListType => new ObjectType(2);

        public static ObjectType DictionaryType => new ObjectType(3);

        public static ObjectType InterfaceType => new ObjectType(4);

        public static ObjectType RootType => new ObjectType(5);

        public static ObjectType ArrayType => new ObjectType(6);

        public static ObjectType EnumerableType => new ObjectType(7);

        public static ObjectType AbstractType => new ObjectType(8);

        #endregion ObjectsTypes

        #region Constructors

        private ObjectType(int id)
        {
            Guard.Against<ArgumentException>(!id.IsBetween(0, 8), "Tipo de objeto desconhecido.");
            Id = id;
        }

        #endregion Constructors

        #region Propriedades

        private int Id { get; }

        #endregion Propriedades

        #region Operators

        public static bool operator ==(ObjectType a, ObjectType b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(ObjectType a, ObjectType b)
        {
            return !(a == b);
        }

        #endregion Operators

        #region Methods

        public static ObjectType From(object value)
        {
            return From(value.GetType());
        }

        public static ObjectType From(Type type)
        {
            if (IsPrimitive(type)) return PrimitiveType;
            if (IsInterface(type)) return InterfaceType;
            if (IsAbstract(type)) return AbstractType;
            if (IsList(type)) return ListType;
            if (IsDictionary(type)) return DictionaryType;
            if (IsArray(type)) return ArrayType;
            if (IsEnumerable(type)) return EnumerableType;

            return IsRoot(type) ? RootType : ClassType;
        }

        public override bool Equals(object obj)
        {
            return obj != null && GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        private static bool IsPrimitive(Type type)
        {
            return type == typeof(string)
                   || type == typeof(char)
                   || type == typeof(sbyte)
                   || type == typeof(short)
                   || type == typeof(int)
                   || type == typeof(long)
                   || type == typeof(byte)
                   || type == typeof(ushort)
                   || type == typeof(uint)
                   || type == typeof(ulong)
                   || type == typeof(double)
                   || type == typeof(float)
                   || type == typeof(decimal)
                   || type == typeof(bool)
                   || type == typeof(DateTime)
                   || type == typeof(DateTimeOffset)
                   || type.IsEnum
                   || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && IsPrimitive(type.GetGenericArguments()[0]);
        }

        private static bool IsList(Type type)
        {
            return typeof(IList).IsAssignableFrom(type) && !IsArray(type);
        }

        private static bool IsEnumerable(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type) && !IsArray(type);
        }

        private static bool IsInterface(Type type)
        {
            return type.IsInterface && !IsList(type) && !IsEnumerable(type) && !IsArray(type);
        }

        private static bool IsAbstract(Type type)
        {
            return type.IsAbstract && !IsList(type) && !IsEnumerable(type) && !IsArray(type);
        }

        private static bool IsArray(Type type)
        {
            return type.IsArray;
        }

        private static bool IsRoot(Type type)
        {
            return type.HasAttribute<DFeRootAttribute>();
        }

        public static bool IsDictionary(Type type)
        {
            return typeof(IDictionary).IsAssignableFrom(type);
        }

        #endregion Methods
    }
}