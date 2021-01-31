using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadAchievementsScene()
    {
        SceneManager.LoadScene("Achievements");
    }
    public void loadGameScene()
    {
        SceneManager.LoadScene("marshmallow_roasting_skybox");
    }
}
