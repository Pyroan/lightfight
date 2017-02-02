using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MMButton : MonoBehaviour {

    public int level;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void switchScene()
    {
        SceneManager.LoadScene(level);
    }
}
