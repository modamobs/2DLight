using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArrowBlink : MonoBehaviour
{
    [SerializeField]
    private	float	fadeTime;	// 페이드 인/아웃 하나당 걸리는 시간
    private	Image	fadeImage;	// 페이드 효과가 적용되는 Image UI 컴포넌트

    /// <summary>
    /// 초기화 - Image 컴포넌트를 가져옴
    /// </summary>
    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    /// <summary>
    /// 오브젝트가 활성화되면 깜빡이는 효과 시작
    /// </summary>
    private void OnEnable()
    {
        // Fade 효과를 In -> Out 순서로 반복한다.
        StartCoroutine("FadeInOut");
    }

    /// <summary>
    /// 오브젝트가 비활성화되면 깜빡이는 효과 중단
    /// </summary>
    private void OnDisable()
    {
        // 코루틴 중단
        StopCoroutine("FadeInOut");
    }

    /// <summary>
    /// 페이드 인/아웃을 무한 반복하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeInOut()
    {
        while ( true )
        {
            // 불투명(1) -> 투명(0)으로 페이드 인
            yield return StartCoroutine(Fade(1, 0));

            // 투명(0) -> 불투명(1)으로 페이드 아웃  
            yield return StartCoroutine(Fade(0, 1));
        }
    }

    /// <summary>
    /// 지정된 알파값 범위에서 부드럽게 페이드하는 코루틴
    /// </summary>
    /// <param name="start">시작 알파값</param>
    /// <param name="end">끝 알파값</param>
    /// <returns></returns>
    private IEnumerator Fade(float start, float end)
    {
        float current = 0;		// 경과 시간
        float percent = 0;		// 진행률 (0~1)

        // 페이드 완료까지 반복
        while ( percent < 1 )
        {
            // 경과 시간 누적
            current += Time.deltaTime;
            // 진행률 계산 (0~1 사이값)
            percent = current / fadeTime;

            // 현재 이미지 색상 가져오기
            Color color		= fadeImage.color;
            // 알파값을 시작값에서 끝값으로 선형 보간
            color.a			= Mathf.Lerp(start, end, percent);
            // 변경된 색상 적용
            fadeImage.color	= color;

            // 다음 프레임까지 대기
            yield return null;
        }
    }
}

