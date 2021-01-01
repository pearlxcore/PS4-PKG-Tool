using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS4_PKG_Tool
{
    public static class TreeView
    {
        public static List<TreeNode> GetAllNodes(this TreeNode Node)
        {
            List<TreeNode> list = new List<TreeNode>();
            list.Add(Node);
            foreach (TreeNode n in Node.Nodes)
                list.AddRange(GetAllNodes(n));
            return list;
        }
    }

}
