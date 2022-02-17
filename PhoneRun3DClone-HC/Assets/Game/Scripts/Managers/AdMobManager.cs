using Game.Scripts.Patterns;
using GoogleMobileAds.Api;
using UnityEngine;
using System;

namespace Game.Scripts.Managers
{
    public class AdMobManager : MonoSingleton<AdMobManager>
    {
        private BannerView bannerAd;
        private InterstitialAd interstitialAd;

        private void Start()
        {
            MobileAds.Initialize(initStatus =>{});
            RequestBanner();
        }

        private AdRequest CreateRequest()
        {
            return new AdRequest.Builder().Build();
        }

        private void RequestBanner()
        {
            var adUnitId="ca-app-pub-3940256099942544/6300978111";
            bannerAd?.Destroy();
            bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
            
            bannerAd.LoadAd(CreateRequest());
            
        }

        public void RequestInterstitial()
        {
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
            interstitialAd?.Destroy();

            interstitialAd = new InterstitialAd(adUnitId);
            interstitialAd.LoadAd(CreateRequest());
        }

        public void ShowInterstitial()
        {
            if (!interstitialAd.IsLoaded()) return;
            interstitialAd.Show();
            /*else
            {
                Debug.Log("Interstitial Ad is not ready yet");
            }*/
        }
    }
}
