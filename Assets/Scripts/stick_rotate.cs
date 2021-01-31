using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stick_rotate : MonoBehaviour
{
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rotate stick
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        //transform.Translate(0, 0, scroll * speed, Space.Self);
        transform.Rotate(new Vector3(scroll * speed, 0, 0), Space.Self);
    }
}
