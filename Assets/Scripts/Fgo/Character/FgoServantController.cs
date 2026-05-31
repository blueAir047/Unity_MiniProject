using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FgoServantController : FgoCharacterBase
{
    [Header("--- 전투 연출 설정 ---")]
    [SerializeField] private float dashSpeed = 0.2f;
    [SerializeField] private float attackOffset = 2.5f;

    public void ExecuteCommandCards(List<FgoCommandCardData> selectedCards, FgoCharacterBase targetEnemy)
    {
        Debug.Log(" [1단계] 무라마사가 배틀 UI로부터 공격 명령을 받았습니다!");

        if (targetEnemy == null)
        {
            Debug.LogError(" 에러: 타겟(에너미)이 없어서 이동할 수 없습니다!");
            return;
        }

        StartCoroutine(AttackRoutine(selectedCards, targetEnemy));
    }

    private IEnumerator AttackRoutine(List<FgoCommandCardData> cards, FgoCharacterBase targetEnemy)
    {
        Vector3 originalPosition = transform.position;
        Vector3 attackPosition = targetEnemy.transform.position + new Vector3(attackOffset, 0f, 0f);

        Debug.Log($" [2단계] 타겟의 위치 확인! 목적지로 돌진합니다. (목적지 좌표: {attackPosition})");

        for (int i = 0; i < cards.Count; i++)
        {
            FgoCommandCardData card = cards[i];
            string attackTrigger = card.CardType;

            yield return StartCoroutine(MoveToPosition(attackPosition, dashSpeed));

            Debug.Log($" [3단계] {i + 1}타: {attackTrigger} 공격 애니메이션 실행!");

            if (_animator != null)
            {
                _animator.SetTrigger(attackTrigger);
            }
            else
            {
                Debug.LogWarning(" 경고: 애니메이터가 연결되어 있지 않아 모션 없이 데미지만 들어갑니다!");
            }

            float hitDelay = 0.4f;
            yield return new WaitForSeconds(hitDelay);

            if (targetEnemy != null) targetEnemy.TakeDamage(100);

            float totalMotionTime = 1.2f;
            yield return new WaitForSeconds(totalMotionTime - hitDelay);
        }

        Debug.Log("[4단계] 3연타 종료. 원래 자리로 복귀합니다.");
        yield return StartCoroutine(MoveToPosition(originalPosition, dashSpeed));

        Debug.Log("⭐⭐⭐ 턴 완전히 종료! ⭐⭐⭐");
    }

    private IEnumerator MoveToPosition(Vector3 targetPos, float duration)
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        if (_animator != null)
        {
            _animator.SetBool("IsMove", true);
        }

        if (duration <= 0f) duration = 0.1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPos, targetPos, 1f - Mathf.Pow(1f - t, 3));
            yield return null;
        }

        transform.position = targetPos;

        if (_animator != null)
        {
            _animator.SetBool("IsMove", false);
        }
    }
}
