using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
 
public class gancho2 : MonoBehaviour
{
    [SerializeField] float hookshotSpeed = 1.0f;
    [SerializeField] float hookshotSpeedMultiplier = 1.0f;
 //this is the coold down hookready switch / bool
 [SerializeField] bool HookReady=true;
 // this stops being to hook shot when hook not in place
 [SerializeField] bool HookInPlace=false;
 
 [SerializeField] AudioSource FireHookAudioSource;
 [SerializeField] AudioSource RatchetHookAudioSource;
 
 [SerializeField] KeyCode EscapeKey;
 [SerializeField] KeyCode FireHookKey;
 [SerializeField] KeyCode PullHookKey;
 [SerializeField] KeyCode ShowFPSKey;
//  [SerializeField] KeyCode Escape1;
//  [SerializeField] KeyCode Escape1;
 
// tag functionality
    private GameObject FoundObject;
    private string RaycastReturnString;
    [SerializeField] private LayerMask layerMask1;
 
 
    [SerializeField] private bool useRope = true;
    // transform or game object?
    [SerializeField] private Transform ropeTransform;
 
    //private float hookshotSize;
    private float ropeSize;
 
    [SerializeField] private float HookClimbForward = 2.0f;
    [SerializeField] private float HookClimbUp = 5.0f;
    [SerializeField] private Vector3 HookClimbOffset = new Vector3(0f, 50.0f, 0f);
 
    private float HookDistance;
  public float HookMaxDistance = 1000.0f;
    public float HookMinDistance = 2.0f;
    public float HookShotFinishDistance = 1.0f;
    [SerializeField] GameObject HookOrigin;
    [SerializeField] private Transform debugHitPointTransform;
  [SerializeField] private Transform hookshotTransform;
 
    [SerializeField]  float hookshotSpeedMin = .1f;
    [SerializeField]    float hookshotSpeedMax = 1f;
 
    [SerializeField] LineRenderer LineRenderer1;
 
    private Vector3 hookshotPosition;
    public Vector3 hookOffset = new Vector3(0f, 25.0f, 0f);
    private bool isHookshotdrag = false;
    private bool isHookShotThrown = false;
 
    public bool showfps = false;
    public bool ispaused = false;
 
    float deltaTime = 0.0f;
 
    public float timer1 = 60.0f;
  public float timer1Remaining = 60.0f;
  public bool timer1IsRunning = false;
  public string timer1Text;
 
    public float timer2 = 600.0f;
  public float timer2Remaining = 600.0f;
  public bool timer2IsRunning = false;
  public string timer2Text;
 
    public string debugText;
 
    //private Camera _camera;
 
    [SerializeField] GameObject firstPersonCamera;
    [SerializeField] GameObject InvectorController;
    [SerializeField] GameObject InvectorCamera;
 
    //Get Camera Listeners
    AudioListener cameraOneAudioLis;
    AudioListener cameraTwoAudioLis;
 
    Camera cameraOne;
    Camera cameraTwo;
 
 
//  LineRenderer LineRendererRope1;
 
