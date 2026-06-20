using YandexMobileAds.Base;

namespace YandexMobileAds.Editor.IntegrationManager.AdapterInfo
{
    public static class SdkVersionReader
    {
        public static string GetSdkVersion()
        {
            return MobileAdsPackageInfo.PackageVersion;
        }
    }
}