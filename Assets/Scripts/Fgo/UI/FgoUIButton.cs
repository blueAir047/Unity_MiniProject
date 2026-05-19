using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FgoUIButton : MonoBehaviour
{
    [SerializeField] private Button Button_Base;
    [SerializeField] private Text Text_Base;
    [SerializeField] private Image Image_Base;
    [SerializeField] private Image Image_Select;


    private Dictionary<Action, UnityEngine.Events.UnityAction> _actionDic = new Dictionary<Action, UnityEngine.Events.UnityAction>();
    private void Awake()
    {
        InitUIButton();
        SetDefaultUI();
    }

    private void OnEnable()
    {
        BindOnClickButtonEvent(OnClickSetSelectUI);
    }

    private void OnDisable()
    {
        if (Button_Base != null)
        {
            Button_Base.onClick.RemoveAllListeners();
        }
    }

    private void SetDefaultUI()
    {
        if (Image_Select != null)
        {
            Image_Select.gameObject.SetActive(false);
        }
    }

    private void InitUIButton()
    {
        if (Button_Base != null)
        {
            return;
        }

        Button button = this.gameObject.GetComponentInChildren<Button>();

        if (button != null) 
        { 
            this.Button_Base = button;
        }
    }

    public void BindOnClickButtonEvent(Action onClickCallback)
    {
        if (Button_Base == null) return;
        if (_actionDic.ContainsKey(onClickCallback) == false)
        {
            UnityEngine.Events.UnityAction unityAction = new UnityEngine.Events.UnityAction(onClickCallback);
            _actionDic.Add(onClickCallback, unityAction);
            Button_Base.onClick.AddListener(unityAction);
        }
    }

    public void UnBindOnClickButtonEvent(Action onClickCallback)
    {
        if (Button_Base != null) return;
        if (_actionDic.ContainsKey(onClickCallback) == true)
        {
            Button_Base.onClick.RemoveListener(_actionDic[onClickCallback]);
            _actionDic.Remove(onClickCallback);
        }
    }

    public void ChangeButtonText(string buttonStr)
    {
        if (Text_Base == null) return;
        Text_Base.text = buttonStr;
    }

    private void OnClickSetSelectUI()
    {
        if (Image_Select != null)
        {
            bool currentActive = Image_Select.gameObject.activeSelf;
            Image_Select.gameObject.SetActive(!currentActive);
        }
    }
}
