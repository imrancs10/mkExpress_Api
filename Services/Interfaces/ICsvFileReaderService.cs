using CsvHelper.Configuration;
using System.Collections.Generic;

namespace CatalogService.API.Services.Interfaces
{
    public interface ICsvFileReaderService
    {
        List<T> ReadCsvFile<T, TMap>(string resourceName) where TMap : ClassMap;
    }
}