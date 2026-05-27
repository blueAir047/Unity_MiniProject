using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FgoBattleUI : FgoUIBase
{
    [Header("----UI 구역 상자---")]
    [SerializeField] private GameObject commandArea;
    [SerializeField] private GameObject commandCardArea;



    [Header("----클릭할 버튼---")]
    [SerializeField] private Button btn_Attack;
    [SerializeField] private Button btn_BattleEnd;

    [Header("----커맨드 카드---")]
    [SerializeField] private FgoCommandCardUI[] cardSlots;

    private List<FgoCommandCardData> _selectedCards = new List<FgoCommandCardData>();
    private void OnEnable()
    {
        ResetBattleUI();
        _selectedCards.Clear();
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

            commandArea.SetActive(isCardActive);

            if (!isCardActive)
            {
                SetupCommandCards();
            }
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

    private void OnCommandCardClicked(FgoCommandCardUI clickedCard)
    {
        if (clickedCard.IsSelected)
        {
            _selectedCards.Remove(clickedCard.CurrentCardData);
            clickedCard.SetSelectedState(false);

            return;
        }

        if (_selectedCards.Count >= 3) 
        { 
            return; 
        }

        _selectedCards.Add(clickedCard.CurrentCardData);
        clickedCard.SetSelectedState(true);

        if (_selectedCards.Count == 3) 
        {
            StartAttackPhase();
        }
    }

    private void SetupCommandCards()
    {
        FgoCommandCardData busterData = FgoGameDataManager.Instance.GetCommandCardData("card_buster_01");

        if (busterData == null) return;

        _selectedCards.Clear();

        foreach (FgoCommandCardUI card in cardSlots)
        {
            if (card != null)
            {
                card.gameObject.SetActive(true);
                card.InitCard(busterData);

                card.OnCardSelectedEvent -= OnCommandCardClicked;
                card.OnCardSelectedEvent += OnCommandCardClicked;

            }
        }
    }

    private void StartAttackPhase()
    {
        if (commandCardArea != null) commandCardArea.SetActive(false);
        if (commandArea != null) commandArea.SetActive(true);
    }
}
