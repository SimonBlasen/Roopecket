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
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            Manager.Instance.Landed(GameObject.FindObjectOfType<RocketProps>().transform, levelToLoad);
        }*/
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

            if (levelToLoad != "_Restart")
            {
                par.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                par.GetComponent<RocketProps>().Indestroyable = true;
                par.GetComponent<RocketProps>().enabled = false;
                par.GetComponent<RocketController>().enabled = false;
            }
        

            if (isTutorial)
            {
                //SceneManager.LoadScene(levelToLoad);
                canvasTutEndscreen.GetComponent<Canvas>().enabled = true;
                canvasTutEndscreen.GetComponent<TutorialEndscreen>().SceneToLoad = levelToLoad;
            }
            else
            {
                if (levelToLoad == "_Restart")
                {
                    rocketPar = par;
                    Invoke("DestroyRocket", 0.45f);
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    }

    private Transform rocketPar = null;
    private void DestroyRocket()
    {
        rocketPar.GetComponent<RocketProps>().Damage(rocketPar.GetComponent<RocketProps>().MaxHealth + 10);
    }

    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
    }
}
