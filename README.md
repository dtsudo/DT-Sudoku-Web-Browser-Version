# Overview

This is a port of the "DT Sudoku" project (https://github.com/dtsudo/DT-Sudoku), so that it can run in a web browser.

It was written to experiment with the Bridge.NET C#-to-javascript compiler.

# How to execute the program

Open the `DT Sudoku.html` file in a web browser.

# Licensing

The source code is licensed under the MIT license.  However, note that it uses dependencies and other assets that are licensed under different licenses.

This project uses Bridge.NET to compile the C# code into javascript. Bridge.NET is licensed under the Apache License Version 2.0. See https://github.com/bridgedotnet/Bridge and https://github.com/bridgedotnet/CLI for more details about Bridge.NET.

The font used in the images was generated by metaflop.  (See http://www.metaflop.com/modulator for more details about metaflop.)  As the website notes: "All outline-based fonts (webfonts or otf) that are generated with this project are licensed under the SIL Open Font License v1.1 (OFL). This means that you can freely use and extend the fonts and also use them commercially. Any derivative work has to be made freely available under the same license."

# How to compile the source code

This project uses Bridge.NET to compile the C# code into javascript. The Bridge CLI (https://github.com/bridgedotnet/CLI) needs to be installed so that we can run the Bridge compiler on the command line.

Once the Bridge CLI is installed, go to the source code folder and run `bridge build` to compile the C# code:

* `cd "Source code/DTSudoku/"`
* `bridge build`

This will generate a few files in the `Source code/DTSudoku/dist/` folder. However, the project also needs a few additional javascript and html files:

* Copy `Source code/DTSudoku/DTSudokuBridgeDisplayJavascript.js` to the `Source code/DTSudoku/dist/` folder.
* Copy `Source code/DTSudoku/DTSudokuBridgeImagesJavascript.js` to the `Source code/DTSudoku/dist/` folder.
* Copy `Source code/DTSudoku/DTSudokuBridgeKeyboardJavascript.js` to the `Source code/DTSudoku/dist/` folder.
* Copy `Source code/DTSudoku/DTSudokuBridgeMouseJavascript.js` to the `Source code/DTSudoku/dist/` folder.
* Copy `Source code/DTSudoku/DTSudokuInitializerJavascript.js` to the `Source code/DTSudoku/dist/` folder.
* Copy `Source code/DTSudoku/DT Sudoku.html` to the `Source code/DTSudoku/dist/` folder.
* Optionally, delete `Source code/DTSudoku/dist/index.html` since we don't need this file.

Then, to run the program, simply run `Source code/DTSudoku/dist/DT Sudoku.html` in a web browser.
