using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class Node
    {
        public Node leftNode;
        public Node rightNode;
        public Node parNode;
        public RectInt nodeRect;

        public Node(RectInt rect)
        {
            nodeRect = rect;
        }
        

        public void TestCode()
        {
            
        }
    }
}