using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherParticles : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> weatherList;

    // Use this for initialization
    void Awake()
    {
        foreach (GameObject weatherObject in weatherList)
        {
            weatherObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayWeatherEffect(GameObject gameObject)
    {
        foreach (GameObject weatherObject in weatherList)
        {
            if (gameObject == weatherObject)
            {
                if (weatherObject.activeSelf)
                    weatherObject.SetActive(false);
                else
                    weatherObject.SetActive(true);
                //break;
            }            
            else
                weatherObject.SetActive(false);
        }
    }
}
