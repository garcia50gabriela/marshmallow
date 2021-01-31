using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marshmallow_stick : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 targetPos;
    public Vector3 ogPos;
    public bool isMoving;
    public GameObject fire;
    const int MOUSE = 0;
    private float zpos;
    // Use this for initialization1
    void Start()
    {

        targetPos = transform.position;
        ogPos = transform.position;
        zpos = transform.position.z;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        // move around fire
        if (Input.GetMouseButton(MOUSE))
        {
            SetTargetPosition();
        }
        else
        {
            //SetOriginalPosition();
        }
        if (isMoving)
        {
            MoveObject();
        }
    }
        
    void SetTargetPosition()
    {
        Plane plane = new Plane(Vector3.forward, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0f;

        if (plane.Raycast(ray, out point)) 
        {
            targetPos = ray.GetPoint(point);
            if (targetPos.x < ogPos.x - 2.5 || targetPos.x > ogPos.x + 2.5)
            {
                targetPos = ogPos;
            }
            else 
            {
                targetPos.x = ogPos.x + ((ogPos.x - targetPos.x) * 5f);
                targetPos.y = ogPos.y + ((ogPos.y - targetPos.y) * 5f);
                targetPos.z = fire.transform.position.z;
            }
            
        }

        isMoving = true;
    }
    void SetOriginalPosition()
    {
        targetPos = new Vector3(ogPos.x, 100f, ogPos.y);

        isMoving = true;
    }
    void MoveObject()
    {
        // pivot
        transform.LookAt(targetPos);
        
        //3d ish
        //transform.LookAt(fire.transform.position);

        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPos.x, targetPos.y, zpos), speed * Time.deltaTime);
        //if (transform.position == targetPos)
        //isMoving = false;

    }
}
