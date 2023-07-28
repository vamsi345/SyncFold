using SyncFold;
public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.WriteLine("Usage: FolderSync.exe <source directory> <replica directory> <sync interval in seconds> <log file path>");
            return;
        }

        string sourceDirectory = args[0];
        string replicaDirectory = args[1];
        double interval = double.Parse(args[2]) * 1000;
        string logFilePath = args[3];

        FolderSync sync = new FolderSync(sourceDirectory, replicaDirectory, logFilePath, interval);
        sync.StartSync();

        Console.WriteLine("Press \'q\' to quit the folder synchronization");
        while (Console.Read() != 'q') ;
    }


}