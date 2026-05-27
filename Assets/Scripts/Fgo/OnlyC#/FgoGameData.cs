using UnityEngine;
[System.Serializable]
public class FgoCharacterData : FgoGameDataBase
{
    public string Name;
    public string ServantClass;
    public string SkillList;
}

[System.Serializable]
public class FgoCommandCardData : FgoGameDataBase
{
    public string CardName;
    public string CardType;
    public int DamageMultiplier;
    public string IconPath;
}
