using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicsCameraMover : MonoBehaviour {

    [Header("Settings")]
    public Transform[] lookPoints;
    public float[] howNear;
    public float[] transferSpeeds;
    public Vector3 wobbleRange;
    public Vector3 wobbleRangeLook;
    public float lerpSpeed = 0.1f;
    public float lerpSpeedLook = 0.1f;
    public float lerpSpeedWabble = 0.1f;
    public float lerpSpeedWabbleLook = 0.1f;
    public float lazyWeight = 0.9f;
    public float damperTime = 0.9f;
    public float maxSpeed = 0.9f;
    public AudioClip[] audioClips;

    [Space]

    [Header("References")]
    [SerializeField]
    private Transform lookat;
    [SerializeField]
    private Transform lookatPos;
    [SerializeField]
    private Transform goalpos;
    [SerializeField]
    private Transform lookatStatic;
    [SerializeField]
    private Transform goalposStatic;

    private Vector3 oldMoveDelta = Vector3.zero;
    private Vector3 oldMoveDelta2 = Vector3.zero;

    private float reachedThresh = 1f;

    public string LevelToLoad;

    private int index = -1;

    private bool[] reachedIndices = new bool[2];

	// Use this for initialization
	void Start ()
    {
        reachedIndices[0] = false;
        reachedIndices[1] = false;
        NextWindow();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextWindow();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Loading scene " + LevelToLoad);
            SceneManager.LoadScene(LevelToLoad);
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {

        Vector3 oldPos = transform.position;

        //transform.position = Vector3.Lerp(transform.position, goalpos.position, reachedIndices[0] ? lerpSpeedWabble : transferSpeeds[index]);
        transform.position = Vector3.SmoothDamp(transform.position, goalpos.position, ref oldMoveDelta, damperTime * (reachedIndices[0] ? 1f : transferSpeeds[index]), maxSpeed);
        lookat.position = Vector3.SmoothDamp(lookat.position, lookatPos.position, ref oldMoveDelta2, damperTime * (reachedIndices[1] ? 1f : transferSpeeds[index]), maxSpeed);
        //lookat.position = Vector3.Lerp(lookat.position, lookatPos.position, reachedIndices[1] ? lerpSpeedWabbleLook : transferSpeeds[index]);
        transform.LookAt(lookat);

        //oldMoveDelta = transform.position - oldPos;

		if (Vector3.Distance(transform.position, goalpos.position) < reachedThresh)
        {
            Debug.Log("Reched: " + index.ToString());
            reachedIndices[0] = true;
            Vector3 rand = new Vector3(Random.Range(-wobbleRange.x, wobbleRange.x), Random.Range(-wobbleRange.y, wobbleRange.y), Random.Range(-wobbleRange.z, wobbleRange.z));

            goalpos.position = goalposStatic.position + rand;
        }

        if (Vector3.Distance(lookat.position, lookatPos.position) < reachedThresh)
        {
            reachedIndices[1] = true;
            Vector3 randLook = new Vector3(Random.Range(-wobbleRangeLook.x, wobbleRangeLook.x), Random.Range(-wobbleRangeLook.y, wobbleRangeLook.y), Random.Range(-wobbleRangeLook.z, wobbleRangeLook.z));

            lookatPos.position = lookatStatic.position + randLook;
        }
    }

    public void NextWindow()
    {
        
        
        

        index++;

        if (audioClips[index] != null)
        {

            GetComponent<AudioSource>().clip = audioClips[index];
            GetComponent<AudioSource>().Play();

        }

        if (index == lookPoints.Length)
        {
            Debug.Log("Loading scene " + LevelToLoad);
            SceneManager.LoadScene(LevelToLoad);
        }

        else if (index >= lookPoints.Length)
        {
            index = lookPoints.Length - 1;

            //TODO finish //vllt hat Marc das halbwegs gefixed
        }
        else
        {

            reachedIndices[0] = false;
            reachedIndices[1] = false;

            lookatStatic.position = lookPoints[index].position;
            goalposStatic.position = lookPoints[index].position + new Vector3(0f, 0f, -howNear[index]);
            goalpos.position = goalposStatic.position;
            lookatPos.position = lookatStatic.position;
        }

       
    }
}
