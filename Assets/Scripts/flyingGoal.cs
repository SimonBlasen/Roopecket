using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class flyingGoal : MonoBehaviour {

    public string levelToLoad;
    [SerializeField]
    private bool isTutorial = false;
    [SerializeField]
    private GameObject canvasTutEndscreen;


    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Rocket")
        {
            Transform par = other.transform;
            while (par.parent != null)
            {
                par = par.parent;
            }
            par.GetComponent<RocketProps>().Indestroyable = true;
            par.GetComponent<RocketProps>().enabled = false;
            par.GetComponent<RocketController>().enabled = false;

            if (isTutorial)
            {
                //SceneManager.LoadScene(levelToLoad);
                canvasTutEndscreen.GetComponent<Canvas>().enabled = true;
                canvasTutEndscreen.GetComponent<TutorialEndscreen>().SceneToLoad = levelToLoad;
            }
            else
            {
                //StartCoroutine(Example());

                Statics.nextScene = levelToLoad;

                Manager.Instance.Landed(other.transform, "Finish_" + levelToLoad);

                //SceneManager.LoadScene(levelToLoad);
            }

        }
    }

    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
    }
}
