using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class FileHandlerException : Exception
{
    public FileHandlerException() { }

    public FileHandlerException(string message)
        : base(message) { }

}
