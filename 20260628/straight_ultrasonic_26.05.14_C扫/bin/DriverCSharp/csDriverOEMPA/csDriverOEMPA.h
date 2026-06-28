#pragma once

using System::String;
using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::Collections::Generic;

#ifndef _DRIVER_11XY_
namespace csDriverOEMPA
#else //_DRIVER_11XY_
namespace csDriverOEM
#endif //_DRIVER_11XY_
{
	ref class csRoot;
	ref class csGate;
	ref class csCycle;
	ref class csFocalLaw;

#pragma region enumerations
	public enum class csEnumHardware{
									csNoHW=eNoHW,csOEMPA1=eOEMPA1,csOEMMC1=eOEMMC1,csOEMPA2=eOEMPA2,csOEMPAmini=eOEMPAmini,csOEMPAmax=eOEMPAmax, csOEMMC2 = eOEMMC2, csOEMMCu = eOEMMCu, csOEMMCuF = eOEMMCuF, csOEMPAsave=eOEMPAsave,csOEMPAX=eOEMPAX
									};
	public enum class csEnumHardwareLink{
									csUnlinked=eUnlinked,
									csMaster=eMaster,
									csSlave=eSlave
									};
	public enum class csEnumMsgBoxButtons{
									csOK												= 0x00000000,
									csOKCANCEL											= 0x00000001,
									csABORTRETRYIGNORE									= 0x00000002,
									csYESNOCANCEL										= 0x00000003,
									csYESNO												= 0x00000004,
									csRETRYCANCEL										= 0x00000005
									};
	public enum class csEnumMsgBoxReturn{
									csOK												= 1,
									csCANCEL											= 2,
									csABORT												= 3,
									csRETRY												= 4,
									csIGNORE											= 5,
									csYES												= 6,
									csNO												= 7
									};
	public enum class csEnumCustomizedFlags : int64_t
									{
									csOEMPA_ASCAN_ENABLE								= 0x0000000000000001,
									csOEMPA_CSCAN_ENABLE_TOF							= 0x0000000000000002,
									csOEMPA_ASCAN_BITSIZE								= 0x0000000000000004,
									csOEMPA_TRIGGER_MODE								= 0x0000000000000008,
									csOEMPA_TRIGGER_ENCODER_STEP						= 0x0000000000000010,
									csOEMPA_REQUESTIO									= 0x0000000000000020,
									csOEMPA_REQUESTIO_DIGITAL_INPUT_MASK				= 0x0000000000000040,
									csOEMPA_KEEPALIVE									= 0x0000000000000080,
									csOEMPA_DEBOUNCER_ENCODER							= 0x0000000000000100,
									csOEMPA_DEBOUNCER_DIGITAL							= 0x0000000000000200,
									csOEMPA_DIGITAL_OUTPUT_0							= 0x0000000000000400,
									csOEMPA_DIGITAL_OUTPUT_1							= 0x0000000000000800,
									csOEMPA_DIGITAL_OUTPUT_2							= 0x0000000000001000,
									csOEMPA_DIGITAL_OUTPUT_3							= 0x0000000000002000,
									csOEMPA_DIGITAL_OUTPUT_4							= 0x0000000000004000,
									csOEMPA_DIGITAL_OUTPUT_5							= 0x0000000000008000,
									csOEMPA_SW_ENCODER1_RESOLUTION					= 0x0000000000010000,
									csOEMPA_SW_ENCODER1_DIVIDER						= 0x0000000000020000,
									csOEMPA_ENCODER1A									= 0x0000000000040000,
									csOEMPA_ENCODER1B									= 0x0000000000080000,
									csOEMPA_ENCODER2A									= 0x0000000000100000,
									csOEMPA_ENCODER2B									= 0x0000000000200000,
									csOEMPA_EXTERNAL_TRIGGER_CYCLE					= 0x0000000000400000,
									csOEMPA_EXTERNAL_TRIGGER_SEQUENCE					= 0x0000000000800000,
									csOEMPA_SW_ENCODER2_RESOLUTION					= 0x0000000001000000,
									csOEMPA_SW_ENCODER2_DIVIDER						= 0x0000000002000000,
									csOEMPA_ENCODER1_TYPE								= 0x0000000004000000,
									csOEMPA_ASCAN_REQUEST								= 0x0000000008000000,
									csOEMPA_DEFAULT_HW_LINK							= 0x0000000010000000,
									csOEMPA_TRACKING									= 0x0000000020000000,
									csOEMPA_ENCODER2_TYPE								= 0x0000000040000000,
									csOEMPA_COUNT_MAX_DAC								= 0x0000400000000000,
									csOEMPA_COUNT_MAX_DDF								= 0x0000800000000000
									};
	public enum class csEnumAcquisitionState{	csAcqOff=eAcqOff,	//pulse shot is disabled.
									csAcqOn=eAcqOn,					//pulse shot is enabled.
									csAcqContinue=eAcqContinue,		//HW state is not modified.
									csAcqDefault = eAcqDefault		//the configuration file is used to stop or start the pulse shot.
									};
	public enum class csEnumConnectionState{	csDisconnected=eDisconnected,csConnected=eConnected };
	public enum class csEnumUpdateStatus{	csOutOfDate=eOutOfDate,csUpToDate=eUpToDate };
	public enum class csEnumUnit{
		csNoUnit=eNoUnit,csHexadecimal=eHexadecimal,csPercent=ePercent,csMeter=eMeter,csMilliMeter=eMilliMeter,csSecond=eSecond,csMilliSecond=eMilliSecond,csMicroSecond=eMicroSecond,csNanoSecond=eNanoSecond,
		csMeterPerSecond=eMeterPerSecond,csRadian=eRadian,csDegree=eDegree,csDecibel=eDecibel,csHertz=eHertz,csKiloHertz=eKiloHertz,csMegaHertz=eMegaHertz,csVolt=eVolt,
		csBytePerSecond=eBytePerSecond,csKiloBytePerSecond=eKiloBytePerSecond,csMegaBytePerSecond=eMegaBytePerSecond,csDegreeCelsius=eDegreeCelsius,csLinkRootId=eLinkRootId,
		csBinary=eBinary,csStepPerMilliMeter=eStepPerMilliMeter,
		csLastUnit=eLastUnit
	};
	public enum class csEnumEncoderType{
		csStaticScan=eStaticScan,	//offset are used to store the data (no encoder).
		csEncoderQuadrature=eEncoderQuadrature,	//encoder are used to store the data (quadrature).
		csEncoderQuadrature4Edges=eEncoderQuadrature4Edges,//encoder are used to store the data (quadrature).
		csEncoderDirectionCount=eEncoderDirectionCount,	//encoder : one signal for the direction and one signal for the count.
		csEncoderForwardBackward=eEncoderForwardBackward,	//encoder : one signal for the forward and one signal for the back.
		csSpeedMeasurement=eSpeedMeasurement //OnLine.
	};

	public enum class csEnumOEMPAFilter{
		csOEMPAFilterCustom=eOEMPAFilterCustom,
		csOEMPAHighPassOrder64Cut1Mhz=eOEMPAHighPassOrder64Cut1Mhz,
		csOEMPAHighPassOrder64Cut2Mhz=eOEMPAHighPassOrder64Cut2Mhz,
		csOEMPAHighPassOrder64Cut3Mhz=eOEMPAHighPassOrder64Cut3Mhz,
		csOEMPAHighPassOrder64Cut4Mhz=eOEMPAHighPassOrder64Cut4Mhz,
		csOEMPAHighPassOrder64Cut5Mhz=eOEMPAHighPassOrder64Cut5Mhz,
		csOEMPAHighPassOrder64Cut6Mhz=eOEMPAHighPassOrder64Cut6Mhz,
		csOEMPAHighPassOrder64Cut7Mhz=eOEMPAHighPassOrder64Cut7Mhz,
		csOEMPAHighPassOrder64Cut8Mhz=eOEMPAHighPassOrder64Cut8Mhz,
		csOEMPAHighPassOrder64Cut9Mhz=eOEMPAHighPassOrder64Cut9Mhz,
		csOEMPAHighPassOrder64Cut10Mhz=eOEMPAHighPassOrder64Cut10Mhz
	};
	public enum class csEnumFeatureDigitalInput{
		csDigitalInputWire1=eDigitalInputWire1,
		csDigitalInputWire2=eDigitalInputWire2,
		csDigitalInputEncoder2Wire1=eDigitalInputEncoder2Wire1,
		csDigitalInputEncoder2Wire2=eDigitalInputEncoder2Wire2,
		csDigitalInputExternalCycle=eDigitalInputExternalCycle,
		csDigitalInputExternalSequence=eDigitalInputExternalSequence,
		csDigitalInputBlanking=eDigitalInputBlanking,
		csDigitalInputPresetRising=eDigitalInputPresetRising,
		csDigitalInputSpeed1=eDigitalInputSpeed1,
		csDigitalInputSpeed2=eDigitalInputSpeed2,
		csDigitalInputEncoder2PresetRising=eDigitalInputEncoder2PresetRising,
		csDigitalInputLast=eDigitalInputLast
	};
	public enum class csEnumDigitalInput{
		csDigitalInputOff=eDigitalInputOff,
		csDigitalInput01=eDigitalInput01,
		csDigitalInput02=eDigitalInput02,
		csDigitalInput03=eDigitalInput03,
		csDigitalInput04=eDigitalInput04,
		csDigitalInput05=eDigitalInput05,
		csDigitalInput06=eDigitalInput06,
		csDigitalInput07=eDigitalInput07,
		csDigitalInput08=eDigitalInput08,
		csDigitalInput09=eDigitalInput09,
		csDigitalInput10=eDigitalInput10,
		csDigitalInput11=eDigitalInput11,
		csDigitalInput12=eDigitalInput12,
		csDigitalInput13=eDigitalInput13,
		csDigitalInput14=eDigitalInput14,
		csDigitalInput15=eDigitalInput15,
		csDigitalInput16=eDigitalInput16,
		csDigitalInput17=eDigitalInput17,
		csDigitalInput18=eDigitalInput18,
		csDigitalInput19=eDigitalInput19,
		csDigitalInput20=eDigitalInput20,
		csDigitalInput21=eDigitalInput21,
		csDigitalInput22=eDigitalInput22,
		csDigitalInput23=eDigitalInput23,
		csDigitalInput24=eDigitalInput24
	};
	enum  { eNoCommunication, eTCP, ePCIe, eUDP };
	public enum class csEnumCommunication {csNoCommunication = eNoCommunication, csTCP = eTCP, csPCIe = ePCIe, csUDP= eUDP};
	public enum class csEnumKeepAlive {
		eKeepAliveOff,
		eKeepAliveHardwareAndComputer,
		eKeepAliveHardwareOnly,
		eKeepAliveComputerOnly
	};
	public enum class csEnumFocusing{csStandard=eStandard,csDDF=eDDF};
	public enum class csEnumAscanRequest{
		eAscanAll,
		eAscanSampled,
		eBscanOnLine
	};
	public enum class csEnumBitSize {cs8Bits=e8Bits,cs12Bits=e12Bits,cs16Bits=e16Bits,csLog8Bits=eLog8Bits};
	public enum class csEnumCompressionType {csCompression=eCompression,csDecimation=eDecimation};
	public enum class csEnumGateModeAmp{csAmpAbsolute=eAmpAbsolute,csAmpMaximum=eAmpMaximum,csAmpMinimum=eAmpMinimum,csAmpPeakToPeak=eAmpPeakToPeak};
	public enum class csEnumGateModeTof{csTofAmplitudeDetection=eTofAmplitudeDetection,//"AMP's TOF" : where the AMP result has been found, for Peak-Peak--> where Max has been found
								csTofThresholdCross=eTofThresholdCross,//"TH cross": first cross of the THRESHOLD  
								csTofZeroFirstAfterThresholdCross=eTofZeroFirstAfterThresholdCross,//"ZrA": first time crossed 0 after crossing THRESHOLD
								csTofZeroLastBeforeThresholdCross=eTofZeroLastBeforeThresholdCross//"ZrB": last time crossed 0 before crossing THRESHOLD
								};
	public enum class csEnumRectification{
							csSigned=eSigned,			//Rectification: RF --> not rectified, signed value,
							csUnsigned=eUnsigned,			//FW --> rectified, unsigned
							csUnsignedPositive=eUnsignedPositive,	//HWP --> only positive, unsigned,
							csUnsignedNegative=eUnsignedNegative	//HWN --> only negative
							};

	public enum class csEnumOEMPATrigger{
		csOEMPAInternal=eOEMPAInternal,//encoder and external signals are not used.
		csOEMPAEncoder=eOEMPAEncoder,//encoder 1 is used for the sequence (cycle is internal).
		csOEMPAExternalCycle=eOEMPAExternalCycle,//the next cycle after the last cycle is for the next sequence.
		csOEMPAExternalSequence=eOEMPAExternalSequence,//sequence is coming from external pulse (cycle are internal).
		csOEMPAExternalCycleSequence=eOEMPAExternalCycleSequence,//cycle and sequence are coming from external signals.
		csOEMPASoftware=eOEMPASoftware//the computer trig the device by himself.
	};
	public enum class csEnumOEMPAEncoderDirection{
		csOEMPAUpDown=eOEMPAUpDown,//pulser is triggered for both directions.
		csOEMPAUp=eOEMPAUpOnly,//pulser is triggered only if encoder goes up.
		csOEMPADown=eOEMPADownOnly//pulser is triggered only if encoder goes down.
	};
	public enum class csEnumOEMPARequestIO{
		csOEMPANotRequired=eOEMPANotRequired,
		csOEMPAOnCycleOnly=eOEMPAOnCycleOnly,//use this value if you want Ascan with encoder.
		csOEMPAOnSequenceOnly=eOEMPAOnSequenceOnly,
		csOEMPAOnDigitalInputOnly=eOEMPAOnDigitalInputOnly,
		csOEMPAOnDigitalInputAndCycle=eOEMPAOnDigitalInputAndCycle,//use this value if you want Ascan with encoder and digital inputs.
		csOEMPAOnDigitalInputAndSequence=eOEMPAOnDigitalInputAndSequence

	};
	public enum class csEnumOEMPAFilterIndex{
		csOEMPAFilterOff=eOEMPAFilterOff,
		csOEMPAFilter1=eOEMPAFilter1,
		csOEMPAFilter2=eOEMPAFilter2,
		csOEMPAFilter3=eOEMPAFilter3,
		csOEMPAFilter4=eOEMPAFilter4,
		csOEMPAFilter5=eOEMPAFilter5,
		csOEMPAFilter6=eOEMPAFilter6,
		csOEMPAFilter7=eOEMPAFilter7,
		csOEMPAFilter8=eOEMPAFilter8,
		csOEMPAFilter9=eOEMPAFilter9,
		csOEMPAFilter10=eOEMPAFilter10,
		csOEMPAFilter11=eOEMPAFilter11,
		csOEMPAFilter12=eOEMPAFilter12,
		csOEMPAFilter13=eOEMPAFilter13,
		csOEMPAFilter14=eOEMPAFilter14,
		csOEMPAFilter15=eOEMPAFilter15

	};
	public enum class csEnumOEMPAMappingDigitalInput{
		csOEMPANotUsed=eOEMPANotUsed,
		csOEMPAEncoder1A=eOEMPAEncoder1A,
		csOEMPAEncoder1B=eOEMPAEncoder1B,
		csOEMPAEncoder2A=eOEMPAEncoder2A,
		csOEMPAEncoder2B=eOEMPAEncoder2B,
		csOEMPAExternalTriggerCycle=eOEMPAExternalTriggerCycle,
		csOEMPAExternalTriggerSequence=eOEMPAExternalTriggerSequence,
		csOEMPADigitalInput0=eOEMPADigitalInput0,
		csOEMPADigitalInput1=eOEMPADigitalInput1,
		csOEMPADigitalInput2=eOEMPADigitalInput2,
		csOEMPADigitalInput3=eOEMPADigitalInput3,
		csOEMPADigitalInput4=eOEMPADigitalInput4,
		csOEMPADigitalInput5=eOEMPADigitalInput5,
		csOEMPAResetTrackingTable=eOEMPAResetTrackingTable
	};
	public enum class csEnumOEMPAMappingDigitalOutput{
		csOEMPALow=eOEMPALow,
		csOEMPAHigh=eOEMPAHigh,
		csOEMPASignalCycle=eOEMPASignalCycle,
		csOEMPASignalSequence=eOEMPASignalSequence
	};
	public enum class csEnumOEMPARequestCscan{
		csOEMPACycle=eOEMPACycle,
		csOEMPASequence=eOEMPASequence
	};
	public enum class csEnumStepCustomizedAPI
	{
		csReadFileWriteHW_Enter=eReadFileWriteHW_Enter,
		csWriteHW_Enter=eWriteHW_Enter,
		csWriteHW_Leave=eWriteHW_Leave,
		csReadFileWriteHW_Leave=eReadFileWriteHW_Leave,
		csWriteFile_Enter=eWriteFile_Enter,
		csWriteFile_Leave=eWriteFile_Leave
	};
	public enum class csEnumTFMParameters
	{csTFMOff=eTFMOff,csFlatContact_Linear1D=eFlatContact_Linear1D};
	public enum class csEnumAcquisitionFifo
	{csFifoAscan=eFifoAscan,csFifoCscan=eFifoCscan,csFifoIO=eFifoIO};
	public enum class csEnumReferenceTimeOfFlight
	{csPulse=ePulse,csInterface=eInterface};
	public enum class csEnumSetupFileType
	{
		csNoFileType = eNoFileType, csFilePA = eFilePA, csFileFMC = eFileFMC, csFileMC1 = eFileMC1, csFileMC2 = eFileMC2, csFileMCu = eFileMCu
	};
#pragma endregion enumerations
///////////////////////////////////////////////////////////////////////////////////////
#pragma region structures
	[StructLayout(LayoutKind::Sequential)]
	public ref class csAcqInfoEx
	{
	public:
		[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iDriverEncoderCountMax)]
		cli::array<long>^ lEncoder;//unit given by the encoder
		[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iDriverEncoderCountMax)]
		cli::array<double>^ dEncoder;//unit : meter (encoder resolution is taken into account).
		DWORD dwDigitalInputs;
		long lMaxTemperature;//0 if not significant (for some FW version or in case IO stream are not sent by the HW).
		long lDeviceId;//device identifier
		long bMultiChannel;//1 if Multi-Channel is enabled, 0 otherwise.
		long bFullMatrixCapture;//1 if FullMatrixCapture is enabled, 0 otherwise.
		long lFMCElementIndex;//in case of Full Matrix Capture, this is the element index for the current ascan;
	};

	[StructLayout(LayoutKind::Sequential)]
	public ref class csHeaderStream_0x0001
	{
	public:
		unsigned long start;
		unsigned long size;
		unsigned long frameId;
		unsigned long settingId;
		unsigned short subStreamCount;
		unsigned short version;
	};

	[StructLayout(LayoutKind::Sequential)]
	public ref class csHeaderStream_0x0002
	{
	public:
		unsigned long newStart;
		unsigned long size2;
		unsigned long nextSize;
		unsigned long error;
		unsigned long swMarge0;
		unsigned char digitalInputs;
		unsigned char maxTemperature;
		unsigned short swMarge1;
		unsigned long encoder0;
		unsigned long encoder1;
		unsigned long encoder2;
		unsigned long encoder3;
		unsigned long swMarge2;
		unsigned long totalSize;
		unsigned long swMarge3;
		// Header V1
		unsigned long start;
		unsigned long size;
		unsigned long frameId;
		unsigned long settingId;
		unsigned short subStreamCount;
		unsigned short version;
	};

	[StructLayout(LayoutKind::Sequential)]
	public ref class csHeaderIO_0x0001
	{
	public:
		BYTE id;
		BYTE version;
		WORD size;
		DWORD timeStampLow;
		DWORD timeStampHigh;
		WORD cycle;
		WORD padding0;
		DWORD seqLow;
		DWORD seqHigh;
		DWORD inputs;//digital inputs value.
		DWORD edges;//edges : which digital input has been updated ?
						//if bit 16 is set to 1 then this is initialisation stream (sent on any updates from "SetRequestIO").
		DWORD encoder1;
		DWORD encoder2;

		property int maxTemperature{int get(){return padding0 & 0x3f;};}
	};

	[StructLayout(LayoutKind::Sequential,Size=sizeof(structCscanAmp_0x0102))]
	public ref class csCscanAmp_0x0102
	{
	public:
		BYTE byAmp;//cscan amplitude. Could be signed value, it depends of the GateModeThreshold setting data.
		BYTE gateType;//please use properties
							//unsigned gateId:2;//gate identifier (could be: 0=first gate, 1=second gate, 2=third gate, 3=fourth gate).
							//unsigned sign:1;
							//unsigned reserved:5;
		WORD wAcqId;//acquisition identifier (could be used to link the unique gate/cycle cscan data with the setting). But it is not required to use it.

		property int gateId{int get(){return gateType & 0x3;};}
		property bool sign {bool get(){return (gateType & 4?true:false);};}
		property bool AmpOverThreshold {bool get(){return (gateType & 8?true:false);};}
		property bool IFOldReference {bool get(){return (gateType & 0x20?true:false);};}
		property bool IFNotInitialized {bool get(){return (gateType & 0x40?true:false);};}
	};

	[StructLayout(LayoutKind::Sequential,Size=sizeof(structCscanAmpTof_0x0202))]
	public ref class csCscanAmpTof_0x0202
	{
	public:
		BYTE byAmp;//cscan amplitude. Could be signed value, it depends of the GateModeThreshold setting data.
		BYTE gateType;//please use properties
					//unsigned gateId:2;//gate identifier (could be: 0=first gate, 1=second gate, 2=third gate, 3=fourth gate).
					//unsigned sign:1;
					//unsigned reserved:5;
		WORD wAcqIdAmp;//acquisition identifier (could be used to link the unique gate/cycle cscan data with the setting). But it is not required to use it.
		WORD wTof;//cscan time of flight for the same gate.
		WORD wAcqIdTof;//time of flight acquisition identifier (could be used to link the unique gate/cycle cscan data with the setting). But it is not required to use it.

		property int gateId{int get(){return gateType & 0x3;};}
		property bool sign {bool get(){return (gateType & 4?true:false);};}
		property bool AmpOverThreshold {bool get(){return (gateType & 8?true:false);};}
		property bool TofValidity {bool get(){return (gateType & 0x10?true:false);};}
		property bool IFOldReference {bool get(){return (gateType & 0x20?true:false);};}
		property bool IFNotInitialized {bool get(){return (gateType & 0x40?true:false);};}
	};

	[StructLayout(LayoutKind::Sequential, Size = sizeof(structCscanAmpTof_0x0402))]
		public ref class csCscanAmpTof_0x0402
	{
	public:
		WORD gateType;//please use properties
		//unsigned gateId:2;//gate identifier (could be: 0=first gate, 1=second gate, 2=third gate, 3=fourth gate).
		//unsigned channelId : 3;//channel identifier, from 0 to 7.
		//unsigned sign:1;
		//unsigned AmpOverThreshold : 1;//0 if byAmp is under threshold, and 1 if it is over the threshold.
		//unsigned TofValidity : 1;//1 if wTof is valid.
		//unsigned IFOldReference : 1;//(IFtracking) 1 if old reference time of flight has been used to synchronize the start or the stop.
		//unsigned IFNotInitialized : 1;//(IFtracking) 1 if the first reference time of flight has not been initialized yet (because of acoustic or because of bad setting stop>start).
		//unsigned reserved:6;
		WORD wAcqIdAmp;//acquisition identifier (could be used to link the unique gate/cycle cscan data with the setting). But it is not required to use it.
		DWORD dwAmp;
		WORD wTof;//cscan time of flight for the same gate.
		WORD wAcqIdTof;//time of flight acquisition identifier (could be used to link the unique gate/cycle cscan data with the setting). But it is not required to use it.

		property int gateId {int get() { return gateType & 0x3; }; }
		property int channel {int get() { return (gateType & 0x1c) >> 2; }; }
		property bool sign {bool get() { return (gateType & 0x20 ? true : false); }; }
		property bool AmpOverThreshold {bool get() { return (gateType & 0x40 ? true : false); }; }
		property bool TofValidity {bool get() { return (gateType & 0x80 ? true : false); }; }
		property bool IFOldReference {bool get() { return (gateType & 0x100 ? true : false); }; }
		property bool IFNotInitialized {bool get() { return (gateType & 0x200 ? true : false); }; }
	};

	[StructLayout(LayoutKind::Sequential)]
	public ref class csSubStreamCscan_0x0X02
	{
	public:
		BYTE id;
		BYTE version;	//could be 1 or 2 (it depends of the setting, parameter RequestCscan with enumeration enumOEMPARequestCscan):
							//1: all cscan gate (amplitude only), packed together in the current sub stream, are for the same cycle number.
							//	 in this case the member "cylce" could used.
							//2: all cscan gate (amplitude + time of flight) of all cycles of the sequence are packed together in the same substream.
							//	 in this case the member "cylce" has no significance.
		WORD size;
		DWORD timeStampLow;
		DWORD timeStampHigh;
		WORD cycle;//cycle value in case "version" member value is 1, cycle=0xffff in case "version" member value is 2 (not significant in this case).
		WORD exCount;//please use property count
					//unsigned count:15;//count of gates (amplitude only in case of "version" is 1, or amplitude + time of flight in case of "version" is 2).
					//unsigned reserved:1;
		DWORD seqLow;//sequence number (LOW WORD).
		DWORD seqHigh;//sequence number (HIGH WORD).

		property int count{int get(){return exCount & 0x7fff;};}
	};

	[StructLayout(LayoutKind::Sequential)]
	public ref class csSubStreamCscan_0x0402
	{
	public:
		BYTE id2;
		BYTE version2;
		WORD size2;
		BYTE deviceId;
		BYTE marge0;
		WORD marge1;
		WORD marge2;
		BYTE maxTemperature;
		BYTE digitalInputs;
		DWORD encoder0;
		DWORD encoder1;
		DWORD marge3;
		DWORD marge4;
		DWORD marge5;
		DWORD marge6;
		// Header V1
		BYTE id;
		BYTE version;	//could be 1 or 2 (it depends of the setting, parameter RequestCscan with enumeration enumOEMPARequestCscan):
		//1: all cscan gate (amplitude only), packed together in the current sub stream, are for the same cycle number.
		//	 in this case the member "cylce" could used.
		//2: all cscan gate (amplitude + time of flight) of all cycles of the sequence are packed together in the same substream.
		//	 in this case the member "cylce" has no significance.
		WORD size;
		DWORD timeStampLow;
		DWORD timeStampHigh;
		WORD cycle;//cycle value in case "version" member value is 1, cycle=0xffff in case "version" member value is 2 (not significant in this case).
		WORD exCount;//please use property count
		//unsigned count:15;//count of gates (amplitude only in case of "version" is 1, or amplitude + time of flight in case of "version" is 2).
		//unsigned reserved:1;
		DWORD seqLow;//sequence number (LOW WORD).
		DWORD seqHigh;//sequence number (HIGH WORD).

		property int count {int get() { return exCount & 0x7fff; }; }
	};

	[StructLayout(LayoutKind::Sequential)]
	public ref class csSubStreamAscan_0x0103
	{
	//ONLY USEFUL MEMBERS FOR THE USER HAVE COMMENT.
	public:
		BYTE id;
		BYTE version;
		WORD size;
		DWORD timeStampLow;
		DWORD timeStampHigh;
		WORD cycle;//cycle number. this value is the cycle index (range 0 to max CycleCount-1, where CycleCount is the cycle count in your OEMPA file.
		WORD dataCount;	//data count (dataCount x dataSize = total data size). This is the sample count.
						//For example if you have AscanRange=30 us in your OEMPA file without compression:
						//	3000 ns/10 ns=3000 plus one point (10 ns= digitizing clock) that is dataCount=3001.
						//If you increase the AscanRange this value will be increased.
						//The buffer size of pBufferMax is (dataCount x dataSize).
		BYTE ascan_byte1;//please use properties
							//unsigned src:1;
							//unsigned dst:1;
							//unsigned type:1;
							//unsigned error:1;
							//unsigned dataSize:4;//size of one data (in bytes), take a look also to member "bitSize".
		BYTE ascan_byte2;//please use properties
							//unsigned align:4;
							//unsigned max:1;//maximum buffer is valid.
							//unsigned min:1;//minimum buffer is valid.
							//unsigned sat:1;//saturation buffer is valid.
							//unsigned sign:1;//sign of maximum and minimum buffer data.
		WORD ascan_word1;//please use properties
							//unsigned bitSize:2;//see enumBitSize
							//unsigned seqLost:1;//encoder speed too high.
							//unsigned padding0:13;
		WORD FWAcqIdChannelCycle;//acquisition Id for saturation data, see "OEMPA_SetAscanAcqIdChannelCycle".
		WORD FWAcqIdChannelScan;//acquisition Id for minimum data, see "OEMPA_SetAscanAcqIdChannelScan".
		WORD FWAcqIdChannelProbe;//acquisition Id for maximum data, see "OEMPA_SetAscanAcqIdChannelProbe".
		WORD padding1;
		DWORD seqLow;//sequence number (LOW DWORD)
		DWORD seqHigh;//sequence number (HIGH DWORD)

		property bool src {bool get(){return (ascan_byte1 & 1?true:false);};}
		property bool dst {bool get(){return (ascan_byte1 & 2?true:false);};}
		property bool type {bool get(){return (ascan_byte1 & 4?true:false);};}
		property bool error {bool get(){return (ascan_byte1 & 8?true:false);};}
		property int dataSize {int get(){return (ascan_byte1 & 0xf0)/16;};}

		property int align {int get(){return (ascan_byte2 & 0xf);};}
		property bool max {bool get(){return (ascan_byte2 & 0x10?true:false);};}//true if pBufferMax is not NULL.
		property bool min {bool get(){return (ascan_byte2 & 0x20?true:false);};}//true if pBufferMin is not NULL.
		property bool sat {bool get(){return (ascan_byte2 & 0x40?true:false);};}//true if pBufferSat is not NULL.
		property bool sign {bool get(){return (ascan_byte2 & 0x80?true:false);};}//1 if ascan (pBufferMax) is signed data and 0 if it is unsigned.

		property csEnumBitSize bitSize {csEnumBitSize get(){return (csEnumBitSize)(ascan_word1 & 0x3);};}//This is the size of one sample. This size can be set by the OEMPA file. For example:
																										 //cs8Bits -> bitSize=1
																										 //cs12Bits -> bitSize=2
																										 //cs16Bits -> bitSize=2
		//property bool seqLost {bool get(){return (ascan_word1 & 0x4?true:false);};}
		property bool dacIFOldReference {bool get(){return (ascan_word1 & 0x4?true:false);};}
		property bool dacIFNotInitialized {bool get(){return (ascan_word1 & 0x8?true:false);};}
		property bool IFOldReference {bool get(){return (ascan_word1 & 0x10?true:false);};}
		property bool IFNotInitialized {bool get(){return (ascan_word1 & 0x20?true:false);};}
	};

	[StructLayout(LayoutKind::Sequential)]
	public ref class csSubStreamAscan_0x0203
	{
	public:
		unsigned char id2;
		unsigned char version2;
		unsigned short size2;
		unsigned char deviceId;
		unsigned char maxElement;
		unsigned short marge1;
		unsigned short element;
		unsigned char maxTemperature;
		unsigned char digitalInputs;
		unsigned long encoder0;
		unsigned long encoder1;
		unsigned long encoder2;
		unsigned long encoder3;
		unsigned long marge2;
		unsigned long marge3;
		// Header V1
		BYTE id;
		BYTE version;
		WORD size;
		DWORD timeStampLow;
		DWORD timeStampHigh;
		WORD cycle;//cycle number. this value is the cycle index (range 0 to max CycleCount-1, where CycleCount is the cycle count in your OEMPA file.
		WORD dataCount;	//data count (dataCount x dataSize = total data size). This is the sample count.
		//For example if you have AscanRange=30 us in your OEMPA file without compression:
		//	3000 ns/10 ns=3000 plus one point (10 ns= digitizing clock) that is dataCount=3001.
		//If you increase the AscanRange this value will be increased.
		//The buffer size of pBufferMax is (dataCount x dataSize).
		BYTE ascan_byte1;//please use properties
		//unsigned src:1;
		//unsigned dst:1;
		//unsigned type:1;
		//unsigned error:1;
		//unsigned dataSize:4;//size of one data (in bytes), take a look also to member "bitSize".
		BYTE ascan_byte2;//please use properties
		//unsigned align:4;
		//unsigned max:1;//maximum buffer is valid.
		//unsigned min:1;//minimum buffer is valid.
		//unsigned sat:1;//saturation buffer is valid.
		//unsigned sign:1;//sign of maximum and minimum buffer data.
		WORD ascan_word1;//please use properties
		//unsigned bitSize:2;//see enumBitSize
		//unsigned seqLost:1;//encoder speed too high.
		//unsigned padding0:13;
		WORD FWAcqIdChannelCycle;//acquisition Id for saturation data, see "OEMPA_SetAscanAcqIdChannelCycle".
		WORD FWAcqIdChannelScan;//acquisition Id for minimum data, see "OEMPA_SetAscanAcqIdChannelScan".
		WORD FWAcqIdChannelProbe;//acquisition Id for maximum data, see "OEMPA_SetAscanAcqIdChannelProbe".
		WORD padding1;
		DWORD seqLow;//sequence number (LOW DWORD)
		DWORD seqHigh;//sequence number (HIGH DWORD)

		property bool src {bool get() { return (ascan_byte1 & 1 ? true : false); }; }
		property bool dst {bool get() { return (ascan_byte1 & 2 ? true : false); }; }
		property bool type {bool get() { return (ascan_byte1 & 4 ? true : false); }; }
		property bool error {bool get() { return (ascan_byte1 & 8 ? true : false); }; }
		property int dataSize {int get() { return (ascan_byte1 & 0xf0) / 16; }; }

		property int align {int get() { return (ascan_byte2 & 0xf); }; }
		property bool max {bool get() { return (ascan_byte2 & 0x10 ? true : false); }; }//true if pBufferMax is not NULL.
		property bool min {bool get() { return (ascan_byte2 & 0x20 ? true : false); }; }//true if pBufferMin is not NULL.
		property bool sat {bool get() { return (ascan_byte2 & 0x40 ? true : false); }; }//true if pBufferSat is not NULL.
		property bool sign {bool get() { return (ascan_byte2 & 0x80 ? true : false); }; }//1 if ascan (pBufferMax) is signed data and 0 if it is unsigned.

		property csEnumBitSize bitSize {csEnumBitSize get() { return (csEnumBitSize)(ascan_word1 & 0x3); }; }//This is the size of one sample. This size can be set by the OEMPA file. For example:
		//cs8Bits -> bitSize=1
		//cs12Bits -> bitSize=2
		//cs16Bits -> bitSize=2
//property bool seqLost {bool get(){return (ascan_word1 & 0x4?true:false);};}
		property bool dacIFOldReference {bool get() { return (ascan_word1 & 0x4 ? true : false); }; }
		property bool dacIFNotInitialized {bool get() { return (ascan_word1 & 0x8 ? true : false); }; }
		property bool IFOldReference {bool get() { return (ascan_word1 & 0x10 ? true : false); }; }
		property bool IFNotInitialized {bool get() { return (ascan_word1 & 0x20 ? true : false); }; }
	};
