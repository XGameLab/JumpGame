using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject winMessage;
    public string message = "You Win!"; // 要显示的文字
    private static int goalCount = 0;
    private int sceneNum = 0;
    private int currentSceneIndex; // 记录当前场景的索引
    private bool isPlayingMusic = false;
    [SerializeField] AudioSource coin;//コインを取った時の音
    [SerializeField] AudioSource sceneChange;//シーン切り替わりの音

    void Start()
    {
        // 获取当前场景的索引
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !Timer.isGameover)
        {
            Debug.Log(message);
            winMessage.SetActive(true);
            Timer.timerCount = false;
            coin.Play();

            // 获取所有已存在场景的数量
            int totalScenes = SceneManager.sceneCountInBuildSettings;
            Debug.Log("totalScenes： " + totalScenes);

            // 检查是否已经加载过所有场景
            if (goalCount >= totalScenes - 1)
            {
                Debug.Log("All Scenes Loaded!");
                // 所有场景都已加载，退出游戏
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
                return;
            }


            // 生成一个不超过场景数量范围且不等于当前场景索引的随机数
            do
            {
                sceneNum = Random.Range(0, totalScenes);
            } while (sceneNum == currentSceneIndex);

            // 等待3秒
            StartCoroutine(LoadSceneAfterDelay(2f));
            //二秒後に音楽が鳴る
            Invoke("MusicScene", 1.2f);
        }
    }

    private void MusicScene()
    {
        if (!isPlayingMusic)
        {
            // 音楽を再生
            sceneChange.Play();
        }
        isPlayingMusic = true;
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // 加载随机场景
        SceneManager.LoadScene(sceneNum);

        goalCount++;
        Debug.Log("goalCount: " + goalCount);
    }
}
