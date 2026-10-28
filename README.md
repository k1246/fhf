# 水浸超声扫查系统 README


## 1. 简介

本程序是 Windows Forms 上位机。它分别通过厂家接口连接 **ACS 运动控制器**和
**OEMPAmini 超声采集设备**：ACS 负责驱动机构、报告运动状态；OEMPA 负责激发、
接收和数字化超声回波；`Forms/FrmTestTool.cs` 安排两者的操作顺序，接收数据并显示或保存。

```操作顺序
操作员
  ↓
主窗口 / FrmTestTool 扫查页
  ├─ 读取 config.ini 和路径 TXT
  ├─ 调用 ACS 接口 → 运动控制器 → 电机和机构
  ├─ 调用 OEMPA 驱动 → 超声设备 ↔ 阵列探头
  ├─ 接收超声回调 → A 扫 / B 扫 / C 扫
  └─ 保存 MAT、日志和业务记录

```

## 2. 关键代码和运行依赖

- `FlatUI_TestPlatform.csproj`、`FlatUI_TestPlatform.sln`

  当前项目定义与解决方案；入口类是 `FlatUI_TestPlatform.Program`，目标框架是 .NET Framework
  4.8.1。

- `Program.cs`

  `Main` 启动、重复运行检查、授权和 OEMPA DLL 检查；同文件还含一个设备 setup 用的 `IniFile`
  辅助类。

- `FormMain.cs`

  主窗口、菜单跳转、从运行目录读取 INI、退出清理。

- `Forms/FrmTestTool.cs`

  扫查主界面；ACS 控制、OEMPA 配置、采集回调、A/B/C 扫、路径运动和 MAT 保存都集中于此。

- `Forms/*.cs`、`Forms/FrmTestUserControl/`

  配方、历史记录、用户、登录、配置、调试、维护、工具以及界面控件。对应 `*.Designer.cs`
  主要是 WinForms 自动生成的控件布局和事件绑定。

- `PubCls/`

  全局配置、INI/日志/Excel/Word/SQLite、PLC、打印与辅助函数。

- `bin/DriverCSharp/`、`bin/Debug/`、`bin/x64/Debug/`

  OEMPA 包装源码与运行 DLL；C# 调用包装层，后者再调用厂家原生驱动。


## 3. 程序运行调用链

```调用链
Program.Main
  → 防止重复启动、给第三方控件授权、检查 csDriverOEMPA.dll
  → Application.Run(new FormMain())
  → FormMain_Load
      → 设置运行时配置路径，读取轴号、方向、功能开关等
      → 根据登录设置进入主界面或等待用户操作
  → iconButton1_Click → OpenChildForm(new FrmTestTool(null))
  → FrmTestTool 构造函数 / FrmTestTool_Load
      → 建立界面和扫描管理器，读取运动参数
      → 选择 OEMPAmini，创建设备对象，InitDialog 注册数据回调
  → 等待按钮、定时器、后台任务和设备回调
```

### 3.1 主要页面

`FormMain` 的菜单可打开 `FrmTestTool`、`FrmRecipe`、`FrmReview`、`FrmOption`、
`FrmMaintain`、`FrmTools`、`FrmUser`、`FrmConfig`、`FrmDebug`、`FrmLogin` 和示例页面。
菜单显隐由 `config.ini` 的功能开关控制。配方、用户、历史结果及导出属于辅助业务；超声原始
FMC 的主流程在 `FrmTestTool`。

## 4. 两台设备分别怎样通信

### 4.1 ACS

```ACS
按钮 / 路径任务
  ↓
ACS.SPiiPlusNET.Api
  ↓ 通过以太网发送命令
ACS 控制器 → 电机和机构

ACS 控制器
  ↓ 返回位置、速度和状态
tmrMonitor_Tick → 更新界面
```

- 连接

  `hslBtnConnect_Click` 调 `_ACS.OpenCommEthernet("10.0.0.100", 701)`；成功后启动约 500 ms
  一次的监控定时器。

- 监控

  `tmrMonitor_Tick` 读取位置 `GetFPosition`、速度 `FVEL`、故障 `FAULT`、电机状态、
  控制器程序状态及输出 8/9。界面相对位置由反馈位置减配置偏移得到。

- 运动

  先设速度 `SetVelocity`，再用 `ToPoint` 发绝对或相对移动；需要等待的流程另调
  `WaitMotionEnd`。

- 回零

  `HX_GoHomimg` 运行控制器内的缓冲区 5/6/7，并查询结束状态。应以控制器**实际装载**的程序为准。

