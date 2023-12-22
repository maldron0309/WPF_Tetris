using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public abstract class Block
    {
        protected abstract Position[][] Tiles { get; }
        protected abstract Position StartOffset { get; }

        public abstract int Id { get; }

        private int rotationState;
        private Position offset;

        /// <summary>
        /// 'Block' 클래스의 기본 생성자입니다. 'offset'을 'StartOffset'으로 초기화합니다.
        /// </summary>
        public Block()
        {
            offset = new Position(StartOffset.Row, StartOffset.Column);
        }

        /// <summary>
        /// 현재 블록의 모든 타일 위치를 열거합니다.
        /// </summary>
        /// <returns>현재 회전 상태와 위치 오프셋을 고려한 타일 위치의 열거자를 반환합니다.</returns>
        public IEnumerable<Position> TilePositions()
        {
            foreach (Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }

        /// <summary>
        /// 블록을 시계 방향으로 회전시킵니다.
        /// </summary>
        public void RotateCW()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }

        /// <summary>
        /// 블록을 반시계 방향으로 회전시킵니다.
        /// </summary>
        public void RotateCCW()
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }

        /// <summary>
        /// 블록을 주어진 행과 열 만큼 이동시킵니다.
        /// </summary>
        /// <param name="rows">이동할 행의 수입니다.</param>
        /// <param name="columns">이동할 열의 수입니다.</param>
        public void Move(int rows, int columns)
        {
            offset.Row += rows;
            offset.Column += columns;
        }

        /// <summary>
        /// 블록의 상태를 초기화합니다. 회전 상태를 0으로 되돌리고, 위치를 'StartOffset'으로 재설정합니다.
        /// </summary>
        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }

        internal void Move(double v)
        {
            throw new NotImplementedException();
        }
    }
}