//////////////////////////////////////////////////////////////////////////
	[StructLayout(LayoutKind::Sequential,CharSet = CharSet::Unicode)]
	public ref class csFilter
	{
	public:
		[MarshalAs(UnmanagedType::ByValTStr, SizeConst = g_iFilterTitleLength)]
		String^ pTitle;
		WORD wScale;
		[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iOEMPAFilterCoefficientMax)]
		cli::array<short>^ aCoefficient;
	};


	[StructLayout(LayoutKind::Sequential)]
	public ref class csOptionsCom
	{
	public:
		WORD uEthernetSpeed;
		WORD uReserved;
	};
	[StructLayout(LayoutKind::Sequential)]
	public ref class csOptionsTCP
	{
	public:
		BYTE uScale;
		BYTE uWndSize;
		BYTE uMss;
		BYTE uOption;
	};
	[StructLayout(LayoutKind::Sequential)]
	public ref class csOptionsFlash
	{
	public:
		BYTE uManufacturer;
		BYTE uMemoryType;
		BYTE uMemoryCapacity;
		BYTE uReserved;
	};
	[StructLayout(LayoutKind::Sequential)]
	public ref class csVersion
	{
	public:
		BYTE uMinorLSB;
		BYTE uMinorMSB;
		BYTE uMajorLSB;
		BYTE uMajorMSB;
	};

