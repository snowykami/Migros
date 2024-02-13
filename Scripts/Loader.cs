using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class Loader
{

    [System.Serializable]
    public class ChartsPack
    {

        public string name;
        public string composer;
        public string file;

        public string type;
        public string uuid;
        public string[] charts;
        public string[] resource;
    }

    [System.Serializable]
    public class Chart
    {
        public int formatVersion;
        public float offset;
        public int numOfNotes;
        public JudgeLine[] judgeLineList;
        public Chart()
        {
            Debug.Log("判定线加载成功");
        }

    }

    [System.Serializable]
    public class JudgeLine
    {
        public int numOfNotes;
        public int numOfNotesAbove;
        public int numOfNotesBelow;
        public float bpm;
        public SpeedEvent[] speedEvents;
        public JudgeLineMoveEvent[] judgeLineMoveEvents;
        public JudgeLineRotateEvent[] judgeLineRotateEvents;
        public JudgeLineDisappearEvent[] judgeLineDisappearEvents;

        private double[] position, rotation, alpha;
        public double[] GetPosition(int pTime)
        {
            // 获取当前时间的坐标数组
            double[] r = { 0.0, 0.0 };
            {
                foreach (JudgeLineMoveEvent moveEvent in this.judgeLineMoveEvents)
                {
                    if (moveEvent.startTime <= pTime & pTime <= moveEvent.endTime)
                    {
                        float t = (pTime - moveEvent.startTime) / (moveEvent.endTime - moveEvent.startTime);
                        r[0] = moveEvent.start + (moveEvent.end - moveEvent.start) * t;
                        r[1] = moveEvent.start2 + (moveEvent.end2 - moveEvent.start2) * t;
                        break;
                    }
                }
                return r;
            }
        }
        public double GetRotation(int pTime)
        {
            // 获取当前时间的角度R
            double r = 0.0;
            {
                foreach (JudgeLineRotateEvent rotateEvent in this.judgeLineRotateEvents)
                {
                    if (rotateEvent.startTime <= pTime & pTime <= rotateEvent.endTime)
                    {
                        float t = (pTime - rotateEvent.startTime) / (rotateEvent.endTime - rotateEvent.startTime);
                        r = rotateEvent.start + (rotateEvent.end - rotateEvent.start) * t;
                        break;
                    }
                }
                return r;
            }
        }
        public double GetAlpha(int pTime)
        {
            // 获取当前时间的透明度0-1
            double r = 0.0;
            {
                foreach (JudgeLineDisappearEvent disappearEvent in this.judgeLineDisappearEvents)
                {
                    if (disappearEvent.startTime <= pTime & pTime <= disappearEvent.endTime)
                    {
                        float t = (pTime - disappearEvent.startTime) / (disappearEvent.endTime - disappearEvent.startTime);
                        r = disappearEvent.start + (disappearEvent.end - disappearEvent.start) * t;
                        break;
                    }
                }
                return r;
            }
        }
    }

    [System.Serializable]
    public class Note
    {
        public int type;
        public int time;
        public float positionX;
        public float holdTime;
        public double speed;
        public double floorPosition;
    }

    [System.Serializable]
    public class SpeedEvent
    {
        public float startTIme;
        public float endTime;
        public double floorPosition;
        public double value;
    }

    [System.Serializable]
    public class BaseEvent
    {
        public float startTime;
        public float endTime;
        public double start;
        public double end;
        public double start2;
        public double end2;

    }

    [System.Serializable]
    public class JudgeLineMoveEvent : BaseEvent
    {
        public JudgeLineMoveEvent(float startTime, float endTime, double start, double end, double start2, double end2)
        {
            base.startTime = startTime;
            base.endTime = endTime;
            base.start = start;
            base.end = end;
            base.start2 = start2;
            base.end2 = end2;
        }
    }

    [System.Serializable]
    public class JudgeLineRotateEvent : BaseEvent
    {
        public JudgeLineRotateEvent(float startTime, float endTime, double start, double end, double start2, double end2)
        {
            base.startTime = startTime;
            base.endTime = endTime;
            base.start = start;
            base.end = end;
            base.start2 = start2;
            base.end2 = end2;
        }
    }

    [System.Serializable]
    public class JudgeLineDisappearEvent : BaseEvent
    {
        public JudgeLineDisappearEvent(float startTime, float endTime, double start, double end, double start2, double end2)
        {
            base.startTime = startTime;
            base.endTime = endTime;
            base.start = start;
            base.end = end;
            base.start2 = start2;
            base.end2 = end2;
        }
    }
}
