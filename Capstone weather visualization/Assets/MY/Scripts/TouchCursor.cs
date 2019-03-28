using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchCursor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject cursor;

    public GameObject[] cursors;

    const int maxNum = 10;
    // Start is called before the first frame update
    void Start()
    {
        if(cursor)
        {
            cursors = new GameObject[maxNum];
            cursor.transform.SetParent(transform);
            for(int i = 0; i < maxNum; i++)
            {
                cursors[i] = cursor;
                Instantiate(cursors[i]);
                cursors[i].SetActive(false);
            }


        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    //If a mouse clicked the UI was detected
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        if (Input.GetMouseButton(0))
        {
            if (!cursors[0].activeSelf)
                cursors[0].SetActive(true);


            cursors[0].GetComponent<RectTransform>().position = Input.mousePosition;
        }

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < maxNum; i++)
            {
                if (!cursors[i].activeSelf)
                {
                    cursors[i].SetActive(true);

                }

                cursors[i].transform.position = Input.touches[i].position;
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            if (cursors[0].activeSelf)
                cursors[0].SetActive(false);
        }
        for (int i = 0; i < maxNum; i++)
        {
            if (cursors[i].activeSelf)
                cursors[i].SetActive(false);
        }
    }
}
