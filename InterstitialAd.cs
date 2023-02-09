using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
 
public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{    
    [SerializeField] Button WallpaperButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOsAdUnitId = "Rewarded_iOS";
    string _adUnitId;
    
    public Toast toast;
 
    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        /*_adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;*/
        
        _adUnitId = _androidAdUnitId;
       /* if(PlayerPrefs.GetInt("premium", 0) != 1)
            WallpaperButton.interactable = false;*/
    }
 
    // Load content to the Ad Unit:
    public void LoadAd()
    {
        if(PlayerPrefs.GetInt("premium", 0) != 1)
            WallpaperButton.interactable = false;
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
 
    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        if(PlayerPrefs.GetInt("premium", 0) != 1)
        {// Note that if the ad content wasn't previously loaded, this method will fail
            Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
        }
        else
           Debug.Log("Premium user no ads shown");
    }
 
    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if(PlayerPrefs.GetInt("premium", 0) != 1)
            WallpaperButton.interactable = true;
        // Optionally execute code if the Ad Unit successfully loads content.
    }
 
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        
            toast._ShowAndroidToastMessage("Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) { 
        
        if(PlayerPrefs.GetInt("premium", 0) != 1)
            LoadAd();
    }
}