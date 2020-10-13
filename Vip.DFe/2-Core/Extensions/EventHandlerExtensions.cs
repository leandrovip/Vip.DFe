using System;
using System.ComponentModel;

namespace Vip.DFe.Extensions
{
    /// <summary>
    ///     Classe EventHandlerExtension.
    /// </summary>
    internal static class EventHandlerExtensions
    {
        /// <summary>
        ///     Chama o evento.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public static void Raise(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler == null)
                return;

            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
                synchronizeInvoke.Invoke(eventHandler, new[] {sender, e});
            else
                eventHandler.DynamicInvoke(sender, e);
        }

        /// <summary>
        ///     Chama o evento com os argumentos passado.
        ///     Passando null para o sender.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Raise(this EventHandler eventHandler, EventArgs e)
        {
            if (eventHandler == null)
                return;

            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
                synchronizeInvoke.Invoke(eventHandler, new[] {null, e});
            else
                eventHandler.DynamicInvoke(null, e);
        }

        /// <summary>
        ///     Chama o evento.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public static void Raise<T>(this EventHandler<T> eventHandler, object sender, T e) where T : EventArgs
        {
            if (eventHandler == null)
                return;

            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
                synchronizeInvoke.Invoke(eventHandler, new[] {sender, e});
            else
                eventHandler.DynamicInvoke(sender, e);
        }

        /// <summary>
        ///     Chama o evento com os argumentos passado.
        ///     Passando null para o sender.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="e">Argumentos do evento.</param>
        public static void Raise<T>(this EventHandler<T> eventHandler, T e) where T : EventArgs
        {
            if (eventHandler == null)
                return;

            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
                synchronizeInvoke.Invoke(eventHandler, new[] {null, e});
            else
                eventHandler.DynamicInvoke(null, e);
        }

        /// <summary>
        ///     Chama o evento.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        public static void Raise(this EventHandler<EventArgs> eventHandler, object sender)
        {
            if (eventHandler == null)
                return;

            var e = EventArgs.Empty;
            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
                synchronizeInvoke.Invoke(eventHandler, new[] {sender, e});
            else
                eventHandler.DynamicInvoke(sender, e);
        }

        /// <summary>
        ///     Chama o evento.
        ///     Passando null para o sender e EventArgs.Empty
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        public static void Raise(this EventHandler<EventArgs> eventHandler)
        {
            if (eventHandler == null)
                return;

            var e = EventArgs.Empty;
            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
                synchronizeInvoke.Invoke(eventHandler, new[] {null, e});
            else
                eventHandler.DynamicInvoke(null, e);
        }

        /// <summary>
        ///     Chama o evento.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        public static void Raise(this EventHandler eventHandler, object sender)
        {
            if (eventHandler == null)
                return;

            var e = EventArgs.Empty;
            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
                synchronizeInvoke.Invoke(eventHandler, new[] {sender, e});
            else
                eventHandler.DynamicInvoke(sender, e);
        }

        /// <summary>
        ///     Chama o evento.
        ///     Passando null para o sender e EventArgs.Empty
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        public static void Raise(this EventHandler eventHandler)
        {
            if (eventHandler == null)
                return;

            var e = EventArgs.Empty;
            if (eventHandler.Target is ISynchronizeInvoke synchronizeInvoke && synchronizeInvoke.InvokeRequired)
                synchronizeInvoke.Invoke(eventHandler, new[] {null, e});
            else
                eventHandler.DynamicInvoke(null, e);
        }
    }
}