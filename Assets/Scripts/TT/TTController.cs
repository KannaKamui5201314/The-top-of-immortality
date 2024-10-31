using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TTSDK;
using UnityEngine;


public class TTController : MonoBehaviour
{
    
    private void Awake()
    {
        TT.InitSDK(OnTTContainerInit);
    }

    private void OnTTContainerInit(int code, ContainerEnv env)
    {
        if (code == 0)
        {
            //���ʵ�����
            //TT.SetRealNameAuthenticationCallback(onRealNameAuthenticationSuccess);
            //����Ƿ���Ҫ���µ�¼
            TT.CheckSession(OnCheckSessionSuccess, OnCheckSessionFailed);
            TT.CheckScene(TTSideBar.SceneEnum.SideBar, CheckSceneSuccess, CheckSceneComplete, CheckSceneError);
        }
    }

    private void CheckSceneSuccess(bool obj)
    {
        
    }

    private void CheckSceneComplete()
    {
        
    }

    private void CheckSceneError(int arg1, string arg2)
    {
        
    }

    private void OnCheckSessionSuccess()
    {
        //��ȡ��Ϣ
        TT.GetUserInfo(false, true, OnGetUserInfoSuccess, OnGetUserInfoFailed);
    }
    private void OnCheckSessionFailed(string errMsg)
    {
        //��¼
        TT.Login(OnLoginSuccess, OnLoginFailed);
    }

    private void OnLoginSuccess(string code, string anonymousCode, bool islogin)
    {
        if (islogin)
        {
            TT.GetUserInfo(false, true, OnGetUserInfoSuccess, OnGetUserInfoFailed);
        }
    }

    private void OnLoginFailed(string errMsg)
    {
        TT.Login(OnLoginSuccess, OnLoginFailed);
    }



    private void OnGetUserInfoSuccess(ref TTUserInfo scUserInfo)
    {
        Debug.Log("scUserInfo=" + scUserInfo);
    }

    private void OnGetUserInfoFailed(string errMsg)
    {
        
    }

    private void OnAuthenticateRealNameSuccess(string errMsg)
    {
        
    }
    private void OnAuthenticateRealNameFailed(string errMsg)
    {
        //TT.AuthenticateRealName(OnAuthenticateRealNameSuccess, OnAuthenticateRealNameFailed);
    }

    private void OnRealNameAuthenticationSuccess()
    {
       //ʵ����֤��ɺ�Ĳ���
    }


    //�����ǳ�ʼ������¼��ʵ����֤


}
