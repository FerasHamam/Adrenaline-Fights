using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UiManager : MonoBehaviour
{   
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject WinMenu;
    public GameObject deathMenu;
    public Animator anim;
    public Slider slide;
    public void Awake()
    {
        anim.Play("CrossfadeIn");
        FindObjectOfType<AudioManager>().playSound("General");
        FindObjectOfType<AudioManager>().playSound("windEffect");
    }
    public void activatePauseMenu()
    {
        FindObjectOfType<AudioManager>().PlaySoundOneShot("Touch");
        pauseGame();
        pauseMenu.SetActive(true);
    }
    public void deactivatePauseMenu()
    {
        FindObjectOfType<AudioManager>().PlaySoundOneShot("Touch");
        pauseMenu.SetActive(false);
        resumeGame();
    }
    public void quitGame()
    {
        FindObjectOfType<AudioManager>().pauseSounds();
        FindObjectOfType<AudioManager>().PlaySoundOneShot("Touch");
        Time.timeScale = 1;
        StartCoroutine(loadScene());
    }
    IEnumerator loadScene()
    {
        anim.Play("CrossfadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
    public void activateOptionsMenu()
    {
        FindObjectOfType<AudioManager>().PlaySoundOneShot("Touch");
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void deactivateOptionsMenu()
    {
        FindObjectOfType<AudioManager>().PlaySoundOneShot("Touch");
        FindObjectOfType<AudioManager>().controlSounds(slide.value);
        optionsMenu.SetActive(false);
        activatePauseMenu();
    }
    void pauseGame()
    {
        FindObjectOfType<AudioManager>().PlaySoundOneShot("Touch");
        FindObjectOfType<AudioManager>().pauseSounds();
        Time.timeScale = 0;
    }
    void resumeGame()
    {
        FindObjectOfType<AudioManager>().PlaySoundOneShot("Touch");
        FindObjectOfType<AudioManager>().resumeSounds();
        Time.timeScale = 1;
    }
}
