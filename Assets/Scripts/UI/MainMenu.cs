﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnStart()
    {
        GameManager.Instance.ChangeScene("GameLevel");
    }

    public void OnOptions()
    {

    }
}
