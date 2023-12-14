
using System.Threading;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Caracter : MonoBehaviour
{
    public float Vitesse=0.5f;
    public float JumpForce;
    public float Horizontale;
    public float Vertical;
    public CharacterController Controller;
    public Vector3 Direction;
    public bool IsGrounded=>Controller.isGrounded;
    public float Gravity = 9.81f;
    public float VerticalSpeed;
    public GameObject Poisson;
    [Range(0, 10)] public float Force = 5f;
    public TextMeshProUGUI Stricking;
    public TextMeshProUGUI TimerText;
    public float TimerSec;
    public float TimerMin;
    public TextMeshProUGUI Fin;
   
    

    // Start is called before the first frame update
    void Start()
    {
      
       
        
    }

    // Update is called once per frame
    void Update()
    {

    
        TimerSec += Time.deltaTime;
        TimerMin =Mathf.Floor(TimerSec / 60);
        TimerSec =TimerSec% 60;

        if (TimerSec < 10)
        { 

            if (TimerMin < 10)
            {
                TimerText.text="0"+TimerMin.ToString() + " : 0" + Mathf.Round(TimerSec).ToString();
            }
            else
            {
                TimerText.text = TimerMin.ToString() + " : 0" + Mathf.Round(TimerSec).ToString();
            }
        }
        else
        {

        
        

            if (TimerMin < 10)
            {
                TimerText.text = "0" + TimerMin.ToString() + " : " + Mathf.Round(TimerSec).ToString();
            }
            else
            {
                TimerText.text = TimerMin.ToString() + " : " + Mathf.Round(TimerSec).ToString();
            }



        }
        /*if (TimerSec >= 59.9)*
        {
            TimerSec = 0;
            TimerMin++;

            if (TimerMin == 2)
            {
                FinishGame();  
            }
            

        }
        */
        Move();


        }
    public void Move()
    {
        
        Direction = Vector3.zero;
        Horizontale = 0;
        Vertical= 0;
        Horizontale += Input.GetAxisRaw("Horizontal");
        Vertical += Input.GetAxisRaw("Vertical");
        Direction = new Vector3(Horizontale, 0, Vertical);
        Direction.Normalize();
        Direction *= Vitesse;
        Direction.z +=Vertical * Vitesse * Time.deltaTime;
        Direction.x +=Horizontale * Vitesse * Time.deltaTime;

        VerticalSpeed-=Gravity*Time.deltaTime; 
        if (Input.GetButtonDown("Jump") && IsGrounded) {
            VerticalSpeed = Mathf.Sqrt(3 * JumpForce * Gravity);
           // Direction += transform.up * Time.deltaTime * JumpForce;
            Debug.Log("Le if s'est déclenché");
        }



        Direction.y = VerticalSpeed;
        if(Direction.y< -3)
        {
            Direction.y = -3;
        }
        Controller.Move(Direction*Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Poisson"))
        {

            Rigidbody rb= collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushDirection = collision.transform.position - transform.position;
                rb.AddForce(pushDirection.normalized * Force, ForceMode.Impulse);


            }
           
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CubeRef"))
        {

            GameObject PoissonInstance;
            PoissonInstance=Instantiate(Poisson);
            PoissonInstance.GetComponent<PoissonDie>().Scoring = Stricking;
            PoissonInstance.transform.position = new Vector3(Random.Range(-5,5), Random.Range(0,5), Random.Range(-5,5));
            

        }
        
    }

    void FinishGame()
    {
        Fin.text = "END";
        this.enabled= false;
    }

}