#pragma endregion structures
//////////////////////////////////////////////////////////////////////////
#pragma region delegates
	public delegate int TypeAcquisitionAscan_0x00010103(Object ^pAcquisitionParameter,csAcqInfoEx^ %acqInfo,csHeaderStream_0x0001^ %StreamHeader,csSubStreamAscan_0x0103^ %ascanHeader,const void* pBufferMax,const void* pBufferMin,const void* pBufferSat);
	public delegate int TypeAcquisitionAscan_0x00020203(Object^ pAcquisitionParameter, csHeaderStream_0x0002^% StreamHeader, csSubStreamAscan_0x0203^% ascanHeader, const void* pBufferMax, const void* pBufferMin, const void* pBufferSat);
	public delegate int TypeAcquisitionCscan_0x00010X02(Object ^pAcquisitionParameter,csAcqInfoEx^ %acqInfo,csHeaderStream_0x0001^ %streamHeader,csSubStreamCscan_0x0X02^ %cscanHeader,cli::array<csCscanAmp_0x0102^>^ %bufferAmp,cli::array<csCscanAmpTof_0x0202^>^ %bufferAmpTof);
	public delegate int TypeAcquisitionCscan_0x00020402(Object^ pAcquisitionParameter, csAcqInfoEx^% acqInfo, csHeaderStream_0x0002^% streamHeader, csSubStreamCscan_0x0402^% cscanHeader, cli::array<csCscanAmpTof_0x0402^>^% bufferAmpTof);
	public delegate int TypeAcquisitionIO_0x00010101(Object ^pAcquisitionParameter,csHeaderStream_0x0001^ %streamHeader,csHeaderIO_0x0001^ %ioHeader);
	public delegate int TypeAcquisitionIO_1x00010101(Object ^pAcquisitionParameter,csAcqInfoEx^ %acqInfo,csHeaderStream_0x0001^ %streamHeader,csHeaderIO_0x0001^ %ioHeader);
	public delegate int TypeAcquisitionInfo(Object ^pAcquisitionParameter,String ^pInfo);

	public delegate int TypeCallbackCustomizedDriverAPI(Object ^pAcquisitionParameter,csEnumStepCustomizedAPI %eStepCustomizedAPI,String ^pInfo,int %iCycleCount);

	public delegate void TypeCallbackSystemMessageBox(const String ^pMsg);
	public delegate void TypeCallbackSystemMessageBoxList(const String ^pMsg);
	public delegate csEnumMsgBoxReturn TypeCallbackSystemMessageBoxButtons(const String ^pMsg,const String ^pTitle,csEnumMsgBoxButtons nType);//should return IDOK IDYES IDNO...depending of the button pressed by the user.
	public delegate csEnumMsgBoxReturn TypeCallbackOempaApiMessageBox(/*HWND hWnd,*/const String ^lpszText,const String ^lpszCaption,csEnumMsgBoxButtons nType);
	ref class csHWDevice;
	public delegate void TypeCallbackHWMemory(csHWDevice ^hwDevice, bool bMaster, unsigned long addr, unsigned long data, unsigned long size);


#pragma endregion delegates
//////////////////////////////////////////////////////////////////////////
#pragma region pin_list
	public ref class csPinListItem
    {
	public:
		csPinListItem(Object^ object_){
			pin_object = object_;
			pin_handle = GCHandle::Alloc(object_, GCHandleType::Pinned);
			pin_pointer = pin_handle.AddrOfPinnedObject();
		};
		~csPinListItem(){Free();};
		void Free()
		{
			if((pin_object!=nullptr) && (pin_pointer!=(IntPtr)NULL))
			{
				pin_handle.Free();
				pin_pointer = (IntPtr)NULL;
			}
		}
		void *Pointer(){
			if(pin_pointer==(IntPtr)NULL)
				return NULL;
			return (void*)pin_pointer.ToPointer();
		}
	private:
		Object^ pin_object;
		GCHandle pin_handle;
		IntPtr pin_pointer;
	protected:
		!csPinListItem(){Free();};
	};
	public ref class csPinList
    {
	private:
		List<csPinListItem^> list;
	public:
		csPinList(){};
		~csPinList(){Free();};
		void Free()
		{
			for each(csPinListItem^ st in list)
			{
				st->Free();
				delete st;
			}
			list.Clear();
		};
		void* Add(Object^ object){
			csPinListItem^ x=gcnew csPinListItem(object);
			list.Add(x);
			return x->Pointer();
		};

	protected:
		!csPinList(){Free();};
	};

	public ref class csDac
	{
	public:
		csDac(double time,float slope)
		{dTime=time;fSlope=slope;};
		double dTime;
		float fSlope;
	};

	public ref class acsDac
	{
	public:
		cli::array<csDac^>^ list;
		acsDac()
		{
			m_pointer=new gcroot<acsDac^>(this);
		};
		~acsDac()
		{
			delete m_pointer;
			m_pointer = NULL;
		};
		csDac^ operator[](int a)
		{
			return list[a];
		};
		gcroot<acsDac^>* GetGcroot(){return m_pointer;}
	private:
		gcroot<acsDac^>* m_pointer;//Array type can't be inherited. Array can't be extended.
	};

	public ref class acsDouble
	{
	public:
		cli::array<double>^ list;
		acsDouble()
		{
			m_pointer=new gcroot<acsDouble^>(this);
		};
		~acsDouble()
		{
			delete m_pointer;
			m_pointer = NULL;
		};
		gcroot<acsDouble^>* GetGcroot(){return m_pointer;}
	private:
		gcroot<acsDouble^>* m_pointer;//Array type can't be inherited. Array can't be extended.
	};

	public ref class acsFloat
	{
	public:
		cli::array<float>^ list;
		acsFloat()
		{
			m_pointer=new gcroot<acsFloat^>(this);
		};
		~acsFloat()
		{
			delete m_pointer;
			m_pointer = NULL;
		};
		gcroot<acsFloat^>* GetGcroot(){return m_pointer;}
	private:
		gcroot<acsFloat^>* m_pointer;//Array type can't be inherited. Array can't be extended.
	};

	public ref class acsDelayReception
	{
	public:
		cli::array<float,2>^ list;
		acsDelayReception()
		{
			m_pointer=new gcroot<acsDelayReception^>(this);
		};
		~acsDelayReception()
		{
			delete m_pointer;
			m_pointer = NULL;
		};
		gcroot<acsDelayReception^>* GetGcroot(){return m_pointer;}
	private:
		gcroot<acsDelayReception^>* m_pointer;//Array type can't be inherited. Array can't be extended.
	};

	public ref class acsByte
	{
	public:
		cli::array<BYTE>^ list;
		acsByte()
		{
			m_pointer=new gcroot<acsByte^>(this);
		};
		~acsByte()
		{
			delete m_pointer;
			m_pointer = NULL;
		};
		gcroot<acsByte^>* GetGcroot(){return m_pointer;}
	private:
		gcroot<acsByte^>* m_pointer;//Array type can't be inherited. Array can't be extended.
	};
public ref class csCProcessingTime
{//ljr 2016
public:
	csCProcessingTime()
	{
		LARGE_INTEGER x;
		QueryPerformanceFrequency(&x);
		m_llFrequency = x.QuadPart;
		Start();
	};
	void Start()
	{
		LARGE_INTEGER x;
		QueryPerformanceCounter(&x);
		m_llTimeStart = x.QuadPart;
		m_llTimeStop = m_llTimeStart;
	};
	void Stop()
	{
		LARGE_INTEGER x;
		QueryPerformanceCounter(&x);
		m_llTimeStop = x.QuadPart;
	};
	double GetProcessingTime()
	{
		double dProcessingTime = (double)(m_llTimeStop-m_llTimeStart)/(double)(m_llFrequency);
		return dProcessingTime;
	};
private:
	int64_t m_llFrequency;
	int64_t m_llTimeStart;
	int64_t m_llTimeStop;//LARGE_INTEGER
};

#pragma endregion pin_list
//////////////////////////////////////////////////////////////////////////
	public ref class csKernelDriver
    {
	public:
		static void GetVersion([Out] String^ %pMsg);
		static char GetVersionLetter();
		static bool CrtCheckMemory();
		static bool CrtSetDbgFlag(bool bEnable);
		//static void debug_EnableHeapEx(bool bEnable, String ^pFileName);
		//static bool debug_DumpHeap(String ^pFileName,bool bStatistics);
	};
	public ref class csMsgBox
    {
	private:
		static TypeCallbackSystemMessageBox ^g_CallbackSystemMessageBox;
		static TypeCallbackSystemMessageBoxList ^g_CallbackSystemMessageBoxList;
		static TypeCallbackSystemMessageBoxButtons ^g_CallbackSystemMessageBoxButtons;
		static TypeCallbackOempaApiMessageBox ^g_CallbackOempaApiMessageBox;
		static csMsgBox();
	public:
		//To call the functions used by the driver to display the popup.
		static void SystemMessageBox(String ^pMsg);
		static void SystemMessageBoxList(String ^pMsg);
		static csEnumMsgBoxReturn SystemMessageBoxButtons(String ^pMsg,String ^pTitle,csEnumMsgBoxButtons nType);
		static csEnumMsgBoxReturn OempaApiMessageBox(String ^pMsg,String ^pTitle,csEnumMsgBoxButtons nType);

		//function to register callback to avoid the popup.
		static void SetCallbackSystemMessageBox(TypeCallbackSystemMessageBox ^pCallback);
		static void SetCallbackSystemMessageBoxList(TypeCallbackSystemMessageBoxList ^pCallback);
		static void SetCallbackSystemMessageBoxButtons(TypeCallbackSystemMessageBoxButtons ^pCallback);
		static void SetCallbackOempaApiMessageBox(TypeCallbackOempaApiMessageBox ^pCallback);

		static TypeCallbackSystemMessageBox ^GetCallbackSystemMessageBox();
		static TypeCallbackSystemMessageBoxList ^GetCallbackSystemMessageBoxList();
		static TypeCallbackSystemMessageBoxButtons ^GetCallbackSystemMessageBoxButtons();
		static TypeCallbackOempaApiMessageBox ^GetCallbackOempaApiMessageBox();

		static void CallbackSystemMessageBox(const wchar_t *pMsg);
		static void CallbackSystemMessageBoxList(const wchar_t *pMsg);
		static unsigned int CallbackSystemMessageBoxButtons(const wchar_t *pMsg,const wchar_t *pTitle,UINT nType);
		static int CallbackOempaApiMessageBox(HWND hWnd,LPCTSTR lpszText,LPCTSTR lpszCaption,UINT nType);

		static bool IsUserInterfaceThread();//this function could be called to know if the current thread is attached to the management of window.
	};
	ref class csHWDeviceOEMPA;
	public ref class csCustomizedOEMPA
    {
	private:
		CHWDeviceOEMPA *m_pHWDeviceOEMPA;
		Object ^m_csAcquisitionParameter;
		structRoot *m_pRoot;
		structCycle *m_pCycle;
		CFocalLaw *m_pEmission,*m_pReception;
		int m_iCycleCount;
		void Constructor(CHWDeviceOEMPA *pHWDeviceOEMPA);
	public:
		csCustomizedOEMPA(CHWDeviceOEMPA *pHWDeviceOEMPA);
		csCustomizedOEMPA(System::Void *pHWDeviceOEMPA);
		~csCustomizedOEMPA();
		void Free();

		bool ReadFileWriteHW(String ^pValue);
		bool ReadHWWriteFile(String ^pValue);
		bool WriteHW(csHWDeviceOEMPA^ %pHWDeviceOEMPA,csRoot^ %root,cli::array<csCycle^>^ %cycle,cli::array<csFocalLaw^>^ %emission,cli::array<csFocalLaw^>^ %reception,csEnumAcquisitionState eAcqState);

	//callback functions
		bool GetRoot([Out] csRoot^ %root);
		bool SetRoot([In] csRoot^ %root);
		bool GetCycle(int iCycle,[Out] csCycle^ %cycle);
		bool SetCycle(int iCycle,[In] csCycle^ cycle);
		bool GetFocalLaw(bool bEmission,int iCycle,[Out] csFocalLaw^ %focalLaw);
		bool SetFocalLaw(bool bEmission,int iCycle,[In] csFocalLaw^ focalLaw);

	//callback management
		bool SetCallbackCustomizedDriverAPI(TypeCallbackCustomizedDriverAPI ^pProcess);
		TypeCallbackCustomizedDriverAPI ^GetCallbackCustomizedDriverAPI();
		TypeCallbackCustomizedDriverAPI ^m_csCallback;
		bool SetAcquisitionParameter(Object ^pParameter);//first parameter of the callback acquisition function.
		Object ^GetAcquisitionParameter();
		void CallbackCustomizedAPI(const wchar_t *pFileName,enumStepCustomizedAPI eStepCustomizedAPI,structRoot *pRoot,structCycle *pCycle,CFocalLaw *pEmission,CFocalLaw *pReception);

	protected:
		!csCustomizedOEMPA();
	};

