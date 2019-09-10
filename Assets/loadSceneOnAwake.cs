﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadSceneOnAwake : MonoBehaviour

{

    public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
