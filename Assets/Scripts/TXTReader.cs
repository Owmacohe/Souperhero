using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TXTReader : MonoBehaviour
{
    // Method to read the entire contents of a file as a string array
    public string[] readFromWholeFile(string fileName)
    {
        StreamReader output = new StreamReader(fileName); // Starting the reading stream

        using (output)
        {
            // Initializing the list of lines
            List<string> lines = new List<string>();
            string line = output.ReadLine();

            // Adding each line to the list
            while (line != null)
            {
                lines.Add(line);
                line = output.ReadLine();
            }

            output.Close(); // Stopping the reading stream
            return lines.ToArray();
        }
    }

    // Method to read the contents of a line of a file
    public string readFromIndex(string fileName, int index)
    {
        string[] lines = readFromWholeFile(fileName);

        try
        {
            return lines[index];
        }
        catch
        {
            print("Index " + index + " is out of bounds! (max " + lines.Length + ")");
            return null;
        }
    }

    // Method to write the entire contents of a file from a string array
    public void writeToWholeFile(string fileName, string[] lines)
    {
        StreamWriter input = new StreamWriter(fileName); // Starting the writing stream

        using (input)
        {
            // Writing each line
            foreach (string i in lines)
            {
                input.WriteLine(i);
            }

            input.Close(); // Stopping the writing stream
        }
    }

    // Method to write the contents of a line of a file
    public void writeToIndex(string fileName, int index, string line)
    {
        string[] lines = readFromWholeFile(fileName);

        try
        {
            lines[index] = line;

            writeToWholeFile(fileName, lines);
        }
        catch
        {
            print("Index " + index + " is out of bounds! (max " + lines.Length + ")");
        }
    }

    // Method to write the contents of the new last line of a file
    public void writeToEndOfFile(string fileName, string line)
    {
        List<string> lines = new List<string>(readFromWholeFile(fileName));
        lines.Add(line);

        writeToWholeFile(fileName, lines.ToArray());
    }
}