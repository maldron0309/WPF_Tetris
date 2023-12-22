using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// 블럭을 관리하는 클래스입니다.
    /// </summary>
    internal class BlockQueue
    {
        // 블럭의 종류를 배열로 저장합니다.
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock(),
        };

        // 무작위로 블럭을 선택하기 위한 Random 객체입니다.
        private readonly Random random = new Random();

        /// <summary>
        /// 다음에 사용할 블럭입니다.
        /// </summary>
        public Block NextBlock { get; private set; }

        /// <summary>
        /// BlockQueue 객체를 생성하고, NextBlock을 무작위로 선택합니다.
        /// </summary>
        public BlockQueue()
        {
            NextBlock = RandomBlock();
        }

        /// <summary>
        /// 무작위로 블럭을 선택하는 메서드입니다.
        /// </summary>
        /// <returns>무작위로 선택된 블럭</returns>
        private Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        /// <summary>
        /// 현재 블럭을 가져오고, 다음 블럭을 무작위로 업데이트하는 메서드입니다.
        /// </summary>
        /// <returns>현재 블럭</returns>
        public Block GetAndUpdate()
        {
            Block block = NextBlock;

            do
            {
                NextBlock = RandomBlock();
            }
            while (block.Id == NextBlock.Id); // 현재 블럭과 다음 블럭이 같지 않을 때까지 무작위로 블럭을 선택합니다.

            return block;
        }
    }
}
