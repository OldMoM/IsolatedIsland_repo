using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

namespace Peixi
{
    /// <summary>布置在BuildSketch中，代替原有建造判断节点，迫使玩家只能按照固定的序列顺序建造岛块</summary>
    public class FixedSequenceBuilder 
    {
        public List<Vector2Int> fixedSequence = new List<Vector2Int>();
        public int currentBuildPosPointer = 0;
        public Vector2Int CurrentBuildPos => fixedSequence[currentBuildPosPointer];

        public FixedSequenceBuilder()
        {
            fixedSequence
                .AddItem(new Vector2Int(0, -1))
                .AddItem(new Vector2Int(-1, 1))
                .AddItem(new Vector2Int(-1, 2));
        }
    }
}
