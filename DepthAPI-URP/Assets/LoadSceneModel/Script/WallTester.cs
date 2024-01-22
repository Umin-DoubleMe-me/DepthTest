using UnityEngine;

public class WallTester : MonoBehaviour
{
    public GameObject AnchorParentObj;
    public OVRSceneManager SceneManager;
    public GameObject TestCube;

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
}
