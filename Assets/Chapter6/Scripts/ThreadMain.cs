using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class ThreadMain
{
    public static ThreadMain instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ThreadMain();
            }
            return _instance;
        }
    }
    static ThreadMain _instance;

    public Chapter6MulThread main;

    System.Random random = new System.Random();

    System.Action finishCallBack;
    List<Task> taskList = new List<Task>();
    List<Thread> threadList = new List<Thread>();
    int threadNum;
    public Color[,] pic = new Color[1000, 500];

    public void AddTask(int x, int y)
    {
        taskList.Add(new Task(() =>
        {
            Color c = new Color();
            for (int s = 0; s < main.AntialiasLevel; s++)
            {
                float u = (x + (float)random.NextDouble()) / Screen.width;
                float v = (y + (float)random.NextDouble()) / Screen.height;
                Ray r = main.cam.GetRay(u, v);
                c += main.color(r);
            }

            c /= main.AntialiasLevel;
            pic[x, y] = c;
        }));
    }

    public void Run(System.Action finishCallBack)
    {
        for(int i=0;i<taskList.Count;i++)
        {
            taskList[i].Start();
        }
        Task.WhenAll(taskList.ToArray()).ContinueWith((Task t)=> { finishCallBack(); });
    }
}
