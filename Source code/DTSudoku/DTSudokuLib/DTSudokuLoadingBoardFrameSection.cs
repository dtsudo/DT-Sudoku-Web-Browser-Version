
namespace DTSudokuLib
{
	using System;

	using Sudoku;

	public class DTSudokuLoadingBoardFrameSection
	{		
		private ISudokuGenerator sudokuGenerator;

		public DTSudokuLoadingBoardFrameSection(DTSudokuDifficultyValue difficulty, ISudokuRandom random)
		{			
			SudokuDifficulty sudokuDifficulty;
			if (difficulty == DTSudokuDifficultyValue.Easy)
				sudokuDifficulty = SudokuDifficulty.Easy;
			else if (difficulty == DTSudokuDifficultyValue.Normal)
				sudokuDifficulty = SudokuDifficulty.Normal;
			else if (difficulty == DTSudokuDifficultyValue.Hard)
				sudokuDifficulty = SudokuDifficulty.Hard;
			else
				throw new Exception();
			
			this.sudokuGenerator = new SudokuGenerator(new RandomizedSudokuSolver(), random);
			this.sudokuGenerator.StartGeneratingSudokuPuzzle(sudokuDifficulty);
		}
		
		public void KeepGeneratingSudokuPuzzle()
		{
			this.sudokuGenerator.KeepGeneratingSudokuPuzzle();
		}
		
		public bool IsDoneGeneratingSudokuPuzzle()
		{
			return this.sudokuGenerator.IsDoneGeneratingSudokuPuzzle();
		}

		public int[,] GetGeneratedSudokuPuzzle()
		{
			return this.sudokuGenerator.GetGeneratedSudokuPuzzle();
		}
	}
}
