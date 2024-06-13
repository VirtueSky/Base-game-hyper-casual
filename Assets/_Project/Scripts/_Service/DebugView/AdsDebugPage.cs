using System.Threading.Tasks;
using Base.Data;
using UnityDebugSheet.Runtime.Core.Scripts;
using UnityEngine;
using VirtueSky.Ads;

namespace Base.Services
{
    public class AdsDebugPage : DefaultDebugPageBase
    {
        private Sprite iconToggle;
        protected override string Title => "Advertising Debug";

        public void Init(Sprite _iconToggle)
        {
            iconToggle = _iconToggle;
        }

        public override Task Initialize()
        {
            AddButton("Show Banner", clicked: ShowBanner);
            AddButton("Hide Banner", clicked: HideBanner);
            AddButton("Show Inter", clicked: ShowInter);
            AddButton("Show Reward", clicked: ShowReward);
            AddSwitch(UserData.IsOffInterAdsDebug, "Is Off Inter", valueChanged: b => UserData.IsOffInterAdsDebug = b,
                icon: iconToggle);
            AddSwitch(UserData.IsOffBannerAdsDebug, "Is Off Banner",
                valueChanged: b => UserData.IsOffBannerAdsDebug = b,
                icon: iconToggle);
            AddSwitch(UserData.IsOffRewardAdsDebug, "Is Off Reward",
                valueChanged: b => UserData.IsOffRewardAdsDebug = b,
                icon: iconToggle);
            return base.Initialize();
        }

        void ShowBanner()
        {
            if (Application.isMobilePlatform)
            {
                Advertising.Instance.ShowBanner();
            }
            else
            {
                NotificationInGame.Instance.Show("Only works on mobile platform");
            }
        }

        void HideBanner()
        {
            if (Application.isMobilePlatform)
            {
                Advertising.Instance.HideBanner();
            }
            else
            {
                NotificationInGame.Instance.Show("Only works on mobile platform");
            }
        }

        void ShowInter()
        {
            if (Application.isMobilePlatform)
            {
                Advertising.Instance.ShowInterstitial();
            }
            else
            {
                NotificationInGame.Instance.Show("Only works on mobile platform");
            }
        }

        void ShowReward()
        {
            if (Application.isMobilePlatform)
            {
                Advertising.Instance.ShowReward();
            }
            else
            {
                NotificationInGame.Instance.Show("Only works on mobile platform");
            }
        }
    }
}