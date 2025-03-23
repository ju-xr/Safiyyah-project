# Safiyyah VR Project V2 / Safiyyah VR项目 V2版本

[English](#english) | [中文](#chinese)

## English

### Project Overview
This is a VR experiment platform developed with Unity, designed for studying the effects of audio and video stimuli on user behavior. The project now includes two versions:
1. Original version (Amsterdam/New York scenes)
2. Germany version (new implementation)

### Germany Version Features
- 20 fixed-order 360-degree videos (1 minute each)
- Customizable audio files for each participant
- Head rotation data collection
- LSL signal integration for video start/end markers
- 23 customizable UI text prompts
- Embodiment process with 360-degree image

### Project Structure
```
Assets/
├── Scripts/
│   ├── Germany/              # Germany version scripts
│   │   ├── Germany_UI_Function.cs
│   │   ├── Germany_clipcontrol.cs
│   │   └── Germany_DataCollection.cs
│   └── Original/             # Original version scripts
│       ├── clipcontrol.cs
│       └── UI_Function.cs
├── Resources/
│   ├── Audios/              # Participant-specific audio files
│   │   └── [ParticipantID]/ # Audio folder named by participant ID
│   └── Videos/
│       └── Germany/         # 20 fixed-order videos
├── Scenes/
│   ├── Germany_Scene        # New scene for Germany version
│   └── [Original_Scenes]    # Original experiment scenes
└── ParticipantData/         # Data collection folder
```

### Setup Instructions
1. Place 20 videos in Resources/Videos/Germany/
2. Create participant folders in Resources/Audios/
3. Place corresponding audio files in participant folders
4. Open Germany_Scene
5. Configure 23 UI text prompts and timing
6. Set up LSL markers (code placeholders provided)

### Data Collection
- Timestamp
- Video name
- Audio name
- Head rotation (X, Y, Z)
- LSL markers for video start/end

### Requirements
- Unity 2022.3 LTS
- VR headset support
- Windows 10/11

---

## Chinese

### 项目概述
这是一个使用Unity开发的VR实验平台，用于研究音频和视频刺激对用户行为的影响。项目现包含两个版本：
1. 原始版本（阿姆斯特丹/纽约场景）
2. 德国版本（新实现）

### 德国版本特点
- 20个固定顺序的360度视频（每个1分钟）
- 可自定义的参与者音频文件
- 头部旋转数据收集
- 视频开始/结束LSL信号标记
- 23段可自定义UI提示文字
- 使用360度图片的适应过程

### 项目结构
```
Assets/
├── Scripts/
│   ├── Germany/              # 德国版本脚本
│   │   ├── Germany_UI_Function.cs
│   │   ├── Germany_clipcontrol.cs
│   │   └── Germany_DataCollection.cs
│   └── Original/             # 原始版本脚本
│       ├── clipcontrol.cs
│       └── UI_Function.cs
├── Resources/
│   ├── Audios/              # 参与者特定的音频文件
│   │   └── [ParticipantID]/ # 以参与者ID命名的音频文件夹
│   └── Videos/
│       └── Germany/         # 20个固定顺序的视频
├── Scenes/
│   ├── Germany_Scene        # 德国版本新场景
│   └── [Original_Scenes]    # 原始实验场景
└── ParticipantData/         # 数据收集文件夹
```

### 设置说明
1. 将20个视频放入 Resources/Videos/Germany/
2. 在 Resources/Audios/ 中创建参与者文件夹
3. 将对应的音频文件放入参与者文件夹
4. 打开 Germany_Scene
5. 配置23段UI提示文字及其显示时间
6. 设置LSL标记（已提供代码占位符）

### 数据收集
- 时间戳
- 视频名称
- 音频名称
- 头部旋转（X, Y, Z）
- 视频开始/结束的LSL标记

### 系统要求
- Unity 2022.3 LTS
- VR头显支持
- Windows 10/11

---

## Version History / 版本历史

### V2 (Current / 当前)
- Added Germany version / 添加德国版本
- Improved data collection / 改进数据收集
- Enhanced experiment flow / 增强实验流程
- Separated versions / 版本分离

### V1 (Original / 原始)
- Amsterdam/New York scenes / 阿姆斯特丹/纽约场景
- Basic data collection / 基础数据收集
- Gaze tracking / 注视追踪