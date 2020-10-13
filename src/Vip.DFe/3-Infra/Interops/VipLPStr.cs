using System;
using System.Runtime.InteropServices;

namespace Vip.DFe.Interops
{
    public class VipLPStr : ICustomMarshaler
    {
        #region Fields

        private static VipLPStr marshaler;

        #endregion Fields

        #region Methods

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return marshaler ??= new VipLPStr();
        }

        /// <inheritdoc />
        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return Marshal.PtrToStringAnsi(pNativeData);
        }

        /// <inheritdoc />
        public void CleanUpNativeData(IntPtr pNativeData) { }

        /// <inheritdoc />
        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            return IntPtr.Zero;
        }

        /// <inheritdoc />
        public void CleanUpManagedData(object ManagedObj) { }

        /// <inheritdoc />
        public int GetNativeDataSize()
        {
            return IntPtr.Size;
        }

        #endregion Methods
    }
}