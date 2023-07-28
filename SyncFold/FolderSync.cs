using System.Security.Cryptography;
using Timer = System.Timers.Timer;

namespace SyncFold
{
    public class FolderSync
    {
        private string sourceDirectory;
        private string replicaDirectory;
        private string logFilePath;
        private Timer timer;


        public FolderSync(string sourceDirectory, string replicaDirectory, string logFilePath, double interval)
        {
            this.sourceDirectory = sourceDirectory;
            this.replicaDirectory = replicaDirectory;
            this.logFilePath = logFilePath;

            this.timer = new Timer(interval);
            this.timer.Elapsed += (source, e) => SyncFolders();
            this.timer.AutoReset = true;
        }

        public void StartSync()
        {
            this.timer.Enabled = true;
        }
        private void SyncFolders()
        {
            Log("Synchronization started");

            new DirectoryInfo(replicaDirectory).GetFiles()
     .Where(file => !File.Exists(Path.Combine(sourceDirectory, file.Name)))
     .ToList().ForEach(file =>
     {
         Log($"Deleting {file.Name}");
         file.Delete();
     });

            foreach (var file in new DirectoryInfo(sourceDirectory).GetFiles())
            {
                string destinationFile = Path.Combine(replicaDirectory, file.Name);
                if (!File.Exists(destinationFile))
                {
                    Log($"Copying {file.Name}");
                    file.CopyTo(destinationFile, true);
                }
                else if (CalculateMD5(file.FullName) != CalculateMD5(destinationFile))
                {
                    Log($"Updating {file.Name}");
                    file.CopyTo(destinationFile, true);
                }
            }

            Log("Synchronization completed");
        }
        private string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    byte[] hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private void Log(string message)
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                string logMessage = $"{DateTime.Now}: {message}";
                writer.WriteLine(logMessage);
                Console.WriteLine(logMessage);
            }
        }
    }
}
