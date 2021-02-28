﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_manager : MonoBehaviour
{
    public GameObject raccoons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        DontDestroyOnLoad(raccoons);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
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
