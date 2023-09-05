using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS4PKGTool.Utilities.Extension
{
    public static class TreeView
    {
        public static List<TreeNode> GetAllNodes(this TreeNode Node)
        {
            List<TreeNode> list = new List<TreeNode>();
            list.Add(Node);
            foreach (TreeNode n in Node.Nodes)
                list.AddRange(n.GetAllNodes());
            return list;
        }
    }

}
