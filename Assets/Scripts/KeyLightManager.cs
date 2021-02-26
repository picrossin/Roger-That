using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLightManager : MonoBehaviour
{
    public enum LightColor
    {
        Red,
        Blue,
        Yellow,
        Green
    };

    [SerializeField] private Material offMat;
    [SerializeField] private Material onMat;
    [SerializeField] private Material correctLight;
    [SerializeField] private Material incorrectLight;

    private LightColor[] _colorOrder = new[]
    {
        LightColor.Green, LightColor.Red, LightColor.Blue, LightColor.Green, LightColor.Green, LightColor.Yellow,
        LightColor.Yellow, LightColor.Blue
    };
    private List<LightColor> _inputLights = new List<LightColor>();

    private List<MeshRenderer> _childrenMats = new List<MeshRenderer>();
    private int _terminalCount = 0;
    private bool _on;
    private int _lightIndex = 0;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            _childrenMats.Add(child.GetComponent<MeshRenderer>());
        }
    }

    public void AddTerminal()
    {
        _terminalCount++;
        if (_terminalCount >= 4)
        {
            _on = true;
            SetAllLights(onMat);
        }
    }

    public void GuessLight(string guess)
    {
        if (_on)
        {
            LightColor colorGuess = LightColor.Red;
        
            switch (guess)
            {
                case ("Red"):
                    colorGuess = LightColor.Red;
                    break;
                case ("Blue"):
                    colorGuess = LightColor.Blue;
                    break;
                case ("Yellow"):
                    colorGuess = LightColor.Yellow;
                    break;
                case ("Green"):
                    colorGuess = LightColor.Green;
                    break;
            }
            
            _inputLights.Add(colorGuess);

            _childrenMats[_lightIndex].material = 
                _colorOrder[_lightIndex] == colorGuess ? correctLight : incorrectLight;

            if (_lightIndex == _colorOrder.Length - 1)
            {
                _on = false;
                _lightIndex = 0;
                CheckLights();
            }
            else
            {
                _lightIndex++;
            }
        }
        
    }

    private void CheckLights()
    {
        bool correct = true;
        for (int i = 0; i < _colorOrder.Length; i++)
        {
            if (_inputLights[i] != _colorOrder[i])
            {
                correct = false;
            }
        }

        if (correct)
        {
            StartCoroutine(LightCompletedAnimation());
        }
        else
        {
            _inputLights = new List<LightColor>();
            StartCoroutine(LightFailedAnimation());
        }
        
    }

    private void SetAllLights(Material mat)
    {
        foreach (MeshRenderer renderer in _childrenMats)
        {
            renderer.material = mat;
        }
    }

    private IEnumerator LightFailedAnimation()
    {
        for (int i = 0; i < 3; i++)
        {
            SetAllLights(offMat);
            yield return new WaitForSeconds(0.3f);
            SetAllLights(incorrectLight);
            yield return new WaitForSeconds(0.3f);
        }
        
        SetAllLights(onMat);
        _on = true;
    }

    private IEnumerator LightCompletedAnimation()
    {
        for (int i = 0; i < 3; i++)
        {
            SetAllLights(offMat);
            yield return new WaitForSeconds(0.3f);
            SetAllLights(correctLight);
            yield return new WaitForSeconds(0.3f);
        }
        
        SetAllLights(correctLight);
    }
}