//////////////////////////////////////////////////////////////////////////
#pragma region csSWEncoder
    public ref class csSWEncoder
    {
	private:
		//CHWDevice *m_pHWDevice;
		CSWEncoder *m_pSWEncoder;
		void Constructor(CSWEncoder *pSWEncoder);
	public:
		csSWEncoder(CSWEncoder *pSWEncoder);
		~csSWEncoder();
		void Free();

	//setting part
		bool Enable(bool bEnabled);//value could be negative if encoder need to be inverted.
		bool IsEnabled();

		bool lSetResolution(long lResolution);//this function is only for integer type resolution (the resolution divider is equal to 1).
		bool lSetResolution(long lResolution,DWORD dwDivider);//divider is useful for a double type resolution encoder. DoubleResolution=Resolution/Divider.
		long lGetResolution();
		DWORD GetDivider();
		bool dSetResolution(double dResolution);//it is the same than to call "ConvertResolution(dResolution,lResolution,dwDivider);" followed by "SetResolution(lResolution,dwDivider)"
		double dGetResolution();

		bool SetUnit(csEnumUnit eUnit);
		csEnumUnit GetUnit();

		bool SetType(csEnumEncoderType eEncoderType);
		csEnumEncoderType GetType();

		bool SetDigitalInput(csEnumFeatureDigitalInput eEncoderInput,csEnumDigitalInput eDigitalInput);
		csEnumDigitalInput GetDigitalInput(csEnumFeatureDigitalInput eEncoderInput);

	//acquisition part
		bool SetSpeedDistance(double dValue);
		bool GetSpeedDistance([Out] double %dValue);

		bool SetInspectionHWValue(int iValue);
		int GetInspectionHWValue();
		bool SetInspectionSWValue(double dValue);
		double GetInspectionSWValue();

		bool SetInspectionSpeed(double dValue);
		double GetInspectionSpeed();

		bool SetInspectionLength(double dValue);
		double GetInspectionLength();

		bool SetInspectionCount(int iValue);
		int GetInspectionCount();

	protected:
		!csSWEncoder();
	};
#pragma endregion csSWEncoder

#pragma region csSWDevice
	public ref class csSWDevice
    {
	private:
		csSWEncoder ^m_csSWEncoder1;
		csSWEncoder ^m_csSWEncoder2;
		CHWDeviceOEMPA *m_pHWDeviceOEMPA;
		CHWDevice *m_pHWDevice;
		CSWDevice *m_pSWDevice;
	public:
		csSWDevice();
		~csSWDevice();
		void Constructor(CHWDeviceOEMPA *pHWDeviceOEMPA,CHWDevice *pHWDevice);
		void Free();
		csSWEncoder ^GetSWEncoder(int iEncoderIndex);

		csEnumHardware GetHardware();

	//root ID management.
		bool SetBoardName(String ^pName);//to register this new data in the kernel database.
		bool GetBoardName([Out] String^ %pName);

	//Setting
		bool GetConfigurationFilePath(bool bFlashName,String ^pPathName);//to find the configuration file path.

		bool GetSetupFileDefault(csEnumSetupFileType csFileType, [Out] String^ %wcFile);
		bool SetSetupFileCurrent(String ^wcFile);
		bool GetSetupFileCurrent([Out] String^ %wcFile);

		//bool SetAcquisitionFlush(enumAcquisitionFlush eAcquisitionFlush);
		//enumAcquisitionFlush GetAcquisitionFlush();

		bool SetConnectionState(csEnumConnectionState eConnection,bool bDisplayErrorMsg);
		csEnumConnectionState GetConnectionState();
		bool IsConnected();

		bool CheckProcessId();//only one process have ownership of the driver.

		bool SetCfgStatus(csEnumUpdateStatus eUpdateStatus);
		csEnumUpdateStatus GetCfgStatus();

		int SetStreamCount(int iCount);
		int GetStreamCount();
		int SetStreamDataSize(uint64_t ui64DataSize);
		uint64_t GetStreamDataSize();
		int SetStreamRetransmit(int iCount);
		int GetStreamRetransmit();
		int SetStreamError(int iCount);
		int GetStreamError();

		int GetLostCountAscan();
		int SetLostCountAscan(int iDataLostCount);
		int GetLostCountCscan();
		int SetLostCountCscan(int iDataLostCount);
		int GetLostCountEncoder();
		int SetLostCountEncoder(int iDataLostCount);
		int GetLostCountUSB3();
		int SetLostCountUSB3(int iDataLostCount);
		int GetLostCountSocket();
		int SetLostCountSocket(int iDataLostCount);
		int GetErrorCountUSB3();
		int GetLostCountFifo(csEnumAcquisitionFifo csFifo);//data (ascan, cscan or IO) lost because the output speed to get data is too low.
		void ResetCounters();

		//bool SetEncoderCount(int iCount);//0 1 or 2.
		int GetEncoderEnabledCount();
		void swProcessEncoder(structAcqInfoEx &acqInfo);

		bool EnablePulser(bool bEnable);
		bool IsPulserEnabled();

		bool SetLockDefaultDisablePulser(bool bDisable);
		bool GetLockDefaultDisablePulser();
		bool SetUnlockDefaultEnablePulser(bool bEnable);
		bool GetUnlockDefaultEnablePulser();

		bool SetAcqSpeedAscan(double dSpeed);
		double GetAcqSpeedAscan();
		bool SetAcqSpeedCscan(double dSpeed);
		double GetAcqSpeedCscan();
		bool SetAcqSpeedIO(double dSpeed);
		double GetAcqSpeedIO();

		csEnumCommunication GetCommunication();
	protected:
		!csSWDevice();
	};
#pragma endregion csSWDevice

#pragma region csHWDevice
	ref class csAcquisitionFifo;
	public ref class csHWDevice
    {
	private:
		csPinList ^m_List;
		csSWDevice ^m_csSWDevice;
		CHWDevice *m_pHWDevice;
		CSWDevice *m_pSWDevice;
		Object ^m_csAcquisitionParameter;
		csCustomizedOEMPA ^m_csCustomizedOEMPA;
		bool m_bDigitalEdgesOnly;
	public:
		csHWDevice();
		~csHWDevice();
		void Constructor(CHWDeviceOEMPA *pHWDeviceOEMPA,CHWDevice *pHWDevice);
		void Constructor(System::Void *_pHWDeviceOEM,System::Void *_pHWDevice);
		void Constructor(csCustomizedOEMPA ^_csCustomizedAPI);
		void Free();
		csSWDevice ^GetSWDevice();
		int GetDeviceId();
		static int GetMonitorPort(int iDeviceId);

//MultiSystem management start (shared functions between applications)
	csEnumHardwareLink GetHardwareLink();
	int GetMasterDeviceId();

	//FUNCTION to call before connection (this information is registered in the default setup OEMPA file)
	bool SetDefaultHwLink(csEnumHardwareLink csHardwareLink,[Out] bool %bPreviousMasterUnregistered);
	csEnumHardwareLink GetDefaultHwLink();
	bool IsDefaultHwLinkEnabled();

	//FUNCTION to call after connection
	//DO NOT CALL LockDevice/UnlockDevice to call following:
	bool SlaveConnect(int iMasterDeviceId);
	bool SlaveDisconnect();

	//Device low level management
		//Those function can be used to receive acquisition data.
		bool SetAcquisitionParameter(Object ^pParameter);//first parameter of the callback acquisition function.
		Object ^GetAcquisitionParameter();
		bool SetAcquisitionAscan_0x00010103(TypeAcquisitionAscan_0x00010103 ^pProcess);
		TypeAcquisitionAscan_0x00010103 ^GetAcquisitionAscan_0x00010103();
		bool SetAcquisitionAscan_0x00020203(TypeAcquisitionAscan_0x00020203^ pProcess);
		TypeAcquisitionAscan_0x00020203^ GetAcquisitionAscan_0x00020203();
		bool SetAcquisitionCscan_0x00010X02(TypeAcquisitionCscan_0x00010X02 ^pProcess);
		TypeAcquisitionCscan_0x00010X02 ^GetAcquisitionCscan_0x00010X02();
		bool SetAcquisitionCscan_0x00020402(TypeAcquisitionCscan_0x00020402^ pProcess);
		TypeAcquisitionCscan_0x00020402^ GetAcquisitionCscan_0x00020402();
		bool SetAcquisitionIO_0x00010101(TypeAcquisitionIO_0x00010101 ^pProcess,bool bDigitalEdgesOnly);
		TypeAcquisitionIO_0x00010101 ^GetAcquisitionIO_0x00010101([Out] bool^ %bDigitalEdgesOnly);
		bool SetAcquisitionIO_1x00010101(TypeAcquisitionIO_1x00010101 ^pProcess,bool bDigitalEdgesOnly);
		TypeAcquisitionIO_1x00010101 ^GetAcquisitionIO_1x00010101([Out] bool^ %bDigitalEdgesOnly);

		virtual csAcquisitionFifo ^GetAcquisitionFifo(csEnumAcquisitionFifo csFifo);

		bool SetAcquisitionInfo(TypeAcquisitionInfo ^pProcess);
		TypeAcquisitionInfo ^GetAcquisitionInfo();

		bool IsDriverEncoderManagementEnabled();
		void EnableDriverEncoderManagement(bool bEnable);

	//lock/unlock the device
	//you have to lock the device before to use its functions (for example "SetStandby" function).
	//(the communication link with the HW is shared between all threads, only one at a time
	//can lock it)
		bool LockDevice();
		bool LockDevice(csEnumAcquisitionState eAcqState);//use "eAcqOff" to disable shot pulse.
						//eAcqDefault is used to read the value from the configuration file.
						//return true if no error and false in case of communication error.
		bool UnlockDevice();
		bool UnlockDevice(csEnumAcquisitionState eAcqState);//use "eAcqOn" to enable shot pulse again.
						//eAcqDefault is used to read the value from the configuration file.
						//return true if no error and false in case of communication error.
		bool IsDeviceLocked();
		DWORD GetSettingId();

	//management of the device (can be called after device has been locked).
		bool Flush();//return true if no error and false in case of communication error.

	//Don't use those very low level functions, they are reserved for advanced user.
		bool WriteHW(DWORD dwAddress,DWORD dwData,int iSize);
		bool WriteHW(int iCount,DWORD dwAddress,DWORD *pdwData,int iSize);
		/*unsafe*/bool ReadHW(DWORD dwAddress,[Out] DWORD *pdwData,int iSize);
		/*unsafe*/bool ReadHW(int iCount,DWORD dwAddress,[Out] DWORD *pdwData,int iSize);
		bool SetCallbackHWMemory(TypeCallbackHWMemory ^callbackHWMemory);
		TypeCallbackHWMemory ^GetCallbackHWMemory();

	//callback management
		gcroot<csHWDevice^>* m_pointer;

		TypeAcquisitionAscan_0x00010103 ^m_csAscan;
		TypeAcquisitionAscan_0x00020203^ m_csAscan2;
		TypeAcquisitionCscan_0x00010X02 ^m_csCscan;
		TypeAcquisitionCscan_0x00020402^ m_csCscan4;
		TypeAcquisitionIO_0x00010101 ^m_csIo0;
		TypeAcquisitionIO_1x00010101 ^m_csIo1;
		TypeAcquisitionInfo ^m_csInfo;
		TypeCallbackHWMemory ^g_CallbackHWMemory;

		int AcquisitionAscan_0x00010103(structAcqInfoEx &acqInfo,const CStream_0x0001 *pStreamHeader,const CSubStreamAscan_0x0103 *pAscanHeader,const void *pBufferMax,const void *pBufferMin,const void *pBufferSat);
		int AcquisitionAscan_0x00020203(/*structAcqInfoEx &acqInfo,*/const CStream_0x0002* pStreamHeader, const CSubStreamAscan_0x0203* pAscanHeader, const void* pBufferMax, const void* pBufferMin, const void* pBufferSat);
		int AcquisitionCscan_0x00010X02(structAcqInfoEx &acqInfo,const CStream_0x0001 *pStreamHeader,const CSubStreamCscan_0x0X02 *pCscanHeader,const structCscanAmp_0x0102 *pBufferAmp, const structCscanAmpTof_0x0202 *pBufferAmpTof);
		int AcquisitionCscan_0x00020402(structAcqInfoEx& acqInfo, const CStream_0x0002* pStreamHeader, const CSubStreamCscan_0x0402* pCscanHeader, const structCscanAmpTof_0x0402* pBufferAmpTof);
		int AcquisitionIO_0x00010101(const CStream_0x0001 *pStreamHeader,const CSubStreamIO_0x0101 *pIOHeader);
		int AcquisitionIO_1x00010101(structAcqInfoEx &acqInfo,const CStream_0x0001 *pStreamHeader,const CSubStreamIO_0x0101 *pIOHeader);
		int AcquisitionInfo(const wchar_t *pInfo);
		void CallbackCustomizedAPI(const wchar_t *pFileName,enumStepCustomizedAPI eStepCustomizedAPI,structRoot *pRoot,structCycle *pCycle,CFocalLaw *pEmission,CFocalLaw *pReception);
		void CallbackHWMemory(bool bMaster, unsigned long addr, unsigned long data, int size);
		void* ListAddObject(Object^ object);
		void test();
	protected:
		!csHWDevice();
	};
#pragma endregion hwDriver

//////////////////////////////////////////////////////////////////////////
#pragma region csSWFilterOEMPA
   public ref class csSWFilterOEMPA
    {
	private:
		CHWDeviceOEMPA *m_pHWDeviceOEMPA;
		CSWDeviceOEMPA *m_pSWDeviceOEMPA;
		CSWFilterOEMPA *m_pSWFilterOEM;
	public:
		csSWFilterOEMPA();
		~csSWFilterOEMPA();
		void Constructor(CHWDeviceOEMPA *pHWDeviceOEMPA,CSWDeviceOEMPA *pSWDeviceOEM,CSWFilterOEMPA *pSWFilterOEM);
		void Free();

		bool SetFilter(csEnumOEMPAFilter eFilter);
		bool GetFilter([Out] csEnumOEMPAFilter %eFilter);
		bool SetTitle(String^ pValue);//useful for custom filter.
		bool GetTitle([Out] String^ %pValue);

		//custom filter coefficient: functions used by the "CustomFilter" software.
		bool SetScale(WORD wScale);
		bool GetScale([Out] WORD %wScale);
		bool SetCoefficientCount(int iCoefficientCount);
		bool GetCoefficientCount([Out] int %iCoefficientCount);
		bool SetCoefficient(int iCoefficientIndex,short wValue);
		bool GetCoefficient(int iCoefficientIndex,[Out] short %wValue);
		bool SetFilter(WORD wScale,cli::array<short>^ wValue,bool bUpdateHardware);//if you want to update all hardware filter, it is quicker to call "CSWDeviceOEMPA1::UpdateAllFilter" at the end and before to call "SetFilter" with "bUpdateHardware=false" for all filters.
		bool GetFilter([Out] WORD %wScale,[Out] cli::array<short>^ %wValue);
	protected:
		!csSWFilterOEMPA();
	};
#pragma endregion csSWFilterOEMPA

