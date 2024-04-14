using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.IO;
using System.Collections.Generic;
using System;

public class YamlFileHandler : FileHandler
{
    public void WriteToFile<T>(string filePath, T obj) where T : new()
    {
        ISerializer serializer;
        try
        {
            serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        }
        catch (Exception)
        {
            throw new FileHandlerException("Exception occured on YAML Library side.");
        }


        try
        {
            using (StreamWriter writer = FileManager.GetStreamWriter(filePath))
            {
                serializer.Serialize(writer, obj);
            }
        }
        catch (FileManagerException)
        {
            throw;
        }
        catch (Exception)
        {
            throw new FileHandlerException("An unexpected error occurred inside YAML Library while writing to the file.");
        }

    }

    public List<T> ReadFromFile<T>(string filePath) where T : new()
    {
        IDeserializer deserializer;
        try
        {
            deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        }
        catch (Exception)
        {
            throw new FileHandlerException("Exception occured on YAML Library side.");
        }

        try
        {
            using (StreamReader reader = FileManager.GetStreamReader(filePath))
            {
                var result = deserializer.Deserialize<List<T>>(reader);
                return result;
            }
        }
        catch (FileManagerException)
        {
            throw;
        }
        catch (Exception)
        {
            throw new FileHandlerException("File contents contain invalid YAML scheme.");
        }

    }


}