using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Author: Md. Al Kaiyum(Rumman)
/// Email: kaiyumce06rumman@gmail.com
/// Placeable actor class that has some commonly used funtionalities
/// </summary>
namespace GameplayFramework
{
    public abstract class GameActor : MonoBehaviour
    {
        protected virtual void StartOrSpawnActor() 
        { 
            life = initialLife; 
            isDead = false;
            OnStartOrSpawn?.Invoke();
        }

        protected virtual void AwakeActor() { }
        protected abstract void UpdateActor(float dt, float fixedDt);
        protected abstract void UpdateActorPhysics(float dt, float fixedDt);
        protected virtual void OnEditorUpdate() { }
        public Transform _Transform { get { return _transform; } }
        public GameObject _GameObject { get { return _gameObject; } }

        [SerializeField] List<GameplayComponent> gameplayComponents;
        bool componentListDirty = false;
        Transform _transform;
        GameObject _gameObject;
        [SerializeField] float life = 100f;
        [SerializeField] bool isDead = false;
        [SerializeField] float timeScale = 1.0f;
        float initialLife;

        public UnityEvent OnStartOrSpawn, OnDeath;
        public UnityEvent<float> OnDamage, OnGainHealth;
        public float FullLife { get { return initialLife; } }
        public float CurrentLife { get { return life; } }
        public float NormalizedLifeValue { get { return life / initialLife; } }
        public bool IsDead { get { return isDead; } }
        public float TimeScale { get { return timeScale; } set { timeScale = value; } }

        public T GetGameplayComponent<T>() where T : GameplayComponent
        {
            T result = null;
            if (gameplayComponents != null && gameplayComponents.Count > 0)
            {
                for (int i = 0; i < gameplayComponents.Count; i++)
                {
                    var gcom = gameplayComponents[i];
                    if (gcom.GetType() == typeof(T))
                    {
                        result = (T)gcom;
                        break;
                    }
                }
            }
            return result;
        }

        void ReloadComponents()
        {
            if (gameplayComponents == null) { gameplayComponents = new List<GameplayComponent>(); }
            var gComs = GetComponentsInChildren<GameplayComponent>();
            if (gComs != null && gComs.Length > 0)
            {
                for (int i = 0; i < gComs.Length; i++)
                {
                    var comp = gComs[i];
                    if (gameplayComponents.Contains(comp) == false)
                    {
                        gameplayComponents.Add(comp);
                    }
                }
            }
            gameplayComponents.RemoveAll((comp) => { return comp == null; });
            if (gameplayComponents == null) { gameplayComponents = new List<GameplayComponent>(); }
        }

        public void DoDamage(float damage)
        {
            var dm = Mathf.Abs(damage);
            if (isDead) { return; }
            this.life -= dm;
            OnDamage?.Invoke(dm);
            if (this.life <= 0f)
            {
                isDead = true;
                OnDeath?.Invoke();
                _gameObject.SetActive(false);
            }
        }

        public void AddLife(float life)
        {
            var lf = Mathf.Abs(life);
            if (isDead) { return; }
            this.life += lf;
            OnGainHealth?.Invoke(lf);
        }

        public void AddGameplayComponent<T>() where T : GameplayComponent
        {
            componentListDirty = true;
            var _dyn_obj = new GameObject("_Gen_" + typeof(T) + "_CreatedAtT_" + Time.time + "_Level_" +
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            _dyn_obj.transform.SetParent(_transform);
            _dyn_obj.transform.localPosition = Vector3.zero;
            _dyn_obj.transform.localRotation = Quaternion.identity;
            _dyn_obj.transform.localScale = Vector3.one;
            this.AddGameplayComponent<T>(_dyn_obj);
            componentListDirty = false;
        }

        public void AddGameplayComponent<T>(GameObject objOnWhichToAdd) where T : GameplayComponent
        {
            componentListDirty = true;
            var comp = objOnWhichToAdd.AddComponent<T>();
            if (gameplayComponents.Contains(comp) == false)
            {
                gameplayComponents.Add(comp);
            }
            componentListDirty = false;
        }

        private void OnValidate()
        {
            OnEditorUpdate();
        }

        private void Awake()
        {
            initialLife = life;
            _transform = transform;
            _gameObject = gameObject;
            ReloadComponents();
            AwakeActor();

            for (int i = 0; i < gameplayComponents.Count; i++)
            {
                gameplayComponents[i].AwakeComponent();
            }

            if (isDead)
            {
                OnDeath?.Invoke();
            }
        }

        private void OnEnable()
        {
            StartOrSpawnActor();
            for (int i = 0; i < gameplayComponents.Count; i++)
            {
                gameplayComponents[i].OnStartOrSpawnActor();
            }
        }

        void Update()
        {
            var dt = Time.deltaTime * timeScale;
            var fixedDt = Time.fixedDeltaTime;
            UpdateActor(dt, fixedDt);

            if (componentListDirty) { return; }
            for (int i = 0; i < gameplayComponents.Count; i++)
            {
                gameplayComponents[i].UpdateComponent(dt, fixedDt);
            }
        }

        private void FixedUpdate()
        {
            var dt = Time.deltaTime * timeScale;
            var fixedDt = Time.fixedDeltaTime;
            UpdateActorPhysics(dt, fixedDt);

            if (componentListDirty) { return; }
            for (int i = 0; i < gameplayComponents.Count; i++)
            {
                gameplayComponents[i].UpdateComponentPhysics(dt, fixedDt);
            }
        }
    }
}