#pragma region csSWDeviceOEMPA
    public ref class csSWDeviceOEMPA
    {
	private:
		cli::array<csSWFilterOEMPA^> ^m_acsSWFilterOEM;
		CHWDeviceOEMPA *m_pHWDeviceOEMPA;
		CSWDeviceOEMPA *m_pSWDeviceOEMPA;
	public:
		csSWDeviceOEMPA();
		~csSWDeviceOEMPA();
		void Constructor(CHWDeviceOEMPA *pHWDeviceOE,CSWDeviceOEMPA *pSWDeviceOEM);
		void Free();
		csSWFilterOEMPA^ Filter(int iFilterIndex);

		virtual bool IsPulserEnabled();

		virtual bool SetAddress(String ^pValue);
		virtual bool GetAddress([Out] String^ %pValue);

		virtual bool IsUSB3Connected();

		virtual bool GetSerialNumber([Out] String^ %pSN);
		virtual bool GetSystemType([Out] String^ %pType);
		virtual int GetRXBoardCount();

		virtual int GetApertureCountMax();//to get the maximum element count of an aperture.
		virtual int GetElementCountMax();//to get the maximum element count of the system (in case of mux).

		virtual const double dGetClockPeriod();//ns
		virtual const float fGetClockPeriod();//ns
		virtual const long lGetClockFrequency();//Hz

		virtual WORD GetFirmwareId();

		virtual bool IsFullMatrixCapture();//FMC could be enabled or disabled.
		virtual bool IsFullMatrixCaptureReadWrite();//FMC bit could be Read/Write or Read only (for old FMC FW: FMC bit is not read only).
		virtual bool GetFMCElement([Out] int %iElementStart, [Out] int %iElementStop, [Out] int %iElementStep);
		virtual bool IsMultiHWChannelSupported();
		virtual bool IsTemperatureAlarmSupported();
		virtual bool IsMultiHWChannelEnabled();
		virtual bool IsMatrixAvailable();
		virtual bool IsLabviewAvailable();
		virtual bool IsTpacquisitionAvailable();
		virtual bool IsWTSWAvailable();
		virtual bool IsEncoderDecimal();
		virtual bool IsFMCElementStepSupported();

		virtual bool SetKeepAlive(csEnumKeepAlive eKeepAlive);
		virtual csEnumKeepAlive GetKeepAlive();

		virtual bool EnableAscan(bool bEnable);
		virtual bool IsAscanEnabled();

		virtual bool SetAscanBitSize(csEnumBitSize eBitSize);
		virtual csEnumBitSize GetAscanBitSize();

		virtual bool SetAscanRequest(csEnumAscanRequest eAscanRequest);
		virtual csEnumAscanRequest GetAscanRequest();

		virtual bool SetAscanRequestFrequency(double dFreq);//Hz
		virtual bool GetAscanRequestFrequency([Out] double %dFreq);

		virtual bool EnableCscanTof(bool bEnable);
		virtual bool IsCscanTofEnabled();

		virtual bool SetCycleCount(int iCount);
		virtual int GetCycleCount();

		virtual bool SetTriggerMode(csEnumOEMPATrigger eTrig);
		virtual csEnumOEMPATrigger GetTriggerMode();

		virtual bool SetTriggerEncoderStep(double dStep);
		virtual bool GetTriggerEncoderStep([Out] double %dStep);

		virtual bool SetSignalTriggerHighTime(double dTime);
		virtual double GetSignalTriggerHighTime();

		virtual bool SetRequestIO(csEnumOEMPARequestIO eRequest);
		virtual csEnumOEMPARequestIO GetRequestIO();

		virtual bool SetRequestIODigitalInputMaskRising(int iMask);
		virtual bool GetRequestIODigitalInputMaskRising([Out] int %iMask);

		virtual bool SetRequestIODigitalInputMaskFalling(int iEvent);
		virtual bool GetRequestIODigitalInputMaskFalling([Out] int %iEvent);

		virtual bool SetExternalTriggerCycle(csEnumDigitalInput eDigitalInput);
		virtual bool GetExternalTriggerCycle([Out] csEnumDigitalInput %eDigitalInput);

		virtual bool SetExternalTriggerSequence(csEnumDigitalInput eDigitalInput);
		virtual bool GetExternalTriggerSequence([Out] csEnumDigitalInput %eDigitalInput);

		virtual bool SetExternalTimestampReset(csEnumDigitalInput eDigitalInput);
		virtual bool GetExternalTimestampReset([Out] csEnumDigitalInput% eDigitalInput);

		virtual bool SetMappingOutput(int iOutputIndex,csEnumOEMPAMappingDigitalOutput eMapping);
		virtual bool GetMappingOutput(int iOutputIndex,[Out] csEnumOEMPAMappingDigitalOutput %eMapping);

		virtual bool SetRequestCscan(csEnumOEMPARequestCscan eRequest);
		virtual csEnumOEMPARequestCscan GetRequestCscan();

		virtual bool SetEncoderDebouncer(double dTime);
		virtual double GetEncoderDebouncer();

		virtual WORD GetDigitalInput();

		virtual bool SetDigitalDebouncer(double dTime);
		virtual double GetDigitalDebouncer();

		virtual bool ResetEncoder();

		virtual bool GetTemperatureCount([Out] int %iBoardCount,[Out] int %iSensorCountMax);
		virtual bool GetTemperatureSensorCount(int iBoardIndex,[Out] int %iSensorCount);
		virtual bool GetTemperature(int iBoardIndex,int iSensorIndex,[Out] float %fValue);

		virtual bool IsIOBoardEnabled();
		virtual bool IsOEMMCEnabled();
		virtual double GetPulserPowerMax();
		virtual double GetPulserPowerCurrent();
		virtual BYTE GetFlashUSB3Version();
		virtual DWORD GetFWUSB3Version();
		virtual bool EnableUSB3(bool bEnable);
		virtual bool IsUSB3Enabled();
		virtual DWORD GetMBOptions();

		virtual bool GetEmbeddedVersion([Out] csVersion^ %version);
		virtual bool GetOptionsCom([Out] csOptionsCom^ %optionsCom);
		virtual bool GetOptionsTCP([Out] csOptionsTCP^ %optionsTCP);
		virtual bool GetOptionsFlash([Out] csOptionsFlash^ %optionsFlash);
		virtual int GetPasscodeCount();
		virtual bool GetPasscode(int iIndex,[Out] DWORD %dwPasscode);
		virtual double GetMaximumThroughput();//Unit is Bytes/second.

		virtual bool UpdateAllFilter();//to update all hardware filters at the same time.

		static void EnableMultiChannel(bool bEnable);//you need to restart the sw to take the new value in account. "CUTChannels::SetDefaultMultiChannels" is the same.
		static bool IsMultiChannelEnabled();
		static void EnableLoadDefaultSetup(bool bEnable);//you need to restart the sw to take the new value in account. "CUTPreference::SetDefaultSetupAuto" is the same.
		static bool IsLoadDefaultSetupEnabled();

		virtual bool IsSubSequenceAverageSupported();
		virtual bool IsTimeOffsetSupported();
		virtual bool EnableAlignment(bool bEnable);
		virtual float GetCalibrationAlignment();//maximum correction.
		virtual float GetCalibrationOffset();//required value after calibration has been performed.
		virtual bool IsCalibrationPerformed();//same than "IsAlignmentPerformed"
		virtual bool IsAlignmentPerformed();
		virtual bool IsAlignmentEnabled();
		virtual bool ResetAlignment();

		virtual void SetCalibrationParameters(float fWidth,float fStart,float fRange,float fGainAnalog,double dGainDigital);
		virtual void GetCalibrationParameters(float &fWidth,float &fStart,float &fRange,float &fGainAnalog,double &dGainDigital);
		virtual bool SetCalibrationFileReport(String ^pFileReport);
		virtual bool GetCalibrationFileReport([Out] String^ %pFileReport);

		//The calibration time offset is internally added to RecoveryTimes.
		virtual bool SetTimeOffset(float fTimeOffset);
		virtual float GetTimeOffset();//current value used to correct time axis.

		//For OEM-MC or OEM-PA (VF).
		virtual void EnablePulserDuringReplay(bool bEnable);
		virtual bool IsPulserDuringReplayEnabled();
		virtual bool EnableCscanTimeOfFlightCorrection(bool bEnable);
		virtual bool IsCscanTimeOfFlightCorrectionEnabled();
		virtual bool GetCscanTimeOfFlightCorrection(int iCycle,BYTE %byDecimation,float %fAscanStart);//decimation range is 1..16.
		virtual bool SetCscanTimeOfFlightCorrection(int iCycle,BYTE byDecimation,float fAscanStart);//decimation range is 1..16.

		virtual double GetFWAscanRecoveryTime();
		virtual double GetFMCSubCycleRecoveryTime();
		virtual double GetFMCCycleRecoveryTime();
	protected:
		!csSWDeviceOEMPA();
	};
#pragma endregion csSWDeviceOEMPA

