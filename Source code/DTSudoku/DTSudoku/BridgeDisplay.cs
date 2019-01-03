
namespace DTSudoku
{
	using DTLib;
	using DTSudokuLib;
	using System;
	using Bridge;

	public class BridgeDisplayUtil
	{
		/*
			For whatever reason, i.ToString(CultureInfo.InvariantCulture)
			doesn't work in Bridge.NET, so we manually implement it here.
		*/
		public static string IntToString(int i)
		{
			switch (i)
			{
				case 0: return "0";
				case 1: return "1";
				case 2: return "2";
				case 3: return "3";
				case 4: return "4";
				case 5: return "5";
				case 6: return "6";
				case 7: return "7";
				case 8: return "8";
				case 9: return "9";
				case int.MinValue: return "-2147483648";
			}
			
			if (i < 0)
				return "-" + IntToString(-i);
			
			int x = i / 10;
			int y = i % 10;
			
			return IntToString(x) + IntToString(y);
		}
	}
	
	public abstract class BridgeDisplay<T> : IDisplay<T> where T : IAssets
	{
		public BridgeDisplay()
		{
		}

		public void DrawRectangle(int x, int y, int width, int height, DTColor color, bool fill)
		{
			int red = color.R;
			int green = color.G;
			int blue = color.B;
			int alpha = color.Alpha;
			
			var javascriptCode = "DTSudokuBridgeDisplayJavascript.drawRectangle("
				+ BridgeDisplayUtil.IntToString(x)
				+ ", "
				+ BridgeDisplayUtil.IntToString(y)
				+ ", "
				+ BridgeDisplayUtil.IntToString(width)
				+ ", "
				+ BridgeDisplayUtil.IntToString(height)
				+ ", "
				+ BridgeDisplayUtil.IntToString(red)
				+ ", "
				+ BridgeDisplayUtil.IntToString(green)
				+ ", "
				+ BridgeDisplayUtil.IntToString(blue)
				+ ", "
				+ BridgeDisplayUtil.IntToString(alpha)
				+ ", "
				+ (fill ? "true" : "false")
				+ ")";
		
			Script.Eval(javascriptCode);
		}
		
		public abstract T GetAssets();
	}

	public class DTSudokuBridgeDisplay : BridgeDisplay<IDTSudokuAssets>
	{
		private DTSudokuAssets assets;

		private class DTSudokuAssets : IDTSudokuAssets
		{
			public void DrawImage(DTSudokuImage image, int x, int y)
			{
				string imageName;
				if (image == DTSudokuImage.BlueOne) imageName = "blueOne";
				else if (image == DTSudokuImage.BlueTwo) imageName = "blueTwo";
				else if (image == DTSudokuImage.BlueThree) imageName = "blueThree";
				else if (image == DTSudokuImage.BlueFour) imageName = "blueFour";
				else if (image == DTSudokuImage.BlueFive) imageName = "blueFive";
				else if (image == DTSudokuImage.BlueSix) imageName = "blueSix";
				else if (image == DTSudokuImage.BlueSeven) imageName = "blueSeven";
				else if (image == DTSudokuImage.BlueEight) imageName = "blueEight";
				else if (image == DTSudokuImage.BlueNine) imageName = "blueNine";
				else if (image == DTSudokuImage.RedOne) imageName = "redOne";
				else if (image == DTSudokuImage.RedTwo) imageName = "redTwo";
				else if (image == DTSudokuImage.RedThree) imageName = "redThree";
				else if (image == DTSudokuImage.RedFour) imageName = "redFour";
				else if (image == DTSudokuImage.RedFive) imageName = "redFive";
				else if (image == DTSudokuImage.RedSix) imageName = "redSix";
				else if (image == DTSudokuImage.RedSeven) imageName = "redSeven";
				else if (image == DTSudokuImage.RedEight) imageName = "redEight";
				else if (image == DTSudokuImage.RedNine) imageName = "redNine";
				else if (image == DTSudokuImage.BlackOne) imageName = "blackOne";
				else if (image == DTSudokuImage.BlackTwo) imageName = "blackTwo";
				else if (image == DTSudokuImage.BlackThree) imageName = "blackThree";
				else if (image == DTSudokuImage.BlackFour) imageName = "blackFour";
				else if (image == DTSudokuImage.BlackFive) imageName = "blackFive";
				else if (image == DTSudokuImage.BlackSix) imageName = "blackSix";
				else if (image == DTSudokuImage.BlackSeven) imageName = "blackSeven";
				else if (image == DTSudokuImage.BlackEight) imageName = "blackEight";
				else if (image == DTSudokuImage.BlackNine) imageName = "blackNine";
				else if (image == DTSudokuImage.HighlightedCell)
					imageName = "highlightedCell";
				else if (image == DTSudokuImage.SolvedText)
					imageName = "solvedText";
				else if (image == DTSudokuImage.DifficultyText)
					imageName = "difficultyText";
				else if (image == DTSudokuImage.EasyText)
					imageName = "easyText";
				else if (image == DTSudokuImage.NormalText)
					imageName = "normalText";
				else if (image == DTSudokuImage.HardText)
					imageName = "hardText";
				else if (image == DTSudokuImage.HighlightedDifficulty)
					imageName = "highlightedDifficulty";
				else if (image == DTSudokuImage.NewGameText)
					imageName = "newGameText";
				else if (image == DTSudokuImage.HighlightedNewGame)
					imageName = "highlightedNewGame";
				else if (image == DTSudokuImage.LoadingText)
					imageName = "loadingText";
				else
					throw new Exception();

				var javascriptCode = "DTSudokuBridgeDisplayJavascript.drawImage('"
					+ imageName
					+ "', "
					+ BridgeDisplayUtil.IntToString(x)
					+ ", "
					+ BridgeDisplayUtil.IntToString(y)
					+ ")";
			
				Script.Eval(javascriptCode);
			}
		}

		public DTSudokuBridgeDisplay()
		{
			this.assets = new DTSudokuAssets();
		}

		public override IDTSudokuAssets GetAssets()
		{
			return this.assets;
		}
	}
}
