using UnityEngine;

public class DialTrigger : MonoBehaviour
{
    public Dialogue[] dialogue;
    public bool oneTimeTrigger;
    public void triggerDialogue()
    {
        FindObjectOfType<DialManager>().startDialogue(dialogue);
    }
}
