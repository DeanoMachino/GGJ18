using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Parts Part;
    SpriteRenderer SR;

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        Part = (Parts)Random.Range(0, 3);
        switch (Part)
        {
            case Parts.RadioP1:
                //SR.sprite
                //SR.color = Color.blue;
                break;
            case Parts.RadioP2:
                //SR.sprite
                SR.color = Color.white;
                break;
            case Parts.RadioP3:
                //SR.sprite
                SR.color = Color.black;
                break;
            case Parts.RadioP4:
                //SR.sprite
                SR.color = Color.grey;
                break;
        }
        Destroy(gameObject, 5);
    }

    public void Collected()
    {
        Debug.Log("Collected");
    }

}