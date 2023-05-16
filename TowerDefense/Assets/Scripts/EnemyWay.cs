using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWay : MonoBehaviour
{
    [SerializeField] private Vector3[] pointList;

    public Vector3[] Points => pointList;
    public Vector3 CurrentPosition => _currentPosition;

    private Vector3 _currentPosition;
    private bool _gameStarted;

    private void Start()
    {
        _gameStarted = true;
        _currentPosition = transform.position;
    }

    public Vector3 GetWaipointPosition(int index)
    {
        return CurrentPosition + Points[index];
    }

    private void OnDrawGizmos()
    {
        if(!_gameStarted && transform.hasChanged)
        {
            _currentPosition= transform.position;
        }
        for (int i = 0; i < pointList.Length; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(pointList[i] + _currentPosition, 0.5f);

            if(i< pointList.Length - 1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(pointList[i] +_currentPosition, pointList[i+1] + _currentPosition);
            }
        }
    }
}
