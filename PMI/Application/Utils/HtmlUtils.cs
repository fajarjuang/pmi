using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using System.Text;
using System.Text.RegularExpressions;

namespace PMI.Application.Utils
{
    public static class HtmlUtils
    {
        // Original list courtesy of Robert Beal :
        // http://www.robertbeal.com/37/sanitising-html

        private static readonly Dictionary<string, string[]> ValidHtmlTags =
            new Dictionary<string, string[]>
        {
            {"p", new string[]          {"style", "class", "align"}},
            {"div", new string[]        {"style", "class", "align"}},
            {"span", new string[]       {"style", "class"}},
            {"br", new string[]         {"style", "class"}},
            {"hr", new string[]         {"style", "class"}},
            {"label", new string[]      {"style", "class"}},

            {"h1", new string[]         {"style", "class"}},
            {"h2", new string[]         {"style", "class"}},
            {"h3", new string[]         {"style", "class"}},
            {"h4", new string[]         {"style", "class"}},
            {"h5", new string[]         {"style", "class"}},
            {"h6", new string[]         {"style", "class"}},

            {"font", new string[]       {"style", "class", "color", "face", "size"}},
            {"strong", new string[]     {"style", "class"}},
            {"b", new string[]          {"style", "class"}},
            {"em", new string[]         {"style", "class"}},
            {"i", new string[]          {"style", "class"}},
            {"u", new string[]          {"style", "class"}},
            {"strike", new string[]     {"style", "class"}},
            {"ol", new string[]         {"style", "class"}},
            {"ul", new string[]         {"style", "class"}},
            {"li", new string[]         {"style", "class"}},
            {"blockquote", new string[] {"style", "class"}},
            {"code", new string[]       {"style", "class"}},

            {"a", new string[]          {"style", "class", "href", "title"}},
            {"img", new string[]        {"style", "class", "src", "height", "width",
                "alt", "title", "hspace", "vspace", "border"}},

            {"table", new string[]      {"style", "class"}},
            {"thead", new string[]      {"style", "class"}},
            {"tbody", new string[]      {"style", "class"}},
            {"tfoot", new string[]      {"style", "class"}},
            {"th", new string[]         {"style", "class", "scope"}},
            {"tr", new string[]         {"style", "class"}},
            {"td", new string[]         {"style", "class", "colspan"}},

            {"q", new string[]          {"style", "class", "cite"}},
            {"cite", new string[]       {"style", "class"}},
            {"abbr", new string[]       {"style", "class"}},
            {"acronym", new string[]    {"style", "class"}},
            {"del", new string[]        {"style", "class"}},
            {"ins", new string[]        {"style", "class"}}
        };

        // declare an array to mark which characters are to be encoded.
        private static string[] encodedCharacters = new string[256];

        /// <summary>
        /// Takes raw HTML input and cleans against a whitelist
        /// </summary>
        /// <param name="source">Html source</param>
        /// <returns>Clean output</returns>
        public static string SanitizeHtml(string source)
        {
            HtmlDocument html = GetHtml(source);
            if (html == null) return String.Empty;

            // All the nodes
            HtmlNode allNodes = html.DocumentNode;

            // Select whitelist tag names
            string[] whitelist = (from kv in ValidHtmlTags
                                  select kv.Key).ToArray();

            // Scrub tags not in whitelist
            CleanNodes(allNodes, whitelist);

            // Filter the attributes of the remaining
            foreach (KeyValuePair<string, string[]> tag in ValidHtmlTags)
            {
                IEnumerable<HtmlNode> nodes = (from n in allNodes.DescendantsAndSelf()
                                               where n.Name == tag.Key
                                               select n);

                if (nodes == null) continue;

                foreach (var n in nodes)
                {
                    if (!n.HasAttributes) continue;

                    // Get all the allowed attributes for this tag
                    HtmlAttribute[] attr = n.Attributes.ToArray();
                    foreach (HtmlAttribute a in attr)
                    {
                        if (!tag.Value.Contains(a.Name))
                        {
                            a.Remove(); // Wasn't in the list
                        }
                        else
                        {
                            CleanAttributeValues(a);
                        }
                    }
                }
            }

            return allNodes.InnerHtml;
        }

