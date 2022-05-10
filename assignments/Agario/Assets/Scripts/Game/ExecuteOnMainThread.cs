using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ExecuteOnMainThread : MonoBehaviour
    {
        //Singleton for executing on main thread
        public static ExecuteOnMainThread Instance;
        private Queue<Action> actions = new Queue<Action>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void ExecuteActionOnMainThread(Action action)
        {
            actions.Enqueue(action);
        }

        private void Update()
        {
            while (actions.Count > 0)
            {
                actions.Dequeue()?.Invoke();
            }
        }
    }
}
