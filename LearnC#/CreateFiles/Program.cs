// Creating and Deleting Files.

const string filename = "TestFile.txt";

// Create new file - this will override any existing file of same name.
// Use the "using" construct to automatically close the file stream.
using (StreamWriter sw = File.CreateText(filename))
{
    sw.WriteLine("This file is automatically created by stream write using construct.");
}