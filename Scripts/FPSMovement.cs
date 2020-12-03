using Photon.Pun;
using UnityEngine;


public class FPSMovement : MonoBehaviour, IPunObservable
{
    PhotonView photonView;
    
    [Header("Movement")]
    [SerializeField] float speed;
    Rigidbody rb;
    [SerializeField] float sprintMultiplier = 1.5f;
    [SerializeField] Animator anim;

    [Header("Looking around")]
    [SerializeField] Transform cam;
    [SerializeField] float sensitivity;
    float headRotation = 0f;
    [SerializeField] float headRotationLimit = 90f;

    // [HideInInspector]
    //public bool isMaster = false;
    Vector3 syncPosition;
    Quaternion syncRotation;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        photonView.ObservedComponents.Add(this);

        if (!photonView.IsMine)
        {
            //print("not master");
            enabled = false;
            cam.gameObject.SetActive(false);
        }
        else
        {
            //print("master");
        }
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
          
            {//moving
               
                float x = Input.GetAxisRaw("Horizontal");
                float z = Input.GetAxisRaw("Vertical");
               // print(z);
                if (z > 0)
                {
                    anim.SetBool("Walk", true);
                }
                else
                {
                    anim.SetBool("Walk", false);

                }

                anim.SetFloat("Horizontal", x);

                //Vector3 moveBy = transform.right * x + transform.forward * z;

                //float actualSpeed = speed;
                
                //rb.MovePosition(transform.position + moveBy.normalized * actualSpeed * Time.deltaTime);
            }


            {//looking around
                Debug.Log("looking around");
                float x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
                float y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime * -1f;

               // transform.Rotate(0f, x, 0f);

                headRotation += y;
                headRotation = Mathf.Clamp(headRotation, -headRotationLimit, headRotationLimit);
                cam.localEulerAngles = new Vector3(headRotation, 0f, 0f);
            }
        }
        else
        {
            transform.position = syncPosition;
            transform.rotation = syncRotation;
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position); //position of the character
            stream.SendNext(transform.rotation); //rotation of the character
        }

        else
        {
            // Network player, receive data
             syncPosition = (Vector3)stream.ReceiveNext();
             syncRotation = (Quaternion)stream.ReceiveNext();
        }

    }

}