- 断开

  `hslBtnDisConnect_Click` / `_ACSFormClosed` 关闭通信，停止状态更新。

运动按钮中的“清零”调用 `SetFPosition(axis, 0)`，是设置控制器反馈坐标，
并不是沿机械轴运动回原点。界面“急停”调用软件 API `KillAll` 和 `StopBuffer`；

### 4.2 OEMPA

```OEMPA
参数设置 / 激发按钮
  ↓
csDriverOEMPA 包装层 → 厂家原生驱动
  ↓ 通过 TCP 或 UDP 通信
OEMPAmini ↔ 阵列探头
  ↓ 回传 A 扫 / C 扫 / IO
已注册的 C# 回调
  ↓
FMC 矩阵 / C 扫点 → 实时图像或 MAT 文件
```

`FrmTestTool_Load` 默认选择 OEMPAmini；`comboBoxOEMType_SelectedIndexChanged` 创建设备对象；
`InitDialog` 注册 A 扫、C 扫、IO 和信息回调。操作员勾选连接框后，
`checkBoxConnect_CheckedChanged` 用 `SetAddress`、`SetConnectionState(Connected, true)`
建立连接。默认界面地址是 `192.168.1.11`；驱动报告 TCP 时使用端口 `5001`，否则使用
`udp://192.168.1.11`。实际通信方式应读取运行时 `GetCommunication()`

配置设备时，`buttonFMC_Click` 将 FMC、周期、增益、采样起点/范围、
发射/接收阵元等参数写入设备，最后 `Flush`；`comboBoxSynchronisation_SelectedIndexChanged`
配触发模式，`UpdateResolution` 和 `buttonWriteStep_Click` 配编码器分辨率、触发步距。
`EnablePulser(true)` 是打开激发，不等于立刻同步返回完整数据；收到设备数据后才进入 A 扫或 C
扫回调。若采用外部或编码器触发，还需要真实触发信号。

## 5. 逐点采集FMC完整流程

这是 `checkBoxMotionCapture_CheckedChanged` 实现的路线。先分别连接 ACS、OEMPA，
并写入合适的运动、FMC 和触发参数；再提供路径 TXT。

### 5.1 路径文件输入

`hslBtnLoadLine_Click` 解析每行六个逗号分隔数字，并存到 `_currentMotionMatrix`：

```eg
X, Y, Z, SpeedX, SpeedY, SpeedZ
10,0,0,30,50,50
```

这一行中的前三列在自动流程中最终用于 `ToPoint(ACSC_AMF_RELATIVE, ...)`，
所以当前实现把它们作为**相对移动量**使用。连续两行都写 `X=10`，理想情况下 X 会累计移动两次，
最终加 20。当前解析器先按文件行数分配矩阵，只写入恰好六列的行；
格式错误的行可能留下全零数据。文件没有正式表头、单位、坐标系或绝对/相对声明；
速度物理单位也需要对照 ACS 用户单位确认。

### 5.2 每一行实际发生什么

```采集流程
勾选“运动采集”
  ↓
重新加载路径，读取轴号、偏移和间隔
  ↓
对每行路径重复：
  ① ACS 依次移动 Y → Z → X
  ② 等待运动结束，再等待设定间隔
  ③ 分配当前点的 FMC 矩阵
  ④ OEMPA 打开激发
  ⑤ 收到 A 扫回调，每次写入矩阵的一列
  ⑥ 达到预定列数，复制数据，关闭激发
  ↓
所有路径行完成 → 保存 MAT
```

实际调用链是：

```采集流程
checkBoxMotionCapture_CheckedChanged
  → hslBtnLoadLine.PerformClick() 重新载入路径
  → 每行计算 targetX/Y/Z = 路径值 + 配置偏移
  → MoveAxisAsync(Y) → MoveAxisAsync(Z) → MoveAxisAsync(X)
      → SetVelocity → ToPoint(RELATIVE) → WaitMotionEnd
  → 等待 scanInterval
  → FMC_lengthwrite_logic 分配 [采样点, totalElements²] 矩阵
  → EnablePulser(true)
  → OEMPA 驱动多次调用 AcquisitionAscan_0x00010103
      → ProcessData 写 FMC_DataMatrix 的当前列
  → _currentElementIndex 达到 totalElements²
  → 复制当前矩阵，记录本行位置/速度，关闭激发
  → 所有行结束后 SaveallScandataToMat
```


## 6. 超声数据处理

### 6.1 A 扫与 FMC

