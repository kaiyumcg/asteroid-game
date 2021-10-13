using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Md. Al Kaiyum(Rumman)
/// Email: kaiyumce06rumman@gmail.com
/// Game Manager to controll the game systems
/// </summary>
namespace GameplayFramework
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] List<GameSystem> allSystems = new List<GameSystem>();
        [SerializeField] bool asyncWaitForInit = false;

        void ReloadSysData()
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

            allSystems.RemoveAll((sys) => { return sys == null; });
            if (allSystems == null) { allSystems = new List<GameSystem>(); }
        }

        private void OnValidate()
        {
            ReloadSysData();
        }

        // Start is called before the first frame update
        IEnumerator Start()
        {
            ReloadSysData();
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