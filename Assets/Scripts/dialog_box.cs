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
    public GameObject disablePanel;
    public GameObject raccoonImage;
    public GameObject possumImage;
    private float dialog_box_height;
    private Vector3 dialog_box_position;
    private float done_button_height;
    private Vector3 done_button_position;
    private float followup_box_height;
    private Vector3 followup_box_position;
    private bool give_chosen = false;
    private bool isOnFire, isSparkly, fellOff;
    private string[] raccoon_dialog = new string[] {
        "Thanks pal, this looks almost as good as the trash I just ate.",
        "Oh no, looks like I'll have to postpone my diet until tomorrow!",
        "One man's trash is another raccoon's treasure.",
        "Wow, I didn't even need to knock over a bin to get this.",
        "For me? Thanks! And I'll I got you was rabies.",
        "Ya know, I don't usually eat this sort of junk, but maybe just this once.",
        "Oh it's my lucky day, it must be trash day!",
        "Are you giving me this? Usually I just steal food!",
        "Gross! I hope you washed your hands.",
        "If you had thrown this out, I would've ended up eating it anyway.",
        "This tastes like garbage! Compliments to the chef!",
        "I wouldn't take this without saying thank you. That would be trashy."
    };
    private string[] eat_dialog = new string[] {
        "marshmallow! yum!",
        "now that's what I call R & R (roast and relaxation)!",
        "toasty AND tasty",
        "could this be more s'more scrumptious?!",
        "that was a mouthwatering marshmallow!",
        "...mmmmm... gooey..",
        "exquisite taste on a stick",
        "delicious, divine, delectable, do I need say s'more?"
    };

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
        isOnFire = marshmallow.GetComponent<marshmallow>().isOnFire;
        isSparkly = marshmallow.GetComponent<marshmallow>().isSparkly;
        fellOff = marshmallow.GetComponent<marshmallow>().fellOff;
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
        disablePanel.active = true;
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
        if (give_chosen)
        {
            show_raccoon();
        }
        marshmallow.GetComponent<marshmallow>().end_marshmallow();
        raccoonImage.active = false;
        possumImage.active = false;
        disablePanel.active = false;
        followupBoxHolder.transform.position = new Vector3(followup_box_position.x, followup_box_position.y - followup_box_height, followup_box_position.z);
        marshmallow.GetComponent<marshmallow>().re_enable_buttons();
        give_chosen = false;
    }

    public void eat()
    {
        marshmallow.GetComponent<marshmallow>().hide_marshmallow();
        hide_dialog_box();
        hide_done_button();
        if (fellOff)
        {
            followupText.GetComponent<Text>().text = "Do you usually eat things off the ground?";
        }
        else if (isOnFire)
        {
            followupText.GetComponent<Text>().text = "Ow! That one might of been too hot..";
        }
        else 
        {
            followupText.GetComponent<Text>().text = eat_dialog[Random.Range(0, eat_dialog.Length)];
        }
        show_followup_box();
    }

    public void give()
    {
        give_chosen = true;
        hide_dialog_box();
        hide_done_button();
        if (fellOff) 
        {
            followupText.GetComponent<Text>().text = "I don't mind that you dropped it, a little dirt never hurt anyone.";
        }
        else if (isOnFire)
        {
            followupText.GetComponent<Text>().text = "Thanks, this looks like it's flaming with flavor!";
        }
        else
        {
            followupText.GetComponent<Text>().text = raccoon_dialog[Random.Range(0, raccoon_dialog.Length)];
        }
        raccoonImage.active = true;
        marshmallow.GetComponent<marshmallow>().hide_marshmallow();
        show_followup_box();
    }

    public void inspect()
    {
        bool isNewBadge = marshmallow.GetComponent<marshmallow>().checkForAchievements();
        marshmallow.GetComponent<marshmallow>().hide_marshmallow();

        hide_dialog_box();
        hide_done_button();
        if (isNewBadge)
        {
            followupText.GetComponent<Text>().text = "Warmest congratulations! Based on my proffessional opinion you have earned a new badge!";
        }
        else 
        {
            followupText.GetComponent<Text>().text = "Deepest apologies, your marshmallow didn't pass inspection. Please try again!";
        }
        possumImage.active = true;
        show_followup_box();
    }

    private void show_raccoon() 
    {
        var color_1 = marshmallow.GetComponent<MeshRenderer>().material.GetColor("Color_1");
        var color_2 = marshmallow.GetComponent<MeshRenderer>().material.GetColor("Color_2");
        raccoons[game_data.raccoonCounter].GetComponent<raccoon>().update_marshmallow(color_1, color_2, isOnFire, isSparkly);
        raccoons[game_data.raccoonCounter].active = true;
        game_data.raccoonCounter++;
        if (game_data.raccoonCounter >= raccoons.Length) 
        {
            game_data.raccoonCounter = 0;
        }
    }
}