using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marshmallow : MonoBehaviour
{
    public GameObject stick;
    public GameObject North, South, Achievements;
    public int burn_inc = 3;
    private float roast = 0f;
    public int roast_inc = 1;
    private float n, s, e, w = 0f;
    public GameObject bottomInd;
    public GameObject topInd;
    public static float volumeMod;

    // Start is called before the first frame update
    void Start()
    {
        volumeMod = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (n > 0f) 
        {
            var currentColor = gameObject.GetComponent<MeshRenderer>().material.GetColor("Color_2");
            
            var red = currentColor.r;
            var blue = 1f - (n * 0.10f);
            var green = 1f - (n * 0.05f);

            if (green <= 0.5f) 
            {
                red = 1f - (n * 0.10f);
                green = 1f - (n * 0.10f);
            }
            
            if (red <= 0f)
                red = 0f;
            if (blue <= 0f)
                blue = 0f;
            if (green <= 0f)
                green = 0f;

            gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_2", new Color(red, green, blue, 1f));
        }
        if (s > 0f) 
        {
            var currentColor = gameObject.GetComponent<MeshRenderer>().material.GetColor("Color_1");

            var red = currentColor.r;
            var blue = 1f - (s * 0.10f);
            var green = 1f - (s * 0.05f);

            if (green <= 0.5f)
            {
                red = 1f - (s * 0.10f);
                green = 1f - (s * 0.10f);
            }

            if (red <= 0f)
                red = 0f;
            if (blue <= 0f)
                blue = 0f;
            if (green <= 0f)
                green = 0f;

            gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_1", new Color(red, green, blue, 1f));
        }

        if (n < 8f)
        {
            volumeMod = n;
        }

        else
        {
            volumeMod = 8;
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "roast")
        {
            if (stick.transform.localRotation.eulerAngles.x < 180 && stick.transform.rotation.eulerAngles.x > 0)
            {
                n += (float)roast_inc * Time.deltaTime;
                North.GetComponent<UnityEngine.UI.Text>().text = (n).ToString();
            }
            else if (stick.transform.localRotation.eulerAngles.x > 180 && stick.transform.rotation.eulerAngles.x < 360)
            {
                s += (float)roast_inc * Time.deltaTime;
                South.GetComponent<UnityEngine.UI.Text>().text = (s).ToString();

            }
        }
        if (other.name == "burn")
        {
            if (bottomInd.transform.position.x < topInd.transform.position.x)
            {
                n += (float)burn_inc * Time.deltaTime;
                s += (float)roast_inc * Time.deltaTime;
                North.GetComponent<UnityEngine.UI.Text>().text = (n).ToString();
                South.GetComponent<UnityEngine.UI.Text>().text = (s).ToString();
            }
            else if (bottomInd.transform.position.x > topInd.transform.position.x)
            {
                s += (float)burn_inc * Time.deltaTime;
                n += (float)roast_inc * Time.deltaTime;
                South.GetComponent<UnityEngine.UI.Text>().text = (s).ToString();
                North.GetComponent<UnityEngine.UI.Text>().text = (n).ToString();
            }
        }
    }
    public void resetMarshmallow()
    {
        checkForAchievements(n, s);
        n = 0f;
        s = 0f;
        gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_1", new Color(1f, 1f, 1f, 1f));
        gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_2", new Color(1f, 1f, 1f, 1f));
        South.GetComponent<UnityEngine.UI.Text>().text = (s).ToString();
        North.GetComponent<UnityEngine.UI.Text>().text = (n).ToString();
    }
    void checkForAchievements(float n, float s) 
    {
        UnityEngine.UI.Text achievementsText = Achievements.GetComponent<UnityEngine.UI.Text>();
        // dark side
        if (n < 1 && s > 10 || s < 2 && n > 10) 
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[0].Name);
            game_data.AchievementDict[0].Earned = true;
        }
        // good and golden
        if (n < 9 && s < 9 && s > 7 && n > 7)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[1].Name);
            game_data.AchievementDict[1].Earned = true;
        }
        // unroasted
        if (n < 2 && s < 2)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[2].Name);
            game_data.AchievementDict[2].Earned = true;
        }
        // charred
        if (n > 10 && s > 10)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[3].Name);
            game_data.AchievementDict[3].Earned = true;
        }
        StartCoroutine(FadeTextToZeroAlpha(3f, achievementsText));
    }

    public IEnumerator FadeTextToZeroAlpha(float t, UnityEngine.UI.Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
