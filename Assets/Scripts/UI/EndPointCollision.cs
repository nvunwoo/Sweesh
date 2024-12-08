using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPointCollision : MonoBehaviour
{
    public string targetSceneName = "Start"; // 버튼을 활성화시킬 씬 이름
    public int stageNumber; // 현재 스테이지 번호 (1 또는 2)
    public float delayTime = 4f; // 씬 전환 전에 대기할 시간 (초)

    private void OnTriggerEnter(Collider other)
    {
        // Player 태그와 충돌하면 4초 후 Start 씬으로 전환
        Debug.Log("충돌 시작");
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌 확인");

            // 스테이지 번호에 따라 클리어 플래그 설정 (여기서는 단순히 플래그만 세운다)
            if (stageNumber == 1)
            {
                Debug.Log("스테이지 1 클리어 처리");
                GameManager.instance.Stage1Cleared();
            }
            else if (stageNumber == 2)
            {
                Debug.Log("스테이지 2 클리어 처리");
                GameManager.instance.Stage2Cleared();
            }
            else
            {
                Debug.LogError("잘못된 스테이지 번호: " + stageNumber);
            }

            // 씬 전환
            Debug.Log("씬 전환 시작");
            StartCoroutine(WaitAndChangeScene());
        }
        else
        {
            Debug.Log("플레이어가 아닌 오브젝트와 충돌");
        }
    }

    // 씬 전환을 위한 코루틴
    private IEnumerator WaitAndChangeScene()
    {
        Debug.Log("대기 시간 시작: " + delayTime + "초");
        // 지정된 대기 시간 후 씬 전환
        yield return new WaitForSeconds(delayTime);
        Debug.Log("대기 시간 종료");

        // Start 씬을 로드
        try
        {
            Debug.Log("씬 전환 시도: " + targetSceneName);
            SceneManager.LoadScene(targetSceneName);
            Debug.Log("씬 찾기 완료: " + targetSceneName);
        }
        catch (System.Exception e)
        {
            Debug.LogError("씬 로딩 중 오류 발생: " + e.Message);
        }
    }
}
