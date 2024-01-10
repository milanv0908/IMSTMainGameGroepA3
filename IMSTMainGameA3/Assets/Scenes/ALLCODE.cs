using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuuu : MonoBehaviour
{
public CursorState cursorState;
public static bool gameIsPaused;
public GameObject pauseMenuUI;

 public Camerabob Camerabob;

public PlayerMove playerMovement;

 public speler player;
void Start() {
    resume();
}

    void Update()
    {
       if(cursorState.cursorState == true) {
        Cursor.lockState = CursorLockMode.None;
       }
        ShowPauseMenu();
        if (gameIsPaused) {
            resume();
            Debug.Log("resume");
        } else {
            pause();
            Debug.Log("pause");
        }
       
    }

    void ShowPauseMenu () {
            Cursor.visible = true;
            cursorState.cursorState = true;
    }

    public void resume () {
pauseMenuUI.SetActive(false);
gameIsPaused = false;
playerMovement.enabled = true;
Camerabob.enabled = true;
cursorState.cursorState = false;
    }

    void pause () {
pauseMenuUI.SetActive(true);
// Time.timeScale = 1f;
playerMovement.enabled = false;
Camerabob.enabled = false;
gameIsPaused = true;
    }

    public void Quitgame() {
    SceneManager.LoadScene("Start menu");
}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class williamgaatlos : MonoBehaviour
{
    public frank Frank;
    Animator animator;
    AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();  // Fix the typo here
        audioSource.enabled = false;
    }

    void Update()
    {
        if (Frank.animation == true)
            animator.SetTrigger("start");

        if (Frank.beginend == true)
        {
            audioSource.enabled = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorState : MonoBehaviour
{
public bool cursorState = false;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExitSystem : MonoBehaviour
{
    public MonoBehaviour CarController;
    public Transform Car;
    public Transform Player;

    [Header("Cameras")]
    public Camera PlayerCam;
    public Camera CarCam;

    bool CanDrive;

    // Start is called before the first frame update
    void Start()
    {
        CarController.enabled = false;
        CarCam.enabled = false;
        PlayerCam.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && CanDrive)
        {
            CarController.enabled = true;

            Player.transform.SetParent(Car);
            Player.gameObject.SetActive(false);

            // Switch to the car camera
            CarCam.enabled = true;
            PlayerCam.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            CarController.enabled = false;

            Player.transform.SetParent(null);
            Player.gameObject.SetActive(true);

            // Switch to the player camera
            CarCam.enabled = false;
            PlayerCam.enabled = true;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            CanDrive = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            CanDrive = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerofmass : MonoBehaviour
{
    public static class PhysicsHelper
    {

        public static void ApplyForceToReachVelocity(Rigidbody rigidbody, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
        {
            if (force == 0 || velocity.magnitude == 0)
                return;

            velocity = velocity + velocity.normalized * 0.2f * rigidbody.drag;

            //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
            force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

            //dot product is a projection from rhs to lhs with a length of result / lhs.magnitude https://www.youtube.com/watch?v=h0NJK4mEIJU
            if (rigidbody.velocity.magnitude == 0)
            {
                rigidbody.AddForce(velocity * force, mode);
            }
            else
            {
                var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude);
                rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
            }
        }

        public static void ApplyTorqueToReachRPS(Rigidbody rigidbody, Quaternion rotation, float rps, float force = 1)
        {
            var radPerSecond = rps * 2 * Mathf.PI + rigidbody.angularDrag * 20;

            float angleInDegrees;
            Vector3 rotationAxis;
            rotation.ToAngleAxis(out angleInDegrees, out rotationAxis);

            if (force == 0 || rotationAxis == Vector3.zero)
                return;

            rigidbody.maxAngularVelocity = Mathf.Max(rigidbody.maxAngularVelocity, radPerSecond);

            force = Mathf.Clamp(force, -rigidbody.mass * 2 * Mathf.PI / Time.fixedDeltaTime, rigidbody.mass * 2 * Mathf.PI / Time.fixedDeltaTime);

            var currentSpeed = Vector3.Project(rigidbody.angularVelocity, rotationAxis).magnitude;

            rigidbody.AddTorque(rotationAxis * (radPerSecond - currentSpeed) * force);
        }

        public static Vector3 QuaternionToAngularVelocity(Quaternion rotation)
        {
            float angleInDegrees;
            Vector3 rotationAxis;
            rotation.ToAngleAxis(out angleInDegrees, out rotationAxis);

            return rotationAxis * angleInDegrees * Mathf.Deg2Rad;
        }

        public static Quaternion AngularVelocityToQuaternion(Vector3 angularVelocity)
        {
            var rotationAxis = (angularVelocity * Mathf.Rad2Deg).normalized;
            float angleInDegrees = (angularVelocity * Mathf.Rad2Deg).magnitude;

            return Quaternion.AngleAxis(angleInDegrees, rotationAxis);
        }

        public static Vector3 GetNormal(Vector3[] points)
        {
            //https://www.ilikebigbits.com/2015_03_04_plane_from_points.html
            if (points.Length < 3)
                return Vector3.up;

            var center = GetCenter(points);

            float xx = 0f, xy = 0f, xz = 0f, yy = 0f, yz = 0f, zz = 0f;

            for (int i = 0; i < points.Length; i++)
            {
                var r = points[i] - center;
                xx += r.x * r.x;
                xy += r.x * r.y;
                xz += r.x * r.z;
                yy += r.y * r.y;
                yz += r.y * r.z;
                zz += r.z * r.z;
            }

            var det_x = yy * zz - yz * yz;
            var det_y = xx * zz - xz * xz;
            var det_z = xx * yy - xy * xy;

            if (det_x > det_y && det_x > det_z)
                return new Vector3(det_x, xz * yz - xy * zz, xy * yz - xz * yy).normalized;
            if (det_y > det_z)
                return new Vector3(xz * yz - xy * zz, det_y, xy * xz - yz * xx).normalized;
            else
                return new Vector3(xy * yz - xz * yy, xy * xz - yz * xx, det_z).normalized;

        }

        public static Vector3 GetCenter(Vector3[] points)
        {
            var center = Vector3.zero;
            for (int i = 0; i < points.Length; i++)
                center += points[i] / points.Length;
            return center;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController2 : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    // [SerializeField] Transform frontRightTransform;
    // [SerializeField] Transform frontLeftTransform;
    // [SerializeField] Transform backRightTransform;
    // [SerializeField] Transform backleftTransform;

    public float acceleration = 500f;
    public float breakingForce = 600f;
    public float maxTurnAngle = 15f;

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;


    private void FixedUpdate() {
        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space)) {
            currentBreakForce = breakingForce;
        } else {
            currentBreakForce = 0f;
        }

        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;

        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;

        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        // UpdateWheel(frontLeft, frontLeftTransform);
        // UpdateWheel(backLeft, backleftTransform);
        // UpdateWheel(frontRight, frontRightTransform);
        // UpdateWheel(backRight, backRightTransform);
    }

    void  UpdateWheel(WheelCollider col, Transform trans) {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRollBar : MonoBehaviour
{
	public WheelCollider WheelL;
	public WheelCollider WheelR;
	public float AntiRoll = 5000.0f;

	private Rigidbody step;

	void Start(){
		step = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		WheelHit hit;
		float travelL = 1.0f;
		float travelR = 1.0f;


		bool groundedL = WheelL.GetGroundHit (out hit);
		if (groundedL) {
			travelL = (-WheelL.transform.InverseTransformPoint (hit.point).y - WheelL.radius) / WheelL.suspensionDistance;
		}

		bool groundedR = WheelR.GetGroundHit (out hit);
		if (groundedR) {
			travelR = (-WheelR.transform.InverseTransformPoint (hit.point).y - WheelR.radius) / WheelR.suspensionDistance;
		}

		float antiRollForce = (travelL - travelR) * AntiRoll;

		if (groundedL)
			step.AddForceAtPosition (WheelL.transform.up * -antiRollForce, WheelL.transform.position);

		if (groundedR)
			step.AddForceAtPosition (WheelR.transform.up * antiRollForce, WheelR.transform.position);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class steptutorial : MonoBehaviour
{
    public Animator animator;
    private bool hasRunPopIn = false;
    private bool hasShowed;
    public Image image;
    public static bool gameIsPaused;

    void Start()
    {
        hasShowed = false;
        image.enabled = false;
    }

    void OnTriggerEnter()
    {
        if (!hasShowed)
        {
            image.enabled = true;
            hasShowed = true;
            animator.SetTrigger("PopInTrigger");
            // Debug.Log("werken!!!!!!");
            StartCoroutine(RunPopInThenPause());
            // pause();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && gameIsPaused == true)
        {
            animator.SetTrigger("PopOut");
            // image.enabled = false;
            resume();
        }
    }

    public void resume()
    {
        // image.enabled = false;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void pause()
    {
        image.enabled = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    private IEnumerator RunPopInThenPause()
    {
        if (!hasRunPopIn)
        {
            animator.SetTrigger("PopInTrigger");
            hasRunPopIn = true;
            yield return new WaitForSeconds(0.8f);
            gameIsPaused = true;
        }
        else
        {
            while (gameIsPaused)
            {
                yield return null; // Wacht totdat de game niet meer is gepauzeerd.
            }
        }

        pause();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class looptutorial1 : MonoBehaviour
{
    public static bool gameIsPaused;
    public Image image;
    public Animator animator;

    private bool hasRunPopIn = false;

    void Start()
    {
        image.enabled = true;
        // StartCoroutine(RunPopInThenPause());
        Debug.Log("pause");
    }

public void startCoroutineMenuShits(){
    hasRunPopIn = false;
    StartCoroutine(RunPopInThenPause());
}
    void Update()
    {
        if (gameIsPaused == true && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("PopOut");
            resume();
            Debug.Log("resume");
        }
    }

    private void resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
         hasRunPopIn = true;
        if(Input.GetKeyDown(KeyCode.E)){
            resume();
        }
    }

    private IEnumerator RunPopInThenPause()
    {
        if (!hasRunPopIn)
        {
            animator.SetTrigger("PopInTrigger");
            hasRunPopIn = true;
            yield return new WaitForSeconds(0.8f);
            gameIsPaused = true;
            Debug.Log("hiii");
        }
        else
        {
            while (gameIsPaused)
            {
                yield return null; // Wacht totdat de game niet meer is gepauzeerd.
            }
        }

        pause();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class looptutorial : MonoBehaviour
{
    public static bool gameIsPaused;
    public Image image;
    public Animator animator;

    private bool hasRunPopIn = false;

    void Start()
    {
        image.enabled = true;
        StartCoroutine(RunPopInThenPause());
        Debug.Log("pause");
    }

public void startCoroutineMenuShits(){
    hasRunPopIn = false;
    StartCoroutine(RunPopInThenPause());
}
    void Update()
    {
        if (gameIsPaused == true && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("PopOut");
            resume();
            Debug.Log("resume");
        }
    }

    private void resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    private IEnumerator RunPopInThenPause()
    {
        if (!hasRunPopIn)
        {
            animator.SetTrigger("PopInTrigger");
            hasRunPopIn = true;
            yield return new WaitForSeconds(0.8f);
            gameIsPaused = true;
        }
        else
        {
            while (gameIsPaused)
            {
                yield return null; // Wacht totdat de game niet meer is gepauzeerd.
            }
        }

        pause();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TateInteract : MonoBehaviour
{
    public bool TateTalked = false;
public void suzanneActive(){
    TateTalked = true;
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeterInteract : MonoBehaviour
{
    // Start is called before the first frame update
    public bool PeterTalked = false;
public void suzanneActive(){
    PeterTalked = true;
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GertInteract : MonoBehaviour
{
    // Start is called before the first frame update
    public bool GertTalked = false;
public void suzanneActive(){
    GertTalked = true;
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DialogueEditor;

public class spelertelefoon : MonoBehaviour
{
    public NPCConversation Conversation;
    public speler player;
    public Camerabob Camerabob;
    public koffiemok koffie;
    public Image telefoon;
    public TextMeshProUGUI text;

public AudioSource otherGameObjectAudioS;

public GameObject stappen;

    public Animator animator;
    private bool hasRunPopIn = false;
    private bool hasShowed;
    public Image image;
    public static bool gameIsPaused;


    bool heeftOpgehangen;
    private bool runOnce = false;
    public bool DeurOpen = false;

    void Start()
    {
        otherGameObjectAudioS = GetComponent<AudioSource>();
        runOnce = false;
                hasShowed = false;
        image.enabled = false;
    }

    void Update()
    {
        if (koffie.telefoonfrank == true && Input.GetKeyDown(KeyCode.F))
        {
            telefoon.enabled = false;
            koffie.telefoonfrank = false;
            text.enabled = false;
            heeftOpgehangen = true;

            if (!hasShowed)
        {
            image.enabled = true;
            hasShowed = true;
            animator.SetTrigger("PopInTrigger");
            // Debug.Log("werken!!!!!!");
            StartCoroutine(RunPopInThenPause());
            // pause();
        }

        }

             if (Input.GetKeyDown(KeyCode.E) && gameIsPaused == true)
        {
            animator.SetTrigger("PopOut");
            // image.enabled = false;
            resume();
        }

        if (runOnce == false)
        {
            if (!ConversationManager.Instance.IsConversationActive && heeftOpgehangen == true)
            {
                ConversationManager.Instance.StartConversation(Conversation);
                runOnce = true;
            }
        }

        if (ConversationManager.Instance != null)
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                Camerabob.enabled = false;

                if (Input.GetKeyDown(KeyCode.UpArrow))
                    ConversationManager.Instance.SelectPreviousOption();
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    ConversationManager.Instance.SelectNextOption();
                else if (Input.GetKeyDown(KeyCode.E))
                    ConversationManager.Instance.PressSelectedOption();
            }
            else
            {
                Camerabob.enabled = true;
            }
        }

        if (!ConversationManager.Instance.IsConversationActive && heeftOpgehangen == true && !DeurOpen)
        {
            DeurOpen = true;
        }
    }




     public void resume()
    {
        // image.enabled = false;
        Time.timeScale = 1f;
        gameIsPaused = false;
        otherGameObjectAudioS.enabled = true;
        stappen.SetActive(true);
    }

    void pause()
    {
        image.enabled = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
        otherGameObjectAudioS.enabled = false;
         stappen.SetActive(false);
    }

    private IEnumerator RunPopInThenPause()
    {
        if (!hasRunPopIn)
        {
            animator.SetTrigger("PopInTrigger");
            hasRunPopIn = true;
            yield return new WaitForSeconds(0.8f);
            gameIsPaused = true;
        }
        else
        {
            while (gameIsPaused)
            {
                yield return null; // Wacht totdat de game niet meer is gepauzeerd.
            }
        }

        pause();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class speler : MonoBehaviour
{
    public bool heeftPlayer2Gekozen;
    public bus buss;

    private int currentSceneIndex; // Variabele om de huidige scène-index bij te houden

    void Start()
    {
        heeftPlayer2Gekozen = false;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Haal de huidige scène-index op
    }

    void Update()
    {
        // Controleer of we in "Scene 2" zijn voordat we de code uitvoeren
        if (currentSceneIndex == 2 && buss.playerd == true)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class profielfoto1 : MonoBehaviour
{
    public Image profiel;
    public Image profielmetmask;

    public maskerscript maskopgepakt;



    void Start()
    {
        profiel.enabled = true;
        profielmetmask.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (maskopgepakt.maskeraan)
        {
            profiel.enabled = false;
            profielmetmask.enabled = true;
        }


    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class objectivescript1 : MonoBehaviour
{
    AudioSource audiosource;
    public TextMeshProUGUI text;
    public koffiezetapparaat koffiedrinken;
    public koffiemok koffiemok;
    public AudioClip objectivegeluid;
    public spelertelefoon spelerbool;
    private bool textchanged1 = false;
    private bool textchanged2 = false;
    private bool textchanged3 = false;


    void Start()
    {
        text.enabled = true;
        text.text = "Objective: Set a cup of Coffee";
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (koffiedrinken.koffieklaar == true && !textchanged1)
        {
            text.text = "Objective: Drink the cup of coffee";
            textchanged1 = true;
            audiosource.PlayOneShot(objectivegeluid);

        }

        if (koffiemok.telefoonfrank == true && !textchanged2)
        {
            text.text = "Objective: Answer the phone and listen to the caller";
            textchanged2 = true;
        }

            if (spelerbool.DeurOpen == true && !textchanged3)
        {
            text.text = "Objective: Find you're AirMask and saxophone and leave your appartment";
            textchanged3 = true;
            audiosource.PlayOneShot(objectivegeluid);
        }

        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepscriptsimple : MonoBehaviour
{
    PlayerMove playermove;
    public GameObject Footstep;

    void Start()
    {
        playermove = GetComponent<PlayerMove>();
        Footstep.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKey("w"))
        {
            footsteps();
        }

        if(Input.GetKeyDown("s"))
        {
            footsteps();
        }

        if(Input.GetKeyDown("a"))
        {
            footsteps();
        }

        if(Input.GetKeyDown("d"))
        {
            footsteps();
        }

        if(Input.GetKeyUp("w"))
        {
            StopFootsteps();
        }

        if(Input.GetKeyUp("s"))
        {
            StopFootsteps();
        }

        if(Input.GetKeyUp("a"))
        {
            StopFootsteps();
        }

        if(Input.GetKeyUp("d"))
        {
            StopFootsteps();
        }

        if(Input.GetKeyUp("space"))
        {
            StopFootsteps();
        }


if (!playermove.isGrounded())
{
    StopFootsteps();
}

    }

    void footsteps()
    {
        Footstep.SetActive(true);
    }

    void StopFootsteps()
    {
        Footstep.SetActive(false);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepscriptbuiten : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstepscript : MonoBehaviour
{
    public PlayerMove PlayerMove;
    private AudioSource audiosource;
    private float footstepTimer = 0.0f; // You need to declare this variable.
    private float timePerStep = 5.3f;

    private enum TerrainTags
    {
wood,
stone,
grass,

    }

    [SerializeField] private AudioClip[] footstepaudio;

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

 private void Update()
{
    footstepTimer += Time.deltaTime;

    if (PlayerMove.isMoving && audiosource.clip && footstepTimer > timePerStep)
    {
        audiosource.Play();
        footstepTimer = 0;
            // Debug.Log("voetstappen");
    }
    else if (!PlayerMove.isMoving)
    {
        audiosource.Stop(); // Stop the audio when the player is not moving.
            // Debug.Log("geen voetstappen");
    }

    if (!PlayerMove.isGrounded())
    {
        audiosource.enabled = false;
    }
    else
    {
        audiosource.enabled = true;
    }
}


private void OnCollisionEnter(Collision col)
{
    string currentTag = col.gameObject.tag;

    for (int index = 0; index < footstepaudio.Length; index++)
    {
        if (currentTag == Enum.GetNames(typeof(TerrainTags))[index])
        {
            // Check if the current audio clip is different from the new one.
            if (audiosource.clip != footstepaudio[index])
            {
                audiosource.clip = footstepaudio[index];
                audiosource.Play(); // Start playing the new audio clip.
            }
            return; // Exit the loop since we found a matching tag.
        }
    }
}

}
using UnityEngine;

public class FogFollowPlayer : MonoBehaviour
{
    public Transform target; // De speler transform

    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerabob : MonoBehaviour
{

    [Range(0.001f, 0.1f)]
    public float Amount = 0.002f;
    [Range(1f, 30f)]
    public float Frequency = 10.0f;
    [Range(10f, 100f)]
    public float Smooth = 10.0f;

    Vector3 StartPos;

    void Start() {
        StartPos = transform.localPosition;
    }

    void Update() {
        CheckForHeadbobTrigger();
        StopHeadbob();
    }

    private void CheckForHeadbobTrigger() {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if (inputMagnitude > 0) {
            StartHeadBob();
        }
    }

    private Vector3 StartHeadBob() {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * Frequency) * Amount * 1.4f, Smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * Frequency / 2f) * Amount * 1.6f, Smooth * Time.deltaTime);
        transform.localPosition += pos;
        return pos;
    }

    private void StopHeadbob() {
        if (transform.localPosition == StartPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, 1 * Time.deltaTime);
    }
}
//  https://www.youtube.com/watch?v=vqc9f7HU-Vc&ab_channel=WorldofZero
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetector : MonoBehaviour

{
    public GameObject lasthit;
    public Vector3 collision = Vector3.zero;

    void Update()
        {
            var ray = new Ray(this.transform.position, this.transform.up * -1);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                {
                      lasthit = hit.transform.gameObject;
                      collision = hit.point;
                }
        }

    public GameObject PlayerTerrain()
        {
          return lasthit;
        }

    void OnDrawGizmos()
        {
          Gizmos.color = Color.red;
          Gizmos.DrawWireSphere(collision, 0.2f);
        }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footSteps : MonoBehaviour
{

  private AudioSource audioSource;
  private GameObject terrainFoot;
  private AudioSource terrainFootPrev;
  private AudioSource terrainFootNext;

  // LIST OF TERRAINS
  public AudioSource footstepGrass;  // 1
  public AudioSource footstepFloor;
  public AudioSource footstepSand;
  public AudioSource footstepWater;
  public AudioSource footstepSnow;
  public AudioSource footstepBridge;

  private void Start(){
    terrainFootNext = terrainFootPrev = footstepGrass;  // PLAYER STARTING TERRAIN
  }


  void Update()
  {
    audioSource = GetComponent<AudioSource>();

    terrainFoot = FindObjectOfType<TerrainDetector>().PlayerTerrain();


        if( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)  )
            {

              switch(terrainFoot.name)
              {
                    case "GRASS":
                          footstepGrass.enabled   = true;   // 2
                          terrainFootPrev = footstepGrass;  // 3
                          break;
                    case "floor":
                          footstepFloor.enabled   = true;
                          terrainFootPrev = footstepFloor;
                          break;
                    case "SAND":
                          footstepSand.enabled   = true;
                          terrainFootPrev = footstepSand;
                          break;
                    case "WATER":
                          footstepWater.enabled   = true;
                          terrainFootPrev = footstepWater;
                          break;
                    case "SNOW":
                          footstepSnow.enabled   = true;
                          terrainFootPrev = footstepSnow;
                          break;
                    case string a when a.Contains("BRIDGE"):
                          footstepBridge.enabled   = true;
                          terrainFootPrev = footstepBridge;
                          break;

                    default:
                          break;

              }


              if(terrainFootPrev != terrainFootNext)
                {
                    terrainFootNext.enabled = false;
                    terrainFootNext = terrainFootPrev;
                }

            }else{
              footstepGrass.enabled = false;  // 4
              footstepFloor.enabled = false;
              footstepSand.enabled = false;
              footstepWater.enabled = false;
              footstepSnow.enabled = false;
              footstepBridge.enabled = false;
            }

  }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwap : MonoBehaviour
{
    public AudioClip newTrack;

    private void OnTriggerEnter(Collider other)
        {
          if(other.CompareTag("Player"))
              {
                AudioManager.instance.SwapTrack(newTrack);
              }
        }

    private void OnTriggerExit(Collider other)
        {
          if(other.CompareTag("Player"))
              {
                AudioManager.instance.ReturnToDefault();
              }
        }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

  private AudioSource track01, track02;
  private bool isPlayingTrack01;
  public AudioClip defaultAmbience;
  public static AudioManager instance;

  public void Awake()
      {
        if (instance == null)
          instance = this;
      }

  private void Start()
      {
        track01 = gameObject.AddComponent<AudioSource>();
        track02 = gameObject.AddComponent<AudioSource>();
        isPlayingTrack01 = true;
        SwapTrack(defaultAmbience);
      }

  public void SwapTrack(AudioClip newClip)
      {

            StopAllCoroutines();

            StartCoroutine(FadeTrack(newClip));

            isPlayingTrack01 = !isPlayingTrack01;
      }

      public void ReturnToDefault()
          {
            SwapTrack(defaultAmbience);
          }


      private IEnumerator FadeTrack(AudioClip newClip)
          {
            float timeToFade = 1.25f;
            float timeElapsed = 0;
            if (isPlayingTrack01)
                {
                  track02.clip = newClip;
                  track02.Play();
                  track02.loop = true;

                  while(timeElapsed < timeToFade)
                      {
                        track02.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                        track01.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                        timeElapsed += Time.deltaTime;
                        yield return null;
                      }
                  track01.Stop();
                }
                else
                {
                  track01.clip = newClip;
                  track01.Play();
                  track01.loop = true;

                  while(timeElapsed < timeToFade)
                      {
                        track01.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                        track02.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                        timeElapsed += Time.deltaTime;
                        yield return null;
                      }
                  track02.Stop();
                }

          }



}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if(waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

   public void PlayGame()
{
    Time.timeScale = 1f; // Stel de tijd schaal in op 1
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
public CursorState cursorState;
public static bool gameIsPaused;
public GameObject pauseMenuUI;

 public Camerabob Camerabob;

public PlayerMove playerMovement;

 public speler player;
void Start() {
    resume();
}

    void Update()
    {
       if(cursorState.cursorState == true) {
        Cursor.lockState = CursorLockMode.None;
       }

       if (Input.GetKeyDown(KeyCode.Escape)) {
        ShowPauseMenu();
        if (gameIsPaused) {
            resume();
            Debug.Log("resume");
        } else {
            pause();
            Debug.Log("pause");
        }
       }
    }

    void ShowPauseMenu () {
            Cursor.visible = true;
            cursorState.cursorState = true;
    }

    public void resume () {
pauseMenuUI.SetActive(false);
gameIsPaused = false;
playerMovement.enabled = true;
Camerabob.enabled = true;
cursorState.cursorState = false;
    }

    void pause () {
pauseMenuUI.SetActive(true);
// Time.timeScale = 1f;
playerMovement.enabled = false;
Camerabob.enabled = false;
gameIsPaused = true;
    }

    public void Quitgame() {
    SceneManager.LoadScene("Start menu");
}
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class buttonsound : MonoBehaviour
// {
//     public AudioSource audioSource;
//     public AudioClip hover;
//     public AudioClip click;

//     public void HoverSound() {
//         audioSource.PlayOneShot(hover);
//         Debug.Log("hover");
//     }

//     public void ClickSound() {
//         audioSource.PlayOneShot(click);
//         Debug.Log("click");
//     }


// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonsound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hover;
    public AudioClip click;

    private bool isPlayingClick = false;

    public void HoverSound()
    {
        if (!isPlayingClick)
        {
            audioSource.PlayOneShot(hover);
            Debug.Log("hover");
        }
    }

    public void ClickSound()
    {
        if (!isPlayingClick)
        {
            StartCoroutine(PlayClickSound());
        }
    }

    private IEnumerator PlayClickSound()
    {
        isPlayingClick = true;
        audioSource.PlayOneShot(click);
        yield return new WaitForSeconds(click.length);
        isPlayingClick = false;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zuurstofPickup : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public hpBar hpBar;
    

    void Start()
    {
        currentHealth = maxHealth; // Stel currentHealth in op de maximale gezondheid
        hpBar.setMaxHealth(maxHealth);
    }

  public void zuurstofPickupObject()
{
    PlayerHP playerHP = FindObjectOfType<PlayerHP>(); // Vind de spelergezondheidsscript
    if (playerHP != null)
    {
        playerHP.RestoreHealthToMax(); // Herstel de spelergezondheid naar 100%
    }

    // Reset de schade timer in het PlayerHP-script
    playerHP.damageTimer = 0.1f; // Stel de timer in op 0.1 seconden (1 frame)
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public hpBar hpBar;
    public float damageTimer = 0.5f; // Wacht x seconde tussen schade

    void Start()
    {
        currentHealth = maxHealth;
        hpBar.setMaxHealth(maxHealth);
    }

    void Update()
    {
        // Verminder de timer met de verstreken tijd sinds de vorige frame
        damageTimer -= Time.deltaTime;

        // Als de timer is verstreken, haal 1 HP af en reset de timer
        if (damageTimer <= 0)
        {
            TakeDamage(1);
            damageTimer = 0.5f; // Reset de timer naar x seconde
        }

        if(currentHealth == 0){
            Debug.Log("Game Over");
            currentHealth = 0;
            hpBar.setHealth(currentHealth);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        hpBar.setHealth(currentHealth);
    }

    public void RestoreHealthToMax()
{
    currentHealth = maxHealth;
    hpBar.setHealth(currentHealth);
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpBar : MonoBehaviour
{

 public Slider slider;
 public Gradient gradient;
 public Image fill;

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void setHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spelerfrank : MonoBehaviour
{
    public frank Frank;
    void Update()
    {
     if (Frank.beginend == true) {
        GetComponent<MeshRenderer>().enabled = false;
     }   
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class objectivefrank : MonoBehaviour
{
    public coffeemachine air;
    public luchtfilter filter;
    public frank Frank;
    public TextMeshProUGUI text;
    AudioSource audiosource;
    public AudioClip objectivegeluid;

    void Start()
    {
        text.enabled = true;
        text.text = "Objective: Talk to Frank.";
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        if (Frank.newobjective == true) {
            text.text = "Objective: Set a cup of Coffee.";
            Debug.Log("newobjective");
            Frank.newobjective = false;
            audiosource.PlayOneShot(objectivegeluid);
        }

        if (air.airobjective == true) {
            text.text = "Objective: Turn on the airfilter";
            air.airobjective = false;
            audiosource.PlayOneShot(objectivegeluid);
        }

        if (filter.objectiveend == true){
            text.text = "Objective: Go play with Frank";
            filter.objectiveend = false;
            audiosource.PlayOneShot(objectivegeluid);
        }
        

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nintendo : MonoBehaviour
{
    public GameObject UIUpdate; // The GameObject with the Image component
    public Transform player; // Player reference
    public coffeemachine koffie; // Assuming coffeemachine is the correct class
    private float activationDistance = 5.0f;

    void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    void Update()
    {
        if (koffie.lekkerkoffie == true)
        {
            GetComponent<BoxCollider>().enabled = true;
        }

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= activationDistance && koffie.lekkerkoffie == true)
            {
                UIUpdate.SetActive(true);
            }
            else
            {
                UIUpdate.SetActive(false);
            }

            // Update the rotation of the UIUpdate object to follow the player's rotation
            UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);

            // Reset the scale to positive values
            UIUpdate.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class luchtfilter : MonoBehaviour
{
    public GameObject UIUpdate; // The GameObject with the Image component
    public Transform player; // Player reference
    public coffeemachine koffie; // Assuming coffeemachine is the correct class
    public bool ending = false;
    public bool objectiveend = false;
    private float activationDistance = 5.0f;
    public NPCConversation Conversation;
    private bool poep;

    void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    public void Interact() {
        
        ending = true;
    
        poep = true;
    }

    void Update()
    {
        if (koffie.lekkerkoffie == true)
        {
            GetComponent<BoxCollider>().enabled = true;
        }

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= activationDistance && koffie.lekkerkoffie == true && ending == false)
            {
                UIUpdate.SetActive(true);
            }
            else
            {
                UIUpdate.SetActive(false);
            }

            // Update the rotation of the UIUpdate object to follow the player's rotation
            UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);

            // Reset the scale to positive values
            UIUpdate.transform.localScale = new Vector3(1, 1, 1);
        }

                 if (!ConversationManager.Instance.IsConversationActive && poep == true)
        {
            poep = false;
            objectiveend = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frankdeur : MonoBehaviour
{
    public williambellen deur;
    Animator animator;
    void Start()
    {
      animator = GetComponent<Animator>();  
    }

    void Update()
    {
    if (deur.dooropen == true) {
        animator.SetTrigger("open");
    }
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class frankcanvasdisable : MonoBehaviour
{
    public Canvas canvas; // Reference to the Canvas component. Assign the Canvas in the Inspector.
    public frank Frank;
    public switchcamerascene2 jot;

    void Start()
    {
        // Ensure that the canvas component is assigned in the Inspector.
        if (canvas != null)
        {
            canvas.enabled = false;
        }
        else
        {
            Debug.LogError("Canvas component is not assigned. Please assign it in the Inspector.");
        }
    }

    private void Update()
    {
        if (jot.eind == true && canvas != null && Frank.beginend == false)
        {
            canvas.enabled = true;
        }

        if (Frank.beginend == true) {
            canvas.enabled = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class frank : MonoBehaviour
{

public GameObject UIUpdate;
private float activationDistance = 10.0f;
public Transform player;
AudioSource audiosource;
public luchtfilter filter;
public bool hasinteracted1 = false;
public bool newobjective = false;
public bool isused = false;
private bool isused1 = false;
        public bool animation = false;

public bool beginend = false;
        private bool poop = false;

public NPCConversation Conversation;
private Vector3 originalPosition; // Variabele om de oorspronkelijke positie op te slaan

void Start() {
        audiosource = GetComponent<AudioSource>();
        audiosource.enabled = false;
        StartCoroutine(audioisgone());
        UIUpdate.SetActive(false);
}
    IEnumerator audioisgone() {
        yield return new WaitForSeconds(13);
        audiosource.enabled = true;

}

public void interacting() {
                if (isused == false)
                {
                        hasinteracted1 = true;
                        isused = true;
                        poop = true;
                }

                if (filter.ending == true) {
                        beginend = true;
                        UIUpdate.SetActive(false);
                        filter.ending = false;
                        animation = true;
                        audiosource.enabled = false;
                }
              
}

void Update()
{
    if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && isused == false)
        {
            UIUpdate.SetActive(true);
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Check if filter.ending is true and activate UIUpdate accordingly
        if (filter.ending == true)
        {
            UIUpdate.SetActive(true);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
    }

                if (!ConversationManager.Instance.IsConversationActive && poop == true)
        {
            newobjective = true;
                poop = false;
        }
}


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dishwash : MonoBehaviour
{
    public GameObject UIUpdate; // The GameObject with the Image component
    public Transform player; // Player reference
    public coffeemachine koffie; // Assuming coffeemachine is the correct class
    private float activationDistance = 5.0f;

    void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    void Update()
    {
        if (koffie.lekkerkoffie == true)
        {
            GetComponent<BoxCollider>().enabled = true;
        }

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= activationDistance && koffie.lekkerkoffie == true)
            {
                UIUpdate.SetActive(true);
            }
            else
            {
                UIUpdate.SetActive(false);
            }

            // Update the rotation of the UIUpdate object to follow the player's rotation
            UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);

            // Reset the scale to positive values
            UIUpdate.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class couch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class coffeemachine : MonoBehaviour
{
    public GameObject UIUpdate; // Het gameobject met de Image-component
    public Transform player; // Spelerreferentie
    public frank frank;

    public bool lekkerkoffie = false;
    public bool airobjective = false;
    public bool poep = false;
    AudioSource audiosource;
    private float activationDistance = 5.0f;

    void Start() {
    GetComponent<BoxCollider>().enabled = false;
    audiosource = GetComponent<AudioSource>();
    UIUpdate.SetActive(false);
        poep = false;
    }

    public void interact() {
        lekkerkoffie = true;
        poep = true;

    }

    void Update() {
        if (frank.hasinteracted1 == true) {
GetComponent<BoxCollider>().enabled = true;
        }
        if(lekkerkoffie == true) {
            GetComponent<BoxCollider>().enabled = false;

        }

    if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && frank.hasinteracted1 == true && lekkerkoffie == false)
        {
            UIUpdate.SetActive(true);
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
        //  UIUpdate.transform.rotation = Quaternion.Euler(270, player.rotation.eulerAngles.z, 0);
        
        // Reset de schaal naar positieve waarden
        // UIUpdate.transform.localScale = new Vector3(1, 1, 1);
    }

         if (!ConversationManager.Instance.IsConversationActive && poep == true)
        {
            airobjective = true;
            poep = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiouit : MonoBehaviour
{
    AudioSource audiosource;
public frank Frank;
    void Start()
    {
    audiosource = GetComponent<AudioSource>();  
    }

    

    void Update()
    {
        if (Frank.beginend == true) {
            audiosource.enabled = false;
        }
    }
}
using UnityEngine;

public class TextSpawnTimeCalculator : MonoBehaviour
{
    private float startTime;
    private float stopTime;
    private bool isTiming;

    public void StartSpawnTimer()
    {
        startTime = Time.time;
        isTiming = true;
    }

    public void StopSpawnTimer()
    {
        stopTime = Time.time;
        CalculateAndLogTime();
        isTiming = false;
    }

    private void CalculateAndLogTime()
    {
        if (isTiming)
        {
            float currentTime = Time.time;
            float elapsedTime = currentTime - startTime;

            // In dit voorbeeld loggen we de tijd wanneer het inspelen van tekst begint
            // en wanneer het inspelen stopt.
            Debug.Log("Text spawning started at " + startTime.ToString("F2") + " seconds.");
            Debug.Log("Text spawning stopped at " + stopTime.ToString("F2") + " seconds.");
            Debug.Log("Total spawn time: " + elapsedTime.ToString("F2") + " seconds.");
        }
    }
}
// op het object voeg als component het NPCConversationscript
//voeg dit script toe
//sleep dit in je object interactionscript en kies als functie interactietest1
//voeg de speler/camera etc. script toe om te disabelen in je inspector.
//bij conversation voeg het object toe waarmee je wilt praten (deze is hetzelfde als waar dit script op zit)
//ga naar windows dialogue editor en edit de dialoog. vergeet niet om de audio op 1 te zetten
//wil je nieuwe dingen doe dat in je editor met rechterklik dan krijg je de opties


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class dialoguesettingsCursorTate : MonoBehaviour
{

     public Camerabob Camerabob; // Voeg een referentie naar het bewegingsscript van de speler toe.
    public NPCConversation Conversation;
    public speler player;
    public ObjectInteraction scriptToDisable; // Voeg een referentie naar het script dat je wilt uitschakelen toe.
    public PlayerMove playerMovement; // Voeg een referentie naar het bewegingsscript van de speler toe.

    private void Update()
    {
        if (ConversationManager.Instance != null)
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                // Schakel het script uit wanneer de conversatie actief is
                scriptToDisable.enabled = false;
                // Schakel de beweging van de speler uit
                playerMovement.enabled = false;
                Camerabob.enabled = false;

//maak inputs
                // Controleer of de bool "kies2" true is
        
            }
            else
            {
                // Zorg ervoor dat het script weer wordt ingeschakeld wanneer de conversatie niet actief is
                scriptToDisable.enabled = true;
                // Schakel de beweging van de speler weer in
                playerMovement.enabled = true;
                Camerabob.enabled = true;

                // Voer andere logica uit wanneer de conversatie niet actief is
            }
        }
    }

    public void interactieTestTate()
    {
        if (!ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.StartConversation(Conversation);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueSettingsOnTriggerEnter : MonoBehaviour
{
    public NPCConversation Conversation;
    public speler player; // 'speler' veranderd naar 'Speler'

    private Rigidbody playerRigidbody;
    private float originalDrag;
    private float originalAngularDrag;
    private BoxCollider boxCollider;

    private void Start()
    {
        // Haal een verwijzing naar de Rigidbody van de speler op
        playerRigidbody = player.GetComponent<Rigidbody>();
        originalDrag = playerRigidbody.drag;
        originalAngularDrag = playerRigidbody.angularDrag;

        // Haal een verwijzing naar de BoxCollider op
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        // De rest van je Update-logica hier...
    }

    public void OnTriggerEnter(Collider other)
    {
        playerRigidbody.drag = 5.0f; // Lineaire demping
        playerRigidbody.angularDrag = 5.0f; // Angulaire demping

        StartCoroutine(WaitForTutorial());
        if (!ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.StartConversation(Conversation);
        }

        // Schakel de BoxCollider uit
        boxCollider.enabled = false;
    }

    IEnumerator WaitForTutorial()
    {
        yield return new WaitForSeconds(3);

        // Herstel de oorspronkelijke waarden van de Rigidbody na een vertraging
        playerRigidbody.drag = originalDrag;
        playerRigidbody.angularDrag = originalAngularDrag;
    }
}
// op het object voeg als component het NPCConversationscript
//voeg dit script toe
//sleep dit in je object interactionscript en kies als functie interactietest1
//voeg de speler/camera etc. script toe om te disabelen in je inspector.
//bij conversation voeg het object toe waarmee je wilt praten (deze is hetzelfde als waar dit script op zit)
//ga naar windows dialogue editor en edit de dialoog. vergeet niet om de audio op 1 te zetten
//wil je nieuwe dingen doe dat in je editor met rechterklik dan krijg je de opties


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class dialoguesettingsGert : MonoBehaviour
{

     public Camerabob Camerabob; // Voeg een referentie naar het bewegingsscript van de speler toe.
    public NPCConversation Conversation;
    public speler player;
    public ObjectInteraction scriptToDisable; // Voeg een referentie naar het script dat je wilt uitschakelen toe.
    public PlayerMove playerMovement; // Voeg een referentie naar het bewegingsscript van de speler toe.

    private void Update()
    {
        if (ConversationManager.Instance != null)
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                // Schakel het script uit wanneer de conversatie actief is
                scriptToDisable.enabled = false;
                // Schakel de beweging van de speler uit
                playerMovement.enabled = false;
                Camerabob.enabled = false;

                // Voer de rest van je dialooglogica uit
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    ConversationManager.Instance.SelectPreviousOption();
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    ConversationManager.Instance.SelectNextOption();
                else if (Input.GetKeyDown(KeyCode.E))
                    ConversationManager.Instance.PressSelectedOption();

                // Controleer of de bool "kies2" true is
        
            }
            else
            {
                // Zorg ervoor dat het script weer wordt ingeschakeld wanneer de conversatie niet actief is
                scriptToDisable.enabled = true;
                // Schakel de beweging van de speler weer in
                playerMovement.enabled = true;
                Camerabob.enabled = true;

                // Voer andere logica uit wanneer de conversatie niet actief is
            }
        }
    }

    public void interactieTestGert()
    {
        if (!ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.StartConversation(Conversation);
        }
    }
}
// op het object voeg als component het NPCConversationscript
//voeg dit script toe
//sleep dit in je object interactionscript en kies als functie interactietest1
//voeg de speler/camera etc. script toe om te disabelen in je inspector.
//bij conversation voeg het object toe waarmee je wilt praten (deze is hetzelfde als waar dit script op zit)
//ga naar windows dialogue editor en edit de dialoog. vergeet niet om de audio op 1 te zetten
//wil je nieuwe dingen doe dat in je editor met rechterklik dan krijg je de opties


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class dialoguesettings : MonoBehaviour
{

     public Camerabob Camerabob; // Voeg een referentie naar het bewegingsscript van de speler toe.
    public NPCConversation Conversation;
    public speler player;
    public ObjectInteraction scriptToDisable; // Voeg een referentie naar het script dat je wilt uitschakelen toe.
    public PlayerMove playerMovement; // Voeg een referentie naar het bewegingsscript van de speler toe.

    private void Update()
    {
        if (ConversationManager.Instance != null)
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                // Schakel het script uit wanneer de conversatie actief is
                scriptToDisable.enabled = false;
                // Schakel de beweging van de speler uit
                playerMovement.enabled = false;
                Camerabob.enabled = false;

                // Voer de rest van je dialooglogica uit
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    ConversationManager.Instance.SelectPreviousOption();
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    ConversationManager.Instance.SelectNextOption();
                else if (Input.GetKeyDown(KeyCode.E))
                    ConversationManager.Instance.PressSelectedOption();

                // Controleer of de bool "kies2" true is
        
            }
            else
            {
                // Zorg ervoor dat het script weer wordt ingeschakeld wanneer de conversatie niet actief is
                scriptToDisable.enabled = true;
                // Schakel de beweging van de speler weer in
                playerMovement.enabled = true;
                Camerabob.enabled = true;

                // Voer andere logica uit wanneer de conversatie niet actief is
            }
        }
    }

    public void interactieTest1()
    {
        if (!ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.StartConversation(Conversation);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class blokkieInteract : MonoBehaviour
{
    public NPCConversation Conversation;
    public speler player;
    public ObjectInteraction scriptToDisable; // Voeg een referentie naar het script dat je wilt uitschakelen toe.
    public PlayerMove playerMovement; // Voeg een referentie naar het bewegingsscript van de speler toe.

    private void Update()
    {
        if (ConversationManager.Instance != null)
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                // Schakel het script uit wanneer de conversatie actief is
                scriptToDisable.enabled = false;
                // Schakel de beweging van de speler uit
                playerMovement.enabled = false;

                // Voer de rest van je dialooglogica uit
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    ConversationManager.Instance.SelectPreviousOption();
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    ConversationManager.Instance.SelectNextOption();
                else if (Input.GetKeyDown(KeyCode.E))
                    ConversationManager.Instance.PressSelectedOption();

                // Controleer of de bool "kies2" true is
                bool kies2Value = ConversationManager.Instance.GetBool("kies2");
                if (kies2Value)
                {
                    player.heeftPlayer2Gekozen = true;
                }
                else
                {
                    player.heeftPlayer2Gekozen = false;
                }
            }
            else
            {
                // Zorg ervoor dat het script weer wordt ingeschakeld wanneer de conversatie niet actief is
                scriptToDisable.enabled = true;
                // Schakel de beweging van de speler weer in
                playerMovement.enabled = true;

                // Voer andere logica uit wanneer de conversatie niet actief is
            }
        }
    }

    public void interactieTest1()
    {
        if (!ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.StartConversation(Conversation);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class blok2script : MonoBehaviour
{
public NPCConversation Conversation;

 public speler player;

 private void Update() { if (ConversationManager.Instance != null) {
 if (ConversationManager.Instance.IsConversationActive)
 {
 if (Input.GetKeyDown(KeyCode.UpArrow))
 ConversationManager.Instance.SelectPreviousOption();

 else if (Input.GetKeyDown(KeyCode.DownArrow))
 ConversationManager.Instance.SelectNextOption();

 else if (Input.GetKeyDown(KeyCode.F))
 ConversationManager.Instance.PressSelectedOption();
 }
 } 
        if(player.heeftPlayer2Gekozen == true){
            ConversationManager.Instance.SetBool("blok1heeft2gekozen", true);
        }
 }


public void interactieTest1(){
ConversationManager.Instance.StartConversation(Conversation);

}


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMarkerTate : MonoBehaviour, IQuestMarker
{
public suzanne uitt;
    public Sprite icon;
    public Image image;
    public streetevent Streetevent;

    public Sprite Icon { get { return icon; } }
    public Image Image { get { return image; } }

        void Start() {

        StartCoroutine(uit());

    }

    public Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }

    public void AddMarker()
    {
        if (image != null)
        {
            
            image.enabled = true;
            Debug.Log("enable");
        }
    }

    public void RemoveMarker() {
        image.enabled = false;
    }

    public void SetImage(Image newImage)
    {
        image = newImage;
    }

    void Update()
    {
        if (Streetevent.hasinteracted == true)
        {
            AddMarker();
            Debug.Log("huts");
        }

        if (uitt.Suzanne == true) {
            RemoveMarker();
        }
    }

                      IEnumerator uit() {
        yield return new WaitForSeconds(0.1f);
        image.enabled = false;
    

}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questmarkersuzanne : MonoBehaviour, IQuestMarker
{

     public PeterInteract peterInteractScript;
     public TateInteract tateInteractScript;
     public GertInteract gertInteractScript;

     public bool heeftshits = false;


    public Sprite icon;
    public Image image;
    public streetevent Streetevent;

    public Sprite Icon { get { return icon; } }
    public Image Image { get { return image; } }

     private Vector3 originalPosition; // Variabele om de oorspronkelijke positie op te slaan

        void Start() {

             originalPosition = transform.position;
        // Stel de Z-positie in op -1000
        transform.position = new Vector3(transform.position.x, transform.position.y, -1000);

        StartCoroutine(uit());

    }

    public Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }

    public void AddMarker()
    {
        if (image != null )
        {
            
            image.enabled = true;
            Debug.Log("enable");
        }
    }

    public void hasinteracted(){
        RemoveMarker();
    }

    public void RemoveMarker() {
        image.enabled = false;
    }

    public void SetImage(Image newImage)
    {
        image = newImage;
    }

    void Update()
    {
        if (peterInteractScript.PeterTalked == true && tateInteractScript.TateTalked == true && gertInteractScript.GertTalked == true && heeftshits == false)
        {
              transform.position = originalPosition;
            AddMarker();
            Debug.Log("huts");
            heeftshits = true;
        }

    }

                      IEnumerator uit() {
        yield return new WaitForSeconds(0.1f);
        image.enabled = false;
    

}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questmarkerpeter : MonoBehaviour, IQuestMarker
{
    public suzanne uitt;
    public Sprite icon;
    public Image image;
    public streetevent Streetevent;

    public Sprite Icon { get { return icon; } }
    public Image Image { get { return image; } }

        void Start() {

        StartCoroutine(uit());

    }

    public Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }

    public void AddMarker()
    {
        if (image != null)
        {
            
            image.enabled = true;
            Debug.Log("enable");
        }
    }

    public void RemoveMarker() {
        image.enabled = false;
    }

    public void SetImage(Image newImage)
    {
        image = newImage;
    }

    void Update()
    {
        if (Streetevent.hasinteracted == true)
        {
            AddMarker();
            Debug.Log("huts");
        }
                if (uitt.Suzanne == true) {
            RemoveMarker();
        }
    }

                      IEnumerator uit() {
        yield return new WaitForSeconds(0.1f);
        image.enabled = false;
    

}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMarkerNPC : MonoBehaviour, IQuestMarker
{
    public Sprite icon;
    public Image image;

    public Sprite Icon { get { return icon; } }
    public Image Image { get { return image; } }

    public bool jonasinteracted = false;

    public Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }

    public void RemoveMarker()
    {
        if (image != null)
        {
            image.enabled = false;
            jonasinteracted = true;
        }
    }

    public void SetImage(Image newImage)
    {
        image = newImage;
    }

    // void Update()
    // {
    //     if (praatpaal.hasinteracted == true)
    //     {
    //         RemoveMarker();
    //     }
    // }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestmarkerGert : MonoBehaviour, IQuestMarker
{
    public suzanne uitt;
    public Sprite icon;
    public Image image;
    public streetevent Streetevent;

    public Sprite Icon { get { return icon; } }
    public Image Image { get { return image; } }

        void Start() {

        StartCoroutine(uit());

    }

    public Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }

    public void AddMarker()
    {
        if (image != null)
        {
            
            image.enabled = true;
            Debug.Log("enable");
        }
    }

    public void RemoveMarker() {
        image.enabled = false;
    }

    public void SetImage(Image newImage)
    {
        image = newImage;
    }

    void Update()
    {
        if (Streetevent.hasinteracted == true)
        {
            AddMarker();
            Debug.Log("huts");
        }

        if (uitt.Suzanne == true) {
            RemoveMarker();
        }
    }

                      IEnumerator uit() {
        yield return new WaitForSeconds(0.1f);
        image.enabled = false;
    

}

}
// QuestMarkerBankje.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMarkerBankje : MonoBehaviour, IQuestMarker
{
    public Sprite icon;
    public Image image;
    public mondmaskerdispenser mondmask;

    public Sprite Icon { get { return icon; } }
    public Image Image { get { return image; } }

    
    void Start() {

            
            Debug.Log("hoi");
        StartCoroutine(uit());

    }

                  IEnumerator uit() {
        yield return new WaitForSeconds(0.1f);
        image.enabled = false;
    

}

    public Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }

    public void RemoveMarker()
    {
      
            image.enabled = true;
            
        
    }

    public void SetImage(Image newImage)
    {
        image = newImage;
    }

    void Update()
    {
        if (mondmask.hasinteracted == true)
        {
            RemoveMarker();
        }
    }
}


// QuestMarker.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMarker : MonoBehaviour, IQuestMarker
{
    public suzanne activate;
    public Sprite icon;
    public Image image;

    public Sprite Icon { get { return icon; } }
    public Image Image { get { return image; } }

        void Start() {

        StartCoroutine(uit());

    }

    public Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }

    public void AddMarker()
    {
        if (image != null)
        {
            image.enabled = true;
            Debug.Log("enable");
        }
    }

    public void RemoveMarker() {
        image.enabled = false;
    }

    public void SetImage(Image newImage)
    {
        image = newImage;
    }

    void Update()
    {
        if (activate.Suzanne == true)
        {
            
            AddMarker();
        }
    }

                      IEnumerator uit() {
        yield return new WaitForSeconds(0.1f);
        image.enabled = false;
        
    

}
}


// IQuestMarker.cs
using UnityEngine;
using UnityEngine.UI;

public interface IQuestMarker
{
    Sprite Icon { get; }
    Image Image { get; } // This should remain as a getter
    Vector2 Position { get; }
    void RemoveMarker();
    void SetImage(Image image); // Add this setter method
}// compassscript.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compassscript : MonoBehaviour
{
    public GameObject iconPrefab;
    List<IQuestMarker> questMarkers = new List<IQuestMarker>();
    public RawImage compassImage;
    public Transform player;

    float compassUnit;

    public QuestMarker Frank;
    public QuestMarkerBankje bankje;
    // public QuestMarkerNPC Jonas;
    public QuestmarkerGert Gert;

    public QuestMarkerTate Tate;
    public questmarkerpeter Peter;
    public questmarkersuzanne suzanne;

    public bus buss;

    private void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360f;
        AddQuestMarker(Frank);
        AddQuestMarker(bankje);
        // AddQuestMarker(Jonas);
        AddQuestMarker(Gert);
        AddQuestMarker(Tate);
        AddQuestMarker(Peter);
        AddQuestMarker(suzanne);
        iconPrefab.SetActive(true);
    }

    private void Update()
    {
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);

        foreach (IQuestMarker marker in questMarkers)
        {
            marker.Image.rectTransform.anchoredPosition = GetPosOnCompass(marker);
        }

        // ...
    }

    public void AddQuestMarker(IQuestMarker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
        marker.SetImage(newMarker.GetComponent<Image>());
        marker.Image.sprite = marker.Icon;
        questMarkers.Add(marker);
    }

    Vector2 GetPosOnCompass(IQuestMarker marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.Position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }

    // ...
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class compassscript : MonoBehaviour
// {
//     public GameObject iconPrefab;
//     List<QuestMarker> questMarkers = new List<QuestMarker>();
//     public RawImage compassImage;
//     public Transform player;

//     float compassUnit;

//     public QuestMarker Frank;

//     private void Start() {
//         compassUnit = compassImage.rectTransform.rect.width / 360f;
//         AddQuestMarker(Frank);
//     }

//     private void Update() {
//         compassImage.uvRect = new Rect (player.localEulerAngles.y / 360f, 0f, 1f, 1f);

//         foreach (QuestMarker marker in questMarkers) {
//             marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);

//         }
//     }
//     public void AddQuestMarker (QuestMarker marker) {
//         GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
//         marker.image = newMarker.GetComponent<compassImage>();
//         marker.image.sprite = marker.icon;
//         questMarkers.Add(marker); 
//     }

//     Vector2 GetPosOnCompass (QuestMarker marker) {
//         Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.Z);
//         Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.Z);

//         float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

//         return new Vector2(compassUnit * angle, 0f);
//     }
// }
/* 
 * PLAYER MOVE
 * Moves the Player object according to key inputs.
 * Crouching and jumping are optional
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    public float walkingSpeed = 3.0f;

    public bool jumpEnabled;

    public float jumpSpeed = 1.0f;
    public bool crouchEnabled;
    public float crouchHeight = 0.4f;
    private float normalHeight;
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;

    public bool isMoving; 

  void Start()
{
    rb = GetComponent<Rigidbody>(); // Verander GetComponent() naar GetComponent<Rigidbody>
    normalHeight = transform.localScale.y;
}


    void FixedUpdate()
    {
        Vector3 movement = new Vector3();
        bool hasInput = false;

        // Walking
        if (Input.GetKey(forwardKey))
        {
            movement += transform.forward * walkingSpeed;
            hasInput = true;
        }

        if (Input.GetKey(backKey))
        {
            movement += -transform.forward * walkingSpeed;
            hasInput = true;
        }

        if (Input.GetKey(rightKey))
        {
            movement += transform.right * walkingSpeed;
            hasInput = true;
        }

        if (Input.GetKey(leftKey))
        {
            movement += -transform.right * walkingSpeed;
            hasInput = true;
        }

        // Jumping
        if (jumpEnabled && Input.GetKey(jumpKey) && isGrounded())
        {
            movement += transform.up * jumpSpeed;
        }

        // Check if there's an obstacle in the movement direction
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movement.normalized, out hit, movement.magnitude * Time.fixedDeltaTime))
        {
            // If there's an obstacle, set movement to zero
            movement = Vector3.zero;
        }

        // make sure the rigidbody isn't sliding around when there's no input
        if (!hasInput)
        {
            rb.constraints =
                RigidbodyConstraints.FreezePositionX |
                RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            rb.constraints =
                RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ;
        }

        // maintain vertical speed
        movement.y += rb.velocity.y;

        // apply movement to rigidbody
        rb.velocity = movement;
    }

    void Update()
    {
        if (crouchEnabled && Input.GetKeyDown(crouchKey))
        {
            // Crouching
            transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
        }
        else if (crouchEnabled && Input.GetKeyUp(crouchKey))
        {
            // Not crouching
            transform.localScale = new Vector3(transform.localScale.x, normalHeight, transform.localScale.z);
        }

        // Check for movement keys here
        isMoving = Input.GetKey(forwardKey) || Input.GetKey(backKey) || Input.GetKey(leftKey) || Input.GetKey(rightKey);

        
    }
    

    // Check if player is on the ground
    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f + transform.localScale.y);
    }

    
}
/* 
 * PLAYER MOVE
 * Rotates the Player object (horizontally) and the camera (vertically)
 * according to mouse input
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float lookSpeed = 3.0f;
    private Vector2 rotation = Vector2.zero;

    public CursorState cursorState;

    void Start()
    {
      Cursor.visible = false;
    }
    void Update()
    {
      if (cursorState.cursorState == false) {
        Cursor.lockState = CursorLockMode.Locked;
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        transform.eulerAngles = new Vector2(0,rotation.y) * lookSpeed;
        Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
      } 
    }
}
/* 
 * OBJECT INTERACTION
 * Attach this to every object that needs to be interactible.
 * Here you can define:
 * - What is the object's name (used only for debugging)
 * - What is its icon (used as a crosshair)
 * - What function should it run upon interaction (make a separate script with a public function
 *   and connect it here)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectInteraction : MonoBehaviour
{
    public string objectName;
    public Sprite cursor;
    public UnityEvent interactFunction;

    public void OnInteract() {
      Debug.Log("Interacted with " + objectName);
      if (interactFunction != null)
        interactFunction.Invoke();
    }
}
/* 
 * MOUSE CURSOR
 * Attach this to an Image in the Canvas, to act as a crosshair
 * The CameraLookDetector script interacts with this to change
 * the crosshair if the player is looking at an object
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour
{
    Sprite DefaultCursor;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
        DefaultCursor = image.sprite;
    }

    public void SetCursor(Sprite cursor = null) 
    {
      if (cursor == null) cursor = DefaultCursor;

      image.sprite = cursor;
    }
}
/* 
 * CAMERA LOOK DETECTOR
 * Detects what the camera is looking at
 * If it is looking at something that has a collider, it will attempt 
 * to tell the MouseCursor script to change its cursor
 * If the Interact key is pressed while looking at an object
 * it will attempt to run the 'Interact Function' of the targets' Object Interaction script
 */

using UnityEngine;
using System.Collections;

public class CameraLookDetector : MonoBehaviour
{
    Camera cam;
    public MouseCursor mouseCursor;

    public KeyCode interactKey = KeyCode.E;

    public float maxInteractionDistance = 2.0f;

    ObjectInteraction currentLookTarget;
    void Start()
    {
        cam = GetComponent<Camera>();
        currentLookTarget = null;
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxInteractionDistance))
          LookAtInteractible(hit.transform.GetComponent<ObjectInteraction>() ?? null);
        else
          LookAtInteractible(null);

        if (Input.GetKeyDown(interactKey) && currentLookTarget != null)
        {
          currentLookTarget.OnInteract();
        }

    }

    void LookAtInteractible(ObjectInteraction obj)
    {
      currentLookTarget = obj;
      if (obj != null && obj.cursor != null)
        mouseCursor.SetCursor(obj.cursor);
      else 
        mouseCursor.SetCursor();
    }


}using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suzanne : MonoBehaviour
{
   public bool hasInteracted = false;
    public bool Suzanne = false;
    private bool interact = false;


    private float activationDistance = 10.0f;

    bool hasBeenUsed = false;
    public GameObject UIUpdate; // Het gameobject met de Image-component
    public Transform player; // Spelerreferentie

    

     void Update(){
         if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && hasInteracted == false)
        {
            UIUpdate.SetActive(true);
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);

        //  UIUpdate.transform.rotation = Quaternion.Euler(270, player.rotation.eulerAngles.z, 0);
        
        // Reset de schaal naar positieve waarden
        // UIUpdate.transform.localScale = new Vector3(1, 1, 1);
    }
     }

public void Interact() {
   if(interact == false) {
            Suzanne = true;
            interact = false;
            hasInteracted = true;
            bool hasBeenUsed = true;
            StartCoroutine(uit());
   }
}

    IEnumerator uit() {
        yield return new WaitForSeconds(1);
        Suzanne = false;

}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class streetevent : MonoBehaviour
{
    public bool hasinteracted = false;
    public Image masker;
    public Image geenmasker;

    void OnTriggerEnter()
    {
        hasinteracted = true;
        StartCoroutine(bloep());
        masker.enabled = false;
        geenmasker.enabled = true;
        
    }

                        IEnumerator bloep() {
        yield return new WaitForSeconds(0.1f);
        hasinteracted = false;
    

}
  

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peter : MonoBehaviour
{
  
    private float activationDistance = 10.0f;

    bool hasBeenUsed = false;
    public GameObject UIUpdate; // Het gameobject met de Image-component
    public Transform player; // Spelerreferentie
    // Update is called once per frame
  void Update(){
         if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && hasBeenUsed == false)
        {
            UIUpdate.SetActive(true);
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);

        //  UIUpdate.transform.rotation = Quaternion.Euler(270, player.rotation.eulerAngles.z, 0);
        
        // Reset de schaal naar positieve waarden
        // UIUpdate.transform.localScale = new Vector3(1, 1, 1);
    }
     }

     public void interact(){
         hasBeenUsed = true;
     }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class objectives : MonoBehaviour
{
    public TextMeshProUGUI text;
    public streetevent streetevent;
    public mondmaskerdispenser facemask;
     public suzanne activate;

         AudioSource audiosource;
    public AudioClip objectivegeluid;
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        text.enabled = true;
        text.text = "Objective: Walk to the busstation.";
        audiosource.PlayOneShot(objectivegeluid);
    }

    // Update is called once per frame
    void Update()
    {
       if (streetevent.hasinteracted == true) {
            text.text = "Objective: Maybe somebody in park can tell you where to find a facemask.";
            audiosource.PlayOneShot(objectivegeluid);
       }

       if (activate.Suzanne == true) {
               text.text = "Objective: Buy a facemask at the dispenser.";
            //    audiosource.PlayOneShot(objectivegeluid);
       }

       if (facemask.objectivebus == true) {
            text.text = "Objective: Go to the busstation and wait for your ride";
            // audiosource.PlayOneShot(objectivegeluid);
       }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;

public class mondmaskerdispenser : MonoBehaviour
{
    AudioSource audiosource;
    public suzanne heeftGepraat;
    public Image mondmask;
    public Image geenmondmask;

    public GameObject UIUpdate;
    private float activationDistance = 10.0f;
    public Transform player;
    public AudioClip breek; 
    public bool hasinteracted = false;
    public bool heeftmondmask = false;
    public bool objectivebus = false;
    public NPCConversation Conversation;
    

    private Vector3 originalPosition; // Variabele om de oorspronkelijke positie op te slaan

    void Start()
    {
        UIUpdate.SetActive(false);

        // Sla de oorspronkelijke positie op bij het starten
        originalPosition = transform.position;
        // Stel de Z-positie in op -1000
        transform.position = new Vector3(transform.position.x, transform.position.y, -1000);
        audiosource = GetComponent<AudioSource>();
    }

    public void mondmasker()
    {
        heeftmondmask = true;
        hasinteracted = true;
        objectivebus = true;
        StartCoroutine(bloep());
    }

    void Update()
    {
        if (heeftGepraat.hasInteracted == true)
        {
            // Zet de positie terug naar de oorspronkelijke positie
            transform.position = originalPosition;
        }

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= activationDistance && hasinteracted == false)
            {
                UIUpdate.SetActive(true);
                GetComponent<BoxCollider>().enabled = true;
            }
            else
            {
                UIUpdate.SetActive(false);
            }

            // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
            UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
        }

        if (!ConversationManager.Instance.IsConversationActive && hasinteracted == true && !objectivebus)
        {
            objectivebus = true;
            mondmask.enabled = true;
            geenmondmask.enabled = false;
            audiosource.PlayOneShot(breek);
        }
    }

    IEnumerator bloep()
    {
        yield return new WaitForSeconds(0.1f);
        objectivebus = false;
        // audiosource.PlayOneShot(breek);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mondkapjebreekt : MonoBehaviour
{
    public Image geenmondkap;
    public Image mondkap;

    void Start()
    {
        mondkap.enabled = true;
        geenmondkap.enabled = false;
    }

    
     public void OnTriggerEnter(Collider other)
    {
     if (other.CompareTag("Player")){
        mondkap.enabled = false;
        geenmondkap.enabled = true;
     }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerTutorial : MonoBehaviour
{
    public Animator animator;
    private bool hasRunPopIn = false;
    private bool hasShowed;
    public Image image;
    public static bool gameIsPaused;

    void Start()
    {
        hasShowed = false;
        image.enabled = false;
    }

    void OnTriggerEnter()
    {
        if (!hasShowed)
        {
            image.enabled = true;
            hasShowed = true;
            animator.SetTrigger("PopInTrigger");
            // Debug.Log("werken!!!!!!");
            StartCoroutine(RunPopInThenPause());
            // pause();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && gameIsPaused == true)
        {
            animator.SetTrigger("PopOut");
            // image.enabled = false;
            resume();
        }
    }

    public void resume()
    {
        // image.enabled = false;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void pause()
    {
        image.enabled = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    private IEnumerator RunPopInThenPause()
    {
        if (!hasRunPopIn)
        {
            animator.SetTrigger("PopInTrigger");
            hasRunPopIn = true;
            yield return new WaitForSeconds(0.8f);
            gameIsPaused = true;
        }
        else
        {
            while (gameIsPaused)
            {
                yield return null; // Wacht totdat de game niet meer is gepauzeerd.
            }
        }

        pause();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jonas : MonoBehaviour
{
    public bool jonasinteract = false;
    public void jonas()
    {
        jonasinteract = true;
        StartCoroutine(bloep());
    }
IEnumerator bloep() {
 yield return new WaitForSeconds(0.1f);
jonasinteract = false;
    

}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoest : MonoBehaviour
{

AudioSource audiosource;
public streetevent Streetevent;
public AudioClip scheur;
    void Start()
    {
    audiosource = GetComponent<AudioSource>();
        audiosource.enabled = false;  
    }

    void Update()
    {
       if (Streetevent.hasinteracted == true) {
            audiosource.enabled = true;
            audiosource.PlayOneShot(scheur);
       } 
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gert : MonoBehaviour
{
    public bool gertI = false;

    private float activationDistance = 10.0f;

    bool hasBeenUsed = false;
    public GameObject UIUpdate; // Het gameobject met de Image-component
    public Transform player; // Spelerreferentie

    void Update(){
         if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && hasBeenUsed == false)
        {
            UIUpdate.SetActive(true);
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);

        //  UIUpdate.transform.rotation = Quaternion.Euler(270, player.rotation.eulerAngles.z, 0);
        
        // Reset de schaal naar positieve waarden
        // UIUpdate.transform.localScale = new Vector3(1, 1, 1);
    }
    }
    public void interact()
    {
        gertI = true;
        hasBeenUsed = true;
        StartCoroutine(uit());
    }

                          IEnumerator uit() {
        yield return new WaitForSeconds(0.1f);
        gertI = false;
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class busstation : MonoBehaviour
{
    public Image image;
    AudioSource audiosource;
    public AudioClip cutscene;
    public GameObject UIUpdate; // Het gameobject met de Image-component
    public Transform player; // Spelerreferentie
    private float activationDistance = 10.0f;

    public bool playanimation;
    public mondmaskerdispenser mondmask;

    void Start() {
        image.enabled = false;
        audiosource = GetComponent<AudioSource>();
        UIUpdate.SetActive(false);
    }
public void wachtenopdebus() {
        if (mondmask.heeftmondmask == true){
            image.enabled = true;
            audiosource.PlayOneShot(cutscene);
            StartCoroutine(frank());
            playanimation = true;
            Debug.Log("huts");
        }
    
    
}

       void Update()
    {
    if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && mondmask.hasinteracted == true)
        {
            UIUpdate.SetActive(true);
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
        //  UIUpdate.transform.rotation = Quaternion.Euler(270, player.rotation.eulerAngles.z, 0);
        
        // Reset de schaal naar positieve waarden
        // UIUpdate.transform.localScale = new Vector3(1, 1, 1);
    }
}

    IEnumerator frank() {
        yield return new WaitForSeconds(13f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        

}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class andrewtate : MonoBehaviour
{
    public bool andrewI = false;

    private float activationDistance = 5.0f;
    public GameObject UIUpdate; // Het gameobject met de Image-component
    public Transform player; // Spelerreferentie

    void Update(){
         if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance)
        {
            UIUpdate.SetActive(true);
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
        //  UIUpdate.transform.rotation = Quaternion.Euler(270, player.rotation.eulerAngles.z, 0);
        
        // Reset de schaal naar positieve waarden
        // UIUpdate.transform.localScale = new Vector3(1, 1, 1);
    }
    }
    void interact()
    {
        andrewI = true;
        StartCoroutine(uit());
    }

                          IEnumerator uit() {
        yield return new WaitForSeconds(0.1f);
        andrewI = false;
    

}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telefoonscript : MonoBehaviour
{
    AudioSource audiosource;
    public koffiemok koffie;

    void Start()
    {
    audiosource = GetComponent<AudioSource>();
        audiosource.enabled = false;
    }

    void Update()
    {
        if (koffie.telefoonfrank == true) {
            audiosource.enabled = true;
            } else {
            audiosource.enabled = false;
            }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platenspeler : MonoBehaviour
{
    AudioSource audiosource;
    public AudioClip dikkebeat;
    public GameObject UIUpdate; // Het gameobject met de Image-component
    private float activationDistance = 5.0f;
    public Transform player; // Spelerreferentie

    public GameObject muziekparticles;
    public bool musicon = false;

    private bool isPlaying = false; // Flag to track if audio is already playing

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        UIUpdate.SetActive(false);
        muziekparticles.SetActive(false);
    }

    public void beat()
    {
        if (!isPlaying)
        {
            audiosource.time = 0f;
            audiosource.PlayOneShot(dikkebeat);
            isPlaying = true; // Set the flag to indicate audio is playing
            muziekparticles.SetActive(true);
            musicon = true;
        }
    }

    // Add this method to reset the flag when the audio is finished
    void Update()
    {
        if (!audiosource.isPlaying)
        {
            isPlaying = false;
            muziekparticles.SetActive(false);
        }

                if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && musicon == false)
        {
            UIUpdate.SetActive(true);
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
        //  UIUpdate.transform.rotation = Quaternion.Euler(270, player.rotation.eulerAngles.z, 0);
        
        // Reset de schaal naar positieve waarden
        // UIUpdate.transform.localScale = new Vector3(1, 1, 1);
    }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextsceneappartment : MonoBehaviour
{

void OnTriggerEnter () {
SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class maskerscript : MonoBehaviour
{
    public GameObject UIUpdate; // Het gameobject met de Image-component
    public koffiemok koffiemok;
    public bool maskeraan = false;
    private float activationDistance = 5.0f;
    public Transform player; // Spelerreferentie
    public bool pickedup;

    public Image profielfotoMondmasker;

    public GameObject MondkapjePicca;

    void Start() {
        GetComponent<BoxCollider>().enabled = false;
        UIUpdate.SetActive(false);
        MondkapjePicca.SetActive(true);
    profielfotoMondmasker.enabled = true;
    }
public void Maskeroppakken() {
        
GetComponent<MeshRenderer>().enabled = false;
    GetComponent<BoxCollider>().enabled = false;
        maskeraan = true;
        Debug.Log("functiewerkt");
    MondkapjePicca.SetActive(false);
}

void Update() {
    // if (koffiemok.hasBeenUsed == true) {
    //     GetComponent<BoxCollider>().enabled = true;
    //     UIUpdate.SetActive(true);

    // }

        if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && koffiemok.hasBeenUsed == true && maskeraan == false)
        {
            UIUpdate.SetActive(true);
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
        //  UIUpdate.transform.rotation = Quaternion.Euler(270, player.rotation.eulerAngles.z, 0);
        
        // Reset de schaal naar positieve waarden
        // UIUpdate.transform.localScale = new Vector3(1, 1, 1);
    }
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class koffiezetapparaat : MonoBehaviour
{

 public GameObject UIUpdate; // Het gameobject met de Image-component
  public Transform player; // Spelerreferentie
    public bool koffieklaar;
    private bool hasBeenUsed = false;
    public AudioClip koffiegeluid;
    Animator animator;
    AudioSource audiosource;

    private float activationDistance = 5.0f;
    void Start()
    {
    animator = GetComponent<Animator>();
    audiosource = GetComponent<AudioSource>();
    UIUpdate.SetActive(false);
    }

       void Update()
    {
    if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && hasBeenUsed == false)
        {
            UIUpdate.SetActive(true);
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
        //  UIUpdate.transform.rotation = Quaternion.Euler(270, player.rotation.eulerAngles.z, 0);
        
        // Reset de schaal naar positieve waarden
        // UIUpdate.transform.localScale = new Vector3(1, 1, 1);
    }
}

    public void koffieZetten()
    {
        if (hasBeenUsed == false)
        {
            animator.SetTrigger("koffie");
            audiosource.PlayOneShot(koffiegeluid);
            StartCoroutine(koffie());
            hasBeenUsed = true;
            UIUpdate.SetActive(false);
        }
    }

    IEnumerator koffie() {
        yield return new WaitForSeconds(5);
        koffieklaar = true;

}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koffiemokverschijn : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public koffiezetapparaat koffie;
    void Start()
    {
                // Get the MeshRenderer component attached to this GameObject
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;

        // Check if we found the MeshRenderer
        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer component not found on this object.");
        }
    }

    // Update is called once per frame
    void Update()
    {
             if (koffie.koffieklaar == true && meshRenderer != null)
        {
            meshRenderer.enabled = true;
        }   
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koffiemokverdwijn : MonoBehaviour
{
    private MeshRenderer meshRenderer; // Reference to the MeshRenderer component

    public koffiezetapparaat koffie;

    void Start()
    {
        // Get the MeshRenderer component attached to this GameObject
        meshRenderer = GetComponent<MeshRenderer>();

        // Check if we found the MeshRenderer
        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer component not found on this object.");
        }
    }

    void Update()
    {
        if (koffie.koffieklaar == true && meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class koffiemok : MonoBehaviour
{

     public GameObject UIUpdate; // Het gameobject met de Image-component
  public Transform player; // Spelerreferentie
    Animator animator;
    AudioSource audiosource;
    BoxCollider boxcollider;
    public AudioClip drinken;
    public koffiezetapparaat koffie;
    public Image telefoon;
    public TextMeshProUGUI text;
    public bool telefoonfrank;

    public bool hasBeenUsed = false;

    private float activationDistance = 5.0f;


    void Start()
    {
    animator = GetComponent<Animator>();
    audiosource = GetComponent<AudioSource>();
        boxcollider = GetComponent<BoxCollider>();
        boxcollider.enabled = false;
        telefoon.enabled = false;
        telefoonfrank = false;
        text.enabled = false;
        UIUpdate.SetActive(false);

    }

    void Update() {
        if (koffie.koffieklaar == true) {
            boxcollider.enabled = true;
        }


    if (player != null && koffie.koffieklaar == true)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && hasBeenUsed == false)
        {
            UIUpdate.SetActive(true);
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
        //  UIUpdate.transform.rotation = Quaternion.Euler(270, player.rotation.eulerAngles.z, 0);
        
        // Reset de schaal naar positieve waarden
        // UIUpdate.transform.localScale = new Vector3(1, 1, 1);
    }

    }

            public void koffiedrinken() {
        if (hasBeenUsed == false)
        {
            animator.SetTrigger("drinken");
            audiosource.PlayOneShot(drinken);
            StartCoroutine(phone());
            hasBeenUsed = true;
        }
            }

                IEnumerator phone() {
        yield return new WaitForSeconds(5);
        telefoon.enabled = true;
        telefoonfrank = true;
        text.enabled = true;
        text.text = "Press F to awnser your phone!";
    

}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class hoofddeur : MonoBehaviour
{
    Animator animator;
    AudioSource audiosource;
    public deurklink deurklink;
    public bool isopen;
    private bool isCoroutineRunning = false;
    public TextMeshProUGUI text;
    // public speler player;
    public maskerscript spelerbool;

    public gitaar gitaarbool;
    public AudioClip deuropen;
    public AudioClip deurdicht;
    public AudioClip deuropslot;

    void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        isopen = false;
        text.enabled = false;
    }

    void Update()
    {
        if (deurklink.isActivated == true && spelerbool.maskeraan == false)
        {
            if (!isCoroutineRunning) // Check if the coroutine is already running
            {
                text.enabled = true;
                text.text = "You can't leave the apartment yet!";
                Debug.Log("hoi");
                StartCoroutine(tekst());
                audiosource.PlayOneShot(deuropslot);
            }
        }

        if (spelerbool.maskeraan == true) {
            Debug.Log("maskeraan");

        }

        if (gitaarbool.pickedupguitar == true) {
            Debug.Log("sax");
            
        }

        if (isopen == false && deurklink.isActivated == true && !isCoroutineRunning && spelerbool.maskeraan == true && gitaarbool.pickedupguitar == true)
        {
            StartCoroutine(isopen1());
            Debug.Log("huts");
        }

        if (isopen == true && deurklink.isActivated == true && !isCoroutineRunning && spelerbool.maskeraan == true)
        {
            StartCoroutine(isdicht());
        }
    }

    IEnumerator tekst()
    {
        isCoroutineRunning = true; // Set the flag to indicate the coroutine is running
        yield return new WaitForSeconds(3);
        text.enabled = false;
        isCoroutineRunning = false; // Reset the flag when the coroutine is done
    }

    IEnumerator isopen1()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("open");
        isopen = true;
        Debug.Log("open");
        isCoroutineRunning = false;
        audiosource.PlayOneShot(deuropen);
    }

    IEnumerator isdicht()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("sluit");
        isopen = false;
        Debug.Log("Sluiten");
        isCoroutineRunning = false;
        audiosource.PlayOneShot(deurdicht);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gitaar : MonoBehaviour
{
        public GameObject UIUpdate; // Het gameobject met de Image-component
    public koffiemok koffiemok;
    private float activationDistance = 5.0f;
    public Transform player; // Spelerreferentie
    public bool pickedupguitar;

    void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
        UIUpdate.SetActive(false); 
    }

public void Gitaaropakken() {   
GetComponent<MeshRenderer>().enabled = false;
    GetComponent<BoxCollider>().enabled = false;
        pickedupguitar = true;
        Debug.Log("hoi");

}
    void Update()
    {
            // if (koffiemok.hasBeenUsed == true) {
    //     GetComponent<BoxCollider>().enabled = true;
    //     UIUpdate.SetActive(true);

    // }

        if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && koffiemok.hasBeenUsed == true && pickedupguitar == false)
        {
            UIUpdate.SetActive(true);
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
        //  UIUpdate.transform.rotation = Quaternion.Euler(270, player.rotation.eulerAngles.z, 0);
        
        // Reset de schaal naar positieve waarden
        // UIUpdate.transform.localScale = new Vector3(1, 1, 1);
    }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deurklink : MonoBehaviour
{
    private float activationDistance = 5.0f;
    public GameObject UIUpdate; // Het gameobject met de Image-component
    public Transform player; // Spelerreferentie
    Animator animator;
    AudioSource audiosource;
    public bool isActivated;



    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        isActivated = false;
        UIUpdate.SetActive(false);
    }

   void Update()
{
    if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance)
        {
            UIUpdate.SetActive(true);
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
        //  UIUpdate.transform.rotation = Quaternion.Euler(270, player.rotation.eulerAngles.z, 0);
        
        // Reset de schaal naar positieve waarden
        // UIUpdate.transform.localScale = new Vector3(1, 1, 1);
    }
}

    public void activate()
    {
        animator.SetTrigger("activate");
        isActivated = true;
        StartCoroutine(openDeur());
        
    }

    IEnumerator openDeur()
    {
        yield return new WaitForSeconds(0.1f);
        isActivated = false;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deur : MonoBehaviour
{
    Animator animator;
    AudioSource audiosource;
    public deurklink deurklink;
    public bool isopen;
    private bool isCoroutineRunning = false;
    public AudioClip deuropen;
    public AudioClip deurdicht;

    void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        isopen = false;
    }

    public void Update()
    {
        if (isopen == false && deurklink.isActivated == true && !isCoroutineRunning)
        {
            StartCoroutine(isopen1());
        }

        if (isopen == true && deurklink.isActivated == true && !isCoroutineRunning)
        {
            StartCoroutine(isdicht());
        }
    }

    IEnumerator isopen1()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("open");
        isopen = true;
        Debug.Log("open");
        isCoroutineRunning = false;
        audiosource.PlayOneShot(deuropen);
    }

    IEnumerator isdicht()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("sluit");
        isopen = false;
        Debug.Log("Sluiten");
        isCoroutineRunning = false;
        audiosource.PlayOneShot(deurdicht);
    }
}using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endingcutscene : MonoBehaviour
{
    public frank Frank;
    Animator animator;
    
    void Start()
    {
    animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Frank.beginend == true)
        animator.SetTrigger("start");
        StartCoroutine(end());
    }

        IEnumerator end() {
        yield return new WaitForSeconds(13);
        SceneManager.LoadScene("WilliamAppartement", LoadSceneMode.Single);

}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class williamcutscene1 : MonoBehaviour
{
    public bus bussie;
    Animator animator;

    void Start() {
animator = GetComponent<Animator>();
    }

    void Update() {
        if (bussie.playerd == true) {
            animator.SetTrigger("appear");
            StartCoroutine(dissapear());
            Debug.Log("Appear");
        }
    }

    IEnumerator dissapear() {
        yield return new WaitForSeconds(4);
        animator.SetTrigger("dissapear");
        Debug.Log("Dissapear");
        

}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class williambus2 : MonoBehaviour
{
    Animator animator;

    public bus2 bus;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
      if (bus.william == true) {
        StartCoroutine(appear());
      }
    }

        IEnumerator appear() {
        yield return new WaitForSeconds(3);
        animator.SetTrigger("appear");
        Debug.Log("jot");
        

}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class williambellen : MonoBehaviour
{
    public bool dooropen = false;

    Animator animator;

    void Start()
    {
        StartCoroutine(aanbellen());
        animator = GetComponent<Animator>();
    }

                      IEnumerator aanbellen() {
        yield return new WaitForSeconds(2);
        animator.SetTrigger("ring");
        yield return new WaitForSeconds(10);
        dooropen = true;
        
    

}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchcamerascene2 : MonoBehaviour
{
    public frank Frank;
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Camera3;
    public busstation bus;
    public luchtfilter einde;
    public int Manager;
    public bool eind = false;
    public bool bus2;

    void Start()
    {
        Cam1();
    }

    private bool changed = true;

    void Cam1()
    {
        Camera1.SetActive(true);
        Camera2.SetActive(false);
                Camera3.SetActive(false);
        StartCoroutine(huts());
    }

    void Cam2()
    {
        Camera1.SetActive(false);
        Camera2.SetActive(true);
                Camera3.SetActive(false);
        eind = true;
    }

    void Update()
    {
        if (Frank.beginend == true)
        {
            Debug.Log("Frank.beginend is true"); // Debug statement
            Camera2.SetActive(false);
            Camera3.SetActive(true);
        }
    }

    IEnumerator huts()
    {
        yield return new WaitForSeconds(14);
        Camera1.SetActive(false);
        Camera2.SetActive(true);
        eind = true;
    }
}using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchcamera : MonoBehaviour
{
public GameObject Camera1;
public GameObject Camera2;
        public GameObject Camera3;
public busstation bus;
public int Manager;

        public bool bus2;

        void Start(){
                bus2 = false;
        }



    private bool changed = true;

public void ManageCamera() {
    if(Manager == 0) {
            Cam2();
            Manager = 1;
    } else {
            Cam1();
            Manager = 0;
    }
}

void Update() {
        if (bus.playanimation == true && changed== true)
        {
            GetComponent<Animator>().SetTrigger("change");
            changed = false;

        }
}

void Cam1() {
        Camera1.SetActive(true);
        Camera2.SetActive(false);
}

void Cam2() {
        Camera1.SetActive(false);
        Camera2.SetActive(true);
        StartCoroutine(camera3());
}

                IEnumerator camera3() {
        yield return new WaitForSeconds(5);
                Camera2.SetActive(false);
                Camera3.SetActive(true);
                bus2 = true;

}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDisabler : MonoBehaviour
{

    public bus buss;

    private Canvas canvas; // Reference to the Canvas component.

    private void Start()
    {
        // Get the Canvas component attached to this GameObject.
        canvas = GetComponent<Canvas>();

        // Ensure the Canvas component is not null.
        if (canvas == null)
        {
            Debug.LogError("Canvas component not found on the GameObject.");
        }
    }

    private void Update()
    {
        // Check the playerd boolean and enable/disable the Canvas accordingly.
        canvas.enabled = !buss.playerd;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bus2 : MonoBehaviour
{
    Animator animator;
    public switchcamera playerbool;
    public bool william = false;
    
 
    void Start()
    {
    GetComponent<MeshRenderer>().enabled = false;
    GetComponent<BoxCollider>().enabled = false;
    animator = GetComponent<Animator>();
        
    }


    void Update()
    {
       if (playerbool.bus2 == true) {
       GetComponent<MeshRenderer>().enabled = true;
       GetComponent<BoxCollider>().enabled = true;
       animator.SetTrigger("start2");
            william = true;
       
       

       } 
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bus : MonoBehaviour
{
    public busstation buss;
    Animator animator;
    public bool playerd;
 
    void Start()
    {
    GetComponent<MeshRenderer>().enabled = false;
    GetComponent<BoxCollider>().enabled = false;
    animator = GetComponent<Animator>();
    playerd = false;    
    }


    void Update()
    {
       if (buss.playanimation == true) {
       GetComponent<MeshRenderer>().enabled = true;
       GetComponent<BoxCollider>().enabled = true;
       animator.SetTrigger("startanimatie");
       playerd = true;
        Debug.Log("kok");
       

       } 
    }
}
    private void SetupSpeech(SpeechNode speech)
        {
            if (speech == null)
            {
                EndConversation();
                return;
            }

            m_currentSpeech = speech;

            // Clear current options
            ClearOptions();
            m_currentSelectedIndex = 0;

            // Set sprite
            if (speech.Icon == null)
            {
                NpcIcon.sprite = BlankSprite;
            }
            else
            {
                NpcIcon.sprite = speech.Icon;
            }

            // Set font
            if (speech.TMPFont != null)
            {
                DialogueText.font = speech.TMPFont;
            }
            else
            {
                DialogueText.font = null;
            }

            // Set name
            NameText.text = speech.Name;

            // Set text
            if (string.IsNullOrEmpty(speech.Text))
            {
                if (ScrollText)
                {
                    DialogueText.text = "";
                    m_targetScrollTextCount = 0;
                    DialogueText.maxVisibleCharacters = 0;
                    m_elapsedScrollTime = 0f;
                    m_scrollIndex = 0;
                }
                else
                {
                    DialogueText.text = "";
                    DialogueText.maxVisibleCharacters = 1;
                }
            }
            else
            {
                if (ScrollText)
                {
                    DialogueText.text = speech.Text;
                    m_targetScrollTextCount = speech.Text.Length + 1;
                    DialogueText.maxVisibleCharacters = 0;
                    m_elapsedScrollTime = 0f;
                    m_scrollIndex = 0;
                }
                else
                {
                    DialogueText.text = speech.Text;
                    DialogueText.maxVisibleCharacters = speech.Text.Length;
                }
            }

            // Call the event
            if (speech.Event != null)
                speech.Event.Invoke();

            DoParamAction(speech);

            // Play the audio
           if (speech.Audio != null)
    {
        AudioPlayer.clip = speech.Audio;
        AudioPlayer.volume = speech.Volume;
        AudioPlayer.Play();
    }

    if (ScrollText)
    {
        SetState(eState.ScrollingText);
        // Als audio wordt afgespeeld en tekst aan het scrollen is, roep een methode op om audio te stoppen wanneer de tekst volledig is gescrold
        if (AudioPlayer.isPlaying)
        {
            // Bereken de tijd die nodig is om de tekst volledig te scrollen
            float scrollDuration = CalculateScrollDuration(speech.Text);
            // Roep de StopAudioAfterTextScrollDelayed-methode op met een vertraging gelijk aan de scrolltijd
            Invoke("StopAudioAfterTextScrollDelayed", scrollDuration);
        }
    }
    else
    {
        SetState(eState.TransitioningOptionsOn);
    }
        }

        private void StopAudioAfterTextScrollDelayed()
{
    // Stop de audioweergave
    if (AudioPlayer.isPlaying)
    {
        AudioPlayer.Stop();
    }
}

// Deze functie berekent de tijd die nodig is om de tekst volledig te scrollen
private float CalculateScrollDuration(string text)
{
    // Implementeer je eigen logica om dit te berekenen op basis van de tekstlengte en de scrollsnelheid
    // Voor demonstratiedoeleinden ga ik uit van een constante scrollsnelheid van 10 tekens per seconde.
    float tekensPerSeconde = 25.0f;
    float scrollDuur = text.Length / tekensPerSeconde;
    return scrollDuur;
}
