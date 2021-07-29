using ETModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TestAwait : MonoBehaviour
{
    int a = 0;
    // Start is called before the first frame update
    void Start()
    {

        OneThreadSynchronizationContext s = OneThreadSynchronizationContext.Instance;
        TestAsync();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   async void TestAsync()
    {
        Debug.LogError("Test1");
        await WaitTimeAsync(1 );
        Debug.LogError("Test2");
        await WaitTimeAsync(1);
        Debug.LogError("Test3");
    }


    private static Task WaitTimeAsync(int waitTime )
    {
        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
        Thread thread = new Thread(() => WaitTime(waitTime, tcs));
        thread.Start();
        return tcs.Task;
    }
    private static void WaitTime(int waitTime, TaskCompletionSource<bool>   tcs   )
    {
        Thread.Sleep(waitTime);

        // 将action扔回主线程执行
        OneThreadSynchronizationContext.Instance.Post((o) => tcs.SetResult(true), null);
    }
}
