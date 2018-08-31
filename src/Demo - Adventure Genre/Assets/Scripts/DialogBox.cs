using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    //public TimeInterval WaitTime = 1f;
    public int CurrentTextIndex = -1;
    public Image ImageContainer;
    public List<string> Texts = new List<string>();
    public Text TextContainer;

    private string currentText = "";
    private States state = States.Normal;
    protected States State
    {
        get
        {
            return this.state;
        }
        set
        {
            if (this.state != value)
            {
                this.state = value;

                //Set variables depending new state.
                switch (this.state)
                {
                    #region Normal
                    case States.Normal:
                        {
                            break;
                        }
                    #endregion
                    //#region Waiting
                    //case States.Waiting:
                    //    {

                    //        break;
                    //    }
                    //#endregion
                }
            }
        }
    }


    // Use this for initialization
    void Start () {
        this.CompleteLineOrNextLine();
    }
	
	// Update is called once per frame
	void Update () {
        switch (this.State)
        {
            #region Normal
            case States.Normal:
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        this.CompleteLineOrNextLine();

                        //this.State = States.Waiting;
                    }

                    break;
                }
            #endregion
        }


    }

    private void CompleteLineOrNextLine()
    {
        this.CurrentTextIndex++;

        if (this.CurrentTextIndex < this.Texts.Count)
            this.currentText = this.Texts[this.CurrentTextIndex];

        this.TextContainer.text = this.currentText;
    }

    #region Subclasses

    protected enum States : int
    {
        Normal = 0,
        //Waiting,
    }

    #endregion
}
