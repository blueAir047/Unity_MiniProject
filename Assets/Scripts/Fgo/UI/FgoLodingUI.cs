//using Cysharp.Threading.Tasks;
//using System;
//using UnityEngine;

//public class FgoLoadingUI : FgoUIBase
//{
//    private void OnEnable()
//    {
//        CloseSelfafterDelay().Forget();
//    }
//    public async UniTaskVoid CloseSelfafterDelay()
//    {

//        await UniTask.Delay(TimeSpan.FromSeconds(1.0f));

//        FgoUIManager.Instance.CloseUI(FgoUIType.FgoLoadingUI);
//    }
//}
