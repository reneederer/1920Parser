using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _1920Parser
{
    class Schema
    {
        private string pattern = @"^\s*(?<redefines>R?)(?<level>\d+)\s+(?<varName>\S+)((\s+(?<type>[xcnpXCNP])\s+((?<length>\d+))(\,(?<decimalPlaces>\d+))?)?(\s+(?<repeatCount>\d+))?)?(\s{2,}(?<comment>.*))?$";

        private string schemaStr = "";

        public Schema()
        {
        }

        public AbstractNode ParseLine(string line)
        {
            try
            {
                Match m = Regex.Match(line, pattern, RegexOptions.IgnoreCase);
                if (m.Groups["level"].Value == "" || m.Groups["varName"].Value == "") { return null; }

                int repeatCount = (m.Groups["repeatCount"].Value == "") ? 1 : int.Parse(m.Groups["repeatCount"].Value);

                if (m.Groups["type"].Value == "")
                {
                    return new GroupNode(
                        redefines: (m.Groups["redefines"].Value.ToUpper() == "R"),
                        level: int.Parse(m.Groups["level"].Value),
                        varName: m.Groups["varName"].Value,
                        repeatCount: (m.Groups["repeatCount"].Value == "") ? 1 : int.Parse(m.Groups["repeatCount"].Value),
                        repeatIndex: 1,
                        comment: m.Groups["comment"].Value);
                }
                else
                {
                    return new ValueNode(
                        redefines: (m.Groups["redefines"].Value == "R"),
                        level: int.Parse(m.Groups["level"].Value),
                        varName: m.Groups["varName"].Value,
                        type: m.Groups["type"].Value,
                        length: int.Parse(m.Groups["length"].Value),
                        repeatCount: (m.Groups["repeatCount"].Value == "") ? 1 : int.Parse(m.Groups["repeatCount"].Value),
                        repeatIndex: 1,
                        comment: m.Groups["comment"].Value);
                }
            }
            catch
            {
                return null;
            }
        }

        public GroupNode Parse()
        {
            var stack = new Stack<AbstractNode>();
            var root = new GroupNode(false, 0, "root", 1, 1, "");
            stack.Push(root);

            var schemaLines = schemaStr.Split(new string[]{"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            foreach(var currentLine in schemaLines)
            {
                var currentNode = ParseLine(currentLine);
                if (currentNode == null) { continue; }
                while(currentNode.Level <= stack.Peek().Level)
                {
                    var child = stack.Pop();
                    var parent = stack.Peek();
                    parent.AddChild(child);
                    for (int currentRepeatIndex = 2; currentRepeatIndex <= child.RepeatCount; ++currentRepeatIndex)
                    {
                        parent.AddChild(child.CreateCopyWithIndex(currentRepeatIndex));
                    }
                }
                stack.Push(currentNode);
            }
            while(stack.Count >= 2)
            {
                var child = stack.Pop();
                var parent = stack.Peek();
                parent.AddChild(child);
                for (int currentRepeatIndex = 2; currentRepeatIndex <= child.RepeatCount; ++currentRepeatIndex)
                {
                    parent.AddChild(child.CreateCopyWithIndex(currentRepeatIndex));
                }
            }
            return root;
        }
    }
}
