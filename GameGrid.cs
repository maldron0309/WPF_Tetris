using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class GameGrid
    {
        private readonly int[,] grid;

        public int Rows { get; } // 행
        public int Columns { get; } // 열

        /// <summary>
        /// 인덱서를 통해 그리드의 특정 위치에 있는 값을 가져오거나 설정합니다.
        /// </summary>
        /// <param name="r">행의 위치를 나타내는 정수입니다.</param>
        /// <param name="c">열의 위치를 나타내는 정수입니다.</param>
        /// <returns>해당 위치에 있는 그리드의 값입니다.</returns>
        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        /// <summary>
        /// 'GameGrid' 클래스의 생성자입니다. 행과 열의 수를 인자로 받아 그리드를 초기화합니다.
        /// </summary>
        /// <param name="rows">그리드의 행 수를 나타내는 정수입니다.</param>
        /// <param name="columns">그리드의 열 수를 나타내는 정수입니다.</param>
        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];
        }

        /// <summary>
        /// 주어진 행과 열의 위치가 그리드 내부에 있는지 확인합니다.
        /// </summary>
        /// <param name="r">확인하려는 위치의 행을 나타내는 정수입니다.</param>
        /// <param name="c">확인하려는 위치의 열을 나타내는 정수입니다.</param>
        /// <returns>행과 열의 위치가 그리드 범위 내에 있으면 true를 반환하고, 그렇지 않으면 false를 반환합니다.</returns>
        public bool IsInside(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }

        /// <summary>
        /// 주어진 위치의 셀이 비어있는지 확인합니다.
        /// </summary>
        /// <param name="r">확인하려는 셀의 행 위치입니다.</param>
        /// <param name="c">확인하려는 셀의 열 위치입니다.</param>
        /// <returns>셀이 비어있으면 true를 반환하고, 그렇지 않으면 false를 반환합니다.</returns>
        public bool IsEmpty(int r, int c)
        {
            return IsInside(r, c) && grid[r, c] == 0;
        }

        /// <summary>
        /// 주어진 행이 가득 찼는지 확인합니다.
        /// </summary>
        /// <param name="r">확인하려는 행의 위치입니다.</param>
        /// <returns>행이 가득 찼으면 true를 반환하고, 그렇지 않으면 false를 반환합니다.</returns>
        public bool IsRowFull(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 주어진 행이 비어 있는지 확인합니다.
        /// </summary>
        /// <param name="r">확인하려는 행의 위치입니다.</param>
        /// <returns>행이 비어 있으면 true를 반환하고, 그렇지 않으면 false를 반환합니다.</returns>
        public bool IsRowEmpty(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 주어진 행의 모든 셀을 비웁니다.
        /// </summary>
        /// <param name="r">비우려는 행의 위치입니다.</param>
        private void ClearRow(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r, c] = 0;
            }
        }

        /// <summary>
        /// 주어진 행의 모든 셀을 아래로 이동시킵니다.
        /// </summary>
        /// <param name="r">이동하려는 행의 위치입니다.</param>
        /// <param name="numRows">아래로 이동하려는 행의 수입니다.</param>
        private void MoveRowDown(int r, int numRows)
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r + numRows, c] = grid[r, c];
                grid[r, c] = 0;
            }
        }

        /// <summary>
        /// 가득 찬 행을 지우고, 지워진 행의 수를 반환합니다.
        /// </summary>
        /// <returns>지워진 행의 수입니다.</returns>
        public int ClearFullRows()
        {
            int cleared = 0;

            for (int r = Rows - 1; r >= 0; r--)
            {
                if (IsRowFull(r))
                {
                    ClearRow(r);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    MoveRowDown(r, cleared);
                }
            }
            return cleared;
        }

    }
}
