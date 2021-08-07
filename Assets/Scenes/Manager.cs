using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    public GameObject bluePlayer;
    public GameObject redPlayer;
    public GameObject whitePlayer;
    public GameObject gameCanvas;
    public Vector3 startPos = Vector3.zero;

    void Awake()
    {
        gameCanvas.SetActive(true);
    }
}
