using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrophyPreviewer : MonoBehaviour
{
    public GameObject[] trophies;
    public Transform[] trophySpawns;
    public Transform spawnPos;
    public Transform hoverPos;
    public Transform endPos;
    public float lerpSpeedHover = 0.04f;
    public float accelerationAway = 1f;
    public float rotSpeed = 1f;
    public float hoverTime = 3f;


    private int flyState = -1;
    private int trophyIndex = 0;
    private float hoveringTime = 0f;
    private float flyAwaySpeedCur = 0f;

    private GameObject instTroph = null;
    private float trophyRefresh = 0f;


    // Start is called before the first frame update
    void Start()
    {
        int trIndex = 0;
        for (int i = 0; i < SavedGame.TrophiesWorth.Length; i++)
        {
            if (SavedGame.TrophiesWorth[i] > 0f)
            {

            }
        }
    }

    private bool instantiatedTrophies = false;

    // Update is called once per frame
    void Update()
    {
        trophyRefresh += Time.deltaTime;
        if (instantiatedTrophies == false && trophyRefresh >= 2f)
        {
            instantiatedTrophies = true;
            trophyRefresh = 0f;

            int trIndex = 0;
            for (int i = 0; i < SavedGame.TrophiesWorth.Length; i++)
            {
                if (SavedGame.TrophiesWorth[i] > 0f)
                {
                    GameObject instTr = Instantiate(trophies[i], transform);
                    instTr.transform.position = trophySpawns[trIndex].position;
                    instTr.transform.rotation = trophySpawns[trIndex].rotation;
                    if (instTr.GetComponentInChildren<TextMeshPro>() != null)
                    {
                        instTr.GetComponentInChildren<TextMeshPro>().text = SavedGame.TrophiesWorth[i].ToString("n0");
                    }
                    trIndex++;
                }
            }
            Debug.Log("Loaded " + trIndex.ToString() + " trophies");
        }

        if (flyState >= 0)
        {
            instTroph.transform.Rotate(0f, Time.deltaTime * rotSpeed, 0f);

            if (flyState == 0)
            {
                instTroph.transform.position = Vector3.Lerp(instTroph.transform.position, hoverPos.position, lerpSpeedHover);
                if (Vector3.Distance(instTroph.transform.position, hoverPos.position) < 0.02f)
                {
                    flyState = 1;
                }
            }
            else if (flyState == 1)
            {
                hoveringTime += Time.deltaTime;
                if (hoveringTime >= hoverTime)
                {
                    flyState = 2;
                }
            }
            else if (flyState == 2)
            {
                flyAwaySpeedCur += Time.deltaTime * accelerationAway;

                instTroph.transform.position = Vector3.MoveTowards(instTroph.transform.position, endPos.position, flyAwaySpeedCur * Time.deltaTime);
                if (Vector3.Distance(instTroph.transform.position, endPos.position) < 0.1f)
                {
                    flyState = -1;
                    instTroph.SetActive(false);
                }
            }
        }
    }

    public void ShowTrophy(int index, float worth)
    {
        if (index >= 0 && index < trophies.Length)
        {
            trophyIndex = index;

            instTroph = Instantiate(trophies[index], transform);

            if (instTroph.GetComponentInChildren<TextMeshPro>() != null)
            {
                instTroph.GetComponentInChildren<TextMeshPro>().text = worth.ToString("n0");
            }
            flyState = 0;
            instTroph.transform.position = spawnPos.position;
            instTroph.transform.rotation = Quaternion.identity;
        }
    }
}
