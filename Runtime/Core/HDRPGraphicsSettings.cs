using Studio23.SS2.Settings.Video.Data;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace Studio23.SS2.Settings.Video.HDRP.Core
{
    public class HDRPGraphicsSettings : GraphicsConfigurationBase
    {
        private Bloom _bloom;
        private ColorAdjustments _colorAdjustments;
        private ScreenSpaceAmbientOcclusion _ambientOcclusion;



        

        public override void Initialize(Volume currentVolume)
        {
            CurrentVolumeProfile = currentVolume.profile;
            CurrentVolumeProfile.TryGet(typeof(Bloom), out _bloom);
            CurrentVolumeProfile.TryGet(typeof(ColorAdjustments), out _colorAdjustments);
            CurrentVolumeProfile.TryGet(out _ambientOcclusion);

            SetRenderScale(_renderScale);
            SetBloomState(_bloomEnabled);
            SetAmbientOcclusionState(_ambientOcclusion);
        }

        public override void SetBloomState(bool state)
        {
            if (_bloom == null) return;
            _bloom.active = state;
            _bloomEnabled = state;
        }

        public override void SetAmbientOcclusionState(bool state)
        {
            if (_ambientOcclusion == null) return;
            _ambientOcclusion.active = state;
            _ambientOccusionEnabled = state;
        }

        public override void SetBrightness(float brightnessValue)
        {
            //if (_colorAdjustments == null) return;
            //_colorAdjustments.postExposure.value = brightnessValue;
        }

        public override void SetRenderScale(float scaleValue)
        {
            DynamicResolutionHandler.SetDynamicResScaler(
                () => scaleValue,
                DynamicResScalePolicyType.ReturnsPercentage
            );

            _renderScale = scaleValue;
        }

        public override void UpdatePipelineRenderAsset()
        {

        }
    }
}

