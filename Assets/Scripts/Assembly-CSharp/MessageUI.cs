using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;

public class MessageUI : AbstractUI, IMessageUI
{
    private int previousUIID;
    private float timeScale;
    private VNLUtil.NoNameMethod onOK;
    private bool isClosed;
    private UISlicedSprite nonBlockedBG;
    private UISlicedSprite blockedBG;
    private string replacementValue;
    private Camera cam;
    private List<string> texts = new List<string>();
    private int textIndex;
    private UILabel messageLabel;
    private float autoCloseTimer = 0f; // Timer for auto-closing

    public bool IsClosed
    {
        get { return isClosed; }
    }

    protected override void Awake()
    {
        base.Awake();
        cam = base.transform.Find("Camera").GetComponent<Camera>();
        timeScale = Time.timeScale;
        previousUIID = VNLUtil.getInstance().activeUIID;
        VNLUtil.getInstance().activeUIID = 10;
        nonBlockedBG = base.transform.Find("Camera/Anchor/Panel/MsgBG1").GetComponent<UISlicedSprite>();
        blockedBG = base.transform.Find("Camera/Anchor/Panel/MsgBG2").GetComponent<UISlicedSprite>();
        blockedBG.enabled = false;
        messageLabel = base.transform.Find("Camera/Anchor/Panel/MessageUILabel").GetComponent<UILabel>();
        //APIService.toggleAd(true);
    }

    public void setMessage(string txt)
    {
        setMessage(new string[1] { txt }, null, true, null);
    }

    public void setMessage(string txt, VNLUtil.NoNameMethod onOK, bool pause)
    {
        setMessage(new string[1] { txt }, onOK, pause, null);
    }

    public void setMessage(string[] txts, VNLUtil.NoNameMethod onOK, bool pause, string replacementValue)
    {
        this.replacementValue = replacementValue;
        int count = texts.Count;
        texts = texts.Union(new List<string>(txts)).ToList();
        if (pause)
        {
            VNLUtil.getInstance().pause();
        }
        if (count == 0)
        {
            displayCurrentMessage();
        }
        if (onOK != null)
        {
            this.onOK = (VNLUtil.NoNameMethod)Delegate.Combine(this.onOK, onOK);
        }
    }

    public IMessageUI setTitle(string title)
    {
        UILabel component = base.transform.Find("Camera/Anchor/Panel/TitleLabel").GetComponent<UILabel>();
        component.text = title;
        return this;
    }

    public IMessageUI setBlock(bool block)
    {
        nonBlockedBG.enabled = !block;
        blockedBG.enabled = block;
        return this;
    }

    public string getQueuedTitle()
    {
        return string.Empty;
    }

    public bool getQueuedBlock()
    {
        return false;
    }

    private void displayCurrentMessage()
    {
        string text = Language.Get(texts[textIndex]).Replace("_PLACEHOLDER_", replacementValue);
        if (text.Contains("MISSING LANG:"))
        {
            text = texts[textIndex].Replace("\n ", "\n").Replace(" \n", "\n").Replace("\n", " ")
                .Replace("MISSING LANG:", string.Empty);
        }
        text = text.Replace("==>", ":\n\n");
        if (Application.platform == RuntimePlatform.Android && GameStart.requiresAndroidText)
        {
            displayAndroidText(text);
        }
        else
        {
            displayNGuiText(text);
        }
        textIndex++;
        autoCloseTimer = 0f; // Reset auto-close timer when a new message appears
    }

    private void displayNGuiText(string msg)
    {
        float num = 447f / (float)Screen.height;
        float num2 = (float)Screen.width * 0.75f * num;
        messageLabel.lineWidth = (int)num2;
        messageLabel.text = msg;
        Debug.Log("displayNGuiText: " + msg);
    }

    private void displayAndroidText(string msg)
    {
        messageLabel.text = string.Empty;
        APIService.displayText(msg);
    }

    protected virtual void Update()
    {
        // Allow Zeemote controller input
        if (GameStart.isZeemoteConnected && ZeemoteInput.GetButtonUp(1, 2))
        {
            Debug.Log("Zeemote button pressed. Calling clickedOK()");
            if (this.GetType().GetMethod("clickedOK") != null)
            {
                Debug.Log("clickedOK() method found!");
                clickedOK();
            }
            else
            {
                Debug.LogError("clickedOK() method not found!");
            }
        }

        // If running in a build (not in Editor), auto-close after 3 seconds
        if (!Application.isEditor)
        {
            autoCloseTimer += Time.unscaledDeltaTime; // Works even if the game is paused
            if (autoCloseTimer >= 3f)
            {
                Debug.Log("Auto-close timer reached 3 seconds. Calling clickedOK()");
                Invoke("SafeClickedOK", 0f); // Call SafeClickedOK instead of directly calling clickedOK()
                autoCloseTimer = 0f; // Reset in case multiple messages queue up
            }
        }
    }
    [Preserve]
    public void SafeClickedOK()
    {
        try
        {
            clickedOK();
        }
        catch (Exception e)
        {
            Debug.LogError("Error calling clickedOK(): " + e.Message);
        }
    }

    [Preserve]
    public void clickedOK()
    {
        Debug.Log("clickedOK was called");

        if (!isActiveUI())
        {
            Debug.LogWarning("clickedOK() called but UI is not active.");
            return;
        }
        if (textIndex < texts.Count)
        {
            displayCurrentMessage();
            playOpenSound();
            return;
        }
        close();
        if (onOK != null)
        {
            VNLUtil.getInstance().doStartCoRoutine(onOK);
        }
    }

    protected void close()
    {
        APIService.unDisplayText();
        if (!isClosed)
        {
            VNLUtil.toggleController(true);
            Time.timeScale = timeScale;
            VNLUtil.getInstance().activeUIID = previousUIID;
            playCloseSound();
            UnityEngine.Object.Destroy(base.gameObject);
            isClosed = true;
          //  APIService.toggleAd(false);
        }
    }

    protected bool isActiveUI()
    {
        return VNLUtil.getInstance().activeUIID == 10;
    }
}
