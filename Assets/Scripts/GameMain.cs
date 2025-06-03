using System;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public WorldSetting worldSetting;
    public Transform plane;
    LayerMask planeLayerMask;
    private void Awake()
    {
        planeLayerMask = LayerMask.NameToLayer("Plane");
        RenderManagerController.Init();
        LogicManagerController.Init();
    }

    private void Start()
    {
        var instance = FlowField.GetInstance(); 
        instance.GenerateWorld(worldSetting.worldSize, worldSetting.cellSize);
        plane.localScale = new Vector3(worldSetting.worldSize.x * worldSetting.cellSize.x, worldSetting.worldSize.y * worldSetting.cellSize.z, 0);
    }

    private void Update()
    {
        RenderManagerController.Update(Time.deltaTime);
        LogicManagerController.Update(Time.deltaTime);

        ProcessUserInput();
    }

    private void OnDestroy()
    {
        RenderManagerController.Close();
        LogicManagerController.Close();
    }

    void ProcessUserInput()
    {
        bool isLeftMouseButtonDown = Input.GetMouseButtonDown(0);
        bool isRightMouseButtonDown = Input.GetMouseButtonDown(1);
        if (isLeftMouseButtonDown || isRightMouseButtonDown)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))//, planeLayerMask))
            {
                var hitPosition = new Vector3(hit.point.x, 0, hit.point.z);
                if (isLeftMouseButtonDown)
                {
                    InputManager.Instance.OnLeftMouseClick(hitPosition);
                }
                else if (isRightMouseButtonDown)
                {
                    InputManager.Instance.OnRightMouseClick(hitPosition);
                }
            }
            else
            {
                Debug.Log("射线未击中任何物体");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InputManager.Instance.OnSpaceKeyDown();
        }
    }
}
