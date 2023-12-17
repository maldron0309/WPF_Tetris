using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// 'Position' 클래스는 테트리스 게임 그리드에서의 위치를 표현합니다.
    /// 이 클래스는 내부적으로만 사용되며, 동일한 어셈블리 내에서만 접근이 가능합니다.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// 위치의 행을 나타내는 속성입니다.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// 위치의 열을 나타내는 속성입니다.
        /// </summary>
        public int Column { get; set; }
        public int Coloumn { get; internal set; }

        /// <summary>
        /// 'Position' 클래스의 생성자입니다. 행과 열의 위치를 인자로 받아 위치를 초기화합니다.
        /// </summary>
        /// <param name="row">위치의 행을 나타내는 정수입니다.</param>
        /// <param name="column">위치의 열을 나타내는 정수입니다.</param>
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }

}
