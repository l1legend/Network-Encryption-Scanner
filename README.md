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
2. Within this folder, open a text editor (like Notepad) and create a file named "flag". Save it as `password.txt`.
3. Add any text to `password.txt` and save.
4. Open a terminal or command prompt and navigate to the `testserver` directory.
5. Use the `dir` (or `ls` on Linux/Mac) command. You should see the `password.text` file listed.
6. Start a local server on port 80 with the command: `python -m http.server 80` (optionally you can run this command in any directory and the program should still be able to find the file)
7. Leave this terminal window open. This step simulates a "remote" server (though it's actually a local server).
   
### Running the Scanner (Windows)
The current build utilizes a `windows.dll`. Follow these steps to run the scanner:
1. Navigate to the `Network-Server-Scanner` folder, then `Project` > `bin` > `debug`. The `NetworkScanner.exe` executable is located here.
2. To run the program, doubleclick `NetworkScanner.exe`
3. Alternatively you can open a terminal or command prompt, navigate to the location of the program, and enter the command: `NetworkScanner`.
4. Input the IP address. If the server and file are set up correctly, you'll establish a successful connection.
5. Input the name of the file. Assuming the file is located in the same directory where you setup your test server, the content of the file will be displayed.
6. If desired you have the option the encryption file. Select 'yes' or 'no'.
7. If 'yes' is selection, enter password (minimum of 8 characters).
8. The file will become a zip file. 


