using System;
using System.IO;
using System.Collections.Generic;

public static class FileManager {
    private static readonly List<String> _allowedExtensions = new List<string>
    {
        ".yaml", ".yml", ".xml", ".json", ".csv"
    };

    public static StreamWriter GetStreamWriter(string filePath)
    {
        try
        {
            string extension = Path.GetExtension(filePath);
            if (!_allowedExtensions.Contains(extension))
            {
                throw new FileManagerException("Unsupported file extension.", filePath, FileManagerExceptionType.WrongExtension);
            }

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
            string extension = Path.GetExtension(filePath);
            if (!_allowedExtensions.Contains(extension))
            {
                throw new FileManagerException("Unsupported file extension.", filePath, FileManagerExceptionType.WrongExtension);
            }

            var streamReader = File.OpenText(filePath);
            //if (File.GetAccessControl(filePath) != null) { }   
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