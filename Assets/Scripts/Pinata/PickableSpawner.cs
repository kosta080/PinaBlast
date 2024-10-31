using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kosta
{
    public class PickableSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private GameObject _EnergyPrefab;
        [SerializeField] private GameObject _heartPrefab;
        [SerializeField] private GameObject _bombPrefab;

        private Dictionary<SpawnType, GameObject> _prefabsMap;

        private void Awake()
        {
            _prefabsMap = new Dictionary<SpawnType, GameObject>
            {
                { SpawnType.Coin, _coinPrefab },
                { SpawnType.Energy, _EnergyPrefab },
                { SpawnType.Heart, _heartPrefab },
                { SpawnType.Bomb, _bombPrefab }
            };
        }

        public enum SpawnType
        {
            Random, Energy, Coin, Heart, Bomb
        }

        public void SpawnPickable(SpawnType spawnType)
        {
            GameObject prefab = spawnType == SpawnType.Random ? GenerateRandomSpawn() : _prefabsMap[spawnType];
            var spawnedObject = Instantiate(prefab, transform.position, Quaternion.identity);
            spawnedObject.GetComponent<Rigidbody2D>().AddForce(GenerateRandomDirection(), ForceMode2D.Impulse);
        }

        private GameObject GenerateRandomSpawn()
        {
            float totalWeight = 0f;
            List<float> cumulativeWeights = new List<float>();
            int index = 1;
            foreach (var item in _prefabsMap)
            {
                float weight = 1f / index;
                totalWeight += weight;
                cumulativeWeights.Add(totalWeight);
                index++;
            }
            
            float randomValue = Random.Range(0f, totalWeight);
            
            int selectedIndex = 0;
            for (int i = 0; i < cumulativeWeights.Count; i++)
            {
                if (randomValue <= cumulativeWeights[i])
                {
                    selectedIndex = i;
                    break;
                }
            }
            
            SpawnType selectedType = (SpawnType)(selectedIndex + 1);
            return _prefabsMap[selectedType];
        }

        private Vector2 GenerateRandomDirection()
        {
            int randomIndex = Random.Range(0, 2);
            float randomForce = Random.Range(0f, 3.5f);
            return randomIndex == 0 ? Vector2.left * randomForce : Vector2.right * randomForce;
        }
    }
}