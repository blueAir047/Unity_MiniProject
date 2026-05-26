using UnityEngine;
using UnityEngine.UI;

public class FgoBattleUI : FgoUIBase
{
    [Header("----UI 구역 상자---")]
    [SerializeField] private GameObject commandArea;
    [SerializeField] private GameObject commandCardArea;

    [Header("----클릭할 버튼---")]
    [SerializeField] private Button btn_Attack;
    [SerializeField] private Button btn_BattleEnd;


    private void OnEnable()
    {
        ResetBattleUI();
    }

    private void Start()
    {
        if (btn_Attack != null)
        {
            btn_Attack.onClick.AddListener(OnAttackButtonClicked);
        }
        if (btn_BattleEnd != null)
        {
            btn_BattleEnd.onClick.AddListener(OnBattleEndButtonClicked);
        }
    }

    public void ResetBattleUI()
    {
        if (commandArea != null) commandArea.SetActive(true);
        if (commandCardArea != null) commandCardArea.SetActive(false);
    }

    private void OnAttackButtonClicked()
    {
        if (commandCardArea != null)
        {
            bool isCardActive = commandCardArea.activeSelf;

            commandCardArea.SetActive(!isCardActive);
        }
    }

    private void OnBattleEndButtonClicked()
    {
        OpenBattleEndPopup();
    }

    private void OpenBattleEndPopup()
    {
        FgoUIManager.Instance.OpenUI(FgoUIRootType.PopupUI, FgoUIType.ChoiceBattleEndPopup);
    }
}
