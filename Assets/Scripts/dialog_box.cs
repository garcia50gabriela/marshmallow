using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialog_box : MonoBehaviour
{
    public GameObject dialogBoxHolder;
    public GameObject doneButtonHolder;
    public GameObject marshmallow;
    private float dialog_box_height;
    private Vector3 dialog_box_position;
    private float done_button_height;
    private Vector3 done_button_position;

    // Start is called before the first frame update
    void Start()
    {
        dialog_box_height = dialogBoxHolder.GetComponent<RectTransform>().sizeDelta.y;
        dialog_box_position = dialogBoxHolder.transform.position;
        done_button_height = doneButtonHolder.GetComponent<RectTransform>().sizeDelta.y;
        done_button_position = doneButtonHolder.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show_done_button()
    {
        doneButtonHolder.transform.position = new Vector3(dialog_box_position.x, dialog_box_position.y + dialog_box_height, dialog_box_position.z);
    }

    public void show_dialog_box() 
    {
        dialogBoxHolder.transform.position = new Vector3(dialog_box_position.x, dialog_box_position.y + dialog_box_height, dialog_box_position.z);
    }

    public void hide_dialog_box()
    {
        dialogBoxHolder.transform.position = new Vector3(dialog_box_position.x, dialog_box_position.y - dialog_box_height, dialog_box_position.z);
    }

    public void eat()
    {
        hide_marshmallow();
        hide_dialog_box();
    }

    public void give()
    {
        hide_marshmallow();
        hide_dialog_box();
    }

    public void inspect()
    {
        hide_marshmallow();
        hide_dialog_box();
    }

    private void hide_marshmallow() 
    {
        marshmallow.SetActive(false);
    }
}