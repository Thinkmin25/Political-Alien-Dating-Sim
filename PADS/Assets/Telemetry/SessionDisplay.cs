using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SessionDisplay : MonoBehaviour
{
    public void OnSuccess(int sessionId) {
        GetComponent<TextMeshProUGUI>().text = sessionId < 0 ?
        $"Offline session #{sessionId} - debug logging only" :
        $"Connected to server: session #{sessionId}";
    }

    public void OnError(string error) {
        GetComponent<TextMeshProUGUI>().text = $"Connection error: {error}";
    }
}
