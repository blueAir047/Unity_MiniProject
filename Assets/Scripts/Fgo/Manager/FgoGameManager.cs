using UnityEngine;
using UnityEngine.Rendering;

public class FgoGameManager : MonoBehaviour
{
    public static FgoGameManager instance {  get; set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
            FgoUIManager.Instance.ShowStartupUIONGameStart();
    }
}
