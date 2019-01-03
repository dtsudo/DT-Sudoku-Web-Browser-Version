
namespace DTSudoku
{
	using DTLib;
	using Bridge;
	
	public class BridgeMouse : IMouse
	{
		public BridgeMouse()
		{
		}
		
		public int GetX()
		{
			return Script.Write<int>("DTSudokuBridgeMouseJavascript.getMouseX()");
		}

		public int GetY()
		{
			return Script.Write<int>("DTSudokuBridgeMouseJavascript.getMouseY()");
		}

		public bool IsLeftMouseButtonPressed()
		{
			return Script.Write<bool>("DTSudokuBridgeMouseJavascript.isLeftMouseButtonPressed()");
		}

		public bool IsRightMouseButtonPressed()
		{
			return Script.Write<bool>("DTSudokuBridgeMouseJavascript.isRightMouseButtonPressed()");
		}
	}
}