OEMPA 原生驱动收到数据后，经包装层调用注册的 `AcquisitionAscan_0x00010103`。回调读
`acqInfo.dEncoder[0/1]`，再调用 `ProcessData` 读取采样缓冲区。`csSubStreamAscan_0x0103` 的
`dataCount` 是采样点数，`dataSize` 是每点字节数，`sign` 表示有无符号，另有周期和序列字段。
当前代码在有符号分支将缓冲区按 `short*` 读取，依回调到达顺序写入
`FMC_DataMatrix[采样点索引, 当前列索引]`；无符号分支里的矩阵写入语句目前被注释。列号没有按
`cycle`、`lFMCElementIndex` 或序列号校验。

`StartUiTimer`、`DrawAscanRealtime` 从矩阵中抽取某个发射/接收组合，刷新 A 扫曲线；
`GetBscanDataFromFMC`、`ComputeBScanBitmapInBackground`、`CreateBScanBitmap` 组织 B 扫图。
单次 FMC 由 `SaveFMCDataAsMat` / `SaveWithMathNet` 保存 `FMC_DataMatrix` 与 `time_axis`。
`time_axis` 当前按固定 0.02 μs 间隔生成，改变设备实际采样率时要同步检查。

### 6.2 C 扫

`AcquisitionCscan_0x00010X02` 读取回调里的编码器 X/Y、闸门幅值及 TOF，将有效点加入
`_cscanPoints`；`BuildCscanMatrix` 和 `CreateCscanBitmap` 负责转成平面图，
`UpdateCscanDisplay` 由约 200 ms 的图像定时器触发。这里的数据是“某位置上的闸门特征”，
不是整条 A 扫波形。

### 6.3 文件记录

- 单次 FMC MAT

  用户选路径；变量主要是 `FMC_DataMatrix` 和 `time_axis`。

- 运动逐点 MAT

  用户选路径；逐点写 `Point_01_FMC`、`Point_01_Motion` 等。运动数组保存路径参数，不保存 ACS
  实测位置。

- OEMPA setup TXT

  `buttonLoad_Click` 用 `ReadFileWriteHW` 将文件写入设备；`buttonSave_Click` /
  `buttonFMC_Click` 用 `ReadHWWriteFile` 导出设备设置。

- 路径 TXT

  `hslBtnLoadLine_Click` 从用户选择的文件读六列数字。

- `config.ini`

  `MyIniFile` 读写应用选项、轴映射、方向及默认参数。

- SQLite 库

  `PubCls/MyDB.cs` 为用户、配方、历史记录页面做查询、增删改；与原始 FMC MAT 数据分别理解。
    
## 7. 主要函数索引：从功能定位源码

以下按职责列出实际入口、核心函数和重复按钮组。`*.Designer.cs` 的自动生成布局方法、
纯控件值变更事件可在找到对应界面后按需读；阅读主流程时先看下面这些函数。

### 7.1 启动、页面、配置

- `Program.cs:40` `Main`

  程序入口、互斥锁、授权、DLL 检查、启动主窗体。

- `Program.cs:109` `CheckDllDependencies`

  尝试从启动目录加载 `csDriverOEMPA.dll`，报告 DLL 加载或架构问题。

- `FormMain.cs:69` `FormMain_Load`

  设置 `config.ini` 路径，载入轴和功能配置，初始化主窗口。

- `FormMain.cs:263` `iconButton1_Click`；`iconButton2_Click`～`iconButton11_Click`

  打开扫查页及其他业务页面。

- `FormMain.cs:656` `OpenChildForm`；`:675` `SoftExit`

  切换子窗口、退出与资源清理。

- `PubCls/OperateFile/MyIniFile.cs` `ReadData` / `WriteData`

  读取和写回运行时 INI。

- `Forms/FrmConfig.cs`、`FrmOption.cs` 的加载/关闭事件

  配置页、选项页的载入与保存。

### 7.2 ACS 手动运动和状态

- `FrmTestTool_Load`（约 590）、`_ACSFormLoad`（约 2407）

  初始化扫查页，读取运动默认参数并建立轴与界面控件的映射。

- `hslBtnConnect_Click`（约 894）、`hslBtnDisConnect_Click`（约 933）、
  `_ACSFormClosed`（约 2451）

  建立/关闭 ACS 以太网通信。

- `tmrMonitor_Tick`（约 663）、`UpdateLimitState`（约 1521）、`UpdateAxisState`（约 1530）

  周期读取位置、速度、电机、限位和故障状态并刷新界面。

- `BtnEnableState`（约 1584）

  按连接和使能状态控制按钮可用性。

