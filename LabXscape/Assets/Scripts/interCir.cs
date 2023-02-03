using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interCir : MonoBehaviour
{
    [SerializeField] public GameObject panel;
    private bool triggerPanel = false;
    private bool inCircle = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inCircle && Input.GetKeyDown(KeyCode.F)) {
            PanelAction();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            inCircle = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            inCircle = false;
        }
    }

    private void PanelAction() {
        if (!triggerPanel) {
            triggerPanel = true;
            panel.SetActive(true);
        }
        else {
            triggerPanel = false;
            panel.SetActive(false);
        }
    }
}
