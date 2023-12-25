using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;

public class SendButtonHandler : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro m_phoneNumber;

    public void SavePhoneNumber()
    {
        PersistentDataManager._instance.SaveData(m_phoneNumber.text);
    }
}
