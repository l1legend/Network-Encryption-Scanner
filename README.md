# Network-Server-Scanner
A prototype software that scans a server, locates a specific file (flag.txt) and outputs the contents of the file on the terminal or command line.
The only caveat is that it is design to be used on a local device, therefore only the IP address 127.0.0.1 can be used.

# Instruction
1. Setting up a local server.
2. Running the Scanner.

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
3. Enter the IP address. If the server and the file have been configured correctly, it should make a successful connection and output the contents of the file.


![scanner](https://github.com/l1legend/Network-Server-Scanner/assets/28288764/65892a27-efbe-4fc8-a219-51a4d883e1d1)

# Modifying the tool for difference Operating systems.
1. If you are using Linux or MacOS then you need to create a version of the software to use the .dll that matches corresponding OS.
2. Download .NET 6 or .NET 7 and install.
3. If you don't have it installed, go download Visual Studio 2022.
4. Open Solution Explorer. If its not open then click 'View' > 'Solution Explorer'.
5. In the Solution Explorer, right-click on the 'Dependencies' node of your project (not the solution).
6. Click 'Add Project Reference'. This might be a bit misleading because youi're not adding a project but a DLL. Bear with the process.
7. In the opened dialog, click on the 'Browse' button at the bottom.
8. Navigate to the 'project' folder. Inside there a file called Assessment_DLL_Files.
9. Open either net6.0 or net7.0 depend on which .NET version you are using.
10. Select the folder that matches your OS and architecture.
11. Select the Assessment.DLL file.
12. Ensure the checkbox next to 'Assessment.dll' file is checked.
13. Click 'OK' to close dialog. After these steps, 'Assessment.dll' should be referenced in your project.
14. In Visual Studio, on the top menu, select 'Build' > 'Build Network Scanner'

Follow the 'Running the Scanner' to run the program. To run on linux or MacOS enter the command: ./NetworkScanner

