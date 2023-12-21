using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private Block _blockPrefab;
    private int _playWidth = 8;
    private int _rowsSpawned;
    private float _distanceBetweenBlocks = .7f;
    private List<Block> _blocksSpawned = new List<Block>();
    private List<Ball> _balls = new List<Ball>();
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnRowOfBlocks();
        }
    }

    internal void SpawnRowOfBlocks()
    {
        foreach (var block in _blocksSpawned)
        {
            if (block != null)
            {
                block.transform.position += Vector3.down * _distanceBetweenBlocks;
            }
        }
        for (int i = 0; i < _playWidth; i++)
        {
            if (UnityEngine.Random.Range(0, 100) <= 30)
            {
                var block = Instantiate(_blockPrefab, GetPosition(i), Quaternion.identity);
                int hits = UnityEngine.Random.Range(1, 3) + _rowsSpawned;

                block.SetHits(hits);
                
                _blocksSpawned.Add(block);
            }
        }

        _rowsSpawned++;
    }

    private Vector3 GetPosition(int i)
    {
        Vector3 pos = transform.position;
        pos += Vector3.right * i * _distanceBetweenBlocks;
        return pos;
    }
}

