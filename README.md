# SyncFold

SyncFold is a utility program for synchronizing files from a source directory to a replica directory. This synchronization is one-way, meaning that changes in the source directory will be reflected in the replica directory, but changes in the replica directory will not affect the source directory. The synchronization interval and a log file path can also be specified.

## How it works

The program watches the source directory for changes and reflects these changes in the replica directory based on a specified time interval in seconds. It also logs the operations to a specified log file.

## Usage

The program expects four command-line arguments: 

1. **Source Directory**: The directory that you want to synchronize from.
2. **Replica Directory**: The directory where you want the changes to be mirrored to.
3. **Time Interval (in seconds)**: The time interval at which you want the program to synchronize the directories.
4. **Log File Path**: The path to a file where the program will log its operations.

The arguments should be passed in the order mentioned above.

Here's an example of how to run the program:

```sh
./SyncFold.exe "D:\TaskTest\Source" "D:\TaskTest\Replica" "10" "D:\TaskTest\LogFile.txt"
