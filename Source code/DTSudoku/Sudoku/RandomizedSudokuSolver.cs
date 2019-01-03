
namespace Sudoku
{
	using System;
	using System.Collections.Generic;

	public class RandomizedSudokuSolver : IRandomizedSudokuSolver
	{
		public RandomizedSudokuSolver()
		{
		}

		public int[,] SolveForFirstSolution(int[,] board)
		{
			return this.SolveForRandomSolution(board, null);
		}

		public bool HasAtLeastOneSolution(int[,] board)
		{
			return this.GetSolutions(board, 1, null).Count == 1;
		}

		public bool HasExactlyOneSolution(int[,] board)
		{
			return this.GetSolutions(board, 2, null).Count == 1;
		}

		public int[,] SolveForRandomSolution(int[,] board, ISudokuRandom random)
		{
			var solution = this.GetSolutions(board, 1, random);
			if (solution.Count == 0)
			{
				throw new Exception("Undefined behavior - board must have at least one solution");
			}
			return solution[0];
		}

		// Given a board, returns up to numSolutions solutions, chosen randomly.
		// If there are less than numSolutions solutions, the returned list's
		// length is the number of solutions the board has.
		private List<int[,]> GetSolutions(int[,] board, int numSolutions, ISudokuRandom random)
		{
			if (numSolutions == 0)
			{
				return new List<int[,]>();
			}

			if (!this.NoConflicts(board))
			{
				return new List<int[,]>();
			}
			
			List<int[,]> solutions = this.GetSolutionsRecursive(this.CopyBoard(board), numSolutions, random);
			
			if (solutions == null)
				return new List<int[,]>();
			
			return solutions;
		}
		
		/*
			Unlike this.GetSolutions, this function requires that numSolutions > 0
			and that the board has no obvious conflicts. (An obvious conflict is two
			duplicate numbers on the same row, column, or 3x3 box.)
			
			Also, unlike this.GetSolutions, if the board has no solutions, this function returns
			null instead of an empty list.
		*/
		private List<int[,]> GetSolutionsRecursive(int[,] board, int numSolutions, ISudokuRandom random)
		{
			int xEmpty = -1;
			int yEmpty = -1;
			
			int numEmptyCells = 0;
			
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					if (board[i, j] == 0)
					{
						xEmpty = i;
						yEmpty = j;
						numEmptyCells++;
					}
				}
			}
		
			if (xEmpty == -1)
			{
				var list = new List<int[,]>();
				list.Add(this.CopyBoard(board));
				return list;
			}
			
			if (numEmptyCells > 50)
			{
				Tuple<int, int> mostConstrainedEmptyCell = this.GetMostConstrainedEmptyCell(board);
				xEmpty = mostConstrainedEmptyCell.Item1;
				yEmpty = mostConstrainedEmptyCell.Item2;
			}

			bool[] potentialValues = new bool[]
			{
				true,
				true, true, true,
				true, true, true,
				true, true, true
			};
			
			for (int i = 0; i < 9; i++)
			{
				int num = board[i, yEmpty];
				if (num != 0)
					potentialValues[num] = false;
			}
			
			for (int j = 0; j < 9; j++)
			{
				int num = board[xEmpty, j];
				if (num != 0)
					potentialValues[num] = false;
			}
			
