using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// GRID
/// 
/// This class is required to effectively navigate our enemies throughout our 
/// world. We do this using a 2D array of our Node script, which contains our positions 
/// of each of our nodes. We use a public vector to set the world size of our grid and 
/// the script will automatically instantiate our nodes based on the size of the world. 
/// This script also handles setting the node to walkable or unwalkable so our enemies can 
/// avoid these nodes. We also use a public radius variable so we can set how many nodes we want in our grid, 
/// for larger maps, a larger radius is required so our pathfinding algorithm is as optimal as possible. 
public class Grid : MonoBehaviour 
{
    public Vector2 m_gridworldsize;
    public float m_noderadius;
    public LayerMask m_unwalkablemask;

    Node[,] m_grid;

    int m_gridsizeX, m_gridsizeY;
    float m_NodeDiameter;
    Vector3 m_worldbottomleft;

    public int MaxSize
    {
        get
        {
            return m_gridsizeX * m_gridsizeY;
        }
    }

    /// AWAKE
    /// 
    /// This function simply sets our grid size x and y variables, our node diameter and also called the creategrid
    /// function which initializes our grid. 
    void Awake()
    {
        m_NodeDiameter = m_noderadius * 2;
        m_gridsizeX = Mathf.RoundToInt(m_gridworldsize.x / m_NodeDiameter);
        m_gridsizeY = Mathf.RoundToInt(m_gridworldsize.y / m_NodeDiameter);
        CreateGrid();
    }

    /// GETNEIGHBOURS
    /// 
    /// The get neighbours function returns a list of nodes which are 
    /// neighbours to the passed in node. We use an outer and inner loop
    /// for our x and y variables, we start at -1 and end at 1 for each. Within these
    /// loops, we check if x and y is equal to zero and if they are we want to continue 
    /// to the next iteration as this would be our passed in node. We then create two ints 
    /// which will act as our index's for passing our neighours to our neighbours list. We set
    /// these to our passed in node's gridX and Y plus our x and y. If it's to the right, x will add
    /// 1 to checkx ect. We then check if our checkx and y variables are within the grid's index and if so
    /// we add the node as checkx and y's index in the grid.
    public List<Node> GetNeighbours(Node _node)
    {
        List<Node> m_neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue; 
                }

                int m_checkx = _node.GetGridX() + x;
                int m_checky = _node.GetGridY() + y;

                if (m_checkx >= 0 && m_checkx < m_gridsizeX && m_checky >= 0 && m_checky < m_gridsizeY)
                {
                    m_neighbours.Add(m_grid[m_checkx, m_checky]);
                }
            }
        }

        return m_neighbours;
    }

    /// NODEFROMWORLDPOINT 
    /// 
    /// This function takes in a position and returns the node which is at this location. 
    /// We start by creating a percentx and y value which we set as our worldposition x and y 
    /// value plus the size of our grid divided by 2, giving us half the grid size in that axis and
    /// then dividing it by the total world size. This gives us a percentage between 0 and 1 which we can 
    /// use to find our node. We then create two integers which are the closest into to the amount of nodes 
    /// in each axis multiplied by our percentage vales in each axis. This returns the index in each axis relative 
    /// to the world position passed in. We then return our node at our x and y ints we found in our grid. 
    public Node NodeFromWorldPoint(Vector3 _worldpos)
    {
        float m_percentX = (_worldpos.x + m_gridworldsize.x / 2) / m_gridworldsize.x;
        float m_percentY = (_worldpos.y + m_gridworldsize.y / 2) / m_gridworldsize.y;

        m_percentX = Mathf.Clamp01(m_percentX);
        m_percentY = Mathf.Clamp01(m_percentY);

        int m_x = Mathf.RoundToInt((m_gridsizeX - 1) * m_percentX);
        int m_y = Mathf.RoundToInt((m_gridsizeX - 1) * m_percentY);

        return m_grid[m_x, m_y];
    }

    /// CREATEGRID
    /// 
    /// This function instantiates our nodes in 2D array for us in pathfinding. We start by creating our array and then finding the bottom left corner 
    /// of our grid in world space. Then we use a outer and inner for loop for each node in our gridx and y values to find the world point of each node 
    /// based on the x and y values in our for loop. Then we check if the node will be walkable by checking the radius of each node for a collider in the
    /// unwalkable layer. This will return false if the tile is walkable. We then set our x and y as indexes in our grid and then instantiate a new Node 
    /// passing in the walkable flag, the world point and the x and y indexes.
    /// </summary>
    void CreateGrid()
    {
        m_grid = new Node[m_gridsizeX, m_gridsizeY];
        m_worldbottomleft = transform.position - Vector3.right * m_gridworldsize.x / 2 - Vector3.up * m_gridworldsize.y / 2;


        for (int x = 0; x < m_gridsizeX; x++)
        {
            for (int y = 0; y < m_gridsizeY; y++)
            {
                Vector3 worldpoint = m_worldbottomleft + Vector3.right * (x * m_NodeDiameter + m_noderadius) + Vector3.up * (y * m_NodeDiameter + m_noderadius);
                bool walkable = (bool)!Physics2D.OverlapCircle(worldpoint, m_noderadius, m_unwalkablemask);
               
                m_grid[x, y] = new Node(walkable, worldpoint, x, y);
            }
        }
    }
}