#pragma region csHWDeviceOEMPA
	public ref class csHWDeviceOEMPA : public csHWDevice
    {
	private:
		bool m_bExternCustomizedAPI;
		csCustomizedOEMPA ^m_csCustomizedOEMPA;
		csSWDeviceOEMPA ^m_csSWDeviceOEM;
		CHWDeviceOEMPA *m_pHWDeviceOEMPA;
		CHWDevice *m_pHWDevice;
		CSWDeviceOEMPA *m_pSWDeviceOEMPA;
		csAcquisitionFifo ^m_FifoAscan;
		csAcquisitionFifo ^m_FifoCscan;
		csAcquisitionFifo ^m_FifoIO;
		void Constructor(csEnumHardware csHW, bool bExternCustomizedAPI, int iOption);
	public:
		csHWDeviceOEMPA(csEnumHardware csHW);
		csHWDeviceOEMPA(csEnumHardware csHW, bool bExternCustomizedAPI, bool bTCP);
		~csHWDeviceOEMPA();
		void Free();
		System::Void* cGetHWDeviceOEMPA();
		csHWDeviceOEMPA^ GetHWDeviceOEMPA();
		csSWDeviceOEMPA^ GetSWDeviceOEMPA();
		csHWDevice ^GetHWDevice();
		csCustomizedOEMPA ^GetCustomizedOEMPA();
		bool SetCustomizedOEMPA(csCustomizedOEMPA ^pCustomizedOEM);

		//MultiProcess management begin
		static bool IsMultiProcessRegistered();
		static bool RegisterMultiProcess(String ^wcProcessName);//call this function before instantiating any device.
		static bool UnregisterMultiProcess();
		static bool IsMultiProcessConnected(String ^wcAddress,[Out] DWORD %dwProcessId);//call this function to know if another process has been already connected with the device.
				//Input "wcAddress": (IP) address of the device.
				//Output "dwProcessId": process ID of the process that has already been connected with the device.
				//return true if any other process has already been connected with the device, false otherwise.
		static bool DisconnectMultiProcess(String ^wcAddress,DWORD dwProcessId);//call this function to disconnect the device of another process (.
				//Input "wcAddress": (IP) address of the device.
				//Input "dwProcessId": process ID of the process that has already been connected with the device.
				//return true if the device has been disconnected, false otherwise.
		static int GetMultiProcessCount();//to get the count of other process that has been registered with function "RegisterMultiProcess".
		static bool GetMultiProcessInfo(int iIndex,[Out] DWORD %dwProcessId,[Out] String ^wcProcessName);//to get information of other process that has been registered with function "RegisterMultiProcess".
		//MultiProcess management end

		virtual csAcquisitionFifo ^GetAcquisitionFifo(csEnumAcquisitionFifo csFifo) override;

		/*unsafe*/virtual bool GetDigitalInput(DWORD *pdwData);//reading the current state of digital inputs

		/*unsafe*/virtual bool GetFWId(WORD *pwFWId);//to get the device version.

		virtual bool DisableUSB3(bool bDisable);
		/*unsafe*/virtual bool GetUSB3Disabled(/*fixed*/[Out] bool *pbDisable);

		/*unsafe*/virtual bool GetTemperatureSensor(int iIndexBoard,int iIndexSensor,WORD *pwTemperature);//FW parameter
								//iIndexBoard: 0=ComBoard, 1=first RX board, 2=second RX board, 3=third RX board, 4=four RX board.
		virtual bool SetTemperatureAlarm(BYTE &byWarning,BYTE &byAlarm);
		/*unsafe*/virtual bool GetTemperatureAlarm(BYTE *pbyWarning,BYTE *pbyAlarm);

		virtual bool EnableFMC(bool bEnable);//return false if the FW does not support this feature (old FMC FW).
		/*unsafe*/virtual bool GetEnableFMC(bool *pbEnable);//return false if the FW does not support this feature (old FMC FW).
		//you can use the following functions in any case (step equal or different to 1):
		virtual bool SetFMCElement(int %iElementStart,int %iElementStop,int %iElementStep);
		/*unsafe*/virtual bool GetFMCElement(int *piElementStart,int *piElementStop,int *piElementStep);
		//you can use the following functions only if the step is 1:
		virtual bool SetFMCElementStart(int %iElementIndex);
		/*unsafe*/virtual bool GetFMCElementStart(int *piElementIndex);
		virtual bool SetFMCElementStop(int %iElementIndex);
		/*unsafe*/virtual bool GetFMCElementStop(int *piElementIndex);
	
		virtual bool EnableMultiHWChannel(bool bEnable);//return false if the FW does not support the MultiChannel feature.
		/*unsafe*/virtual bool GetEnableMultiHWChannel(bool *pbEnable);//return false if the FW does not support the MultiChannel feature.

		virtual bool ResetTimeStamp();

		virtual bool ResetEncoder(int iEncoderIndex);
		virtual bool SetEncoder(int iEncoderIndex,double %dValue);//unit=meter
		virtual bool SetEncoder(int iEncoderIndex,DWORD %dwValue);//unit=encoder step
		virtual bool SetEncoderType(csEnumEncoderType %eEncoder1Type,csEnumEncoderType %eEncoder2Type);
		/*unsafe*/virtual bool GetEncoderType(csEnumEncoderType *peEncoder1Type,csEnumEncoderType *peEncoder2Type);

		virtual bool EnableAscan(bool bAscan);//to enable Ascan acquisition that will be sent by the FW to the SW.
		/*unsafe*/virtual bool GetEnableAscan(bool *pbAscan);

		//Cscan time of flight : you have frist to define gates (see "SetGateXXX").
		virtual bool EnableCscanTof(bool bCscanTof);//to enable Cscan time of flight acquisition that will be sent by the FW to the SW.
		/*unsafe*/virtual bool GetEnableCscanTof(bool *pbCscanTof);

		virtual bool SetAscanBitSize(csEnumBitSize %eBitSize);//Ascan data size.
		/*unsafe*/virtual bool GetAscanBitSize(csEnumBitSize *peBitSize);

		virtual bool SetAscanRequest(csEnumAscanRequest %eAscanRequest);
		/*unsafe*/virtual bool GetAscanRequest(csEnumAscanRequest *peAscanRequest);

		virtual bool SetAscanRequestFrequency(double %dValue);
		/*unsafe*/virtual bool GetAscanRequestFrequency(double *pdValue);
		virtual bool CheckAscanRequestFrequency(double %dValue);

//<<PARAMETERS MANAGEMENT FUNCTIONS : BEGIN>>
	//low level API
		virtual bool SetCycleCount(int %lCycleCount);//to write the cycle count.
		/*unsafe*/virtual bool GetCycleCount(/*fixed*/[Out] int *piCycleCount);//to read the cycle count.
		virtual bool CheckCycleCount(int %iCycleCount);//to check the cycle count (minimum value, maximum value).

		virtual bool SetTriggerMode(csEnumOEMPATrigger %eTrig);
		/*unsafe*/virtual bool GetTriggerMode(/*fixed*/[Out] csEnumOEMPATrigger *peTrig);
		virtual bool SetEncoderDirection(csEnumOEMPAEncoderDirection &eEncoderDirection);
		/*unsafe*/virtual bool GetEncoderDirection(/*fixed*/[Out] csEnumOEMPAEncoderDirection *peEncoderDirection);
		//In case of SW trigger you have also to call the following functions:
		virtual bool SWTrigger_Sequence();
		virtual bool SWTrigger_Cycle(int iCycleCount);
		virtual bool SWTrigger_ResetFWCurrentCycle();

		//It is better to use following function (unit is meter), previously you have to set the resolution ("GetSWEncoder(int iEncoder)->lSetResolution()").
		virtual bool SetTriggerEncoderStep(double %dStep);//unit=meter
		/*unsafe*/virtual bool GetTriggerEncoderStep(/*fixed*/[Out] double *pdStep);//unit=meter

		virtual bool SetSignalTriggerHighTime(double %dTime);
		/*unsafe*/virtual bool GetSignalTriggerHighTime(/*fixed*/[Out] double *pdTime);

		virtual bool SetRequestIO(csEnumOEMPARequestIO %eRequest);
		/*unsafe*/virtual bool GetRequestIO(/*fixed*/[Out] csEnumOEMPARequestIO *peRequest);

		virtual bool SetRequestIODigitalInputMask(int %iMaskFalling,int %iMaskRising);
		/*unsafe*/virtual bool GetRequestIODigitalInputMask(/*fixed*/[Out] int *piMaskFalling,/*fixed*/[Out] int *piMaskRising);
		virtual bool CheckRequestDigitalInputMask(int %iMask);

		/*unsafe*/bool GetFilterCoefficient_(csEnumOEMPAFilter eFilter,/*fixed*/WORD *pwScale,/*fixed*/[Out] cli::array<short>^ %wValue/*g_iOEMPAFilterCoefficientMax*/);
#ifndef COMPIL_DRIVER_OEMPAX
		/*unsafe*/virtual bool GetFilterCoefficient(csEnumOEMPAFilter eFilter,/*fixed*/WORD *pwScale,/*fixed*/[Out] cli::array<short>^ %wValue/*g_iOEMPAFilterCoefficientMax*/);
#else //COMPIL_DRIVER_OEMPAX
		/*unsafe*/virtual bool GetFilterCoefficient(csEnumOEMPAFilterIndex eFilter,/*fixed*/WORD *pwScale,/*fixed*/[Out] cli::array<short>^ %wValue/*g_iOEMPAFilterCoefficientMax*/);
#endif //COMPIL_DRIVER_OEMPAX
		virtual bool FindFilterCoefficient(WORD %wScale,cli::array<short>^ %wValue/*g_iOEMPAFilterCoefficientMax*/,[Out] csEnumOEMPAFilter^ %eFilter);

		virtual bool SetFilter(csEnumOEMPAFilterIndex eFilterIndex,WORD %wScale,cli::array<short>^ %wValue/*g_iOEMPAFilterCoefficientMax*/);
		/*unsafe*/virtual bool GetFilter(csEnumOEMPAFilterIndex eFilterIndex,/*fixed*/WORD *pwScale,/*fixed*/[Out] cli::array<short>^ %wValue/*g_iOEMPAFilterCoefficientMax*/);

		virtual bool SetEncoderWire1(int iEncoderIndex,csEnumDigitalInput %eDigitalInput);//int iEncoderIndex : 0 for first encoder, 1 for second encoder.
		virtual bool SetEncoderWire2(int iEncoderIndex,csEnumDigitalInput %eDigitalInput);
		virtual bool SetExternalTriggerCycle(csEnumDigitalInput %eDigitalInput);
		virtual bool SetExternalTriggerSequence(csEnumDigitalInput %eDigitalInput);
		/*unsafe*/virtual bool GetEncoderWire1(int iEncoderIndex,/*fixed*/[Out] csEnumDigitalInput *peDigitalInput);
		/*unsafe*/virtual bool GetEncoderWire2(int iEncoderIndex,/*fixed*/[Out] csEnumDigitalInput *peDigitalInput);
		/*unsafe*/virtual bool GetExternalTriggerCycle(/*fixed*/[Out] csEnumDigitalInput *peDigitalInput);
		/*unsafe*/virtual bool GetExternalTriggerSequence(/*fixed*/[Out] csEnumDigitalInput *peDigitalInput);

		virtual bool SetDigitalOutput(int iIndex,csEnumOEMPAMappingDigitalOutput eMappingDigitalOutput);
		/*unsafe*/virtual bool GetDigitalOutput(int iIndex,/*fixed*/[Out] csEnumOEMPAMappingDigitalOutput *peMappingDigitalOutput);

		virtual bool SetEncoderDebouncer(double %dValue);
		/*unsafe*/virtual bool GetEncoderDebouncer(/*fixed*/[Out] double *pdValue);
		virtual bool CheckEncoderDebouncer(double %dValue);

		virtual bool SetDigitalDebouncer(double %dValue);
		/*unsafe*/virtual bool GetDigitalDebouncer(/*fixed*/[Out] double *pdValue);
		virtual bool CheckDigitalDebouncer(double %dValue);

		virtual bool SetFlushTimer(double% dTimer);
		virtual bool CheckStreamTimer(double% dTimer);

		virtual bool SetGainDigital(int iCycle,double %dGain);//to write the amplification.
		/*unsafe*/virtual bool GetGainDigital(int iCycle,/*fixed*/[Out] double *pdGain);//to read the amplification.
		virtual bool CheckGainDigital(double %dGain);//to check the amplification value (minimum, maximum, step).
		// Functions to handle MC channels.
		virtual bool SetGainDigital(int iCycle, int iChannel, double% dGain);//to write the amplification.
		/*unsafe*/virtual bool GetGainDigital(int iCycle, int iChannel,/*fixed*/[Out] double* pdGain);//to read the amplification.

		virtual bool SetBeamCorrection(int iCycle,float %fGain);
		/*unsafe*/virtual bool GetBeamCorrection(int iCycle,float *pfGain);
		virtual bool CheckBeamCorrection(float %fGain);
		// Functions to handle MC channels.
		virtual bool SetBeamCorrection(int iCycle, int iChannel, float% fGain);
		virtual bool GetBeamCorrection(int iCycle, int iChannel, float* pfGain);

		virtual bool SetDACSlope(int iCycle,acsDac^ %dac/*int &iCountMax,structCallbackArrayFloatDac &callbackArrayFloatDac*/);
		virtual bool GetDACSlope(int iCycle,[Out] acsDac^ %dac/*int &iCountMax,structCallbackArrayFloatDac &callbackArrayFloatDac*/);
		virtual bool SetDACGain(bool bAutoStop,int iCycle,acsDac^ %dac/*int &iCountMax,structCallbackArrayFloatDac &callbackArrayFloatDac*/);
		virtual bool GetDACGain(int iCycle,[Out] acsDac^ %dac/*int &iCountMax,structCallbackArrayFloatDac &callbackArrayFloatDac*/);
		virtual bool EnableDAC(int iCycle,bool %bEnable);
		/*unsafe*/virtual bool GetEnableDAC(int iCycle,bool *pbEnable);

		// Functions to handle MC channels.
		virtual bool SetDACSlope(int iCycle, int iChannel, acsDac^% dac/*int &iCountMax,structCallbackArrayFloatDac &callbackArrayFloatDac*/);
		virtual bool GetDACSlope(int iCycle, int iChannel, [Out] acsDac^% dac/*int &iCountMax,structCallbackArrayFloatDac &callbackArrayFloatDac*/);
		virtual bool SetDACGain(bool bAutoStop, int iCycle, int iChannel, acsDac^% dac/*int &iCountMax,structCallbackArrayFloatDac &callbackArrayFloatDac*/);
		virtual bool GetDACGain(int iCycle, int iChannel, [Out] acsDac^% dac/*int &iCountMax,structCallbackArrayFloatDac &callbackArrayFloatDac*/);
		virtual bool EnableDAC(int iCycle, int iChannel, bool% bEnable);
		/*unsafe*/virtual bool GetEnableDAC(int iCycle, int iChannel, bool* pbEnable);

		virtual bool CheckDACSlope(double %dTime,float %fSlope);
		virtual bool CheckDACCount(int %iCount);

		virtual bool SetAscanRectification(int iCycle,csEnumRectification %eRectification);
		/*unsafe*/virtual bool GetAscanRectification(int iCycle,csEnumRectification *peRectification);
		// Functions to handle MC channels.
		virtual bool SetAscanRectification(int iCycle, int iChannel, csEnumRectification% eRectification);
		virtual bool GetAscanRectification(int iCycle, int iChannel, csEnumRectification* peRectification);

		virtual bool SetAscanStart(int iCycle,double %dStart);
		/*unsafe*/virtual bool GetAscanStart(int iCycle,double *pdStart);
		virtual bool CheckAscanStart(double %dStart);
		// Functions to handle MC channels.
		virtual bool SetAscanStart(int iCycle, int iChannel, double% dStart);
		virtual bool GetAscanStart(int iCycle, int iChannel, double* pdStart);
	
		virtual bool SetAscanRange(int iCycle,double %dRange,/*output only*/[Out] csEnumCompressionType %eCompressionType,/*output only*/[Out] long %lPointCount,/*output only*/[Out] long %lPointFactor);//list count is automatically calculated from the digitizing clock, compression could be used.
		virtual bool SetAscanRangeWithFactor(int iCycle,double %dRange,csEnumCompressionType %eCompressionType,/*in/out*/long %lPointFactor,/*output only*/[Out] long %lPointCount);
		virtual bool SetAscanRangeWithCount(int iCycle,double %dRange,/*in/out (check)*/csEnumCompressionType %eCompressionType,/*in/out (check)*/long %lPointCount,/*output only*/[Out] long %lPointFactor);
		/*unsafe*/virtual bool GetAscanRange(int iCycle,double *pdRange,csEnumCompressionType *peCompressionType,long *plPointCount,long *plPointFactor);
		virtual bool CheckAscanRange(double %dRange);
		virtual bool CheckAscanRangeWithCount(double %dRange,csEnumCompressionType %eCompressionType,long %lPointCount);
		virtual bool GetSamplingFrequency(csEnumCompressionType %eCompressionType,long %lPointFactor,[Out] double %dSamplingFrequency/*Hertz*/);
		// Functions to handle MC channels.
		virtual bool SetAscanRange(int iCycle, int iChannel, double% dRange,/*output only*/csEnumCompressionType% eCompressionType,/*output only*/LONG% lPointCount,/*output only*/LONG% lPointFactor);//point count is automatically calculated from the digitizing clock.
		virtual bool SetAscanRangeWithFactor(int iCycle, int iChannel, double% dRange, csEnumCompressionType% eCompressionType,/*in/out*/LONG% lPointFactor,/*output only*/LONG% lPointCount);
		virtual bool SetAscanRangeWithCount(int iCycle, int iChannel, double% dRange,/*in/out (check)*/csEnumCompressionType% eCompressionType,/*in/out (check)*/LONG% lPointCount,/*output only*/LONG% lPointFactor);
		virtual bool GetAscanRange(int iCycle, int iChannel, double* pdRange, csEnumCompressionType* peCompressionType, LONG* plPointCount, LONG* plPointFactor);
		
		virtual bool SetFilterIndex(int iCycle,csEnumOEMPAFilterIndex %eFilterIndex);
		/*unsafe*/virtual bool GetFilterIndex(int iCycle,csEnumOEMPAFilterIndex *peFilterIndex);
		// Functions to handle MC channels.
		virtual bool SetFilterIndex(int iCycle, int iChannel, csEnumOEMPAFilterIndex% eFilterIndex);
		virtual bool GetFilterIndex(int iCycle, int iChannel, csEnumOEMPAFilterIndex* peFilterIndex);
		
		virtual bool SetTimeSlot(int iCycle,double %dTime);
		/*unsafe*/virtual bool GetTimeSlot(int iCycle,double *pdTime);
		virtual bool CheckTimeSlot(double %dTime);
	virtual double vf_GetMinTimeSlotRecovery(long lAscanPointCount, enumBitSize eAscanBitSize);//Minimum timeSlot for maximum useful data throughput (not LAN throughput, data header is not taken into account).
	virtual double GetAscanThroughput(double dTimeSlot, long lAscanPointCount, enumBitSize eAscanBitSize);//useful data throughput not LAN throughput (data header is not taken into account).
	//FMC SubTimeSlot management
	virtual bool SetFMCSubTimeSlotAcqReplay(int iCycle,double dAscanStart,double dAscanRange,double %dTimeSlot);//easy function to use
	virtual bool GetFMCTimeLimitation(double dAscanStart,double dAscanRange,double dTimeSlot,double %dTimeSlotMin,double %dHWAcqSubTimeSlotMin,double %dReplaySubTimeSlotMin,double %dReplaySubTimeSlotOptimizedForThroughput);
	virtual int GetFMCSubTimeSlotCount();
	//instead to call "SetFMCSubTimeSlotAcq" and "SetFMCSubTimeSlotReplay" you can call "SetFMCSubTimeSlot" which is easier to use.
	virtual bool SetFMCSubTimeSlotAcq(int iCycle,double %dSubTimeSlot);
	virtual bool SetFMCSubTimeSlotReplay(int iCycle,double %dSubTimeSlot);
	/*unsafe*/virtual bool GetFMCSubTimeSlotAcq(int iCycle,double *pdSubTimeSlot);
	/*unsafe*/virtual bool GetFMCSubTimeSlotReplay(int iCycle,double *pdSubTimeSlot);
		
		virtual bool SetAscanAcqIdChannelProbe(int iCycle,WORD %wID);
		/*unsafe*/virtual bool GetAscanAcqIdChannelProbe(int iCycle,WORD *pwID);
		// Functions to handle MC channels.
		virtual bool SetAscanAcqIdChannelProbe(int iCycle, int iChannel, WORD% wID);
		virtual bool GetAscanAcqIdChannelProbe(int iCycle, int iChannel, WORD* pwID);
		
		virtual bool SetAscanAcqIdChannelScan(int iCycle,WORD %wID);
		/*unsafe*/virtual bool GetAscanAcqIdChannelScan(int iCycle,WORD *pwID);
		// Functions to handle MC channels.
		virtual bool SetAscanAcqIdChannelScan(int iCycle, int iChannel, WORD% wID);
		virtual bool GetAscanAcqIdChannelScan(int iCycle, int iChannel, WORD* pwID);
		
		virtual bool SetAscanAcqIdChannelCycle(int iCycle,WORD %wID);
		/*unsafe*/virtual bool GetAscanAcqIdChannelCycle(int iCycle,WORD *pwID);
		// Functions to handle MC channels.
		virtual bool SetAscanAcqIdChannelCycle(int iCycle, int iChannel, WORD% wID);
		virtual bool GetAscanAcqIdChannelCycle(int iCycle, int iChannel, WORD* pwID);
		
		virtual bool EnableAscanMaximum(int iCycle,bool %bEnable);
		/*unsafe*/virtual bool GetEnableAscanMaximum(int iCycle,bool *pbEnable);
		
		virtual bool EnableAscanMinimum(int iCycle,bool %bEnable);
		/*unsafe*/virtual bool GetEnableAscanMinimum(int iCycle,bool *pbEnable);
		
		virtual bool EnableAscanSaturation(int iCycle,bool %bEnable);
		/*unsafe*/virtual bool GetEnableAscanSaturation(int iCycle,bool *pbEnable);
		
		virtual bool SetGateModeThreshold(int iCycle,int iGate,bool %bEnable,csEnumGateModeAmp %eGateModeAmp,csEnumGateModeTof %eGateModeTof,csEnumRectification %eGateRectification,double %dThresholdPercent);
		/*unsafe*/virtual bool GetGateModeThreshold(int iCycle,int iGate,bool *pbEnable,csEnumGateModeAmp *peGateModeAmp,csEnumGateModeTof *peGateModeTof,csEnumRectification *peGateRectification,double *pdThresholdPercent);
		virtual bool CheckGateModeThreshold(bool %bEnable,csEnumGateModeAmp %eGateModeAmp,csEnumGateModeTof %eGateModeTof,csEnumRectification %eGateRectification,double %dThresholdPercent);
		// Functions to handle MC channels.
		virtual bool SetGateModeThreshold(int iCycle, int iChannel, int iGate, bool% bEnable, csEnumGateModeAmp% eGateModeAmp, csEnumGateModeTof% eGateModeTof, csEnumRectification% eGateRectification, double% dThresholdPercent);
		virtual bool GetGateModeThreshold(int iCycle, int iChannel, int iGate, bool* pbEnable, csEnumGateModeAmp* peGateModeAmp, csEnumGateModeTof* peGateModeTof, csEnumRectification* peGateRectification, double* pdThresholdPercent);
		
		virtual bool SetGateStart(int iCycle,int iGate,double %dStart);
		/*unsafe*/virtual bool GetGateStart(int iCycle,int iGate,double *pdStart);
		virtual bool CheckGateStart(double %dStart);
		// Functions to handle MC channels.
		virtual bool SetGateStart(int iCycle, int iChannel, int iGate, double% dStart);
		virtual bool GetGateStart(int iCycle, int iChannel, int iGate, double* pdStart);
		
		virtual bool CheckGateStartStop(double %dStart,double %dStop);
		virtual bool SetGateStop(int iCycle,int iGate,double %dStop);
		/*unsafe*/virtual bool GetGateStop(int iCycle, int iGate, double *pdStop);
		/*unsafe*/virtual bool GetGateStop(int iCycle,int iGate,double *pdStop,bool *pbWidth);
		virtual bool CheckGateStop(double %dStop);
		// Functions to handle MC channels.
		virtual bool SetGateStop(int iCycle, int iChannel, int iGate, double% dStop);
		virtual bool GetGateStop(int iCycle, int iChannel, int iGate, double* pdStop);
		
		virtual bool SetGateAcqIDAmp(int iDriverId,int iCycle,WORD %wID);
		/*unsafe*/virtual bool GetGateAcqIDAmp(int iDriverId,int iCycle,WORD *pwID);
		virtual bool SetGateAcqIDTof(int iDriverId,int iCycle,WORD %wID);
		/*unsafe*/virtual bool GetGateAcqIDTof(int iDriverId,int iCycle,WORD *pwID);
		// Functions to handle MC channels.
		virtual bool SetGateAcqIDAmp(int iCycle, int iChannel, int iGate, WORD% wID);
		virtual bool GetGateAcqIDAmp(int iCycle, int iChannel, int iGate, WORD* pwID);
		virtual bool SetGateAcqIDTof(int iCycle, int iChannel, int iGate, WORD% wID);
		virtual bool GetGateAcqIDTof(int iCycle, int iChannel, int iGate, WORD* pwID);
		
		virtual bool SetTrackingGateStart(int iCycle,int iGate,bool bEnable,int %iTrackingCycleIndex,int %iTrackingGateIndex);
		/*unsafe*/virtual bool GetTrackingGateStart(int iCycle,int iGate,bool *pbEnable,int *piTrackingCycleIndex,int *piTrackingGateIndex);
		virtual bool SetTrackingGateStop(int iCycle,int iGate,bool bEnable,int %iTrackingCycleIndex,int %iTrackingGateIndex);
		/*unsafe*/virtual bool GetTrackingGateStop(int iCycle,int iGate,bool *pbEnable,int *piTrackingCycleIndex,int *piTrackingGateIndex);
		virtual bool SetTrackingAscan(int iCycle,bool bEnable,int %iTrackingCycleIndex,int %iTrackingGateIndex);
		/*unsafe*/virtual bool GetTrackingAscan(int iCycle,bool *pbEnable,int *piTrackingCycleIndex,int *piTrackingGateIndex);
		virtual bool SetTrackingDac(int iCycle,bool bEnable,int %iTrackingCycleIndex,int %iTrackingGateIndex);
		/*unsafe*/virtual bool GetTrackingDac(int iCycle,bool *pbEnable,int *piTrackingCycleIndex,int *piTrackingGateIndex);
		virtual bool CheckTracking(bool bEnable,int %iTrackingCycleIndex,int %iTrackingGateIndex);
		virtual bool ResetTrackingTable();//this is called automatically when the IF tracking of one gate is updated.
		// Functions to handle MC channels.
		virtual bool SetTrackingGateStart(int iCycle, int iChannel, int iGate, bool bEnable, int% iTrackingCycleIndex, int% iTrackingChannelIndex, int% iTrackingGateIndex);
		/*unsafe*/virtual bool GetTrackingGateStart(int iCycle, int iChannel, int iGate, bool* pbEnable, int* piTrackingCycleIndex, int* piTrackingChannelIndex, int* piTrackingGateIndex);
		virtual bool SetTrackingGateStop(int iCycle, int iChannel, int iGate, bool bEnable, int% iTrackingCycleIndex, int% iTrackingChannelIndex, int% iTrackingGateIndex);
		/*unsafe*/virtual bool GetTrackingGateStop(int iCycle, int iChannel, int iGate, bool* pbEnable, int* piTrackingCycleIndex, int* piTrackingChannelIndex, int* piTrackingGateIndex);
		virtual bool SetTrackingAscan(int iCycle, int iChannel, bool bEnable, int% iTrackingCycleIndex, int% iTrackingChannelIndex, int% iTrackingGateIndex);
		/*unsafe*/virtual bool GetTrackingAscan(int iCycle, int iChannel, bool* pbEnable, int* piTrackingCycleIndex, int* piTrackingChannelIndex, int* piTrackingGateIndex);
		virtual bool SetTrackingDac(int iCycle, int iChannel, bool bEnable, int% iTrackingCycleIndex, int% iTrackingChannelIndex, int% iTrackingGateIndex);
		/*unsafe*/virtual bool GetTrackingDac(int iCycle, int iChannel, bool* pbEnable, int* piTrackingCycleIndex, int* piTrackingChannelIndex, int* piTrackingGateIndex);

		// Bipolar high voltage control, positive = [25; 100], negative = [-100; -25], abs(negative) >= positive. Unit is Volt.
		virtual bool SetHighVoltageBipolar(int% positive, int% negative);
		virtual bool GetHighVoltageBipolar(int* positive, int* negative);
		// Capacitive (negative) high voltage control, voltage = [25; 400]. Unit is Volt. Negative values in the same range are also valid.
		virtual bool SetHighVoltage(int% voltage);
		virtual bool GetHighVoltage(int* voltage);

		virtual bool SetGainAnalog(int iCycle,float %fGain);
		/*unsafe*/virtual bool GetGainAnalog(int iCycle,float *pfGain);
		virtual bool CheckGainAnalog(float %fGain);
		
		//For some of the following functions the CycleCount is required
		//(it should be the total cycle count of the sequence).
		virtual bool SetEmissionWedgeDelay(int iCycle,int iCycleCount,double %dWedgeDelay);//"iCycleCount"=cycle count.
		/*unsafe*/virtual bool GetEmissionWedgeDelay(int iCycle,int iCountMax,double *pdWedgeDelay);
		virtual bool SetReceptionWedgeDelay(int iCycle,int iCycleCount,double %dWedgeDelay);
		/*unsafe*/virtual bool GetReceptionWedgeDelay(int iCycle,int iCountMax,double *pdWedgeDelay);
		virtual bool CheckWedgeDelay(double %dWedgeDelay);
		// Functions to handle MC channels.
		virtual bool SetEmissionWedgeDelaySingleChannel(int iCycle, int iChannel, double% dWedgeDelay);
		virtual bool GetEmissionWedgeDelaySingleChannel(int iCycle, int iChannel, double* pdWedgeDelay);
		virtual bool SetReceptionWedgeDelaySingleChannel(int iCycle, int iChannel, double% dWedgeDelay);
		virtual bool GetReceptionWedgeDelaySingleChannel(int iCycle, int iChannel, double* pdWedgeDelay);
		
		static bool SetAllElementEnable(bool bEnable,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/);
		static bool SetElementEnable(int iElement,bool bEnable,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/);
		static bool GetElementEnable(int iElement,/*const*/ cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/,bool %bEnable);
		
//OLD STYLE FUNCTIONS (limited to 128 elements).
		//APERTURE DEFINITION : 2 cases.
		virtual bool IsMultiplexer();//this function can be used to know if a multiplexer (16:128 or 32:128) is included in the system.
		//Case of a system with a multiplexer (16:128 or 32:128), please use following functions:
		//	multiplexer case: if emission and reception have same aperture:
		virtual bool SetMultiplexerEnable(int iCycle,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/);
		virtual bool GetMultiplexerEnable(int iCycle,/*fixed*/[Out] cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/);
		//	multiplexer case: if emission and reception aperture are different:
		//	(you cannot have more than 32 elements in case of a 32/128 and 16 elements in case of a 16/128).
		virtual bool SetMultiplexerEnable(int iCycle,cli::array<DWORD>^ %adwHWApertureEmission/*[g_iOEMPAApertureDWordCount]*/,cli::array<DWORD>^ %adwHWApertureReception/*[g_iOEMPAApertureDWordCount]*/);
		virtual bool GetMultiplexerEnable(int iCycle,/*fixed*/[Out] cli::array<DWORD>^ %adwHWApertureEmission/*[g_iOEMPAApertureDWordCount]*/,/*fixed*/[Out] cli::array<DWORD>^ %adwHWApertureReception/*[g_iOEMPAApertureDWordCount]*/);
		virtual bool CheckMultiplexerAperture(cli::array<DWORD>^ %adwHWApertureEmission/*[g_iOEMPAApertureDWordCount]*/,cli::array<DWORD>^ % adwHWApertureReception/*[g_iOEMPAApertureDWordCount]*/);//to check that emission and reception apertures are coherent.
		//Case of a system (16:16, 32:32, 64:64 etc...) without a multiplexer, then please use the following functions:
		virtual bool SetEmissionEnable(int iCycle,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/);
		virtual bool GetEmissionEnable(int iCycle,/*fixed*/[Out] cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/);
		virtual bool SetReceptionEnable(int iCycle,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/);
		virtual bool GetReceptionEnable(int iCycle,/*fixed*/[Out] cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/);

		// Functions to handle MC channels.
		virtual bool SetEmissionEnableBipolar(int iCycle, cli::array<DWORD>^% adwHWAperture);
		virtual bool GetEmissionEnableBipolar(int iCycle, [Out] cli::array<DWORD>^% adwHWAperture);
		/*!
		 * \brief Set bipolar settings.
		 * Function specific to the OEM-MC2 hardware.
		 * Won't enable or disable the channel for pulse, only for settings (pulse width, count and period).
		 * Use SetEmissionEnableBipolar() to enable channels.
		 * \param[in] iCycle: cycle number for which to set bipolar settings.
		 * \param[in] iChannel: channel number for which to set bipolar settings.
		 * \param[in] iPulseCount: number of pulses.
		 * \param[in] fPulseWidth: width of a pulse in second.
		 * \param[in] fPulsePeriod: period duration in second.
		 */
		virtual bool SetEmissionBipolarPulse(int iCycle, int iChannel, int% iPulseCount, float% fPulseWidth, float% fPulsePeriod);
		/*!
		 * \brief Get bipolar settings.
		 * Function specific to the OEM-MC2 hardware.
		 * \param[in] iCycle: cycle number for which to get bipolar settings.
		 * \param[in] iChannel: channel number for which to get bipolar settings.
		 * \param[out] iPulseCount: number of pulses.
		 * \param[out] fPulseWidth: width of a pulse in second.
		 * \param[out] fPulsePeriod: period duration in second.
		 */
		virtual bool GetEmissionBipolarPulse(int iCycle, int iChannel, [Out] int* iPulseCount, [Out] float* fPulseWidth, [Out] float* fPulsePeriod);

		virtual bool SetEmissionDelays(int iCycle,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/,acsFloat^ %afDelay/*,structCallbackArrayFloat1D &callbackArrayFloat1D*/);//Size = element size.
		virtual bool SetEmissionWidths(int iCycle,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/,acsFloat^ %afWidth/*,structCallbackArrayFloat1D &callbackArrayFloat1D*/);
		virtual bool SetReceptionGains(int iCycle,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/,acsFloat^ %afGain/*,structCallbackArrayFloat1D &callbackArrayFloat1D*/);
		virtual bool SetReceptionDelays(int iCycle,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/,acsDelayReception^ %afDelay/*,structCallbackArrayFloat2D &callbackArrayFloat2D*/);//Size1 = element size, Size2 = DDF size.

//NEW STYLE FUNCTIONS (no limitation for the aperture).
		virtual bool SetEmissionDelays(int iCycle,cli::array<int>^ %aiElementList,acsFloat^ %afDelay);//Size = element size.
		virtual bool GetEmissionDelays(int iCycle,/*fixed*/[Out] acsFloat^ %afDelay/*,int &iElementCountMax,structCallbackArrayFloat1D &callbackArrayFloat1D*/);//Size = element size.
		// Functions for MC series hardware (single channel).
		virtual bool SetEmissionDelay(int iCycle, int iChannel, float% fDelay);
		virtual bool GetEmissionDelay(int iCycle, int iChannel, float* pfDelay);

		virtual bool SetEmissionWidths(int iCycle,cli::array<int>^ %aiElementList,acsFloat^ %afWidth/*,structCallbackArrayFloat1D &callbackArrayFloat1D*/);
		virtual bool GetEmissionWidths(int iCycle,/*fixed*/[Out] acsFloat^ %afWidth/*,int &iElementCountMax,structCallbackArrayFloat1D &callbackArrayFloat1D*/);
		// Functions for MC series hardware (single channel), width for negative pulse.
		virtual bool SetEmissionWidth(int iCycle, int iChannel, float% fWidth);
		virtual bool GetEmissionWidth(int iCycle, int iChannel, float* pfWidth);

		virtual bool SetReceptionGains(int iCycle,cli::array<int>^ %aiElementList,acsFloat^ %afGain/*,structCallbackArrayFloat1D &callbackArrayFloat1D*/);
		virtual bool GetReceptionGains(int iCycle,/*fixed*/[Out] acsFloat^ %afGain/*,int &iElementCountMax,structCallbackArrayFloat1D &callbackArrayFloat1D*/);
		virtual bool SetReceptionDelays(int iCycle,cli::array<int>^ %aiElementList,acsDelayReception^ %afDelay/*,structCallbackArrayFloat2D &callbackArrayFloat2D*/);//Size1 = element size, Size2 = DDF size.
		virtual bool GetReceptionDelays(int iCycle,/*fixed*/[Out] acsDelayReception^ %afDelay/*,int &iElementCountMax,int &iFocalCountMax,structCallbackArrayFloat2D &callbackArrayFloat2D*/);//Size1 = element size, Size2 = DDF size.
		virtual bool SetReceptionFocusing(int iCycle,acsDouble^ %dFocalTof);
		/*unsafe*/virtual bool GetReceptionFocusing(int iCycle,/*fixed*/[Out] acsDouble^ %dFocalTof);
		virtual bool SetReceptionFocusing(int iCycle,acsDouble^ %dFocalTof,float %fCenterDelayE,float %fCenterDelayR);
		/*unsafe*/virtual bool GetReceptionFocusing(int iCycle,/*fixed*/[Out] acsDouble^ %dFocalTof,[Out] float *pfCenterDelayE,[Out] float *pfCenterDelayR);
		virtual bool EnableDDF(int iCycle,bool %bEnable);
		/*unsafe*/virtual bool GetEnableDDF(int iCycle,bool *pbEnable);
		virtual bool EnableDDF(int iCycle,csEnumFocusing %eFocusing);
		/*unsafe*/virtual bool GetEnableDDF(int iCycle,csEnumFocusing *peFocusing);
		static void SetDDFTimeOfFlightMiddle(bool bEnable);
		static bool IsDDFTimeOfFlightMiddle();
		static void SetDDFWaveTrackingCorrection(int iEnable);
		static int GetDDFWaveTrackingCorrection();
		static void SetFMCReceptionSimplified(bool bEnable);
		static bool IsFMCReceptionSimplified();
		virtual bool CheckFocalTimeOfFlight(double %dDelay);
		virtual bool CheckEmissionWidth(float %fWidth);
		virtual bool CheckReceptionGain(float %fGain);
		virtual bool CheckEmissionDelay(float %fDelay);
		virtual bool CheckReceptionDelay(float %fDelay);
		virtual DWORD GetSWBaseAddress();

		bool GetApertureOEM(cli::array<int>^ %aiElementList,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/);
		bool GetApertureOEM(cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/,cli::array<int>^ %aiElementList);

		virtual bool EnableMultiHWChannelAcquisition(int iCycle,int iCycleCount,bool bEnable);
		/*unsafe*/virtual bool GetEnableMultiHWChannelAcquisition(int iCycle,bool *pbEnable);
		virtual bool SetMultiHWChannelAcqDecimation(int iCycle,acsByte^ %abyData);
		/*unsafe*/virtual bool GetMultiHWChannelAcqDecimation(int iCycle,[Out] acsByte^ %abyData);
		//A-scan Start is specified for consecutive elements from the first single element #1 (no gap).
		virtual bool SetMultiHWChannelAcqWriteStart(int iCycle,int iAcqElement,int iStartCount,acsFloat^ %afStart/*float *pfStart*/);
		virtual bool GetMultiHWChannelAcqWriteStart(int iCycle,/*fixed*/[Out] acsFloat^ %afStart/*int &iStartCountMax,int *piStartCount,float *pfStart*/);
		virtual double GetMultiHWChannelRangeMax();
		virtual double GetFWAscanRecoveryTime();

		virtual bool SetSettingId(DWORD dwSettingId);
		/*unsafe*/virtual bool GetSettingId(DWORD *pdwSettingId);//ask hw as for others functions.
		DWORD swGetSettingId();//ask sw (boolean memory image resident in computer) instead of hw.
		
		virtual bool SetTimeOffset(float %fTime);
		virtual bool GetTimeOffset(float *pfTime);
////in case of Ethernet, KeepAlive is useful to prevent communication deadlock
////but if the developper stay a long time inside a breakpoint it will break the socket.
//bool SetKeepAlive(enumKeepAlive eKeepAlive);
//bool GetKeepAlive(enumKeepAlive *peKeepAlive);//ask hw as for others functions.

//<<PARAMETERS MANAGEMENT FUNCTIONS : END>>

		void test();
		static int Test();
	protected:
		!csHWDeviceOEMPA();
	};
