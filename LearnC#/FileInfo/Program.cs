// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to file info app.");

const string filename = "MyTestFile.txt";

// Create file if not exists.
if (!File.Exists(filename))
{
    using (StreamWriter sw = File.CreateText(filename))
    {
        sw.WriteLine("This is an auto generated file. Please ignore it if you came here to do anything else.");
        Console.WriteLine($"File: {filename} created!");
    }
}

// Append text to existing file.
// if (File.Exists(filename))
// {
//     using (StreamWriter sw = File.AppendText(filename))
//     {
//         sw.WriteLine("Ok. I know it. Thank you for that information.");
//         Console.WriteLine($"File: {filename} modified!");
//     }
// }

// Read file text
// System.Console.WriteLine(File.ReadAllText(filename));

// Get file information.
Console.WriteLine($"Created on: {File.GetCreationTime(filename)}");
Console.WriteLine($"Last Modified on: {File.GetLastWriteTime(filename)}");
Console.WriteLine($"Last Access on: {File.GetLastAccessTime(filename)}");

// Set file attributes.
File.SetAttributes(filename, FileAttributes.ReadOnly);

// Get file attributes.
Console.WriteLine($"File attributes: {File.GetAttributes(filename)}");

// File info 
try
{
    FileInfo info = new(filename);
    Console.WriteLine($"File Size: {info.Length}");
    Console.WriteLine($"File Path: {info.DirectoryName}");
    Console.WriteLine($"File Full Name {info.FullName}");
}
catch (Exception ex)
{
    Console.WriteLine($"Exception: {ex}");
}

// Modify file information
DateTime dt = new(2000, 03, 08);
File.SetCreationTime(filename, dt);
Console.WriteLine($"Created on: {File.GetCreationTime(filename)}");
