using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kosta
{
    public class PickableSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private GameObject _heartPrefab;

        private Dictionary<SpawnType, GameObject> _prefabsMap;

        private void Awake()
        {
            _prefabsMap = new Dictionary<SpawnType, GameObject>
            {
                { SpawnType.Coin, _coinPrefab },
                { SpawnType.Heart, _heartPrefab }
            };
        }

        public enum SpawnType
        {
            Random, Coin, Heart
        }

        public void SpawnPickable(SpawnType spawnType)
        {
            GameObject prefab = spawnType == SpawnType.Random ? GenerateRandomSpawn() : _prefabsMap[spawnType];
            var spawnedObject = Instantiate(prefab, transform.position, Quaternion.identity);
            spawnedObject.GetComponent<Rigidbody2D>().AddForce(GenerateRandomDirection(), ForceMode2D.Impulse);
        }

        private GameObject GenerateRandomSpawn()
        {
            int randomIndex = Random.Range(0, _prefabsMap.Count);
            SpawnType randomSpawnType = (SpawnType)randomIndex+1;
            return _prefabsMap[randomSpawnType];
        }

        private Vector2 GenerateRandomDirection()
        {
            int randomIndex = Random.Range(0, 2);
            float randomForce = Random.Range(0f, 3.5f);
            return randomIndex == 0 ? Vector2.left * randomForce : Vector2.right * randomForce;
        }
    }
}