    private void HookshotShoot()
    {
        if (HookReady)
        {
            // move the debug so it does not interfere with Shot
                debugHitPointTransform.transform.position = Vector3.zero;
            //  FireHookAudioSource.Play();
 
         if (Physics.Raycast(HookOrigin.transform.position, HookOrigin.transform.forward, out RaycastHit raycastHit))
         {
            //check tag  if (gameobject.tag == "hookable")
 
            RaycastReturnString = raycastHit.collider.gameObject.name;
            FoundObject = GameObject.Find(RaycastReturnString);
 
            debugText = ("Hit! " + FoundObject.name);
            UnityEngine.Debug.Log("Hit! "+ FoundObject.name);
 
            if (FoundObject.tag == "Hookable")
            {
                                //check max Distance  HookDistance <= public float HookMaxDistance = 1000.0f;
                                //if (Vector3.Distance(InvectorController.transform.position, hookshotTransform.position) < HookShotFinishDistance)
                                if ((Vector3.Distance(HookOrigin.transform.position, raycastHit.point) <= HookMaxDistance) && (Vector3.Distance(HookOrigin.transform.position, raycastHit.point) > HookMinDistance))
                                {
                                    //only play sound if it hits maybe have another sound for hit fail.
                                    FireHookAudioSource.Play();
 
                                    LineRenderer1.enabled = true;
 
                // Hit something
                  debugHitPointTransform.position = raycastHit.point;
                  hookshotPosition = raycastHit.point;
                                hookshotPosition = hookshotPosition + hookOffset;
 
                //  hookshotSize = 0f;
                  hookshotTransform.gameObject.SetActive(true);
                  hookshotTransform.localScale = Vector3.zero;
                                LineRenderer1.transform.position = HookOrigin.transform.position;
                                //InvectorController.transform.position
               // state = State.HookshotThrown;
                                isHookShotThrown = true;
                                    HookInPlace = true;
                                    // use rope vs use line Renderer
                                    HookReady =false;
 
                                    if(useRope)
                                    {
                                        LineRenderer1.enabled = false;
 
                                    }
                                    if (!useRope)
                                    {
                                 hookshotTransform.position = hookshotPosition;
                                 LineRenderer1.SetPosition(index: 0,HookOrigin.transform.position );
                                 LineRenderer1.SetPosition(index: 1,hookshotTransform.transform.position );
 
                                    }
 
                                     DrawRope();
                            }
                    }
            }
        }
    }
 
    private void HookshotMove()
    {
            RatchetHookAudioSource.Play();
 
        hookshotTransform.LookAt(hookshotPosition);
 
            Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;
 
            //float hookshotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookshotPosition), hookshotSpeedMin, hookshotSpeedMax);
            //float hookshotSpeedMultiplier = 1f;
 
 
 
 
    //    hookshotTransform.position = debugHitPointTransform.position;
    //  hookshotPosition = hookshotTransform.position + hookOffset;
        hookshotTransform.position = hookshotPosition;
        InvectorController.transform.position = Vector3.MoveTowards(InvectorController.transform.position, hookshotTransform.position, hookshotSpeed);
 
        if (Vector3.Distance(InvectorController.transform.position, hookshotTransform.position) < HookShotFinishDistance)
        {
            //InvectorController
            //How to stop the physics?
            StopHookShot();
            //stop being able to right click when hook not in place
            HookInPlace = false;
            HookClimb();
        }
    }
 
    public void StopHookShot()
    {
        isHookshotdrag =false;
        isHookShotThrown = false;
        //LineRenderer1.SetActive(false);
    //  LineRenderer1.GetComponent<LineRenderer>().SetActive(true);
    //  LineRenderer1.SetActive = false;
        LineRenderer1.enabled = false;
    }
 
// beware ; after functions
    public void HookClimb()
    {
        InvectorController.transform.Translate(Vector3.forward * Time.deltaTime * HookClimbForward);
        InvectorController.transform.Translate(Vector3.up * Time.deltaTime * HookClimbUp);
        InvectorController.transform.position = InvectorController.transform.position + HookClimbOffset;
    }
 
// make sure start has capital S
    void Start()
    {
        Cursor.visible = false;
        Screen.lockCursor = true;
    //Cursor.lockMode = Cursor.Locked;
        Cursor.lockState = CursorLockMode.Locked;
    //SetCursorPosition(x : int,y : int)
        timer1IsRunning = true;
        timer1Remaining = timer1;
 
        timer2IsRunning = true;
        timer2Remaining = timer2;
 
        debugText = ("Testing debugText");
        UnityEngine.Debug.Log("initialising test of debug log");
 
//  InvectorCamera.GetComponent<Camera>().SetActive(true);
    //firstPersonCamera.GetComponent<Camera>().SetActive(false);
 
        cameraOne= firstPersonCamera.GetComponent<Camera>();
        cameraTwo= InvectorCamera.GetComponent<Camera>();
 
        cameraOneAudioLis = firstPersonCamera.GetComponent<AudioListener>();
    cameraTwoAudioLis = InvectorCamera.GetComponent<AudioListener>();
 
    //  FireHookAudioSource= Audio1Fire.GetComponent<AudioSource>();
        //RatchetHookAudioSource = Audio2Ratchet.GetComponent<AudioSource>();
 
 
 
        //  cameraOne.gameObject.SetActive (false);
        //  cameraTwo.gameObject.SetActive (true);
    }
 
