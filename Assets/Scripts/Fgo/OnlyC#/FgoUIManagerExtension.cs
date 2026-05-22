using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public enum FgoUIRootType
{
    None = 0,
    MainUI,
    VeryFront,
    Popup,
    Content
}
public enum FgoUIType
{
    None = 0,
    FgoTitleUI,
    FgoLoadingUI,
    FgoLobbyUI,
    FgoBattleUI
}

public static class FgoUIManagerExtension
{
    public static string GetUIPath(this FgoUIManager uiManager, FgoUIRootType uiRootType, FgoUIType uiType)
    {
        return $"Prefabs/UI/{uiRootType}/{uiType}";
    }
    public static void ShowStartupUIONGameStart(this FgoUIManager uiManager)
    {
    uiManager.OpenUI(FgoUIRootType.MainUI, FgoUIType.FgoTitleUI);
    }

    public static async UniTaskVoid TransitionTitleToLobby(this FgoUIManager uiManager)
    {
        uiManager.OpenUI(FgoUIRootType.VeryFront, FgoUIType.FgoLoadingUI);

        uiManager.CloseUI(FgoUIType.FgoTitleUI);

        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

        uiManager.OpenUI(FgoUIRootType.MainUI, FgoUIType.FgoLobbyUI);

        uiManager.CloseUI(FgoUIType.FgoLoadingUI);
    }

    public static async UniTaskVoid TransitionLobbyToBattle(this FgoUIManager uiManager)
    {
        uiManager.OpenUI(FgoUIRootType.VeryFront, FgoUIType.FgoLoadingUI);

        uiManager.CloseUI(FgoUIType.FgoLobbyUI);
 
        FgoGameManager.Instance.ShowBattleMap();

        await UniTask.Delay(TimeSpan.FromSeconds(1.0f));

        uiManager.OpenUI(FgoUIRootType.MainUI, FgoUIType.FgoBattleUI);

        uiManager.CloseUI(FgoUIType.FgoLoadingUI);
    }
}


