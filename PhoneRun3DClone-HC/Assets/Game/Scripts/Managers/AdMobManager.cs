using Game.Scripts.Patterns;
using GoogleMobileAds.Api;
using UnityEngine;
using System;

namespace Game.Scripts.Managers
{
    public class AdMobManager : MonoSingleton<AdMobManager>
    {
        private BannerView bannerAd;

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
            bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
            
            bannerAd.LoadAd(CreateRequest());
            
        }
    }
}
