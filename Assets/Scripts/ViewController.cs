using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.SceneManagement;

public class ViewController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ResetScene();
    }

    void MoveSelectionForElement()
    {
        if (XCI.GetButtonDown(XboxButton.DPadLeft))
        {

        }
    }

    void ResetScene()
    {
        if (XCI.GetButtonDown(XboxButton.Start))
        {
            SceneManager.LoadScene(0);
        }
    }
}
