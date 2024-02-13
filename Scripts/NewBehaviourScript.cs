using System.Collections;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using static Loader;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 50f;
    public GameObject judgeLineObject;
    public float globalTime = 0f;
    public Chart chart;
    void Start()
    {
        string jsonText = File.ReadAllText(@"C:\Users\snowy\Documents\MyWork\Migros\res\chapter-6\micro.wav\Chart_IN.json", UTF8Encoding.UTF8);
        chart = JsonUtility.FromJson<Chart>(jsonText);
       
    }

    // Update is called once per frame
    void Update()
    {
        
        globalTime += Time.deltaTime;
        Debug.Log(globalTime);
        int pTime = Convert.ToInt32(globalTime / (1.875 / chart.judgeLineList[0].bpm));
        double[] pos = chart.judgeLineList[0].GetPosition(pTime);
        Debug.Log(((float)pos[0], (float)pos[1]));
        transform.localPosition = new Vector3((float)pos[0], (float)pos[1], 0);
        transform.localEulerAngles = new Vector3(0, 0 ,(float)chart.judgeLineList[0].GetRotation(pTime));

    }
}

