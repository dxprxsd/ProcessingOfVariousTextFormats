using System.Collections.Generic;

public interface FileHandler
{
    void WriteToFile<T>(string filePath, T obj) where T : new();
    List<T> ReadFromFile<T>(string filePath) where T : new();
}