			int boxX = (xEmpty / 3) * 3;
			int boxY = (yEmpty / 3) * 3;
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					int num = board[boxX + i, boxY + j];
					if (num != 0)
						potentialValues[num] = false;
				}
			}
			
			if (!potentialValues[1]
				&& !potentialValues[2]
				&& !potentialValues[3]
				&& !potentialValues[4]
				&& !potentialValues[5]
				&& !potentialValues[6]
				&& !potentialValues[7]
				&& !potentialValues[8]
				&& !potentialValues[9])
			{
				return null;
			}
			
			List<int> valuesToTry = new List<int>();
			for (int x = 1; x <= 9; x++)
			{
				if (potentialValues[x])
					valuesToTry.Add(x);
			}

			List<int[,]> solutions = null;
			while (valuesToTry.Count > 0)
			{
				int nextIndex = random != null ? random.NextInt(valuesToTry.Count) : 0;
				int nextValueToTry = valuesToTry[nextIndex];
				valuesToTry.RemoveAt(nextIndex);

				board[xEmpty, yEmpty] = nextValueToTry;

				int numSolutionsNeeded = numSolutions - (solutions != null ? solutions.Count : 0);

				List<int[,]> subsolutions = this.GetSolutionsRecursive(board, numSolutionsNeeded, random);

				if (subsolutions != null)
				{
					if (solutions == null)
					{
						solutions = subsolutions;
					}
					else
					{
						foreach (int[,] subsolution in subsolutions)
						{
							solutions.Add(subsolution);
						}
					}
				}

				if (solutions != null && solutions.Count == numSolutions)
				{
					board[xEmpty, yEmpty] = 0;
					return solutions;
				}
			}

			board[xEmpty, yEmpty] = 0;
			return solutions;
		}

		private int[,] CopyBoard(int[,] board)
		{
			int[,] copy = new int[9, 9];

			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					copy[i, j] = board[i, j];
				}
			}

			return copy;
		}

		// Returns null if board has no empty cells
		private Tuple<int, int> GetMostConstrainedEmptyCell(int[,] board)
		{
			HashSet<int>[,] invalid = new HashSet<int>[9, 9];
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					invalid[i, j] = new HashSet<int>();
				}
			}

			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					int value = board[i, j];

					if (value != 0)
					{
						for (int k = 1; k <= 9; k++)
						{
							invalid[i, j].Add(k);
						}

						for (int k = 0; k < 9; k++)
						{
							invalid[i, k].Add(value);
							invalid[k, j].Add(value);
						}

						int xBox = (i / 3) * 3;
						int yBox = (j / 3) * 3;
						invalid[xBox, yBox].Add(value);
						invalid[xBox, yBox+1].Add(value);
						invalid[xBox, yBox+2].Add(value);
						invalid[xBox+1, yBox].Add(value);
						invalid[xBox+1, yBox+1].Add(value);
						invalid[xBox+1, yBox+2].Add(value);
						invalid[xBox+2, yBox].Add(value);
						invalid[xBox+2, yBox+1].Add(value);
						invalid[xBox+2, yBox+2].Add(value);
					}
				}
			}

			Tuple<int, int> mostConstrainedCell = null;
			int? mostConstrainedCellNumPossibleValues = null;

			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					if (board[i, j] == 0)
					{
						int numPossibleValues = 9 - invalid[i, j].Count;
						if (mostConstrainedCell == null || ((int)mostConstrainedCellNumPossibleValues > numPossibleValues))
						{
							mostConstrainedCell = new Tuple<int, int>(i, j);
							mostConstrainedCellNumPossibleValues = numPossibleValues;
						}
					}
				}
			}

			return mostConstrainedCell;
		}

		// Returns true iff the board does not have any immediate conflicts
		private bool NoConflicts(int[,] board)
		{
			// Check rows
			for (int i = 0; i < 9; i++)
			{
				if (!this.NoConflicts(board[i, 0],
									  board[i, 1],
									  board[i, 2],
									  board[i, 3],
									  board[i, 4],
									  board[i, 5],
									  board[i, 6],
									  board[i, 7],
									  board[i, 8]))
				{
					return false;
				}
			}

			// Check columns
			for (int j = 0; j < 9; j++)
			{
				if (!this.NoConflicts(board[0, j],
									  board[1, j],
									  board[2, j],
									  board[3, j],
									  board[4, j],
									  board[5, j],
									  board[6, j],
									  board[7, j],
									  board[8, j]))
				{
					return false;
				}
			}

			// Check 3x3 boxes
			for (int i = 0; i < 9; i = i + 3)
			{
				for (int j = 0; j < 9; j = j + 3)
				{
					if (!this.NoConflicts(board[i, j],
										  board[i, j+1],
										  board[i, j+2],
										  board[i+1, j],
										  board[i+1, j+1],
										  board[i+1, j+2],
										  board[i+2, j],
										  board[i+2, j+1],
										  board[i+2, j+2]))
					{
						return false;
					}
				}
			}

			return true;
		}

		private bool NoConflicts(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9)
		{
			int[] array = new int[10];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 0;
			}
			array[i1] = array[i1] + 1;
			array[i2] = array[i2] + 1;
			array[i3] = array[i3] + 1;
			array[i4] = array[i4] + 1;
			array[i5] = array[i5] + 1;
			array[i6] = array[i6] + 1;
			array[i7] = array[i7] + 1;
			array[i8] = array[i8] + 1;
			array[i9] = array[i9] + 1;
			return array[1] <= 1 && array[2] <= 1 && array[3] <= 1
				&& array[4] <= 1 && array[5] <= 1 && array[6] <= 1
				&& array[7] <= 1 && array[8] <= 1 && array[9] <= 1;
		}
	}
}
