using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;
using System.Linq;

public class Goal : MonoBehaviour
{
    public GameObject winMessage;
    public GameObject clearMessage;
    public string message = "You Win!"; // 要显示的文字
    private static int goalCount = 0;
    private int sceneNum = 0;
    private int currentSceneIndex; // 记录当前场景的索引
    private bool isPlayingMusic = false;
    private bool isGoal = false;
    [SerializeField] AudioSource coin;//コインを取った時の音
    [SerializeField] AudioSource sceneChange;//シーン切り替わりの音

    public static List<int> loadedScenes = new List<int>();

    void Start()
    {
        // 获取当前场景的索引
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 初始化已加载场景的数组
        loadedScenes.Add(currentSceneIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !Timer.isGameover)
        {
            Debug.Log(message);
            winMessage.SetActive(true);
            Timer.timerCount = false;
            coin.Play();

            if (!isGoal)
            {
                goalCount++;
                Debug.Log("goalCount: " + goalCount);

                // 获取 BoxCollider2D 组件
                BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
                // 将 BoxCollider2D 设置为 inactive
                boxCollider2D.enabled = false;

                isGoal = true;
            }
        
            // 获取所有已存在场景的数量
            int totalScenes = SceneManager.sceneCountInBuildSettings;
            Debug.Log("totalScenes： " + totalScenes);

            // 检查是否已经加载过所有场景
            if (goalCount >= totalScenes)
            {
                Debug.Log("GameClear!!");
                clearMessage.SetActive(true);
                Invoke("GameClear", 3f);
                return;
            }
            else
            {
                // 生成一个不超过场景数量范围且不等于当前场景索引和已加载场景的随机数
                do
                {
                    sceneNum = Random.Range(0, totalScenes);
                } while (loadedScenes.Contains(sceneNum));
            }

            // 将随机场景加入已加载场景数组
            loadedScenes.Add(sceneNum);

            // 等待2秒
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

    private void GameClear()
    {
        // 所有场景都已加载，退出游戏
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // 加载随机场景
        SceneManager.LoadScene(sceneNum);

        isGoal = false;
    }
}
