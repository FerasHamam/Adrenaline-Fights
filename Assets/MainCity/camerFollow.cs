using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class camerFollow : MonoBehaviour
{
    GameObject player;
    [SerializeField]float timeOffSet;
    [SerializeField]Vector2 posOffset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = player.transform.position;
        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;
        transform.position = Vector3.Lerp(startPos, endPos, timeOffSet * Time.deltaTime);
    }
}
