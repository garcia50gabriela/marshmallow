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
    public GameObject topBar;
    public GameObject bottomBar;
    public GameObject topMarshmallow;
    public GameObject bottomMarshmallow;
    public float timePassedPerMallow = 0.0f;
    private int fireCounterPerMallow = 0;
    public bool isOnFire;
    public GameObject marshmallowFire;
    public bool isNewBadge;
    public GameObject sparkles;
    public bool isSparkly;
    public bool fellOff;
    public GameObject newMarshmallowButton;
    public GameObject admireAchievementsButton;
    public GameObject smallMarshmallow;
    public GameObject smallStick;
    private UnityEngine.UI.Text achievementsText;
    private Vector3 startPosition;
    private Quaternion startRotation;

    //SFX
    public static float nVolMult;
    public float nVolMod;
    public static float sVolMult;
    public float sVolMod;
    public AudioSource nRoastSFX;
    public AudioSource sRoastSFX;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.localPosition;
        startRotation = gameObject.transform.localRotation;
        achievementsText = Achievements.GetComponent<UnityEngine.UI.Text>();

        nVolMult = 0;
        sVolMult = 0;
        nVolMod = 0f;
        sVolMod = 0f;
        nRoastSFX = bottomInd.GetComponent<AudioSource>();
        sRoastSFX = topInd.GetComponent<AudioSource>();
        nRoastSFX.Play(0);
        sRoastSFX.Play(0);
        
        //nRoastSFX.Pause();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            smallStick.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        updateVisualIndicators();
        checkAndUpdateFlame();
        timePassedPerMallow += Time.deltaTime;

        //if (Input.GetMouseButton(MOUSE))
        //{
        //    nRoastSFX.UnPause();
        //}
        //else
        //{
        //    nRoastSFX.Pause();
        //}

        if (n < 10f)
        {
            nVolMult = n;
        }

        else
        {
            nVolMult = 10;
        }

        if (s < 10f)
        {
            sVolMult = s;
        }

        else
        {
            sVolMult = 10;
        }

        nVolMod = (nVolMult / 20);
        nRoastSFX.volume = nVolMod;
        sVolMod = (sVolMult / 20);
        sRoastSFX.volume = sVolMod;
    }
    void updateVisualIndicators() 
    {
        // marshmallow colors
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
            smallMarshmallow.GetComponent<MeshRenderer>().material.SetColor("Color_2", newColor);
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
            gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_1", newColor);
            smallMarshmallow.GetComponent<MeshRenderer>().material.SetColor("Color_1", newColor);
            bottomBar.GetComponent<Image>().color = newColor;
            if (s <= 10f)
            {
                bottomBar.GetComponent<RectTransform>().localScale = new Vector3(s * 0.1f, 1f, 1f);
            }
            South.GetComponent<UnityEngine.UI.Text>().text = (s * 10).ToString("F0") + "%";
            bottomMarshmallow.GetComponent<Image>().color = newColor;
        }
        //sparkles
        if (gameObject.GetComponent<MeshRenderer>().enabled)
        {
            if (n < 9 && s < 9 && s > 7 && n > 7)
            {
                sparkles.SetActive(true);
                isSparkly = true;
            }
            else
            {
                sparkles.SetActive(false);
                isSparkly = false;
            }
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
        fallOffPossibility();
    }
    void fallOffPossibility()
    {
        // add probability?
        if ((bottomInd.transform.position.y < topInd.transform.position.y) && s >= 10 && n <=2)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            fellOff = true;
        }
        else if ((bottomInd.transform.position.y > topInd.transform.position.y) && n >= 10 && s <= 2)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            fellOff = true;
        }
    }
    // stick meter and flame chance
    void OnTriggerEnter(Collider other)
    {
        if (!fellOff) 
        {
            if (other.name == "roast")
            {
                smallStick.transform.eulerAngles = new Vector3(0, 0, -20);
            }
            if (other.name == "burn")
            {
                smallStick.transform.eulerAngles = new Vector3(0, 0, -40);
            }
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if (!fellOff)
        {
            if (other.name == "burn")
            {
                applyFlameProbability();
                smallStick.transform.eulerAngles = new Vector3(0, 0, -20);
            }
            if (other.name == "roast")
            {
                smallStick.transform.eulerAngles = new Vector3(0, 0, 0);
            }
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
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.transform.localPosition = startPosition;
        gameObject.transform.localRotation = startRotation;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        smallMarshmallow.GetComponent<MeshRenderer>().enabled = true;
        game_data.marshmallowIsPresent = true;
        newMarshmallowButton.GetComponent<Button>().interactable = false;
        admireAchievementsButton.GetComponent<Button>().interactable = false;

        //checkForAchievements(n, s);
        n = 0f;
        s = 0f;
        var white = new Color(1f, 1f, 1f, 1f);
        gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_1", white);
        gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_2", white);
        smallMarshmallow.GetComponent<MeshRenderer>().material.SetColor("Color_1", white);
        smallMarshmallow.GetComponent<MeshRenderer>().material.SetColor("Color_2", white);
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
    public bool checkForAchievements() 
    {

        isNewBadge = false;
        
        // fell off
        if (fellOff && !game_data.AchievementDict[10].Earned)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[10].Name);
            game_data.AchievementDict[10].Earned = true;
            isNewBadge = true;
        }
        // dark side
        else if ((n < 1 && s > 10 || s < 2 && n > 10) && !game_data.AchievementDict[0].Earned) 
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[0].Name);
            game_data.AchievementDict[0].Earned = true;
            isNewBadge = true;
        }
        // good and golden
        else if (n < 9 && s < 9 && s > 7 && n > 7 && !game_data.AchievementDict[1].Earned)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[1].Name);
            game_data.AchievementDict[1].Earned = true;
            isNewBadge = true;
        }
        // unroasted
        else if (n < 2 && s < 2 && !game_data.AchievementDict[2].Earned)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[2].Name);
            game_data.AchievementDict[2].Earned = true;
            isNewBadge = true;
        }
        // charred
        else if (n > 10 && s > 10 && !game_data.AchievementDict[3].Earned)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[3].Name);
            game_data.AchievementDict[3].Earned = true;
            isNewBadge = true;
        }
        // Rapid Roast
        else if (timePassedPerMallow <= 25f && n < 9 && s < 9 && s > 7 && n > 7 && !game_data.AchievementDict[5].Earned && game_data.AchievementDict[1].Earned)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[5].Name);
            game_data.AchievementDict[5].Earned = true;
            isNewBadge = true;
        }
        // one flame
        else if (fireCounterPerMallow == 1 && !game_data.AchievementDict[6].Earned)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[6].Name);
            game_data.AchievementDict[6].Earned = true;
            isNewBadge = true;
        }
        // two flame
        else if (fireCounterPerMallow >= 2 && !game_data.AchievementDict[7].Earned)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[7].Name);
            game_data.AchievementDict[7].Earned = true;
            isNewBadge = true;
        }
        // on fire
        else if (isOnFire && !game_data.AchievementDict[8].Earned)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[8].Name);
            game_data.AchievementDict[8].Earned = true;
            isNewBadge = true;
        }
        // slow roast
        else if (timePassedPerMallow > 30f && !game_data.AchievementDict[9].Earned)
        {
            achievementsText.text = ("New Achievement! " + game_data.AchievementDict[9].Name);
            game_data.AchievementDict[9].Earned = true;
            isNewBadge = true;
        }
        // we need an if statement here!
        if (isNewBadge == true)
        {
            StartCoroutine(FadeTextToZeroAlpha(6f, achievementsText));
        }
        return isNewBadge;

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

    public void hide_marshmallow()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        smallMarshmallow.GetComponent<MeshRenderer>().enabled = false;
        marshmallowFire.SetActive(false);
        sparkles.SetActive(false);
        game_data.marshmallowIsPresent = false;
    }

    public void end_marshmallow() 
    {
        n = 0f;
        s = 0f;

        isSparkly = false;
        isOnFire = false;
        fellOff = false;

    }

    public void re_enable_buttons()
    {
        newMarshmallowButton.GetComponent<Button>().interactable = true;
        admireAchievementsButton.GetComponent<Button>().interactable = true;
    }
}
