using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// CLASS PATHREQUEST MANAGER 
/// 
/// This class is used to manage requests of each enemy currently active and queue them 
/// so they can be done sequentially. This way if multiple enemies are requesting paths our 
/// performance will not significantly drop. We use a static instance to we can always access the 
/// request manager and retrieve a path easily. 
public class PathRequestManager : MonoBehaviour 
{
    Queue<PathRequest> m_pathrequestqueue;
    PathRequest m_currentrequest;

    static PathRequestManager s_instance;
    PathFinder m_finder;
    bool m_isprocessing;

    /// AWAKE 
    /// 
    /// This is used to intialise our components of this script. 
    void Awake()
    {
        s_instance = this;
        m_finder = GetComponent<PathFinder>();
        m_pathrequestqueue = new Queue<PathRequest>();
    }

    /// REQUESTPATH 
    /// 
    /// This function takes in a position for both our start and end points of our path and we also take an 
    /// action which will act as a callback. If this function is called, we create a new path request by passing in our parameters
    /// we then queue our request and then call try process next.
    public static void RequestPath(Vector3 _pathstart, Vector3 _pathend, Action<Vector3[], bool> _callback)
    {
        Debug.Log("requesting");
        PathRequest newrequest = new PathRequest(_pathstart, _pathend, _callback);
        s_instance.m_pathrequestqueue.Enqueue(newrequest);
        s_instance.TryProcessNext();
    }

    /// TRYPROCESSNEXT 
    /// 
    /// This function checks if the processing flag is not set and the queue's count is more than zero. 
    /// If so, we dequeue the current path request, set processing to true and call StartFindPath, if
    /// a job is being done then no other requests will go through, keeping our performance steady. 
    void TryProcessNext()
    {
        if(!m_isprocessing && m_pathrequestqueue.Count > 0)
        {
            m_currentrequest = m_pathrequestqueue.Dequeue();
            m_isprocessing = true;
            m_finder.StartFindPath(m_currentrequest.m_pathstart, m_currentrequest.m_pathend);
        }

    }

    /// FINISHPROCESSINGPATH 
    /// 
    /// This is called when a path has been found or the coroutine has stopped for any reason. We take in 
    /// a list of positions and our sucess flag. We then call our current request's callback, passing in our list of positions and our 
    /// success. Then we set processing to false and then call try process next. 
    public void FinishProcessingPath(Vector3[] _path, bool _success)
    {
        Debug.Log("pathfound");
        m_currentrequest.m_callback(_path, _success);
        m_isprocessing = false;
        TryProcessNext();
    }

    /// PathRequest 
    /// 
    /// This struct simple gives us a container structure to store our callback functionand the start and end positions of our 
    /// request. Then in the constructor we simply set the passed in variables to our members.  
    struct PathRequest
    {
        public Vector3 m_pathstart;
        public Vector3 m_pathend;
        public Action<Vector3[], bool> m_callback;

        public PathRequest(Vector3 _pathstart, Vector3 _pathend, Action<Vector3[], bool> _callback)
        {
            m_pathstart = _pathstart;
            m_pathend = _pathend;
            m_callback = _callback;
        }
    }
}
