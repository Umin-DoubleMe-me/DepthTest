using UnityEngine;

public class WallTester : MonoBehaviour
{
    public GameObject AnchorParentObj;
    public OVRSceneManager SceneManager;
    public GameObject TestCube;
    public GameObject Mask;
    public Transform LeftController;

    public bool TargetSizeWallSmall = false; //벽면의 짧은면을 타겟할지 긴 면을 타겟할지.

    private const float _disOffset = 0.01f;
    private const float _wallPlaneOffset = 5;

    private GameObject _testCube;
    private GameObject _testMask;

    private int _wallCount = 0;
    private void Start()
    {
        SceneManager.SceneModelLoadedSuccessfully += GetAllWall;
    }

    private void GetAllWall()
    {
        var sceneRoom = AnchorParentObj.GetComponentsInChildren<OVRSceneRoom>();

        foreach(var scene in sceneRoom)
        {
            _wallCount++;
            if(_wallCount >= scene.Walls.Length)
                _wallCount = 0;

            OVRScenePlane wall = scene.Walls[_wallCount];

            var targetLength = TargetSizeWallSmall
                ? wall.Width < wall.Height ? wall.Width : wall.Height
                : wall.Width > wall.Height ? wall.Width : wall.Height;

            if(_testCube == null)
                _testCube = Instantiate(TestCube);
            _testCube.transform.parent = wall.transform;
            _testCube.transform.localRotation = Quaternion.identity;
            _testCube.transform.Rotate(0, 180, 0);
            _testCube.transform.localScale = Vector3.one * targetLength;
            _testCube.transform.localPosition = new Vector3(0, 0, -_testCube.transform.lossyScale.z / 2 - _disOffset);

            if(_testMask == null)
                _testMask = Instantiate(Mask);
            _testMask.transform.parent = wall.transform;
            _testMask.transform.localRotation = Quaternion.identity;
            Vector3 maskSize = TargetSizeWallSmall
                ? new Vector3(targetLength * _wallPlaneOffset, targetLength * _wallPlaneOffset, 1)
                : new Vector3(wall.Width * _wallPlaneOffset, wall.Height * _wallPlaneOffset, 1);
            _testMask.transform.localScale = maskSize;
            _testMask.transform.localPosition = new Vector3(0, 0, _disOffset);

        }
    }

    private void Update()
    {
        if(OVRInput.GetUp(OVRInput.Button.One))
        {
            GetAllWall();
        }
    }
}