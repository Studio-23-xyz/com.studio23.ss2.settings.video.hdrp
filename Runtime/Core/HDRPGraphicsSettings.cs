using Studio23.SS2.Settings.Video.Data;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;


public class HDRPGraphicsSettings : GraphicsConfigurationBase
{
    private Bloom _bloom;
    private ColorAdjustments _colorAdjustments;
    private ScreenSpaceAmbientOcclusion _ambientOcclusion;
    private float _currentRenderScale;

    public override void Initialize(Volume currentVolume)
    {
        CurrentVolumeProfile = currentVolume.profile;
        CurrentVolumeProfile.TryGet(typeof(Bloom), out _bloom);
        CurrentVolumeProfile.TryGet(typeof(ColorAdjustments), out _colorAdjustments);
        GetAmbientOcclusion();
    }

    public override void SetBloomState(bool state)
    {
        if (_bloom == null) return;
        _bloom.active = state;
    }

    public override void SetAmbientOcclusionState(bool state)
    {
        if (_ambientOcclusion == null) return;
        _ambientOcclusion.active = state;
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

        _currentRenderScale = scaleValue;
    }

    public override void UpdatePipelineRenderAsset()
    {
        float renderScaleValue = _currentRenderScale;
        bool isAmbientOcclusion = _ambientOcclusion.active;
        UpdateAmbientOcclusion(isAmbientOcclusion);
        SetRenderScale(renderScaleValue);
    }

    private void UpdateAmbientOcclusion(bool state)
    {
        GetAmbientOcclusion();
        SetAmbientOcclusionState(state);
    }

    private void GetAmbientOcclusion()
    {
        if (CurrentVolumeProfile != null && CurrentVolumeProfile.TryGet(out _ambientOcclusion))
        {
            Debug.Log("Ambient Occlusion found in Volume Profile.");
        }
        else
        {
            Debug.LogError("No Ambient Occlusion override found in the Volume Profile.");
        }
    }
}