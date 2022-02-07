using System.Collections;
using System.Collections.Generic;
using Panzers.Utility;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;


namespace Panzers.Manager
{
    public class MenuSceneAudioManager : BaseAudioManager
    {
        [Inject] private IUtilityAssetLoader _assetLoader;
        [SerializeField] private AssetReference _musicAsset;
        [SerializeField] private AudioSource _musicSource;


        private void StartMusic()
        {
            _assetLoader.LoadAssetByAssetReferenceAsync<AudioClip>(_musicAsset, OnLoadMusicFinished);
        }

        private void OnLoadMusicFinished(AsyncOperationHandle<AudioClip> clip)
        {
            _musicSource.clip = clip.Result;
            _musicSource.Play();
        }


        protected override void Start()
        {
            base.Start();
            StartMusic();
        }
    }
}