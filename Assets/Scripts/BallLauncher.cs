using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    private Vector3 _startDragPosition;
    private Vector3 _endDragPosition;
    private LaunchPreview _launchPreview;
    [SerializeField] private Ball _ballPrefab;
    private int ballsReady;
    private List<Ball> _balls = new List<Ball>();
    private BlockSpawner _blockSpawner;

    void Awake()
    {
        _blockSpawner = FindObjectOfType<BlockSpawner>();
        _launchPreview = GetComponent<LaunchPreview>();
        CreateBall();
    }
    void Update()
    {
        Vector3 _worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10;
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag(_worldPosition);
        }
        else if (Input.GetMouseButton(0))
        {
            ContinueDrag(_worldPosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrag();        
        }
    }

    private void StartDrag(Vector3 worldPosition)
    {
        _startDragPosition = worldPosition;
        _launchPreview.SetStartPoint(transform.position);
    }

    private void ContinueDrag(Vector3 worldPosition)
    {
        _endDragPosition = worldPosition;
        Vector3 direction = _endDragPosition - _startDragPosition;
        _launchPreview.SetEndPoint(transform.position - direction);
    }

    private void EndDrag()
    {
        StartCoroutine(LaunchBalls());
       
    }

    private void CreateBall()
    {
        var ball = Instantiate(_ballPrefab);
        _balls.Add(ball);
        ballsReady++;
    }

    private IEnumerator LaunchBalls()
    {
        Vector3 direction = _endDragPosition - _startDragPosition;
        direction.Normalize();
        List<Ball> ballsCopy = new List<Ball>(_balls);
        foreach (var ball in ballsCopy)
        {
            ball.transform.position = transform.position;
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(-direction);

            yield return new WaitForSeconds(.1f);
        }

        ballsReady = 0;
    }

    internal void ReturnBall()
    {
        ballsReady++;
        if (ballsReady == _balls.Count)
        {
            _blockSpawner.SpawnRowOfBlocks();
            CreateBall();
        }
    }
}
