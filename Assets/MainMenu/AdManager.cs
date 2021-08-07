using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdManager : MonoBehaviour
{   
    public AdManager instance
    {
        set;
        get;
    }
    string googlePlay_ID = "4027619";
    bool gameMode = true;
    void Start()
    {
       Advertisement.Initialize(googlePlay_ID, gameMode);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("Interstitial_Android");
        }
        else
            return;
    }
    public void ShowAdBanner()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("Banner_Android");
        }
        else
            return;
    }
}
