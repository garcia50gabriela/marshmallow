using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialog_box : MonoBehaviour
{
    public GameObject dialogBoxHolder;
    public GameObject doneButtonHolder;
    public GameObject followupBoxHolder;
    public GameObject followupText;
    public GameObject marshmallow;
    public GameObject[] raccoons;
    private float dialog_box_height;
    private Vector3 dialog_box_position;
    private float done_button_height;
    private Vector3 done_button_position;
    private float followup_box_height;
    private Vector3 followup_box_position;

    // Start is called before the first frame update
    void Start()
    {
        dialog_box_height = dialogBoxHolder.GetComponent<RectTransform>().sizeDelta.y;
        dialog_box_position = dialogBoxHolder.transform.position;
        done_button_height = doneButtonHolder.GetComponent<RectTransform>().sizeDelta.y;
        done_button_position = doneButtonHolder.transform.position;
        followup_box_height = followupBoxHolder.GetComponent<RectTransform>().sizeDelta.y;
        followup_box_position = followupBoxHolder.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void show_done_button()
    {
        doneButtonHolder.transform.position = new Vector3(done_button_position.x, done_button_position.y + done_button_height, done_button_position.z);
    }

    public void hide_done_button()
    {
        doneButtonHolder.transform.position = new Vector3(done_button_position.x, done_button_position.y - done_button_height, done_button_position.z);
    }

    public void show_dialog_box() 
    {
        dialogBoxHolder.transform.position = new Vector3(dialog_box_position.x, dialog_box_position.y + dialog_box_height, dialog_box_position.z);
    }

    public void hide_dialog_box()
    {
        dialogBoxHolder.transform.position = new Vector3(dialog_box_position.x, dialog_box_position.y - dialog_box_height, dialog_box_position.z);
    }

    public void show_followup_box()
    {
        followupBoxHolder.transform.position = new Vector3(followup_box_position.x, followup_box_position.y + followup_box_height, followup_box_position.z);
    }

    public void hide_followup_box()
    {
        followupBoxHolder.transform.position = new Vector3(followup_box_position.x, followup_box_position.y - followup_box_height, followup_box_position.z);
        marshmallow.GetComponent<marshmallow>().re_enable_buttons();
    }

    public void eat()
    {
        hide_marshmallow();
        hide_dialog_box();
        hide_done_button();
        followupText.GetComponent<Text>().text = "Yum!";
        show_followup_box();
    }

    public void give()
    {
        hide_dialog_box();
        hide_done_button();
        followupText.GetComponent<Text>().text = "You gave it to a nearby racoon, they seemed appreciative!";
        show_raccoon();
        hide_marshmallow();
        show_followup_box();
    }

    public void inspect()
    {
        bool isNewBadge = marshmallow.GetComponent<marshmallow>().checkForAchievements();
        hide_marshmallow();

        hide_dialog_box();
        hide_done_button();
        if (isNewBadge)
        {
            followupText.GetComponent<Text>().text = "Warmest congratulations! You earned a new badge!";
        }
        else 
        {
            followupText.GetComponent<Text>().text = "Sorry, your marshmallow didn't pass inspection. Please try again!";
        }
        show_followup_box();
    }

    private void hide_marshmallow() 
    {
        marshmallow.GetComponent<marshmallow>().end_marshmallow();
    }

    private void show_raccoon() 
    {
        var color_1 = marshmallow.GetComponent<MeshRenderer>().material.GetColor("Color_1");
        var color_2 = marshmallow.GetComponent<MeshRenderer>().material.GetColor("Color_2");
        var isOnFire = marshmallow.GetComponent<marshmallow>().isOnFire;
        var isSparkly = marshmallow.GetComponent<marshmallow>().isSparkly;
        raccoons[game_data.raccoonCounter].GetComponent<raccoon>().update_marshmallow(color_1, color_2, isOnFire, isSparkly);
        raccoons[game_data.raccoonCounter].active = true;
        game_data.raccoonCounter++;
        if (game_data.raccoonCounter >= raccoons.Length) 
        {
            game_data.raccoonCounter = 0;
        }
    }
}