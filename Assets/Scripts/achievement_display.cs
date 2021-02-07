using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class achievement_display : MonoBehaviour
{

    public GameObject golden_badge;
    public GameObject raw_badge;
    public GameObject darkside_badge;
    public GameObject charred_badge;
    private static Dictionary<string, GameObject> badge_dict;
    // Start is called before the first frame update
    void Start()
    {
        badge_dict = new Dictionary<string, GameObject>() {
        {"Dark Side", darkside_badge},
        {"Good & Golden", golden_badge},
        {"UnRoasted", raw_badge},
        {"Completely Charred", charred_badge}
        };

        foreach (KeyValuePair<int, game_data.Achievement> a in game_data.AchievementDict)
        {
            if (a.Value.Earned) 
            {
                var badge = badge_dict[a.Value.Name];
                badge.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
