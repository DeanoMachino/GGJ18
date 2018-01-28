using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Parts Part;
    SpriteRenderer SR;

    public Sprite part_1;
    public Sprite part_2;
    public Sprite part_3;
    public Sprite part_4;

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        Part = (Parts)Random.Range(0, 4);
        //Debug.Log(Part);
        switch (Part)
        {
            case Parts.RadioP1:
                SR.sprite = part_1;
                break;
            case Parts.RadioP2:
                SR.sprite = part_2;
                break;
            case Parts.RadioP3:
                SR.sprite = part_3;
                break;
            case Parts.RadioP4:
                SR.sprite = part_4;
                break;
        }
        Destroy(gameObject, 10);
    }
}