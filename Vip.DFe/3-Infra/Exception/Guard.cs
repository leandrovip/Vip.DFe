using System;

namespace Vip.DFe.Exception
{
    /// <summary>
    ///     Helper class for guard statements, which allow prettier
    ///     code for guard clauses
    /// </summary>
    public static class Guard
    {
        /// <summary>
        ///     Will throw a <see cref="InvalidOperationException" /> if the assertion
        ///     is true, with the specificied message.
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <example>
        ///     Sample usage:
        ///     <code><![CDATA[
        /// Guard.Against(string.IsNullOrEmpty(name), "Name must have a value");
        /// ]]></code>
        /// </example>
        public static void Against(bool assertion, string message = "")
        {
            if (assertion == false) return;
            throw new InvalidOperationException(message);
        }

        /// <summary>
        ///     Will throw a <see cref="InvalidOperationException" /> if the assertion
        ///     is true, with the specificied message.
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <param name="args">Parametros da mensagem</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <example>
        ///     Sample usage:
        ///     <code><![CDATA[
        /// Guard.Against(string.IsNullOrEmpty(name), "Name must have a value");
        /// ]]></code>
        /// </example>
        public static void Against(bool assertion, string message, params object[] args)
        {
            if (assertion == false) return;
            throw new InvalidOperationException(string.Format(message, args));
        }

        /// <summary>
        ///     Will throw exception of type <typeparamref name="TException" />
        ///     with the specified message if the assertion is true
        /// </summary>
        /// <typeparam name="TException">The type of the t exception.</typeparam>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <param name="beforeThowAction">The action.</param>
        /// <example>
        ///     Sample usage:
        ///     <code><![CDATA[
        /// Guard.Against<ArgumentException>(string.IsNullOrEmpty(name), "Name must have a value", (ex) => Logger.Erro("Name must have a value"));
        /// ]]></code>
        /// </example>
        public static void Against<TException>(bool assertion, string message = "", Action<TException> beforeThowAction = null) where TException : System.Exception
        {
            if (assertion == false) return;
            var exception = (TException) Activator.CreateInstance(typeof(TException), message);
            beforeThowAction?.Invoke(exception);
            throw exception;
        }

        /// <summary>
        ///     Will throw exception of type <typeparamref name="TException" />
        ///     with the specified message if the assertion is true
        /// </summary>
        /// <typeparam name="TException">The type of the t exception.</typeparam>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        /// <example>
        ///     Sample usage:
        ///     <code><![CDATA[
        /// Guard.Against<ArgumentException>(string.IsNullOrEmpty(name), "{0} must have a value", Object);
        /// ]]></code>
        /// </example>
        public static void Against<TException>(bool assertion, string message, params object[] args) where TException : System.Exception
        {
            if (assertion == false) return;
            throw (TException) Activator.CreateInstance(typeof(TException), string.Format(message, args));
        }
    }
}