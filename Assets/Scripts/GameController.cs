using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    public bool TutorialEnabled = true;
    public bool GameStarted = false;
    public bool ForceTouchScreen = false;

    private void Awake()
    {
        

        if (instance != null || instance != this)
            Destroy(this.gameObject);

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        //load UI additive
        SceneManager.LoadScene(1, LoadSceneMode.Additive);

    }
	
	void Update () {
		//distance score
        //quests?
	}

    private void RestartGame()
    {
    }
}
