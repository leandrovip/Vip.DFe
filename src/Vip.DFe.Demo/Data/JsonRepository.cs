using System;
using System.IO;
using Newtonsoft.Json;

namespace Vip.DFe.Demo.Data
{
    public class JsonRepository<T> where T : class, new()
    {
        #region Propriedades

        private readonly string _filePath;

        #endregion

        #region Construtores

        public JsonRepository(string fileName)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            _filePath = Path.Combine(directory, fileName);
        }

        #endregion

        #region Métodos Públicos

        public void Save(T data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public T Load()
        {
            if (!File.Exists(_filePath))
                return new T();

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<T>(json) ?? new T();
        }

        #endregion
    }
}