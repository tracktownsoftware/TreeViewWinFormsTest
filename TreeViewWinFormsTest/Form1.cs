using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeViewWinFormsTest
{
    public partial class Form1 : Form
    {
        public SampleViewModel SampleViewModel;
        public ImageList ImageList;

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeViewModel();
            InitializeImageList();
            FillTreeview();
        }

        private void InitializeImageList()
        {
            ImageList = new ImageList();
            ImageList.Images.Add(new Bitmap(TreeViewWinFormsTest.Properties.Resources.FolderOpened_32));
            ImageList.Images.Add(new Bitmap(TreeViewWinFormsTest.Properties.Resources.FolderClosed_32));
            ImageList.Images.Add(new Bitmap(TreeViewWinFormsTest.Properties.Resources.EngineCode));
            ImageList.Images.Add(new Bitmap(TreeViewWinFormsTest.Properties.Resources.WindowsCode));
            ImageList.ImageSize = new Size(32, 32);

            treeView1.ImageList = ImageList;
            treeView1.SelectedImageIndex = 1;
            treeView1.ImageIndex = 1;

            treeView1.AfterCollapse += TreeView1_AfterCollapse;
            treeView1.AfterExpand += TreeView1_AfterExpand;
        }

        private void TreeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 0;
        }

        private void TreeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 1;
        }

        private void FillTreeview(SampleViewModel parentSample = null, TreeNodeCollection parentChildNodes = null)
        {
            if (parentSample is null)
            {
                parentSample = SampleViewModel;
                TreeNode parentTreeNode = new TreeNode(parentSample.Name, 0, 0);
                treeView1.Nodes.Add(parentTreeNode);
                parentChildNodes = parentTreeNode.Nodes;
            }

            foreach (SampleViewModel childSample in parentSample)
            {
                int imageIndex = 0;
                int selectedImageIndex = 0;

                switch (childSample.SampleType)
                {
                    case SampleType.Engine:
                        imageIndex = selectedImageIndex = 2;
                        break;
                    case SampleType.Windows:
                        imageIndex = selectedImageIndex = 3;
                        break;
                    default: //Category
                        break;
                }
                var childTreeNode = new TreeNode(childSample.Name, imageIndex, selectedImageIndex);
                parentChildNodes.Add(childTreeNode);
                FillTreeview(childSample, childTreeNode.Nodes);
                childTreeNode.Expand();
            }

            if (parentSample is SampleViewModel)
                treeView1.Nodes[0].Expand();
        }

        private void InitializeViewModel()
        {
            SampleViewModel category1 = new SampleViewModel("category1", SampleType.Category);
            for (int i = 1; i < 5; i++)
                category1.Add(new SampleViewModel("category1-item-" + i, SampleType.Engine));

            SampleViewModel category2 = new SampleViewModel("category2", SampleType.Category);
            for (int i = 1; i < 4; i++)
                category2.Add(new SampleViewModel("category2-item-" + i, SampleType.Windows));

            SampleViewModel category3 = new SampleViewModel("category3", SampleType.Category);
            for (int i = 1; i < 7; i++)
                category3.Add(new SampleViewModel("category3-item-" + i, SampleType.Windows));

            SampleViewModel category4 = new SampleViewModel("category4", SampleType.Category);
            for (int i = 1; i < 3; i++)
                category4.Add(new SampleViewModel("category4-item-" + i, SampleType.Engine));

            SampleViewModel = new SampleViewModel("Root node", SampleType.Category);
            SampleViewModel.Add(category1);
            SampleViewModel.Add(category2);
            SampleViewModel.Add(category3);
            SampleViewModel.Add(category4);
        }
    }
}
