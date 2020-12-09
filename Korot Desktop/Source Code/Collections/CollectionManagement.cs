/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace Korot
{
    /// <summary>
    /// Main manager for collections.
    /// </summary>
    public class CollectionManager
    {
        public CollectionManager(string collectionXML)
        {
            Collections = new List<Collection>();
            readCollections(collectionXML, true);
        }

        public List<Collection> Collections { get; set; }

        public bool readCollections(string collections, bool clearCurrent = true)
        {
            // Clear the current list of collections.
            if (clearCurrent)
            {
                Collections.Clear();
            }
            XmlDocument document = new XmlDocument();
            if (string.IsNullOrWhiteSpace(collections) || collections.ToLowerInvariant().Replace(Environment.NewLine, "") == "<collections></collections>")
            {
                return true;
            }
            document.LoadXml(collections); //Loads our XML Stream

            // This is the part where fun begins.
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                if (node.Name.ToLowerInvariant() == "collection") //Top must always only have Collections. This ain't favorites.
                {
                    Collection newCol = new Collection(); // New Collection to add
                    // Collection - ID
                    if (node.Attributes["ID"] == null)
                    {
                        newCol.ID = HTAlt.Tools.GenerateRandomText(12);
                    }
                    else
                    {
                        newCol.ID = node.Attributes["ID"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                    // Collection - Text
                    if (node.Attributes["Text"] == null)
                    {
                        newCol.Text = newCol.ID;
                    }
                    else
                    {
                        newCol.Text = node.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                    // Items
                    foreach (XmlNode subnode in node.ChildNodes)
                    {
                        if (subnode.Name.ToLowerInvariant() == "text") //TextItem
                        {
                            TextItem item = new TextItem(); //New Item
                            // Item - ID
                            if (subnode.Attributes["ID"] == null)
                            {
                                item.ID = HTAlt.Tools.GenerateRandomText(12);
                            }
                            else
                            {
                                item.ID = subnode.Attributes["ID"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                            }
                            // Item - BackColor
                            if (subnode.Attributes["BackColor"] == null)
                            {
                                item.BackColor = Color.Transparent;
                            }
                            else
                            {
                                item.BackColor = HTAlt.Tools.HexToColor(subnode.Attributes["BackColor"].Value);
                            }
                            // TextItem - ForeColor
                            if (subnode.Attributes["ForeColor"] == null)
                            {
                                item.ForeColor = Color.Empty;
                            }
                            else
                            {
                                item.ForeColor = HTAlt.Tools.HexToColor(subnode.Attributes["ForeColor"].Value);
                            }
                            // TextItem - Text
                            if (subnode.Attributes["Text"] == null)
                            {
                                item.Text = item.ID;
                            }
                            else
                            {
                                item.Text = subnode.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                            }
                            // TextItem - Font
                            if (subnode.Attributes["Font"] == null)
                            {
                                item.Font = "Ubuntu";
                            }
                            else
                            {
                                item.Font = subnode.Attributes["Font"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                            }
                            // TextItem - FontSize
                            if (subnode.Attributes["FontSize"] == null)
                            {
                                item.FontSize = 10;
                            }
                            else
                            {
                                item.FontSize = Convert.ToInt32(subnode.Attributes["FontSize"].Value);
                            }
                            // TextItem - FontProperties
                            if (subnode.Attributes["FontProperties"] == null)
                            {
                                item.FontProperties = FontType.Regular;
                            }
                            else
                            {
                                if (subnode.Attributes["FontProperties"].Value == "Regular")
                                {
                                    item.FontProperties = FontType.Regular;
                                }
                                else if (subnode.Attributes["FontProperties"].Value == "Bold")
                                {
                                    item.FontProperties = FontType.Bold;
                                }
                                else if (subnode.Attributes["FontProperties"].Value == "Italic")
                                {
                                    item.FontProperties = FontType.Italic;
                                }
                                else if (subnode.Attributes["FontProperties"].Value == "Underline")
                                {
                                    item.FontProperties = FontType.Underline;
                                }
                                else if (subnode.Attributes["FontProperties"].Value == "Strikeout")
                                {
                                    item.FontProperties = FontType.Strikeout;
                                }
                            }
                            newCol.CollectionItems.Add(item); //Final touch
                        }
                        else if (subnode.Name.ToLowerInvariant() == "link") //LinkItem
                        {
                            LinkItem item = new LinkItem(); //New Item
                            // Item - ID
                            if (subnode.Attributes["ID"] == null)
                            {
                                item.ID = HTAlt.Tools.GenerateRandomText(12);
                            }
                            else
                            {
                                item.ID = subnode.Attributes["ID"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                            }
                            // Item - BackColor
                            if (subnode.Attributes["BackColor"] == null)
                            {
                                item.BackColor = Color.Transparent;
                            }
                            else
                            {
                                item.BackColor = HTAlt.Tools.HexToColor(subnode.Attributes["BackColor"].Value);
                            }
                            // LinkItem - ForeColor
                            if (subnode.Attributes["ForeColor"] == null)
                            {
                                item.ForeColor = Color.Empty;
                            }
                            else
                            {
                                item.ForeColor = HTAlt.Tools.HexToColor(subnode.Attributes["ForeColor"].Value);
                            }
                            // LinkItem - Text
                            if (subnode.Attributes["Text"] == null)
                            {
                                item.Text = item.ID;
                            }
                            else
                            {
                                item.Text = subnode.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                            }
                            // LinkItem - Font
                            if (subnode.Attributes["Font"] == null)
                            {
                                item.Font = "Ubuntu";
                            }
                            else
                            {
                                item.Font = subnode.Attributes["Font"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                            }
                            // LinkItem - Source
                            if (subnode.Attributes["Source"] == null)
                            {
                                item.Source = "korot://empty";
                            }
                            else
                            {
                                item.Source = subnode.Attributes["Source"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                            }
                            // LinkItem - FontSize
                            if (subnode.Attributes["FontSize"] == null)
                            {
                                item.FontSize = 10;
                            }
                            else
                            {
                                item.FontSize = Convert.ToInt32(subnode.Attributes["FontSize"].Value);
                            }
                            // LinkItem - FontProperties
                            if (subnode.Attributes["FontProperties"] == null)
                            {
                                item.FontProperties = FontType.Regular;
                            }
                            else
                            {
                                if (subnode.Attributes["FontProperties"].Value == "Regular")
                                {
                                    item.FontProperties = FontType.Regular;
                                }
                                else if (subnode.Attributes["FontProperties"].Value == "Bold")
                                {
                                    item.FontProperties = FontType.Bold;
                                }
                                else if (subnode.Attributes["FontProperties"].Value == "Italic")
                                {
                                    item.FontProperties = FontType.Italic;
                                }
                                else if (subnode.Attributes["FontProperties"].Value == "Underline")
                                {
                                    item.FontProperties = FontType.Underline;
                                }
                                else if (subnode.Attributes["FontProperties"].Value == "Strikeout")
                                {
                                    item.FontProperties = FontType.Strikeout;
                                }
                            }
                            newCol.CollectionItems.Add(item); //Final touch
                        }
                        else if (subnode.Name.ToLowerInvariant() == "image") //ImageItem
                        {
                            ImageItem item = new ImageItem(); //New Item
                            // Item - ID
                            if (subnode.Attributes["ID"] == null)
                            {
                                item.ID = HTAlt.Tools.GenerateRandomText(12);
                            }
                            else
                            {
                                item.ID = subnode.Attributes["ID"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                            }
                            // Item - BackColor
                            if (subnode.Attributes["BackColor"] == null)
                            {
                                item.BackColor = Color.Transparent;
                            }
                            else
                            {
                                item.BackColor = HTAlt.Tools.HexToColor(subnode.Attributes["BackColor"].Value);
                            }
                            // ImageItem - Source
                            if (subnode.Attributes["Source"] == null)
                            {
                                item.Source = "korot://empty";
                            }
                            else
                            {
                                item.Source = subnode.Attributes["Source"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                            }
                            // ImageItem - Width
                            if (subnode.Attributes["Width"] == null)
                            {
                                item.Width = 100;
                            }
                            else
                            {
                                item.Width = Convert.ToInt32(subnode.Attributes["Width"].Value);
                            }
                            // ImageItem - Height
                            if (subnode.Attributes["Height"] == null)
                            {
                                item.Height = 100;
                            }
                            else
                            {
                                item.Height = Convert.ToInt32(subnode.Attributes["Height"].Value);
                            }
                            newCol.CollectionItems.Add(item); //Final touch
                        }
                    }
                    //Final touch
                    Collections.Add(newCol);
                }
            }
            //End
            return true;
        }

        /// <summary>
        /// Gets current collections list.
        /// </summary>
        /// <returns>XML code</returns>
        public string writeCollections
        {
            get
            {
                string xmlCode = "   <Collections>" + Environment.NewLine; //Starting point
                foreach (Collection x in Collections)
                {
                    xmlCode += "     " + x.outXML + Environment.NewLine;
                }
                xmlCode += "   </Collections>" + Environment.NewLine;
                //End
                return xmlCode;
            }
        }

        /// <summary>
        /// Gets collections from specific ID.
        /// </summary>
        /// <param name="id">ID to search.</param>
        /// <returns></returns>
        public Collection GetCollectionFromID(string id)
        {
            List<Collection> foundCollections = new List<Collection>();
            foreach (Collection x in Collections)
            {
                if (x.ID == id)
                {
                    foundCollections.Add(x);
                }
            }
            if (foundCollections.Count > 0)
            {
                return foundCollections[0];
            }
            else
            {
                return null;
            }
        }

        public CollectionItem GetItemFromID(string id)
        {
            List<Collection> foundCollections = new List<Collection>();
            List<CollectionItem> foundItems = new List<CollectionItem>();
            foreach (Collection x in Collections)
            {
                foreach (CollectionItem y in x.CollectionItems)
                {
                    if (y.ID == id)
                    {
                        foundCollections.Add(x);
                        foundItems.Add(y);
                    }
                }
            }
            if (foundItems.Count > 0 && foundCollections.Count > 0)
            {
                return foundCollections[0].GetItemFromID(foundItems[0].ID);
            }
            else
            {
                return null;
            }
        }
    }

    #region "Structures"

    /// <summary>
    /// Main structure that includes collection items.
    /// </summary>
    public class Collection
    {
        public Collection()
        {
            CollectionItems = new List<CollectionItem>();
        }

        public Collection(string xmlCode)
        {
            CollectionItems = new List<CollectionItem>();
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlCode); //Loads our XML Stream
            ID = document.FirstChild.Attributes["ID"] == null ? HTAlt.Tools.GenerateRandomText(12) : document.FirstChild.Attributes["ID"].Value;
            Text = document.FirstChild.Attributes["Text"] == null ? ID : document.FirstChild.Attributes["Text"].Value;
            foreach (XmlNode subnode in document.FirstChild.ChildNodes)
            {
                if (subnode.Name.ToLowerInvariant() == "text") //TextItem
                {
                    TextItem item = new TextItem(); //New Item
                                                    // Item - ID
                    if (subnode.Attributes["ID"] == null)
                    {
                        item.ID = HTAlt.Tools.GenerateRandomText(12);
                    }
                    else
                    {
                        item.ID = subnode.Attributes["ID"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                    // Item - BackColor
                    if (subnode.Attributes["BackColor"] == null)
                    {
                        item.BackColor = Color.Transparent;
                    }
                    else
                    {
                        item.BackColor = HTAlt.Tools.HexToColor(subnode.Attributes["BackColor"].Value);
                    }
                    // TextItem - ForeColor
                    if (subnode.Attributes["ForeColor"] == null)
                    {
                        item.ForeColor = Color.Empty;
                    }
                    else
                    {
                        item.ForeColor = HTAlt.Tools.HexToColor(subnode.Attributes["ForeColor"].Value);
                    }
                    // TextItem - Text
                    if (subnode.Attributes["Text"] == null)
                    {
                        item.Text = item.ID;
                    }
                    else
                    {
                        item.Text = subnode.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                    // TextItem - Font
                    if (subnode.Attributes["Font"] == null)
                    {
                        item.Font = "Ubuntu";
                    }
                    else
                    {
                        item.Font = subnode.Attributes["Font"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                    // TextItem - FontSize
                    if (subnode.Attributes["FontSize"] == null)
                    {
                        item.FontSize = 10;
                    }
                    else
                    {
                        item.FontSize = Convert.ToInt32(subnode.Attributes["FontSize"].Value);
                    }
                    // TextItem - FontProperties
                    if (subnode.Attributes["FontProperties"] == null)
                    {
                        item.FontProperties = FontType.Regular;
                    }
                    else
                    {
                        if (subnode.Attributes["FontProperties"].Value == "Regular")
                        {
                            item.FontProperties = FontType.Regular;
                        }
                        else if (subnode.Attributes["FontProperties"].Value == "Bold")
                        {
                            item.FontProperties = FontType.Bold;
                        }
                        else if (subnode.Attributes["FontProperties"].Value == "Italic")
                        {
                            item.FontProperties = FontType.Italic;
                        }
                        else if (subnode.Attributes["FontProperties"].Value == "Underline")
                        {
                            item.FontProperties = FontType.Underline;
                        }
                        else if (subnode.Attributes["FontProperties"].Value == "Strikeout")
                        {
                            item.FontProperties = FontType.Strikeout;
                        }
                    }
                    CollectionItems.Add(item); //Final touch
                }
                else if (subnode.Name.ToLowerInvariant() == "link") //LinkItem
                {
                    LinkItem item = new LinkItem(); //New Item
                                                    // Item - ID
                    if (subnode.Attributes["ID"] == null)
                    {
                        item.ID = HTAlt.Tools.GenerateRandomText(12);
                    }
                    else
                    {
                        item.ID = subnode.Attributes["ID"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                    // Item - BackColor
                    if (subnode.Attributes["BackColor"] == null)
                    {
                        item.BackColor = Color.Transparent;
                    }
                    else
                    {
                        item.BackColor = HTAlt.Tools.HexToColor(subnode.Attributes["BackColor"].Value);
                    }
                    // LinkItem - ForeColor
                    if (subnode.Attributes["ForeColor"] == null)
                    {
                        item.ForeColor = Color.Empty;
                    }
                    else
                    {
                        item.ForeColor = HTAlt.Tools.HexToColor(subnode.Attributes["ForeColor"].Value);
                    }
                    // LinkItem - Text
                    if (subnode.Attributes["Text"] == null)
                    {
                        item.Text = item.ID;
                    }
                    else
                    {
                        item.Text = subnode.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                    // LinkItem - Font
                    if (subnode.Attributes["Font"] == null)
                    {
                        item.Font = "Ubuntu";
                    }
                    else
                    {
                        item.Font = subnode.Attributes["Font"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                    // LinkItem - Source
                    if (subnode.Attributes["Source"] == null)
                    {
                        item.Source = "korot://empty";
                    }
                    else
                    {
                        item.Source = subnode.Attributes["Source"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                    // LinkItem - FontSize
                    if (subnode.Attributes["FontSize"] == null)
                    {
                        item.FontSize = 10;
                    }
                    else
                    {
                        item.FontSize = Convert.ToInt32(subnode.Attributes["FontSize"].Value);
                    }
                    // LinkItem - FontProperties
                    if (subnode.Attributes["FontProperties"] == null)
                    {
                        item.FontProperties = FontType.Regular;
                    }
                    else
                    {
                        if (subnode.Attributes["FontProperties"].Value == "Regular")
                        {
                            item.FontProperties = FontType.Regular;
                        }
                        else if (subnode.Attributes["FontProperties"].Value == "Bold")
                        {
                            item.FontProperties = FontType.Bold;
                        }
                        else if (subnode.Attributes["FontProperties"].Value == "Italic")
                        {
                            item.FontProperties = FontType.Italic;
                        }
                        else if (subnode.Attributes["FontProperties"].Value == "Underline")
                        {
                            item.FontProperties = FontType.Underline;
                        }
                        else if (subnode.Attributes["FontProperties"].Value == "Strikeout")
                        {
                            item.FontProperties = FontType.Strikeout;
                        }
                    }
                    CollectionItems.Add(item); //Final touch
                }
                else if (subnode.Name.ToLowerInvariant() == "image") //ImageItem
                {
                    ImageItem item = new ImageItem(); //New Item
                                                      // Item - ID
                    if (subnode.Attributes["ID"] == null)
                    {
                        item.ID = HTAlt.Tools.GenerateRandomText(12);
                    }
                    else
                    {
                        item.ID = subnode.Attributes["ID"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                    // Item - BackColor
                    if (subnode.Attributes["BackColor"] == null)
                    {
                        item.BackColor = Color.Transparent;
                    }
                    else
                    {
                        item.BackColor = HTAlt.Tools.HexToColor(subnode.Attributes["BackColor"].Value);
                    }
                    // ImageItem - Source
                    if (subnode.Attributes["Source"] == null)
                    {
                        item.Source = "korot://empty";
                    }
                    else
                    {
                        item.Source = subnode.Attributes["Source"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                    // ImageItem - Width
                    if (subnode.Attributes["Width"] == null)
                    {
                        item.Width = 100;
                    }
                    else
                    {
                        item.Width = Convert.ToInt32(subnode.Attributes["Width"].Value);
                    }
                    // ImageItem - Height
                    if (subnode.Attributes["Height"] == null)
                    {
                        item.Height = 100;
                    }
                    else
                    {
                        item.Height = Convert.ToInt32(subnode.Attributes["Height"].Value);
                    }
                    CollectionItems.Add(item); //Final touch
                }
            }
        }

        public bool NewItemFromCode(string xmlCode)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlCode);
            XmlElement subnode = doc.DocumentElement;
            if (subnode.Name.ToLowerInvariant() == "text") //TextItem
            {
                TextItem item = new TextItem(); //New Item
                                                // Item - ID
                if (subnode.Attributes["ID"] == null)
                {
                    item.ID = HTAlt.Tools.GenerateRandomText(12);
                }
                else
                {
                    item.ID = subnode.Attributes["ID"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                }
                // Item - BackColor
                if (subnode.Attributes["BackColor"] == null)
                {
                    item.BackColor = Color.Transparent;
                }
                else
                {
                    item.BackColor = HTAlt.Tools.HexToColor(subnode.Attributes["BackColor"].Value);
                }
                // TextItem - ForeColor
                if (subnode.Attributes["ForeColor"] == null)
                {
                    item.ForeColor = Color.Empty;
                }
                else
                {
                    item.ForeColor = HTAlt.Tools.HexToColor(subnode.Attributes["ForeColor"].Value);
                }
                // TextItem - Text
                if (subnode.Attributes["Text"] == null)
                {
                    item.Text = item.ID;
                }
                else
                {
                    item.Text = subnode.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                }
                // TextItem - Font
                if (subnode.Attributes["Font"] == null)
                {
                    item.Font = "Ubuntu";
                }
                else
                {
                    item.Font = subnode.Attributes["Font"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                }
                // TextItem - FontSize
                if (subnode.Attributes["FontSize"] == null)
                {
                    item.FontSize = 10;
                }
                else
                {
                    item.FontSize = Convert.ToInt32(subnode.Attributes["FontSize"].Value);
                }
                // TextItem - FontProperties
                if (subnode.Attributes["FontProperties"] == null)
                {
                    item.FontProperties = FontType.Regular;
                }
                else
                {
                    if (subnode.Attributes["FontProperties"].Value == "Regular")
                    {
                        item.FontProperties = FontType.Regular;
                    }
                    else if (subnode.Attributes["FontProperties"].Value == "Bold")
                    {
                        item.FontProperties = FontType.Bold;
                    }
                    else if (subnode.Attributes["FontProperties"].Value == "Italic")
                    {
                        item.FontProperties = FontType.Italic;
                    }
                    else if (subnode.Attributes["FontProperties"].Value == "Underline")
                    {
                        item.FontProperties = FontType.Underline;
                    }
                    else if (subnode.Attributes["FontProperties"].Value == "Strikeout")
                    {
                        item.FontProperties = FontType.Strikeout;
                    }
                }
                CollectionItems.Add(item); //Final touch
            }
            else if (subnode.Name.ToLowerInvariant() == "link") //LinkItem
            {
                LinkItem item = new LinkItem(); //New Item
                                                // Item - ID
                if (subnode.Attributes["ID"] == null)
                {
                    item.ID = HTAlt.Tools.GenerateRandomText(12);
                }
                else
                {
                    item.ID = subnode.Attributes["ID"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                }
                // Item - BackColor
                if (subnode.Attributes["BackColor"] == null)
                {
                    item.BackColor = Color.Transparent;
                }
                else
                {
                    item.BackColor = HTAlt.Tools.HexToColor(subnode.Attributes["BackColor"].Value);
                }
                // LinkItem - ForeColor
                if (subnode.Attributes["ForeColor"] == null)
                {
                    item.ForeColor = Color.Empty;
                }
                else
                {
                    item.ForeColor = HTAlt.Tools.HexToColor(subnode.Attributes["ForeColor"].Value);
                }
                // LinkItem - Text
                if (subnode.Attributes["Text"] == null)
                {
                    item.Text = item.ID;
                }
                else
                {
                    item.Text = subnode.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                }
                // LinkItem - Font
                if (subnode.Attributes["Font"] == null)
                {
                    item.Font = "Ubuntu";
                }
                else
                {
                    item.Font = subnode.Attributes["Font"].Value;
                }
                // LinkItem - Source
                if (subnode.Attributes["Source"] == null)
                {
                    item.Source = "korot://empty";
                }
                else
                {
                    item.Source = subnode.Attributes["Source"].Value;
                }
                // LinkItem - FontSize
                if (subnode.Attributes["FontSize"] == null)
                {
                    item.FontSize = 10;
                }
                else
                {
                    item.FontSize = Convert.ToInt32(subnode.Attributes["FontSize"].Value);
                }
                // LinkItem - FontProperties
                if (subnode.Attributes["FontProperties"] == null)
                {
                    item.FontProperties = FontType.Regular;
                }
                else
                {
                    if (subnode.Attributes["FontProperties"].Value == "Regular")
                    {
                        item.FontProperties = FontType.Regular;
                    }
                    else if (subnode.Attributes["FontProperties"].Value == "Bold")
                    {
                        item.FontProperties = FontType.Bold;
                    }
                    else if (subnode.Attributes["FontProperties"].Value == "Italic")
                    {
                        item.FontProperties = FontType.Italic;
                    }
                    else if (subnode.Attributes["FontProperties"].Value == "Underline")
                    {
                        item.FontProperties = FontType.Underline;
                    }
                    else if (subnode.Attributes["FontProperties"].Value == "Strikeout")
                    {
                        item.FontProperties = FontType.Strikeout;
                    }
                }
                CollectionItems.Add(item); //Final touch
            }
            else if (subnode.Name.ToLowerInvariant() == "image") //ImageItem
            {
                ImageItem item = new ImageItem(); //New Item
                                                  // Item - ID
                if (subnode.Attributes["ID"] == null)
                {
                    item.ID = HTAlt.Tools.GenerateRandomText(12);
                }
                else
                {
                    item.ID = subnode.Attributes["ID"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                }
                // Item - BackColor
                if (subnode.Attributes["BackColor"] == null)
                {
                    item.BackColor = Color.Transparent;
                }
                else
                {
                    item.BackColor = HTAlt.Tools.HexToColor(subnode.Attributes["BackColor"].Value);
                }
                // ImageItem - Source
                if (subnode.Attributes["Source"] == null)
                {
                    item.Source = "korot://empty";
                }
                else
                {
                    item.Source = subnode.Attributes["Source"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                }
                // ImageItem - Width
                if (subnode.Attributes["Width"] == null)
                {
                    item.Width = 100;
                }
                else
                {
                    item.Width = Convert.ToInt32(subnode.Attributes["Width"].Value);
                }
                // ImageItem - Height
                if (subnode.Attributes["Height"] == null)
                {
                    item.Height = 100;
                }
                else
                {
                    item.Height = Convert.ToInt32(subnode.Attributes["Height"].Value);
                }
                CollectionItems.Add(item); //Final touch
            }
            return true;
        }

        /// <summary>
        /// Gets the XML code of this collection.
        /// </summary>
        public string outXML
        {
            get
            {
                string cCode = "<Collection";
                cCode += " ID=\"" + ID + "\" ";
                cCode += " Text=\"" + Text + "\" ";
                cCode += ">";
                foreach (CollectionItem item in CollectionItems)
                {
                    if (item is TextItem)
                    {
                        TextItem ti = (item as TextItem);
                        cCode += Environment.NewLine + ti.outXML;
                    }
                    else if (item is LinkItem)
                    {
                        LinkItem ti = (item as LinkItem);

                        cCode += Environment.NewLine + ti.outXML;
                    }
                    else if (item is ImageItem)
                    {
                        ImageItem ti = (item as ImageItem);
                        cCode += Environment.NewLine + ti.outXML;
                    }
                }
                cCode += Environment.NewLine + "</Collection>";
                return cCode;
            }
        }

        /// <summary>
        /// List of Collection items. These items can be Text, Image or Link.
        /// </summary>
        public List<CollectionItem> CollectionItems { get; set; }

        /// <summary>
        /// ID of collection.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Text to display.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets a collection item by a specific ID.
        /// </summary>
        /// <param name="id">ID to search.</param>
        /// <returns></returns>
        public CollectionItem GetItemFromID(string id)
        {
            List<CollectionItem> foundItems = new List<CollectionItem>();
            foreach (CollectionItem x in CollectionItems)
            {
                if (x.ID == id)
                {
                    foundItems.Add(x);
                }
            }
            if (foundItems.Count > 0)
            {
                return foundItems[0];
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// An collection item.
    /// </summary>
    public class CollectionItem
    {
        protected Color _backColor = Color.Transparent;

        /// <summary>
        /// Specific ID of the collection item.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Background color of the item.
        /// </summary>
        public Color BackColor
        {
            get => _backColor;
            set => _backColor = value;
        }
    }

    /// <summary>
    /// A colelction item which is just a raw text.
    /// </summary>
    public class TextItem : CollectionItem
    {
        protected Color _foreColor = Color.Empty;

        /// <summary>
        /// Text to display.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Font family name for displaying text.
        /// </summary>
        public string Font { get; set; }

        /// <summary>
        /// Size of the font for displaying text.
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// Font properties for displaying text.
        /// </summary>
        public FontType FontProperties { get; set; }

        /// <summary>
        /// Color of the displaying text.
        /// </summary>
        public Color ForeColor
        {
            get => _foreColor;
            set => _foreColor = value;
        }

        /// <summary>
        /// Gets the XML code of this item.
        /// </summary>
        public string outXML
        {
            get
            {
                string iCode = "<Text";
                iCode += " ID=\"" + ID.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" ";
                iCode += "BackColor=\"" + HTAlt.Tools.ColorToHex(BackColor) + "\" ";
                iCode += "ForeColor=\"" + HTAlt.Tools.ColorToHex(ForeColor) + "\" ";
                iCode += "Text=\"" + Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" ";
                iCode += "Font=\"" + Font.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" ";
                iCode += "FontSize=\"" + FontSize + "\" ";
                iCode += "FontProperties=\"";
                if (FontProperties == FontType.Regular)
                {
                    iCode += "Regular\" ";
                }
                else if (FontProperties == FontType.Bold)
                {
                    iCode += "Bold\" ";
                }
                else if (FontProperties == FontType.Italic)
                {
                    iCode += "Italic\" ";
                }
                else if (FontProperties == FontType.Underline)
                {
                    iCode += "Underline\" ";
                }
                else if (FontProperties == FontType.Strikeout)
                {
                    iCode += "Strikeout\" ";
                }
                iCode += "/>";
                return iCode;
            }
        }
    }

    /// <summary>
    /// A colelction item which is just a raw text with link.
    /// </summary>
    public class LinkItem : CollectionItem
    {
        protected Color _foreColor = Color.Empty;

        /// <summary>
        /// Text to display.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Source of the Link. This URL will be activated on clicked.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Font family name for displaying text.
        /// </summary>
        public string Font { get; set; }

        /// <summary>
        /// Size of the font for displaying text.
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// Font properties for displaying text.
        /// </summary>
        public FontType FontProperties { get; set; }

        /// <summary>
        /// Color of the displaying text.
        /// </summary>
        public Color ForeColor
        {
            get => _foreColor;
            set => _foreColor = value;
        }

        /// <summary>
        /// Gets the XML code of this item.
        /// </summary>
        public string outXML
        {
            get
            {
                string iCode = "<Link";
                iCode += " ID=\"" + ID.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" ";
                iCode += "BackColor=\"" + HTAlt.Tools.ColorToHex(BackColor) + "\" ";
                iCode += "ForeColor=\"" + HTAlt.Tools.ColorToHex(ForeColor) + "\" ";
                iCode += "Text=\"" + Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" ";
                iCode += "Source=\"" + Source.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" ";
                iCode += "Font=\"" + Font.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" ";
                iCode += "FontSize=\"" + FontSize + "\" ";
                iCode += "FontProperties=\"";
                if (FontProperties == FontType.Regular)
                {
                    iCode += "Regular\" ";
                }
                else if (FontProperties == FontType.Bold)
                {
                    iCode += "Bold\" ";
                }
                else if (FontProperties == FontType.Italic)
                {
                    iCode += "Italic\" ";
                }
                else if (FontProperties == FontType.Underline)
                {
                    iCode += "Underline\" ";
                }
                else if (FontProperties == FontType.Strikeout)
                {
                    iCode += "Strikeout\" ";
                }
                iCode += "/>";
                return iCode;
            }
        }
    }

    /// <summary>
    /// A colelction item which is just an image.
    /// </summary>
    public class ImageItem : CollectionItem
    {
        /// <summary>
        /// Source of the image.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Width of image.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of image.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets the XML code of this item.
        /// </summary>
        public string outXML
        {
            get
            {
                string iCode = "<Image";
                iCode += " ID=\"" + ID.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" ";
                iCode += "BackColor=\"" + HTAlt.Tools.ColorToHex(BackColor) + "\" ";
                iCode += "Source=\"" + Source.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" ";
                iCode += "Width=\"" + Width + "\" ";
                iCode += "Height=\"" + Height + "\" ";
                iCode += "/>";
                return iCode;
            }
        }
    }

    /// <summary>
    /// FontProperties for Text-based colelciton items such as TextItem, LinkItem etc.
    /// </summary>
    public enum FontType
    {
        Regular = 0,
        Bold = 1,
        Italic = 2,
        Underline = 3,
        Strikeout = 4,
    };

    #endregion "Structures"
}