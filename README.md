# OS-Homework3_MemoryManagement

## Program Functionality
This program can either generate random memory addresses or take manual input, and will convert the logical memory address into its physical memory address. It displays the physical memory address, the page number, and the offset. If a page is not loaded into memory, it will attempt to load it. If an address is out of range of the virtual memory, it will throw an error. If a page cannot be loaded because the memory is full, it will also throw an error.

## Instructions for Execution
To run this you'll need .NET 8 installed. This can be easily installed through the Visual Studio Installer if you don't already have it. Open the solution file .sln in Visual Studio, and build/run in the local visual studio debugger, just like a c++ project. All source code is in Program.cs