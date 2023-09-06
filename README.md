# Network Security Scanner

A prototype software that scans a server, locates a specific file (`password.txt`), and displays the contents of the file in the terminal or command line.

> **Note**: This software is designed to be used on a local device; hence, only the IP address `127.0.0.1` can be utilized.

## Table of Contents
- [Instruction](#instruction)
  - [Setting up a Local Server](#setting-up-a-local-server)
  - [Running the Scanner (Windows)](#running-the-scanner-windows)
- [Modifying the Tool for Different Operating Systems](#modifying-the-tool-for-different-operating-systems)

## Instruction
Follow the steps below:

### Setting up a Local Server
1. Create a file on your desktop named "TestServer". (You can place this file in any directory, and the scanner should still locate the file.)
2. Within this folder, open a text editor (like Notepad) and create a file named "flag". Save it as `password.txt`.
3. Add any text to `password.txt` and save.
4. Open a terminal or command prompt and navigate to the `testserver` directory.
5. Use the `dir` (or `ls` on Linux/Mac) command. You should see the `password.text` file listed.
6. Start a local server on port 80 with the command: `python -m http.server 80`
7. Leave this terminal window open. This step simulates a "remote" server (though it's actually a local server).
   
![Local Server Setup](https://github.com/l1legend/Network-Server-Scanner/assets/28288764/18fc752c-c6bd-4d42-a8c1-69cac47af420)

### Running the Scanner (Windows)
The current build utilizes a `windows.dll`. Follow these steps to run the scanner:
1. Navigate to the `Network-Server-Scanner` folder, then `Project` > `bin` > `release`. The `NetworkScanner` executable is located here.
2. To execute this program, open a terminal or command prompt, navigate to the location of the executable, and enter the command: `NetworkScanner`.
3. Input the IP address. If the server and file are set up correctly, you'll establish a successful connection, and the content of the file will be displayed.

![Scanner in Action](https://github.com/l1legend/Network-Server-Scanner/assets/28288764/65892a27-efbe-4fc8-a219-51a4d883e1d1)

## Modifying the Software for Different Operating Systems

If you're on Linux or MacOS, follow these steps to create a version of the software compatible with your OS:

1. Ensure you've created a version of the software that references the correct `.dll` for your OS.
2. Download and install either .NET 6 or .NET 7.
3. If not already installed, download and set up Visual Studio 2022.
4. Open the Solution Explorer. If it's not visible, click `View` > `Solution Explorer`.
5. Right-click the 'Dependencies' node of your project (not the solution) in the Solution Explorer.
6. Click 'Add Project Reference'. This might seem misleading since you're not adding a project but a DLL. Bear with the process.
7. Click the 'Browse' button at the bottom of the dialog that appears.
8. Navigate to the `project` folder and find the file named `Assessment_DLL_Files`.
9. Access either the `net6.0` or `net7.0` folder, depending on your .NET version.
10. Choose the folder aligning with your OS and architecture.
11. Select the `Assessment.DLL` file.
12. Make sure the checkbox next to the `Assessment.dll` file is selected.
13. Click 'OK' to close the dialog. Now, the `Assessment.dll` file should be referenced in your project.
14. In Visual Studio, navigate to the top menu and select `Build` > `Build Network Scanner`.

To run the scanner on Linux or MacOS, use the command: `./NetworkScanner`
