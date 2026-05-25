using UnityEngine;
using UnityEngine.SceneManagement;


public class mainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);


    }
    public void Reviewer()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void Java1()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void quiz1()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void Java2()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void quiz2()
    {
        SceneManager.LoadSceneAsync(5);
    }
    public void webdev1()
    {
        SceneManager.LoadSceneAsync(6);
    }
    public void quiz3()
    {
        SceneManager.LoadSceneAsync(7);
    }
    public void webdev2()
    {
        SceneManager.LoadSceneAsync(8);
    }
    public void quiz4()
    {
        SceneManager.LoadSceneAsync(9);
    }

    public void QuitGame()
    {
        Debug.Log("Game is quitting..."); // Only visible in editor
        Application.Quit();
    }

}
