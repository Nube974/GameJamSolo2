using System.Collections;
using UnityEngine;
using TMPro;
public class CountdownTimer : MonoBehaviour
{
    public float time;
    public TextMeshProUGUI Timertext;

    void Start()
    {
        StartCoroutine(Timer());
        Timertext.text = time.ToString();
    }
    public IEnumerator Timer()
    {

        while (time > 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
            Timertext.text = time.ToString();
            if (time == 0) Timertext.text = "YOU WIN";

        }

    }
}
