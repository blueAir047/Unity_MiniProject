using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public bool IsDialogueActive => _dialoguePanel != null && _dialoguePanel.activeSelf;

    [Header("UI 연결")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _dialogueNameText;

    [Header("선택지 버튼 연결")]
    [SerializeField] private GameObject[] _selectionButtons; // 버튼 오브젝트 배열
    [SerializeField] private TextMeshProUGUI[] _selectionTexts; // 버튼 안의 글자 배열

    private Dictionary<string, DialogueNode> _dialogueDatabase = new Dictionary<string, DialogueNode>();
    private DialogueNode _currentNode;
    private bool _isTyping = false;
    private string _currentSentence;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _dialoguePanel.SetActive(false);
        foreach (var btn in _selectionButtons) btn.SetActive(false); // 시작 시 버튼 다 숨기기

        LoadDialogueData();
    }

    public void StartDialogueByID(string dialogueID)
    {
        _dialoguePanel.SetActive(true);
        ShowNode(dialogueID);
    }

    private void ShowNode(string nodeID)
    {
        if (!_dialogueDatabase.TryGetValue(nodeID, out _currentNode))
        {
            EndDialogue();
            return;
        }

        if (_currentNode.Id.Contains("Cuchulainn")) _dialogueNameText.text = "쿠훌린";
        else if (_currentNode.Id.Contains("Mash")) _dialogueNameText.text = "마슈";
        else _dialogueNameText.text = "";

        _currentSentence = _currentNode.Description;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(_currentSentence));

        if (_currentNode.SelectionNameList != null && _currentNode.SelectionNameList.Count > 0)
        {
            for (int i = 0; i < _currentNode.SelectionNameList.Count; i++)
            {
                if (i >= _selectionButtons.Length) break;

                _selectionButtons[i].SetActive(true);
                _selectionTexts[i].text = _currentNode.SelectionNameList[i];

                int index = i;
                Button btnComponent = _selectionButtons[i].GetComponent<Button>();
                btnComponent.onClick.RemoveAllListeners();
                btnComponent.onClick.AddListener(() => OnSelectChoice(index));
            }
        }
        else
        {
             foreach (var btn in _selectionButtons) btn.SetActive(false);
        }
    }

    private void OnSelectChoice(int index)
    {
        foreach (var btn in _selectionButtons) btn.SetActive(false);

   
        string nextID = _currentNode.SelectionDialogueIdList[index];

        if (nextID == "Action_QuitGame")
        {
            Debug.Log("게임 종료 명령이 실행되었습니다!");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 플레이 중지
#else
                Application.Quit(); // 실제 빌드된 게임 종료
#endif
            return;
        }

        ShowNode(nextID);
    }

    void Update()
    {
        if (_dialoguePanel.activeSelf && (_currentNode.SelectionNameList == null || _currentNode.SelectionNameList.Count == 0))
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (_isTyping)
                {
                    StopAllCoroutines();
                    _dialogueText.text = _currentSentence;
                    _isTyping = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(_currentNode.NextDialogueId)) ShowNode(_currentNode.NextDialogueId);
                    else EndDialogue();
                }
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        _dialogueText.text = "";
        _isTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
        _isTyping = false;
    }

    void EndDialogue()
    {
        _dialoguePanel.SetActive(false);
        foreach (var btn in _selectionButtons) btn.SetActive(false);
    }

    private void LoadDialogueData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("DialogueData");
        if (jsonFile != null)
        {
            DialogueWrapper wrapper = JsonUtility.FromJson<DialogueWrapper>(jsonFile.text);
            _dialogueDatabase.Clear();
            foreach (DialogueNode node in wrapper.dialogues)
            {
                if (!string.IsNullOrEmpty(node.Id)) _dialogueDatabase.Add(node.Id, node);
            }
        }
    }
}