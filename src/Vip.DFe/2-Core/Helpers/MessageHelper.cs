using System.IO;
using System.ServiceModel.Channels;
using System.Xml;
using Vip.DFe.Writer;

namespace Vip.DFe.Helpers
{
    public static class MessageHelper
    {
        /// <summary>
        ///     Converte a message em uma Xml string.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ToXml(ref Message message)
        {
            string messageXml;
            using (var sw = new VipStringWriter())
            using (var writer = XmlWriter.Create(sw))
            {
                message.WriteMessage(writer);
                writer.Flush();

                messageXml = sw.GetStringBuilder().ToString();
            }

            var reader = XmlReader.Create(new StringReader(messageXml));
            var copy = Message.CreateMessage(reader, int.MaxValue, message.Version);

            copy.Headers.Clear();
            copy.Headers.CopyHeadersFrom(message);

            foreach (var property in message.Properties) copy.Properties[property.Key] = property.Value;

            message.Close();
            message = copy;

            return messageXml;
        }

        public static void SaveXml(ref Message message, string file)
        {
            using (var fs = new FileStream(file, FileMode.CreateNew))
                SaveXml(ref message, fs);
        }

        public static void SaveXml(ref Message message, Stream stream)
        {
            var buffer = message.CreateBufferedCopy(int.MaxValue);
            message = buffer.CreateMessage();

            using (var copy = buffer.CreateMessage())
            {
                var writer = XmlWriter.Create(stream);
                copy.WriteMessage(writer);
                writer.Flush();
            }
        }
    }
}