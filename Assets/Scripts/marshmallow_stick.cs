using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marshmallow_stick : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 targetPos;
    public Vector3 ogPos;
    public Vector3 clickPos;
    public Vector3 relPos;
    public Vector3 adjPos;
    public bool isMoving;
    public GameObject fire;
    const int MOUSE = 0;
    private float zpos;
    public GameObject dialogBox;
    //private static float VolumeModifier;
    //AudioSource nRoastSFX;

    // Use this for initialization1
    void Start()
    {

        targetPos = transform.position;
        ogPos = transform.position;
        zpos = transform.position.z;
        clickPos = Input.mousePosition;
        relPos = Input.mousePosition;
        isMoving = false;
        //nRoastSFX = GetComponent<AudioSource>();
        //nRoastSFX.Play(0);
        //nRoastSFX.Pause();
        //VolumeModifier = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (game_data.marshmallowIsPresent)
        {
            // move around fire
            if (Input.GetMouseButton(MOUSE))
            {
                if (Input.mousePosition.y > 25)
                {
                    SetTargetPosition();
                    dialogBox.GetComponent<dialog_box>().hide_done_button();
                }
                //marshmallow.newRoastSFX.UnPause();
            }
            else
            {
                SetOriginalPosition();
                dialogBox.GetComponent<dialog_box>().show_done_button();
                //marshmallow.newRoastSFX.Pause();
            }
            if (isMoving)
            {
                MoveObject();
            }
            if (Input.GetMouseButtonDown(0))
            {
                clickPos = Input.mousePosition;
                // Debug.Log("Left click detected at " + clickPos.x + " , " + clickPos.y + " , " + clickPos.z );
            }
            else
            {
                // nothing
            }
            //VolumeModifier = (marshmallow.volumeMod / 8);
            //Debug.Log("Volume Modifier = " + VolumeModifier);
            //nRoastSFX.volume = VolumeModifier;
        }
    }
        
    void SetTargetPosition()
    {
        adjPos = new Vector3(600,50,0);
        // ^
        // This position adjustment is based on a 1200x600 resolution. 
        // If we want to support multiple resolutions this would need actual math based on screen size.
        // X would be Screen width / 2. Y would be screen height / 12

        relPos = Input.mousePosition - clickPos + adjPos;
        Plane plane = new Plane(Vector3.forward, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(relPos);
        float point = 0f;

        if (plane.Raycast(ray, out point)) 
        {
            targetPos = ray.GetPoint(point);
            if (targetPos.x < ogPos.x - 2.5 || targetPos.x > ogPos.x + 2.5 || targetPos.y < 1f)
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
