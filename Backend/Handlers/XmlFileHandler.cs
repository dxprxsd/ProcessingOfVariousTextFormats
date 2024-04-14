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
            throw new FileHandlerException("An unexpected error occurred inside XML Library while writing to the file.");
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

        try
        {
            using (StreamReader reader = FileManager.GetStreamReader(filePath))
            {
                var result = (List<T>)xmlSerializer.Deserialize(reader);
                return result;
            }
        }
        catch (FileManagerException)
        {
            throw;
        }
        catch (Exception)
        {
            throw new FileHandlerException("File contents contain invalid XML scheme.");
        }
        
    }
}