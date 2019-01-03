
namespace Sudoku
{
	using System;
	using System.Collections.Generic;

	public class SudokuGenerator : ISudokuGenerator
	{
		private IRandomizedSudokuSolver solver;
		private ISudokuRandom random;
		
		private int[,] currentPuzzle;
		private int numRemoved;
		private int numTries;
		private bool isDone;
		private SudokuDifficulty difficulty;
		private List<Tuple<int, int>> cellsThatCannotBeRemoved;

		public SudokuGenerator(IRandomizedSudokuSolver s, ISudokuRandom r)
		{
			this.solver = s;
			this.random = r;
			this.currentPuzzle = null;
			this.numRemoved = -1;
			this.numTries = -1;
			this.isDone = false;
			this.difficulty = SudokuDifficulty.Easy;
			this.cellsThatCannotBeRemoved = null;
		}
		
		public void StartGeneratingSudokuPuzzle(SudokuDifficulty difficulty)
		{
			int[,] solvedBoard = this.solver.SolveForRandomSolution(new int[9, 9], this.random);

			Func<int[,], int[,]> copyBoard = board =>
				{
					var newBoard = new int[9, 9];
					for (int i = 0; i < 9; i++)
						for (int j = 0; j < 9; j++)
							newBoard[i, j] = board[i, j];

					return newBoard;
				};

			this.numRemoved = 0;
			this.numTries = 0;
			
			this.currentPuzzle = copyBoard(solvedBoard);
			
			this.isDone = false;
			
			this.difficulty = difficulty;
			
			this.cellsThatCannotBeRemoved = new List<Tuple<int, int>>();
		}
		
		public void KeepGeneratingSudokuPuzzle()
		{
			// Note that maxRemoved could be off by one because we remove two cells at a time
			int maxRemoved = 0;
			int maxNumTries = 0;
			if (this.difficulty == SudokuDifficulty.Easy)
			{
				maxRemoved = 45;
				maxNumTries = 100;
			}
			else if (this.difficulty == SudokuDifficulty.Normal)
			{
				maxRemoved = 50;
				maxNumTries = 200;
			}
			else if (this.difficulty == SudokuDifficulty.Hard)
			{
				maxRemoved = 55;
				maxNumTries = 300;
			}
			else
			{
				throw new Exception("Unrecognized difficulty");
			}
			
			Func<Tuple<int, int, int, int>> getLocation = () =>
			{
				int i1 = this.random.NextInt(9);
				int i2 = this.random.NextInt(9);
				int i3 = 8 - i1;
				int i4 = 8 - i2;
				return new Tuple<int, int, int, int>(i1, i2, i3, i4);
			};
			
			Func<int[,], int[,]> copyBoard = board =>
				{
					var newBoard = new int[9, 9];
					for (int i = 0; i < 9; i++)
						for (int j = 0; j < 9; j++)
							newBoard[i, j] = board[i, j];

					return newBoard;
				};
			
			if (this.numRemoved >= maxRemoved)
			{
				this.isDone = true;
				return;
			}
			if (this.numTries >= maxNumTries)
			{
				this.isDone = true;
				return;
			}

			var location = getLocation();
			
			foreach (Tuple<int, int> cell in this.cellsThatCannotBeRemoved)
			{
				if (cell.Item1 == location.Item1 && cell.Item2 == location.Item2)
				{
					this.numTries = this.numTries + 1;
					return;
				}
			}
			
			if (this.currentPuzzle[location.Item1, location.Item2] == 0)
			{
				this.numTries = this.numTries + 1;
				return;
			}
			
			int[,] proposedPuzzle = copyBoard(this.currentPuzzle);
			proposedPuzzle[location.Item1, location.Item2] = 0;
			proposedPuzzle[location.Item3, location.Item4] = 0;
			
			if (this.solver.HasExactlyOneSolution(proposedPuzzle))
			{
				this.currentPuzzle = copyBoard(proposedPuzzle);
				this.numRemoved = 0;
				for (int i = 0; i < 9; i++)
				{
					for (int j = 0; j < 9; j++)
					{
						if (this.currentPuzzle[i, j] == 0)
							this.numRemoved = this.numRemoved + 1;
					}
				}
			}
			else
			{
				this.cellsThatCannotBeRemoved.Add(new Tuple<int, int>(location.Item1, location.Item2));
				if (location.Item1 != location.Item3 || location.Item2 != location.Item4)
					this.cellsThatCannotBeRemoved.Add(new Tuple<int, int>(location.Item3, location.Item4));
			}

			this.numTries = this.numTries + 1;
		}
		
		public bool IsDoneGeneratingSudokuPuzzle()
		{
			return this.isDone;
		}
		
		public int[,] GetGeneratedSudokuPuzzle()
		{
			if (!this.IsDoneGeneratingSudokuPuzzle())
				throw new Exception();
			
			return this.currentPuzzle;
		}
	}
}

