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
    [SerializeField]
    private GameObject roopocketKey;
    [SerializeField]
    private Transform keySpawn;


    // Use this for initialization
    void Start ()
    {

        if (Statics.isInFreestyle == false)
        {
            bool spawnKey = false;
            if (SceneManager.GetActiveScene().name == "Platform sixth Level"
                || SceneManager.GetActiveScene().name == "Platform Level 11"
                || SceneManager.GetActiveScene().name == "Platform Level 16")
            {
                float rand = Random.Range(0f, 1f);
                if (rand <= 0.33333f)
                {


                    spawnKey = true;
                }
            }
            if (SceneManager.GetActiveScene().name == "Platform Level 20")
            {

                spawnKey = true;
            }

            if (spawnKey)
            {
                Debug.Log("Spawned key");
                GameObject instKey = Instantiate(roopocketKey);
                instKey.transform.position = keySpawn.position;
                instKey.transform.rotation = keySpawn.rotation;
            }
        }

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
