using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pop_up_text : MonoBehaviour
{
    private bool animationOver = false;
    private Animation anim;
    public float animdelay;
    public GameObject Text;

    private void Start()
    {
       
        Text.SetActive(true);
        anim = gameObject.GetComponent<Animation>();
        anim["text_pop_down"].layer = 123;
        animationOver = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        { 
            print("down");
            anim.Play("text_pop_down");
            Invoke("CloseText", animdelay);
        }
    }
    private void OnMouseDown()
    {
        print("down");
        anim.Play("text_pop_down");
        Invoke("CloseText", animdelay);


    }

    private void CloseText()
    {

        Text.SetActive(false);
       

    }

}