    void Update()
    {
    //seem to have to put in update to work :(
        Cursor.visible= false;
        Screen.lockCursor = true;
 
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
 
        //setting rotation of hook origin to camera
        HookOrigin.transform.rotation = InvectorCamera.transform.rotation ;
        firstPersonCamera.transform.rotation = InvectorCamera.transform.rotation ;
 
//      if (Input.GetKey(KeyCode.Escape))
            if (Input.GetKey(EscapeKey))
                {
                #if UNITY_EDITOR
                    if (EditorApplication.isPlaying)
                    {
                    EditorApplication.isPlaying = false;
                    }
                #else
                Application.Quit();
                #endif
                }
 
                if (Input.GetKey (KeyCode.P))
                {
                ispaused = !ispaused;
                }
 
        //if (Input.GetMouseButtonDown(0))
        //if (FireHook)
        if (Input.GetKey(FireHookKey))
                {
          //    UnityEngine.Debug.Log("Pressed primary button.");
                    //  debugText = ("Pressed primary button");
 
                        HookshotShoot();
                }
 
     if (Input.GetMouseButtonDown(1))
         // prefer using mouse button down
        //  if (Input.GetKey(PullHookKey))
                {
        //    UnityEngine.Debug.Log("Pressed secondary button.");
                    //  debugText = ("Pressed secondary button.");
                        isHookshotdrag = !isHookshotdrag;
                        if (!isHookshotdrag)
                        {
                            // switch off hook in place on right click
                          HookInPlace=false;
                            LineRenderer1.enabled = false;
                        }
                }
 
        if (isHookshotdrag)
                {
               //   ActivateInvector();
                // debugHitPointTransform.transform.Collider.enabled = true;
 
                 //InvectorController.isHookShot.SetBool(true);
                 // InvectorController.setHookShotTrue();
                //  InvectorController.HookShot();
                // LineRenderer1.GetComponent<LineRenderer>().SetActive(true);
                    LineRenderer1.enabled = true;
                    if (HookInPlace)
                     {
                         HookshotMove();
                     }
                }
 
                if (!isHookshotdrag)
                {
                    InvectorController.SetActive(true);
                //  debugHitPointTransform.transform.Collider.enabled = false;
                //  debugHitPointTransform.transform.position = Vector3.zero;
                }
 
 
                if (Input.GetMouseButtonDown(2))
        {
                    UnityEngine.Debug.Log("Pressed middle click.");
                    debugText = ("Pressed middle click.");
                }
 
                if (Input.GetKey("1"))
                {
            //      ActivateFirstPerson();
            cameraOne.gameObject.SetActive (false);
            cameraOne.gameObject.SetActive (true);
            cameraTwo.gameObject.SetActive (true);
 
 
                }
 
                if (Input.GetKey("2"))
                {
                //activate third person
                    cameraTwo.gameObject.SetActive (false);
                    cameraTwo.gameObject.SetActive (true);
                    cameraOne.gameObject.SetActive (true);
                }
 
                if (Input.GetKey("3"))
                {
                    //ActivateInvector();
                }
 
                //if (Input.GetKey (KeyCode.F))
                if (Input.GetKey (ShowFPSKey))
                {
               showfps = !showfps;
                }
 
                if (timer1IsRunning)
                {
            if (timer1Remaining > 0)
            {
                timer1Remaining -= Time.deltaTime;
                UpdateTimer1(timer1Remaining);
            }
 
            else
            {
                                HookReady = true;
                UnityEngine.Debug.Log("Hook is ready!");
                //string text1 = string.Format ("{0:0.0} ms ({1:0.} fps)", msec, fps);
                debugText = ("Hook is ready!");
 
                //timeRemaining = 0;
                //timerIsRunning = false;
                timer1Remaining = timer1;
 
             }
        }
 
                if (timer2IsRunning)
        {
            if (timer2Remaining > 0)
            {
                timer2Remaining -= Time.deltaTime;
                UpdateTimer2(timer2Remaining);
            }
 
            else
            {
 
                UnityEngine.Debug.Log("Time has run out!");
 
                debugText = ("Time has run out!");
 
                timer2Remaining = timer2;
 
 
                if (ispaused)
                {
                #if UNITY_EDITOR
                if (EditorApplication.isPlaying)
                    {
                    //toggle ispaused here to allow unpausing in editor menu system
                    ispaused = !ispaused;
 
                    EditorApplication.isPaused = true;
 
                    //not sure how to get back to game without clicking in editor now.
                    }
 
                #else
                //Application.Pause();
                #endif
                //Time.timeScale = 0;
                }
 
                else if (ispaused==false)
                {
                #if UNITY_EDITOR
                if (EditorApplication.isPlaying)
                {
                    EditorApplication.isPaused = false;
                }
 
                #else
                //Application.Pause();
                #endif
 
                //Time.timeScale = 1;
                }
 
 
            }
 
 
        }
    }
 
void LateUpdate()
    {
        DrawRope();
    }
 
void    DrawRope()
    {
        // if state = hookshot travelling
    //  if (Input.GetMouseButtonDown(1))
        if (isHookshotdrag)
        {
            if (!useRope)
            {
             isHookShotThrown = false;
         LineRenderer1.SetPosition(index: 0,HookOrigin.transform.position );
             LineRenderer1.SetPosition(index: 1,hookshotTransform.transform.position );
            }
 
            if (useRope)
            {
                    LineRenderer1.enabled = false;
                 //LineRenderer1.transform.position = HookOrigin.transform.position;
                 //ropeTransform.transform.position(HookOrigin.transform.position);
                 ropeTransform.transform.position = HookOrigin.transform.position;
                 ropeTransform.LookAt(hookshotTransform.transform.position);
                 ropeSize = Vector3.Distance(HookOrigin.transform.position, hookshotTransform.transform.position);
                // ropeTransform.localScale = new Vector3(1,1,ropeSize);
            }
        }
 
        if(isHookShotThrown)
        {
            if (!useRope)
            {
                LineRenderer1.SetPosition(index: 0,HookOrigin.transform.position );
                LineRenderer1.SetPosition(index: 1,hookshotTransform.transform.position );
            }
 
            if (useRope)
            {
                LineRenderer1.enabled = false;
                //ropeSize=10.0f;
             //ropeTransform.position(HookOrigin.transform.position);
             ropeTransform.transform.position = HookOrigin.transform.position;
             ropeTransform.LookAt(hookshotTransform.transform.position);
            // ropeSize = Vector3.Distance(HookOrigin.transform.position, hookshotTransform.transform.position);
             //ropeTransform.localScale = new Vector3(1,1,ropeSize);
            }
 
        }
    }
 
    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
 
