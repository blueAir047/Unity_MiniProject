using UnityEngine;

public class NPC_Interaction : MonoBehaviour
{
    [Header("JSON 대화 ID 설정")]
    [Tooltip("이 NPC가 시작할 JSON 내의 Id를 적어주세요. (예: dialogue_Cuchulainn_1_1)")]
    [SerializeField] private string _dialogueID;

    private bool _isPlayerClose = false;

    void Update()
    {
        if (_isPlayerClose && Input.GetKeyDown(KeyCode.E))
        {
            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.StartDialogueByID(_dialogueID);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerClose = true;
            Debug.Log("대화 가능! [E] 키를 누르세요.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerClose = false;
        }
    }
}