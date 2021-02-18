using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class marshmallow : MonoBehaviour
{
    public GameObject stick;
    public GameObject North, South, Achievements;
    public int burn_inc = 3;
    public int roast_inc = 1;
    private float n, s = 0f;
    public GameObject bottomInd;
    public GameObject topInd;
    public static float volumeMod;
    public GameObject topBar;
    public GameObject bottomBar;
    public GameObject topMarshmallow;
    public GameObject bottomMarshmallow;
    public float timePassedPerMallow = 0.0f;
    private int fireCounterPerMallow = 0;
    private bool isOnFire;
    public GameObject marshmallowFire;

    // Start is called before the first frame update
    void Start()
    {
        volumeMod = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateVisualIndicators();
        checkAndUpdateFlame();
        timePassedPerMallow += Time.deltaTime;

        if (n < 8f)
        {
            volumeMod = n;
        }

        else
        {
            volumeMod = 8;
        }
        
    }
    void updateVisualIndicators() 
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
            var newColor = new Color(red, green, blue, 1f);
            gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_2", newColor);
            topBar.GetComponent<Image>().color = newColor;
            if (n <= 10f)
            {
                topBar.GetComponent<RectTransform>().localScale = new Vector3(n * 0.1f, 1f, 1f);
            }
            North.GetComponent<UnityEngine.UI.Text>().text = (n * 10).ToString("F0") + "%";
            topMarshmallow.GetComponent<Image>().color = newColor;
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
            var newColor = new Color(red, green, blue, 1f);
            gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_1", new Color(red, green, blue, 1f));
            bottomBar.GetComponent<Image>().color = newColor;
            if (s <= 10f)
            {
                bottomBar.GetComponent<RectTransform>().localScale = new Vector3(s * 0.1f, 1f, 1f);
            }
            South.GetComponent<UnityEngine.UI.Text>().text = (s * 10).ToString("F0") + "%";
            bottomMarshmallow.GetComponent<Image>().color = newColor;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.name == "roast")
        {
            if (bottomInd.transform.position.y < topInd.transform.position.y)
            {
                n += (float)roast_inc * Time.deltaTime;
                //North.GetComponent<UnityEngine.UI.Text>().text = (n).ToString();
            }
            else if (bottomInd.transform.position.y > topInd.transform.position.y)
            {
                s += (float)roast_inc * Time.deltaTime;
                //South.GetComponent<UnityEngine.UI.Text>().text = (s).ToString();

            }
        }
        if (other.name == "burn")
        {
            if (bottomInd.transform.position.y < topInd.transform.position.y)
            {
                n += (float)burn_inc * Time.deltaTime;
                s += (float)roast_inc * Time.deltaTime;
                North.GetComponent<UnityEngine.UI.Text>().text = (n).ToString();
                South.GetComponent<UnityEngine.UI.Text>().text = (s).ToString();
            }
            else if (bottomInd.transform.position.y > topInd.transform.position.y)
            {
                s += (float)burn_inc * Time.deltaTime;
                n += (float)roast_inc * Time.deltaTime;
                South.GetComponent<UnityEngine.UI.Text>().text = (s).ToString();
                North.GetComponent<UnityEngine.UI.Text>().text = (n).ToString();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == "burn")
        {
            applyFlameProbability();
        }
    }
    public void applyFlameProbability() 
    {
        // 1 in every 250 updates
        var r = Random.Range(0, 3);
        if (r == 0) 
        {
            isOnFire = true;
            marshmallowFire.SetActive(true);
        }
    }
    public void checkAndUpdateFlame() 
    {
        if (isOnFire) 
        {
            s += (float)roast_inc * Time.deltaTime;
            n += (float)roast_inc * Time.deltaTime;
            if (Input.GetKeyDown("space"))
            {
                isOnFire = false;
                marshmallowFire.SetActive(false);
                fireCounterPerMallow++;
            }
        }
    }
    public void resetMarshmallow()
    {
        checkForAchievements(n, s);
        n = 0f;
        s = 0f;
        var white = new Color(1f, 1f, 1f, 1f);
        gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_1", white);
        gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_2", white);
        South.GetComponent<UnityEngine.UI.Text>().text = (s).ToString() + "%";
        North.GetComponent<UnityEngine.UI.Text>().text = (n).ToString() + "%";
        topBar.GetComponent<Image>().color = white;
        bottomBar.GetComponent<Image>().color = white;
        topMarshmallow.GetComponent<Image>().color = white;
        bottomMarshmallow.GetComponent<Image>().color = white;
        isOnFire = false;
        marshmallowFire.SetActive(false);
        timePassedPerMallow = 0.0f;
        fireCounterPerMallow = 0;
    }
    void checkForAchievements(float n, float s) 
    {
        UnityEngine.UI.Text achievementsText = Achievements.GetComponent<UnityEngine.UI.Text>();
        // dark side
        if ((n < 1 && s > 10 || s < 2 && n > 10) && !game_data.AchievementDict[0].Earned) 
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[0].Name);
            game_data.AchievementDict[0].Earned = true;
        }
        // good and golden
        else if (n < 9 && s < 9 && s > 7 && n > 7 && !game_data.AchievementDict[1].Earned)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[1].Name);
            game_data.AchievementDict[1].Earned = true;
        }
        // unroasted
        else if (n < 2 && s < 2 && !game_data.AchievementDict[2].Earned)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[2].Name);
            game_data.AchievementDict[2].Earned = true;
        }
        // charred
        else if (n > 10 && s > 10 && !game_data.AchievementDict[3].Earned)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[3].Name);
            game_data.AchievementDict[3].Earned = true;
        }
        // 6666
        else if (Mathf.Round(n * 10f) == 66 && Mathf.Round(s * 10f) == 66 && !game_data.AchievementDict[4].Earned)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[4].Name);
            game_data.AchievementDict[4].Earned = true;
        }
        // Rapid Roast
        else if (timePassedPerMallow <= 25f && n < 9 && s < 9 && s > 7 && n > 7 && !game_data.AchievementDict[5].Earned && game_data.AchievementDict[1].Earned)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[5].Name);
            game_data.AchievementDict[5].Earned = true;
        }
        // one flame
        else if (fireCounterPerMallow == 1)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[6].Name);
            game_data.AchievementDict[6].Earned = true;
        }
        // two flame
        else if (fireCounterPerMallow >= 2)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[7].Name);
            game_data.AchievementDict[7].Earned = true;
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
