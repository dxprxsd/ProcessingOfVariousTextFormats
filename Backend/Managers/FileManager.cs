using System;
using System.IO;
using System.Collections.Generic;

public static class FileManager {
    public static readonly List<String> allowedExtensions = new List<string>
    {
        ".yaml", ".yml", ".xml", ".json", ".csv"
    };

    public static string GetExtension(string filePath)
    {
        try
        {
            string extension = Path.GetExtension(filePath);
            if (extension == null || string.IsNullOrEmpty(extension))
            {
                throw new Exception();
            }
            return extension;
        }
        catch (Exception)
        {
            throw new FileManagerException("Invalid path: path does not contain extension.", filePath, FileManagerExceptionType.WrongExtension);
        }
    }

    public static void CheckAllowedExtension(string filePath)
    {
        try
        {
            string extension = GetExtension(filePath);
            if (!allowedExtensions.Contains(extension))
            {
                throw new Exception();
            }
        }
        catch (Exception)
        {
            throw new FileManagerException("Unsupported file extension.", filePath, FileManagerExceptionType.WrongExtension);
        }

    }

    public static StreamWriter GetStreamWriter(string filePath)
    {
        try
        {
            CheckAllowedExtension(filePath);

            var streamWriter = File.CreateText(filePath);
            return streamWriter;
        }
        catch (FileManagerException)
        {
            throw;
        }
        catch (UnauthorizedAccessException)
        {
            throw new FileManagerException("Access denied.", filePath, FileManagerExceptionType.AccessDenied);
        }
        catch (PathTooLongException)
        {
            throw new FileManagerException("Invalid path: path exceeds system-defined maximum length.", filePath, FileManagerExceptionType.InvalidPath);
        }
        catch (DirectoryNotFoundException)
        {
            throw new FileManagerException("Invalid path: possibly an unmapped drive.", filePath, FileManagerExceptionType.InvalidPath);
        }
        catch (ArgumentNullException)
        {
            throw new FileManagerException("Invalid path: null string.", filePath, FileManagerExceptionType.InvalidPath);
        }
        catch (ArgumentException)
        {
            throw new FileManagerException("Invalid path: an empty string or string of whitespaces.", filePath, FileManagerExceptionType.InvalidPath);
        }
        catch (NotSupportedException)
        {
            throw new FileManagerException("Invalid path: unsupported format of path for this operating system.", filePath, FileManagerExceptionType.InvalidPath);
        }
        catch (Exception)
        {
            throw new FileManagerException("Invalid path: an empty string or string of whitespaces.", filePath, FileManagerExceptionType.OtherException);
        }
    }

    public static StreamReader GetStreamReader(string filePath)
    {
        try
        {
            CheckAllowedExtension(filePath);

            var streamReader = File.OpenText(filePath);
            return streamReader;
        }
        catch (FileManagerException)
        {
            throw;
        }
        catch (FileNotFoundException)
        {
            throw new FileManagerException("File not found.", filePath, FileManagerExceptionType.FileNotFound);
        }
        catch (UnauthorizedAccessException)
        {
            throw new FileManagerException("Access denied.", filePath, FileManagerExceptionType.AccessDenied);
        }
        catch (PathTooLongException)
        {
            throw new FileManagerException("Invalid path: path exceeds system-defined maximum length.", filePath, FileManagerExceptionType.InvalidPath);
        }
        catch (DirectoryNotFoundException)
        {
            throw new FileManagerException("Invalid path: possibly an unmapped drive.", filePath, FileManagerExceptionType.InvalidPath);
        }
        catch (ArgumentNullException)
        {
            throw new FileManagerException("Invalid path: null string.", filePath, FileManagerExceptionType.InvalidPath);
        }
        catch (ArgumentException)
        {
            throw new FileManagerException("Invalid path: an empty string or string of whitespaces.", filePath, FileManagerExceptionType.InvalidPath);
        }
        catch (NotSupportedException)
        {
            throw new FileManagerException("Invalid path: unsupported format of path for this operating system.", filePath, FileManagerExceptionType.InvalidPath);
        }
        catch (Exception)
        {
            throw new FileManagerException("Invalid path: an empty string or string of whitespaces.", filePath, FileManagerExceptionType.OtherException);
        }
    }

}