using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    AudioSource btnSound;

    public void changeScene0()
    {
        SceneManager.LoadScene(0);
    }
    public void changeScene1()
    {
        //animator.SetTrigger("Fade");
        //SceneManager.LoadScene(1);
        StartCoroutine(Fade());
    }
    public void exitApp()
    {
        Application.Quit();
    }
    public IEnumerator Fade()
    {
        animator.SetTrigger("Fade");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
    
    public void playButton()
    {
        btnSound.enabled = false;
        btnSound.enabled = true;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        btnSound.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
