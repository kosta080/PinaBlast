using System.Collections;
using System.Collections.Generic;
using Kosta.Infra;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Kosta.Pinata
{
    public class PickableSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private GameObject _EnergyPrefab;
        [SerializeField] private GameObject _bombPrefab;

        private Dictionary<SpawnType, GameObject> _prefabsMap;
        
        private int _coinSpawnCount = 0;
        private const int CoinSpawnLimit = 5;
        EventManager _eventManager;

        private void Awake()
        {
            _prefabsMap = new Dictionary<SpawnType, GameObject>
            {
                { SpawnType.Coin, _coinPrefab },
                { SpawnType.Energy, _EnergyPrefab },
            };
        }
        
        
        private void Start()
        {
            _eventManager = ServiceLocator.Resolve<EventManager>();
            _eventManager.OnPinataExploded += OnPinataExploded;
        }

        private void OnPinataExploded()
        {
            StartCoroutine(StartFinalSpawnRain());
        }

        public enum SpawnType
        {
            Random, Energy, Coin
        }

        public void SpawnPickable(SpawnType spawnType)
        {
            GameObject prefab = spawnType == SpawnType.Random ? GenerateRandomSpawn() : _prefabsMap[spawnType];
            var spawnedObject = Instantiate(prefab, transform.position, Quaternion.identity);
            spawnedObject.GetComponent<Rigidbody2D>().AddForce(GenerateRandomDirection(), ForceMode2D.Impulse);
        }
        
        // spawning 5 Coins and then spawning Energy with 50% chance otherwise spawning Coin.
        private GameObject GenerateRandomSpawn()
        {
            if (_coinSpawnCount < CoinSpawnLimit)
            {
                _coinSpawnCount++;
                return _prefabsMap[SpawnType.Coin];
            }
            _coinSpawnCount = 0;

            bool spawnEnergy = Random.value < 0.5;
            if (spawnEnergy)
            {
                return _prefabsMap[SpawnType.Energy];
            }
            return _prefabsMap[SpawnType.Coin];
        }

        private Vector2 GenerateRandomDirection()
        {
            int randomIndex = Random.Range(0, 2);
            float randomForce = Random.Range(0f, 3.5f);
            return randomIndex == 0 ? Vector2.left * randomForce : Vector2.right * randomForce;
        }

        private IEnumerator StartFinalSpawnRain()
        {
            float finalSpawnTime = 0;
            float finalSpawnMax = 5;
            
            while (finalSpawnTime < finalSpawnMax)
            {
                SpawnPickable(SpawnType.Coin);
                yield return new WaitForSeconds(0.1f);
                finalSpawnTime += 0.1f;
            }
            _eventManager.OnFinalSpawnFinished?.Invoke();
        }
    }
}