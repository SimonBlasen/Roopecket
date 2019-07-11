using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Arrow3D : MonoBehaviour
{
    public UnityEvent myUnityEvent;

    Vector3 spawn;
    Vector3 selected;

    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.position;
        selected = spawn + new Vector3(0f, 0f, -0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
