using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CrewmateSelector : MonoBehaviour
{
    
    RectTransform rectTransform;
    public Vector2 startPos, mouseOffset, mousePos, fixedPos = Vector2.zero;
    int dropValue = 0;
    bool dragging = false;

    public GameObject crewOneGO, crewTwoGO;
    Vector2 crewOnePos, crewTwoPos;
    float startRadius, crewOneRadius, crewTwoRadius;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        startRadius = rectTransform.sizeDelta.magnitude / 2;

        crewOnePos = crewOneGO.GetComponent<RectTransform>().position;
        crewOneRadius = crewOneGO.GetComponent<RectTransform>().sizeDelta.magnitude / 2;

        crewTwoPos = crewTwoGO.GetComponent<RectTransform>().position;
        crewTwoRadius = crewTwoGO.GetComponent<RectTransform>().sizeDelta.magnitude / 2;
    }

    // Update is called once per frame
    void Update()
    {
        fixedPos = new (rectTransform.position.x, rectTransform.position.y);
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition) * new Vector2(1920, 1080);
        if (EventSystem.current.IsPointerOverGameObject() && !dragging)
        {
            Debug.Log("hoverin");
            if (Input.GetMouseButtonDown(0))
            {
                
                dragging = true;
                mouseOffset = mousePos - fixedPos;
                Debug.Log("dragging " + mousePos + " " + mouseOffset + " " + rectTransform.position);
            }
        }

        if (Input.GetMouseButton(0) && dragging)
        {
            rectTransform.position = mousePos + mouseOffset;
            fixedPos = rectTransform.position;

            if (Vector2.Distance(fixedPos, startPos) < startRadius)
            {
                dropValue = 0;
            }
            else if (Vector2.Distance(fixedPos, crewOnePos) < crewOneRadius)
            {
                dropValue = 1;
            }
            else if (Vector2.Distance(fixedPos, crewTwoPos) < crewTwoRadius)
            {
                dropValue = 2;
            }
        }
        else
        {
            dragging = false;
        }
    }

    public void ResetPos()
    {
        dropValue = 0;
        
    }
}
