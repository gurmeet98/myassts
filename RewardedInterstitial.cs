using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections.Generic;



public class RewardedInterstitial : MonoBehaviour
{
    private RewardedAd rewardedAd;
    string adUnitId = "ca-app-pub-3940256099942544/5224354917";

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        this.rewardedAd = CreateAndLoadRewardedAd(adUnitId);
    }

    // Update is called once per frame
    public void ShowAd(){
        if (this.rewardedAd.IsLoaded()) {
            this.rewardedAd.Show();
        }
        else{
            Debug.Log("add not loaded");
        }
    }

    //ads callbacks
    //on ads closed callback
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("Video rewarded Loading Closed");
        this.CreateAndLoadRewardedAd(adUnitId);
    }    

        //ads user rewards currency
    public void HandleUserEarnedReward(object sender, EventArgs args)
    {
        
    }   

    //on ads loaded callback
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Video rewarded Loaded");
    }    
    public void HandleRewardedAdLoadFailed(object sender, EventArgs args)
    {
        Debug.Log("Video rewarded Fail to Loaded");
    }

    //ads initializer
    public RewardedAd CreateAndLoadRewardedAd(string adUnitId)
    {
        RewardedAd rewardedAd = new RewardedAd(adUnitId);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdLoadFailed;


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
        return rewardedAd;
    }

}
