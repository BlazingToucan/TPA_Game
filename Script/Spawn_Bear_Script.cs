using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Bear_Script : MonoBehaviour
{
    public List<GameObject> bears;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       foreach (GameObject bear in bears)
        {
            if (!bear.activeInHierarchy)
            {
                StartCoroutine(respawnTime(bear));
                
            }
        }
    }
    IEnumerator respawnTime(GameObject bear)
    {
        yield return new WaitForSeconds(10);
        bear.gameObject.SetActive(true);
        bear.gameObject.GetComponent<Bear_Movement>().hp = 100;
    }
}
