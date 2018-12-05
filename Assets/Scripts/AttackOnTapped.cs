using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOnTapped : MonoBehaviour
{
    [SerializeField] ParticleSystem _breath;
    float _breathSpeed = 1.0f;
    Vector3 _breathDest;
    bool _isBreath;
    
    public void OnClick()
    {
        Debug.Log("tapped");
    }

    //http://nn-hokuson.hatenablog.com/entry/2018/04/04/212402#ARオブジェクトをタッチしたい
    void Update () 
    {
        if (_isBreath) {
            var step = _breathSpeed * Time.deltaTime;
            _breath.transform.position = Vector3.MoveTowards(_breath.transform.position, _breathDest, step);
        }
        
        if (Input.touchCount > 0 && _breath.gameObject.activeSelf) 
        {
            var t = Input.GetTouch(0);
            var ray = Camera.main.ScreenPointToRay(t.position);
            var hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 1000)) {
                _isBreath = true;
                _breathDest = hit.collider.gameObject.transform.position;
//                _breath.transform.Translate((dest - _breath.transform.position) * 0.1f);
//                _breath.transform.Translate(dest.right.normalized * 0.1f);
//                _breath.gameObject.SetActive(false);
            }
        }
        
        if (Input.touchCount > 0 && !_breath.gameObject.activeSelf) 
        {
            var t = Input.GetTouch(0);
            var ray = Camera.main.ScreenPointToRay(t.position);
            var hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 1000)) {
                _breath.gameObject.SetActive(true);
            }
        }
        
        if (Input.GetMouseButtonDown(0) && !_breath.gameObject.activeSelf) 
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 1000)) {
                _breath.gameObject.SetActive(true);
            }
        }
    }
    
    //スマホ向け そのオブジェクトがタッチされていたらtrue（マルチタップ対応）
    bool OnTouchDown() {
        // タッチされているとき
        if( 0 < Input.touchCount){
            // タッチされている指の数だけ処理
            for(int i = 0; i < Input.touchCount; i++){
                // タッチ情報をコピー
                Touch t = Input.GetTouch(i);
                // タッチしたときかどうか
                if(t.phase == TouchPhase.Began ){
                    //タッチした位置からRayを飛ばす
                    Ray ray = Camera.main.ScreenPointToRay(t.position);
                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(ray, out hit)){
                        //Rayを飛ばしてあたったオブジェクトが自分自身だったら
                        if (hit.collider.gameObject == this.gameObject){
                            return true;
                        }
                    }
                }
            }
        }
        return false; //タッチされてなかったらfalse
    }

//    bool TouchJudge()
//    {
//        int touchCount = Input.touchCount;
//
//        if (touchCount > 0)
//        {
//            Debug.Log("touch");
//            for (int i = 0; i < touchCount; i++)
//            {
//                Touch touch = Input.GetTouch(i);
//
//                Ray ray = _arCamera.ScreenPointToRay(touch.position);
//                RaycastHit hit = new RaycastHit();
//
//                Debug.DrawRay(ray.origin, ray.direction * 100);
//
//                if (Physics.Raycast(ray, out hit))
//                {
//                    Debug.Log("hit");
//                    if (hit.collider.gameObject == gameObject)
//                    {
//                        return true;
//                    }
//                }
//            }
//        }
//
//        return false;
//    }
}
