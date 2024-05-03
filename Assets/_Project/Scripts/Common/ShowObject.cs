using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VirtueSky.Inspector;

[DeclareHorizontalGroup("horizontal")]
[DeclareVerticalGroup("horizontal/vars")]
[DeclareVerticalGroup("horizontal/buttons")]
public class ShowObject : MonoBehaviour
{
    public bool IsShowByTesting;
    public bool IsShowByLevel;
    public bool IsShowByTime;
    public float DelayShowTime;
    [ShowIf(nameof(IsShowByLevel))] public List<int> LevelsShow;
    [ShowIf("IsShowByTime")] public int MaxTimeShow;

    [ShowIf("IsShowByTime")] [Group("horizontal/vars")] [ReadOnly]
    public string ShowID;

    [ShowIf("IsShowByTime")]
    [Button, Group("horizontal/buttons")]
    public void RandomShowID()
    {
        // if (ShowID == null || ShowID == "")
        // {
        //     ShowID = Ulid.NewUlid().ToString();
        // }
    }

    private bool IsLevelInLevelsShow()
    {
        foreach (int item in LevelsShow)
        {
            if (UserData.CurrentLevel == item)
            {
                return true;
            }
        }

        return false;
    }

    private bool EnableToShow()
    {
        bool testingCondition = !IsShowByTesting || (IsShowByTesting && UserData.IsTesting);
        bool levelCondition = !IsShowByLevel || (IsShowByLevel && IsLevelInLevelsShow());
        bool timeCondition = !IsShowByTime || (IsShowByTime && UserData.GetNumberShowGameObject(ShowID) <= MaxTimeShow);
        return testingCondition && levelCondition && timeCondition;
    }

    public void Awake()
    {
        Setup();

        if (IsShowByLevel) Observer.CurrentLevelChanged += Setup;
        if (IsShowByTesting) Observer.DebugChanged += Setup;
    }

    private void OnDestroy()
    {
        if (IsShowByLevel) Observer.CurrentLevelChanged -= Setup;
        if (IsShowByTesting) Observer.DebugChanged -= Setup;
    }

    public void Setup()
    {
        if (DelayShowTime > 0) gameObject.SetActive(false);
        DOTween.Sequence().AppendInterval(DelayShowTime).AppendCallback(() =>
        {
            if (IsShowByTime) UserData.IncreaseNumberShowGameObject(ShowID);
            gameObject.SetActive(EnableToShow());
        });
    }
}