using UnityEngine;

public class FgoBattleManager : MonoBehaviour
{
    public static FgoBattleManager Instance { get; private set; }

    private FgoCharacterBase playerCharacter;
    private FgoCharacterBase enemyCharacter;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void InitBattleField()
    {
        playerCharacter = FindFirstObjectByType<FgoServantController>();
        enemyCharacter = FindFirstObjectByType<FgoEnemyController>();

        if (playerCharacter != null && enemyCharacter != null)
        {
               InitPlacedCharacter(playerCharacter, "ch_muramasa_01");
            InitPlacedCharacter(enemyCharacter, "ch_melusine_01");
        }
    }

    private void InitPlacedCharacter(FgoCharacterBase charBase, string characterId)
    {
        if (charBase == null) return;

        FgoCharacterData charData = FgoGameDataManager.Instance.GetCharacterData(characterId);
        if (charData == null)
        {
            Debug.LogError($"[FgoBattleManager] {characterId} 데이터를 찾을 수 없습니다!");
            return;
        }

        charBase.InitCharacter(charData);
    }
    public void StartPlayerAttack(System.Collections.Generic.List<FgoCommandCardData> selectedCards)
    {

        FgoServantController servant = playerCharacter as FgoServantController;

        if (servant != null)
        {
            servant.ExecuteCommandCards(selectedCards, enemyCharacter);
        }

    }
}