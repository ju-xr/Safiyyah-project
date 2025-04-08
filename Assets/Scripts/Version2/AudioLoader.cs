using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;
using UnityEditor;

public class AudioLoader : MonoBehaviour
{
    string AudioFolderName = "Default"; //Participant Name/Tag

    [Header("Auto Read Audios")]
    public List<AudioClip> audioPlayList = new List<AudioClip>();
    public List<AudioClip> backupClips = new List<AudioClip>();

    [Header("Participant Folder")]
    public TMP_Dropdown participantFolder_Dropdown;    

    [Header("Audio Dropdowns")]
    public List<TMP_Dropdown> audioDropdown = new List<TMP_Dropdown>();
    //[SerializeField]
    //private TMP_Dropdown[] emptyAudioDropdown;
    protected List<string> m_DropOptions = new List<string>();

    [MenuItem("Tools/Audio/生成 folder_list.txt")]
    static void GenerateAudioFolderList()
    {
        string audioRootPath = "Assets/Resources/Audios";
        string outputPath = Path.Combine("Assets/Resources/", "folder_list.txt");

        if (!Directory.Exists(audioRootPath))
        {
            Debug.LogError("找不到路径：" + audioRootPath);
            return;
        }

        string[] subDirs = Directory.GetDirectories(audioRootPath);
        using (StreamWriter writer = new StreamWriter(outputPath, false))
        {
            foreach (string dir in subDirs)
            {
                string folderName = Path.GetFileName(dir);
                writer.WriteLine(folderName);
            }
        }

        AssetDatabase.Refresh(); // 刷新资源
        Debug.Log("✅ 生成完毕: " + outputPath);
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateAudioFolderList();
        PopulateParticipantDropdown();
    }

    void PopulateParticipantDropdown()
    {
        // 🔥 加载 Resources/folder_list.txt
        TextAsset folderListAsset = Resources.Load<TextAsset>("folder_list");

        if (folderListAsset == null)
        {
            Debug.LogWarning("⚠️ folder_list.txt not found in Resources/");
            return;
        }

        // ✂️ 拆分每一行（移除空行和多余空格）
        string[] folderNames = folderListAsset.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        List<string> cleanFolderNames = new List<string>();

        foreach (string name in folderNames)
        {
            string trimmed = name.Trim();
            if (!string.IsNullOrEmpty(trimmed))
            {
                cleanFolderNames.Add(trimmed);
            }
        }

        // 🧼 清空旧选项并添加新选项
        participantFolder_Dropdown.ClearOptions();
        participantFolder_Dropdown.AddOptions(cleanFolderNames);

        // ✅ 可选：注册监听，加载选中项的音频
        participantFolder_Dropdown.onValueChanged.AddListener(index =>
        {
            string selectedFolder = cleanFolderNames[index];
            Debug.Log("选中了: " + selectedFolder);
            LoadAudio(selectedFolder);
        });

        // ✅ 自动加载第一个选项
        if (cleanFolderNames.Count > 0)
        {
            LoadAudio(cleanFolderNames[0]);
        }
    }


    void LoadAudio(string folderName)
    {
        //AudioFolderName = participantFolder_Dropdown.captionText.text;
        //AudioPath = Application.dataPath + "/Resources/Audios";
        var audioFile = Resources.LoadAll("Audios/" + folderName, typeof(AudioClip));

        audioPlayList.Clear();
        backupClips.Clear();
        m_DropOptions.Clear();

        var optData = new List<Dropdown.OptionData>();

        //Loop Audio File in Resources Folder with the ascending order
        for (int i = 0; i < audioFile.Length; i++)
        {
            //Debug.Log(audioClip[i].name);

            //add folder audio into playable audio clips
            audioPlayList.Add((AudioClip)audioFile[i]);
            backupClips.Add((AudioClip)audioFile[i]);                        

            m_DropOptions.Add(audioPlayList[i].name);
        }

        //Choose audio function
        for (int i = 0; i < audioDropdown.Count; i++)
        {
            audioDropdown[i].ClearOptions();
            audioDropdown[i].AddOptions(m_DropOptions);

            audioDropdown[i].value = i;

            //foreach (TMP_Dropdown dp in emptyAudioDropdown)
            //{
            //    dp.ClearOptions();
            //    dp.interactable = false;
            //}

            audioDropdown[i].interactable = true;
            audioDropdown[i].RefreshShownValue();
            
            // 设置 onValueChanged 事件监听器
            int index = i; // 创建一个局部变量来捕获当前的索引值
            audioDropdown[i].onValueChanged.RemoveAllListeners(); // 移除所有现有的监听器
            audioDropdown[i].onValueChanged.AddListener((value) => OnAudioDropdownValueChanged(audioDropdown[index], value));
        }
    }

    //处理下拉菜单值变化的方法
    private void OnAudioDropdownValueChanged(TMP_Dropdown dropdown, int value)
    {
        // 调用 PressAudioDropdown 方法
        PressAudioDropdown(dropdown);

        // 可以在这里添加额外的逻辑，例如播放音频预览等
        Debug.Log($"Audio dropdown {dropdown.tag} changed to value {value}: {backupClips[value].name}");
    }

    public virtual void PressAudioDropdown(TMP_Dropdown currentDropdown)
    {
        int i = int.Parse(currentDropdown.tag);
        audioPlayList[i] = backupClips[currentDropdown.value];
    }
}
