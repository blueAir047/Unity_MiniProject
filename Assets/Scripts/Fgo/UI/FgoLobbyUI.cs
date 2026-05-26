using UnityEngine;

public class FgoLobbyUI : FgoUIBase
{
    [SerializeField] private FgoUIButton Button_GoBattle;

    private void OnEnable()
    {
        if (Button_GoBattle != null) 
        {
            Button_GoBattle.gameObject.SetActive(true);

            Button_GoBattle.BindOnClickButtonEvent(OnClick_GoBattle);
        }
    }

    private void OnDisable()
    {
        if(Button_GoBattle != null)
        {
            Button_GoBattle.UnBindOnClickButtonEvent(OnClick_GoBattle);
        }
    }
    private void OnClick_GoBattle()
    {
        Button_GoBattle.gameObject.SetActive(false);

        FgoUIManager.Instance.TransitionLobbyToBattle().Forget();
    }
}
