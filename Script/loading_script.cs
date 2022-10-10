using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loading_script : MonoBehaviour
{
    public Slider slider;

    
    void Start()
    {
        StartCoroutine(loading());
    }

    // Update is called once per frame

    IEnumerator loading()
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(2);
        while (!scene.isDone)
        {
            float progress = Mathf.Clamp01(scene.progress/0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
