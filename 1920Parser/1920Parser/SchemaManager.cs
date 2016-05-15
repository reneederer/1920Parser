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
    class SchemaManager
    {
        /// <summary>
        /// The schema directory
        /// </summary>
        private string schemaDirectory = @"schemas\";
        /// <summary>
        /// The schema configuration
        /// </summary>
        private schemas schemaConfig;
        /// <summary>
        /// The schema string
        /// </summary>
        private string schemaStr = "";
        /// <summary>
        /// The pattern
        /// </summary>
        /// <summary>
        /// Sets the schema file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the schema file names.
        /// </summary>
        /// <returns></returns>
        public string[] getSchemaFileNames()
        {
            Directory.CreateDirectory(schemaDirectory);
            return Directory.GetFiles(schemaDirectory).Select((x) => Path.GetFileName(x)).ToArray();
        }

        /// <summary>
        /// Finds the schema.
        /// </summary>
        /// <param name="dataStream">The data stream.</param>
        /// <returns></returns>
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


        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        public SchemaManager()
        {
        }

        /// <summary>
        /// Sets the schema configuration.
        /// </summary>
        /// <param name="configFile">The configuration file.</param>
        /// <exception cref="System.Exception"></exception>
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


        /// <summary>
        /// Adds the schema.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="name">The name.</param>
        /// <param name="identifierStart">The identifier start.</param>
        /// <param name="value">The value.</param>
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
            schemaConfig.schema = schemaConfig.schema.Concat(new List<schemasSchema>() { schemasSchema }).ToArray();
            var serializer = new XmlSerializer(typeof(schemas));
            serializer.Serialize(XmlWriter.Create("schemas.xml"), schemaConfig);
        }
    }
}
