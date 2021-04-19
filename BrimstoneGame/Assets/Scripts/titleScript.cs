using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleScript : MonoBehaviour
{
    // Start is called before the first frame update
     public Button startButton;
    public Button quitButton;

    private void Start()
    {
        startButton.onClick.AddListener((UnityEngine.Events.UnityAction)this.startButtonClick);
        quitButton.onClick.AddListener((UnityEngine.Events.UnityAction)this.quitButtonClick);

    }
    void Update()
    {
        
    }
   void startButtonClick()
    {
        Application.LoadLevel("Level1");
    }
    
    void quitButtonClick()
    {
        Application.Quit(0);
    }
}
