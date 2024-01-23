using UnityEngine;

public class WallTester : MonoBehaviour
{
    public GameObject AnchorParentObj;
    public OVRSceneManager SceneManager;
    public GameObject TestCube;
    public Transform LeftController;

    private const float _disOffset = 0.1f;

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
                tt.transform.localRotation = Quaternion.identity;
                tt.transform.Rotate(0, 180, 0);
                tt.transform.localPosition = new Vector3(0, 0, -tt.transform.lossyScale.z / 2 + _disOffset);
                tt.transform.localScale = Vector3.one;

                Debug.Log("Umin wall forward" + wall.transform.forward * -1);
            }
        }
    }
}