// Operating Systems Homework 3 - Memory Management
// by Aaron Barrett

namespace OS_Homework3_MemoryManagement
{
    internal class Program
    {
        const int PAGESIZE = 1024;
        const int FRAMESIZE = 1024;
        const int NUMPAGES = 16;
        const int NUMFRAMES = 8;

        static Random random = new Random();
        static List<int> pageFrameTable = new List<int>();
        static List<bool> frameOccupied = new List<bool>(new bool[NUMFRAMES]);

        static void LoadPageIntoMemory(int pageNumber)
        {
            // Get the first unoccupied frame
            int frameNumber = frameOccupied.IndexOf(false);

            if (frameNumber == -1)
            {
                // Handle if no empty frame exists
                Console.WriteLine("Memory is full!");
                return;
            }

            pageFrameTable[pageNumber] = frameNumber;
            frameOccupied[frameNumber] = true;

            Console.WriteLine($"Loaded page {pageNumber} into frame {frameNumber}");
        }

        static void LogicalToPhysicalAddress(int logicalAddress)
        {
            int pageNumber = logicalAddress / PAGESIZE;
            int offset = logicalAddress % PAGESIZE;

            if(pageNumber >= NUMPAGES || pageNumber < 0)
            {
                Console.WriteLine("Invalid logical address");
                return;
            }

            // Handle a page fault and attempt to load page into memory
            if (pageFrameTable[pageNumber] == -1)
            {
                Console.WriteLine($"Page fault at {pageNumber}. Attempting to load...");
                LoadPageIntoMemory(pageNumber);
            }

            int frameNumber = pageFrameTable[pageNumber];
            int physicalAddress = frameNumber * FRAMESIZE + offset;

            Console.WriteLine($"Logical Address: 0x{logicalAddress:X} => Physical Address: 0x{physicalAddress:X}, Page Number: 0x{pageNumber:X}, Offset: 0x{offset:X}");
        }

        static void Main(string[] args)
        {
            // Initialize the page frame table with -1's just like the example
            for (int i = 0; i < NUMPAGES; i++)
            {
                pageFrameTable.Add(-1);
            }

            // Handle automatic or manual input
            Console.Write("Enter 1 for automatic address generation or 2 for manual: ");
            string choice = Console.ReadLine();

            if(choice == "2") // Manual mode
            {
                Console.WriteLine("Enter logical address in decimal (or 'exit' to quit):");
                string input = Console.ReadLine();
                while (input != null && input.ToLower() != "exit")
                {
                    if (int.TryParse(input, out int logicalAddress))
                    {
                        LogicalToPhysicalAddress(logicalAddress);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a decimal number.");
                    }
                    Console.Write("\nAddress: ");
                    input = Console.ReadLine();
                }

            }
            else // Automatic generation
            {
                // Generate some logical addresses and translate them to physical addresses
                for (int i = 0; i < 5; i++)
                {
                    int logicalAddress = random.Next(NUMPAGES * PAGESIZE);
                    LogicalToPhysicalAddress(logicalAddress);
                }
            }
        }
    }
}
