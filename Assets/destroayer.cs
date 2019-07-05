using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroayer : MonoBehaviour
{
   public float lifeTime = 10f;

    // Update is called once per frame
    void Update()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
                Destruction();
        }
    }

    private void Destruction()
    {

        Destroy(this.gameObject);

    }
}
