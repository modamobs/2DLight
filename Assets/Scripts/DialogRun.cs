using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class DialogRun : MonoBehaviour
{
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private DialogSystem[] dialogSystems;           // 여러 대화 시스템을 배열로 관리
    [SerializeField]
    private TextMeshProUGUI textCountdown;          // 엔딩 메시지 출력용 텍스트

    [SerializeField]
    private string EndText;                   // 마지막 내용

    [SerializeField]
    private PlayableDirector playableDirector; // 타임라인 제어용

    // 타임라인에서 호출: 타임라인 일시정지 후 다이얼로그 시작
    public void StartDialogFromTimeline()
    {
        if (playableDirector != null)
            playableDirector.Pause();

        dialoguePanel.SetActive(true);
        StartDialogCore();
    }

    // 버튼에서 호출: 타임라인 제어 없이 다이얼로그만 시작
    public void StartDialogFromButton()
    {
        StartDialogCore();
    }

    // 실제 다이얼로그 실행 로직
    private void StartDialogCore()
    {
        // 엔딩 메시지 텍스트 비활성화
        textCountdown.gameObject.SetActive(false);

        // 코루틴 한 번만 실행
        StartCoroutine(RunDialogSystems());
    }

    private IEnumerator RunDialogSystems()
    {
        textCountdown.gameObject.SetActive(false);

        // 모든 대화 시스템을 순차적으로 실행
        for (int i = 0; i < dialogSystems.Length; i++)
        {
            yield return new WaitUntil(() => dialogSystems[i].UpdateDialog());
        }

        // 엔딩 메시지 출력
        textCountdown.gameObject.SetActive(true);
        textCountdown.text = EndText;
        yield return new WaitForSeconds(2);
        dialoguePanel.SetActive(false);

        // 타임라인에서 시작한 경우에만 Resume
        if (playableDirector != null && playableDirector.state == PlayState.Paused)
            playableDirector.Resume();
    }
}

