using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// 테트리스 게임의 상태를 관리하는 클래스입니다.
    /// </summary>
    internal class GameState
    {
        private Block currentBlock;

        /// <summary>
        /// 현재 블럭을 가져오거나 설정합니다. 설정 시 블럭이 초기화됩니다.
        /// </summary>
        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset(); // 블럭을 설정할 때, 블럭을 초기화합니다.
            }
        }

        /// <summary>
        /// 게임 그리드를 가져옵니다.
        /// </summary>
        public GameGrid GameGrid { get; }

        /// <summary>
        /// 블럭 큐를 가져옵니다.
        /// </summary>
        public BlockQueue BlockQueue { get; }

        /// <summary>
        /// 게임 오버 상태를 가져오거나 설정합니다.
        /// </summary>
        public bool GameOver { get; private set; }

        /// <summary>
        /// GameState 객체를 생성하고, 게임 그리드와 블럭 큐를 초기화하며, 현재 블럭을 설정합니다.
        /// </summary>
        public GameState()
        {
            GameGrid = new GameGrid(22, 10); // 22행 10열의 게임 그리드를 생성합니다.
            BlockQueue = new BlockQueue(); // 블럭 큐를 생성합니다.
            CurrentBlock = BlockQueue.GetAndUpdate(); // 현재 블럭을 블럭 큐에서 가져옵니다.
        }

        /// <summary>
        /// 현재 블럭이 게임 그리드에 맞는지 확인하는 메서드입니다.
        /// </summary>
        /// <returns>블럭이 게임 그리드에 맞으면 true, 그렇지 않으면 false</returns>
        private bool BlockFits()
        {
            foreach (Position p in CurrentBlock.TilePositions()) // 현재 블럭의 모든 타일에 대해
            {
                if (!GameGrid.IsEmpty(p.Row, p.Column)) // 타일이 게임 그리드에 맞지 않으면
                {
                    return false; // false를 반환합니다.
                }
            }

            return true; // 모든 타일이 게임 그리드에 맞으면 true를 반환합니다.
        }

        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();

            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }

        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();

            if (!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }

        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0,1);
            }
        }

        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        private bool IsGameOver()
        {
            return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
        }

        private void PlaceBlock()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
            }

            GameGrid.ClearFullRows();

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = BlockQueue.GetAndUpdate();
            }

        }

        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);

            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }
    }
}
