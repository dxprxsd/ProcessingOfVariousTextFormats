using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class FileHandlersManagerException : Exception
{
    public FileHandlersManagerException() { }

    public FileHandlersManagerException(string message)
        : base(message) { }

}