#ifdef _WIN64
	public ref class csAcquisitionFifo
    {
	private:
		csHWDeviceOEMPA ^m_csHWDeviceOEM;
		csEnumAcquisitionFifo m_csFifo;
		CAcquisitionFifo *m_pAcquisitionFifo;
		bool m_bNewFifo;
	public:
		csAcquisitionFifo(csEnumAcquisitionFifo csFifo,csHWDeviceOEMPA ^csHWDeviceOEMPA);
		~csAcquisitionFifo();
		void Free();

		csEnumAcquisitionFifo GetFifo();//return the type of the fifo.
		bool IsEnabled();//Fifo has been allocated.
		bool Alloc(int iCycleCountMax, int64_t iBufferByteSize);//to enable the fifo, should be called just after the constructor of "CHWDeviceOEMPA1".
			//"iCycleCountMax": maximum cycle count in the fifo (in case of AscanFifo maximum ascan count).
			//"iBufferByteSize": maximum buffer size to store all datas (ascan or cscan or IO).
			//Example:	iCycleCountMax=8    iBufferByteSize=1024+8*4=1056 (ascan header is 8 DWORDs)
			//			you can store 8 ascan of 128 BYTES each, OR 4 ascan of 256 BYTES each.
		bool GetAlloc([Out] int %iDataCountMax, [Out] int64_t %iBufferByteSize);//get allocation sizes.
		bool Desalloc();

		//count of data in the fifo.
		int GetCount();//count of data in the fifo.
		int GetLost();//count of lost data (new input data but the fifo was full so old data has been lost to save new data).
		int64_t GetTotalCount();//total data count that have been inputed of the fifo.
		int64_t GetTotalByteCount();//total byte count that have been inputed of the fifo.
		void ResetCounters();
		bool RemoveAll();//remove all data, can fail in the case the fifo is accessed at the same time than the priority input thread.
		bool RemoveTail();//remove the oldest data in the fifo, can fail in the case the data is accessed at the same time than the priority input thread.
		bool RemoveItem(int iItem);//remove the specified item and previous ones, -1 means the oldest data in the fifo (same than "RemoveTail"). Can fail in the case the data is accessed at the same time than the priority input thread.
		bool DumpFifoStatus(String ^pFileName);//debug purpose.

		//Output data from the fifo. The input parameter is the index of the data in the Fifo. This input parameter "iItem" is retrieved by others functions.
		//"bPeek": false to remove the data from the fifo or true to keep the data inside the fifo.
		//			if "bPeek=true" then the function "RemoveTail" or "RemoveItem" should be called.
		//"iItem": index of the item in the fifo, -1 means the last data.
		/*unsafe*/bool OutAscan(int iItem,bool bPeek,[Out] csAcqInfoEx^ %acqInfo,[Out] csHeaderStream_0x0001^ %streamHeader,[Out] csSubStreamAscan_0x0103^ %ascanHeader,[Out] const void* %pBufferMax,[Out] const void* %pBufferMin,[Out] const void* %pBufferSat);
		bool OutCscan(int iItem,bool bPeek,[Out] csAcqInfoEx^ %acqInfo,[Out] csHeaderStream_0x0001^ %streamHeader,[Out] csSubStreamCscan_0x0X02^ %cscanHeader,[Out] cli::array<csCscanAmp_0x0102^>^ %bufferAmp,[Out] cli::array<csCscanAmpTof_0x0202^>^ %bufferAmpTof);
		bool _OutIO(int iItem,bool bPeek,[Out] csHeaderStream_0x0001^ %streamHeader,[Out] csHeaderIO_0x0001^ %ioHeader);
		bool OutIO(int iItem,bool bPeek,[Out] csAcqInfoEx^ %acqInfo,[Out] csHeaderStream_0x0001^ %streamHeader,[Out] csHeaderIO_0x0001^ %ioHeader);

		//Functions to retrieve index of the data in the fifo for the specified cycle or the specified sequence/cycle.
		//For the following functions: "iFMCElement" and "iStartItem" should be -1 by default.
		//"iFMCElement" is used only for FMC mode, this is the pulser element, -1 means the parameter is not used.
		//"iStartItem" is the last item from which the search is done, -1 means the parameter is not used.
		//"iCycle" if -1 then the first cycle of the sequence is returned.
		//FIFO means FirstInFirstOut.
		//LIFO means LastInFirstOut.
		int GetFifoItem(LONGLONG sequence, int iCycle, int iFMCElement, int iStartItem);//return -1 if not found, otherwise the index.
		int GetFifoItem(int iCycle, int iFMCElement, int iStartItem);//(for the first sequence in the fifo) return -1 if not found, otherwise the index.
		int GetLifoItem(LONGLONG sequence, int iCycle, int iFMCElement, int iStartItem);//return -1 if not found, otherwise the index.
		int GetLifoItem(int iCycle, int iFMCElement, int iStartItem);//(for the last sequence in the fifo) return -1 if not found, otherwise the index.
		//Functions to retrieve index of any data in the fifo.
		int GetItemLimit([Out] int %iIndexTail, [Out] int %iIndexHead);//the return value is the same than "GetFifoCount", output parameter "iIndexTail" is the index of the oldest data in the fifo, output parameter "iIndexHead" is the index of the next input data that will be saved in the fifo.
		void IncrementItemIndex(int %iIndex);//function to increment the index from the Tail (oldest input in the fifo) to the Head (last data in the fifo).
		void DecrementItemIndex(int %iIndex);//function to decrement the index from the Head to the Tail.

		BYTE *GetSubStreamItem(int iItem,[Out] int %iSubStreamDataSize,[Out] csAcqInfoEx^ %acqInfo,[Out] csHeaderStream_0x0001^ %streamHeader);
		BYTE *GetSubStreamItem(int iItem,[Out] int %iSubStreamDataSize,[Out] csAcqInfoEx^ %acqInfo,[Out] csHeaderStream_0x0001^ %streamHeader,BYTE %byVersion);

		////The integrated thread is launched automatically by the driver, so the user dont have to call the following functions:
		bool IsRunning();//Is the integrated thread running.

		void AddFifoLost(int iLostCount);
		DWORD GetExit();
		void Exit();
	protected:
		!csAcquisitionFifo();
	};
