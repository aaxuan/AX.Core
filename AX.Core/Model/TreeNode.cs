using System.Collections.Generic;
using System.Linq;

namespace AX.Core.Model
{
    public interface ITreeNode
    {
        public string Id { get; set; }

        public string Pid { get; set; }
    }

    public class TreeNode<ITreeNode>
    {
        public ITreeNode Node { get; set; }

        public List<TreeNode<ITreeNode>> Child { get; set; }
    }

    public class TreeManager
    {
        public TreeNode<ITreeNode> GetTree(List<ITreeNode> treeNodes, TreeNode<ITreeNode> node = null)
        {
            if (node == null || node.Node == null)
            {
                node = new TreeNode<ITreeNode>();
                node.Child = treeNodes.Where(p => p.Pid == "0" || string.IsNullOrWhiteSpace(p.Pid)).Select(p => new TreeNode<ITreeNode>() { Node = p }).ToList();
            }
            else
            {
                node.Child = treeNodes.Where(p => p.Pid == node.Node.Id).Select(p => new TreeNode<ITreeNode>() { Node = p }).ToList();
            }
            foreach (var item in node.Child)
            {
                GetTree(treeNodes, item);
            }
            return node;
        }
    }
}