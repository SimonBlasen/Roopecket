using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAffectedList : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private List<Transform> affectedObjects = new List<Transform>();

    public List<Transform> AffectedObjects
    { get
        {
            return affectedObjects;
        }
        set
        {
            affectedObjects = value;
        }
    }

    private static ExplosionAffectedList inst = null;
    public static ExplosionAffectedList Inst
    {
        get
        {
            if (inst == null)
            {
                GameObject instGo = new GameObject("Explosion Affected List");
                instGo.AddComponent<ExplosionAffectedList>();
                inst = instGo.GetComponent<ExplosionAffectedList>();
            }

            return inst;
        }
    }
}
