using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1920Parser
{
    /// <summary>
    /// GroupNode represents an internal node.
    /// </summary>
    class GroupNode : AbstractNode
    {
        private List<AbstractNode> children = new List<AbstractNode>();
        public GroupNode(bool redefines, int level, string varName, int repeatCount, int repeatIndex, string comment)
            : base(redefines, level, varName, repeatCount, repeatIndex, comment)
        {}

        /// <summary>
        /// Assigns substrings of data to its children. The begin of a substring shifts with each assignment to a child.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override int AssignValue(string data)
        {
            int currentShift = 0;
            int totalShift = 0;
            foreach (var child in children)
            {
                if (!child.Redefines)
                {
                    currentShift = child.AssignValue(data.Substring(totalShift));
                    totalShift += currentShift;
                }
                else
                {
                    child.AssignValue(data.Substring(totalShift - currentShift));
                }
            }
            return totalShift;

        }

        /// <summary>
        /// Returns a string representing this GroupNode and its children.
        /// </summary>
        /// <param name="tabCount">indicates how much the string is indented.
        /// It is used to increase the indentation of child nodes.
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString(int tabCount)
        {
            StringBuilder strBuilder = new StringBuilder();
            if (Level != 0)
            {
                strBuilder.Append(string.Format("{0}{1}{2} {3}{4}\r\n",
                new string(' ', tabCount * 4 - (Redefines ? 1 : 0)),
                Redefines ? "R" : "",
                Level.ToString().PadLeft(2, '0'),
                VarName,
                RepeatCount > 1 ? string.Format("({0})", RepeatIndex) : ""));
            }
            foreach (var child in children)
            {
                strBuilder.Append(child.ToString(tabCount + (Level == 0 ? 0 : 1)));
            }
            return strBuilder.ToString();
        }

        /// <summary>
        /// Creates a deep copy of this GroupNode and sets its RepeatIndex to the value of the parameter.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public override AbstractNode CreateCopyWithIndex(int index)
        {
            GroupNode g = new GroupNode(Redefines, Level, VarName, RepeatCount, index, Comment);
            foreach (var child in children)
            {
                g.AddChild(child.CreateCopyWithIndex(child.RepeatIndex));
            }
            return g;
        }

        /// <summary>
        /// Adds a child node. The child could be another GroupNode.
        /// </summary>
        public override void AddChild(AbstractNode child)
        {
            children.Add(child);
        }

        /// <summary>
        /// Returns the Hashcode of this class.
        /// This method is not supposed to be called directly, it was overwritten in conjunction with Equals().
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Compares two GroupNodes. GroupNodes are equal if all their attributes are equal
        /// and the attributes of all their children are equal.
        ///</summary>
        public override bool Equals(object obj)
        {
            GroupNode other = null;
            try
            {
                other = (GroupNode)obj;
            }
            catch
            {
                return false;
            }
            bool areAttributesEqual =
                Redefines == other.Redefines &&
                Level == other.Level &&
                VarName == other.VarName &&
                RepeatCount == other.RepeatCount &&
                Comment == other.Comment;
            if(!areAttributesEqual || children.Count != other.children.Count)
            {
                return false;
            }

            for(int i = 0; i < children.Count; ++i)
            {
                if (!children[i].Equals(other.children[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
