using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro; 

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    public GameObject gameoverMessage;
    private float time = 10f;
    public static bool timerCount;
    public static bool isGameover;
 

    void Start()
    {
        timerCount = true;
        isGameover = false;
    }

    void Update()
    {
        if (0 <= time && timerCount ) 
        {
            time -= Time.deltaTime;
            timeText.text = "Time: " + time.ToString("F1");
        }

        else if(0 >= time && timerCount )
        {
            timeText.text = "TimeUp!";
            StartCoroutine(GameOver(2f));
            gameoverMessage.SetActive(true);
            isGameover = true;
        }
    }

    IEnumerator GameOver(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}