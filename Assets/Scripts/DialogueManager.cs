using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogueManager : MonoBehaviour, INotificationReceiver
{
    [Header("다이얼로그 UI")]
    public GameObject dialoguePanel;
    public TMPro.TextMeshProUGUI dialogueText;
    
    [Header("다이얼로그 내용")]
    public string[] dialogueLines;
    
    private PlayableDirector playableDirector;
    private int currentLineIndex = 0;

    DialogSystem dialogSystem;

    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
            
        dialogSystem = FindFirstObjectByType<DialogSystem>();
        if (dialogSystem == null)
        {
            Debug.LogError("DialogSystem 컴포넌트를 찾을 수 없습니다!");
            return;
        }
    }
    
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is SignalEmitter signalEmitter)
        {
            if (signalEmitter.asset != null && signalEmitter.asset.name == "DialogueSignal")
            {
                StartDialogue();
            }
        }
    }
    
    void StartDialogue()
    {
        playableDirector.Pause();
        dialoguePanel.SetActive(true);
        currentLineIndex = 0;
        ShowCurrentLine();
    }
    
    void ShowCurrentLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
        }
    }
    
    void Update()
    {
        if (dialoguePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
    }
    
    void NextLine()
    {
        currentLineIndex++;
        
        if (currentLineIndex < dialogueLines.Length)
        {
            ShowCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }
    
    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        playableDirector.Resume();
    }
}