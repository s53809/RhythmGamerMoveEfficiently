using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dijkstra
{
    public static Station goalStation;
    public static ArcadeCeneter goalCenter;
    public static String strStation;
    public static void TamsekStart(String startStation)
    {
        strStation = startStation;
        goalCenter = null;
        goalStation = null;
        foreach (KeyValuePair<String, Station> kvp in StationManagement.Instance.dic) // 초기화
        {
            kvp.Value.prevNode = null;
            kvp.Value.minTime = new TimeUnit(0);
        }

        Queue<Station> qu = new Queue<Station>();
        qu.Enqueue(StationManagement.Instance.getStation(startStation));
        StationManagement.Instance.getStation(startStation).minTime = new TimeUnit(0);
        Int32 ii = 0;
        while (qu.Count != 0)
        {
            ii++;
            if(ii >= 1000)
            {
                Debug.Log("무한루프");
                break;
            }
            Station temp = qu.Dequeue();
            for (Int32 i = 0; i < temp.adjNode.Count; i++)
            {
                if(temp.adjNode[i].minTime == new TimeUnit(0, 0, 0) || temp.adjNode[i].minTime > temp.minTime + new TimeUnit(0, 2))
                {
                    temp.adjNode[i].prevNode = temp;
                    temp.adjNode[i].minTime = temp.minTime + new TimeUnit(0, 2);
                    //Debug.Log(temp.adjNode[i].name + " " + temp.adjNode[i].minTime.hour + " " + temp.adjNode[i].minTime.minute);
                    qu.Enqueue(temp.adjNode[i]);
                }
            }
        }
    }

    public static void SearchArcade(List<KindOfOrak> arcadeList)
    {
        TimeUnit minTime = new TimeUnit(0);
        Boolean findCenter = false;
        foreach(KeyValuePair<String, ArcadeCeneter> kvp in OrakManagement.Instance.dic)
        {
            Boolean isCorrect = true;
            for(Int32 i = 0; i < arcadeList.Count; i++)
            {
                if(kvp.Value.orakList[(Int32)arcadeList[i]] == 0)
                {
                    isCorrect = false;
                    break;
                }
            }
            if (isCorrect)
            {
                if (!findCenter)
                {
                    findCenter = true;
                    minTime = kvp.Value.where.minTime;
                    goalStation = kvp.Value.where;
                    goalCenter = kvp.Value;
                }
                else if(minTime > kvp.Value.where.minTime)
                {
                    minTime = kvp.Value.where.minTime;
                    goalStation = kvp.Value.where;
                    goalCenter = kvp.Value;
                }
            }
        }
    }

    public static List<Station> ViewPath()
    {
        List<Station> temp = new List<Station>();
        Station curStation = goalStation;
        Int32 ii = 0;
        while(curStation != StationManagement.Instance.getStation(strStation))
        {
            ii++;
            if(ii > 1000)
            {
                Debug.Log("무한루프");
                break;
            }
            temp.Add(curStation);
            curStation = curStation.prevNode;
        }
        temp.Add(curStation);
        return temp;
    }
}
