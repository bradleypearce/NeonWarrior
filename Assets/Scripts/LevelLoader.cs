using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;

/// LEVELLOADER
/// 
/// This class is used to load between levels in our game. We don't destroy this object while the application 
/// is running so a level can be loaded very simply and effectively. We also load asynchronously so the game 
/// does not freeze on load. 
public class LevelLoader : MonoBehaviour 
{
	AsyncOperation m_async;

	/// START
	/// 
	/// Here we tell the gameobject to not destroy this component between level loads.
	void Start () 
	{
		DontDestroyOnLoad(this);
	}

    /// LOADLEVEL
    /// 
    /// This function simply takes in the name of the level and uses it to assign 
    /// our async variable to our load scene function. async can be useful for getting
    /// the progress of an asynchronous operation. 
	public void LoadLevel(string _levelname)
	{
        //m_async = SceneManager.LoadSceneAsync(_levelname);
	}
}
