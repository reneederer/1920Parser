using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1920Parser
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractNode
    {

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AbstractNode"/> is redefines.
        /// </summary>
        /// <value>
        ///   <c>true</c> if redefines; otherwise, <c>false</c>.
        /// </value>
        public bool Redefines { get; protected set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public int Level { get; protected set; }

        /// <summary>
        /// Gets or sets the name of the variable.
        /// </summary>
        /// <value>
        /// The name of the variable.
        /// </value>
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
