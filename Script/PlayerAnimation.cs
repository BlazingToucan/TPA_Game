using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerAnimation : MonoBehaviour
{
    public AudioSource punch1, punch2, punch3, slash1, slash2, runSound, deathSound;

    [SerializeField]
    private Sword_Script sc;

    GameObject[] enemies;

    private bool holdingSword;

    [SerializeField]
    Animator animatorFade;

    private Animator animator;

    float time,idle;
    int series;
    private bool live;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        time = idle = 0;
        holdingSword = false;
        series = 0;
        live = true;

        punch1.enabled = false; 
        punch2.enabled = false;
        punch3.enabled = false;
        slash1.enabled = false;
        slash2.enabled = false;
        runSound.enabled = false;
        deathSound.enabled = false;
    }

    void hit()
    {
        if (holdingSword == true)
        {
            if (series == 0)
            {
                idle = 0;
                animator.SetTrigger("Slash1");
                slash1.enabled = false;
                slash1.enabled = true;
                hitEnemy();
            }
            else if (series == 1 && (idle > 0.6 && idle < 1.25f))
            {
                animator.SetTrigger("Slash2");
                slash2.enabled = false;
                slash2.enabled = true;
                hitEnemy();
            }
        }
        else if (holdingSword == false) //tangan kosong
        {
            if (series == 0)
            {
                animator.SetTrigger("Hit1");
                idle = 0;
                punch1.enabled = false;
                punch1.enabled = true;
                hitEnemy();
            }
            else if (series == 1 && (idle>0.6 && idle<1.25f))
            {
                animator.SetTrigger("Hit2");
                idle = 0;
                punch2.enabled = false;
                punch2.enabled = true;
                hitEnemy();
            }
            else if (series == 2 && (idle > 0.6f && idle < 1.25f))
            {
                animator.SetTrigger("Hit3");
                series = 0;
                punch3.enabled = false;
                punch3.enabled = true;
                hitEnemy();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        holdingSword = sc.holding();

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        float dir = direction.magnitude;

        //movement wasd, strafing, running
        if (direction.magnitude > 0)
        {
            if (vertical == 0 && horizontal>0)
            {
                animator.SetBool("IsStrafeRight", true);
            }
            else if (vertical == 0 && horizontal < 0)
            {
                animator.SetBool("IsStrafeLeft", true);
            }
            else
            {
                animator.SetBool("IsRunning", true);
                runSound.enabled = true;
                animator.SetBool("IsStrafeLeft", false);
                animator.SetBool("IsStrafeRight", false);
            }
        }
        else
        {
            //idle
            animator.SetBool("IsRunning", false);
            runSound.enabled = false;
            animator.SetBool("IsStrafeLeft", false);
            animator.SetBool("IsStrafeRight", false);
        }

        //rolling
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("IsRolling", true);
        }
        else
        {
            animator.SetBool("IsRolling", false);
        }

        //pukul
        if (Input.GetMouseButtonDown(0))
        {
            hit();
            series++;
        }
        else
        {
            if (idle>=1.25f)
            {
                series = 0;
            }
        }
        //time += Time.deltaTime;
        idle += Time.deltaTime;

        isDeath();

    }
    
    private void isDeath()
    {
        if (TPController.currHP <= 0 && live==true)
        {
            animator.SetTrigger("IsDeath");
            deathSound.enabled = true;
            live = false;
            StartCoroutine(changeScene());
        }
    }
    IEnumerator changeScene()
    {
        animatorFade.SetTrigger("Fade");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(3);
    }

    void hitEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            if ((enemy.transform.position - transform.position).magnitude <= 4f)
            {
                if (holdingSword == true)
                {
                    if (enemy.GetComponent<Bear_Movement>().hp - (15 + TPController.atk) <= 0)
                    {
                        Debug.Log("mati");
                        TPController.currEXP += 25;
                    }
                    Debug.Log("slash");
                    enemy.GetComponent<Bear_Movement>().hp -= (15 + TPController.atk);
                }
                else 
                {
                        Debug.Log("pukul");
                        if (enemy.GetComponent<Bear_Movement>().hp - TPController.atk <= 0)
                        {
                            Debug.Log("mati");
                            TPController.currEXP += 25;
                        }
                        enemy.GetComponent<Bear_Movement>().hp -= TPController.atk;
                }
            }
        }
    }
}
