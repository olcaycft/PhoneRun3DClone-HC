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
        private RewardBasedVideoAd rewardAd;
        static bool bannerAdRequested = false;
        
        private void Start()
        {
            MobileAds.Initialize(initStatus => { });
            if (bannerAdRequested)
                return;

            RequestBanner();
            bannerAdRequested = true;

            this.rewardAd=RewardBasedVideoAd.Instance;
            this.rewardAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
            this.rewardAd.OnAdClosed += HandleRewardBasedVideoClosed;
            RequestRewardBasedVideoAd();
        }

        private AdRequest CreateRequest()
        {
            return new AdRequest.Builder().Build();
        }

        #region Banner
        private void RequestBanner()
        {
            var adUnitId = "ca-app-pub-3940256099942544/6300978111";
            bannerAd?.Destroy();
            bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

            bannerAd.LoadAd(CreateRequest());
        }

        #endregion


        #region Interstitial

        public void RequestInterstitial()
        {
            var adUnitId = "ca-app-pub-3940256099942544/1033173712";
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

        #endregion


        #region Rewarded

        public void RequestRewardBasedVideoAd()
        {
            var adUnitId = "ca-app-pub-3940256099942544/5224354917";
            rewardAd.LoadAd(CreateRequest(), adUnitId);
        }

        public void ShowRewardBasedVideoAd()
        {
            
            Debug.Log("calistim");
            if (!rewardAd.IsLoaded()) return;
            rewardAd.Show();

        }

        #region RewardedBasedAd Callback Handlers

        public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
        {
            RequestRewardBasedVideoAd();
        }
        public void HandleRewardBasedVideoRewarded(object sender,Reward args)
        {
            //burada is reward true dönüp ne vaat ettiysek onu oyun içerisinde true geldiği taktirde kullanıcıya verebiliriz.
            //isRewarded = true;
            //GameManager.Instance.WhenRewardComplete();
            Debug.Log("reward hak kazandı");
        }
        #endregion

        #endregion
    }
}