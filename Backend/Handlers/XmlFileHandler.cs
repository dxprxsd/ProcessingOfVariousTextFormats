using System.IO;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Collections;


public class XmlFileHandler : FileHandler
{
    public void WriteToFile<T>(string filePath, T obj) where T : new()
    {
        XmlSerializer serializer;
        try
        {
            serializer = new XmlSerializer(typeof(T));
        }
        catch (Exception)
        {
            throw new FileHandlerException("Exception occured on XML Library side.");
        }

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            try
            {
                serializer.Serialize(writer, obj);
            }
            catch (Exception)
            {
                throw new FileHandlerException("An unexpected error occurred inside XML Library while writing to the file.");
            }
        }
    }

    public List<T> ReadFromFile<T>(string filePath) where T : new()
    {
        XmlSerializer xmlSerializer;
        try
        {
            xmlSerializer = new XmlSerializer(typeof(List<T>));
        }
        catch (Exception)
        {
            throw new FileHandlerException("Exception occured on XML Library side.");
        }

        using (StreamReader reader = FileManager.GetStreamReader(filePath))
        {
            try
            {
                var result = (List<T>)xmlSerializer.Deserialize(reader);
                return result;
            }
            catch (Exception)
            {
                throw new FileHandlerException("File contents contain invalid XML scheme.");
            }
        }
    }
}