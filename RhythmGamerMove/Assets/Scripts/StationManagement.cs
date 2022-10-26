using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUnit
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
        else
        {
            this.second += second;
        }
        if(this.minute + minute >= 60)
        {
            this.minute = (this.minute + minute) % 60;
            this.hour += 1;
        }
        else
        {
            this.minute += minute;
        }
        this.hour += hour;
    }

    public TimeUnit(Int32 second, Int32 minute = 0, Int32 hour = 0)
    {
        this.second = second;
        this.minute = minute;
        this.hour = hour;
    }

    #region 연산자 오버로딩
    
    public static TimeUnit operator + (TimeUnit a, TimeUnit b)
    {
        TimeUnit temp = new TimeUnit(0, 0, 0);
        if(a.second + b.second >= 60)
        {
            temp.second = (a.second + b.second) % 60;
            temp.minute += 1;
        }
        else
        {
            temp.second = a.second + b.second;
        }
        if(temp.minute + a.minute + b.minute >= 60)
        {
            temp.minute = (temp.minute + a.minute + b.minute) % 60;
            temp.hour += 1;
        }
        else
        {
            temp.minute = temp.minute + a.minute + b.minute;
        }
        temp.hour = temp.hour + a.hour + b.hour;
        return temp;
    }
    public static Boolean operator > (TimeUnit a, TimeUnit b)
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
    public static Boolean operator < (TimeUnit a, TimeUnit b)
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

    public static Boolean operator >= (TimeUnit a, TimeUnit b)
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
    public static Boolean operator <= (TimeUnit a, TimeUnit b)
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

    public static Boolean operator == (TimeUnit a, TimeUnit b)
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
    public static Boolean operator !=(TimeUnit a, TimeUnit b)
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
    public List<TimeUnit> transferTime { get; private set; }
    public Boolean haveArcadeCeneter { get; private set; }
    public ArcadeCeneter arcadeCenter { get; private set; }

    public TimeUnit gotoArcade { get; private set; }

    //다익스트라 전용 변수들
    public Station prevNode;
    public TimeUnit minTime;

    public Station(String name)
    {
        this.name = name;
        line = new List<Int32>();
        adjNode = new List<Station>();
        transferList = new List<Tuple<Int32,Int32>>();
        transferTime = new List<TimeUnit>();
        haveArcadeCeneter = false;
    }

    public void AddLine(Int32 N)
    {
        foreach(Int32 s in line)
        {
            if(s == N) { return; }
        }
        line.Add(N);
    }

    public void AddAdjNode(Station sta)
    {
        adjNode.Add(sta);
    }

    public void AddTransfer(Int32 startLine, Int32 endLine, TimeUnit time)
    {
        Tuple<Int32, Int32> x = new Tuple<Int32, Int32>(startLine,endLine);

        this.transferList.Add(x);
        this.transferTime.Add(time);
    }

    public void AddArcade(ArcadeCeneter center, TimeUnit time)
    {
        if (!haveArcadeCeneter) { haveArcadeCeneter = true; }
        arcadeCenter = center;
        gotoArcade = time;
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
    public Dictionary<String, Station> dic { get; private set; }

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
        dic = new Dictionary<String, Station>();
    }

    
    public Station AddStation(String key)
    {
        if (!dic.ContainsKey(key))
        {
            dic.Add(key, new Station(key));
            return dic[key];
        }
        else
        {
            return dic[key];
        }
    }

    public Boolean AlreadyZonzae(String key)
    {
        if (dic.ContainsKey(key))
        {
            return true;
        }
        else
        {
            return false;
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
