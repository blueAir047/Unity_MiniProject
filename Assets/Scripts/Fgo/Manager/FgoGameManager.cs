using UnityEngine;
using UnityEngine.Rendering;

public class FgoGameManager : MonoBehaviour
{
    public static FgoGameManager Instance {  get; set; }

    private GameObject _currentBattleMap;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
  
    }

    private void Start()
    {
            FgoUIManager.Instance.ShowStartupUIONGameStart();
    }

    public void ShowBattleMap()
    {
        if (_currentBattleMap == null)
        {
            GameObject mapPrefab = Resources.Load<GameObject>("Prefabs/Map/BattleMap_Root");
            _currentBattleMap = Instantiate(mapPrefab);

            _currentBattleMap.transform.position = Vector3.zero;
        }

        _currentBattleMap.SetActive(true);

        if (FgoBattleManager.Instance != null)
        {
            FgoBattleManager.Instance.InitBattleField();
        }
    }
    public void HideBattleMap()
    {
        if (_currentBattleMap != null)
        {
            _currentBattleMap.SetActive(false);
        }
    }


}
 