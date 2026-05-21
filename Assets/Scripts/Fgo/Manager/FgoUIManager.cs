using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class FgoUIManager : MonoBehaviour
{
    [SerializeField] private Canvas Canvas_MainRoot;
    [SerializeField] private Canvas Canvas_VeryFront;
    [SerializeField] private Canvas Canvas_Popup;
    [SerializeField] private Canvas Canvas_Content;
    public static FgoUIManager Instance { get; set; }

    private Dictionary<FgoUIType, FgoUIBase> _createdUIDic = new Dictionary<FgoUIType, FgoUIBase> ();
    private HashSet<FgoUIType> _openedUIDic = new HashSet<FgoUIType> ();

    private void Awake()
    {
        Instance = this;
    }

    public FgoUIBase OpenUI(FgoUIRootType uiRootType, FgoUIType uiType)
    {
        FgoUIBase openedUI = GetCreatedUI(uiRootType, uiType);
        if (_openedUIDic.Contains(uiType)== false)
        {
            openedUI.gameObject.SetActive(true);
            _openedUIDic.Add(uiType);
        }
        return openedUI;
    }

    public void CloseUI(FgoUIType uiType)
    {

        if (_openedUIDic.Contains(uiType) == true)
        {
            FgoUIBase openedUI = _createdUIDic[uiType];
            openedUI.gameObject.SetActive(false);
            _openedUIDic.Remove(uiType);

        }
    }

    private FgoUIBase GetCreatedUI(FgoUIRootType uiRootType, FgoUIType uiType)
    {
       if (_createdUIDic.ContainsKey(uiType) == false)
       {
            CreateUI(uiRootType, uiType);
       }
       return _createdUIDic[uiType];
    }

    private void CreateUI(FgoUIRootType uiRootType, FgoUIType uiType)
    {
        string path = this.GetUIPath(uiRootType, uiType);
        GameObject loadedObj = (GameObject)Resources.Load(path);

        if (loadedObj == null)
        {
            Debug.LogError($" [UIManager] UI 프리팹을 찾지 못했습니다! 경로를 확인하세요: Resources/{path}");
            return;
        }

        Transform root = GetRootTransform(uiRootType);
        GameObject gObj = Instantiate(loadedObj, root);

        if (gObj != null)
        {
            FgoUIBase uIBase = gObj.GetComponent<FgoUIBase>();
            _createdUIDic.Add(uiType, uIBase);
        }
    }

    private Transform GetRootTransform(FgoUIRootType uiRootType)
    {
        Transform root = null;

        if (uiRootType == FgoUIRootType.MainUI) 
        {
            root = Canvas_MainRoot.transform;
        }

        if (uiRootType == FgoUIRootType.VeryFront)
        {
            root = Canvas_VeryFront.transform;
        }

        if (uiRootType == FgoUIRootType.Popup)
        {
            root = Canvas_Popup.transform;
        }

        if (uiRootType == FgoUIRootType.Content)
        {
            root = Canvas_Content.transform;
        }

        return root;
    }

}
