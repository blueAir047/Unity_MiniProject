using UnityEngine;

public class AutoDialogueTrigger : MonoBehaviour
{
    [Header("JSON 대화 ID 설정")]
    [Tooltip("이 구역을 밟았을 때 JSON에서 불러올 대사 ID를 적어주세요.")]
    [SerializeField] private string _dialogueID;

    [Header("재생 설정")]
    [Tooltip("체크하면 게임 중 딱 한 번만 자동 재생됩니다.")]
    [SerializeField] private bool _onlyOnce = true;

    private bool _hasTriggered = false; // 이미 켜졌었는지 기억하는 스위치

    // 💡 플레이어 발바닥이 이 구역 콜라이더에 닿는 순간 '즉시 자동 실행'
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. 부딪힌 게 플레이어 명찰(Tag)을 달고 있는지 확인
        if (other.CompareTag("Player"))
        {
            // 2. 한 번만 켜지는 설정인데 이미 켜진 적이 있다면 통과!
            if (_onlyOnce && _hasTriggered) return;

            // 3. 다이얼로그 매니저에게 JSON ID를 던져서 강제 자동 재생
            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.StartDialogueByID(_dialogueID);
                _hasTriggered = true; // 발동 완료 체크
                Debug.Log($"<color=yellow><b>[자동 재생]</b></color> ID: {_dialogueID} 구역 진입 완료!");
            }
            else
            {
                Debug.LogError("오브젝트에 DialogueManager가 배치되어 있는지 확인해주세요!");
            }
        }
    }
}
