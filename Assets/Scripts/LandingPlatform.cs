using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LandingPlatform : MonoBehaviour
{
    [SerializeField]
    private float speedThreshhold = 0.1f;
    [SerializeField]
    private string platformName;
    [SerializeField]
    private bool isTutorial;
    [SerializeField]
    private GameObject roopocketKey;


    [Space]

    [SerializeField]
    private Light[] spotlights;
    [SerializeField]
    private GameObject[] landedObjects;
    [SerializeField]
    private GameObject[] notLandedObjects;

    private List<Transform> landedTransforms = new List<Transform>();

    private List<bool> landedSended = new List<bool>();
    private List<float> standingStill = new List<float>();

    private bool isOneLandedBefore = false;

    // Use this for initialization
    void Start ()
    {
        if (Statics.isInFreestyle == false)
        {
            bool spawnKey = false;
            /*if (SceneManager.GetActiveScene().name == "Platform sixth Level"
                || SceneManager.GetActiveScene().name == "Platform Level 11"
                || SceneManager.GetActiveScene().name == "Platform Level 16")
            {
                float rand = Random.Range(0f, 1f);
                if (rand <= 0.33333f)
                {


                    spawnKey = true;
                }
            }*/
            if (SceneManager.GetActiveScene().name == "Platform Level 20")
            {

                    spawnKey = true;
                
            }

            if (GameObject.FindObjectOfType<LevelNumber>() != null && GameObject.FindObjectOfType<LevelNumber>().LevelNumberProp >= 0 && SavedGame.ChallengeRewards[SavedGame.LastPlayedRocket, GameObject.FindObjectOfType<LevelNumber>().LevelNumberProp] < -1f)
            {
                spawnKey = true;
            }

            if (spawnKey && platformName.StartsWith("Finish"))
            {
                Debug.Log("Spawned key");
                GameObject instKey = Instantiate(roopocketKey);
                instKey.transform.position = transform.position + new Vector3(0f, 2f, 0f);
                instKey.transform.rotation = transform.rotation;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            Manager.Instance.Landed(GameObject.FindObjectOfType<RocketProps>().transform, platformName);
        }*/

        bool isOneLanded = false;
		for (int i = 0; i < landedTransforms.Count; i++)
        {
            if (IsNotMoving(landedTransforms[i]))
            {
                standingStill[i] += Time.deltaTime;
            }
            else
            {
                standingStill[i] = 0f;
            }

            if (IsLanded(landedTransforms[i]) && landedSended[i] == false)
            {
                landedSended[i] = true;
                Manager.Instance.Landed(landedTransforms[i], platformName);
            }
            else if (IsLanded(landedTransforms[i]) == false && landedSended[i])
            {
                landedSended[i] = false;
                Manager.Instance.Takeoff(landedTransforms[i], platformName);
            }
            if (IsLanded(landedTransforms[i]))
            {
                isOneLanded = true;
            }
        }
        
        toggleLandingEffects(isOneLanded);
        
	}

    private void toggleLandingEffects(bool isOneLanded)
    {
        if (isOneLanded && isOneLandedBefore == false)
        {
            isOneLandedBefore = true;

            executeLandingEffects(true);
        }
        else if (isOneLanded == false && isOneLandedBefore)
        {
            isOneLandedBefore = false;

            executeLandingEffects(false);
        }
    }

    private void executeLandingEffects(bool on)
    {
        for (int i = 0; i < spotlights.Length; i++)
        {
            spotlights[i].GetComponent<FlickerLight>().On = on;
        }
        for (int i = 0; i < landedObjects.Length; i++)
        {
            landedObjects[i].SetActive(on);
        }
        for (int i = 0; i < notLandedObjects.Length; i++)
        {
            notLandedObjects[i].SetActive(!on);
        }
    }

    public bool IsNotMoving(Transform rocket)
    {
        for (int i = 0; i < landedTransforms.Count; i++)
        {
            if (rocket.GetInstanceID() == landedTransforms[i].GetInstanceID())
            {
                return rocket.GetComponent<Rigidbody>().velocity.y < speedThreshhold && rocket.GetComponent<RocketController>().IsThrusting() == false;
                //return rocket.GetComponent<Rigidbody>().velocity.magnitude < speedThreshhold;
            }
        }

        return false;
    }

    public bool IsLanded(Transform rocket)
    {
        for (int i = 0; i < landedTransforms.Count; i++)
        {
            if (rocket.GetInstanceID() == landedTransforms[i].GetInstanceID())
            {
                return standingStill[i] >= 2f;
            }
        }

        return false;
    }

    public bool TriggerIsIn(Transform other)
    {
        if (other.tag == "Rocket")
        {
            if (landedTransforms.Contains(other))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void TriggerExit(Transform other)
    {
        if (other.tag == "Rocket")
        {
            if (landedTransforms.Contains(other))
            {
                for (int i = 0; i < landedTransforms.Count; i++)
                {
                    if (landedTransforms[i].GetInstanceID() == other.GetInstanceID())
                    {
                        if (landedSended[i])
                        {
                            landedSended[i] = false;
                            Manager.Instance.Takeoff(landedTransforms[i], platformName);
                        }

                        standingStill.RemoveAt(i);
                        landedTransforms.RemoveAt(i);
                        landedSended.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }

    public bool TriggerEnter(Transform other)
    {
        if (other.tag == "Rocket")
        {
            if (landedTransforms.Contains(other) == false)
            {
                standingStill.Add(0f);
                landedTransforms.Add(other);
                landedSended.Add(false);

                return false;
            }
            else
            {
                return true;
            }
        }
        return false;

    }
}
