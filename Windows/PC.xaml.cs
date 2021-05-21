using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using PokemonRPG.Configs;
using PokemonRPG.Windows;

namespace PokemonRPG.Windows
{
    /// <summary>
    /// Interaction logic for PC.xaml
    /// </summary>
    public partial class PC : Window
    {
        MasterReferenceClass ReferenceData;
        Player PlayerData;
        private XDocument xmlData;
        public PC(MasterReferenceClass Reference, Player player)
        {
            PlayerData = player;
            ReferenceData = Reference;
            InitializeComponent();
            xmlData = Serialize(player.Pkmnlist);

            TreeViewItem treeNode = new TreeViewItem
            {
                Header = "Pokemon",
                IsExpanded = true
            };

            BuildNodes(treeNode, xmlData.Root);
            Tree_Pkmn.Items.Add(treeNode);

        }

        public static XDocument Serialize(object obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

            XDocument doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                xmlSerializer.Serialize(writer, obj);
            }

            return doc;
        }

        private void BuildNodes(TreeViewItem treeNode, XElement element)
        {

            string attributes = "";
            if (element.HasAttributes)
            {
                foreach (var att in element.Attributes())
                {
                    attributes += " " + att.Name + " = " + att.Value;
                }
            }

            TreeViewItem childTreeNode = new TreeViewItem
            {
                Header = element.Name.LocalName + attributes,
                IsExpanded = true
            };
            if (element.HasElements)
            {
                foreach (XElement childElement in element.Elements())
                {
                    BuildNodes(childTreeNode, childElement);
                }
            }
            else
            {
                TreeViewItem childTreeNodeText = new TreeViewItem
                {
                    Header = element.Value,
                    IsExpanded = true
                };
                childTreeNode.Items.Add(childTreeNodeText);
            }

            treeNode.Items.Add(childTreeNode);
        }

        



    }
}
