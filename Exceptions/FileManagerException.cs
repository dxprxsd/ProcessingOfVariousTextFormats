using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class FileManagerException : Exception
{
    public string FilePath { get; private set; }
    public FileManagerExceptionType ExceptionType { get; private set; }

    public FileManagerException() { }

    public FileManagerException(string message) 
        : base(message) { }

    public FileManagerException(string message, string filePath, FileManagerExceptionType exceptionType)
        : base(message) 
    {
        this.FilePath = filePath;
        this.ExceptionType = exceptionType;  
    }

}

public enum FileManagerExceptionType
{
    FileNotFound,
    InvalidPath,
    AccessDenied,
    WrongExtension,
    OtherException
}