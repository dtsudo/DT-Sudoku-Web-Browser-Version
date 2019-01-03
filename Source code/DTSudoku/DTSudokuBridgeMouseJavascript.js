

DTSudokuBridgeMouseJavascript = ((function () {
	"use strict";
	
	var mouseXPosition = 0;
	var mouseYPosition = 0;
	
	var mouseMoveHandler = function (e) {
	
		var canvas = document.getElementById("dtSudokuCanvas");
		
		if (canvas === null)
			return;
		
		var xPosition = e.clientX - canvas.offsetLeft;
		
		if (xPosition < 0)
			xPosition = 0;
		
		if (xPosition > canvas.width)
			xPosition = canvas.width;
		
		var yPosition = e.clientY - canvas.offsetTop;
		
		if (yPosition < 0)
			yPosition = 0;
		
		if (yPosition > canvas.height)
			yPosition = canvas.height;
		
		mouseXPosition = xPosition;
		mouseYPosition = yPosition;
	};
	
	var isLeftMouseButtonPressed = false;
	var isRightMouseButtonPressed = false;
	
	var checkMouseButtonHandler = function (e) {
		if ((e.buttons & 1) === 1)
			isLeftMouseButtonPressed = true;
		else
			isLeftMouseButtonPressed = false;
		
		if ((e.buttons & 2) === 2)
			isRightMouseButtonPressed = true;
		else
			isRightMouseButtonPressed = false;
	};
	
	document.addEventListener("mousemove", function (e) { mouseMoveHandler(e); checkMouseButtonHandler(e); }, false);
	document.addEventListener("mousedown", function (e) { checkMouseButtonHandler(e); }, false);
	document.addEventListener("mouseup", function (e) { checkMouseButtonHandler(e); }, false);
	
	return {
		isLeftMouseButtonPressed: function () { return isLeftMouseButtonPressed; },
		isRightMouseButtonPressed: function () { return isRightMouseButtonPressed; },
		getMouseX: function () { return Math.round(mouseXPosition); },
		getMouseY: function () { return Math.round(mouseYPosition); }
	};
})());