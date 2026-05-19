using UnityEngine;

public class FgoTitleUI : FgoUIBase
{
    [SerializeField] private FgoUIButton Button_TouchAnywhere;

    private void OnEnable()
    {
        Button_TouchAnywhere.BindOnClickButtonEvent(OnClick_Screen);
    }

    private void OnDisable()
    {
        if (Button_TouchAnywhere != null)
        {
            Button_TouchAnywhere.UnBindOnClickButtonEvent(OnClick_Screen);
        }
    }

    private void OnClick_Screen()
    {
        Button_TouchAnywhere.gameObject.SetActive(false);

    FgoUIManager.Instance.TransitionTitleToLobby().Forget();
    }

}
