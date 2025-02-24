using UnityEngine;
using UnityEngine.UI;

public class messagemanager : MonoBehaviour
{
    public GameObject messageUIPrefab; // Assign the MessageUI prefab here in the inspector
    private GameObject currentMessageUI;

    public void ShowMessage(string messageText)
    {
        // Instantiate the MessageUI prefab
        currentMessageUI = Instantiate(messageUIPrefab, transform);

        // Get the MessageUI component from the instantiated object
        MessageUI messageUI = currentMessageUI.GetComponent<MessageUI>();

        // Set the message text or any other UI element on the MessageUI
        messageUI.setMessage(messageText);

        // Find the button in the MessageUI and assign the OnClick event
        Button okButton = currentMessageUI.transform.Find("OKButton").GetComponent<Button>();
        okButton.onClick.RemoveAllListeners(); // Clear any existing listeners
        okButton.onClick.AddListener(() => OnOkButtonClick(messageUI));
    }

    private void OnOkButtonClick(MessageUI messageUI)
    {
        messageUI.clickedOK(); // Trigger the OK button logic in the MessageUI
        Destroy(currentMessageUI); // Destroy the MessageUI after it's done
    }
}
