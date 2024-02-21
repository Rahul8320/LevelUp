// Creating and Deleting Files.

const string filename = "TestFile.txt";

// Create new file - this will override any existing file of same name.
// Use the "using" construct to automatically close the file stream.
// using (StreamWriter sw = File.CreateText(filename))
// {
//     sw.WriteLine("This file is automatically created by stream write using construct.");
// }

// Check if the file is exist
System.Console.WriteLine(File.Exists(filename));
if (File.Exists(filename))
{
    File.Delete(filename);
    System.Console.WriteLine($"File Deleted: {filename}");
}
else
{
    using StreamWriter sw = File.CreateText(filename);
    sw.WriteLine("This file is automatically created by stream write using construct.");
    System.Console.WriteLine($"File created: {filename}");
}