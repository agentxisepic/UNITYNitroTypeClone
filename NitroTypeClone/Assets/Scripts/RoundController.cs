using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoundController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI StringToTypeField;
    [SerializeField] TextMeshProUGUI CorrectlyTypedOverlay;
    [SerializeField] TextMeshProUGUI PlayerTimerField;

    private string StringToType;

    private int currentIndex;


    private float playerTime;
    private bool bStopTimer;

    private void Awake()
    {
        StringToType = "This is some sample text, can you type it?";
        currentIndex = 0;
        bStopTimer = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        StringToTypeField.text = StringToType;
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInputAgainstText();
    }

    private void StartTimer()
    {
        StartCoroutine(RunTimer());
    }

    private void StopTimer()
    {
        bStopTimer = true;
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        PlayerTimerField.text = playerTime.ToString();
    }

    private IEnumerator RunTimer()
    {
        do
        {
            playerTime = Time.time;
            UpdateTimer();
            yield return new WaitForSeconds(0.01f);
        } while (!bStopTimer);

    }

    private void CheckUserInputAgainstText()
    {
        if (currentIndex == StringToType.Length)
        {
            CorrectlyTypedOverlay.gameObject.SetActive(false);
            StopTimer();
            StringToTypeField.text = "You did it?";
        }

        if (Input.anyKeyDown)
        {
            string textThisFrame = Input.inputString;
            if (textThisFrame.Length > 0)
            {
                string substr = StringToType.Substring(currentIndex, textThisFrame.Length);
                CompareStringsWithError(substr, textThisFrame, currentIndex, textThisFrame.Length);
            }
        }
    }

    private void CompareStringsWithError(string CorrectString, string UserInput, int start, int end)
    {
        for (int i = 0; i < UserInput.Length; i++)
        {
            if (CorrectString[i] == UserInput[i])
            {
                currentIndex++;
                CorrectlyTypedOverlay.text += CorrectString[i];
                continue;
            }
            else
            {
                StartCoroutine(Mistype());
                return;
            }
        }

    }


    private IEnumerator Mistype()//Is there a better way to do this?
    {
        StringToTypeField.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(0.1f);
        StringToTypeField.color = new Color(255, 255, 255);
    }





    ///FUTURE FUNCTIONS, For another class
    private void GetText()
    {
        return;
    }
}
