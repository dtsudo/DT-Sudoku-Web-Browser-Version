
namespace DTSudoku
{
	using DTLib;
	using DTSudokuLib;
	using Bridge;

	public class DTSudokuInitializer
	{
		private static IKeyboard bridgeKeyboard;
		private static IMouse bridgeMouse;
		private static IKeyboard previousKeyboard;
		private static IMouse previousMouse;
		
		private static IDisplay<IDTSudokuAssets> display;
		
		private static IFrame<IDTSudokuAssets> frame;
		
		private static void ClearCanvas()
		{
			Script.Write("DTSudokuBridgeDisplayJavascript.clearCanvas()");
		}
		
		public static void Start()
		{
			frame = DTSudoku.GetFirstFrame();

			bridgeKeyboard = new BridgeKeyboard();
			bridgeMouse = new BridgeMouse();
			
			display = new DTSudokuBridgeDisplay();
			
			previousKeyboard = new EmptyKeyboard();
			previousMouse = new EmptyMouse();
			
			ClearCanvas();
			frame.Render(display);
		}
		
		public static void ComputeAndRenderNextFrame()
		{
			IKeyboard currentKeyboard = new CopiedKeyboard(bridgeKeyboard);
			IMouse currentMouse = new CopiedMouse(bridgeMouse);
			
			frame = frame.GetNextFrame(currentKeyboard, currentMouse, previousKeyboard, previousMouse);
			ClearCanvas();
			frame.Render(display);
			
			previousKeyboard = new CopiedKeyboard(currentKeyboard);
			previousMouse = new CopiedMouse(currentMouse);
		}
	}
}
