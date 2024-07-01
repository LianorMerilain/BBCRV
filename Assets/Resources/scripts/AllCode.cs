using System.Collections; //всё сделано в одном скрипте, так как я не гимнаст, мне не нужен гибкий код, мне нужен рабочий код.                 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class AllCode : MonoBehaviour
{
    public AudioClip Volume; //Volume
    private AudioSource _audio;

    public Vector3[] PositionsArray;
    private readonly Queue _chestPositionPoint = new();
    private LineRenderer _lineRenderer;
    private float _speed;
    private bool _isMoving = false;
    public GameObject Player;
    public Text ScoreText; //UI
    public Scrollbar SdeedScrollbar;
    private void Start() 
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.endWidth = 0.5f;
        _lineRenderer.startWidth = 0.5f;
        _lineRenderer.sortingOrder = 0;
    }
    void Update()
    {
        Queue(_chestPositionPoint);
        UI();
        //Queue(_chestPositionPoint);
        /*if (_isMoving)
        {
            if ((Vector2)Player.transform.position == (Vector2)PositionsArray[0])
            {
            
                _isMoving = false;
                Debug.Log("0");
            }
            else
            {
                Player.transform.position = Vector2.MoveTowards(Player.transform.position, PositionsArray[0], _speed * Time.deltaTime);
                Debug.Log("1");
            }
        }
        else
        {
            if (_chestPositionPoint.Count > 0)
            { 
                _isMoving = true;
                Debug.Log("2");
            }
        }*/
        if (_isMoving)
        {
            PositionsArray = new Vector3[_chestPositionPoint.Count];
            bool ArePositionsClose(Vector2 pos1, Vector2 pos2, float tolerance = 0.01f)
            {
                return Vector2.Distance(pos1, pos2) < tolerance;
            }
            if (ArePositionsClose((Vector2)Player.transform.position, (Vector2)PositionsArray[0]))
            {
                Debug.Log("0");
                _chestPositionPoint.Dequeue();
                _isMoving = false;
            }
            else
            {
                Player.transform.position = Vector2.MoveTowards((Vector2)Player.transform.position, PositionsArray[0], _speed * Time.deltaTime);
                Debug.Log("1");
            }
        }
        else
        {
            if (_chestPositionPoint.Count > 0) _isMoving = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            PointCreator();
        }
    }
    public void PointCreator()
    {
        Vector3 PositionPoint = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _chestPositionPoint.Enqueue(PositionPoint);
        //_audio.PlayOneShot(Volume);
    }
    public void Queue(Queue queue)
    {
        _lineRenderer.positionCount = queue.Count + 1;
        queue.Enqueue(Player.transform.position); //list.Insert(0, (Vector2)Player.transform.position); ERROR
        _lineRenderer.SetPositions(PositionsArray); // _lineRenderer.SetPositions(list.ToArray());
        queue.Dequeue();

    }
    public void UI()
    {
        ScoreText.text = "active click: " + _chestPositionPoint.Count;
        _speed = SdeedScrollbar.value * 10;
    }
}
