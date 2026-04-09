using UnityEngine;

public static class SystemMessage
{
    public static string FileNameNotFound(string fileName) 
        => $"<Error Code 404> FileName \"{fileName}\" Not Founded";

    public static string ObjectNameNotFound(string fileName)
        => $"<Error Code 407> ObjectName \"{fileName}\" Not Founded";
}
