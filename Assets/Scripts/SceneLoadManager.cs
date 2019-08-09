using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private static bool initialized = false;

    // ensure you call this method from a script in your first loaded scene
    public static void Initialize()
    {
        if (initialized == false)
        {
            AbsoluteTimeTracker.StartTime = -1;
            AbsoluteTimeTracker.AccTime = 0;
            initialized = true;
            // adds this to the 'activeSceneChanged' callbacks if not already initialized.
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnSceneWasLoaded;
        }
    }

    private static void OnSceneWasLoaded(UnityEngine.SceneManagement.Scene from, UnityEngine.SceneManagement.Scene to)
    {
        GameObject objTimeTracker = new GameObject("Absolute Playtime Tracker");
        objTimeTracker.AddComponent<AbsoluteTimeTracker>();
    }
}
