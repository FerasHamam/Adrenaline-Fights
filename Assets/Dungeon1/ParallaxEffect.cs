using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject cam;
    [SerializeField]
    float parallaxEffect;
    Vector3 camPos;
    float startPos;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
         camPos = cam.transform.position;
        transform.position = new Vector3((camPos.x * parallaxEffect) + startPos, transform.position.y, transform.position.z);
    }
}
