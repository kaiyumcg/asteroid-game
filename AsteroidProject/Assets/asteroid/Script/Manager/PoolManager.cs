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
        internal Transform Prefab { get { return prefab; }  set { prefab = value; } } 
        internal List<PooledItemState> States { get { return states; } set { states = value; } }

        internal PooledItemState GetItem(Transform tr)
        {
            PooledItemState pState = null;
            if (states != null && states.Count > 0)
            {
                for (int i = 0; i < states.Count; i++)
                {
                    var st = states[i];
                    if (st.ClonedItem == tr)
                    {
                        pState = st;
                        break;
                    }
                }
            }
            return pState;
        }

        internal PooledItemState GetFreeItem()
        {
            PooledItemState pState = null;
            if (states != null && states.Count > 0)
            {
                for (int i = 0; i < states.Count; i++)
                {
                    var st = states[i];
                    if (st.IsItFree)
                    {
                        pState = st;
                        break;
                    }
                }
            }
            return pState;
        }
    }

    public class PooledItemState
    {
        Transform clonedItem;
        GameObject clonedItemObject;
        internal Transform ClonedItem { get { return clonedItem; } set { clonedItem = value; } }
        internal GameObject ClonedItemObject { get { return clonedItemObject; } set { clonedItemObject = value; } }
        internal bool IsItFree { get { return clonedItemObject.activeInHierarchy == false; } }
        internal void MakeFree()
        {
            clonedItemObject.SetActive(true);
        }
    }

    public class PoolManager : GameSystem
    {
        [SerializeField] List<PooledItem> poolStore = new List<PooledItem>();
        PooledItem GetPooledItem(Transform prefab)
        {
            PooledItem pooledItem = null;
            if (poolStore != null && poolStore.Count > 0)
            {
                for (int i = 0; i < poolStore.Count; i++)
                {
                    var item = poolStore[i];
                    if (item == null) { continue; }
                    if (item.Prefab == prefab)
                    {
                        pooledItem = item;
                        break;
                    }
                }
            }
            return pooledItem;
        }

        PooledItemState CreateAndUpdatePool(Transform prefab, out Transform clonedTransform, out GameObject clonedGameObject)
        {
            clonedTransform = Instantiate(prefab) as Transform;
            clonedGameObject = clonedTransform.gameObject;
            clonedGameObject.SetActive(true);

            var state = new PooledItemState { ClonedItem = clonedTransform, ClonedItemObject = clonedGameObject };
            if (poolStore != null && poolStore.Count > 0)
            {
                for (int i = 0; i < poolStore.Count; i++)
                {
                    var it = poolStore[i];
                    if (it == null) { continue; }
                    if (it.Prefab == prefab)
                    {
                        if (poolStore[i].States == null) { poolStore[i].States = new List<PooledItemState>(); }
                        poolStore[i].States.Add(state);
                        break;
                    }
                }
            }
            return state;
        }

        public Transform Clone(Transform prefab, out GameObject clonedObject)
        {
            Transform result = null;
            clonedObject = null;

            PooledItem poolItem = GetPooledItem(prefab);

            if (poolItem == null)
            {
                //our prefab is not included in the list. Make a data and add it
                var pItem = new PooledItem { Prefab = prefab, States = new List<PooledItemState>() };
                if (poolStore == null || poolStore.Count == 0) { poolStore = new List<PooledItem>(); }
                poolStore.Add(pItem);
                CreateAndUpdatePool(prefab, out result, out clonedObject);
            }
            else
            {
                PooledItemState state = poolItem.GetFreeItem();
                if (state == null)
                {
                    //no free has been found
                    CreateAndUpdatePool(prefab, out result, out clonedObject);
                }
                else
                {
                    //free has been found!
                    result = state.ClonedItem;
                    clonedObject = state.ClonedItemObject;
                    clonedObject.SetActive(true);
                }
            }
            return result;
        }

        public void Free(Transform sceneObject)
        {
            if (poolStore != null && poolStore.Count > 0)
            {
                for (int i = 0; i < poolStore.Count; i++)
                {
                    var item = poolStore[i];
                    if (item == null) { continue; }
                    var stateItem = item.GetItem(sceneObject);
                    if (stateItem == null) { continue; }
                    if (stateItem.IsItFree == false)
                    {
                        stateItem.MakeFree();
                        break;
                    }
                }
            }
        }
    }
}