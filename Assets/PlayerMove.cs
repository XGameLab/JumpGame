//////////////////////////////
////////////未使用////////////
/////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 玩家动画控制器
    public Animator animator;

    // 玩家跳跃音效
    public AudioSource jumpSound;

    private float moveSpeed = 5f;
    private float jumpHeight = 8f;
    private float playerScale = 0.3f;

    private void Update()
    {
        // 获取玩家水平方向的输入
        float dirX = Input.GetAxisRaw("Horizontal");

        // 根据输入值，设置玩家的朝向
        if (dirX < 0)
        {
            transform.localScale = new Vector3(-playerScale, playerScale, playerScale);
        }
        else if (dirX > 0)
        {
            transform.localScale = new Vector3(playerScale, playerScale, playerScale);
        }

        // 移动玩家
        transform.Translate(new Vector3(dirX * moveSpeed * Time.deltaTime, 0, 0));

        // 判断玩家是否按下跳跃键
        if (Input.GetButtonDown("Jump"))
        {
            // 给玩家施加一个向上的力
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);

            // 播放跳跃音效
            jumpSound.Play();

            // 触发跳跃动画
            animator.SetTrigger("Jump");
        }
    }
}
