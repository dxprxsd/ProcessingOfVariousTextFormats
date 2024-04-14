using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

        try
        {
            using (StreamWriter writer = FileManager.GetStreamWriter(filePath))
            {
                writer.Write(json);
            }
        }
        catch (FileManagerException)
        {
            throw;
        }
        catch (Exception)
        {
            throw new FileHandlerException("An unexpected error occurred inside JSON Library while writing to the file.");
        }
    }

    public List<T> ReadFromFile<T>(string filePath) where T : new()
    {
        try
        {
            using (StreamReader reader = FileManager.GetStreamReader(filePath))
            {
                var fileContent = reader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<List<T>>(fileContent);

                ValidateList(result);

                return result;
            }
        }
        catch (FileManagerException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new FileHandlerException("File contents contain invalid JSON scheme.");
        }
    }

    private void ValidateList<T>(List<T> list) where T : new()
    {
        foreach (T item in list)
        {
            ValidateObject(item);
        }
    }

    private void ValidateObject<T>(T obj)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        foreach (PropertyInfo property in properties)
        {
            object value = property.GetValue(obj);
            if (value == null || (property.PropertyType.IsValueType && value.Equals(Activator.CreateInstance(property.PropertyType))))
            {
                throw new FileHandlerException($"Property {property.Name} cannot be null or default.");
            }
        }
    }

    private void ValidateElement<T>(T element) where T : new()
    {
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            object value = property.GetValue(element);
            if (value == null && !IsNullableType(property.PropertyType))
            {
                throw new Exception();
            }
        }

    }

    private bool IsNullableType(Type type)
    {
        if (!type.IsValueType) return true; // ref-type
        if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
        return false; // value-type
    }

}
