using Flights.Infrastructure.Interfaces;
using MvvmCross.Plugins.File;

namespace Flights.Services
{
    public class FileStore : IFileStore
    {
        private readonly IMvxFileStore _fileStore;
        private readonly IJsonConverter _jsonConverter;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileStore">Parameter to work with files</param>
        /// <param name="jsonConverter">Parameter to work with json converter</param>
        public FileStore(IMvxFileStore fileStore, IJsonConverter jsonConverter)
        {
            _fileStore = fileStore;
            _jsonConverter = jsonConverter;
        }
        /// <summary>
        /// Load data from file
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="fileName">File's name</param>
        /// <returns>Data from file</returns>
        public T Load<T>(string fileName)
        {
            string txt;
            T result = default(T);
            if (_fileStore.TryReadTextFile(fileName, out txt))
            {
                return _jsonConverter.Deserialize<T>(txt);
            }
            return result;
        }
        /// <summary>
        /// Save data to file
        /// </summary>
        /// <param name="fileName">File's name</param>
        /// <param name="obj">Data to store</param>
        public void Save(string fileName, object obj)
        {
            _fileStore.WriteFile(fileName, _jsonConverter.Serialize(obj));
        }
    }
}
