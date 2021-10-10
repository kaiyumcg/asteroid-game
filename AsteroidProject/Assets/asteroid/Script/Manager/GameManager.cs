using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidGame.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] List<GameSystem> allSystems = new List<GameSystem>();
        [SerializeField] bool asyncWaitForInit = false;

        // Start is called before the first frame update
        IEnumerator Start()
        {
            if (allSystems == null) { allSystems = new List<GameSystem>(); }
            var allsys = FindObjectsOfType<GameSystem>();
            if (allsys != null && allsys.Length > 0)
            {
                for (int i = 0; i < allsys.Length; i++)
                {
                    var sys = allsys[i];
                    if (sys == null) { continue; }
                    if (allSystems.Contains(sys) == false)
                    {
                        allSystems.Add(sys);
                    }
                }
            }

            if (allSystems.Count > 0)
            {
                for (int i = 0; i < allSystems.Count; i++)
                {
                    var sys = allSystems[i];
                    if (sys == null)
                    {
                        throw new System.Exception("By architectural design we do not allow any null system(per frame UnityEngine.Object ==, != check) " +
                            "in the list for performance implication. Please assign valid system(s) in the list!");
                    }
                    sys.InitSystem();
                    if (asyncWaitForInit)
                    {
                        yield return StartCoroutine(sys.InitSystemAsync());
                    }
                    else
                    {
                        StartCoroutine(sys.InitSystemAsync());
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < allSystems.Count; i++)
            {
                var sys = allSystems[i];
                sys.UpdateSystem();
            }
        }
    }
}