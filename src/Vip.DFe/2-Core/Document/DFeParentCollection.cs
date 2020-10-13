using System;
using System.Collections.Generic;
using Vip.DFe.Attributes;

namespace Vip.DFe.Document
{
    public class DFeParentCollection<TTipo, TParent> : DFeCollection<TTipo>
        where TParent : class
        where TTipo : DFeParentItem<TTipo, TParent>
    {
        #region Fields

        protected TParent parent;

        #endregion Fields

        #region Propriedades

        [DFeIgnore]
        public TParent Parent
        {
            get => parent;
            set
            {
                parent = value;
                foreach (var item in this)
                {
                    if (item.Parent == value) continue;
                    item.Parent = value;
                }
            }
        }

        #endregion Propriedades

        #region Methods

        /// <summary>
        ///     Adds an object to the end of the <see cref="DFeParentCollection{T, T}" />.
        /// </summary>
        /// <returns>T.</returns>
        public override TTipo AddNew()
        {
            var item = (TTipo) Activator.CreateInstance(typeof(TTipo), true);
            item.Parent = Parent;
            base.Add(item);
            return item;
        }

        /// <summary>Adds an object to the end of the <see cref="DFeParentCollection{T, T}" />.</summary>
        /// <param name="item">
        ///     The object to be added to the end of the <see cref="DFeParentCollection{T, T}" />. The value can be
        ///     null for reference types.
        /// </param>
        public override void Add(TTipo item)
        {
            item.Parent = Parent;
            base.Add(item);
        }

        /// <summary>Inserts an element into the <see cref="DFeParentCollection{T, T}" /> at the specified index.</summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is greater than
        ///     <see cref="DFeParentCollection{T, T}.Count" />.
        /// </exception>
        public override void Insert(int index, TTipo item)
        {
            item.Parent = Parent;
            base.Insert(index, item);
        }

        /// <summary>Inserts the elements of a collection into the <see cref="DFeParentCollection{T, T}" /> at the specified index.</summary>
        /// <param name="index">The zero-based index at which the new elements should be inserted.</param>
        /// <param name="collection">
        ///     The collection whose elements should be inserted into the
        ///     <see cref="DFeParentCollection{T, T}" />. The collection itself cannot be null, but it can contain elements that
        ///     are null, if type <paramref name="T" /> is a reference type.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="collection" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is greater than
        ///     <see cref="DFeParentCollection{T, T}.Count" />.
        /// </exception>
        public override void InsertRange(int index, IEnumerable<TTipo> collection)
        {
            foreach (var item in collection) item.Parent = Parent;

            base.InsertRange(index, collection);
        }

        public override void AddRange(IEnumerable<TTipo> collection)
        {
            foreach (var item in collection) item.Parent = Parent;
            base.AddRange(collection);
        }

        #endregion Methods
    }
}