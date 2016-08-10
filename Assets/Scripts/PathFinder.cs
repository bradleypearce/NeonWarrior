using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// CLASS PATHFINDER 
/// 
/// This class is dedicated to finding a path by using the grid 
/// and the pathrequestmanager. Basically the request manager called the 
/// startfind path function which begins a coroutine findpath which finds a path 
/// between two positions in space. The pathfinder then feeds back to the requestmanager
/// where the path is passed along to the unit that requested it. We use a coroutine to spread the function
/// out over multiple frames where necessary to improve performance in-game.
/// </summary>
public class PathFinder : MonoBehaviour
{
    PathRequestManager m_requestmanager;
    Grid m_grid;

    /// AWAKE
    /// 
    /// Here we initialise our grid and our requestmanager. 
    /// </summary>
    void Awake()
    {
        m_grid = GetComponent<Grid>();
        m_requestmanager = GetComponent<PathRequestManager>();
    }

    /// STARTFINDPATH 
    /// 
    /// This function initialises our coroutine to find a path. Here we pass in two positions 
    /// in space and then these are passed to the findpath coroutine. 
    public void StartFindPath(Vector2 _startpos, Vector3 _endpos)
    {
        StartCoroutine(FindPath(_startpos, _endpos));
    }

    /// FIND PATH 
    /// 
    /// This function implements the A* algorithm over multiple frames so that the 
    /// game does not freeze or lose performance every call to this function. This is a requirement for having multiple enemies on screen
    /// all chasing the player. We start by initialising an array of positions called waypoints. We also define a success variable and set it to false.
    /// Then we find the start node and end node based on the positions we gave by passing them to the grid's node from world point. Then we check if the end node 
    /// is walkable, if it is then we create our openlist and closed list (the open list uses a heap to skip checking each node in this function, the most expensive 
    /// part of the algorithm). We then add the start node to the openlist heap. Then while the openlist is not empty, we get the first node in the heap, the best
    /// node, and add it to the closed list. Then we check if the current node is the goal node. If so we set our success flag to true and call retrace path. Otherwise
    /// we check each neighbour of the current node's distance from the current node and the end node and set the parent of this node to the current node. We then 
    /// Then we add the neighbour to the open list. Otherwise we update the neighbour in the heap as it's value has been changed. Then if success if true, we set our waypoints
    /// array to the value returned by Retracepath by passing in our start and end node. Then once that's been done we call the pathrequest finished fucntion which should stop 
    /// the coroutine. We pass in our success flag and our waypoints array.
    IEnumerator FindPath(Vector3 _startpos, Vector3 _targetpos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool success = false;

        Node startnode = m_grid.NodeFromWorldPoint(_startpos);
        Node endnode = m_grid.NodeFromWorldPoint(_targetpos);

        if (endnode.GetWalkable())
        {
            Heap<Node> openlist = new Heap<Node>(m_grid.MaxSize);
            HashSet<Node> closedlist = new HashSet<Node>();

            openlist.Add(startnode);

            while (openlist.Count > 0)
            {
                Node currentnode = openlist.RemoveFirst();
                closedlist.Add(currentnode);

                if (currentnode == endnode)
                {
                    success = true;
                    RetracePath(startnode, endnode);
                    break;
                }

                foreach (Node neighbour in m_grid.GetNeighbours(currentnode))
                {
                    if (!neighbour.GetWalkable() || closedlist.Contains(neighbour))
                    {
                        continue;
                    }

                    int costtoneighbour = currentnode.GetGCost() + GetEuclideanDistance(currentnode, neighbour);

                    if (costtoneighbour < neighbour.GetGCost() || !openlist.Contains(neighbour))
                    {
                        neighbour.SetGCost(costtoneighbour);
                        neighbour.SetHCost(GetEuclideanDistance(neighbour, endnode));
                        neighbour.SetParent(currentnode);

                        if (!openlist.Contains(neighbour))
                        {
                            openlist.Add(neighbour);
                        }
                        else
                        {
                            openlist.UpdateItem(neighbour);
                        }
                    }
                }


            }
        }
        else
        {
            yield return null;
        }

        if (success)
        {
            waypoints = RetracePath(startnode, endnode);
        }

        m_requestmanager.FinishProcessingPath(waypoints, success);
    }

    /// RETRACEPATH
    /// 
    /// This function steps through each node and assigns the next node to the current node's parent. 
    /// Then we create an array of vectors and set that equal to our simplify path function. We then return our waypoints array.
    Vector3[] RetracePath(Node _startnode, Node _endnode)
    {
        List<Node> path = new List<Node>();

        Node currentnode = _endnode;

        while (currentnode != _startnode)
        {
            path.Add(currentnode);
            currentnode = currentnode.GetParent();
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;

    }

    /// SimplifyPath
    /// 
    /// This function takes a list nodes and defines a list of 
    /// vector3s called waypoints. We then also define a vector2 called direction. 
    /// Then using a for loop we start at the first position on the path and iterate through each element. 
    /// Then we define our direction new as the node at i - 1 in our path's grid indexes - our current node's grid indexes and if 
    /// the direction is not the same then we add our current node to the waypoints list. Then set the new direction to the old direction. 
    /// Once done, we return the waypoints list as an array.
    /// <param name="_path"></param>
    /// <returns></returns>
    Vector3[] SimplifyPath(List<Node> _path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < _path.Count; i++)
        {
            Vector2 directionNew = new Vector2(_path[i - 1].GetGridX() - _path[i].GetGridX(), _path[i - 1].GetGridY() - _path[i].GetGridY());
            if (directionNew != directionOld)
            {
                waypoints.Add(_path[i].m_worldposition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    /// GETEUCLIDEANDISTANCE 
    /// 
    /// This function gets the heuristic between the passed in nodes. 
    /// We firstly get the absolute value of A's grid indexes minus B's grid idexes and then 
    /// if our X is more than Y we return 14 * y distance + 10 * (our x distance - our y distance);
    /// if the opposite is true we reverse the x's and y's.
    public int GetEuclideanDistance(Node _a, Node _b)
    {
        int dstX = Mathf.Abs(_a.GetGridX() - _b.GetGridX());
        int dstY = Mathf.Abs(_a.GetGridY() - _b.GetGridY());

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        else
        {
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }

}
