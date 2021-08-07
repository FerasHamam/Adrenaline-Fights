using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public GameObject cam;
    [SerializeField] float parallaxEffect;
    float startPos,length;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        startPos = transform.position.x;
    }
    void FixedUpdate()
    {
        if (cam.name != GameObject.FindGameObjectWithTag("MainCamera").name)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera");
        }
        float dist = cam.transform.position.x * parallaxEffect; 
        transform.position = new Vector3(startPos+dist, transform.position.y, transform.position.z);
    }
}
