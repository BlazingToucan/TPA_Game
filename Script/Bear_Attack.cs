using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear_Attack : MonoBehaviour
{
    private int series;
    private Animator animator;
    float idle;
    // Start is called before the first frame update
    void Start()
    {
        series=0;
        animator = GetComponent<Animator>();
        idle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void hit()
    {
        if (series == 0)
        {
            animator.SetTrigger("Hit1");
            idle = 0;
        }
        else if (series == 1 && (idle > 0.6 && idle < 1.25f))
        {
            animator.SetTrigger("Hit2");
            idle = 0;
        }
        else if (series == 2 && (idle > 0.6f && idle < 1.25f))
        {
            animator.SetTrigger("Hit3");
            series = 0;
        }
    }
}
