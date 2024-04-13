using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class JsonFileHandler : FileHandler
{
    public void WriteToFile<T>(string filePath, T obj) where T : new()
    {
        string json;
        try
        {
            json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        catch (Exception)
        {
            throw new FileHandlerException("Exception occured on JSON Library side while parsing to string.");
        }

        using (StreamWriter writer = FileManager.GetStreamWriter(filePath))
        {
            try
            {
                writer.Write(json);
            }
            catch (Exception)
            {
                throw new FileHandlerException("An unexpected error occurred inside JSON Library while writing to the file.");
            }
        }

    }

    public List<T> ReadFromFile<T>(string filePath) where T : new()
    {
        using (StreamReader reader = FileManager.GetStreamReader(filePath))
        {
            try
            {
                var fileContent = reader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<List<T>>(fileContent);
                return result;
            }
            catch (Exception)
            {
                throw new FileHandlerException("File contents contain invalid JSON scheme.");
            }

        }
    }
}
