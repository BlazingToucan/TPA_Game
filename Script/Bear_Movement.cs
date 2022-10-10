using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Bear_Movement : MonoBehaviour
{
    public AudioSource playerGotHit;

    public float hp;
    //public bool hitReady1, hitReady2, hitReady3;

    [SerializeField]
    private NavMeshAgent bear;

    [SerializeField]
    private Slider hpBar;

    [SerializeField]
    private Transform player,bearTr;

    private Animator animationBear;

    private int series;
    float idle;
    private bool isReady;

    void playerGotHitSound()
    {
        playerGotHit.enabled = false;
        playerGotHit.enabled = true;
    }
    void hit()
    {
        if (series == 0 && TPController.currHP>0)
        {
            animationBear.SetTrigger("Attack1");
            series++;
            idle = 0;
            TPController.currHP -= 5;
            playerGotHitSound();
        }
        else if (series == 1 && (idle > 1 && idle < 1.5f) && TPController.currHP > 0)
        {
            animationBear.SetTrigger("Attack2");
            series++;
            idle = 0;
            TPController.currHP -= 10;
            playerGotHitSound();
        }
        else if (series == 2 && (idle > 1 && idle < 1.5f) && TPController.currHP > 0)
        {
            animationBear.SetTrigger("Attack3");
            series++;
            idle = 0;
            isReady = false;
            TPController.currHP -= 15;
            playerGotHitSound();
        }
        else if(idle>=1.5f)
        {
            series = 0;
            isReady = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animationBear = GetComponent<Animator>();
        series = 0;
        idle = 0;
        hp = 100;
        playerGotHit.enabled=false;
    //hitReady1 = hitReady2 = hitReady3 = true;
}

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(bearTr.position, player.position);
        bear.SetDestination(player.position);

        if (distance > 3 && distance < 15)
        {
            animationBear.SetBool("Run Forward", true);
            bear.isStopped = false;
        }
        else if (distance >= 15)
        {
            bear.isStopped = true;
            animationBear.SetBool("Run Forward", false);
            animationBear.SetBool("Idle", true);
            //animationBear.SetTrigger("Attack1");
        }
        else if (distance <= 3)
        {
            bear.isStopped = true;
            animationBear.SetBool("Run Forward", false);
            animationBear.SetBool("Idle", true);
            if (isReady)
            {
                hit();
            }
            else if (idle >= 1.5f)
            {
                series = 0;
                isReady = true;
                //hitReady1 = hitReady2 = hitReady3 = true;
            }
        }

        hpBar.value = (float)(hp / 100);
        if(hp <= 0)
        {
            bear.gameObject.SetActive(false);
        }

        idle += Time.deltaTime;
    }
    
}
