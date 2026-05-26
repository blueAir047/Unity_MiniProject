using UnityEngine;
using UnityEngine.UI;

public class ChoiceBattleEndPopup : FgoUIBase
{
    [Header("--- 버튼 연결 ---")]
    [SerializeField] private Button btn_Yes;
    [SerializeField] private Button btn_No;

    private void Start()
    {
        if (btn_Yes != null) btn_Yes.onClick.AddListener(OnClickYes);
        if (btn_No != null) btn_No.onClick.AddListener(OnClickNo);
    }

    private void OnClickYes()
    {
        FgoUIManager.Instance.CloseUI(FgoUIType.ChoiceBattleEndPopup);

        FgoUIManager.Instance.TransitionBattleToLobby().Forget();
    }

    private void OnClickNo()
    {
        FgoUIManager.Instance.CloseUI(FgoUIType.ChoiceBattleEndPopup);
    }
}
