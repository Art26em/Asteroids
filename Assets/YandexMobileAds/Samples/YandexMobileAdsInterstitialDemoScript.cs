/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Android (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using UnityEngine;
using YandexMobileAds.Base;

namespace YandexMobileAds.Samples
{
    public class YandexMobileAdsInterstitialDemoScript : MonoBehaviour
    {
        private String message = "";

        private InterstitialAdLoader interstitialAdLoader;
        private Interstitial interstitial;

        public void Awake()
        {
            interstitialAdLoader = new InterstitialAdLoader();
            RequestInterstitial();
            ShowInterstitial();
        }
        
        private void RequestInterstitial()
        {
            //Sets COPPA restriction for user age under 13
            //YandexAds.SetAgeRestricted(true);

            // Replace demo Unit ID 'demo-interstitial-yandex' with actual Ad Unit ID
            string adUnitId = "demo-interstitial-yandex";

            if (this.interstitial != null)
            {
                interstitial.Destroy();
            }

            interstitialAdLoader.LoadAd(
                CreateAdRequest(adUnitId),
                onLoaded: HandleAdLoaded,
                onFailed: HandleAdFailedToLoad);
            DisplayMessage("Interstitial is requested");
        }

        private void ShowInterstitial()
        {
            if (interstitial == null)
            {
                DisplayMessage("Interstitial is not ready yet");
                return;
            }

            interstitial.OnAdClicked += HandleAdClicked;
            interstitial.OnAdShown += HandleAdShown;
            interstitial.OnAdFailedToShow += HandleAdFailedToShow;
            interstitial.OnAdImpression += HandleImpression;
            interstitial.OnAdDismissed += HandleAdDismissed;

            interstitial.Show();
        }

        private AdRequest CreateAdRequest(string adUnitId)
        {
            return new AdRequest(adUnitId);
        }

        private void DisplayMessage(String message)
        {
            this.message = message + (this.message.Length == 0 ? "" : "\n--------\n" + this.message);
            print(message);
        }

        #region Interstitial callback handlers

        public void HandleAdLoaded(Interstitial interstitial)
        {
            DisplayMessage("HandleAdLoaded event received");

            this.interstitial = interstitial;
        }

        public void HandleAdFailedToLoad(AdFailedToLoadEventArgs args)
        {
            DisplayMessage($"HandleAdFailedToLoad event received with message: {args.Message}");
        }
        public void HandleAdClicked(object sender, EventArgs args)
        {
            DisplayMessage("HandleAdClicked event received");
        }

        public void HandleAdShown(object sender, EventArgs args)
        {
            DisplayMessage("HandleAdShown event received");
        }

        public void HandleAdDismissed(object sender, EventArgs args)
        {
            DisplayMessage("HandleAdDismissed event received");

            interstitial.Destroy();
            interstitial = null;
        }

        public void HandleImpression(object sender, ImpressionData impressionData)
        {
            var data = impressionData == null ? "null" : impressionData.rawData;
            DisplayMessage($"HandleImpression event received with data: {data}");
        }

        public void HandleAdFailedToShow(object sender, AdFailureEventArgs args)
        {
            DisplayMessage($"HandleAdFailedToShow event received with message: {args.Message}");
        }

        #endregion
    }
}
