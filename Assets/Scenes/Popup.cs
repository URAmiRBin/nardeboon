using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : UIElement {
    [SerializeField] Text popupMessage;

    public void AssemblePopup(string message) { 
        popupMessage.text = message;
    }
}
