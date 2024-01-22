using UnityEngine;

public class WallTester : MonoBehaviour
{
    public GameObject AnchorParentObj;
    public OVRSceneManager SceneManager;
    public GameObject TestCube;
    public Transform LeftController;

    private void Start()
    {
        Debug.Log("Umin WallTester : awake");
        SceneManager.SceneModelLoadedSuccessfully += GetAllWall;

    }

    private void GetAllWall()
    {
        var sceneRoom = AnchorParentObj.GetComponentsInChildren<OVRSceneRoom>();

        Debug.Log("Umin SceneRoom Count : " + sceneRoom.Length);

        foreach(var scene in sceneRoom)
        {
            Debug.Log("Umin sceneWall : " + scene.Walls.Length);
            foreach(var wall in scene.Walls)
            {
                var tt = Instantiate(TestCube);
                tt.transform.parent = wall.transform;
                tt.transform.localPosition = Vector3.zero;
                tt.transform.localRotation = Quaternion.identity;
                tt.transform.localScale = Vector3.one;

                Debug.Log("Umin wall size" + wall.transform.localScale);
                Debug.Log("Umin wall pose" + wall.transform.localPosition);
                Debug.Log("Umin wall rot" + wall.transform.localRotation);
            }
        }
    }

    private void Update()
    {
        if(OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))
        {
            Vector3 anchorPosition = LeftController.position;
            Quaternion anchorRotation = LeftController.rotation;
            RaycastHit hit;

            var lineMaxLength = 1000000.0f;

            if (Physics.Raycast(new Ray(anchorPosition, anchorRotation * Vector3.forward), out hit, lineMaxLength))
            {
                GameObject objectHit = hit.transform.gameObject;
                OVRSemanticClassification classification = objectHit?.GetComponentInParent<OVRSemanticClassification>();

                if (classification != null && classification.Labels?.Count > 0)
                {
                    Debug.Log("Umin Ray Input!");
                    //displayText.text = classification.Labels[0];
                }
            }
        }
    }
}