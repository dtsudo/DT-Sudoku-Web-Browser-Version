
((function () {
	"use strict";
	
	var showFps = false;
	
	DTSudoku.DTSudokuInitializer.Start();
	
	var computeAndRenderNextFrame;
	
	var numberOfFrames = 0;
	var startTimeMillis = Date.now();
	
	var hasAddedFpsLabel = false;
	
	computeAndRenderNextFrame = function () {
		DTSudoku.DTSudokuInitializer.ComputeAndRenderNextFrame();
		
		numberOfFrames++;
		
		if (showFps) {
			if (!hasAddedFpsLabel) {
				var fpsLabelNode = document.getElementById("dtSudokuFpsLabel");
				if (fpsLabelNode !== null) {
					fpsLabelNode.textContent = "FPS: ";
					hasAddedFpsLabel = true;
				}
			}
		}
		
		var currentTimeMillis = Date.now();
		if (currentTimeMillis - startTimeMillis > 2000) {
			var fps = numberOfFrames / 2;
			var fpsNode = document.getElementById("dtSudokuFps");
			
			if (showFps) {
				if (fpsNode !== null)
					fpsNode.textContent = fps.toString();
			}
				
			numberOfFrames = 0;
			startTimeMillis = currentTimeMillis;
		}
	};
	
	setInterval(computeAndRenderNextFrame, 10);
	
})());
