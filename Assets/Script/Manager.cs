using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject[] ppt;
    int currentPPT;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextPPT()
    {
        if (currentPPT + 1 != ppt.Length)
        {
            ppt[currentPPT].SetActive(false);

            currentPPT++;
            ppt[currentPPT].SetActive(true);
        }
    }
    public void prevPPT()
    {
        if (currentPPT - 1 != ppt.Length)
        {
            ppt[currentPPT].SetActive(false);

            currentPPT--;
            ppt[currentPPT].SetActive(true);
        }
    }
}
