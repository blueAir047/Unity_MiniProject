using UnityEngine;

public class FgoCharacterBase : MonoBehaviour
{
    [Header("--- 애니메이터 ---")]
    [SerializeField] protected Animator _animator;

    protected FgoCharacterData _statData;
    public int currentHp;

    public virtual void InitCharacter(FgoCharacterData data)
    {
        _statData = data;
        currentHp = _statData.MaxHp;

        Debug.Log($"[{_statData.Name}] 지정된 위치에 스폰 완료! (HP: {currentHp} / ATK: {_statData.Atk})");
    }

    public virtual void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log($"{gameObject.name} 피격! 데미지: {damage} (남은 HP: {currentHp})");

        if (_animator != null)
        {
            _animator.SetTrigger("Hit");
        }

        if (currentHp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} 사망!");
        if (_animator != null)
        {
            _animator.SetTrigger("Die");
        }
    }
}
