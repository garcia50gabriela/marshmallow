using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class raccoon : MonoBehaviour
{
    public GameObject marshmallow;
    public GameObject marshmallow_fire;
    public GameObject marshmallow_sparkle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void update_marshmallow(Color color_1, Color color_2, bool isOnFire, bool isSparkly)
    {
        marshmallow.GetComponent<MeshRenderer>().material.SetColor("Color_1", color_1);
        marshmallow.GetComponent<MeshRenderer>().material.SetColor("Color_2", color_2);
        if (isOnFire) 
        {
            marshmallow_fire.active = true;
        }
        else
        {
            marshmallow_fire.active = false;
        }
        if (isSparkly)
        {
            marshmallow_sparkle.active = true;
        }
        else
        {
            marshmallow_sparkle.active = false;
        }
    }
}
