
namespace Sudoku
{
	public enum SudokuDifficulty
	{
		Easy,
		Normal,
		Hard
	}

	public interface ISudokuGenerator
	{	
		void StartGeneratingSudokuPuzzle(SudokuDifficulty difficulty);
		void KeepGeneratingSudokuPuzzle();
		bool IsDoneGeneratingSudokuPuzzle();
		int[,] GetGeneratedSudokuPuzzle();
	}
}

