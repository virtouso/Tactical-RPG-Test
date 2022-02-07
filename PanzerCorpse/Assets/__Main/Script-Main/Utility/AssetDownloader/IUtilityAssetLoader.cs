using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Panzers.Utility
{
    public interface IUtilityAssetLoader
    {
        void LoadAssetByStringAddressAsync<T>(string address, Action<AsyncOperationHandle<T>> callback);

        void LoadAssetByAssetReferenceAsync<T>(AssetReference assetReference, Action<AsyncOperationHandle<T>> callback);

        T LoadAssetSync<T>(string address);
    }
}