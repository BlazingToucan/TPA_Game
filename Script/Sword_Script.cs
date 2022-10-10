using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Sword_Script : MonoBehaviour
{
    public Text pickUpMessage;
    public AudioSource equipSound;

    [SerializeField] private Transform player, weaponContainer;

    Rigidbody rb;
    BoxCollider bc;

    private float range;

    private bool equip;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        equip = false;
        range = 2f;
        pickUpMessage.enabled = false;
        equipSound.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = player.position - transform.position;
        if (distance.magnitude <= range && !equip)
        {
            pickUpMessage.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                pick();
            }
        }
        else
        {
            pickUpMessage.enabled = false;
        }
        
    }
    private void pick()
    {
        equipSound.enabled = true;
        bc.isTrigger = true;
        rb.isKinematic = true;
        equip = true;
        pickUpMessage.enabled = false;
        transform.SetParent(weaponContainer);
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
    }

    public bool holding()
    {
        return equip;
    }

}
