using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInit : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId = "4670971";
    //[SerializeField] string _iOSGameId = "4670970";
    [SerializeField] bool _testMode = true;
    private string _gameId;
     
    [SerializeField] InterstitialAd interstitial;
    [SerializeField] BannerAds banner;
    
    public Toast toast;
    void Awake()
    {
        if(PlayerPrefs.GetInt("premium", 0) != 1)
            InitializeAds();
    }
 
    public void InitializeAds()
    {
       /* _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;*/
        Advertisement.Initialize(_androidGameId, _testMode, this);
    }
 
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        interstitial.LoadAd();
        banner.LoadBanner();
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        toast._ShowAndroidToastMessage($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
