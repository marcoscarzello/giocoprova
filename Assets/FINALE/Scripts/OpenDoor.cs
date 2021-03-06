using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    [SerializeField] private float _velocity = 2f;
    [SerializeField] private float _end = 8f;
    private BoxCollider colliderPorta;
    private AudioSource audios;

    private bool _open = false;
    private bool flag = true;


    private void Start()
    {
        audios = GetComponent<AudioSource>();
        colliderPorta = GetComponent<BoxCollider>();
    }


    public void OpenDoorNumber(int x)
    {
        Debug.Log("Sono il client. Apro porta " + x);

        string opening_door = $"door {x}";

        if (this.name == opening_door)
            _open = true;
    }


    void Update()
    {

        if (_open)
        {
            
            var _endPos = new Vector3(
                        transform.position.x,
                        _end,
                        transform.position.z);
            if (flag)
            {
                audios.Play();
                flag = false;
            }

            if (transform.position == _endPos)
                _open = false;


            transform.position = Vector3.MoveTowards(transform.position, _endPos, _velocity * Time.deltaTime);
            colliderPorta.enabled = false;
        }


    }

}
