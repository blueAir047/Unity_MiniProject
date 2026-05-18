using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class DialogueNode
{
    public string Id;
    public string CharacterDataId;
    public string Description;
    public string NextDialogueId;
    public List<string> SelectionNameList;
    public List<string> SelectionDialogueIdList;
    public string TexturePath;
    public string VoicePath;
}

[Serializable]
public class DialogueWrapper
{
    public List<DialogueNode> dialogues;
}
