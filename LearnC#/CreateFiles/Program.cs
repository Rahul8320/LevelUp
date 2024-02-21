// Creating and Deleting Files.

const string filename = "TestFile.txt";

// Create new file - this will override any existing file of same name.
// Use the "using" construct to automatically close the file stream.
// using (StreamWriter sw = File.CreateText(filename))
// {
//     sw.WriteLine("This file is automatically created by stream write using construct.");
// }

// Check if the file is exist
// System.Console.WriteLine(File.Exists(filename));
// if (File.Exists(filename))
// {
//     File.Delete(filename);
//     System.Console.WriteLine($"File Deleted: {filename}");
// }
// else
// {
//     using StreamWriter sw = File.CreateText(filename);
//     sw.WriteLine("This file is automatically created by stream write using construct.");
//     System.Console.WriteLine($"File created: {filename}");
// }

// WriteAllText overwrites a file with the given content.
Console.WriteLine($"Exist filename: {filename}: {File.Exists(filename)}");

if (!File.Exists(filename))
{
    File.WriteAllText(filename, "This content is overwrite by write all text method.");
}

// Append text into file
File.AppendAllText(filename, "This text is appended by append all text method");

using (StreamWriter sw = File.AppendText(filename))
{
    sw.WriteLine("Append one more line.");
    sw.WriteLine("Another one by stream writer.");
    sw.WriteLine("I promise this was the last one.");
}

// ReadAllText reads the file content
string content = File.ReadAllText(filename);
Console.WriteLine($"File Content: {content}");
