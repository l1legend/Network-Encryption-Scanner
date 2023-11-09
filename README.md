# Network Security Scanner

A prototype software that scans an IP address, locates a specific file (for example: `password.txt`), and displays the contents of the file in the terminal or command line.
The user is then given the option to encryption the text file with a password. The file will be encrypted as a zipfile. 

> **Note**: This software is designed to be used on a local device; hence, only the IP address `127.0.0.1` can be utilized.

## Table of Contents
- [Instruction](#instruction)
  - [Setting up a Local Server](#setting-up-a-local-server)
  - [Running the Scanner (Windows)](#running-the-scanner-windows)

## Instruction
Follow the steps below:

### Setting up a Local Test Server
1. Create a file on your desktop named "TestServer". (You can place this file in any directory, and the scanner should still locate the file.)
2. Within this folder, open a text editor (like Notepad) and save it as `password.txt`.
3. Add any text to `password.txt` and save.
4. Open a terminal or command prompt and navigate to the `testserver` directory.
5. Use the `dir` (or `ls` on Linux/Mac) command. You should see the `password.text` file listed.
6. Start a local server on port 80 with the command: `python -m http.server 80` (optionally you can run this command in any directory and the program should still be able to find the file)
7. Leave this terminal window open. This step simulates a "remote" server (though it's actually a local server).
   
### Running the Scanner (Windows)
1. Navigate to the directory Network-Server-Scanner, followed by the subsequent path: Project > bin > debug. Within this location, you'll find the executable NetworkScanner.exe.
2. Initiate the program by double-clicking on NetworkScanner.exe.
3. As an alternative, launch a terminal or command prompt. Navigate to the directory where the program resides and execute the command: NetworkScanner.
4. Provide the requisite IP address (127.0.0.1). A successful connection will be established if the server and file configurations are accurate.
5. Enter the filename. Assuming the file is situated in the directory where the test server was configured, the file contents will be rendered.
6. Should you wish, there's an option to encrypt the file. Make a selection between 'yes' or 'no'.
7. If 'yes' is chosen, input a password comprising a minimum of 8 characters.
8. Subsequently, the file will be compressed into a zip format.


