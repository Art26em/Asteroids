using System;
using UnityEngine;
using YandexMobileAds.Base;

namespace YandexMobileAds.Samples
{
    public class YandexMobileAdsInterstitial : MonoBehaviour
    {
        private InterstitialAdLoader _interstitialAdLoader;
        private Interstitial _interstitial;

        public void Awake()
        {
            _interstitialAdLoader = new InterstitialAdLoader();
            RequestInterstitial();
        }
        
        public void RequestInterstitial()
        {
            // Replace demo Unit ID 'demo-interstitial-yandex' with actual Ad Unit ID
            string adUnitId = "R-M-19285284-2";

            if (_interstitial != null)
            {
                _interstitial.Destroy();
            }

            _interstitialAdLoader.LoadAd(
                CreateAdRequest(adUnitId),
                onLoaded: HandleAdLoaded,
                onFailed: HandleAdFailedToLoad);
            Debug.Log("Interstitial is requested");
        }

        private void HandleAdLoaded(Interstitial interstitial)
        {
            _interstitial = interstitial;
            ShowInterstitial();
        }
        
        private void ShowInterstitial()
        {
            if (_interstitial == null)
            {
                Debug.Log("Interstitial is not ready yet");
                return;
            }

            _interstitial.OnAdClicked += HandleAdClicked;
            _interstitial.OnAdShown += HandleAdShown;
            _interstitial.OnAdFailedToShow += HandleAdFailedToShow;
            _interstitial.OnAdImpression += HandleImpression;
            _interstitial.OnAdDismissed += HandleAdDismissed;

            _interstitial.Show();
        }

        private AdRequest CreateAdRequest(string adUnitId)
        {
            return new AdRequest(adUnitId);
        }
        
        #region Interstitial callback handlers
        

        private void HandleAdFailedToLoad(AdFailedToLoadEventArgs args)
        {
            Debug.Log($"HandleAdFailedToLoad event received with message: {args.Message}");
        }
        private void HandleAdClicked(object sender, EventArgs args)
        {
            Debug.Log("HandleAdClicked event received");
        }

        private void HandleAdShown(object sender, EventArgs args)
        {
            Debug.Log("HandleAdShown event received");
        }

        private void HandleAdDismissed(object sender, EventArgs args)
        {
            Debug.Log("HandleAdDismissed event received");

            _interstitial.Destroy();
            _interstitial = null;
        }

        private void HandleImpression(object sender, ImpressionData impressionData)
        {
            var data = impressionData == null ? "null" : impressionData.rawData;
            Debug.Log($"HandleImpression event received with data: {data}");
        }

        private void HandleAdFailedToShow(object sender, AdFailureEventArgs args)
        {
            Debug.Log($"HandleAdFailedToShow event received with message: {args.Message}");
        }

        #endregion
    }
}
