using UnityEngine;
using UnityEngine.VR;

public class SpawnObject : MonoBehaviour {
    //public GameObject mnemonic; // populate the array in the Inspector
    public bool placeMnemonicMode;

    private Vector3 mnemonicPositionVector;

    private Camera cam;
    private Vector3 positionHit;

    private bool wasTouching = false;

    private string currentMnemonic = "Broadleaf_Desktop";
    private bool spawnMode = false;

    private void Start() {
        placeMnemonicMode = true;
        cam = GetComponent<Camera>();
    }

    public Vector3 getMnemonicPosition() {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
            print("I'm looking at " + hit.transform.name);
            mnemonicPositionVector = hit.point;
        } else {
            mnemonicPositionVector = new Vector3(-55.481f, 15.91f, -97.91f);
        }
        return mnemonicPositionVector;
    }

    public bool inRange() {
        return true;
    }

    public void placeMnemonic() {
        Vector3 forward = InputTracking.GetLocalRotation(VRNode.CenterEye) * cam.transform.forward;
        Vector3 spawnPos = cam.transform.position + forward * 2;

        GameObject mnemonic = generateMnemonic();
        GameObject.Instantiate(mnemonic, spawnPos, Quaternion.identity);
        UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController instance = new UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController();
        instance.Move(new Vector2(-1, 0), 4f);
        // GameObject.Instantiate(mnemonic, getMnemonicPosition(), Quaternion.identity);
    }

    // Selects a mnemonic from a folder of various assets
    public GameObject generateMnemonic() {
        return Resources.Load(currentMnemonic) as GameObject;
    }

    private void clicked() {
        Debug.Log("Clicked");
        var filename = "screenshot.png";
        var path = "/Snapshots/" + filename;
        Application.CaptureScreenshot(path);
    }

    public void selectMnemonic(string newMnemonic) {
        currentMnemonic = newMnemonic;
        spawnMode = true;
    }

    public void Update() {
        if (Input.touchCount > 0) {
            if (!wasTouching) {
                wasTouching = true;
                if (spawnMode) {
                    Debug.Log("Touched");
                    placeMnemonic();
                    clicked();
                    spawnMode = false;
                }
            }
        } else {
            wasTouching = false;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("1");
            if (placeMnemonicMode) {
                Debug.Log("2");
                if (spawnMode) {
                    Debug.Log("space pressed");
                    spawnMode = false;
                    clicked();
                    placeMnemonic();
                }
            }
            //placeMnemonicMode = false; //todo: (hardcoded)
        }
    }
}

/*
 if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForceAtPosition(ray.direction * pokeForce, hit.point);
        }
     */

//    Debug.Log("here: " + x.ToString() + y.ToString() + z.ToString());
//todo: spawn
//todo: write H He shit in the mnemonic
//todo: add rotate
//potential problem: Assets/Plugin/My Plugin.meta
//todo: classroom environment, question queue for students
//todo: change Start Forum UI
//todo: issue "mars"still works