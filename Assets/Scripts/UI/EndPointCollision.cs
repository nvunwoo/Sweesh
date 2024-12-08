using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPointCollision : MonoBehaviour
{
    public string targetSceneName = "Start"; // ��ư�� Ȱ��ȭ��ų �� �̸�
    public int stageNumber; // ���� �������� ��ȣ (1 �Ǵ� 2)
    public float delayTime = 4f; // �� ��ȯ ���� ����� �ð� (��)

    private void OnTriggerEnter(Collider other)
    {
        // Player �±׿� �浹�ϸ� 4�� �� Start ������ ��ȯ
        Debug.Log("�浹 ����");
        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾�� �浹 Ȯ��");

            // �������� ��ȣ�� ���� Ŭ���� �÷��� ���� (���⼭�� �ܼ��� �÷��׸� �����)
            if (stageNumber == 1)
            {
                Debug.Log("�������� 1 Ŭ���� ó��");
                GameManager.instance.Stage1Cleared();
            }
            else if (stageNumber == 2)
            {
                Debug.Log("�������� 2 Ŭ���� ó��");
                GameManager.instance.Stage2Cleared();
            }
            else
            {
                Debug.LogError("�߸��� �������� ��ȣ: " + stageNumber);
            }

            // �� ��ȯ
            Debug.Log("�� ��ȯ ����");
            StartCoroutine(WaitAndChangeScene());
        }
        else
        {
            Debug.Log("�÷��̾ �ƴ� ������Ʈ�� �浹");
        }
    }

    // �� ��ȯ�� ���� �ڷ�ƾ
    private IEnumerator WaitAndChangeScene()
    {
        Debug.Log("��� �ð� ����: " + delayTime + "��");
        // ������ ��� �ð� �� �� ��ȯ
        yield return new WaitForSeconds(delayTime);
        Debug.Log("��� �ð� ����");

        // Start ���� �ε�
        try
        {
            Debug.Log("�� ��ȯ �õ�: " + targetSceneName);
            SceneManager.LoadScene(targetSceneName);
            Debug.Log("�� ã�� �Ϸ�: " + targetSceneName);
        }
        catch (System.Exception e)
        {
            Debug.LogError("�� �ε� �� ���� �߻�: " + e.Message);
        }
    }
}
