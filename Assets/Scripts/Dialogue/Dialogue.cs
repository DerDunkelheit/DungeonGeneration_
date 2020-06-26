using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue",fileName ="NewDialogue")]
public class Dialogue : ScriptableObject
{
    public string NpcName;
   [TextArea(3,10)] public string[] sentences;

}
