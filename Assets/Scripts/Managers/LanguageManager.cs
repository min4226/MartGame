using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

public class LanguageManager : ManagerBase
{
    protected override IEnumerator OnConnected(GameManager newManager)
    {
        yield return LocalizationSettings.InitializationOperation;
        ChangeLanguage("ko");
    }

    protected override void OnDisconnected()
    {
        
    }

    public void ChangeLanguage(string languageCode)
    {
        Debug.Log(languageCode);
        // localization에 있는 언어 들고 오기
        var localLanguage = LocalizationSettings.AvailableLocales.GetLocale(languageCode);
        if (localLanguage != null)
        {
            LocalizationSettings.SelectedLocale = localLanguage;
        }
    }

    public void OnChangeEnglish()
    { 
        ChangeLanguage("en");
    }

    public void OnChangeJapanese()
    {
        ChangeLanguage("ja");
    }

    public void OnChangeThai()
    {
        ChangeLanguage("th-TH");
    }

    public void OnChangeKorean()
    {
        ChangeLanguage("ko");
    }
}
