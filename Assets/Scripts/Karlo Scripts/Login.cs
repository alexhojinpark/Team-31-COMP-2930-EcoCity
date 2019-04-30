﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public GameObject username;
    public GameObject password;
    private string Username;
    private string Password;

    // Update is called once per frame
    void Update()
    {
        Tab();
        Enter();
        AssignInputs();
    }

    public void LoginButton() {
        if (CheckUsername() && CheckPassword()) {
            Password = EncryptPassword();
            StartCoroutine(UserLogin("http://localhost/sqlconnect/login.php"));
        }
    }

    IEnumerator UserLogin(string url) {
        WWWForm form = new WWWForm();
        form.AddField("name", Username);
        form.AddField("password", Password);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form)) {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError || webRequest.isHttpError) {
                Debug.Log(webRequest.error);
            } else {
                Debug.Log("Form upload complete!");
            }
            if (webRequest.downloadHandler.text[0] == '0') {
                DBManager.username = Username;
                DBManager.score = int.Parse(webRequest.downloadHandler.text.Split('\t')[1]);
                Debug.Log("User login success");
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            } else {
                Debug.Log("User login failed. Error #" + webRequest.downloadHandler.text);
            }
        }
    }

    private bool CheckUsername() {
        if (Username != "") {
            return true;
        } else {
            Debug.LogWarning("Username field empty");
            return false;
        }
    }

    private bool CheckPassword() {
        if (Password != "") {
            return true;
        } else {
            Debug.LogWarning("Username field empty");
            return false;
        }
    }


    private void AssignInputs() {
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
    }

    private string EncryptPassword() {
        string EncryptedPass = "";
        for (int i = 1; i <= Password.Length; i++) {
            EncryptedPass += ((char)(Password[i - 1] * (i + 1))).ToString();
        }
        return EncryptedPass;
    }

    private string DecryptPassword() {
        string DecryptedPass = "";
        for (int i = 1; i <= Password.Length; i++) {
            DecryptedPass += ((char)(Password[i - 1] / (i + 1))).ToString();
        }
        return DecryptedPass;
    }

    /*************************
     * Keyboard controls
     ************************/
    private void Tab() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (username.GetComponent<InputField>().isFocused) {
                password.GetComponent<InputField>().Select();
            }
        }
    }

    public void Enter() {
        if (Input.GetKeyDown(KeyCode.Return)
            && (username.GetComponent<InputField>().isFocused
            || password.GetComponent<InputField>().isFocused)) {
            LoginButton();
        }
    }
}
