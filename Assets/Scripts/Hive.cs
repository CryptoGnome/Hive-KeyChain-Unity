using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using TMPro;

public class Hive : MonoBehaviour
{

#if UNITY_WEBGL && !UNITY_EDITOR

    [DllImport("__Internal")]
    private static extern void Connect(string username);


    [DllImport("__Internal")]
    private static extern void SignTx(string username, string id, string data, string prompt);
 
    
    [DllImport("__Internal")]
    private static extern void Transfer(string username, string to, string amount, string memo);

#endif

    private string username = "";


    //start
    void Start()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            username = PlayerPrefs.GetString("username");
        }
    }

    //timed update
    void Update()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }

    //connect
    public void ConnectWallet()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
        GameObject canvas = GameObject.Find("Canvas");
        //get text input from usernameInput TextMesh InputField
        TMP_InputField usernameInput = canvas.transform.Find("usernameInput").GetComponent<TMP_InputField>();
        
        if (usernameInput.text == "")
        {
            //create webgl alert
            Application.ExternalEval("alert('Username is empty, please enter your hive username & use hive keychain to sign in')");
        }
        else
        {
            //if not empty, connect to hive keychain
            Debug.Log("Username Sent: " + usernameInput.text);
            Connect(usernameInput.text);
        }
        #endif
    }


    ///test 
    public void test_btn(){
        GameObject canvas = GameObject.Find("Canvas");
        //get text input from usernameInput TextMesh InputField
        TMP_InputField usernameInput = canvas.transform.Find("usernameInput").GetComponent<TMP_InputField>();
        username = usernameInput.text;
        Debug.Log(username);

    }

    // set username
    public void SetUser(string name)
    {
        username = name;
        PlayerPrefs.SetString("username", username);
        Debug.Log("Username set to: " + username);
        Debug.Log(PlayerPrefs.HasKey("username"));
    }

    //EXAMPLE FUNCTIONS
    // transfer
    public void Transfer()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
  
        Transfer(username, "crypt0gnome", "1", "test xfer unity");
        #endif
    }

    public void Test()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
        // json {"username":"test","id":"1","data":"test","prompt":"test"}
        string json = "{\"username\":\"test\",\"id\":\"1\",\"data\":\"test\",\"prompt\":\"test\"}";
        SignTx(username, "test", json ,"Signing a test message");
        #endif
    }


}
