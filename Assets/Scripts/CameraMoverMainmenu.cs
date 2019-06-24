using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoverMainmenu : MonoBehaviour
{
    public Vector3 wobbleRange;
    public Vector3 wobbleRangeLook;
    public float lerpSpeed = 0.1f;
    public float lerpSpeedLook = 0.1f;
    public float lerpSpeedWabble = 0.1f;
    public float lerpSpeedWabbleLook = 0.1f;
    public float lazyWeight = 0.9f;
    public float damperTime = 0.9f;
    public float maxSpeed = 0.9f;
    public float goFastSpeedup = 5f;
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

    [SerializeField]
    private float reachedThresh = 1f;
    private bool[] reachedIndices = new bool[2];

    private Vector3 oldMoveDelta = Vector3.zero;
    private Vector3 oldMoveDelta2 = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        reachedIndices[0] = false;
        reachedIndices[1] = false;
    }

    private float timeCount = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, goalpos.position, ref oldMoveDelta, damperTime * (reachedIndices[0] ? 1f : goFastSpeedup), maxSpeed);
        lookat.position = Vector3.SmoothDamp(lookat.position, lookatPos.position, ref oldMoveDelta2, damperTime * (reachedIndices[1] ? 1f : goFastSpeedup), maxSpeed);

        transform.LookAt(lookat);



        //oldMoveDelta = transform.position - oldPos;

        if (Vector3.Distance(transform.position, goalpos.position) < reachedThresh)
        {
            Debug.Log("Reached");
            reachedIndices[0] = true;
            Vector3 rand = new Vector3(Random.Range(-wobbleRange.x, wobbleRange.x), Random.Range(-wobbleRange.y, wobbleRange.y), Random.Range(-wobbleRange.z, wobbleRange.z));

            goalpos.position = goalposStatic.position + rand;
        }

        if (Vector3.Distance(lookat.position, lookatPos.position) < reachedThresh)
        {
            //Debug.Log("Reached lookat");
            Vector3 randLook = new Vector3(Random.Range(-wobbleRangeLook.x, wobbleRangeLook.x), Random.Range(-wobbleRangeLook.y, wobbleRangeLook.y), Random.Range(-wobbleRangeLook.z, wobbleRangeLook.z));

            lookatPos.position = lookatStatic.position + randLook;
        }

    }

    public void SetPosition(Vector3 pos)
    {
        goalposStatic.position = pos;
        reachedIndices[0] = false;
        reachedIndices[1] = false;
        
        goalpos.position = goalposStatic.position;
        lookatPos.position = lookatStatic.position;
    }

    public void SetLookat(Vector3 pos)
    {
        lookatStatic.position = pos;
        reachedIndices[0] = false;
        reachedIndices[1] = false;
        
        goalpos.position = goalposStatic.position;
        lookatPos.position = lookatStatic.position;
    }
}