        /// <summary>
        /// Takes a raw source and removes all HTML tags
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string StripHtml(string source)
        {
            source = SanitizeHtml(source);

            // No need to continue if we have no clean Html
            if (String.IsNullOrEmpty(source))
                return String.Empty;

            HtmlDocument html = GetHtml(source);
            StringBuilder result = new StringBuilder();

            // For each node, extract only the innerText
            foreach (HtmlNode node in html.DocumentNode.ChildNodes)
                result.Append(node.InnerText);

            return result.ToString();
        }

        /// <summary>
        /// Recursively delete nodes not in the whitelist
        /// </summary>
        private static void CleanNodes(HtmlNode node, string[] whitelist)
        {
            if (node.NodeType == HtmlNodeType.Element)
            {
                if (!whitelist.Contains(node.Name))
                {
                    node.ParentNode.RemoveChild(node);
                    return; // We're done
                }
            }

            if (node.HasChildNodes)
                CleanChildren(node, whitelist);
        }

        /// <summary>
        /// Apply CleanNodes to each of the child nodes
        /// </summary>
        private static void CleanChildren(HtmlNode parent, string[] whitelist)
        {
            for (int i = parent.ChildNodes.Count - 1; i >= 0; i--)
                CleanNodes(parent.ChildNodes[i], whitelist);
        }

        /// <summary>
        /// This removes the vulnerable keywords and make values safe by html encoding and html character escaping.
        /// </summary>        
        /// <param name="attribute">Attribute that contain values that need to check and clean.</param>
        private static void CleanAttributeValues(HtmlAttribute attribute)
        {

            attribute.Value = HttpUtility.HtmlEncode(attribute.Value);

            attribute.Value = Regex.Replace(attribute.Value, @"\s*j\s*a\s*v\s*a\s*s\s*c\s*r\s*i\s*p\s*t\s*", "", RegexOptions.IgnoreCase);
            attribute.Value = Regex.Replace(attribute.Value, @"\s*s\s*c\s*r\s*i\s*p\s*t\s*", "", RegexOptions.IgnoreCase);

            if (attribute.Name.ToLower() == "style")
            {
                attribute.Value = Regex.Replace(attribute.Value, @"\s*e\s*x\s*p\s*r\s*e\s*s\s*s\s*i\s*o\s*n\s*", "", RegexOptions.IgnoreCase);
                attribute.Value = Regex.Replace(attribute.Value, @"\s*b\s*e\s*h\s*a\s*v\s*i\s*o\s*r\s*", "", RegexOptions.IgnoreCase);
            }

            if (attribute.Name.ToLower() == "href" || attribute.Name.ToLower() == "src")
            {
                //if (!attribute.Value.StartsWith("http://") || attribute.Value.StartsWith("/"))
                //    attribute.Value = "";
                attribute.Value = Regex.Replace(attribute.Value, @"\s*m\s*o\s*c\s*h\s*a\s*", "", RegexOptions.IgnoreCase);
            }

            // HtmlEntity Escape
            StringBuilder sbAttriuteValue = new StringBuilder();
            foreach (char c in attribute.Value.ToCharArray())
            {
                sbAttriuteValue.Append(EncodeCharacterToHtmlEntityEscape(c));
            }

            attribute.Value = sbAttriuteValue.ToString();

        }

        /// <summary>
        /// To encode html attribute characters to hex format except alphanumeric characters. 
        /// </summary>
        /// <param name="c">Character from the attribute value</param>
        /// <returns>Hex formatted string.</returns>
        private static string EncodeCharacterToHtmlEntityEscape(char c)
        {
            string hex;
            // check for alphnumeric characters
            if (c < 0xFF)
            {
                hex = encodedCharacters[c];
                if (hex == null)
                    return "" + c;
            }
            else
                hex = ((int)(c)).ToString("X2");

            // check for illegal characters
            if ((c <= 0x1f && c != '\t' && c != '\n' && c != '\r') || (c >= 0x7f && c <= 0x9f))
            {
                hex = "fffd"; // Let's entity encode this instead of returning it
            }

            return "&#x" + hex + ";";
        }

        /// <summary>
        /// Helper function that returns an HTML document from text
        /// </summary>
        private static HtmlDocument GetHtml(string source)
        {
            HtmlDocument html = new HtmlDocument();
            html.OptionFixNestedTags = true;
            html.OptionAutoCloseOnEnd = true;
            html.OptionDefaultStreamEncoding = Encoding.UTF8;

            html.LoadHtml(source);

            return html;
        }
    }
}