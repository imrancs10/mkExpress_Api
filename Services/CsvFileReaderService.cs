using CatalogService.API.Services.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using MKExpress.API.Exceptions;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MKExpress.API.Services
{

    public class CsvFileReaderService : ICsvFileReaderService
    {
        public List<T> ReadCsvFile<T, TMap>(string resourceName) where TMap : ClassMap
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceName) ??
                               throw new NotFoundException(resourceName);
            CsvConfiguration conf = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null
            };
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            CsvReader csvReader = new CsvReader(reader, conf);
            csvReader.Context.RegisterClassMap<TMap>();
            var data = csvReader.GetRecords<T>().ToList();
            return data;
        }
    }
}