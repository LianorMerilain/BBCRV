using System.Collections; //всЄ сделано в одном скрипте, так как € не гимнаст, мне не нужен гибкий код, мне нужен рабочий код.                 
using System.Collections.Generic; //“ак как € почти англичанин, в моих коментари€х можно заметить немного английского вайба.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class AllCode : MonoBehaviour
{
    //Volume
    public AudioClip Volume;
    public AudioSource Audio; //чЄт западает, в godbox нужно Audio каждый раз выбирать, при загрузке сцены, это своего рода фича
    //Not UI
    private readonly List<Vector3> _chestPositionPoint = new();
    private LineRenderer _lineRenderer;
    private float _speed;
    private bool _isMoving = false;
    public GameObject Player;
    //UI

    public Text ScoreText;
    public Scrollbar SdeedScrollbar;
    private void Start() //махинации with lines po большей части
    {
        Audio = GetComponent<AudioSource>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.endWidth = 0.5f;
        _lineRenderer.startWidth = 0.5f;
        _lineRenderer.sortingOrder = 0;
    }
    void Update()
    {

        UI();
        Line(_chestPositionPoint);
        if (_isMoving)//Ћогика dvijenie персонажа 
        {
            if ((Vector2)Player.transform.position == (Vector2)_chestPositionPoint[0])
            {
                _chestPositionPoint.RemoveAt(0);
                _isMoving = false;
            }
            else
            {
                Player.transform.position = Vector2.MoveTowards(Player.transform.position, _chestPositionPoint[0], _speed * Time.deltaTime);
            }
        }
        else
        {
            if (_chestPositionPoint.Count > 0)
            { 
                _isMoving = true;
            }
        }
            if (Input.GetMouseButtonDown(1))//Creature point po click mouse(not life)
            {
            PointCreator();
            }
    }
    public void PointCreator()//one смысловой блок
    {
     Vector3 PositionPoint = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition); 
     _chestPositionPoint.Add(PositionPoint);   //Zanosim value в list           
        Audio.PlayOneShot(Volume);
    }
    private void Line(List<Vector3> list)//two смысловой блок (Ќастраиваем logic ways к point)
    {
        _lineRenderer.positionCount = list.Count+1; 
        list.Insert(0, (Vector2)Player.transform.position);
        _lineRenderer.SetPositions(list.ToArray());
        list.RemoveAt(0);
    }
    public void UI()//three смысловой блок (module UI(старалс€))
    {
        ScoreText.text = "active click: " + _chestPositionPoint.Count;
        _speed = SdeedScrollbar.value*10;
    }
}
