using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DrawGraffitiVR : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 5;

    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeight;
    
    private RaycastHit _touch;
    private WallVR _wall;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;

    void Start(){
        _renderer = _tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(_renderer.material.color, _penSize * _penSize).ToArray();
        _tipHeight = _tip.localScale.y;
    }

    void Update(){
        Draw();
    } 

    private void Draw(){
        if(Physics.Raycast(_tip.position, transform.up, out _touch, _tipHeight)){
            if(_touch.transform.CompareTag("graffitiWall")){
                if(_wall == null){
                    _wall = _touch.transform.GetComponent<WallVR>();
                }
                
                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);
                var x = (int)(_touchPos.x * _wall.textureSize.x - (_penSize/2));
                var y = (int)(_touchPos.y * _wall.textureSize.y - (_penSize/2));

                if(y < 0 || y > _wall.textureSize.y || x < 0 || x > _wall.textureSize.x){
                    return;
                }

                if(_touchedLastFrame){
                    _wall.texture.SetPixels(x,y,_penSize, _penSize, _colors);

                    for(float f = 0.01f; f < 1.0f; f += 0.03f){
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _wall.texture.SetPixels(x, y, _penSize, _penSize, _colors);
                    }
                    transform.rotation = _lastTouchRot;

                    _wall.texture.Apply();
                }

                _lastTouchPos = new Vector2(x,y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            
            }
            
        }
        _wall = null; 
        _touchedLastFrame = false;
    }
}
