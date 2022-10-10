using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform camera;
    void Start()
    {
        camera = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        camera.Rotate(new Vector3(0, Random.Range(1f, 8f) * Time.deltaTime, 0));

    }
}
