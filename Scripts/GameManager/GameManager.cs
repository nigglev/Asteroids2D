using UnityEngine;
using GameLogic;
using GameView;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.ResourceProviders;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    private PlayerInput _input;
    public PlayerInput PlayerInput => _input;
    private CWorld _world;

    private CCamera _camera;

    private Dictionary<AsyncOperationHandle<GameObject>, ulong> _requests;

    private void Awake()
    {
        _requests = new Dictionary<AsyncOperationHandle<GameObject>, ulong>();
        _input = new PlayerInput();
        _input.Enable();
        _camera = new CCamera();
        _world = new CWorld(this, GetCameraViewHalfSizes().x, GetCameraViewHalfSizes().y);
        _world.Init();
        
    }

    // Update is called once per frame
    void Update()
    {   
        _world.Update(Time.deltaTime);
    }

    public void RequestViewObject(EEntityTypes inEntityType, ulong in_entity_id, Vector2 in_spawn_position)
    {
        InstantiationParameters inst_params = new InstantiationParameters(in_spawn_position, Quaternion.identity, null);
        string adress_key = inEntityType.ToString();
        AsyncOperationHandle<GameObject> op_handler = Addressables.InstantiateAsync(adress_key, inst_params);
        if(!op_handler.IsValid())
        {
            Debug.Log($"Operation Handle is not valid; inEntityType: {inEntityType} inEntityID: {in_entity_id}");
            return;
        }
        op_handler.Completed += Op_handler_Completed;
        _requests.Add(op_handler, in_entity_id);
    }

    private void Op_handler_Completed(AsyncOperationHandle<GameObject> obj)
    {
        if(obj.Status == AsyncOperationStatus.Failed || obj.Status == AsyncOperationStatus.None)
        {
            Debug.Log($"AsyncOperationHandle status is {obj.Status}");
            return;
        }

        IViewObject sh = obj.Result.GetComponent<SpaceshipView>();
        _requests.TryGetValue(obj, out ulong id);
        _world.SetViewObjectById(sh, id);
        _requests.Remove(obj);
    }


    private Vector2 GetCameraViewHalfSizes()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return new Vector2(planes[0].distance, planes[2].distance);
    }

}
