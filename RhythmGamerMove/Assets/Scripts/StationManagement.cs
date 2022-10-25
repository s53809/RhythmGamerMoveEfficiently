using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeUnit
{
    public Int32 hour { get; private set; }
    public Int32 minute { get; private set; }
    public Int32 second { get; private set; }
    public void PlusTime(Int32 second, Int32 minute = 0, Int32 hour = 0)
    {
        if(this.second + second >= 60)
        {
            this.second = (this.second + second) % 60;
            this.minute += 1;
        }
        if(this.minute + minute >= 60)
        {
            this.minute = (this.minute + minute) % 60;
            this.hour += 1;
        }
        this.hour += hour;
    }

    public timeUnit(Int32 second, Int32 minute = 0, Int32 hour = 0)
    {
        this.second = second;
        this.minute = minute;
        this.hour = hour;
    }

    #region 비교 연산자 오버로딩
    public static Boolean operator > (timeUnit a, timeUnit b)
    {
        if(a.hour != b.hour)
        {
            return (a.hour > b.hour);
        }
        else if(a.minute != b.minute)
        {
            return (a.minute > b.minute);
        }
        else
        {
            return (a.second > b.second);
        }
    }
    public static Boolean operator < (timeUnit a, timeUnit b)
    {
        if (a.hour != b.hour)
        {
            return (a.hour < b.hour);
        }
        else if (a.minute != b.minute)
        {
            return (a.minute < b.minute);
        }
        else
        {
            return (a.second < b.second);
        }
    }

    public static Boolean operator >= (timeUnit a, timeUnit b)
    {
        if (a.hour != b.hour)
        {
            return (a.hour >= b.hour);
        }
        else if (a.minute != b.minute)
        {
            return (a.minute >= b.minute);
        }
        else
        {
            return (a.second >= b.second);
        }
    }
    public static Boolean operator <= (timeUnit a, timeUnit b)
    {
        if (a.hour != b.hour)
        {
            return (a.hour <= b.hour);
        }
        else if (a.minute != b.minute)
        {
            return (a.minute <= b.minute);
        }
        else
        {
            return (a.second <= b.second);
        }
    }

    public static Boolean operator == (timeUnit a, timeUnit b)
    {
        if(a.hour == b.hour && a.minute == b.minute && a.second == b.second)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static Boolean operator !=(timeUnit a, timeUnit b)
    {
        if (a.hour == b.hour && a.minute == b.minute && a.second == b.second)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion
}

public class Station
{
    public String name;
    public List<Int32> line { get; private set; }
    public List<Station> adjNode { get; private set; }
    public List<Tuple<Int32, Int32>> transferList { get; private set; }
    public List<timeUnit> transferTime { get; private set; }
    public Boolean haveArcadeCeneter { get; private set; }
    public ArcadeCeneter arcadeCenter { get; private set; }

    public Station(String name)
    {
        this.name = name;
        line = new List<Int32>();
        adjNode = new List<Station>();
        transferList = new List<Tuple<Int32,Int32>>();
        transferTime = new List<timeUnit>();
        haveArcadeCeneter = false;
    }

    public void AddLine(Int32 N)
    {
        line.Add(N);
    }

    public void AddAdjNode(Station sta)
    {
        adjNode.Add(sta);
    }

    public void AddTransfer(Int32 startLine, Int32 endLine, timeUnit time)
    {
        Tuple<Int32, Int32> x = new Tuple<Int32, Int32>(startLine,endLine);

        this.transferList.Add(x);
        this.transferTime.Add(time);
    }

    public void AddArcade(ArcadeCeneter center)
    {
        if (!haveArcadeCeneter) { haveArcadeCeneter = true; }
        arcadeCenter = center;
    }
}

public class Node
{
    public Station curNode { get; private set; }
    public Station prevNode { get; private set; }
    public timeUnit minTime { get; private set; }

    public Node(Station thisNode)
    {
        curNode = thisNode;
        prevNode = null;
        minTime = null;
    }

    public Boolean isMinTime(timeUnit minTime, Station compareNode)
    {
        if(this.minTime > minTime)
        {
            this.minTime = minTime;
            prevNode = compareNode;
            return true;
        }
        else
        {
            return false;
        }
    }
}
public class StationManagement : MonoBehaviour
{
    private static StationManagement instance;
    public static StationManagement Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                instance = new StationManagement();
                return instance;
            }
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private Dictionary<String, Station> dic = new Dictionary<String, Station>();
    
    public Station AddStation(String key)
    {
        if (!dic.ContainsKey(key))
        {
            dic.Add(key, new Station(key));
            return dic[key];
        }
        else
        {
            return null;
        }
    }

    public Station getStation(String key)
    {
        if (!dic.ContainsKey(key))
        {
            Debug.Log("존재하지 않은 역입니다 : " + key);
            return null;
        }
        return dic[key];
    }
}
