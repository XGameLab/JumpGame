using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jama : MonoBehaviour
{
    private Vector3 minScreenPos;
    private Vector3 maxScreenPos;

    void Start()
    {
        // 计算屏幕的最小和最大坐标
        minScreenPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maxScreenPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    void Update()
    {
        // 检查玩家的坐标是否超出屏幕范围
        if (transform.position.x < minScreenPos.x)
        {
            transform.position = new Vector3(minScreenPos.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > maxScreenPos.x)
        {
            transform.position = new Vector3(maxScreenPos.x, transform.position.y, transform.position.z);
        }
    }
}