        if (showfps)
        {
            GUIStyle style = new GUIStyle ();
 
            Rect rect1 = new Rect (0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 8 / 100;
            style.normal.textColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text1 = string.Format ("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label (rect1, text1, style);
 
            Rect rect2 = new Rect (0, 50, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 8 / 100;
            style.normal.textColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
 
            //string text2 = string.Format(timeText); ??
            //string text2 = string.Format ("Text 2 debug stuff here");
 
            GUI.Label (rect2, timer1Text, style);
 
            Rect rect3 = new Rect (0, 100, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 8 / 100;
            style.normal.textColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
 
            GUI.Label (rect3, timer2Text, style);
 
            Rect rect4 = new Rect (0, 200, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 8 / 100;
            style.normal.textColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
 
            GUI.Label (rect4, debugText, style);
        }
 
    }
 
    void UpdateTimer1(float timeToDisplay1)
    {
        timeToDisplay1 += 1;
 
        float minutes1 = Mathf.FloorToInt(timeToDisplay1 / 60);
        float seconds1 = Mathf.FloorToInt(timeToDisplay1 % 60);
 
        timer1Text = string.Format ("Cooldown Timer1: {0:00}:{1:00}", minutes1, seconds1);
    }
 
    void UpdateTimer2(float timeToDisplay2)
    {
        timeToDisplay2 += 1;
 
        float minutes2 = Mathf.FloorToInt(timeToDisplay2 / 60);
        float seconds2 = Mathf.FloorToInt(timeToDisplay2 % 60);
 
        timer2Text = string.Format ("Timer2: {0:00}:{1:00}", minutes2, seconds2);
    }
 
}