using GameplayFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidGame.Manager
{
    public class TaskManager : GameSystem
    {
        protected override void InitSystem()
        {
            //throw new System.NotImplementedException();
        }

        protected override IEnumerator InitSystemAsync()
        {
            //throw new System.NotImplementedException();
            yield return null;
        }

        protected override void UpdateSystem()
        {
            //throw new System.NotImplementedException();
        }

        IEnumerator DelayedCallCOR(float delay, System.Action OnComplete)
        {
            yield return new WaitForSeconds(delay);
            OnComplete?.Invoke();
        }

        public void DelayedCall(float delay, System.Action OnComplete)
        {
            StartCoroutine(DelayedCallCOR(delay, OnComplete));
        }

        public Coroutine StartTask(IEnumerator task, System.Action OnComplete = null)
        {
            return StartCoroutine(TaskRunner(task, OnComplete));
        }

        IEnumerator TaskRunner(IEnumerator task, System.Action OnComplete)
        {
            yield return StartCoroutine(task);
            OnComplete?.Invoke();
        }
    }
}