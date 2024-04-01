using DefaultNamespace;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private float minimumDivideRate;
    [SerializeField] private float maximumDivideRate;
    [SerializeField] private GameObject line;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject roomLine;
    [SerializeField] private int maximumDepth;

    private void Start()
    {
        Node root = new Node(new RectInt(0, 0, mapSize.x, mapSize.y));
        DrawMap(0,0);
        Divide(root, 0);
    }

    void DrawMap(int x, int y)
    {
        LineRenderer lineRenderer = Instantiate(map).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0,new Vector2(x,y) - mapSize / 2);
        lineRenderer.SetPosition(1,new Vector2(x + mapSize.x,y) - mapSize /2);
        lineRenderer.SetPosition(2,new Vector2(x + mapSize.x, y + mapSize.y) - mapSize / 2);
        lineRenderer.SetPosition(3, new Vector2(x, y + mapSize.y) - mapSize / 2);
    }

    void Divide(Node tree, int n)
    {
        if (n == maximumDepth) return;
        int x = tree.nodeRect.x;
        int y = tree.nodeRect.y;
        int width = tree.nodeRect.width;
        int height = tree.nodeRect.height;
        
        
        // 가로 세로 사이즈중 가장 큰 사이즈를 가져옴
        int maxLength = Mathf.Max(width, height);
        // 반으로 자를 위치를 랜덤으로 가져온다
        int split = Mathf.RoundToInt(Random.Range(maxLength * minimumDivideRate, maxLength * maximumDivideRate));
        
        if (width >= height)
        {
            // 가로가 세로보다 크거나 같을경우
            // 
            tree.leftNode = new Node(new RectInt(x, y, split, height));
            tree.rightNode = new Node(new RectInt(x + split, y, width - split, height));
            
            print($"x 값  >> {x} y 값 >> {y} split >> {split} height >> {height}");
            
            
            DrawLine(new Vector2(x + split,y),new Vector2(x + split,y + height));
        }
        else
        {
            tree.leftNode = new Node(new RectInt(x, y, width, split));
            tree.rightNode = new Node(new RectInt(x, y + split, width, height - split));
            DrawLine(new Vector2(x,y + split) , new Vector2(x + width, y + split));
        }

        tree.leftNode.parNode = tree;
        tree.rightNode.parNode = tree;
        Divide(tree.leftNode, n +1);
        Divide(tree.rightNode, n +1);
    }

    void DrawLine(Vector2 from, Vector2 to)
    {
        LineRenderer lineRenderer = Instantiate(line).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0,from - mapSize/2);
        lineRenderer.SetPosition(1,to - mapSize/2);
    }
    
}
