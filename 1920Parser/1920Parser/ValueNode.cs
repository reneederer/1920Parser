using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1920Parser
{
    class ValueNode : AbstractNode
    {
        public string Type { get; private set; }
        public int Length { get; private set; }
        public string Value { get; private set; }

        public ValueNode(bool redefines, int level, string varName, string type, int length, int repeatCount, int repeatIndex, string comment)
            : base(redefines, level, varName, repeatCount, repeatIndex, comment)
        {
            Type = type;
            Length = length;
        }

        public override AbstractNode CreateCopyWithIndex(int index)
        {
            return new ValueNode(Redefines, Level, VarName, Type, Length, RepeatCount, index, Comment);
        }

        public override int AssignValue(string data)
        {
            Value = data.Substring(0, Length);
            return Length;
        }

        public override string ToString(int tabCount)
        {
            if (Level == 0)
            {
                return "";
            }
            return string.Format("{0}{1}{2} {3}{4}={5}\r\n",
                new string(' ', tabCount * 4 - (Redefines ? 1 : 0)),
                Redefines ? "R" : "",
                Level.ToString().PadLeft(2, '0'),
                VarName,
                (RepeatCount > 1) ? ("(" + RepeatIndex + ")") : "",
                Value);
        }

        public override void AddChild(AbstractNode child)
        {
            throw new InvalidOperationException(this.VarName + ": " + "Werteknoten können keine Kindknoten haben!");
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            try
            {
                ValueNode other = (ValueNode)obj;
                return
                    Redefines   == other.Redefines &&
                    Level       == other.Level &&
                    VarName     == other.VarName &&
                    Type        == other.Type &&
                    Length      == other.Length &&
                    RepeatCount == other.RepeatCount &&
                    Comment     == other.Comment &&
                    Value       == other.Value;
            }
            catch
            {
                return false;
            }
        }
    }
}
