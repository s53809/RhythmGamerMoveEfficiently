using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateButtonEditor : MonoBehaviour
{
    private String StateName = "";
    private UnityEngine.UI.Text viewState;
    void Awake()
    {
        viewState = this.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
    }

    public void RefreshButton(String name)
    {
        if (name == null) { Debug.Log("예외처리"); Destroy(this.gameObject); }
        StateName = name;
        viewState.text = StateName;
    }
}
