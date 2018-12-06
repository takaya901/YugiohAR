using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOnTapped : MonoBehaviour
{
    [SerializeField] ParticleSystem _breath;
    [SerializeField] ParticleSystem _outEffect;
    
    static readonly float BREATH_SPEED = 1.0f;
    GameObject _breathDest;
    bool _isTargetSelected;
    
    public void OnClick()
    {
        Debug.Log("tapped");
    }

    //青眼がタッチされたらブレス生成，その後対象がタッチされたらブレス放出
    //http://nn-hokuson.hatenablog.com/entry/2018/04/04/212402#ARオブジェクトをタッチしたい
    void Update () 
    {
        //対象が設定されていたらブレス放出
        if (_isTargetSelected) {
            var step = BREATH_SPEED * Time.deltaTime;
            _breath.transform.position = Vector3.MoveTowards(_breath.transform.position, _breathDest.transform.position, step);

            if (_breath.transform.position == _breathDest.transform.position) {
                Instantiate(_outEffect, _breathDest.transform.position, Quaternion.identity);
                Destroy(_breathDest);
                Destroy(_breath);
            }
        }

        if (Input.touchCount <= 0) return;    //以下，タッチ時処理
        
        var touchedObject = GetTouchedObject();
        
        //ブレス生成後に対象がタッチされたら目標設定
        if (_breath.isPlaying && touchedObject != gameObject) {
            _isTargetSelected = true;
            _breathDest = touchedObject;
        }
        //青眼がタッチされたらブレス生成
        else if(!_breath.isPlaying && touchedObject == gameObject) {
            _breath.gameObject.SetActive(true);
        }

        #region Editorデバッグ用
//        if (Input.GetMouseButtonDown(0) && !_breath.gameObject.activeSelf) 
//        {
//            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            var hit = new RaycastHit();
//            if (Physics.Raycast(ray, out hit, 1000)) {
//                _breath.gameObject.SetActive(true);
//            }
//        }
        #endregion
    }

    GameObject GetTouchedObject()
    {
        var touch = Input.GetTouch(0);
//        if (touch.phase != TouchPhase.Began) return null;
        var ray = Camera.main.ScreenPointToRay(touch.position);
        var hit = new RaycastHit();
        
        return Physics.Raycast(ray, out hit, 1000) ? hit.collider.gameObject : null;
    }
}
