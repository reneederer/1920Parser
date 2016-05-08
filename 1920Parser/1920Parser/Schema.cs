using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace _1920Parser
{
    class Schema
    {
        private string schemaDirectory = @"schemas\";
        private schemas schemaConfig;
        private string schemaStr = "";
        private string pattern = @"^\s*(?<redefines>R?)(?<level>\d+)\s+(?<varName>\S+)((\s+(?<type>[xcnphvXCNPHV])\s+((?<length>\d+))(\,(?<decimalPlaces>\d+))?)?(\s+(?<repeatCount>\d+))?)?(\s{2,}(?<comment>.*))?$";

        public string setSchemaFile(string file)
        {
            try
            {
                schemaStr = File.ReadAllText("schemas\\" + file);
                return schemaStr;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string[] getSchemaFileNames()
        {
            Directory.CreateDirectory(schemaDirectory);
            return Directory.GetFiles(schemaDirectory).Select((x) => Path.GetFileName(x)).ToArray();
        }

        public string findSchema(string dataStream)
        {
            string re = null;
                var matchingSchemas = schemaConfig.schema
                    .Where(
                        (x) => 
                        {
                            try
                            {
                                return x.identifiers.All((idf) => dataStream.Substring(idf.start - 1, idf.value.Length) == idf.value);
                            }
                            catch
                            {
                                return false;
                            }
                        }
                        );
                if (matchingSchemas.Count() > 0)
                {
                    try
                    {
                        var lastMatchWithMaxIdentifiers = matchingSchemas.OrderBy(x => x.identifiers.Length).Where(x => x.identifiers.Length == matchingSchemas.Max(y => y.identifiers.Length)).Last();
                        schemaStr = File.ReadAllText("schemas\\" + lastMatchWithMaxIdentifiers.name);
                        re = lastMatchWithMaxIdentifiers.name;
                    }
                    catch
                    {
                        return null;
                    }
                }
                return re;
        }


        public Schema()
        {
            schemaConfig = new schemas();
            schemaConfig.schema = new schemasSchema[0];
        }

        public void setSchemaConfig(string configFile)
        {
            if (!File.Exists(configFile) || File.ReadAllText(configFile) == "")
            {
                return;
            }
            try
            {
                var deserializer = new XmlSerializer(typeof(schemas));
                using (var reader = XmlReader.Create(configFile))
                {
                    schemaConfig = (schemas)deserializer.Deserialize(reader);
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\nVorsicht, wenn das Programm in die Datei schreibt, wird der ganze Inhalt überschrieben!", e);
            }
        }


        public void addSchema(string schema, string name, int identifierStart, string value)
        {
            Directory.CreateDirectory("schemas");
            File.WriteAllText("schemas\\" + name, schema);
            schemasSchema schemasSchema = new schemasSchema();
            schemasSchema.name = name;
            schemasSchema.identifiers = new schemasSchemaIdentifier[1];
            schemasSchema.identifiers[0] = new schemasSchemaIdentifier();
            schemasSchema.identifiers[0].start = 1;
            //schemasSchema.identifiers[0].startSpecified = true;
            schemasSchema.identifiers[0].start = identifierStart;
            schemasSchema.identifiers[0].value = value;
            schemaConfig.schema = schemaConfig.schema.Concat(new List<schemasSchema>(){schemasSchema}).ToArray();
            var serializer = new XmlSerializer(typeof(schemas));
            serializer.Serialize(XmlWriter.Create("schemas.xml"), schemaConfig);
        }

        private AbstractNode ParseLine(string line)
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
                    if (m.Groups["type"].Value.ToUpper() == "H" || m.Groups["type"].Value.ToUpper() == "V") return null;
                    else
                    return new ValueNode(
                        redefines: (m.Groups["redefines"].Value == "R"),
                        level: int.Parse(m.Groups["level"].Value),
                        varName: m.Groups["varName"].Value,
                        type: m.Groups["type"].Value,
                        length: int.Parse(m.Groups["length"].Value),
                        repeatCount: m.Groups["repeatCount"].Value == "" ? 1 : int.Parse(m.Groups["repeatCount"].Value),
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

        public void setSchema(string p)
        {
            this.schemaStr = p;
        }
    }
}
