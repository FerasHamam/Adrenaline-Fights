using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuController : MonoBehaviour
{
    public GameObject MapMenu;
    public GameObject options;
    public Animator anim;
    public Slider slide;
    public void Start()
    {
        FindObjectOfType<AudioManager>().playSound("General");
    }
    public void activateMapMenu()
    {
        FindObjectOfType<AudioManager>().PlaySoundOneShot("Touch");
        MapMenu.SetActive(true);
    }
    public void activcateOptions()
    {
        FindObjectOfType<AudioManager>().PlaySoundOneShot("Touch");
        options.SetActive(true);
    }
    public void startMap1()
    {
        FindObjectOfType<AudioManager>().PlaySoundOneShot("Touch");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void startMap2()
    {
        FindObjectOfType<AudioManager>().PlaySoundOneShot("Touch");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void deActivateOptions()
    {   
        
        FindObjectOfType<AudioManager>().PlaySoundOneShot("Touch");
        FindObjectOfType<AudioManager>().controlSounds(slide.value);
        options.SetActive(false);
    }
    public void deActivateMapMenu()
    {
        FindObjectOfType<AudioManager>().PlaySoundOneShot("Touch");
        MapMenu.SetActive(false);
    }

    public void quit()
    {
        FindObjectOfType<AudioManager>().PlaySoundOneShot("Touch");
        Application.Quit();
    }
}
