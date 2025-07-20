using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public void SelectCharacter(int characterIndex)
    {
        // 선택한 캐릭터를 저장
        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
        PlayerPrefs.Save();
        
        // 페이드 아웃 후 씬 변경
        SceneTransitionManager.Instance.FadeOutAndChangeScene(2); // 메인 씬 인덱스
    }
}