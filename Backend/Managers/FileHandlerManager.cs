using System;
using System.IO;
using System.Collections.Generic;

public class FileHandlerManager : FileHandler
{
    private JsonFileHandler jsonFileHandler = new JsonFileHandler();
    private YamlFileHandler yamlFileHandler = new YamlFileHandler();
    private XmlFileHandler xmlFileHandler = new XmlFileHandler();
    private CsvFileHandler csvFileHandler = new CsvFileHandler();

    private static readonly List<String> _allowedExtensions = FileManager.allowedExtensions;

    public void WriteToFile<T>(string filePath, T obj) where T : new()
    {
        try
        {
            FileManager.CheckAllowedExtension(filePath);
            string extension = FileManager.GetExtension(filePath);

            switch (extension)
            {
                case ".json":
                    jsonFileHandler.WriteToFile(filePath, obj);
                    break;
                case ".yml":
                case ".yaml":
                    yamlFileHandler.WriteToFile(filePath, obj);
                    break;
                case ".xml":
                    xmlFileHandler.WriteToFile(filePath, obj);
                    break;
                case ".csv":
                    csvFileHandler.WriteToFile(filePath, obj);
                    break;
            }

            Console.WriteLine("done");
        }
        catch (FileManagerException ex)
        {
            Console.WriteLine($"Ошибка. {ex}");
        }
        catch (Exception)
        {
            Console.WriteLine($"Неизвестная ошибка.");
        }
    }

    public List<T> ReadFromFile<T>(string filePath) where T : new()
    {
        return new List<T>(); 

    }
}