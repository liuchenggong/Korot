using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                List<ItemX> ItemList = new List<ItemX>();
                XmlDocument document = new XmlDocument();
                document.LoadXml(Properties.Resources._0700);
                foreach (XmlNode node in document.DocumentElement.ChildNodes)
                {
                    ItemX itemx = new ItemX();
                    itemx.Name = node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\"");
                    itemx.Number = GetLineNumber(Properties.Resources._0610, node.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\""), StringComparison.CurrentCultureIgnoreCase) - 1;
                    ItemList.Add(itemx);
                }
                foreach(ItemX item in ItemList)
                {
                    Console.WriteLine(" [ITEM N:\"" + item.Name + "\" ID:\"" + item.Number + "\" /]");
                }
                Console.ReadLine();
                Console.Clear();
                string result = Properties.Resources.replaceme;
                foreach (ItemX item in ItemList)
                {
                    result = result.Replace("= " + item.Number.ToString() + ";", "= Settings.LanguageSystem.GetItemText(\"" + item.Name + "\");");
                }
                if (result != Properties.Resources.replaceme)
                {
                    Console.WriteLine(result);
                }else
                {
                    Console.WriteLine("Operation ended with no changes.");
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static int GetLineNumber(string text, string lineToFind, StringComparison comparison = StringComparison.CurrentCulture)
        {
            if (lineToFind == "0.7.0.0") { return 0; }
            int lineNum = 0;
            using (StringReader reader = new StringReader(text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineNum++;
                    if (string.Equals(line.TrimEnd(),lineToFind.TrimEnd(), comparison))
                    {
                        return lineNum;
                    }
                }
            }
            throw new ArgumentNullException("Cannot find \"" + lineToFind + "\".");
        }
    }
    class ItemX
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }
}
