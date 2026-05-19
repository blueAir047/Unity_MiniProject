using UnityEngine;
using System.Collections.Generic;

public class FgoUIManager : MonoBehaviour
{
    [SerializeField] private Canvas Canvas_MainRoot;

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
        // 1. 매니저가 끄기 명령을 잘 받았는지 확인하는 탐지기
        Debug.Log($"[UIManager] {uiType} 끄기(CloseUI) 명령 도착!");

        if (_openedUIDic.Contains(uiType) == true)
        {
            FgoUIBase openedUI = _createdUIDic[uiType];
            openedUI.gameObject.SetActive(false);
            _openedUIDic.Remove(uiType);

            Debug.Log($"[UIManager] {uiType} 화면에서 정상적으로 껐습니다! (성공)");
        }
        else
        {
            // 2. 끄려고 봤더니 켜진 목록에 없을 때 울리는 경고음
            Debug.LogWarning($"[UIManager] 어라? {uiType}가 켜진 목록(_openedUIDic)에 없어서 끌 수가 없습니다!");
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
        
        return root;
    }
}