#else //_WIN64
	public ref class csAcquisitionFifo
    {
	public:
		csAcquisitionFifo(csEnumAcquisitionFifo csFifo,csHWDeviceOEMPA ^csHWDeviceOEMPA);
		csAcquisitionFifo(csEnumAcquisitionFifo csFifo);

		bool Alloc(int iDataCountMax, int64_t iBufferByteSize){return false;};
	};
#endif //_WIN64
#pragma endregion hwDriverOEMPA
//////////////////////////////////////////////////////////////////////////

};

namespace csDriverOEMPA
{

	[StructLayout(LayoutKind::Sequential)]
	public ref class csRoot
	{
	public:
		bool vCopyTo(void *pRoot){return CopyTo((structRoot*)pRoot);};
		bool vCopyFrom(void *pRoot){return CopyFrom((structRoot*)pRoot);};
		bool CopyTo(structRoot *pRoot);
		bool CopyFrom(structRoot *pRoot);

		DWORD dwRootSize;
		ULONGLONG ullSavedParameters;//each bit is for one parameter.
		UINT uiSavedFilterCount;
	csEnumHardwareLink csDefaultHwLink;
	bool bEnableFMC;//OEMPA_ENABLE_FMC
	bool bEnableMultiHWChannel;//OEMPA_ENABLE_MULTI_CHANNEL
		bool bAscanEnable;//OEMPA_ASCAN_ENABLE
		bool bEnableCscanTof;//OEMPA_CSCAN_ENABLE_TOF
	csEnumTFMParameters csEnableTFM;
		csEnumBitSize csAscanBitSize;//OEMPA_ASCAN_BITSIZE
		csEnumAscanRequest csAscanRequest;//OEMPA_ASCAN_REQUEST
		double dAscanRequestFrequency;//OEMPA_ASCAN_REQUEST
		csEnumOEMPATrigger csTriggerMode;//OEMPA_TRIGGER_MODE
		csEnumOEMPAEncoderDirection csEncoderDirection;//OEMPA_ENCODER_TRIG_DIRECTION
		char cTemperatureWarning;//OEMPA_TEMPERATURE_WARNING should be >=0.
		char cTemperatureAlarm;//OEMPA_TEMPERATURE_ALARM should be >=0.
		double dTriggerEncoderStep;//OEMPA_TRIGGER_ENCODER_STEP
		csEnumOEMPARequestIO csRequestIO;//OEMPA_REQUESTIO
		int iRequestIODigitalInputMaskRising;//OEMPA_REQUESTIO_DIGITAL_INPUT_MASK
		int iRequestIODigitalInputMaskFalling;//OEMPA_REQUESTIO_DIGITAL_INPUT_MASK
		double dDebouncerEncoder;//OEMPA_DEBOUNCER_ENCODER
		double dDebouncerDigital;//OEMPA_DEBOUNCER_DIGITAL
		[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iOEMPAMappingDigitalOutputMax)]
		cli::array<csEnumOEMPAMappingDigitalOutput>^ csDigitalOuput;//OEMPA_DIGITAL_OUTPUT_0, OEMPA_DIGITAL_OUTPUT_1 ... OEMPA_DIGITAL_OUTPUT_5
		long lSWEncoderResolution1;//OEMPA_SW_ENCODER1_RESOLUTION SW parameter
		long lSWEncoderResolution2;//OEMPA_SW_ENCODER2_RESOLUTION SW parameter
		DWORD dwSWEncoderDivider1;//OEMPA_SW_ENCODER1_DIVIDER SW parameter
		DWORD dwSWEncoderDivider2;//OEMPA_SW_ENCODER2_DIVIDER SW parameter
		csEnumDigitalInput csEncoder1A;//OEMPA_ENCODER1A
		csEnumDigitalInput csEncoder1B;//OEMPA_ENCODER1B
		csEnumDigitalInput csEncoder2A;//OEMPA_ENCODER2A
		csEnumDigitalInput csEncoder2B;//OEMPA_ENCODER2B
		csEnumDigitalInput csExternalTriggerCycle;//OEMPA_EXTERNAL_TRIGGER_CYCLE
		csEnumDigitalInput csExternalTriggerSequence;//OEMPA_EXTERNAL_TRIGGER_SEQUENCE
		csEnumEncoderType csEncoder1Type;//OEMPA_ENCODER1_TYPE
		csEnumEncoderType csEncoder2Type;//OEMPA_ENCODER2_TYPE
		csEnumKeepAlive csKeepAlive;//OEMPA_KEEPALIVE
		//Configuration parameters : end

		int iCycleCount;//-1 means HW cycles are not modified, otherwise the HW cycles are updated.
						//0 the "SetCycleCount" function is called with value 0.
						//1 the "SetCycleCount" function is called with value 1.
						//...
		int iDACCountMax;
		int iDDFCountMax;
		[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iEnumOEMPAFilterIndexLast)]
		cli::array<csFilter^>^ aFilter;//OEMPA_DIGITAL_OUTPUT_0, OEMPA_DIGITAL_OUTPUT_1 ... OEMPA_DIGITAL_OUTPUT_5
		int iFMCElementStart;//FullMatrixCapture
		int iFMCElementStop;//FullMatrixCapture
		int iFMCElementStep;//FullMatrixCapture

		double dSpecimenVelocity;//view correction purpose (m/s)
	double dSpecimenRadius;//meter (if 0.0 it is a plane).
	double dSpecimenThickness;//meter (0.0 means not defined).
		double dDigitizingPeriod;//view correction purpose (second)
		int iOEMPAProbeCountMax;
		int iOEMPAApertureCountMax;
	int iOEMPADDFCountMax;
	BYTE bUSB3Disable;
	double dMultiHWChannelRangeMax;//OEMMC max range
	double dFWAscanRecoveryTime;//phased cli::array or OEM-MC
	double dTriggerHighTime;
	
	//sub-sequence table management
	int iSubSequenceEncoderCount;//size of aiSubSequenceCycleStart and aiSubSequenceCycleStop in case Position is used (size of afSubSequenceValue is one more).
	int iSubSequenceGateCount;//size of aiSubSequenceCycleStart and aiSubSequenceCycleStop in case Gate is used (size of afSubSequenceValue is the same).
	[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iSubSequenceTableSize)]
	cli::array<int>^ aiSubSequenceCycleStart;
	[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iSubSequenceTableSize)]
	cli::array<int>^ aiSubSequenceCycleStop;
	[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iSubSequenceTableSize)]
	cli::array<float>^ afSubSequenceValue;//unit is either millimeter (Position) either second (Gate).
	int iSubSequenceAverage;

	//cycles order.
	bool bAverage;
	bool bCycleOrderEnable;
	int iCycleOrderCount;
	[MarshalAs(UnmanagedType::ByValArray, SizeConst = 65536)]
	cli::array<int>^ aiCycleOrder;

	csEnumReferenceTimeOfFlight eReferenceTimeOfFlight;

	bool b256Master;
	bool b256Slave;

		void* /*CHWDeviceOEMPA*/ pHWDeviceOEMPA;//1.1.5.4g void is better.
		[MarshalAs(UnmanagedType::ByValArray, SizeConst = MAX_PATH)]
		cli::array<wchar_t>^ wcFileName;//wchar_t wcFileName[MAX_PATH];
	};
	[StructLayout(LayoutKind::Sequential)]
	public ref class csGate
	{
	public:
		//ToDo bool CopyTo(structRoot *pRoot);
		//ToDo bool CopyFrom(structRoot *pRoot);

		bool bEnable;
		double dStart;//unit = second
		double dStop;//unit = second
		double dThreshold;//unit = percent
		csEnumRectification eRectification;
		csEnumGateModeAmp eModeAmp;
		csEnumGateModeTof eModeTof;
		WORD wAcqIDAmp;
		WORD wAcqIDTof;
		//tracking
		bool bTrackingStartEnable;
		int iTrackingStartIndexCycle;
		int iTrackingStartIndexGate;
		bool bTrackingStopEnable;
		int iTrackingStopIndexCycle;
		int iTrackingStopIndexGate;
	};

	[StructLayout(LayoutKind::Sequential)]
	public ref class csCycle
	{
	public:
		bool vCopyTo(void *pCycle){return CopyTo((structCycle*)pCycle);};
		bool vCopyFrom(void *pCycle){return CopyFrom((structCycle*)pCycle);};
		bool CopyTo(structCycle *pCycle);
		bool CopyFrom(structCycle *pCycle);

		double dGainDigital;
		float fBeamCorrection;
		double dStart;//unit = second
		double dRange;//unit = second
		double dTimeSlot;//unit = second
	double dFMCSubTimeSlotAcq;//unit = second
	double dFMCSubTimeSlotReplay;//unit = second
		long lPointCount;
		long lPointFactor;//1.1.5.4g
		csEnumCompressionType eCompressionType;
		csEnumRectification eRectification;
		[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iOEMPADACCountMax)]
		cli::array<double>^ adDACTof;//unit = second
		[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iOEMPADACCountMax)]
		cli::array<float>^ afDACPrm;//unit = dB/second (in case of slope) and dB (in case of gain), see "bDACSlope".
		int iDACCount;
		bool bDACSlope;
		bool bDACEnable;
		bool bDACAutoStop;
		bool bMaximum;
		bool bMinimum;
		bool bSaturation;
		WORD wAcqIdChannelProbe;
		WORD wAcqIdChannelScan;
		WORD wAcqIdChannelCycle;
		float fGainAnalog;//unit = dB
		int iFilterIndex;//0=no filter, and range 1 to 15 to select one filter.
		//tracking: ascan and DAC 1.1.5.4i
		bool bTrackingAscanEnable;
		int iTrackingAscanIndexCycle;
		int iTrackingAscanIndexGate;
		bool bTrackingDacEnable;
		int iTrackingDacIndexCycle;
		int iTrackingDacIndexGate;

		int iGateCount;
		[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iOEMPAGateCountMax)]
		cli::array<csGate^>^ gate;
	};

	[StructLayout(LayoutKind::Sequential)]
	public ref class csFocalLaw
	{
	public:
		bool vCopyTo(void *pFocalLaw){return CopyTo((CFocalLaw*)pFocalLaw);};
		bool vCopyFrom(void *pFocalLaw){return CopyFrom((CFocalLaw*)pFocalLaw);};
		bool CopyTo(CFocalLaw *pFocalLaw);
		bool CopyFrom(CFocalLaw *pFocalLaw);

		double dWedgeDelay;//unit = second.
		int iElementCount;//count of element in the aperture.
		[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iOEMPAApertureDWordCount)]
		cli::array<DWORD>^ adwElement;//bit i : 0 if element is disabled, 1 if element is enabled. 2x32 = 64 elements.
		csEnumFocusing csFocusing;//to enable DDF.
		int iDelayCount;//count of useful delays = "iFocalCount" x "iElementCount".
		int iFocalCount;//count of focal law.
		//[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iOEMPAFocalCountMax*g_iOEMPAApertureElementCountMax)]
		cli::array<float,2>^ afDelay;//unit = second, first index = focal law index, 2d index = element.
		[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iOEMPAFocalCountMax)]
		cli::array<double>^ adFocalTimeOfFlight;//unit = second.
		float fCenterDelay;//delay for the aperture centroid.
		int iPrmCount;//useful item count in cli::array "afPrm". Copy of "iElementCount".
		[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iOEMPAApertureElementCountMax)]
		cli::array<float>^ afPrm;//gain (dB) or width (second)
		double dSkew;//view correction purpose
		double dAngle;//view correction purpose
		double dX,dY,dZ;//view correction purpose
		double dFocalX,dFocalY,dFocalZ;//last focal point (absolute referential)
		[MarshalAs(UnmanagedType::ByValArray, SizeConst = g_iOEMPAApertureElementCountMax/2)]
		cli::array<BYTE>^ hwAcqDecimation;//one BYTE is for 2 channels (that is 1 decimation 4 bits).

		float GetDelay(int iFocalIndex,int iElementIndex);
		bool SetDelay(int iFocalIndex,int iElementIndex,float fDelay);
		int GetHWAcquisitionDecimation(int iChannelIndex);
	};

};
