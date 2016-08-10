using UnityEngine;
using System.Collections;

/// NODE 
/// 
/// This class is a representation of each element in our grid. We define them with a world position 
/// and whether they are walkable for not. This enables us to implement a pathfinding algorithm using each node. 
/// This also inherits from IHeapItem so we can implement a heapindex interface variable. This is useful for storing nodes in 
/// our Heap class. It also requires a comparison function (which IHeapItem derives from) so we can define a comparison method between nodes.
/// We need this so our Heap we use later will be able to sort itself in the best order. 
[System.Serializable]
public class Node : IHeapItem<Node>
{
    [SerializeField]
    bool m_walkable;
    [SerializeField]
    public Vector2 m_worldposition;
    int m_GridX;
    int m_GridY;
    int m_gcost;
    int m_hcost;
    int m_heapindex;
    Node m_parent;

    /// CONSTRUCTOR 
    /// 
    /// The constructor takes in a walkable flag, a worldposition and an index in each axis to assign to this node.
    public Node(bool _walkable, Vector3 _worldpos, int _gridx, int _gridy)
    {
        m_walkable = _walkable;
        m_worldposition = _worldpos;
        m_GridX = _gridx;
        m_GridY = _gridy;
    }

    /// HeapIndex
    /// 
    /// This is an interface variable which we use to get the index the node is current at in the heap, we
    /// can also assign this as well to any given value. This is requirement when inheriting from IHeapItem
    public int HeapIndex
    {
        get
        {
            return m_heapindex;
        }
        set
        {
            m_heapindex = value;
        }
    }

    /// COMPARETO
    /// 
    /// This function is defined to create a polymorph of the ICOMPARABLE member function. 
    /// For nodes we want to compare the fcost of this node and the passed in node using the compareto function for our fcosts
    /// and then if the comparison returns 0 then the h cost will be compared instead. This is essentially how the open list in 
    /// our A* algorithm is checked however this method is far more efficient. 
    public int CompareTo(Node _node)
    {
        int compare = m_fcost.CompareTo(_node.m_fcost);

        if (compare == 0)
        {
            compare = m_hcost.CompareTo(_node.GetHCost());
        }
        return -compare;
    }

    /// GETPARENT
    /// 
    /// Returns the parent to this node. 
    public Node GetParent()
    {
        return m_parent;
    }

    /// SETPARENT
    /// 
    /// Sets the parent of this node to the passed in node. 
    public void SetParent(Node _node)
    {
        m_parent = _node;
    }

    /// FCOST
    /// 
    /// Interface variable for returning the fcost without containing a variable for it. 
    public int m_fcost
    {
        get
        {
            return m_gcost + m_hcost;
        }
    }

    /// GETWALKABLE 
    /// 
    /// Returns the walkable flag.
    public bool GetWalkable()
    {
        return m_walkable;
    }

    /// GetGridX
    /// 
    /// Returns the index of the node in the grid. 
    public int GetGridX()
    {
        return m_GridX;
    }

    /// GetGRIDY
    /// 
    /// Returns the index of the node in the grid. 
    public int GetGridY()
    {
        return m_GridY;
    }

    /// GETPOSITION 
    /// 
    /// Returns the worldposition of this node.
    public Vector2 GetPosition()
    {
        return m_worldposition;
    }

    /// GETGCOST 
    /// 
    /// Returns the gcost of this node. 
    public int GetGCost()
    {
        return m_gcost;
    }

    /// SETGCOST
    /// 
    /// Sets the gcost to the passed in value.
    public void SetGCost(int _gcost)
    {
        m_gcost = _gcost;
    }

    /// SETHCOST
    /// 
    /// Sets the hcost to the passed in value.
    public void SetHCost(int _hcost)
    {
        m_gcost = _hcost;
    }

    /// GETHCOST 
    /// 
    /// Returns the hcost of this node. 
    public int GetHCost()
    {
        return m_hcost;
    }
}

