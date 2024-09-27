using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public        Button     btnLoad, btnShow, btnHide, btnDestroy;
    private const string     CollapsibleBannerAdFormat = "CollapsibleBanner";
    private       BannerView collapsibleBannerView;

    private void Awake()
    {
        this.btnLoad.onClick.AddListener(this.LoadCollapsibleBannerAd);
        this.btnShow.onClick.AddListener(() =>
        {
            Debug.Log("Show CollapsibleBanner.");
            this.collapsibleBannerView?.Show();
        });
        this.btnHide.onClick.AddListener(() =>
        {
            Debug.Log("Hide CollapsibleBanner.");
            this.collapsibleBannerView?.Hide();
        });
        this.btnDestroy.onClick.AddListener(() =>
        {
            Debug.Log("Destroy CollapsibleBanner.");
            this.collapsibleBannerView?.Destroy();
        });
    }

    private static string GetNewGuid() => Guid.NewGuid().ToString();

    private void LoadCollapsibleBannerAd()
    {
        var collapsibleBannerGuid = GetNewGuid();

        var adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        this.collapsibleBannerView = new BannerView("ca-app-pub-3940256099942544/2014213617", adSize, AdPosition.Bottom);

        #region Events

        this.collapsibleBannerView.OnBannerAdLoaded            += () => this.OnCollapsibleBannerLoaded(CollapsibleBannerAdFormat, collapsibleBannerView);
        this.collapsibleBannerView.OnBannerAdLoadFailed        += error => this.OnCollapsibleBannerLoadFailed(CollapsibleBannerAdFormat, error);
        this.collapsibleBannerView.OnAdFullScreenContentOpened += () => this.OnCollapsibleBannerPresented(CollapsibleBannerAdFormat);
        this.collapsibleBannerView.OnAdFullScreenContentClosed += () => this.OnCollapsibleBannerDismissed(CollapsibleBannerAdFormat);
        this.collapsibleBannerView.OnAdClicked                 += () => this.OnCollapsibleBannerClicked(CollapsibleBannerAdFormat);

        #endregion

        var request = new AdRequest();
        AddPramsCollapsible();
        Debug.Log("Load CollapsibleBanner.");
        this.collapsibleBannerView.LoadAd(request);

        return;

        void AddPramsCollapsible()
        {
            request.Extras.Add("collapsible_request_id", collapsibleBannerGuid);
            request.Extras.Add("collapsible", "bottom");
        }
    }

    private void OnCollapsibleBannerLoaded(string placement, BannerView collapsibleBannerView) { Debug.Log($"OnCollapsibleBannerLoaded "); }

    private void OnCollapsibleBannerLoadFailed(string placement, AdError adError) { Debug.Log($"OnCollapsibleBannerLoadFailed {placement} - {adError.GetMessage()}"); }

    private void OnCollapsibleBannerPresented(string placement) { Debug.Log("OnCollapsibleBannerPresented"); }

    private void OnCollapsibleBannerDismissed(string placement) { Debug.Log("OnCollapsibleBannerDismissed"); }

    private void OnCollapsibleBannerClicked(string placement) { Debug.Log("OnCollapsibleBannerClicked"); }
}