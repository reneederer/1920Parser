using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1920Parser
{
    public abstract class AbstractNode
    {
        public bool Redefines { get; protected set; }
        public int Level { get; protected set; }
        public string VarName { get; protected set; }
        public int RepeatCount { get; protected set; }
        public int RepeatIndex { get; protected set; }
        public string Comment { get; protected set; }
        public AbstractNode(bool redefines, int level, string varName, int repeatCount, int repeatIndex, string comment)
        {
            this.Redefines = redefines;
            this.Level = level;
            this.VarName = varName;
            this.RepeatCount = repeatCount;
            this.RepeatIndex = repeatIndex;
            this.Comment = comment;
        }

        public abstract AbstractNode CreateCopyWithIndex(int index);
        public abstract int AssignValue(string data);
        public abstract string ToString(int tabCount);
        public override string ToString()
        {
            return ToString(0);
        }
        public abstract void AddChild(AbstractNode child);
    }
}
