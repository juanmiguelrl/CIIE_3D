using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool leverOpened = false;

    Animator anim;

    public GameObject controller;

    // NearView()
    float distance;
    float angleView;
    Vector3 direction;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !leverOpened && NearView())
        {
            anim.SetBool("LeverUp", true); // animation
            leverOpened = true;
            controller.GetComponent<LeverSystem>().checkLevers(gameObject, leverOpened);
        }
        else if (Input.GetKeyDown(KeyCode.E) && leverOpened && NearView())
        {
            anim.SetBool("LeverUp", false); // animation
            leverOpened = false;
            controller.GetComponent<LeverSystem>().checkLevers(gameObject, leverOpened);
        }
    }

    bool NearView() // it is true if you near interactive object
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);
        if (angleView < 45f && distance < 2f) return true;
        else return false;
    }
}