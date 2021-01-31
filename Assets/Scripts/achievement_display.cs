using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class achievement_display : MonoBehaviour
{

    public GameObject achievement_text;
    // Start is called before the first frame update
    void Start()
    {
        string text = "Badge \t\t\t Earned?\n\n";
        foreach (KeyValuePair<int, game_data.Achievement> a in game_data.AchievementDict)
        {
            text += a.Value.Name + ":\t\t\t" + a.Value.Earned + "\n";
        }
        achievement_text.GetComponent<UnityEngine.UI.Text>().text = text;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
