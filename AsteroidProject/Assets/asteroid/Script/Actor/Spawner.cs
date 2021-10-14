using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using AsteroidGame.Manager;

namespace AsteroidGame.Actor
{
    public class Spawner : GameActor
    {
        [Header("Spawn Setting")]
        [SerializeField] bool spawnMoreWithTime = false;
        [SerializeField] float spawnIntervalMin = 1.2f, spawnIntervalMax = 3f;
        [SerializeField] float rateOfInterval = 1.2f;

        [Header("After spawn rigidbody setting")]
        [SerializeField] bool useForce = false;
        [SerializeField] ForceMode forceMode;
        [SerializeField] float preloadForceMin = 2f, preloadForceMax = 3f;
        [SerializeField] float preloadSpeedMin = 10f, preloadSpeedMax = 30f;
        float currentInterval = 0.0f, timer = 0.0f;
        [SerializeField] List<GameActor> prefabs;
        PoolManager poolMan;


        protected override void AwakeActor()
        {
            base.AwakeActor();
            poolMan = FindObjectOfType<PoolManager>();
            currentInterval = spawnMoreWithTime ? spawnIntervalMax : spawnIntervalMin;
        }

        protected override void UpdateActor(float dt, float fixedDt)
        {
            base.UpdateActor(dt, fixedDt);

            timer += dt;
            if (timer > currentInterval)
            {
                if (spawnMoreWithTime)
                {
                    currentInterval -= rateOfInterval * dt;
                }
                else
                {
                    currentInterval += rateOfInterval * dt;
                }
                currentInterval = Mathf.Clamp(currentInterval, spawnIntervalMin, spawnIntervalMax);
                timer = 0.0f;

                var id = UnityEngine.Random.Range(0, prefabs.Count);
                var prefab = prefabs[id];

                GameObject clonedObject = null;
                var clonedTr = poolMan.Clone(prefab.transform, out clonedObject);
                clonedTr.position = transform.position;
                clonedTr.rotation = Quaternion.LookRotation(transform.forward);
                clonedTr.localScale = prefab.transform.localScale;

                var rgd = clonedTr.GetComponent<Rigidbody>();
                if (useForce)
                {
                    rgd.AddForce(UnityEngine.Random.Range(preloadForceMin, preloadForceMax) * transform.forward, forceMode);
                }
                else
                {
                    rgd.velocity = UnityEngine.Random.Range(preloadSpeedMin, preloadSpeedMax) * transform.forward;
                }
            }
        }
    }
}