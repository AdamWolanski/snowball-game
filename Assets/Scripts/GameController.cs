using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    public bool TutorialEnabled = true;
    public bool GameStarted = false;
    public bool ForceTouchScreen = false;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);

        //load UI additive
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
        
	}
	
	void Update () {
		//distance score
        //quests?
	}

    private void RestartGame()
    {
    }
}
