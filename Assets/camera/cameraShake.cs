using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraShake : MonoBehaviour
{
    public static cameraShake instacne
    {
        get;
        set;
    }
    private CinemachineVirtualCamera cam;
    private float time;
    public void Awake()
    {
        instacne = this;
        cam = GetComponent<CinemachineVirtualCamera>();

    }
    private void Update()
    {
        if(time <Time.time)
        {
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        }
    }
    public void shakeCamera(float intensity, float t)
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        t += Time.time;
        time = t;
    }
    

}
