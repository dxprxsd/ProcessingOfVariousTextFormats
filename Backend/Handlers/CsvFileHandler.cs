using CsvHelper;
using System;
using System.IO;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Linq;


public class CsvFileHandler : FileHandler
{
    public void WriteToFile<T>(string filePath, T obj) where T : new()
    {
        try
        {
            using (StreamWriter writer = FileManager.GetStreamWriter(filePath))
            {
                using (var csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords((IEnumerable)obj);

                    /*if (obj is IEnumerable)
                    {
                        csvWriter.WriteRecords((IEnumerable)obj);
                    }
                    else
                    {
                        csvWriter.WriteRecord(obj);
                    }*/
                }
            }
        }
        catch (FileManagerException)
        {
            throw;
        }
        catch (Exception)
        {
            throw new FileHandlerException("An unexpected error occurred inside CSV Library while writing to the file.");
        }
        

    }

    public List<T> ReadFromFile<T>(string filePath) where T : new()
    {
        try
        {
            using (StreamReader reader = FileManager.GetStreamReader(filePath))
            {
                using (var csvReader = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
                {
                    List<T> records = csvReader.GetRecords<T>().ToList();
                    return records;
                }
            }
        }
        catch (FileManagerException)
        {
            throw;
        }
        catch (Exception)
        {
            throw new FileHandlerException("File contents contain invalid CSV scheme.");
        }
    }
}

//public class



/*            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
            {
                var elementType = typeof(T).GetGenericArguments()[0];
                if (elementType == typeof(T))
                {
                    var records = csvReader.GetRecords<T>().ToList();
                    return (T)Convert.ChangeType(records, typeof(T));
                }
                else
                {
                    throw new Exception("Exception occured in ReadFromFile. Wrong data type, it has to be List<T> or T.");
                }
            }
            else
            {
                // Directly return the records if T is not a List type
                var result = csvReader.GetRecord<T>();
                return result;
            }*/