- `hslBtnEnableX/Y/Z_Click`（约 818～852）、`hslBtnEanbleAll_Click`（约 869）

  单轴/多轴使能与禁能。

- `hslBtnCW/CCW..._Click`（约 1055～1116）、`PTP_Move`（约 1556）

  手动点到点运动；由按钮确定轴、方向、目标值和速度。

- 各轴方向按钮 `MouseDown` / `MouseUp`（约 1234～1402）

  点动 `Jog`，松开 `Halt`。

- `hslBtnGoHomeX/Y/Z_Click`（约 878～889）、`HX_GoHomimg`（约 1428）

  运行相应 ACS 回零缓冲区并等待结束。

- `HX_GoGantry`、`HX_CheckGantryFlag`、`HX_AcsPowup`（约 1459～1500）

  龙门校正、状态判断和控制器缓冲区执行。

- `hslBtnSetZeroX/Y/Z_Click`、`hslBtnSetZeroAll_Click`（约 746～787）

  设置反馈位置零点；不能当作机械回零。

- `hslbtnHallAll_Click`（约 1128）、`hslBtnEmg_Click`（约 805）、
  `hslBtnClearError_Click`（约 736）

  Halt、软件 Kill/停缓冲区和清故障。

### 7.3 路径和两条运动扫描链

- `btnBrowseLineFile_Click`（约 1684）、`hslBtnLoadLine_Click`（约 1694）

  选取、解析六列路径，存入 `_currentMotionMatrix`。

- `InitializeScanManager`（约 1797）

  创建 `AutoScanManager` 和 `VirtualScanService`。

- `hslBtnStartLineScan_Click`（约 1750）、`hslBtnStopLineScan_Click`（约 2711）

  启动/停止自动线扫；运动真实、扫描值模拟。

- `AutoScanManager.StartAutoScanAsync`、`ExecuteScanSequenceAsync`、
  `ExecuteScanPointAsync`（约 1858～2077）

  管理路径遍历和每行运动顺序。

- `MoveAxisWithoutScanAsync`、`MoveAxisWithScanningAsync`（约 2106～2122）

  调 ACS 相对运动并等待运动结束。

- `SetupScanTimer`、`ScanTimerCallback`、`GetCurrentPosition`、`StartScan`（约 1919～2013）

  周期读取 X 位置，触发模拟扫描。

- `VirtualScanService.PerformScanAsync`（约 2205）

  生成正弦加噪声的模拟扫描数值。

- `hslBtnPauseLineScan_Click`（约 2462）

  暂停指定 ACS 程序缓冲区；它并未直接暂停 `AutoScanManager` 的 `ToPoint` 路线。

- `MoveAxisAsync`（约 5239）、`checkBoxMotionCapture_CheckedChanged`（约 5503）

  另一条逐点运动后接收真实 FMC 的路线。

### 7.4 OEMPA 连接、参数、触发

- `comboBoxOEMType_SelectedIndexChanged`（约 2908）、`InitDialog`（约 2992）

  创建 OEMPAmini 设备对象并注册 A/C 扫等回调。

- `checkBoxConnect_CheckedChanged`（约 2725）、`UpdateDialog`（约 2593）、
  `Status_OEM`（约 4362）

  连接/断开超声设备，更新界面和状态。

- `buttonRead_Click`、`buttonWrite_Click`（约 4084～4124）

  从设备读取或向设备写入常规采样参数。

- `SetGainStartRange`、`GetGainStartRange`、`GetFMCelements`（约 4153～4299）

  整理增益、起点、范围、采样率及 FMC 阵元参数。

- `buttonFMC_Click`（约 5300）、`buttonSetFMC_Click`（约 5269）

  设置 FMC 周期、阵元及相关硬件参数；后者还应按实际代码核对读回与界面行为。

- `buttonLoad_Click`、`buttonSave_Click`（约 4326～4344）

  从 setup 文件写设备 / 将设备设置导出文件。

- `buttonTemplate_Click`、`PulseEchoTemplate`、`PitchCatchTemplate`（约 2478～2553）

  使用厂家向导生成脉冲回波或一发一收模板。

- `comboBoxSynchronisation_SelectedIndexChanged`（约 4528）

  选内部、编码器或外部触发，并设置相应 IO。

- `UpdateResolution`（约 4631）、`buttonWriteStep_Click`（约 4666）、
  `buttonResetEncoder_Click`（约 4774）

  写编码器分辨率、触发步距和重置编码器。

### 7.5 采集、显示和保存

- `FMC_lengthwrite_logic`（约 5012）

  按范围、采样率和阵元数建立 FMC 矩阵。

