using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Xml.Serialization;
using PokemonRPG.Configs;

namespace PokemonRPG.Windows
{
    /// <summary>
    ///     Interaction logic for PC.xaml
    /// </summary>
    public partial class PC : Window
    {
        private Player PlayerData;
        private MasterReferenceClass ReferenceData;
        private readonly XDocument xmlData;

        public PC(MasterReferenceClass Reference, Player player)
        {
            PlayerData = player;
            ReferenceData = Reference;
            InitializeComponent();
            xmlData = Serialize(player.Pkmnlist);

            var treeNode = new TreeViewItem
            {
                Header = "Pokemon",
                IsExpanded = true
            };

            BuildNodes(treeNode, xmlData.Root);
            Tree_Pkmn.Items.Add(treeNode);
        }

        public static XDocument Serialize(object obj)
        {
            var xmlSerializer = new XmlSerializer(obj.GetType());

            var doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                xmlSerializer.Serialize(writer, obj);
            }

            return doc;
        }

        private void BuildNodes(TreeViewItem treeNode, XElement element)
        {
            var attributes = "";
            if (element.HasAttributes)
                foreach (var att in element.Attributes())
                    attributes += " " + att.Name + " = " + att.Value;

            var childTreeNode = new TreeViewItem
            {
                Header = element.Name.LocalName + attributes,
                IsExpanded = true
            };
            if (element.HasElements)
            {
                foreach (var childElement in element.Elements()) BuildNodes(childTreeNode, childElement);
            }
            else
            {
                var childTreeNodeText = new TreeViewItem
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