using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class TPController : MonoBehaviour
{
    [SerializeField]
    private AudioSource lvlUp1, lvlUp2;
    [SerializeField]
    private Transform camera;

    public Text levelText;
    private Animator animator;
    private CharacterController controller;
    public static float maxHP, maxEXP, currEXP, currHP,atk;

    private float speed;
    private float maxRotateSpeed;
    private int level;
    private float currentVelocity;
    private float gravity = 10f;

    [SerializeField]
    private Slider hpBar,expBar;

    void checkEnemy()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = 6f;
        maxRotateSpeed = 0.1f;
        currHP  = maxHP = 300;
        maxEXP = 100;
        currEXP = 0;
        hpBar.value = 100;
        expBar.value = 0;
        atk = 10;
        level = 1;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //A D
        float horizontal = Input.GetAxisRaw("Horizontal");
        //W S
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized; //normalisasi agar speed konsisten
        Vector3 moveDirection = new Vector3();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += 6;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= 6;
        }
        //kalau ada input(gerak)
        if (currHP>0 && direction.magnitude >= 0.1f)
        {   

            //arah liat kemana
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref currentVelocity, maxRotateSpeed);
            
            moveDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            moveDirection = moveDirection.normalized;
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
        }

        moveDirection.y += (gravity * -1);
        moveDirection.x *= speed;
        moveDirection.z *= speed;
        controller.Move(moveDirection * Time.deltaTime);

        if (currEXP >= 100)
        {
            currHP = maxHP;
            lvlUp1.enabled = false;
            lvlUp2.enabled = false;
            lvlUp1.enabled = true;
            lvlUp2.enabled = true;
            while (currEXP >= 100)
            {
                level++;
                currEXP -= 100;
            }
        }

        //hp exp
        hpBar.value = (float)currHP/maxHP;
        expBar.value = (float)currEXP/maxEXP;
        levelText.text = "Level " + level.ToString();

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState=CursorLockMode.Locked;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
