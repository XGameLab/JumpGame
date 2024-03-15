using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject winMessage;
    public string message = "You Win!"; // 要显示的文字
    private int goalCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && goalCount == 0)
        {
            goalCount++;

            Debug.Log(message);
            winMessage.SetActive(true);

            // 您可以添加其他功能，例如：
            //   * 播放音效
            //   * 显示游戏结束画面
            //   * 加载下一关
        }
    }
}
