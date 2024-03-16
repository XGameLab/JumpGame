using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnime : MonoBehaviour
{
    private Animator p_Animator = null;    

    // 初期化処理
    private void Start()
    {
        p_Animator = GetComponent<Animator>();
    }

    // 更新処理
    private void Update()
    {
        // 左右移動アニメーション
        if (Input.GetButtonDown("Horizontal"))
        {
            p_Animator.SetTrigger("Run");
        }

        if (Input.GetButtonDown("Jump"))
        {
            p_Animator.SetTrigger("Jump");
        }
    }

   
}
