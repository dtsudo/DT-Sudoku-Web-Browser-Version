
/*
	Requires DTSudokuBridgeImagesJavascript.js to already be on the page.
*/
DTSudokuBridgeDisplayJavascript = ((function () {
	"use strict";
	
	var drawImage = function (imageName, x, y) {
		var canvas = document.getElementById("dtSudokuCanvas");
		
		if (canvas === null)
			return;
		
		var context = canvas.getContext("2d");
		
		context.drawImage(DTSudokuBridgeImagesJavascript.sudokuImages[imageName], x, y);
	};
	
	var drawRectangle = function (x, y, width, height, red, green, blue, alpha, fill) {
		var canvas = document.getElementById("dtSudokuCanvas");
		
		if (canvas === null)
			return;
		
		var context = canvas.getContext("2d");
		
		context.fillStyle = "rgba(" + red.toString() + ", " + green.toString() + ", " + blue.toString() + ", " + (alpha / 255).toString() + ")";
		context.strokeStyle = "rgba(" + red.toString() + ", " + green.toString() + ", " + blue.toString() + ", " + (alpha / 255).toString() + ")";
		
		if (fill)
			context.fillRect(x, y, width, height);
		else
			context.strokeRect(x, y, width, height);
	};
	
	var clearCanvas = function () {
		var canvas = document.getElementById("dtSudokuCanvas");
		
		if (canvas === null)
			return;
		
		var context = canvas.getContext("2d");
		
		context.clearRect(0, 0, canvas.width, canvas.height);
	};
	
	return {
		drawImage: drawImage,
		drawRectangle: drawRectangle,
		clearCanvas: clearCanvas
	};
})());

