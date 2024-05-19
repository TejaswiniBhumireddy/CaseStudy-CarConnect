using System;
using System.IO;

public static class DBPropertyUtil
{
    public static string GetConnectionString(string propertyFileName)
    {
        try
        {
            string[] lines = File.ReadAllLines(propertyFileName);

            foreach (string line in lines)
            {
                if (line.StartsWith("connectionString="))
                {
                    return line.Substring("connectionString=".Length);
                }
            }

            throw new Exception("Connection string not found in the property file.");
        }
        catch (FileNotFoundException)
        {
            throw new Exception("Property file not found.");
        }
        catch (IOException ex)
        {
            throw new Exception("Error reading property file: " + ex.Message);
        }
    }
}