- `checkBoxPulser_CheckedChanged`（约 3224）、`StartRealTimeAcquisition`、
  `StopAcquisition`（约 3397、3363）

  手动激发、实时显示或单次保存的开始/停止。

- `AcquisitionAscan_0x00010103`（约 5078）、`ProcessData`（约 5109）

  接收 A 扫回调，把采样写入 FMC 当前列。

- `AcquisitionIO_0x00010101`（约 5199）、`AcquisitionInfo`（约 3022）

  IO/信息回调。

- `StartUiTimer`、`DrawAscanRealtime`（约 3445、3927）

  定时刷新选定的 A 扫曲线。

- `GetSelectedTransmitChannel`、`GetSelectedReceiveChannel`（约 3610～3620）

  决定 A 扫显示使用的发射、接收通道。

- `GetBscanDataFromFMC`、`ComputeBScanBitmapInBackground`、
  `CreateBScanBitmap`（约 3569～3751）

  从 FMC 组织 B 扫数据并绘成位图。

- `SaveFMCDataAsMat`、`SaveWithMathNet`、`CreateTimeArray`（约 3989～4046）

  保存单次 FMC 与时间轴。

- `SaveallScandataToMat`（约 5445）

  保存多路径点 FMC 与运动参数。

- `buttonCscan_Click`（约 5821）、`AcquisitionCscan_0x00010X02`（约 5666）

  启动 C 扫和接收 C 扫闸门数据。

- `IsGateEnabled`（约 5807）、`BuildCscanMatrix`、`CreateCscanBitmap`、
  `UpdateCscanDisplay`（约 5901～5963）

  闸门筛选、C 扫矩阵生成与刷新。

### 7.6 辅助功能和硬件

- `PubCls/MyDevice.cs`

  全局配置、选项、用户和位置偏移等共享状态。

- `PubCls/MyDB.cs` 的 `Query/Insert/Update/Delete` 系列

  SQLite 用户、配方、历史记录 CRUD。

- `Forms/FrmRecipe.cs` 的 `LoadRecipeList` 与按钮事件

  配方查询、选择、新增、修改、删除。

- `Forms/FrmReview.cs` 的 `LoadReviewList`、`btnReviewToExcel_Click`、
  `btnReviewToWord_Click`

  历史记录管理和报表导出。

- `Forms/FrmDebug.cs`

  PLC、计时器和其他调试控件；其中三菱 FX3U 串口示例使用 `COM6`。

- `PubCls/Hardware/PLC/Melsec/ClsFX3U.cs`

  PLC 的串口连接与读写封装；不是逐点 FMC 的主数据通道。

- `PubCls/Hardware/MyBtPrinter.cs`

  可选标签打印。

- `PubCls/OperateFile/MyLogNetFile.cs`

  日志初始化和文件记录。

## 8. 关键状态与变量

- `m_bConnected`

  含义：ACS 通信是否已建立

  谁会改变或读取：ACS 连接/断开按钮及运动校验。

- `hwDeviceOEMPA`

  含义：当前超声设备对象

  谁会改变或读取：OEMPA 型号选择、连接、参数设置和激发。

- `_currentMotionMatrix`

  含义：六列路径矩阵

  谁会改变或读取：路径加载，两条自动运动路线。

- `FMC_DataMatrix`

  含义：`[采样点, FMC列]` 的原始波形矩阵

  谁会改变或读取：分配函数、A 扫回调、显示和保存。

- `_currentElementIndex`

  含义：当前一组 FMC 收到的回调列计数

  谁会改变或读取：A 扫回调、逐点等待循环。

- `_fmcWrittenColumns` / `_fmcDoneTcs`

  含义：单次 FMC 保存使用的列计数与完成通知

  谁会改变或读取：手动激发保存路线。

- `_scanManager._isScanning`

  含义：模拟自动线扫是否在进行

  谁会改变或读取：`AutoScanManager`。

- `_isProcessingMotionCapture`

  含义：逐点 FMC 路线防重复进入标志

  谁会改变或读取：运动采集事件。

- `_cscanPoints`

  含义：C 扫编码器位置与闸门特征点

  谁会改变或读取：C 扫回调和图像定时器。

- `MyDevice.PosX/Y/Z.Offset`

  含义：位置显示或路径计算时使用的偏移

  谁会改变或读取：INI/界面与扫查页。

这些状态由 UI 事件、定时器、后台 `Task` 和驱动回调共同使用。修改采集条件或停止逻辑时，
要同时检查开始、回调、等待和异常清理四处。
