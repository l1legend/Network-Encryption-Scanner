# Network-Server-Scanner
A prototype software that scans a server, locates a specific file (flag.txt) and outputs the contents of the file on the terminal or command line.
The only caveat is that it is design to be used on a local device, therefore only the IP address 127.0.0.1 can be used.

# Instruction

# Setting up a local server.
1. On your desktop create file called "testserver".
2. Inside the folder create a file called "flag" in notepad or any text editor. (Be sure to save it as a text file so the final file name is flag.txt)
3. Enter any characters inside flag.txt and save.
4. Open a command line or terminal, navigate inside testserver.
5. dir or ls command should display "flag.txt"
6. Enter the command: python -m http.server 80 (This will create a test server on port 80)
7. Keep this window open. (It simulates a "remote" server" even though its actually a local server)
![server](https://github.com/l1legend/Network-Server-Scanner/assets/28288764/678224a2-5d15-4cac-b672-c767277ab272)


# Running the Scanner (Windows)
The current build is using a windows.dll. The following are instructions on how to run it.
1. Asuming you are running this on windows, open the "Network-Server-Scanner" folder, navigate inside "Project" folder,
then "bin", "release", and the NetworkScanner executable is located here.
2. To run this program you need to open a command line or terminal, navigate to where the executable is 
and enter the command: NetworkScanner.
3. Enter the IP address. If the server and the file have been configured correctly, it should make a successful connect
and output the contents of the file.

![scanner](https://github.com/l1legend/Network-Server-Scanner/assets/28288764/65892a27-efbe-4fc8-a219-51a4d883e1d1)


