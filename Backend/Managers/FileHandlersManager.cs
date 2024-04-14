using System;
using System.IO;
using System.Collections.Generic;

public class FileHandlersManager : FileHandler
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

            //Console.WriteLine("DEBUG: done writing");
        }
        catch (FileManagerException ex)
        {
            throw new FileHandlersManagerException(ex.Message);
            //Console.WriteLine($"Ошибка. {ex}");
        }
        catch (FileHandlerException ex)
        {
            throw new FileHandlersManagerException(ex.Message);
        }
        /*catch (Exception)
        {
            Console.WriteLine($"Неизвестная ошибка.");
        }*/
    }

    public List<T> ReadFromFile<T>(string filePath) where T : new()
    {
        try
        {
            FileManager.CheckAllowedExtension(filePath);
            string extension = FileManager.GetExtension(filePath);

            List<T> result;

            switch (extension)
            {
                case ".json":
                    result = jsonFileHandler.ReadFromFile<T>(filePath);
                    break;
                case ".yml":
                case ".yaml":
                    result = yamlFileHandler.ReadFromFile<T>(filePath);
                    break;
                case ".xml":
                    result = xmlFileHandler.ReadFromFile<T>(filePath);
                    break;
                case ".csv":
                    result = csvFileHandler.ReadFromFile<T>(filePath);
                    break;
                default:
                    return null; // недостижимый код
            }


            //Console.WriteLine("DEBUG: done reading");
            return result;
        }
        catch (FileManagerException ex)
        {
            throw new FileHandlersManagerException(ex.Message);
            //Console.WriteLine($"Ошибка. {ex}");
        }
        catch (FileHandlerException ex)
        {
            throw new FileHandlersManagerException(ex.Message);
        }
    }
}