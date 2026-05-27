using System;
using UnityEngine;
using UnityEngine.UI;

public class FgoCommandCardUI : MonoBehaviour
{
    [Header("--- 카드 연결---")]
    [SerializeField] private Button btn_Card;
    [SerializeField] private Image img_CardIcon;

    public FgoCommandCardData CurrentCardData { get; private set; }
    public Action<FgoCommandCardUI> OnCardSelectedEvent;

    public bool IsSelected { get; private set; }
    private RectTransform _rectTransform;

    private float _selectedOffsetY = 50f;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

    }
    private void Start()
    {
        if (btn_Card != null) 
        {
            btn_Card.onClick.AddListener(OnClickCard);
        }
    }

    public void OnClickCard()
    {
        if (CurrentCardData == null)
        {
            return;
        }

        if (OnCardSelectedEvent != null)
        {
            OnCardSelectedEvent.Invoke(this);
        }
    }

    public void InitCard(FgoCommandCardData cardData)
    {
        CurrentCardData = cardData;

        SetSelectedState(false);

        if (cardData.IconPath != "")
        {
            Sprite loadedSprite = Resources.Load<Sprite>("Images/" + cardData.IconPath);

            if (loadedSprite != null && img_CardIcon != null)
            {
                img_CardIcon.sprite = loadedSprite;
            }
        }
    }

    public void SetSelectedState(bool isSelected) 
    {
        if (IsSelected == isSelected) return;

        IsSelected = isSelected;

        if (_rectTransform != null)
        {
            Vector2 newPos = _rectTransform.anchoredPosition;

            if (isSelected)
            {
                newPos.y += _selectedOffsetY;
            }

            else
            {
                newPos.y -= _selectedOffsetY;
            }

            _rectTransform.anchoredPosition = newPos;
        }

    }
}
