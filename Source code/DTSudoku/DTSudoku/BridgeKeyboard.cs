
namespace DTSudoku
{
	using DTLib;
	using System;
	using System.Collections.Generic;
	using Bridge;

	public class BridgeKeyboard : IKeyboard
	{
		public BridgeKeyboard()
		{
		}
		
		public bool IsPressed(Key key)
		{
			string correspondingKeyCode;
			
			switch (key)
			{
				case Key.A:
					correspondingKeyCode = "a";
					break;
				case Key.B:
					correspondingKeyCode = "b";
					break;
				case Key.C:
					correspondingKeyCode = "c";
					break;
				case Key.D:
					correspondingKeyCode = "d";
					break;
				case Key.E:
					correspondingKeyCode = "e";
					break;
				case Key.F:
					correspondingKeyCode = "f";
					break;
				case Key.G:
					correspondingKeyCode = "g";
					break;
				case Key.H:
					correspondingKeyCode = "h";
					break;
				case Key.I:
					correspondingKeyCode = "i";
					break;
				case Key.J:
					correspondingKeyCode = "j";
					break;
				case Key.K:
					correspondingKeyCode = "k";
					break;
				case Key.L:
					correspondingKeyCode = "l";
					break;
				case Key.M:
					correspondingKeyCode = "m";
					break;
				case Key.N:
					correspondingKeyCode = "n";
					break;
				case Key.O:
					correspondingKeyCode = "o";
					break;
				case Key.P:
					correspondingKeyCode = "p";
					break;
				case Key.Q:
					correspondingKeyCode = "q";
					break;
				case Key.R:
					correspondingKeyCode = "r";
					break;
				case Key.S:
					correspondingKeyCode = "s";
					break;
				case Key.T:
					correspondingKeyCode = "t";
					break;
				case Key.U:
					correspondingKeyCode = "u";
					break;
				case Key.V:
					correspondingKeyCode = "v";
					break;
				case Key.W:
					correspondingKeyCode = "w";
					break;
				case Key.X:
					correspondingKeyCode = "x";
					break;
				case Key.Y:
					correspondingKeyCode = "y";
					break;
				case Key.Z:
					correspondingKeyCode = "z";
					break;
				case Key.Zero:
					correspondingKeyCode = "0";
					break;
				case Key.One:
					correspondingKeyCode = "1";
					break;
				case Key.Two:
					correspondingKeyCode = "2";
					break;
				case Key.Three:
					correspondingKeyCode = "3";
					break;
				case Key.Four:
					correspondingKeyCode = "4";
					break;
				case Key.Five:
					correspondingKeyCode = "5";
					break;
				case Key.Six:
					correspondingKeyCode = "6";
					break;
				case Key.Seven:
					correspondingKeyCode = "7";
					break;
				case Key.Eight:
					correspondingKeyCode = "8";
					break;
				case Key.Nine:
					correspondingKeyCode = "9";
					break;
				case Key.UpArrow:
					correspondingKeyCode = "ArrowUp";
					break;
				case Key.DownArrow:
					correspondingKeyCode = "ArrowDown";
					break;
				case Key.LeftArrow:
					correspondingKeyCode = "ArrowLeft";
					break;
				case Key.RightArrow:
					correspondingKeyCode = "ArrowRight";
					break;
				case Key.Delete:
					correspondingKeyCode = "Delete";
					break;
				case Key.Backspace:
					correspondingKeyCode = "Backspace";
					break;
				default:
					throw new Exception();
			}
			
			/*
				None of the keycodes need to be escaped
				(but this would be necessary if we had a key
				such as backslash)
			*/
			var javascriptCode = "DTSudokuBridgeKeyboardJavascript.isKeyPressed('" + correspondingKeyCode + "')";
			
			bool result = Script.Eval<bool>(javascriptCode);
			
			if (result)
				return true;
			
			return false;
			
		}
	}
}
