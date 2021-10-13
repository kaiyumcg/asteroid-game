using GameplayFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidGame.Manager
{
    [System.Serializable]
    public class PooledItem
    {
        [SerializeField] Transform prefab;
        List<PooledItemState> states;
        internal Transform Prefab { get { return prefab; } }
        internal List<PooledItemState> States { get { return states; } }
    }

    public class PooledItemState
    {
        Transform clonedItem;
        bool isItFree;
        internal Transform ClonedItem { get { return clonedItem; } set { clonedItem = value; } }
        internal bool IsItFree { get { return isItFree; } set { isItFree = value; } }
    }

    public class PoolManager : GameSystem
    {
        [SerializeField] List<PooledItem> poolStore = new List<PooledItem>();

        

        public Transform Clone(Transform prefab)
        {
            throw new System.NotImplementedException();
        }

        public void Free(Transform sceneObject)
        {
            throw new System.NotImplementedException();
        }

        protected override void InitSystem()
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerator InitSystemAsync()
        {
            throw new System.NotImplementedException();
        }

        protected override void UpdateSystem()
        {
            throw new System.NotImplementedException();
        }
    }
}