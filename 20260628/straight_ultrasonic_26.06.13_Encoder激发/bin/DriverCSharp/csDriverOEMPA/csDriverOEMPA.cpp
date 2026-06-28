#include "stdafx.h"
#include <gcroot.h>
#include "win_stub.h"

#pragma managed(push, off)
#ifdef _DRIVER_11XY_
#include "UTKernelDriver.h"
#include "UTKernelDriverOEMPA.h"
#include "CustomizedDriverAPI.h"
#else //_DRIVER_11XY_
#include "UTDriverOEMPA.h"
#include "UTDriverOEMPA1.h"
#include "UTDriverOEMPA2.h"
#include "UTDriverOEMPAminiMax.h"
#include "UTDriverOEMMC.h"
#include "UTDriverOEMPAsave.h"
#include "UTDriverOEMPAX.h"
#include "CustomizedDriverOEMPA.h"
#endif //_DRIVER_11XY_
#ifndef _CONST_VOID_
#define _CONST_VOID_
typedef union constVoid{
	const void *pcVoid;
	void *pVoid;
}constVoid;
#endif //_CONST_VOID_

#pragma managed(push, on)
#include "csDriverOEMPA.h"

#ifdef _DEBUG
#include <crtdbg.h>
#endif //_DEBUG

DllImport bool UTDriver_IsUserInterfaceThread();
void init(structAcqInfoEx &acqInfo);
void init(CStream_0x0001 &StreamHeader);
void init(CSubStreamIO_0x0101 &ioHeader);
void init(structCscanAmp_0x0102 &bufferAmp);
void init(structCscanAmpTof_0x0202 &bufferAmpTof);
void init(CSubStreamCscan_0x0X02 &cscanHeader);
void init(CSubStreamAscan_0x0103 &AscanHeader);
DllImport void debug_EnableHeapEx(bool bEnable,char *pstrModuleName);
DllExport bool debug_DumpHeap(char *pFileName,bool bStatistics);

UINT WINAPI gAcquisitionAscan_0x00010103(void *pAcquisitionParameter,structAcqInfoEx &acqInfo,const CStream_0x0001 *pStreamHeader,const CSubStreamAscan_0x0103 *pAscanHeader,const void *pBufferMax,const void *pBufferMin,const void *pBufferSat);
UINT WINAPI gAcquisitionAscan_0x00020203(void* pAcquisitionParameter, const CStream_0x0002* pStreamHeader, const CSubStreamAscan_0x0203* pAscanHeader, const void* pBufferMax, const void* pBufferMin, const void* pBufferSat);
UINT WINAPI gAcquisitionCscan_0x00010X02(void *pAcquisitionParameter,structAcqInfoEx &acqInfo,const CStream_0x0001 *pStreamHeader,const CSubStreamCscan_0x0X02 *pCscanHeader,const structCscanAmp_0x0102 *pBufferAmp, const structCscanAmpTof_0x0202 *pBufferAmpTof);
UINT WINAPI gAcquisitionCscan_0x00020402(void* pAcquisitionParameter, structAcqInfoEx& acqInfo, const CStream_0x0002* pStreamHeader, const CSubStreamCscan_0x0402* pCscanHeader, const structCscanAmpTof_0x0402* pBufferAmpTof);
UINT WINAPI gAcquisitionIO_0x00010101(void *pAcquisitionParameter,const CStream_0x0001 *pStreamHeader,const CSubStreamIO_0x0101 *pIOHeader);
UINT WINAPI gAcquisitionIO_1x00010101(void *pAcquisitionParameter,structAcqInfoEx &acqInfo,const CStream_0x0001 *pStreamHeader,const CSubStreamIO_0x0101 *pIOHeader);
UINT WINAPI gAcquisitionInfo(void *pAcquisitionParameter,const wchar_t *pInfo);

#ifndef _DRIVER_11XY_
structCorrectionOEMPA* WINAPI gCallbackCustomizedOEM(CHWDeviceOEMPA *pHWDeviceOEMPA,const wchar_t *pFileName,enumStepCustomizedAPI eStep,structRoot *pRoot,structCycle *pCycle,CFocalLaw *pEmission,CFocalLaw *pReception);
#else //_DRIVER_11XY_
structCorrectionOEMPA* WINAPI gCallbackCustomizedOEM(void *pHWDeviceOEMPA,const wchar_t *pFileName,enumStepCustomizedAPI eStep,structRoot *pRoot,structCycle *pCycle,CFocalLaw *pEmission,CFocalLaw *pReception);
#endif //_DRIVER_11XY_
void WINAPI gCallbackSystemMessageBox(const wchar_t *pMsg);
void WINAPI gCallbackSystemMessageBoxList(const wchar_t *pMsg);
UINT WINAPI gCallbackSystemMessageBoxButtons(const wchar_t *pMsg,const wchar_t *pTitle,UINT nType);//should return IDOK IDYES IDNO...depending of the button pressed by the user.
int WINAPI gCallbackOempaApiMessageBox(HWND hWnd,const wchar_t *lpszText,const wchar_t *lpszCaption,UINT nType);

bool WINAPI gCallbackSetSizeDouble(struct structCallbackArrayDouble1D *pCallbackArray,int iSize);
bool WINAPI gCallbackSetDataDouble(struct structCallbackArrayDouble1D *pCallbackArray,int iIndex,double fData);
bool WINAPI gCallbackGetSizeDouble(struct structCallbackArrayDouble1D *pCallbackArray,int &iSize);
bool WINAPI gCallbackGetDataDouble(struct structCallbackArrayDouble1D *pCallbackArray,int iIndex,double &fData);

bool WINAPI gCallbackSetSizeFloat(struct structCallbackArrayFloat1D *pCallbackArray,int iSize);
bool WINAPI gCallbackSetDataFloat(struct structCallbackArrayFloat1D *pCallbackArray,int iIndex,float fData);
bool WINAPI gCallbackGetSizeFloat(struct structCallbackArrayFloat1D *pCallbackArray,int &iSize);
bool WINAPI gCallbackGetDataFloat(struct structCallbackArrayFloat1D *pCallbackArray,int iIndex,float &fData);

bool WINAPI gCallbackSetSizeDelay2(struct structCallbackArrayFloat2D *pCallbackArray,int iSize1,int iSize2);
bool WINAPI gCallbackSetDataDelay2(struct structCallbackArrayFloat2D *pCallbackArray,int iIndex1,int iIndex2,float fData);
bool WINAPI gCallbackGetSizeDelay2(struct structCallbackArrayFloat2D *pCallbackArray,int &iSize1,int &iSize2);
bool WINAPI gCallbackGetDataDelay2(struct structCallbackArrayFloat2D *pCallbackArray,int iIndex1,int iIndex2,float &fData);

bool WINAPI gCallbackSetSizeDac(struct structCallbackArrayFloatDac *pCallbackArray,int iSize);
bool WINAPI gCallbackSetDataDac(struct structCallbackArrayFloatDac *pCallbackArray,int iIndex,double dTime,float fSlope);
bool WINAPI gCallbackGetSizeDac(struct structCallbackArrayFloatDac *pCallbackArray,int &iSize);
bool WINAPI gCallbackGetDataDac(struct structCallbackArrayFloatDac *pCallbackArray,int iIndex,double &dTime,float &fSlope);

bool WINAPI gCallbackSetSizeByte1D(struct structCallbackArrayByte1D *pCallbackArray,int iSize);
bool WINAPI gCallbackSetDataByte1D(struct structCallbackArrayByte1D *pCallbackArray,int iIndex,BYTE byData);
bool WINAPI gCallbackGetSizeByte1D(struct structCallbackArrayByte1D *pCallbackArray,int &iSize);
bool WINAPI gCallbackGetDataByte1D(struct structCallbackArrayByte1D *pCallbackArray,int iIndex,BYTE &byData);

void gCallbackHWMemory(CHWDevice *pHWDevice, DWORD addr, DWORD data, int size);

//structCallbackArrayDouble1D g_callback[4096];
//structCallbackArrayFloatDac g_callbackDAC[4096];

#ifndef _DRIVER_11XY_
namespace csDriverOEMPA
#else //_DRIVER_11XY_
namespace csDriverOEM
#endif //_DRIVER_11XY_
{
	float csFocalLaw::GetDelay(int iFocalIndex,int iElementIndex)
	{
		if(iFocalIndex<0)
			return false;
		if(iFocalIndex>=afDelay->GetLength(0))
			return false;
		if(iElementIndex<0)
			return false;
		if(iElementIndex>=afDelay->GetLength(1))
			return false;
		//fDelay = afDelay[iFocalIndex*g_iOEMPAApertureElementCountMax+iElementIndex];
		return afDelay[iFocalIndex,iElementIndex];
	}
	bool csFocalLaw::SetDelay(int iFocalIndex,int iElementIndex,float fDelay)
	{
		if(iFocalIndex<0)
			return false;
		if(iFocalIndex>=afDelay->GetLength(0))
			return false;
		if(iElementIndex<0)
			return false;
		if(iElementIndex>=afDelay->GetLength(1))
			return false;
		//afDelay[iFocalIndex*g_iOEMPAApertureElementCountMax+iElementIndex] = fDelay;
		afDelay[iFocalIndex,iElementIndex] = fDelay;
		return true;
	}
	int csFocalLaw::GetHWAcquisitionDecimation(int iChannelIndex)
	{
		if(hwAcqDecimation==nullptr)
			return 0;
		if(iChannelIndex<0)
			return 0;
		if(iChannelIndex>=hwAcqDecimation->GetLength(0))
			return 0;
		if(!(iChannelIndex%2))
			return (hwAcqDecimation[iChannelIndex/2] & 0xf);
		else
			return ((hwAcqDecimation[iChannelIndex/2]>>4) & 0xf);
	}
////////////////////////////////////////////////////////
#pragma region csSWEncoder
	csSWEncoder::csSWEncoder(CSWEncoder *pSWEncoder)
	{
		Free();
		Constructor(pSWEncoder);
	}
	csSWEncoder::~csSWEncoder()
	{
		this->!csSWEncoder();
	}
	csSWEncoder::!csSWEncoder()
	{
		Free();
	}
	void csSWEncoder::Constructor(CSWEncoder *pSWEncoder)
	{
		m_pSWEncoder = pSWEncoder;
	}
	void csSWEncoder::Free()
	{
		//m_pHWDevice = NULL;
		m_pSWEncoder = NULL;
	}

//setting part
	bool csSWEncoder::Enable(bool bEnabled)//value could be negative if encoder need to be inverted.
	{
		if(!m_pSWEncoder)
			return false;
		return m_pSWEncoder->Enable(bEnabled);
	}
	bool csSWEncoder::IsEnabled()
	{
		if(!m_pSWEncoder)
			return false;
		return m_pSWEncoder->IsEnabled();
	}

	bool csSWEncoder::lSetResolution(long lResolution)
	{
		if(!m_pSWEncoder)
			return false;
		return m_pSWEncoder->lSetResolution(lResolution);
	}
	long csSWEncoder::lGetResolution()
	{
		if(!m_pSWEncoder)
			return 0;
		return m_pSWEncoder->lGetResolution();
	}
	DWORD csSWEncoder::GetDivider()
	{
		if(!m_pSWEncoder)
			return 0;
		return m_pSWEncoder->GetDivider();
	}
	bool csSWEncoder::dSetResolution(double dResolution)
	{
		if(!m_pSWEncoder)
			return false;
		return m_pSWEncoder->dSetResolution(dResolution);
	}
	double csSWEncoder::dGetResolution()
	{
		if(!m_pSWEncoder)
			return 0;
		return m_pSWEncoder->dGetResolution();
	}
	bool csSWEncoder::lSetResolution(long lResolution,DWORD dwDivider)
	{
		if(!m_pSWEncoder)
			return false;
		return m_pSWEncoder->lSetResolution(lResolution,dwDivider);
	}

	bool csSWEncoder::SetUnit(csEnumUnit eUnit)
	{
		if(!m_pSWEncoder)
			return false;
		return m_pSWEncoder->SetUnit((enumUnit)eUnit);
	}
	csEnumUnit csSWEncoder::GetUnit()
	{
		if(!m_pSWEncoder)
			return (csEnumUnit)0;
		return (csEnumUnit)m_pSWEncoder->GetUnit();
	}

	bool csSWEncoder::SetType(csEnumEncoderType eEncoderType)
	{
		if(!m_pSWEncoder)
			return false;
		return m_pSWEncoder->SetType((enumEncoderType)eEncoderType);
	}
	csEnumEncoderType csSWEncoder::GetType()
	{
		if(!m_pSWEncoder)
			return (csEnumEncoderType)0;
		return (csEnumEncoderType)m_pSWEncoder->GetType();
	}

	bool csSWEncoder::SetDigitalInput(csEnumFeatureDigitalInput eEncoderInput,csEnumDigitalInput eDigitalInput)
	{
		if(!m_pSWEncoder)
			return false;
		return m_pSWEncoder->SetDigitalInput((enumFeatureDigitalInput)eEncoderInput,(enumDigitalInput)eDigitalInput);
	}
	csEnumDigitalInput csSWEncoder::GetDigitalInput(csEnumFeatureDigitalInput eEncoderInput)
	{
		enumDigitalInput eDigitalInput;

		if(!m_pSWEncoder)
			return (csEnumDigitalInput)0;
		eDigitalInput = m_pSWEncoder->GetDigitalInput((enumFeatureDigitalInput)eEncoderInput);
		return (csEnumDigitalInput)eDigitalInput;
	}

//acquisition part
	bool csSWEncoder::SetSpeedDistance(double dValue)
	{
		if(!m_pSWEncoder)
			return false;
		return m_pSWEncoder->SetSpeedDistance(dValue);
	}
	bool csSWEncoder::GetSpeedDistance(double %dValue)
	{
		double dValue2;
		bool bRet;
		
		if(!m_pSWEncoder)
			return false;
		bRet = m_pSWEncoder->GetSpeedDistance(dValue2);
		dValue = dValue2;
		return bRet;
	}

	bool csSWEncoder::SetInspectionHWValue(int iValue)
	{
		if(!m_pSWEncoder)
			return false;
		return m_pSWEncoder->SetInspectionHWValue(iValue);
	}
	int csSWEncoder::GetInspectionHWValue()
	{
		if(!m_pSWEncoder)
			return 0;
		return m_pSWEncoder->GetInspectionHWValue();
	}
	bool csSWEncoder::SetInspectionSWValue(double dValue)
	{
		if(!m_pSWEncoder)
			return false;
		return m_pSWEncoder->SetInspectionSWValue(dValue);
	}
	double csSWEncoder::GetInspectionSWValue()
	{
		if(!m_pSWEncoder)
			return 0.0;
		return m_pSWEncoder->GetInspectionSWValue();
	}

	bool csSWEncoder::SetInspectionSpeed(double dValue)
	{
		if(!m_pSWEncoder)
			return false;
		return m_pSWEncoder->SetInspectionSpeed(dValue);
	}
	double csSWEncoder::GetInspectionSpeed()
	{
		if(!m_pSWEncoder)
			return 0.0;
		return m_pSWEncoder->GetInspectionSpeed();
	}

	bool csSWEncoder::SetInspectionLength(double dValue)
	{
		if(!m_pSWEncoder)
			return false;
		return m_pSWEncoder->SetInspectionLength(dValue);
	}
	double csSWEncoder::GetInspectionLength()
	{
		if(!m_pSWEncoder)
			return 0.0;
		return m_pSWEncoder->GetInspectionLength();
	}

	bool csSWEncoder::SetInspectionCount(int iValue)
	{
		if(!m_pSWEncoder)
			return false;
		return m_pSWEncoder->SetInspectionCount(iValue);
	}
	int csSWEncoder::GetInspectionCount()
	{
		if(!m_pSWEncoder)
			return 0;
		return m_pSWEncoder->GetInspectionCount();
	}
#pragma endregion csSWEncoder
////////////////////////////////////////////////////////
#pragma region csSWDevice
	csSWDevice::csSWDevice()
	{
		Free();
	}
	csSWDevice::~csSWDevice()
	{
		this->!csSWDevice();
	}
	csSWDevice::!csSWDevice()
	{
		Free();
	}
	csSWEncoder ^csSWDevice::GetSWEncoder(int iEncoderIndex)
	{
		if(iEncoderIndex<0)
			return nullptr;
		if(iEncoderIndex>=g_iDriverEncoderCountMax)
			return nullptr;
		if(!iEncoderIndex)
			return m_csSWEncoder1;
		return m_csSWEncoder2;
	}
	void csSWDevice::Constructor(CHWDeviceOEMPA *pHWDeviceOEMPA,CHWDevice *pHWDevice)
	{
		CSWEncoder *pSWEncoder;

		pSWEncoder = pHWDeviceOEMPA->GetSWEncoder(0);
		m_csSWEncoder1 = gcnew csSWEncoder(pSWEncoder);
		pSWEncoder = pHWDeviceOEMPA->GetSWEncoder(1);
		m_csSWEncoder2 = gcnew csSWEncoder(pSWEncoder);
		m_pHWDeviceOEMPA = pHWDeviceOEMPA;
		m_pHWDevice = pHWDevice;
		m_pSWDevice = m_pHWDevice->GetSWDevice();
	}
	void csSWDevice::Free()
	{
		m_csSWEncoder1 = nullptr;
		m_csSWEncoder2 = nullptr;
		m_pHWDeviceOEMPA = NULL;
		m_pHWDevice = NULL;
		m_pSWDevice = NULL;
	}

	csEnumHardware csSWDevice::GetHardware()
	{
		if(!m_pSWDevice)
			return csEnumHardware::csNoHW;
		return (csEnumHardware)m_pSWDevice->GetHardware();
	}
	bool csSWDevice::SetBoardName(String ^pName)//to register this new data in the kernel database.
	{
		wchar_t* y;
		bool bRet;

		if(!m_pSWDevice)
			return false;
		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pName);
		bRet = m_pSWDevice->SetBoardName(y);
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}
	bool csSWDevice::GetBoardName([Out] String^ %pName)
	{
		wchar_t pAux[MAX_PATH];

		if(!m_pSWDevice)
			return false;
		if(!m_pSWDevice->GetBoardName(MAX_PATH,pAux))
			return false;
		pName = Marshal::PtrToStringUni((IntPtr)pAux);
		return true;
	}

	bool csSWDevice::GetConfigurationFilePath(bool bFlashName,String ^pPathName)//to find the configuration file path.
	{
		wchar_t pAux[MAX_PATH];

		if(!m_pSWDevice)
			return false;
		if(!m_pSWDevice->GetConfigurationFilePath(bFlashName,pAux,MAX_PATH))
			return false;
		pPathName = Marshal::PtrToStringUni((IntPtr)pAux);
		return true;
	}

	bool csSWDevice::GetSetupFileDefault(csEnumSetupFileType csFileType, [Out] String^ %wcFile)
	{
		wchar_t pAux[MAX_PATH];

		if (!m_pSWDevice)
			return false;
		if (!m_pSWDevice->GetSetupFileDefault((enumSetupFileType)csFileType, MAX_PATH, pAux))
			return false;
		wcFile = Marshal::PtrToStringUni((IntPtr)pAux);
		return true;
	}
	bool csSWDevice::SetSetupFileCurrent(String^ wcFile)
	{
		wchar_t* y;
		bool bRet;

		if(!m_pSWDevice)
			return false;
		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(wcFile);
		bRet = m_pSWDevice->SetSetupFileCurrent(y);
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}
	bool csSWDevice::GetSetupFileCurrent([Out] String^ %wcFile)
	{
		wchar_t pAux[MAX_PATH];

		if(!m_pSWDevice)
			return false;
		if(!m_pSWDevice->GetSetupFileCurrent(MAX_PATH,pAux))
			return false;
		wcFile = Marshal::PtrToStringUni((IntPtr)pAux);
		return true;
	}

	csEnumCommunication csSWDevice::GetCommunication()
	{
		if (!m_pSWDevice)
			return csEnumCommunication::csNoCommunication;
		return (csEnumCommunication)m_pSWDevice->GetCommunication();
	}
	bool csSWDevice::SetConnectionState(csEnumConnectionState eConnection,bool bDisplayErrorMsg)
	{
		if(!m_pSWDevice)
			return 0.0;
		return m_pSWDevice->SetConnectionState((enumConnectionState)eConnection,bDisplayErrorMsg);
	}
	csEnumConnectionState csSWDevice::GetConnectionState()
	{
		if(!m_pSWDevice)
			return csEnumConnectionState::csDisconnected;
		return (csEnumConnectionState)m_pSWDevice->GetConnectionState();
	}
	bool csSWDevice::IsConnected()
	{
		if(!m_pSWDevice)
			return false;
		return m_pSWDevice->IsConnected();
	}

	bool csSWDevice::CheckProcessId()//only one process have ownership of the driver.
	{
		if(!m_pSWDevice)
			return false;
		return m_pSWDevice->CheckProcessId();
	}

	bool csSWDevice::SetCfgStatus(csEnumUpdateStatus eUpdateStatus)
	{
		if(!m_pSWDevice)
			return false;
		return m_pSWDevice->SetCfgStatus((enumUpdateStatus)eUpdateStatus);
	}
	csEnumUpdateStatus csSWDevice::GetCfgStatus()
	{
		if(!m_pSWDevice)
			return csEnumUpdateStatus::csOutOfDate;
		return (csEnumUpdateStatus)m_pSWDevice->GetCfgStatus();
	}

	int csSWDevice::SetStreamCount(int iCount)
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->SetStreamCount(iCount);
	}
	int csSWDevice::GetStreamCount()
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->GetStreamCount();
	}
	int csSWDevice::SetStreamDataSize(uint64_t ui64DataSize)
	{
		CSWDeviceOEMPA *pSWDeviceOEMPA;

		if(!m_pSWDevice)
			return 0;
		pSWDeviceOEMPA = dynamic_cast<CSWDeviceOEMPA*>(m_pSWDevice);
		if(!pSWDeviceOEMPA)
			return 0;
		return pSWDeviceOEMPA->SetStreamDataSize(ui64DataSize);
	}
	uint64_t csSWDevice::GetStreamDataSize()
	{
		CSWDeviceOEMPA *pSWDeviceOEMPA;

		if(!m_pSWDevice)
			return 0;
		pSWDeviceOEMPA = dynamic_cast<CSWDeviceOEMPA*>(m_pSWDevice);
		if(!pSWDeviceOEMPA)
			return 0;
		return pSWDeviceOEMPA->GetStreamDataSize();
	}
	int csSWDevice::SetStreamRetransmit(int iCount)
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->SetStreamRetransmit(iCount);
	}
	int csSWDevice::GetStreamRetransmit()
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->GetStreamRetransmit();
	}
	int csSWDevice::SetStreamError(int iCount)
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->SetStreamError(iCount);
	}
	int csSWDevice::GetStreamError()
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->GetStreamError();
	}

	int csSWDevice::GetLostCountAscan()
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->GetLostCountAscan();
	}
	int csSWDevice::SetLostCountAscan(int iDataLostCount)
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->SetLostCountAscan(iDataLostCount);
	}
	int csSWDevice::GetLostCountCscan()
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->GetLostCountCscan();
	}
	int csSWDevice::SetLostCountCscan(int iDataLostCount)
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->SetLostCountCscan(iDataLostCount);
	}
	int csSWDevice::GetLostCountEncoder()
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->GetLostCountEncoder();
	}
	int csSWDevice::SetLostCountEncoder(int iDataLostCount)
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->SetLostCountEncoder(iDataLostCount);
	}
	int csSWDevice::GetLostCountUSB3()
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->GetLostCountUSB3();
	}
	int csSWDevice::SetLostCountUSB3(int iDataLostCount)
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->SetLostCountUSB3(iDataLostCount);
	}
	int csSWDevice::GetLostCountSocket()
	{
		if (!m_pSWDevice)
			return 0;
		return m_pSWDevice->GetLostCountSocket();
	}
	int csSWDevice::SetLostCountSocket(int iDataLostCount)
	{
		if (!m_pSWDevice)
			return 0;
		return m_pSWDevice->SetLostCountSocket(iDataLostCount);
	}
	int csSWDevice::GetErrorCountUSB3()
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->GetErrorCountUSB3();
	}
	int csSWDevice::GetLostCountFifo(csEnumAcquisitionFifo csFifo)
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->GetLostCountFifo((enumAcquisitionFifo)csFifo);
	}
	void csSWDevice::ResetCounters()
	{
		if(!m_pSWDevice)
			return;
		m_pSWDevice->ResetCounters();
	}

	//bool SetEncoderCount(int iCount)//0 1 or 2.
	int csSWDevice::GetEncoderEnabledCount()
	{
		if(!m_pSWDevice)
			return 0;
		return m_pSWDevice->GetEncoderEnabledCount();
	}
	void csSWDevice::swProcessEncoder(structAcqInfoEx &acqInfo)
	{
		if(!m_pSWDevice)
			return;
		m_pSWDevice->swProcessEncoder(acqInfo);
	}

	bool csSWDevice::EnablePulser(bool bEnable)
	{
		if(!m_pSWDevice)
			return false;
		return m_pSWDevice->EnablePulser(bEnable);
	}
	bool csSWDevice::IsPulserEnabled()
	{
		if(!m_pSWDevice)
			return false;
		return m_pSWDevice->IsPulserEnabled();
	}

	bool csSWDevice::SetLockDefaultDisablePulser(bool bDisable)
	{
		if(!m_pSWDevice)
			return false;
		return m_pSWDevice->SetLockDefaultDisablePulser(bDisable);
	}
	bool csSWDevice::GetLockDefaultDisablePulser()
	{
		if(!m_pSWDevice)
			return false;
		return m_pSWDevice->GetLockDefaultDisablePulser();
	}
	bool csSWDevice::SetUnlockDefaultEnablePulser(bool bEnable)
	{
		if(!m_pSWDevice)
			return false;
		return m_pSWDevice->SetUnlockDefaultEnablePulser(bEnable);
	}
	bool csSWDevice::GetUnlockDefaultEnablePulser()
	{
		if(!m_pSWDevice)
			return false;
		return m_pSWDevice->GetUnlockDefaultEnablePulser();
	}

	bool csSWDevice::SetAcqSpeedAscan(double dSpeed)
	{
		if(!m_pSWDevice)
			return false;
		return m_pSWDevice->SetAcqSpeedAscan(dSpeed);
	}
	double csSWDevice::GetAcqSpeedAscan()
	{
		if(!m_pSWDevice)
			return 0.0;
		return m_pSWDevice->GetAcqSpeedAscan();
	}
	bool csSWDevice::SetAcqSpeedCscan(double dSpeed)
	{
		if(!m_pSWDevice)
			return false;
		return m_pSWDevice->SetAcqSpeedCscan(dSpeed);
	}
	double csSWDevice::GetAcqSpeedCscan()
	{
		if(!m_pSWDevice)
			return 0.0;
		return m_pSWDevice->GetAcqSpeedCscan();
	}
	bool csSWDevice::SetAcqSpeedIO(double dSpeed)
	{
		if(!m_pSWDevice)
			return false;
		return m_pSWDevice->SetAcqSpeedIO(dSpeed);
	}
	double csSWDevice::GetAcqSpeedIO()
	{
		if(!m_pSWDevice)
			return 0.0;
		return m_pSWDevice->GetAcqSpeedIO();
	}
#pragma endregion csSWDevice
////////////////////////////////////////////////////////
#pragma region csHWDevice
#ifdef _WIN64
	csAcquisitionFifo::csAcquisitionFifo(csEnumAcquisitionFifo csFifo,csHWDeviceOEMPA ^csHWDeviceOEMPA)
	{
		CHWDeviceOEMPA* pHWDeviceOEMPA;
		enumAcquisitionFifo eFifo=(enumAcquisitionFifo)csFifo;

		m_csHWDeviceOEM = csHWDeviceOEMPA;
		m_csFifo = csFifo;
		m_bNewFifo = false;
		pHWDeviceOEMPA = (CHWDeviceOEMPA*)m_csHWDeviceOEM->cGetHWDeviceOEMPA();
		if(pHWDeviceOEMPA)
			m_pAcquisitionFifo = (CAcquisitionFifo*)pHWDeviceOEMPA->GetAcquisitionFifo(eFifo);
		else
			m_pAcquisitionFifo = NULL;
	}
	csAcquisitionFifo::~csAcquisitionFifo()
	{
		this->!csAcquisitionFifo();
	}
	csAcquisitionFifo::!csAcquisitionFifo()
	{
		Free();
	}
	void csAcquisitionFifo::Free()
	{
		if(m_bNewFifo)
			delete m_pAcquisitionFifo;
		m_pAcquisitionFifo = NULL;
	}

	csEnumAcquisitionFifo csAcquisitionFifo::GetFifo()
	{
		return m_csFifo;
	}
	bool csAcquisitionFifo::IsEnabled()
	{
		if(!m_pAcquisitionFifo)
			return false;
		return m_pAcquisitionFifo->IsEnabled();
	}
	bool csAcquisitionFifo::IsRunning()
	{
		if(!m_pAcquisitionFifo)
			return false;
		return m_pAcquisitionFifo->IsRunning();
	}
	bool csAcquisitionFifo::Alloc(int iDataCountMax, int64_t iBufferByteSize)
	{
		if(!m_pAcquisitionFifo)
			return false;
		return m_pAcquisitionFifo->Alloc(iDataCountMax, iBufferByteSize);
	}
	bool csAcquisitionFifo::GetAlloc([Out] int %iDataCountMax, [Out] int64_t %iBufferByteSize)
	{
		bool bRet;
		int _iDataCountMax;
		int64_t _iBufferByteSize;

		if(!m_pAcquisitionFifo)
			return false;
		bRet = m_pAcquisitionFifo->GetAlloc(_iDataCountMax,_iBufferByteSize);
		iDataCountMax = _iDataCountMax;
		iBufferByteSize = _iBufferByteSize;
		return bRet;
	}
	bool csAcquisitionFifo::Desalloc()
	{
		if(!m_pAcquisitionFifo)
			return false;
		return m_pAcquisitionFifo->Desalloc();
	}

	//count of data in the fifo.
	int csAcquisitionFifo::GetCount()
	{
		if(!m_pAcquisitionFifo)
			return 0;
		return m_pAcquisitionFifo->GetCount();
	}
	int csAcquisitionFifo::GetLost()
	{
		if(!m_pAcquisitionFifo)
			return 0;
		return m_pAcquisitionFifo->GetLost();
	}
	int64_t csAcquisitionFifo::GetTotalCount()
	{
		if(!m_pAcquisitionFifo)
			return 0;
		return m_pAcquisitionFifo->GetTotalCount();
	}
	int64_t csAcquisitionFifo::GetTotalByteCount()
	{
		if(!m_pAcquisitionFifo)
			return 0;
		return m_pAcquisitionFifo->GetTotalByteCount();
	}
	void csAcquisitionFifo::ResetCounters()
	{
		if(!m_pAcquisitionFifo)
			return;
		m_pAcquisitionFifo->ResetCounters();
	}
	bool csAcquisitionFifo::RemoveAll()
	{
		if(!m_pAcquisitionFifo)
			return false;
		return m_pAcquisitionFifo->RemoveAll();
	}
	bool csAcquisitionFifo::RemoveTail()
	{
		if(!m_pAcquisitionFifo)
			return false;
		return m_pAcquisitionFifo->RemoveTail();
	}
	bool csAcquisitionFifo::RemoveItem(int iItem)
	{
		if(!m_pAcquisitionFifo)
			return false;
		return m_pAcquisitionFifo->RemoveItem(iItem);
	}
	bool csAcquisitionFifo::DumpFifoStatus(String ^pFileName)
	{
		wchar_t* y;
		bool bRet;

		if(!m_pAcquisitionFifo)
			return false;
		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pFileName);
		bRet = m_pAcquisitionFifo->DumpFifoStatus(true, y);
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}

	/*unsafe*/bool csAcquisitionFifo::OutAscan(int iItem,bool bPeek,[Out] csAcqInfoEx^ %acqInfo,[Out] csHeaderStream_0x0001^ %streamHeader,[Out] csSubStreamAscan_0x0103^ %ascanHeader,[Out] const void* %pBufferMax,[Out] const void* %pBufferMin,[Out] const void* %pBufferSat)
	{
		structAcqInfoEx* _acqInfo;
		CStream_0x0001* _streamHeader;
		CSubStreamAscan_0x0103* _ascanHeader;
		void *_pBufferMax,*_pBufferMin,*_pBufferSat;
		constVoid p;

		if(!m_pAcquisitionFifo)
			return false;
		if(!m_pAcquisitionFifo->OutAscan(iItem,bPeek,_acqInfo,_streamHeader,_ascanHeader,_pBufferMax,_pBufferMin,_pBufferSat))
			return false;
		p.pcVoid = _acqInfo;
		acqInfo = safe_cast<csAcqInfoEx^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csAcqInfoEx::typeid));
		p.pcVoid = _streamHeader;
		streamHeader = safe_cast<csHeaderStream_0x0001^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csHeaderStream_0x0001::typeid));
		p.pcVoid = _ascanHeader;
		ascanHeader = safe_cast<csSubStreamAscan_0x0103^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csSubStreamAscan_0x0103::typeid));
		pBufferMax = _pBufferMax;
		pBufferMin = _pBufferMin;
		pBufferSat = _pBufferSat;
		return true;
	}
	bool csAcquisitionFifo::OutCscan(int iItem,bool bPeek,[Out] csAcqInfoEx^ %acqInfo,[Out] csHeaderStream_0x0001^ %streamHeader,[Out] csSubStreamCscan_0x0X02^ %cscanHeader,[Out] cli::array<csCscanAmp_0x0102^>^ %bufferAmp,[Out] cli::array<csCscanAmpTof_0x0202^>^ %bufferAmpTof)
	{
		structAcqInfoEx* _acqInfo;
		CStream_0x0001* _streamHeader;
		const CSubStreamCscan_0x0X02* _cscanHeader;
		const structCscanAmp_0x0102* _pBufferAmp;
		const structCscanAmpTof_0x0202* _pBufferAmpTof;
		structCscanAmp_0x0102 *pAmp;
		structCscanAmpTof_0x0202 *pAmpTof;
		constVoid p;

		if(!m_pAcquisitionFifo)
			return false;
		if(!m_pAcquisitionFifo->OutCscan(iItem,bPeek,_acqInfo,_streamHeader,_cscanHeader,_pBufferAmp,_pBufferAmpTof))
			return false;
		p.pcVoid = _acqInfo;
		acqInfo = safe_cast<csAcqInfoEx^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csAcqInfoEx::typeid));
		p.pcVoid = _streamHeader;
		streamHeader = safe_cast<csHeaderStream_0x0001^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csHeaderStream_0x0001::typeid));
		p.pcVoid = _cscanHeader;
		cscanHeader = safe_cast<csSubStreamCscan_0x0X02^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csSubStreamCscan_0x0X02::typeid));
		if(_pBufferAmp)
		{
			p.pcVoid = _pBufferAmp;
			pAmp = (structCscanAmp_0x0102*)p.pVoid;
			bufferAmp = gcnew cli::array<csCscanAmp_0x0102^>(_cscanHeader->count);
			for(int i=0;i<(int)_cscanHeader->count;i++)
				bufferAmp[i] = safe_cast<csCscanAmp_0x0102^>(Marshal::PtrToStructure((IntPtr)&pAmp[i],csCscanAmp_0x0102::typeid));
		}
		if(_pBufferAmpTof)
		{
			p.pcVoid = _pBufferAmpTof;
			pAmpTof = (structCscanAmpTof_0x0202*)p.pVoid;
			bufferAmpTof = gcnew cli::array<csCscanAmpTof_0x0202^>(_cscanHeader->count);
			for(int i=0;i<(int)_cscanHeader->count;i++)
				bufferAmpTof[i] = safe_cast<csCscanAmpTof_0x0202^>(Marshal::PtrToStructure((IntPtr)&pAmpTof[i],csCscanAmpTof_0x0202::typeid));
		}
		return true;
	}
	bool csAcquisitionFifo::_OutIO(int iItem,bool bPeek,[Out] csHeaderStream_0x0001^ %streamHeader,[Out] csHeaderIO_0x0001^ %ioHeader)
	{
		CStream_0x0001* _streamHeader;
		const CSubStreamIO_0x0101* _ioHeader;
		constVoid p;

		if(!m_pAcquisitionFifo)
			return false;
		if(!m_pAcquisitionFifo->OutIO(iItem,bPeek,_streamHeader,_ioHeader))
			return false;
		p.pcVoid = _streamHeader;
		streamHeader = safe_cast<csHeaderStream_0x0001^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csHeaderStream_0x0001::typeid));
		p.pcVoid = _ioHeader;
		ioHeader = safe_cast<csHeaderIO_0x0001^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csHeaderIO_0x0001::typeid));
		return true;
	}
	bool csAcquisitionFifo::OutIO(int iItem,bool bPeek,[Out] csAcqInfoEx^ %acqInfo,[Out] csHeaderStream_0x0001^ %streamHeader,[Out] csHeaderIO_0x0001^ %ioHeader)
	{
		structAcqInfoEx* _acqInfo;
		CStream_0x0001* _streamHeader;
		const CSubStreamIO_0x0101* _ioHeader;
		constVoid p;

		if(!m_pAcquisitionFifo)
			return false;
		if(!m_pAcquisitionFifo->OutIO(iItem,bPeek,_acqInfo,_streamHeader,_ioHeader))
			return false;
		p.pcVoid = _acqInfo;
		acqInfo = safe_cast<csAcqInfoEx^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csAcqInfoEx::typeid));
		p.pcVoid = _streamHeader;
		streamHeader = safe_cast<csHeaderStream_0x0001^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csHeaderStream_0x0001::typeid));
		p.pcVoid = _ioHeader;
		ioHeader = safe_cast<csHeaderIO_0x0001^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csHeaderIO_0x0001::typeid));
		return true;
	}

	int csAcquisitionFifo::GetFifoItem(LONGLONG sequence, int iCycle, int iFMCElement, int iStartItem)
	{
		if(!m_pAcquisitionFifo)
			return false;
		return m_pAcquisitionFifo->GetFifoItem(sequence, iCycle, iFMCElement, iStartItem);
	}
	int csAcquisitionFifo::GetFifoItem(int iCycle, int iFMCElement, int iStartItem)
	{
		if(!m_pAcquisitionFifo)
			return false;
		return m_pAcquisitionFifo->GetFifoItem(iCycle, iFMCElement, iStartItem);
	}
	int csAcquisitionFifo::GetLifoItem(LONGLONG sequence, int iCycle, int iFMCElement, int iStartItem)
	{
		if(!m_pAcquisitionFifo)
			return false;
		return m_pAcquisitionFifo->GetLifoItem(sequence, iCycle, iFMCElement, iStartItem);
	}
	int csAcquisitionFifo::GetLifoItem(int iCycle, int iFMCElement, int iStartItem)
	{
		if(!m_pAcquisitionFifo)
			return false;
		return m_pAcquisitionFifo->GetLifoItem(iCycle, iFMCElement, iStartItem);
	}
	int csAcquisitionFifo::GetItemLimit([Out] int %iIndexTail, [Out] int %iIndexHead)
	{
		int _iIndexTail, _iIndexHead;
		int iRet;

		if(!m_pAcquisitionFifo)
			return false;
		iRet = m_pAcquisitionFifo->GetItemLimit(_iIndexTail, _iIndexHead);
		iIndexTail = _iIndexTail;
		iIndexHead = _iIndexHead;
		return iRet;
	}
	void csAcquisitionFifo::IncrementItemIndex(int %iIndex)
	{
		int _iIndex;

		if(!m_pAcquisitionFifo)
			return;
		_iIndex = iIndex;
		m_pAcquisitionFifo->IncrementItemIndex(_iIndex);
		iIndex = _iIndex;
	}
	void csAcquisitionFifo::DecrementItemIndex(int %iIndex)
	{
		int _iIndex;

		if(!m_pAcquisitionFifo)
			return;
		_iIndex = iIndex;
		m_pAcquisitionFifo->DecrementItemIndex(_iIndex);
		iIndex = _iIndex;
	}

	BYTE *csAcquisitionFifo::GetSubStreamItem(int iItem,[Out] int %iSubStreamDataSize,[Out] csAcqInfoEx^ %acqInfo,[Out] csHeaderStream_0x0001^ %streamHeader)
	{
		BYTE *pSubStream,byVersion;
		structAcqInfoEx* _acqInfo;
		CStream_0x0001* _streamHeader;
		constVoid p;
		int _iSubStreamDataSize;

		if(!m_pAcquisitionFifo)
			return NULL;
		pSubStream = m_pAcquisitionFifo->GetSubStreamItem(iItem,_iSubStreamDataSize,_acqInfo,_streamHeader,byVersion);
		p.pcVoid = _acqInfo;
		acqInfo = safe_cast<csAcqInfoEx^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csAcqInfoEx::typeid));
		p.pcVoid = _streamHeader;
		streamHeader = safe_cast<csHeaderStream_0x0001^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csHeaderStream_0x0001::typeid));
		if(byVersion!=1)
			return NULL;
		return pSubStream;
	}
	BYTE *csAcquisitionFifo::GetSubStreamItem(int iItem,[Out] int %iSubStreamDataSize,[Out] csAcqInfoEx^ %acqInfo,[Out] csHeaderStream_0x0001^ %streamHeader,BYTE %byVersion)
	{
		BYTE *pSubStream,_byVersion;
		structAcqInfoEx* _acqInfo;
		CStream_0x0001* _streamHeader;
		constVoid p;
		int _iSubStreamDataSize;

		if(!m_pAcquisitionFifo)
			return NULL;
		pSubStream = m_pAcquisitionFifo->GetSubStreamItem(iItem,_iSubStreamDataSize,_acqInfo,_streamHeader,_byVersion);
		p.pcVoid = _acqInfo;
		acqInfo = safe_cast<csAcqInfoEx^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csAcqInfoEx::typeid));
		p.pcVoid = _streamHeader;
		streamHeader = safe_cast<csHeaderStream_0x0001^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csHeaderStream_0x0001::typeid));
		byVersion = _byVersion;
		return pSubStream;
	}

	////The integrated thread is launched automatically by the driver, so the user dont have to call the following functions:
	//bool Start(int iType, void* pCallback, LPTHREAD_START_ROUTINE lpStartAddress, PVOID ThreadParameter);
	//bool Stop();
	//bool SetThreadPriority(int nPriority);//input parameter is explained explanation see documentation of "SetThreadPriority".
	//bool CreateEvent(int iDeviceId);
	//bool CreateEvent(wchar_t *pName);
	//HANDLE GetEvent();

	void csAcquisitionFifo::AddFifoLost(int iLostCount)
	{
		if(!m_pAcquisitionFifo)
			return;
		return m_pAcquisitionFifo->AddFifoLost(iLostCount);
	}
	DWORD csAcquisitionFifo::GetExit()
	{
		if(!m_pAcquisitionFifo)
			return false;
		return m_pAcquisitionFifo->GetExit();
	}
	void csAcquisitionFifo::Exit()
	{
		if(!m_pAcquisitionFifo)
			return;
		return m_pAcquisitionFifo->Exit();
	}
#else //_WIN64
	csAcquisitionFifo::csAcquisitionFifo(csEnumAcquisitionFifo csFifo,csHWDeviceOEMPA ^csHWDeviceOEMPA)
	{
	}
#endif //_WIN64

	csHWDevice::csHWDevice()
	{
		m_csSWDevice = nullptr;
		m_pHWDevice = NULL;
		m_pSWDevice = NULL;
		m_csAcquisitionParameter = nullptr;
		m_csAscan = nullptr;
		m_csCscan = nullptr;
		m_csIo0 = nullptr;
		m_csIo1 = nullptr;
		m_csInfo = nullptr;
		m_bDigitalEdgesOnly = false;
		m_pointer = NULL;
		g_CallbackHWMemory = nullptr;
	}
	csHWDevice::~csHWDevice()
	{
		this->!csHWDevice();
	}
	csHWDevice::!csHWDevice()
	{
		Free();
	}
	void csHWDevice::Constructor(System::Void *_pHWDeviceOEM,System::Void *_pHWDevice)
	{
		CHWDeviceOEMPA *pHWDeviceOEMPA=(CHWDeviceOEMPA*)_pHWDeviceOEM;
		CHWDevice *pHWDevice=(CHWDeviceOEMPA*)_pHWDevice;
		Constructor(pHWDeviceOEMPA,pHWDevice);
	}
	void csHWDevice::Constructor(CHWDeviceOEMPA *pHWDeviceOEMPA,CHWDevice *pHWDevice)
	{
		m_csSWDevice = gcnew csSWDevice();
		m_List = gcnew csPinList();

		if(m_csSWDevice!=nullptr)
			m_csSWDevice->Constructor(pHWDeviceOEMPA,pHWDevice);
		m_pHWDevice = pHWDevice;
		m_pSWDevice = m_pHWDevice->GetSWDevice();
		m_pointer = new gcroot<csHWDevice^>(this);
	}
	void csHWDevice::Constructor(csCustomizedOEMPA ^_csCustomizedAPI)
	{
		m_csCustomizedOEMPA = _csCustomizedAPI;
	}
	void csHWDevice::Free()
	{
		m_csSWDevice = nullptr;
		m_List = nullptr;
		m_pHWDevice = NULL;
		m_pSWDevice = NULL;
		if(m_pointer)
			delete m_pointer;
		m_pointer = NULL;
	}
	csAcquisitionFifo ^csHWDevice::GetAcquisitionFifo(csEnumAcquisitionFifo csFifo)
	{
		return nullptr;
	}
	csSWDevice ^csHWDevice::GetSWDevice()
	{
		return m_csSWDevice;
	}
	int csHWDevice::GetDeviceId()
	{
		if(!m_pHWDevice)
			return -1;
		return m_pHWDevice->GetDeviceId();
	}
	int csHWDevice::GetMonitorPort(int iDeviceId)
	{
		return CHWDevice::GetMonitorPort(iDeviceId);
	}
	csEnumHardwareLink csHWDevice::GetHardwareLink()
	{
		if(!m_pHWDevice)
			return csEnumHardwareLink::csUnlinked;
		return (csEnumHardwareLink)m_pHWDevice->GetHardwareLink();
	}
	int csHWDevice::GetMasterDeviceId()
	{
		if(!m_pHWDevice)
			return -1;
		return m_pHWDevice->GetMasterDeviceId();
	}
	bool csHWDevice::SetDefaultHwLink(csEnumHardwareLink csHardwareLink,[Out] bool %bPreviousMasterUnregistered)
	{
		bool bRet,bPreviousMasterUnregistered2;

		bPreviousMasterUnregistered = false;
		if(!m_pHWDevice)
			return false;
		bRet = m_pHWDevice->SetDefaultHwLink((enumHardwareLink)csHardwareLink,bPreviousMasterUnregistered2);
		bPreviousMasterUnregistered = bPreviousMasterUnregistered2;
		return bRet;
	}
	csEnumHardwareLink csHWDevice::GetDefaultHwLink()
	{
		if(!m_pHWDevice)
			return csEnumHardwareLink::csUnlinked;
		return (csEnumHardwareLink)m_pHWDevice->GetDefaultHwLink();
	}
	bool csHWDevice::IsDefaultHwLinkEnabled()
	{
		if(!m_pHWDevice)
			return false;
		return m_pHWDevice->IsDefaultHwLinkEnabled();
	}
	bool csHWDevice::SlaveConnect(int iMasterDeviceId)
	{
		if(!m_pHWDevice)
			return false;
		return m_pHWDevice->SlaveConnect(iMasterDeviceId);
	}
	bool csHWDevice::SlaveDisconnect()
	{
		if(!m_pHWDevice)
			return false;
		return m_pHWDevice->SlaveDisconnect();
	}

	bool csHWDevice::SetAcquisitionParameter(Object ^pParameter)//first parameter of the callback acquisition function.
	{
		m_csAcquisitionParameter = pParameter;
		if(m_csCustomizedOEMPA!=nullptr)
			m_csCustomizedOEMPA->SetAcquisitionParameter(pParameter);
		if(!m_pHWDevice)
			return false;
		return true;
	}
	Object ^csHWDevice::GetAcquisitionParameter()
	{
		return m_csAcquisitionParameter;
	}
	bool csHWDevice::SetAcquisitionAscan_0x00010103(TypeAcquisitionAscan_0x00010103 ^pProcess)
	{
		//IntPtr ip;

		m_csAscan = pProcess;
		if(!m_pHWDevice)
			return false;
		//ip = Marshal::GetFunctionPointerForDelegate(pProcess);
		//pAcquisitionAscan_0x00010103 = ip.ToPointer();
		m_pHWDevice->SetAcquisitionParameter(m_pointer);
		return m_pHWDevice->SetAcquisitionAscan_0x00010103(gAcquisitionAscan_0x00010103);
	}
	TypeAcquisitionAscan_0x00010103 ^csHWDevice::GetAcquisitionAscan_0x00010103()
	{
		return m_csAscan;
	}
	bool csDriverOEMPA::csHWDevice::SetAcquisitionAscan_0x00020203(TypeAcquisitionAscan_0x00020203^ pProcess)
	{
		m_csAscan2 = pProcess;
		if (!m_pHWDevice)
			return false;
		m_pHWDevice->SetAcquisitionParameter(m_pointer);
		return m_pHWDevice->SetAcquisitionAscan_0x00020203(gAcquisitionAscan_0x00020203);
	}
	TypeAcquisitionAscan_0x00020203^ csDriverOEMPA::csHWDevice::GetAcquisitionAscan_0x00020203()
	{
		return m_csAscan2;
	}
	bool csHWDevice::SetAcquisitionCscan_0x00010X02(TypeAcquisitionCscan_0x00010X02 ^pProcess)
	{
		m_csCscan = pProcess;
		if(!m_pHWDevice)
			return false;
		m_pHWDevice->SetAcquisitionParameter(m_pointer);
		return m_pHWDevice->SetAcquisitionCscan_0x00010X02(gAcquisitionCscan_0x00010X02);
	}
	TypeAcquisitionCscan_0x00010X02 ^csHWDevice::GetAcquisitionCscan_0x00010X02()
	{
		return m_csCscan;
	}
	bool csDriverOEMPA::csHWDevice::SetAcquisitionCscan_0x00020402(TypeAcquisitionCscan_0x00020402^ pProcess)
	{
		m_csCscan4 = pProcess;
		if (!m_pHWDevice)
			return false;
		m_pHWDevice->SetAcquisitionParameter(m_pointer);
		return m_pHWDevice->SetAcquisitionCscan_0x00020402(gAcquisitionCscan_0x00020402);
	}
	TypeAcquisitionCscan_0x00020402^ csDriverOEMPA::csHWDevice::GetAcquisitionCscan_0x00020402()
	{
		return m_csCscan4;
	}
	bool csHWDevice::SetAcquisitionIO_0x00010101(TypeAcquisitionIO_0x00010101 ^pProcess,bool bDigitalEdgesOnly)
	{
		m_bDigitalEdgesOnly = bDigitalEdgesOnly;
		m_csIo0 = pProcess;
		if(!m_pHWDevice)
			return false;
		m_pHWDevice->SetAcquisitionParameter(m_pointer);
		return m_pHWDevice->SetAcquisitionIO_0x00010101(gAcquisitionIO_0x00010101,bDigitalEdgesOnly);
	}
	TypeAcquisitionIO_0x00010101 ^csHWDevice::GetAcquisitionIO_0x00010101([Out] bool^ %bDigitalEdgesOnly)
	{
		bDigitalEdgesOnly = m_bDigitalEdgesOnly;
		return m_csIo0;
	}
	bool csHWDevice::SetAcquisitionIO_1x00010101(TypeAcquisitionIO_1x00010101 ^pProcess,bool bDigitalEdgesOnly)
	{
		m_bDigitalEdgesOnly = bDigitalEdgesOnly;
		m_csIo1 = pProcess;
		if(!m_pHWDevice)
			return false;
		m_pHWDevice->SetAcquisitionParameter(m_pointer);
		return m_pHWDevice->SetAcquisitionIO_1x00010101(gAcquisitionIO_1x00010101,bDigitalEdgesOnly);
	}
	TypeAcquisitionIO_1x00010101 ^csHWDevice::GetAcquisitionIO_1x00010101([Out] bool^ %bDigitalEdgesOnly)
	{
		bDigitalEdgesOnly = m_bDigitalEdgesOnly;
		return m_csIo1;
	}
	bool csHWDevice::SetAcquisitionInfo(TypeAcquisitionInfo ^pProcess)
	{
		m_csInfo = pProcess;
		if(!m_pHWDevice)
			return false;
		return m_pHWDevice->SetAcquisitionInfo(gAcquisitionInfo);
	}
	TypeAcquisitionInfo ^csHWDevice::GetAcquisitionInfo()
	{
		return m_csInfo;
	}

	bool csHWDevice::IsDriverEncoderManagementEnabled()
	{
		if(!m_pHWDevice)
			return false;
		return m_pHWDevice->IsDriverEncoderManagementEnabled();
	}
	void csHWDevice::EnableDriverEncoderManagement(bool bEnable)
	{
		if(!m_pHWDevice)
			return;
		m_pHWDevice->EnableDriverEncoderManagement(bEnable);
	}

	bool csHWDevice::LockDevice()
	{
		if(!m_pHWDevice)
			return false;
		m_List->Free();
		return m_pHWDevice->LockDevice();
	}
	bool csHWDevice::LockDevice(csEnumAcquisitionState eAcqState)
	{
		if(!m_pHWDevice)
			return false;
		m_List->Free();
		return m_pHWDevice->LockDevice((enumAcquisitionState)eAcqState);
	}
	bool csHWDevice::UnlockDevice()
	{
		bool bRet;

		if(!m_pHWDevice)
			return false;
		bRet = m_pHWDevice->UnlockDevice();
		m_List->Free();
		return bRet;
	}
	bool csHWDevice::UnlockDevice(csEnumAcquisitionState eAcqState)
	{
		bool bRet;

		if(!m_pHWDevice)
			return false;
		bRet = m_pHWDevice->UnlockDevice((enumAcquisitionState)eAcqState);
		m_List->Free();
		return bRet;
	}
	bool csHWDevice::IsDeviceLocked()
	{
		if(!m_pHWDevice)
			return false;
		return m_pHWDevice->IsDeviceLocked();
	}
	DWORD csHWDevice::GetSettingId()
	{
		if(!m_pHWDevice)
			return 0;
		return m_pHWDevice->GetSettingId();
	}

	bool csHWDevice::Flush()
	{
		if(!m_pHWDevice)
			return false;
		return m_pHWDevice->Flush();
	}
	bool csHWDevice::WriteHW(DWORD dwAddress,DWORD dwData,int iSize)
	{
		if(!m_pHWDevice)
			return false;
		return m_pHWDevice->WriteHW(dwAddress,dwData,iSize);
	}
	bool csHWDevice::WriteHW(int iCount,DWORD dwAddress,DWORD *pdwData,int iSize)
	{
		if(!m_pHWDevice)
			return false;
		return m_pHWDevice->WriteHW(iCount,dwAddress,pdwData,iSize);
	}
	/*unsafe*/bool csHWDevice::ReadHW(DWORD dwAddress,[Out] DWORD *pdwData,int iSize)
	{
		if(!m_pHWDevice)
			return false;
		return m_pHWDevice->ReadHW(dwAddress,pdwData,iSize);
	}
	/*unsafe*/bool csHWDevice::ReadHW(int iCount,DWORD dwAddress,[Out] DWORD *pdwData,int iSize)
	{
		if(!m_pHWDevice)
			return false;
		return m_pHWDevice->ReadHW(iCount,dwAddress,pdwData,iSize);
	}
	bool csHWDevice::SetCallbackHWMemory(TypeCallbackHWMemory ^callbackHWMemory)
	{
		if(m_pHWDevice->GetAcquisitionParameter() && (m_pHWDevice->GetAcquisitionParameter()!=m_pointer))
			return false;
		if(CHWDevice::GetCallbackHWMemory() && (CHWDevice::GetCallbackHWMemory()!=gCallbackHWMemory))
			return false;
		if(!CHWDevice::SetCallbackHWMemory(gCallbackHWMemory))
			return false;
		g_CallbackHWMemory = callbackHWMemory;
		return true;
	}
	TypeCallbackHWMemory ^csHWDevice::GetCallbackHWMemory()
	{
		if(CHWDevice::GetCallbackHWMemory()==gCallbackHWMemory)
			return g_CallbackHWMemory;
		return nullptr;
	}
	void csHWDevice::CallbackHWMemory(bool bMaster, DWORD addr, DWORD data, int size)
	{
		if(g_CallbackHWMemory==nullptr)
			return;
		g_CallbackHWMemory->Invoke(this, bMaster, addr, data, (unsigned long)size);
	}
	int csHWDevice::AcquisitionAscan_0x00010103(structAcqInfoEx &acqInfo_,const CStream_0x0001 *pStreamHeader,const CSubStreamAscan_0x0103 *pAscanHeader,const void *pBufferMax,const void *pBufferMin,const void *pBufferSat)
	{
		csAcqInfoEx^ acqInfo;
		csHeaderStream_0x0001^ headerStream;
		csSubStreamAscan_0x0103^ headerAscan;
		constVoid p;

		if(m_csAscan==nullptr)
			return 1;
		p.pcVoid = &acqInfo_;
		acqInfo = safe_cast<csAcqInfoEx^>(Marshal::PtrToStructure((IntPtr)&acqInfo_,csAcqInfoEx::typeid));
		p.pcVoid = pStreamHeader;
		headerStream = safe_cast<csHeaderStream_0x0001^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csHeaderStream_0x0001::typeid));
		p.pcVoid = pAscanHeader;
		headerAscan = safe_cast<csSubStreamAscan_0x0103^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csSubStreamAscan_0x0103::typeid));
		return m_csAscan->Invoke(m_csAcquisitionParameter,acqInfo,headerStream,headerAscan,pBufferMax,pBufferMin,pBufferSat);
	}
	int csHWDevice::AcquisitionAscan_0x00020203(/*structAcqInfoEx &acqInfo_, */const CStream_0x0002* pStreamHeader, const CSubStreamAscan_0x0203* pAscanHeader, const void* pBufferMax, const void* pBufferMin, const void* pBufferSat)
	{
		//csAcqInfoEx^ acqInfo;
		csHeaderStream_0x0002^ headerStream;
		csSubStreamAscan_0x0203^ headerAscan;
		constVoid p;

		if (m_csAscan2 == nullptr)
			return 1;
		/*p.pcVoid = &acqInfo_;
		acqInfo = safe_cast<csAcqInfoEx^>(Marshal::PtrToStructure((IntPtr)&acqInfo_, csAcqInfoEx::typeid));*/
		p.pcVoid = pStreamHeader;
		headerStream = safe_cast<csHeaderStream_0x0002^>(Marshal::PtrToStructure((IntPtr)p.pVoid, csHeaderStream_0x0002::typeid));
		p.pcVoid = pAscanHeader;
		headerAscan = safe_cast<csSubStreamAscan_0x0203^>(Marshal::PtrToStructure((IntPtr)p.pVoid, csSubStreamAscan_0x0203::typeid));
		return m_csAscan2->Invoke(m_csAcquisitionParameter, headerStream, headerAscan, pBufferMax, pBufferMin, pBufferSat);
	}
	int csHWDevice::AcquisitionCscan_0x00010X02(structAcqInfoEx &acqInfo_,const CStream_0x0001 *pStreamHeader,const CSubStreamCscan_0x0X02 *pCscanHeader,const structCscanAmp_0x0102 *pBufferAmp, const structCscanAmpTof_0x0202 *pBufferAmpTof)
	{
		csAcqInfoEx^ acqInfo;
		csHeaderStream_0x0001^ headerStream;
		csSubStreamCscan_0x0X02^ cscanHeader;
		cli::array<csCscanAmp_0x0102^>^ bufferAmp=nullptr;
		cli::array<csCscanAmpTof_0x0202^>^ bufferAmpTof=nullptr;
		constVoid p;
		structCscanAmp_0x0102 *pAmp;
		structCscanAmpTof_0x0202 *pAmpTof;

		if(m_csCscan==nullptr)
			return 1;
		p.pcVoid = &acqInfo_;
		acqInfo = safe_cast<csAcqInfoEx^>(Marshal::PtrToStructure((IntPtr)&acqInfo_,csAcqInfoEx::typeid));
		p.pcVoid = pStreamHeader;
		headerStream = safe_cast<csHeaderStream_0x0001^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csHeaderStream_0x0001::typeid));
		p.pcVoid = pCscanHeader;
		cscanHeader = safe_cast<csSubStreamCscan_0x0X02^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csSubStreamCscan_0x0X02::typeid));
		if(p.pcVoid=pBufferAmp)//and not ==
		{
			p.pcVoid = pBufferAmp;
			pAmp = (structCscanAmp_0x0102*)p.pVoid;
			bufferAmp = gcnew cli::array<csCscanAmp_0x0102^>(pCscanHeader->count);
			for(int i=0;i<(int)pCscanHeader->count;i++)
				bufferAmp[i] = safe_cast<csCscanAmp_0x0102^>(Marshal::PtrToStructure((IntPtr)&pAmp[i],csCscanAmp_0x0102::typeid));
		}
		if(p.pcVoid=pBufferAmpTof)//and not ==
		{
			p.pcVoid = pBufferAmpTof;
			pAmpTof = (structCscanAmpTof_0x0202*)p.pVoid;
			bufferAmpTof = gcnew cli::array<csCscanAmpTof_0x0202^>(pCscanHeader->count);
			for(int i=0;i<(int)pCscanHeader->count;i++)
				bufferAmpTof[i] = safe_cast<csCscanAmpTof_0x0202^>(Marshal::PtrToStructure((IntPtr)&pAmpTof[i],csCscanAmpTof_0x0202::typeid));
		}
		return m_csCscan->Invoke(m_csAcquisitionParameter,acqInfo,headerStream,cscanHeader,bufferAmp,bufferAmpTof);
	}
	int csDriverOEMPA::csHWDevice::AcquisitionCscan_0x00020402(structAcqInfoEx& acqInfo_, const CStream_0x0002* pStreamHeader, const CSubStreamCscan_0x0402* pCscanHeader, const structCscanAmpTof_0x0402* pBufferAmpTof)
	{
		csAcqInfoEx^ acqInfo;
		csHeaderStream_0x0002^ headerStream;
		csSubStreamCscan_0x0402^ cscanHeader;
		cli::array<csCscanAmpTof_0x0402^>^ bufferAmpTof = nullptr;
		constVoid p;
		structCscanAmpTof_0x0402* pAmpTof;

		if (m_csCscan4 == nullptr)
			return 1;
		p.pcVoid = &acqInfo_;
		acqInfo = safe_cast<csAcqInfoEx^>(Marshal::PtrToStructure((IntPtr)&acqInfo_, csAcqInfoEx::typeid));
		p.pcVoid = pStreamHeader;
		headerStream = safe_cast<csHeaderStream_0x0002^>(Marshal::PtrToStructure((IntPtr)p.pVoid, csHeaderStream_0x0002::typeid));
		p.pcVoid = pCscanHeader;
		cscanHeader = safe_cast<csSubStreamCscan_0x0402^>(Marshal::PtrToStructure((IntPtr)p.pVoid, csSubStreamCscan_0x0402::typeid));
		if (p.pcVoid = pBufferAmpTof)//and not ==
		{
			p.pcVoid = pBufferAmpTof;
			pAmpTof = (structCscanAmpTof_0x0402*)p.pVoid;
			bufferAmpTof = gcnew cli::array<csCscanAmpTof_0x0402^>(pCscanHeader->m_Cscan_0x0X02.count);
			for (int i = 0; i < (int)pCscanHeader->m_Cscan_0x0X02.count; i++)
				bufferAmpTof[i] = safe_cast<csCscanAmpTof_0x0402^>(Marshal::PtrToStructure((IntPtr)&pAmpTof[i], csCscanAmpTof_0x0402::typeid));
		}
		return m_csCscan4->Invoke(m_csAcquisitionParameter, acqInfo, headerStream, cscanHeader, bufferAmpTof);
	}
	int csHWDevice::AcquisitionIO_0x00010101(const CStream_0x0001 *pStreamHeader,const CSubStreamIO_0x0101 *pIOHeader)
	{
		csHeaderStream_0x0001^ headerStream;
		csHeaderIO_0x0001^ ioHeader;
		constVoid p;

		if(m_csIo0==nullptr)
			return 1;
		p.pcVoid = pStreamHeader;
		headerStream = safe_cast<csHeaderStream_0x0001^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csHeaderStream_0x0001::typeid));
		p.pcVoid = pIOHeader;
		ioHeader = safe_cast<csHeaderIO_0x0001^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csHeaderIO_0x0001::typeid));
		return m_csIo0->Invoke(m_csAcquisitionParameter,headerStream,ioHeader);
	}
	int csHWDevice::AcquisitionIO_1x00010101(structAcqInfoEx &acqInfo_,const CStream_0x0001 *pStreamHeader,const CSubStreamIO_0x0101 *pIOHeader)
	{
		csAcqInfoEx^ acqInfo;
		csHeaderStream_0x0001^ headerStream;
		csHeaderIO_0x0001^ ioHeader;
		constVoid p;

		if(m_csIo1==nullptr)
			return 1;
		p.pcVoid = &acqInfo_;
		acqInfo = safe_cast<csAcqInfoEx^>(Marshal::PtrToStructure((IntPtr)&acqInfo_,csAcqInfoEx::typeid));
		p.pcVoid = pStreamHeader;
		headerStream = safe_cast<csHeaderStream_0x0001^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csHeaderStream_0x0001::typeid));
		p.pcVoid = pIOHeader;
		ioHeader = safe_cast<csHeaderIO_0x0001^>(Marshal::PtrToStructure((IntPtr)p.pVoid,csHeaderIO_0x0001::typeid));
		return m_csIo1->Invoke(m_csAcquisitionParameter,acqInfo,headerStream,ioHeader);
	}
	int csHWDevice::AcquisitionInfo(const wchar_t *pInfo)
	{
		String^ pValue;
		constVoid p;

		if(m_csInfo==nullptr)
			return 1;
		p.pcVoid = pInfo;
		pValue = Marshal::PtrToStringUni((IntPtr)p.pVoid);
		return m_csInfo->Invoke(m_csAcquisitionParameter,pValue);
	}
	void csHWDevice::CallbackCustomizedAPI(const wchar_t *pFileName,enumStepCustomizedAPI eStepCustomizedAPI,structRoot *pRoot,structCycle *pCycle,CFocalLaw *pEmission,CFocalLaw *pReception)
	{
		if(m_csCustomizedOEMPA==nullptr)
			return;
		m_csCustomizedOEMPA->CallbackCustomizedAPI(pFileName,eStepCustomizedAPI,pRoot,pCycle,pEmission,pReception);
	}
	void* csHWDevice::ListAddObject(Object^ object)
	{
		if(m_List==nullptr)
			return NULL;
		return m_List->Add(object);
	}
	void csHWDevice::test()
	{
		structAcqInfoEx acqInfo;
		CStream_0x0001 StreamHeader;
		CSubStreamAscan_0x0103 AscanHeader;
		CSubStreamCscan_0x0X02 cscanHeader;
		structCscanAmp_0x0102 bufferAmp[4];
		structCscanAmpTof_0x0202 bufferAmpTof[4];
		CSubStreamIO_0x0101 ioHeader;

		init(acqInfo);
		init(StreamHeader);
		init(AscanHeader);
		init(cscanHeader);
		cscanHeader.count = 4;
		for(int i=0;i<4;i++)
		{
			init(bufferAmp[i]);
			init(bufferAmpTof[i]);
		}
		init(ioHeader);
		gAcquisitionAscan_0x00010103(m_pointer,acqInfo,&StreamHeader,&AscanHeader,NULL,NULL,NULL);
		gAcquisitionInfo(m_pointer,L"Hello world!");
		gAcquisitionIO_0x00010101(m_pointer,&StreamHeader,&ioHeader);
		gAcquisitionCscan_0x00010X02(m_pointer,acqInfo,&StreamHeader,&cscanHeader,bufferAmp,bufferAmpTof);
	}
#pragma endregion csHWDevice
////////////////////////////////////////////////////////
	void csKernelDriver::GetVersion([Out] String^ %pMsg)
	{
		wchar_t pAux[MAX_PATH/4];
		wcscpy_s(pAux,MAX_PATH/4,OEMPA_GetVersion());
		pMsg = Marshal::PtrToStringUni((IntPtr)pAux);
	}
	char csKernelDriver::GetVersionLetter()
	{
		return KIT_VERSION_LETTER;
	}
	bool csKernelDriver::CrtCheckMemory()
	{
#ifdef _DEBUG
		if(::_CrtCheckMemory())
			return true;
		else
			return false;
#else //_DEBUG
		return true;
#endif //_DEBUG

	}
	bool csKernelDriver::CrtSetDbgFlag(bool bEnable)
	{
#ifdef _DEBUG
		if (bEnable)
			::_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF & _CRTDBG_CHECK_ALWAYS_DF);
		else
			::_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF);
		return true;
#else //_DEBUG
		return false;
#endif //_DEBUG

	}
	//void csKernelDriver::debug_EnableHeapEx(bool bEnable, String ^pFileName)
	//{
	//	wchar_t* y;
	//	char cData[MAX_PATH];
	//	DWORD dwConvert;

	//	y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pFileName);
	//	dwConvert = (DWORD)WideCharToMultiByte(CP_ACP, 0, y, -1, cData, MAX_PATH, NULL, NULL);
	//	Marshal::FreeHGlobal((IntPtr)y);
	//	if(dwConvert)
	//		::debug_EnableHeapEx(bEnable, cData);
	//}
	//bool csKernelDriver::debug_DumpHeap(String ^pFileName,bool bStatistics)
	//{
	//	wchar_t* y;
	//	char cData[MAX_PATH];
	//	DWORD dwConvert;

	//	y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pFileName);
	//	dwConvert = (DWORD)WideCharToMultiByte(CP_ACP, 0, y, -1, cData, MAX_PATH, NULL, NULL);
	//	Marshal::FreeHGlobal((IntPtr)y);
	//	if(!dwConvert)
	//		return false;
	//	return ::debug_DumpHeap(cData, bStatistics);
	//}
	static csMsgBox::csMsgBox()
	{
		g_CallbackSystemMessageBox = nullptr;
		g_CallbackSystemMessageBoxList = nullptr;
		g_CallbackSystemMessageBoxButtons = nullptr;
		g_CallbackOempaApiMessageBox = nullptr;
	}

	void csMsgBox::SetCallbackSystemMessageBox(TypeCallbackSystemMessageBox ^pProcess)
	{
		g_CallbackSystemMessageBox = pProcess;
		::SetCallbackSystemMessageBox(gCallbackSystemMessageBox);
	}
	void csMsgBox::SetCallbackSystemMessageBoxList(TypeCallbackSystemMessageBoxList ^pProcess)
	{
		g_CallbackSystemMessageBoxList = pProcess;
		::SetCallbackSystemMessageBoxList(gCallbackSystemMessageBoxList);
	}
	void csMsgBox::SetCallbackSystemMessageBoxButtons(TypeCallbackSystemMessageBoxButtons ^pProcess)
	{
		g_CallbackSystemMessageBoxButtons = pProcess;
		::SetCallbackSystemMessageBoxButtons(gCallbackSystemMessageBoxButtons);
	}
	void csMsgBox::SetCallbackOempaApiMessageBox(TypeCallbackOempaApiMessageBox ^pProcess)
	{
		g_CallbackOempaApiMessageBox = pProcess;
		::SetCallbackMessageBox(gCallbackOempaApiMessageBox);
	}

	TypeCallbackSystemMessageBox ^csMsgBox::GetCallbackSystemMessageBox()
	{
		return g_CallbackSystemMessageBox;
	}
	TypeCallbackSystemMessageBoxList ^csMsgBox::GetCallbackSystemMessageBoxList()
	{
		return g_CallbackSystemMessageBoxList;
	}
	TypeCallbackSystemMessageBoxButtons ^csMsgBox::GetCallbackSystemMessageBoxButtons()
	{
		return g_CallbackSystemMessageBoxButtons;
	}
	TypeCallbackOempaApiMessageBox ^csMsgBox::GetCallbackOempaApiMessageBox()
	{
		return g_CallbackOempaApiMessageBox;
	}

	void csMsgBox::CallbackSystemMessageBox(const wchar_t *pMsg)
	{
		String^ pValue;
		constVoid p;

		if(g_CallbackSystemMessageBox==nullptr)
			return;
		p.pcVoid = pMsg;
		pValue = Marshal::PtrToStringUni((IntPtr)p.pVoid);
		g_CallbackSystemMessageBox->Invoke(pValue);
	}
	void csMsgBox::CallbackSystemMessageBoxList(const wchar_t *pMsg)
	{
		String^ pValue;
		constVoid p;

		if(g_CallbackSystemMessageBoxList==nullptr)
			return;
		p.pcVoid = pMsg;
		pValue = Marshal::PtrToStringUni((IntPtr)p.pVoid);
		g_CallbackSystemMessageBoxList->Invoke(pValue);
	}
	unsigned int csMsgBox::CallbackSystemMessageBoxButtons(const wchar_t *pMsg,const wchar_t *pTitle,UINT nType)
	{
		String^ pValue1;
		String^ pValue2;
		constVoid p1,p2;

		if(g_CallbackSystemMessageBoxButtons==nullptr)
			return 0;
		p1.pcVoid = pMsg;
		pValue1 = Marshal::PtrToStringUni((IntPtr)p1.pVoid);
		p2.pcVoid = pTitle;
		pValue2 = Marshal::PtrToStringUni((IntPtr)p2.pVoid);
		return (unsigned int)g_CallbackSystemMessageBoxButtons->Invoke(pValue1,pValue2,(csEnumMsgBoxButtons)nType);
	}
	int csMsgBox::CallbackOempaApiMessageBox(HWND hWnd,const wchar_t* lpszText,const wchar_t* lpszCaption,UINT nType)
	{
		String^ pValue1;
		String^ pValue2;
		constVoid p1,p2;

		if(g_CallbackOempaApiMessageBox==nullptr)
			return 0;
		p1.pcVoid = lpszText;
		pValue1 = Marshal::PtrToStringUni((IntPtr)p1.pVoid);
		p2.pcVoid = lpszCaption;
		pValue2 = Marshal::PtrToStringUni((IntPtr)p2.pVoid);
		return (int)g_CallbackOempaApiMessageBox->Invoke(/*hWnd,*/pValue1,pValue2,(csEnumMsgBoxButtons)nType);
	}

	void csMsgBox::SystemMessageBox(String ^pMsg)
	{
		wchar_t* y;

		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pMsg);
		UTKernel_SystemMessageBox(y);
		Marshal::FreeHGlobal((IntPtr)y);
	}
	void csMsgBox::SystemMessageBoxList(String ^pMsg)
	{
		wchar_t* y;

		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pMsg);
		UTKernel_SystemMessageBoxList(y);
		Marshal::FreeHGlobal((IntPtr)y);
	}
	csEnumMsgBoxReturn csMsgBox::SystemMessageBoxButtons(String ^pMsg,String ^pTitle,csEnumMsgBoxButtons nType)
	{
		wchar_t *y,*z;
		UINT ret;

		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pMsg);
		z = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pTitle);
		ret = UTKernel_SystemMessageBox(y,z,(UINT)nType);
		Marshal::FreeHGlobal((IntPtr)y);
		Marshal::FreeHGlobal((IntPtr)z);
		return (csEnumMsgBoxReturn)ret;
	}
	csEnumMsgBoxReturn csMsgBox::OempaApiMessageBox(String ^lpszText,String ^lpszCaption,csEnumMsgBoxButtons nType)
	{
		wchar_t *y,*z;
		int ret;

		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(lpszText);
		z = (wchar_t*)(void*)Marshal::StringToHGlobalUni(lpszCaption);
		ret = ::OempaApiMessageBox(NULL,y,z,(UINT)nType);
		Marshal::FreeHGlobal((IntPtr)y);
		Marshal::FreeHGlobal((IntPtr)z);
		return (csEnumMsgBoxReturn)ret;
	}
	bool csMsgBox::IsUserInterfaceThread()
	{
		return UTDriver_IsUserInterfaceThread();//this function could be called to know if the current thread is attached to the management of window.
	}
////////////////////////////////////////////////////////
#pragma region csCustomizedOEMPA
	csCustomizedOEMPA::csCustomizedOEMPA(CHWDeviceOEMPA *pHWDeviceOEMPA)
	{
		Constructor(pHWDeviceOEMPA);
	}
	csCustomizedOEMPA::csCustomizedOEMPA(System::Void *pHWDeviceOEMPA)
	{
		Constructor((CHWDeviceOEMPA*)pHWDeviceOEMPA);
	}
	csCustomizedOEMPA::~csCustomizedOEMPA()
	{
		this->!csCustomizedOEMPA();
	}
	csCustomizedOEMPA::!csCustomizedOEMPA()
	{
		Free();
	}
	void csCustomizedOEMPA::Free()
	{
		m_pHWDeviceOEMPA = NULL;
	}
	void csCustomizedOEMPA::Constructor(CHWDeviceOEMPA *pHWDeviceOEMPA)
	{
		m_pHWDeviceOEMPA = pHWDeviceOEMPA;
		m_csCallback = nullptr;
		m_csAcquisitionParameter = nullptr;
		m_pRoot = NULL;
		m_pCycle = NULL;
		m_pEmission = NULL;
		m_pReception = NULL;
		m_iCycleCount = -2;
	}

	bool csCustomizedOEMPA::SetAcquisitionParameter(Object ^pParameter)//first parameter of the callback acquisition function.
	{
		m_csAcquisitionParameter = pParameter;
		if(!m_pHWDeviceOEMPA)
			return false;
		return true;
	}
	Object ^csCustomizedOEMPA::GetAcquisitionParameter()
	{
		return m_csAcquisitionParameter;
	}
	bool csCustomizedOEMPA::SetCallbackCustomizedDriverAPI(TypeCallbackCustomizedDriverAPI ^pProcess)
	{
		m_csCallback = pProcess;
		if(!m_pHWDeviceOEMPA)
			return false;
		return OEMPA_SetCallbackCustomizedDriverAPI(gCallbackCustomizedOEM);
	}
	TypeCallbackCustomizedDriverAPI ^csCustomizedOEMPA::GetCallbackCustomizedDriverAPI()
	{
		return m_csCallback;
	}
	void csCustomizedOEMPA::CallbackCustomizedAPI(const wchar_t *pFileName,enumStepCustomizedAPI eStepCustomizedAPI,structRoot *pRoot,structCycle *pCycle,CFocalLaw *pEmission,CFocalLaw *pReception)
	{
		String^ pValue;
		constVoid p;

		if(m_csCallback==nullptr)
			return;
		p.pcVoid = pFileName;
		pValue = Marshal::PtrToStringUni((IntPtr)p.pVoid);
		m_iCycleCount = pRoot->iCycleCount;
		m_pRoot = pRoot;
		m_pCycle = pCycle;
		m_pEmission = pEmission;
		m_pReception = pReception;
		m_csCallback->Invoke(m_csAcquisitionParameter,(csEnumStepCustomizedAPI)eStepCustomizedAPI,pValue,m_iCycleCount);
		m_pRoot = NULL;
		m_pCycle = NULL;
		m_pEmission = NULL;
		m_pReception = NULL;
		m_iCycleCount = -2;
	}

	bool csCustomizedOEMPA::GetRoot([Out] csRoot^ %root)
	{
		if(!m_pRoot)
			return false;
		if(root==nullptr)
			root = gcnew csRoot;
		if(root==nullptr)
			return false;
		//root = safe_cast<csRoot^>(Marshal::PtrToStructure((IntPtr)m_pRoot,csRoot::typeid));
		return root->CopyFrom(m_pRoot);
	}
	bool csCustomizedOEMPA::SetRoot([In] csRoot^ %root)
	{
		if(!m_pRoot)
			return false;
		return root->CopyTo(m_pRoot);
	}
	bool csCustomizedOEMPA::GetCycle(int iCycle,[Out] csCycle^ %cycle)
	{
		if(!m_pCycle)
			return false;
		if(iCycle<0)
			return false;
		if(iCycle>=m_iCycleCount)
			return false;
		if(cycle==nullptr)
			cycle = gcnew csCycle;
		if(cycle==nullptr)
			return false;
		return cycle->CopyFrom(&m_pCycle[iCycle]);
	}
	bool csCustomizedOEMPA::SetCycle(int iCycle,[In] csCycle^ cycle)
	{
		if(cycle==nullptr)
			return false;
		if(!m_pCycle)
			return false;
		if(iCycle<0)
			return false;
		if(iCycle>=m_iCycleCount)
			return false;
		return cycle->CopyTo(&m_pCycle[iCycle]);
	}
	bool csCustomizedOEMPA::GetFocalLaw(bool bEmission,int iCycle,[Out] csFocalLaw^ %focalLaw)
	{
		CFocalLaw *pFocalLaw;

		if(bEmission && !m_pEmission)
			return false;
		if(!bEmission && !m_pReception)
			return false;
		if(iCycle<0)
			return false;
		if(iCycle>=m_iCycleCount)
			return false;
		if(focalLaw==nullptr)
			focalLaw = gcnew csFocalLaw;
		if(focalLaw==nullptr)
			return false;
		//1.1.5.3k
		//if(bEmission)
		//	focalLaw = safe_cast<csFocalLaw^>(Marshal::PtrToStructure((IntPtr)&m_pEmission[iCycle],csFocalLaw::typeid));
		//else
		//	focalLaw = safe_cast<csFocalLaw^>(Marshal::PtrToStructure((IntPtr)&m_pReception[iCycle],csFocalLaw::typeid));
		if(bEmission)
			pFocalLaw = &m_pEmission[iCycle];
		else
			pFocalLaw = &m_pReception[iCycle];
		return focalLaw->CopyFrom(pFocalLaw);
	}
	bool csCustomizedOEMPA::SetFocalLaw(bool bEmission,int iCycle,[In] csFocalLaw^ focalLaw)
	{
		CFocalLaw *pFocalLaw;

		if(focalLaw==nullptr)
			return false;
		if(focalLaw->iFocalCount>focalLaw->afDelay->GetLength(0))
			return false;
		if(focalLaw->iElementCount>focalLaw->afDelay->GetLength(1))
			return false;
		if(iCycle<0)
			return false;
		if(iCycle>=m_iCycleCount)
			return false;
		if(bEmission)
		{
			if(!m_pEmission)
				return false;
			pFocalLaw = &m_pEmission[iCycle];
		}else{
			if(!m_pReception)
				return false;
			pFocalLaw = &m_pReception[iCycle];
		}
		return focalLaw->CopyTo(pFocalLaw);
	}
	bool csCustomizedOEMPA::ReadFileWriteHW(String ^pValue)
	{
		wchar_t* y;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pValue);
		bRet = OEMPA_ReadFileWriteHW(m_pHWDeviceOEMPA,y);
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}
	bool csCustomizedOEMPA::ReadHWWriteFile(String ^pValue)
	{
		wchar_t* y;
		bool bRet;
		int dacMax = g_iOEMPADACCountMax;

		if(!m_pHWDeviceOEMPA)
			return false;
		if (m_pHWDeviceOEMPA->GetSWDevice()->GetHardware() == eOEMMC2 || m_pHWDeviceOEMPA->GetSWDevice()->GetHardware() == eOEMMCuF)
			dacMax = g_iOEMMCDACCountMax;
		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pValue);
		bRet = OEMPA_ReadHWWriteFile(m_pHWDeviceOEMPA,g_iOEMPACycleCountMax,g_iOEMPAApertureElementCountMax,g_iOEMPAFocalCountMax,dacMax,y);
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}
	bool csCustomizedOEMPA::WriteHW(csHWDeviceOEMPA^ %pOEMPA,csRoot^ %root,cli::array<csCycle^>^ %cycle,cli::array<csFocalLaw^>^ %emission,cli::array<csFocalLaw^>^ %reception,csEnumAcquisitionState acqState)
	{
		CHWDeviceOEMPA *pHWDeviceOEMPA;
		structRoot Root;
		csCycle^ cycle2;
		csFocalLaw ^emission2,^reception2;
		bool bRet;

		pHWDeviceOEMPA = (CHWDeviceOEMPA*)pOEMPA->cGetHWDeviceOEMPA();
		if(pOEMPA==nullptr)
			return false;
		if(root==nullptr)
			return false;
		m_pRoot = &Root;
		if(!SetRoot(root))
			{bRet = false;goto end;}
		m_iCycleCount = Root.iCycleCount;
		if(m_iCycleCount>0)
		{
			m_pCycle = OEMPA_AllocCycle(Root, m_iCycleCount);
			m_pEmission = new CFocalLaw[m_iCycleCount];
			m_pReception = new CFocalLaw[m_iCycleCount];
			if(!m_pCycle || !m_pEmission || !m_pReception)
				{bRet = false;goto end;}
			OEMPA_ResetArrayFocalLaw(m_iCycleCount,m_pEmission);
			OEMPA_ResetArrayFocalLaw(m_iCycleCount,m_pReception);
			for(int iCycle=0;iCycle<m_iCycleCount;iCycle++)
			{
				cycle2 = cycle[iCycle];
				emission2 = emission[iCycle];
				reception2 = reception[iCycle];
				m_pEmission[iCycle].SetAllocatedSize(emission2->iFocalCount, emission2->iElementCount);
				m_pReception[iCycle].SetAllocatedSize(reception2->iFocalCount, reception2->iElementCount);
				if(!SetCycle(iCycle,cycle2))
					{bRet = false;goto end;}
				if(!SetFocalLaw(true,iCycle,emission2))
					{bRet = false;goto end;}
				if(!SetFocalLaw(false,iCycle,reception2))
					{bRet = false;goto end;}
			}
		}

		OEMPA_InitRoot(*m_pRoot,pHWDeviceOEMPA);
		bRet = OEMPA_WriteHW(pHWDeviceOEMPA,*m_pRoot,m_pCycle,m_pEmission,m_pReception,(enumAcquisitionState)acqState);
	end:
		m_pRoot = NULL;
		if(m_pCycle)
			delete [] m_pCycle;
		m_pCycle = NULL;
		if(m_pEmission)
			delete [] m_pEmission;
		m_pEmission = NULL;
		if(m_pReception)
			delete [] m_pReception;
		m_pReception = NULL;
		m_iCycleCount = -2;
		return bRet;
	}
#pragma endregion csCustomizedOEMPA
////////////////////////////////////////////////////////
#pragma region csSWFilterOEMPA
	csSWFilterOEMPA::csSWFilterOEMPA()
	{
		Free();
	}
	csSWFilterOEMPA::~csSWFilterOEMPA()
	{
		this->!csSWFilterOEMPA();
	}
	csSWFilterOEMPA::!csSWFilterOEMPA()
	{
		Free();
	}
	void csSWFilterOEMPA::Constructor(CHWDeviceOEMPA *pHWDeviceOEMPA,CSWDeviceOEMPA *pSWDeviceOEM,CSWFilterOEMPA *pSWFilterOEM)
	{
		m_pHWDeviceOEMPA = pHWDeviceOEMPA;
		m_pSWDeviceOEMPA = pSWDeviceOEM;
		m_pSWFilterOEM = pSWFilterOEM;
	}
	void csSWFilterOEMPA::Free()
	{
		m_pHWDeviceOEMPA = NULL;
		m_pSWDeviceOEMPA = NULL;
		m_pSWFilterOEM = NULL;
	}

	bool csSWFilterOEMPA::SetFilter(csEnumOEMPAFilter eFilter)
	{
		if(!m_pSWFilterOEM)
			return false;
		return m_pSWFilterOEM->SetFilter((enumOEMPAFilter)eFilter);
	}
	bool csSWFilterOEMPA::GetFilter([Out] csEnumOEMPAFilter %eFilter)
	{
		enumOEMPAFilter eFilter2;
		bool bRet;

		if(!m_pSWFilterOEM)
			return false;
		bRet = m_pSWFilterOEM->GetFilter(eFilter2);
		eFilter = (csEnumOEMPAFilter)eFilter2;
		return bRet;
	}
	bool csSWFilterOEMPA::SetTitle(String^ pValue)//useful for custom filter.
	{
		wchar_t* y;
		bool bRet;

		if(!m_pSWFilterOEM)
			return false;
		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pValue);
		bRet = m_pSWFilterOEM->SetTitle(y);
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}
	bool csSWFilterOEMPA::GetTitle([Out] String^ %pValue)
	{
		wchar_t pAux[MAX_PATH];

		if(!m_pSWFilterOEM)
			return false;
		if(!m_pSWFilterOEM->GetTitle(pAux,MAX_PATH))
			return false;
		pValue = Marshal::PtrToStringUni((IntPtr)pAux);
		return true;
	}

	//custom filter coefficient: functions used by the "CustomFilter" software.
	bool csSWFilterOEMPA::SetScale(WORD wScale)
	{
		if(!m_pSWFilterOEM)
			return false;
		return m_pSWFilterOEM->SetScale(wScale);
	}
	bool csSWFilterOEMPA::GetScale([Out] WORD %wScale)
	{
		WORD wScale2;
		bool bRet;

		if(!m_pSWFilterOEM)
			return false;
		bRet = m_pSWFilterOEM->GetScale(wScale2);
		wScale = wScale2;
		return bRet;
	}
	bool csSWFilterOEMPA::SetCoefficientCount(int iCoefficientCount)
	{
		if(!m_pSWFilterOEM)
			return false;
		return m_pSWFilterOEM->SetCoefficientCount(iCoefficientCount);
	}
	bool csSWFilterOEMPA::GetCoefficientCount([Out] int %iCoefficientCount)
	{
		int iCoefficientCount2;
		bool bRet;

		if(!m_pSWFilterOEM)
			return false;
		bRet = m_pSWFilterOEM->GetCoefficientCount(iCoefficientCount2);
		iCoefficientCount = iCoefficientCount2;
		return bRet;
	}
	bool csSWFilterOEMPA::SetCoefficient(int iCoefficientIndex,short wValue)
	{
		if(!m_pSWFilterOEM)
			return false;
		return m_pSWFilterOEM->SetCoefficient(iCoefficientIndex,wValue);
	}
	bool csSWFilterOEMPA::GetCoefficient(int iCoefficientIndex,[Out] short %wValue)
	{
		short wValue2;
		bool bRet;

		if(!m_pSWFilterOEM)
			return false;
		bRet = m_pSWFilterOEM->GetCoefficient(iCoefficientIndex,wValue2);
		wValue = wValue2;
		return bRet;
	}
	bool csSWFilterOEMPA::SetFilter(WORD wScale,cli::array<short>^ wValue,bool bUpdateHardware)//if you want to update all hardware filter, it is quicker to call "CSWDeviceOEMPA::UpdateAllFilter" at the end and before to call "SetFilter" with "bUpdateHardware=false" for all filters.
	{
		short wValue2[g_iOEMPAFilterCoefficientMax];

		if(!m_pSWFilterOEM)
			return false;
		if(wValue->GetLength(0)!=g_iOEMPAFilterCoefficientMax)
			return false;
		for(int iIndex=0;iIndex<g_iOEMPAFilterCoefficientMax;iIndex++)
			wValue2[iIndex] = wValue[iIndex];
		return m_pSWFilterOEM->SetFilter(wScale,wValue2,bUpdateHardware);
	}
	bool csSWFilterOEMPA::GetFilter([Out] WORD %wScale,cli::array<short>^ %wValue)
	{
		WORD wScale2;
		short wValue2[g_iOEMPAFilterCoefficientMax];
		bool bRet;

		if(!m_pSWFilterOEM)
			return false;
		bRet = m_pSWFilterOEM->GetFilter(wScale2,wValue2);
		wScale = wScale2;
		wValue = gcnew cli::array<short>(g_iOEMPAFilterCoefficientMax);
		for(int iIndex=0;iIndex<g_iOEMPAFilterCoefficientMax;iIndex++)
			wValue[iIndex] = wValue2[iIndex];
		return bRet;
	}

#pragma endregion csSWFilterOEMPA
////////////////////////////////////////////////////////
#pragma region csSWDeviceOEMPA
	csSWDeviceOEMPA::csSWDeviceOEMPA()
	{
		Free();
	}
	csSWDeviceOEMPA::~csSWDeviceOEMPA()
	{
		this->!csSWDeviceOEMPA();
	}
	csSWDeviceOEMPA::!csSWDeviceOEMPA()
	{
		Free();
	}
	void csSWDeviceOEMPA::Constructor(CHWDeviceOEMPA *pHWDeviceOEMPA,CSWDeviceOEMPA *pSWDeviceOEM)
	{
		CSWFilterOEMPA *pSWFilterOEM;

		//cli::array<int>^ strarray = gcnew cli::array<int>(x);
		m_acsSWFilterOEM = gcnew cli::array<csSWFilterOEMPA^>(eOEMPAFilter15+1);
		m_pHWDeviceOEMPA = pHWDeviceOEMPA;
		m_pSWDeviceOEMPA = pSWDeviceOEM;
		for(int iIndex=0;iIndex<eOEMPAFilter15;iIndex++)
		{
			pSWFilterOEM = &pSWDeviceOEM->Filter(iIndex);
			m_acsSWFilterOEM[iIndex] = gcnew csSWFilterOEMPA();
			m_acsSWFilterOEM[iIndex]->Constructor(pHWDeviceOEMPA,pSWDeviceOEM,pSWFilterOEM);
		}
	}
	void csSWDeviceOEMPA::Free()
	{
		if(m_acsSWFilterOEM!=nullptr)
		for(int iIndex=0;iIndex<eOEMPAFilter15;iIndex++)
		{
			m_acsSWFilterOEM[iIndex] = nullptr;
		}
		m_acsSWFilterOEM = nullptr;
		m_pHWDeviceOEMPA = NULL;
		m_pSWDeviceOEMPA = NULL;
	}
	csSWFilterOEMPA^ csSWDeviceOEMPA::Filter(int iFilterIndex)
	{
		if(iFilterIndex<0)
			return nullptr;
		if(iFilterIndex>=eOEMPAFilter15)
			return nullptr;
		return m_acsSWFilterOEM[iFilterIndex];
	}

	bool csSWDeviceOEMPA::IsPulserEnabled()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsPulserEnabled();
	}

	bool csSWDeviceOEMPA::SetAddress(String ^pValue)
	{
		wchar_t* y;
		bool bRet;

		if(!m_pSWDeviceOEMPA)
			return false;
		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pValue);
		bRet = m_pSWDeviceOEMPA->SetAddress(y);
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}
	bool csSWDeviceOEMPA::GetAddress([Out] String^ %pValue)
	{
		wchar_t pAux[MAX_PATH];

		if(!m_pSWDeviceOEMPA)
			return false;
		wcscpy_s(pAux,MAX_PATH,m_pSWDeviceOEMPA->GetAddress());
		pValue = Marshal::PtrToStringUni((IntPtr)pAux);
		return true;
	}

	bool csSWDeviceOEMPA::IsUSB3Connected()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsUSB3Connected();
	}

	bool csSWDeviceOEMPA::GetSerialNumber([Out] String^ %pSN)
	{
		wchar_t pAux[MAX_PATH];

		if(!m_pSWDeviceOEMPA)
			return false;
		if(!m_pSWDeviceOEMPA->GetSerialNumber(pAux,MAX_PATH))
			return false;
		pSN = Marshal::PtrToStringUni((IntPtr)pAux);
		return true;
	}

	bool csSWDeviceOEMPA::GetSystemType([Out] String^ %pType)
	{
		wchar_t pAux[MAX_PATH];

		if(!m_pSWDeviceOEMPA)
			return false;
		if(!m_pSWDeviceOEMPA->GetSystemType(pAux,MAX_PATH))
			return false;
		pType = Marshal::PtrToStringUni((IntPtr)pAux);
		return true;
	}

	int csSWDeviceOEMPA::GetRXBoardCount()
	{
		if(!m_pSWDeviceOEMPA)
			return 0;
		return m_pSWDeviceOEMPA->GetRXBoardCount();
	}
	int csSWDeviceOEMPA::GetApertureCountMax()
	{
		if(!m_pSWDeviceOEMPA)
			return 0;
		return m_pSWDeviceOEMPA->GetApertureCountMax();
	}
	int csSWDeviceOEMPA::GetElementCountMax()
	{
		if(!m_pSWDeviceOEMPA)
			return 0;
		return m_pSWDeviceOEMPA->GetElementCountMax();
	}

	const double csSWDeviceOEMPA::dGetClockPeriod()//ns
	{
		if(!m_pSWDeviceOEMPA)
			return 10.0;
		return m_pSWDeviceOEMPA->dGetClockPeriod();
	}
	const float csSWDeviceOEMPA::fGetClockPeriod()//ns
	{
		if(!m_pSWDeviceOEMPA)
			return 10.0f;
		return m_pSWDeviceOEMPA->fGetClockPeriod();
	}
	const long csSWDeviceOEMPA::lGetClockFrequency()//Hz
	{
		if(!m_pSWDeviceOEMPA)
			return 0;
		return m_pSWDeviceOEMPA->lGetClockFrequency();
	}

	WORD csSWDeviceOEMPA::GetFirmwareId()
	{
		if(!m_pSWDeviceOEMPA)
			return 0;
		return m_pSWDeviceOEMPA->GetFirmwareId();
	}

	bool csSWDeviceOEMPA::IsFullMatrixCapture()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsFullMatrixCapture();
	}
	bool csSWDeviceOEMPA::IsFullMatrixCaptureReadWrite()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsFullMatrixCaptureReadWrite();
	}
	bool csSWDeviceOEMPA::GetFMCElement([Out] int %iElementStart, [Out] int %iElementStop, [Out] int %iElementStep)
	{
		int iElementStart2,iElementStop2,iElementStep2;

		if(!m_pSWDeviceOEMPA)
			return false;
		if(!m_pSWDeviceOEMPA->GetFMCElement(iElementStart2,iElementStop2,iElementStep2))
			return false;
		iElementStart = iElementStart2;
		iElementStop = iElementStop2;
		iElementStep = iElementStep2;
		return true;
	}
	bool csSWDeviceOEMPA::IsMultiHWChannelSupported()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsMultiHWChannelSupported();
	}
	bool csSWDeviceOEMPA::IsTemperatureAlarmSupported()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsTemperatureAlarmSupported();
	}
	bool csSWDeviceOEMPA::IsMultiHWChannelEnabled()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsMultiHWChannelEnabled();
	}
	bool csSWDeviceOEMPA::IsMatrixAvailable()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsMatrixAvailable();
	}
	bool csSWDeviceOEMPA::IsLabviewAvailable()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsLabviewAvailable();
	}
	bool csSWDeviceOEMPA::IsTpacquisitionAvailable()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsTpacquisitionAvailable();
	}
	bool csSWDeviceOEMPA::IsWTSWAvailable()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsWTSWAvailable();
	}
	bool csSWDeviceOEMPA::IsEncoderDecimal()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsEncoderDecimal();
	}
	bool csSWDeviceOEMPA::IsFMCElementStepSupported()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsFMCElementStepSupported();
	}

	bool csSWDeviceOEMPA::SetKeepAlive(csEnumKeepAlive eKeepAlive)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetKeepAlive((enumKeepAlive)eKeepAlive);
	}
	csEnumKeepAlive csSWDeviceOEMPA::GetKeepAlive()
	{
		if(!m_pSWDeviceOEMPA)
			return (csEnumKeepAlive)0;
		return (csEnumKeepAlive)m_pSWDeviceOEMPA->GetKeepAlive();
	}

	bool csSWDeviceOEMPA::EnableAscan(bool bEnable)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->EnableAscan(bEnable);
	}
	bool csSWDeviceOEMPA::IsAscanEnabled()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsAscanEnabled();
	}

	bool csSWDeviceOEMPA::SetAscanBitSize(csEnumBitSize eBitSize)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetAscanBitSize((enumBitSize)eBitSize);
	}
	csEnumBitSize csSWDeviceOEMPA::GetAscanBitSize()
	{
		if(!m_pSWDeviceOEMPA)
			return (csEnumBitSize)0;
		return (csEnumBitSize)m_pSWDeviceOEMPA->GetAscanBitSize();
	}

	bool csSWDeviceOEMPA::SetAscanRequest(csEnumAscanRequest eAscanRequest)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetAscanRequest((enumAscanRequest)eAscanRequest);
	}
	csEnumAscanRequest csSWDeviceOEMPA::GetAscanRequest()
	{
		if(!m_pSWDeviceOEMPA)
			return (csEnumAscanRequest)0;
		return (csEnumAscanRequest)m_pSWDeviceOEMPA->GetAscanRequest();
	}

	bool csSWDeviceOEMPA::SetAscanRequestFrequency(double dFreq)//Hz
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetAscanRequestFrequency(dFreq);
	}
	bool csSWDeviceOEMPA::GetAscanRequestFrequency([Out] double %dFreq)
	{
		double dFreq2;
		bool bRet;

		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetAscanRequestFrequency(dFreq2);
		dFreq = dFreq2;
		return bRet;
	}

	bool csSWDeviceOEMPA::EnableCscanTof(bool bEnable)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->EnableCscanTof(bEnable);
	}
	bool csSWDeviceOEMPA::IsCscanTofEnabled()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsCscanTofEnabled();
	}

	bool csSWDeviceOEMPA::SetCycleCount(int iCount)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetCycleCount(iCount);
	}
	int csSWDeviceOEMPA::GetCycleCount()
	{
		if(!m_pSWDeviceOEMPA)
			return 0;
		return m_pSWDeviceOEMPA->GetCycleCount();
	}

	bool csSWDeviceOEMPA::SetTriggerMode(csEnumOEMPATrigger eTrig)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetTriggerMode((enumOEMPATrigger)eTrig);
	}
	csEnumOEMPATrigger csSWDeviceOEMPA::GetTriggerMode()
	{
		if(!m_pSWDeviceOEMPA)
			return (csEnumOEMPATrigger)0;
		return (csEnumOEMPATrigger)m_pSWDeviceOEMPA->GetTriggerMode();
	}

	bool csSWDeviceOEMPA::SetTriggerEncoderStep(double dStep)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetTriggerEncoderStep(dStep);
	}
	bool csSWDeviceOEMPA::GetTriggerEncoderStep([Out] double %dStep)
	{
		double dStep2;
		bool bRet;

		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetTriggerEncoderStep(dStep2);
		dStep = dStep2;
		return bRet;
	}

	bool csSWDeviceOEMPA::SetSignalTriggerHighTime(double dTime)
	{
		double dTime2;
		bool bRet;

		if(!m_pSWDeviceOEMPA)
			return false;
		dTime2 = dTime;
		bRet = m_pSWDeviceOEMPA->SetSignalTriggerHighTime(dTime2);
		dTime = dTime2;
		return bRet;
	}
	double csSWDeviceOEMPA::GetSignalTriggerHighTime()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->GetSignalTriggerHighTime();
	}

	bool csSWDeviceOEMPA::SetRequestIO(csEnumOEMPARequestIO eRequest)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetRequestIO((enumOEMPARequestIO)eRequest);
	}
	csEnumOEMPARequestIO csSWDeviceOEMPA::GetRequestIO()
	{
		if(!m_pSWDeviceOEMPA)
			return (csEnumOEMPARequestIO)0;
		return (csEnumOEMPARequestIO)m_pSWDeviceOEMPA->GetRequestIO();
	}

	bool csSWDeviceOEMPA::SetRequestIODigitalInputMaskRising(int iMask)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetRequestIODigitalInputMaskRising(iMask);
	}
	bool csSWDeviceOEMPA::GetRequestIODigitalInputMaskRising([Out] int %iMask)
	{
		int iMask2;
		bool bRet;

		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetRequestIODigitalInputMaskRising(iMask2);
		iMask = iMask2;
		return bRet;
	}

	bool csSWDeviceOEMPA::SetRequestIODigitalInputMaskFalling(int iEvent)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetRequestIODigitalInputMaskFalling(iEvent);
	}
	bool csSWDeviceOEMPA::GetRequestIODigitalInputMaskFalling([Out] int %iEvent)
	{
		int iEvent2;
		bool bRet;

		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetRequestIODigitalInputMaskFalling(iEvent2);
		iEvent = iEvent2;
		return bRet;
	}

	bool csSWDeviceOEMPA::SetExternalTriggerCycle(csEnumDigitalInput eDigitalInput)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetExternalTriggerCycle((enumDigitalInput)eDigitalInput);
	}
	bool csSWDeviceOEMPA::GetExternalTriggerCycle([Out] csEnumDigitalInput %eDigitalInput)
	{
		enumDigitalInput eDigitalInput2;
		bool bRet;

		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetExternalTriggerCycle(eDigitalInput2);
		eDigitalInput = (csEnumDigitalInput)eDigitalInput2;
		return bRet;
	}

	bool csSWDeviceOEMPA::SetExternalTriggerSequence(csEnumDigitalInput eDigitalInput)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetExternalTriggerSequence((enumDigitalInput)eDigitalInput);
	}
	bool csSWDeviceOEMPA::GetExternalTriggerSequence([Out] csEnumDigitalInput %eDigitalInput)
	{
		enumDigitalInput eDigitalInput2;
		bool bRet;

		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetExternalTriggerSequence(eDigitalInput2);
		eDigitalInput = (csEnumDigitalInput)eDigitalInput2;
		return bRet;
	}

	bool csDriverOEMPA::csSWDeviceOEMPA::SetExternalTimestampReset(csEnumDigitalInput eDigitalInput)
	{
		if (!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetExternalTimestampReset((enumDigitalInput)eDigitalInput);
	}

	bool csDriverOEMPA::csSWDeviceOEMPA::GetExternalTimestampReset(csEnumDigitalInput% eDigitalInput)
	{
		enumDigitalInput eDigitalInput2;
		bool bRet;

		if (!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetExternalTimestampReset(eDigitalInput2);
		eDigitalInput = (csEnumDigitalInput)eDigitalInput2;
		return bRet;
	}

	bool csSWDeviceOEMPA::SetMappingOutput(int iOutputIndex,csEnumOEMPAMappingDigitalOutput eMapping)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetMappingOutput(iOutputIndex,(enumOEMPAMappingDigitalOutput)eMapping);
	}
	bool csSWDeviceOEMPA::GetMappingOutput(int iOutputIndex,[Out] csEnumOEMPAMappingDigitalOutput %eMapping)
	{
		enumOEMPAMappingDigitalOutput eMapping2;
		bool bRet;

		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetMappingOutput(iOutputIndex,eMapping2);
		eMapping = (csEnumOEMPAMappingDigitalOutput)eMapping2;
		return bRet;
	}

	bool csSWDeviceOEMPA::SetRequestCscan(csEnumOEMPARequestCscan eRequest)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetRequestCscan((enumOEMPARequestCscan)eRequest);
	}
	csEnumOEMPARequestCscan csSWDeviceOEMPA::GetRequestCscan()
	{
		if(!m_pSWDeviceOEMPA)
			return (csEnumOEMPARequestCscan)0;
		return (csEnumOEMPARequestCscan)m_pSWDeviceOEMPA->GetRequestCscan();
	}

	bool csSWDeviceOEMPA::SetEncoderDebouncer(double dTime)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetEncoderDebouncer(dTime);
	}
	double csSWDeviceOEMPA::GetEncoderDebouncer()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->GetEncoderDebouncer();
	}

	//1.1.5.4w
	//bool csSWDeviceOEMPA::SetDigitalInput(WORD usValue)
	//{
	//	if(!m_pSWDeviceOEMPA)
	//		return false;
	//	return m_pSWDeviceOEMPA->SetDigitalInput(usValue);
	//}
	WORD csSWDeviceOEMPA::GetDigitalInput()
	{
		if(!m_pSWDeviceOEMPA)
			return 0;
		return m_pSWDeviceOEMPA->GetDigitalInput();
	}

	bool csSWDeviceOEMPA::SetDigitalDebouncer(double dTime)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetDigitalDebouncer(dTime);
	}
	double csSWDeviceOEMPA::GetDigitalDebouncer()
	{
		if(!m_pSWDeviceOEMPA)
			return 0.0;
		return m_pSWDeviceOEMPA->GetDigitalDebouncer();
	}

	bool csSWDeviceOEMPA::ResetEncoder()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->ResetEncoder();
	}

	bool csSWDeviceOEMPA::GetTemperatureCount([Out] int %iBoardCount,[Out] int %iSensorCount)
	{
		int iBoardCount2,iSensorCount2;
		bool bRet;

		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetTemperatureCount(iBoardCount2,iSensorCount2);
		iBoardCount = iBoardCount2;
		iSensorCount = iSensorCount2;
		return bRet;
	}
	bool csSWDeviceOEMPA::GetTemperatureSensorCount(int iBoardIndex,[Out] int %iSensorCount)
	{
		int iSensorCount2;
		bool bRet;

		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetTemperatureSensorCount(iBoardIndex,iSensorCount2);
		iSensorCount = iSensorCount2;
		return bRet;
	}
	bool csSWDeviceOEMPA::GetTemperature(int iBoardIndex,int iSensorIndex,[Out] float %fValue)
	{
		float fValue2;
		int iRet;

		if(!m_pSWDeviceOEMPA)
			return false;
		iRet = m_pSWDeviceOEMPA->GetTemperature(iBoardIndex,iSensorIndex,fValue2);
		fValue = fValue2;
		return (iRet?false:true);
	}

	bool csSWDeviceOEMPA::IsIOBoardEnabled()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsIOBoardEnabled();
	}
	bool csSWDeviceOEMPA::IsOEMMCEnabled()
	{
		if(!m_pSWDeviceOEMPA)
			return 0.0;
		return m_pSWDeviceOEMPA->IsIOBoardEnabled();
	}
	double csSWDeviceOEMPA::GetPulserPowerMax()
	{
		if(!m_pSWDeviceOEMPA)
			return 0.0;
		return m_pSWDeviceOEMPA->GetPulserPowerMax();
	}
	double csSWDeviceOEMPA::GetPulserPowerCurrent()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->GetPulserPowerCurrent();
	}
	BYTE csSWDeviceOEMPA::GetFlashUSB3Version()
	{
		if(!m_pSWDeviceOEMPA)
			return 0;
		return m_pSWDeviceOEMPA->GetFlashUSB3Version();
	}
	DWORD csSWDeviceOEMPA::GetFWUSB3Version()
	{
		if(!m_pSWDeviceOEMPA)
			return 0;
		return m_pSWDeviceOEMPA->GetFWUSB3Version();
	}
	bool csSWDeviceOEMPA::EnableUSB3(bool bEnable)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->EnableUSB3(bEnable);
	}
	bool csSWDeviceOEMPA::IsUSB3Enabled()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsUSB3Enabled();
	}
	DWORD csSWDeviceOEMPA::GetMBOptions()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->GetMBOptions();
	}

	bool csSWDeviceOEMPA::GetEmbeddedVersion([Out] csVersion^ %version)
	{
		bool bRet;
		CSWDeviceOEMPA::structVersion _version;

		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetEmbeddedVersion(_version);
		version = safe_cast<csVersion^>(Marshal::PtrToStructure((IntPtr)&_version,csVersion::typeid));
		return bRet;
	}
	bool csSWDeviceOEMPA::GetOptionsCom([Out] csOptionsCom^ %optionsCom)
	{
		bool bRet;
		CSWDeviceOEMPA::structOptionsCom _optionsCom;

		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetOptionsCom(_optionsCom);
		optionsCom = safe_cast<csOptionsCom^>(Marshal::PtrToStructure((IntPtr)&_optionsCom,csOptionsCom::typeid));
		return bRet;
	}
	bool csSWDeviceOEMPA::GetOptionsTCP([Out] csOptionsTCP^ %optionsTCP)
	{
		bool bRet;
		CSWDeviceOEMPA::structOptionsTCP _optionsTCP;

		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetOptionsTCP(_optionsTCP);
		optionsTCP = safe_cast<csOptionsTCP^>(Marshal::PtrToStructure((IntPtr)&_optionsTCP,csOptionsTCP::typeid));
		return bRet;
	}
	bool csSWDeviceOEMPA::GetOptionsFlash([Out] csOptionsFlash^ %optionsFlash)
	{
		bool bRet;
		CSWDeviceOEMPA::structOptionsFlash _optionsFlash;

		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetOptionsFlash(_optionsFlash);
		optionsFlash = safe_cast<csOptionsFlash^>(Marshal::PtrToStructure((IntPtr)&_optionsFlash,csOptionsFlash::typeid));
		return bRet;
	}
	int csSWDeviceOEMPA::GetPasscodeCount()
	{
		if(!m_pSWDeviceOEMPA)
			return 0;
		return m_pSWDeviceOEMPA->GetPasscodeCount();
	}
	bool csSWDeviceOEMPA::GetPasscode(int iIndex,[Out] DWORD %dwPasscode)
	{
		bool bRet;
		DWORD _dwPasscode;

		dwPasscode = 0;
		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetPasscode(iIndex,_dwPasscode);
		dwPasscode = _dwPasscode;
		return bRet;
	}
	double csSWDeviceOEMPA::GetMaximumThroughput()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->GetMaximumThroughput();
	}

	bool csSWDeviceOEMPA::UpdateAllFilter()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->UpdateAllFilter();
	}

	void csSWDeviceOEMPA::EnableMultiChannel(bool bEnable)
	{
		CSWDeviceOEMPA::EnableMultiChannel(bEnable);
	}
	bool csSWDeviceOEMPA::IsMultiChannelEnabled()
	{
		return CSWDeviceOEMPA::IsMultiChannelEnabled();
	}
	void csSWDeviceOEMPA::EnableLoadDefaultSetup(bool bEnable)
	{
		CSWDeviceOEMPA::EnableLoadDefaultSetup(bEnable);
	}
	bool csSWDeviceOEMPA::IsLoadDefaultSetupEnabled()
	{
		return CSWDeviceOEMPA::IsLoadDefaultSetupEnabled();
	}

	bool csSWDeviceOEMPA::IsSubSequenceAverageSupported()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsSubSequenceAverageSupported();
	}
	bool csSWDeviceOEMPA::IsTimeOffsetSupported()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsTimeOffsetSupported();
	}

	void csSWDeviceOEMPA::SetCalibrationParameters(float fWidth,float fStart,float fRange,float fGainAnalog,double dGainDigital)
	{
		if(!m_pSWDeviceOEMPA)
			return;
		return m_pSWDeviceOEMPA->SetCalibrationParameters(fWidth,fStart,fRange,fGainAnalog,dGainDigital);
	}
	void csSWDeviceOEMPA::GetCalibrationParameters(float &fWidth,float &fStart,float &fRange,float &fGainAnalog,double &dGainDigital)
	{
		if(!m_pSWDeviceOEMPA)
			return;
		return m_pSWDeviceOEMPA->GetCalibrationParameters(fWidth,fStart,fRange,fGainAnalog,dGainDigital);
	}
	bool csSWDeviceOEMPA::IsCalibrationPerformed()
	{
		return IsAlignmentPerformed();
	}
	bool csSWDeviceOEMPA::IsAlignmentPerformed()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsAlignmentPerformed();
	}
	bool csSWDeviceOEMPA::EnableAlignment(bool bEnable)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->EnableAlignment(bEnable);
	}
	float csSWDeviceOEMPA::GetCalibrationAlignment()
	{
		if(!m_pSWDeviceOEMPA)
			return 0.0f;
		return m_pSWDeviceOEMPA->GetCalibrationAlignment();
	}
	float csSWDeviceOEMPA::GetCalibrationOffset()
	{
		if(!m_pSWDeviceOEMPA)
			return 0.0f;
		return m_pSWDeviceOEMPA->GetCalibrationOffset();
	}
	bool csSWDeviceOEMPA::IsAlignmentEnabled()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsAlignmentEnabled();
	}
	bool csSWDeviceOEMPA::ResetAlignment()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->ResetAlignment();
	}
	bool csSWDeviceOEMPA::SetCalibrationFileReport(String ^pFileReport)
	{
		wchar_t* y;

		if(!m_pSWDeviceOEMPA)
			return false;
		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pFileReport);
		m_pSWDeviceOEMPA->SetCalibrationFileReport(y);
		Marshal::FreeHGlobal((IntPtr)y);
		return true;
	}
	bool csSWDeviceOEMPA::GetCalibrationFileReport([Out] String^ %pFileReport)
	{
		wchar_t pAux[MAX_PATH];

		if(!m_pSWDeviceOEMPA)
			return false;
		if(!m_pSWDeviceOEMPA->GetCalibrationFileReport(MAX_PATH,pAux))
			return false;
		pFileReport = Marshal::PtrToStringUni((IntPtr)pAux);
		return true;
	}
	bool csSWDeviceOEMPA::SetTimeOffset(float fTimeOffset)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->SetTimeOffset(fTimeOffset);
	}
	float csSWDeviceOEMPA::GetTimeOffset()
	{
		if(!m_pSWDeviceOEMPA)
			return 0.0f;
		return m_pSWDeviceOEMPA->GetTimeOffset();
	}
	void csSWDeviceOEMPA::EnablePulserDuringReplay(bool bEnable)
	{
		if(!m_pSWDeviceOEMPA)
			return;
		m_pSWDeviceOEMPA->EnablePulserDuringReplay(bEnable);
	}
	bool csSWDeviceOEMPA::IsPulserDuringReplayEnabled()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsPulserDuringReplayEnabled();
	}
	bool csSWDeviceOEMPA::EnableCscanTimeOfFlightCorrection(bool bEnable)
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->EnableCscanTimeOfFlightCorrection(bEnable);
	}
	bool csSWDeviceOEMPA::IsCscanTimeOfFlightCorrectionEnabled()
	{
		if(!m_pSWDeviceOEMPA)
			return false;
		return m_pSWDeviceOEMPA->IsCscanTimeOfFlightCorrectionEnabled();
	}
	bool csSWDeviceOEMPA::GetCscanTimeOfFlightCorrection(int iCycle,BYTE %byDecimation,float %fAscanStart)
	{
		bool bRet;
		BYTE byDecimation2;
		float fAscanStart2;

		if(!m_pSWDeviceOEMPA)
			return false;
		bRet = m_pSWDeviceOEMPA->GetCscanTimeOfFlightCorrection(iCycle,byDecimation2,fAscanStart2);
		byDecimation = byDecimation2;
		fAscanStart = fAscanStart2;
		return bRet;
	}
	bool csSWDeviceOEMPA::SetCscanTimeOfFlightCorrection(int iCycle,BYTE byDecimation,float fAscanStart)
	{
		BYTE byDecimation2;
		float fAscanStart2;

		if(!m_pSWDeviceOEMPA)
			return false;
		byDecimation2 = byDecimation;
		fAscanStart2 = fAscanStart;
		return m_pSWDeviceOEMPA->SetCscanTimeOfFlightCorrection(iCycle,byDecimation2,fAscanStart2);
	}
	double csSWDeviceOEMPA::GetFWAscanRecoveryTime()
	{
		if(!m_pSWDeviceOEMPA)
			return 0.0;
		return m_pSWDeviceOEMPA->GetFWAscanRecoveryTime();
	}
	double csSWDeviceOEMPA::GetFMCSubCycleRecoveryTime()
	{
		if(!m_pSWDeviceOEMPA)
			return 0.0;
		return m_pSWDeviceOEMPA->GetFMCSubCycleRecoveryTime();
	}
	double csSWDeviceOEMPA::GetFMCCycleRecoveryTime()
	{
		if(!m_pSWDeviceOEMPA)
			return 0.0;
		return m_pSWDeviceOEMPA->GetFMCCycleRecoveryTime();
	}
#pragma endregion csSWDeviceOEMPA
////////////////////////////////////////////////////////
#pragma region csHWDeviceOEMPA
	csHWDeviceOEMPA::csHWDeviceOEMPA(csEnumHardware csHW) : csHWDevice()
	{
		bool bCreate;
		int iCommunicationDirectTCP = CSWDevice::ReadCfgDWord(L"UTKernel" LS L"DeviceOEMPAmini", L"CommunicationDirectTCP", 1, 0, 0xffffffff, bCreate);
		if (csHW == csEnumHardware::csOEMPAmini)
		{
			if (iCommunicationDirectTCP)
				Constructor(csHW, false, g_iOEMPAminiTCP);
			else
				Constructor(csHW, false, g_iOEMPAminiUDP);
		}
		else {
			Constructor(csHW, false, 0);
		}
	}
	csHWDeviceOEMPA::csHWDeviceOEMPA(csEnumHardware csHW, bool bExternCustomizedAPI, bool bTCP) : csHWDevice()
	{
		if (csHW == csEnumHardware::csOEMPAmini)
		{
			if (bTCP)
				Constructor(csHW, bExternCustomizedAPI, g_iOEMPAminiTCP);
			else
				Constructor(csHW, bExternCustomizedAPI, g_iOEMPAminiUDP);
		}
		else {
			Constructor(csHW, bExternCustomizedAPI, 0);
		}
	}
	void csHWDeviceOEMPA::Constructor(csEnumHardware csHW, bool bExternCustomizedAPI, int iOption)
	{
		bool bCreate;
		int iValue = CSWDevice::ReadCfgInt(L"UTKernel" LS L"Device",L"csDriverOEMPACount",0,bCreate);

		iValue++;
		CSWDevice::WriteCfgInt(L"UTKernel" LS L"Device",L"csDriverOEMPACount",iValue);
#ifndef _DRIVER_11XY_
		if (iOption && (csHW == csEnumHardware::csOEMPAmini))
			m_pHWDeviceOEMPA = OEMPA_NewDevice((enumHardware)iOption);
		else
			m_pHWDeviceOEMPA = OEMPA_NewDevice((enumHardware)csHW);
#else //_DRIVER_11XY_
		m_pHWDeviceOEMPA = OEMPA_NewDevice();
#endif //_DRIVER_11XY_
		m_bExternCustomizedAPI = bExternCustomizedAPI;
		if(!bExternCustomizedAPI)
			m_csCustomizedOEMPA = gcnew csCustomizedOEMPA(m_pHWDeviceOEMPA);
		else
			m_csCustomizedOEMPA = nullptr;
		m_pHWDevice = dynamic_cast<CHWDevice*>(m_pHWDeviceOEMPA);
		m_pSWDeviceOEMPA = m_pHWDeviceOEMPA->GetSWDeviceOEMPA();
		csHWDevice::Constructor(m_pHWDeviceOEMPA,m_pHWDevice);
		csHWDevice::Constructor(m_csCustomizedOEMPA);
		m_csSWDeviceOEM = gcnew csSWDeviceOEMPA();
		m_csSWDeviceOEM->Constructor(m_pHWDeviceOEMPA,m_pSWDeviceOEMPA);
		m_FifoAscan = gcnew csAcquisitionFifo(csEnumAcquisitionFifo::csFifoAscan, this);
		m_FifoCscan = gcnew csAcquisitionFifo(csEnumAcquisitionFifo::csFifoCscan, this);
		m_FifoIO = gcnew csAcquisitionFifo(csEnumAcquisitionFifo::csFifoIO, this);
		if(m_pHWDevice)
			m_pHWDevice->SetDerivedClass(L"csHWDeviceOEMPA",NULL);
	}
	csHWDeviceOEMPA::~csHWDeviceOEMPA()
	{
		this->!csHWDeviceOEMPA();
	}
	csHWDeviceOEMPA::!csHWDeviceOEMPA()
	{
		Free();
	}
	void csHWDeviceOEMPA::Free()
	{
		m_pSWDeviceOEMPA = NULL;
		if(m_pHWDeviceOEMPA)
			delete m_pHWDeviceOEMPA;
		m_pHWDeviceOEMPA = NULL;
	}
	bool csHWDeviceOEMPA::SetCustomizedOEMPA(csCustomizedOEMPA ^pCustomizedOEM)
	{
		if(!m_bExternCustomizedAPI)
			return false;
		m_csCustomizedOEMPA = pCustomizedOEM;
		csHWDevice::Constructor(m_csCustomizedOEMPA);
		return true;
	}
	csAcquisitionFifo ^csHWDeviceOEMPA::GetAcquisitionFifo(csEnumAcquisitionFifo csFifo)
	{
		switch(csFifo)
		{
		case csEnumAcquisitionFifo::csFifoAscan: return m_FifoAscan;break;
		case csEnumAcquisitionFifo::csFifoCscan: return m_FifoCscan;break;
		case csEnumAcquisitionFifo::csFifoIO: return m_FifoIO;break;
		}
		return nullptr;
	}
	System::Void* csHWDeviceOEMPA::cGetHWDeviceOEMPA()
	{
		return (System::Void*)m_pHWDeviceOEMPA;
	}
	csHWDeviceOEMPA^ csHWDeviceOEMPA::GetHWDeviceOEMPA()
	{
		return this;
	}
	csSWDeviceOEMPA^ csHWDeviceOEMPA::GetSWDeviceOEMPA()
	{
		return m_csSWDeviceOEM;
	}
	csHWDevice ^csHWDeviceOEMPA::GetHWDevice()
	{
		return this;
	}
	csCustomizedOEMPA ^csHWDeviceOEMPA::GetCustomizedOEMPA()
	{
		return m_csCustomizedOEMPA;
	}

	/*unsafe*/bool csHWDeviceOEMPA::GetDigitalInput(DWORD *pdwData)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetDigitalInput(pdwData);
	}
	bool csHWDeviceOEMPA::DisableUSB3(bool bDisable)
	{
		bool bRet,bDisable2;

		if(!m_pHWDeviceOEMPA)
			return false;
		bDisable2 = bDisable;
		bRet = m_pHWDeviceOEMPA->DisableUSB3(bDisable2);
		bDisable = bDisable2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetUSB3Disabled(/*fixed*/[Out] bool *pbDisable)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetUSB3Disabled(pbDisable);
	}

	/*unsafe*/bool csHWDeviceOEMPA::GetTemperatureSensor(int iIndexBoard,int iIndexSensor,WORD *pwTemperature)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetTemperatureSensor(iIndexBoard,iIndexSensor,pwTemperature);
	}
	bool csHWDeviceOEMPA::SetTemperatureAlarm(BYTE &byWarning,BYTE &byAlarm)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->SetTemperatureAlarm(byWarning,byAlarm);
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetTemperatureAlarm(BYTE *pbyWarning,BYTE *pbyAlarm)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetTemperatureAlarm(pbyWarning,pbyAlarm);
	}

	//MultiProcess management begin
	bool csHWDeviceOEMPA::IsMultiProcessRegistered()
	{
		return CHWDeviceOEMPA::IsMultiProcessRegistered();
	}
	bool csHWDeviceOEMPA::RegisterMultiProcess(String ^wcProcessName)
	{
		wchar_t* y;
		bool bRet;

		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(wcProcessName);
		bRet = CHWDeviceOEMPA::RegisterMultiProcess(y);
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}
	bool csHWDeviceOEMPA::UnregisterMultiProcess()
	{
		return CHWDeviceOEMPA::UnregisterMultiProcess();
	}
	bool csHWDeviceOEMPA::IsMultiProcessConnected(String ^wcAddress,[Out] DWORD %dwProcessId)
	{
		wchar_t* y;
		bool bRet;
		DWORD dwProcessId2;

		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(wcAddress);
		bRet = CHWDeviceOEMPA::IsMultiProcessConnected(y,dwProcessId2);
		dwProcessId = dwProcessId2;
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}
	bool csHWDeviceOEMPA::DisconnectMultiProcess(String ^wcAddress,DWORD dwProcessId)
	{
		wchar_t* y;
		bool bRet;
		DWORD dwProcessId2;

		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(wcAddress);
		dwProcessId2 = dwProcessId;
		bRet = CHWDeviceOEMPA::DisconnectMultiProcess(y,dwProcessId2);
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}
	int csHWDeviceOEMPA::GetMultiProcessCount()
	{
		return CHWDeviceOEMPA::GetMultiProcessCount();
	}
	bool csHWDeviceOEMPA::GetMultiProcessInfo(int iIndex,[Out] DWORD %dwProcessId,[Out] String ^wcProcessName)
	{
		bool bRet;
		DWORD dwProcessId2;
		wchar_t wcProcessName2[MAX_PATH/2];

		bRet = CHWDeviceOEMPA::GetMultiProcessInfo(iIndex,dwProcessId2,MAX_PATH/2,wcProcessName2);
		dwProcessId = dwProcessId2;
		wcProcessName = Marshal::PtrToStringUni((IntPtr)wcProcessName2);
		return bRet;
	}
	//MultiProcess management end

	bool csHWDeviceOEMPA::EnableFMC(bool bEnable)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->EnableFMC(bEnable);
	}
	bool csHWDeviceOEMPA::GetEnableFMC(bool *pbEnable)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEnableFMC(pbEnable);
	}
	bool csHWDeviceOEMPA::SetFMCElement(int %iElementStart,int %iElementStop,int %iElementStep)
	{
		int iElementStart2,iElementStop2,iElementStep2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		iElementStart2 = iElementStart;
		iElementStop2 = iElementStop;
		iElementStep2 = iElementStep;
		bRet = m_pHWDeviceOEMPA->SetFMCElement(iElementStart2,iElementStop2,iElementStep2);
		iElementStart = iElementStart2;
		iElementStop = iElementStop2;
		iElementStep = iElementStep2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetFMCElement(int *piElementStart,int *piElementStop,int *piElementStep)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetFMCElement(piElementStart,piElementStop,piElementStep);
	}
	bool csHWDeviceOEMPA::SetFMCElementStart(int %iElementIndex)
	{
		int iElementIndex2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		iElementIndex2 = iElementIndex;
		bRet = m_pHWDeviceOEMPA->SetFMCElementStart(iElementIndex2);
		iElementIndex = iElementIndex2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetFMCElementStart(int *piElementIndex)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetFMCElementStart(piElementIndex);
	}
	bool csHWDeviceOEMPA::SetFMCElementStop(int %iElementIndex)
	{
		int iElementIndex2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		iElementIndex2 = iElementIndex;
		bRet = m_pHWDeviceOEMPA->SetFMCElementStop(iElementIndex2);
		iElementIndex = iElementIndex2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetFMCElementStop(int *piElementIndex)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetFMCElementStop(piElementIndex);
	}
	bool csHWDeviceOEMPA::EnableMultiHWChannel(bool bEnable)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->EnableMultiHWChannel(bEnable);
	}
	bool csHWDeviceOEMPA::GetEnableMultiHWChannel(bool *pbEnable)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEnableMultiHWChannel(pbEnable);
	}

	bool csHWDeviceOEMPA::ResetTimeStamp()
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->ResetTimeStamp();
	}

	bool csHWDeviceOEMPA::ResetEncoder(int iEncoderIndex)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->ResetEncoder(iEncoderIndex);
	}
	bool csHWDeviceOEMPA::SetEncoder(int iEncoderIndex,double %dValue)
	{
		double dValue2=dValue;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		bRet = m_pHWDeviceOEMPA->SetEncoder(iEncoderIndex,dValue2);
		dValue = dValue2;
		return bRet;
	}
	bool csHWDeviceOEMPA::SetEncoder(int iEncoderIndex,DWORD %dwValue)
	{
		DWORD dwValue2=dwValue;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		bRet = m_pHWDeviceOEMPA->SetEncoder(iEncoderIndex,dwValue2);
		dwValue = dwValue2;
		return bRet;
	}
	bool csHWDeviceOEMPA::SetEncoderType(csEnumEncoderType %eEncoder1Type,csEnumEncoderType %eEncoder2Type)
	{
		enumEncoderType eEncoder1Type2,eEncoder2Type2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eEncoder1Type2 = (enumEncoderType)eEncoder1Type;
		eEncoder2Type2 = (enumEncoderType)eEncoder2Type;
		bRet = m_pHWDeviceOEMPA->SetEncoderType(eEncoder1Type2,eEncoder2Type2);
		eEncoder1Type = (csEnumEncoderType)eEncoder1Type2;
		eEncoder2Type = (csEnumEncoderType)eEncoder2Type2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetEncoderType(csEnumEncoderType *peEncoder1Type,csEnumEncoderType *peEncoder2Type)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEncoderType((enumEncoderType*)peEncoder1Type,(enumEncoderType*)peEncoder2Type);
	}

	bool csHWDeviceOEMPA::EnableAscan(bool bAscan)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->EnableAscan(bAscan);
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetEnableAscan(bool *pbAscan)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEnableAscan(pbAscan);
	}

	bool csHWDeviceOEMPA::EnableCscanTof(bool bCscanTof)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->EnableCscanTof(bCscanTof);
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetEnableCscanTof(bool *pbCscanTof)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEnableCscanTof(pbCscanTof);
	}

	bool csHWDeviceOEMPA::SetAscanBitSize(csEnumBitSize %eBitSize)
	{
		enumBitSize eBitSize2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eBitSize2 = (enumBitSize)eBitSize;
		bRet = m_pHWDeviceOEMPA->SetAscanBitSize(eBitSize2);
		eBitSize = (csEnumBitSize)eBitSize2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetAscanBitSize(csEnumBitSize *peBitSize)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetAscanBitSize((enumBitSize*)peBitSize);
	}

	bool csHWDeviceOEMPA::SetAscanRequest(csEnumAscanRequest %eAscanRequest)
	{
		enumAscanRequest eAscanRequest2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eAscanRequest2 = (enumAscanRequest)eAscanRequest;
		bRet = m_pHWDeviceOEMPA->SetAscanRequest(eAscanRequest2);
		eAscanRequest = (csEnumAscanRequest)eAscanRequest2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetAscanRequest(csEnumAscanRequest *peAscanRequest)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetAscanRequest((enumAscanRequest*)peAscanRequest);
	}

	bool csHWDeviceOEMPA::SetAscanRequestFrequency(double %dValue)
	{
		double dValue2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dValue2 = dValue;
		bRet = m_pHWDeviceOEMPA->SetAscanRequestFrequency(dValue2);
		dValue = dValue2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetAscanRequestFrequency(double *pdValue)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetAscanRequestFrequency(pdValue);
	}
	bool csHWDeviceOEMPA::CheckAscanRequestFrequency(double %dValue)
	{
		double dValue2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dValue2 = dValue;
		bRet = m_pHWDeviceOEMPA->CheckAscanRequestFrequency(dValue2);
		dValue = dValue2;
		return bRet;
	}

	/*unsafe*/bool csHWDeviceOEMPA::GetFWId(WORD *pwFWId)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetFWId(pwFWId);
	}

	bool csHWDeviceOEMPA::SetCycleCount(int %lCycleCount)
	{
		int lCycleCount2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		lCycleCount2 = lCycleCount;
		bRet = m_pHWDeviceOEMPA->SetCycleCount(lCycleCount2);
		lCycleCount = lCycleCount2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetCycleCount(/*fixed*/[Out] int *piCycleCount)//handle de l objet pointe
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetCycleCount(piCycleCount);
	}
	bool csHWDeviceOEMPA::CheckCycleCount(int %iCycleCount)
	{
		int iCycleCount2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		iCycleCount2 = iCycleCount;
		bRet = m_pHWDeviceOEMPA->CheckCycleCount(iCycleCount2);
		iCycleCount = iCycleCount2;
		return bRet;
	}

	bool csHWDeviceOEMPA::SetTriggerMode(csEnumOEMPATrigger %eTrig)
	{
		enumOEMPATrigger eTrig2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eTrig2 = (enumOEMPATrigger)eTrig;
		bRet = m_pHWDeviceOEMPA->SetTriggerMode(eTrig2);
		eTrig = (csEnumOEMPATrigger)eTrig2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetTriggerMode(/*fixed*/[Out] csEnumOEMPATrigger *peTrig)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetTriggerMode((enumOEMPATrigger*)peTrig);
	}
	bool csHWDeviceOEMPA::SWTrigger_Sequence()
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->SWTrigger_Sequence();
	}
	bool csHWDeviceOEMPA::SWTrigger_Cycle(int iCycleCount)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->SWTrigger_Cycle(iCycleCount);
	}
	bool csHWDeviceOEMPA::SWTrigger_ResetFWCurrentCycle()
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->SWTrigger_ResetFWCurrentCycle();
	}

	bool csHWDeviceOEMPA::SetEncoderDirection(csEnumOEMPAEncoderDirection &eEncoderDirection)
	{
		enumOEMPAEncoderDirection eEncoderDirection2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eEncoderDirection2 = (enumOEMPAEncoderDirection)eEncoderDirection;
		bRet = m_pHWDeviceOEMPA->SetEncoderDirection(eEncoderDirection2);
		eEncoderDirection = (csEnumOEMPAEncoderDirection)eEncoderDirection2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetEncoderDirection(/*fixed*/[Out] csEnumOEMPAEncoderDirection *peEncoderDirection)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEncoderDirection((enumOEMPAEncoderDirection*)peEncoderDirection);
	}

	bool csHWDeviceOEMPA::SetTriggerEncoderStep(double %dStep)
	{
		double dStep2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dStep2 = dStep;
		bRet = m_pHWDeviceOEMPA->SetTriggerEncoderStep(dStep2);
		dStep = dStep2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetTriggerEncoderStep(/*fixed*/[Out] double *pdStep)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetTriggerEncoderStep(pdStep);
	}

	bool csHWDeviceOEMPA::SetSignalTriggerHighTime(double %dTime)
	{
		double dTime2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dTime2 = dTime;
		bRet = m_pHWDeviceOEMPA->SetSignalTriggerHighTime(dTime2);
		dTime = dTime2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetSignalTriggerHighTime(/*fixed*/[Out] double *pdTime)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetSignalTriggerHighTime(pdTime);
	}

	bool csHWDeviceOEMPA::SetRequestIO(csEnumOEMPARequestIO %eRequest)
	{
		enumOEMPARequestIO eRequest2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eRequest2 = (enumOEMPARequestIO)eRequest;
		bRet = m_pHWDeviceOEMPA->SetRequestIO(eRequest2);
		eRequest = (csEnumOEMPARequestIO)eRequest2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetRequestIO(/*fixed*/ [Out] csEnumOEMPARequestIO *peRequest)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetRequestIO((enumOEMPARequestIO*)peRequest);
	}

	bool csHWDeviceOEMPA::SetRequestIODigitalInputMask(int %iMaskFalling,int %iMaskRising)
	{
		int iMaskFalling2,iMaskRising2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		iMaskFalling2 = iMaskFalling;
		iMaskRising2 = iMaskRising;
		bRet = m_pHWDeviceOEMPA->SetRequestIODigitalInputMask(iMaskFalling2,iMaskRising2);
		iMaskFalling = iMaskFalling2;
		iMaskRising = iMaskRising2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetRequestIODigitalInputMask(/*fixed*/[Out] int *piMaskFalling,/*fixed*/[Out] int *piMaskRising)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetRequestIODigitalInputMask(piMaskFalling,piMaskRising);
	}
	bool csHWDeviceOEMPA::CheckRequestDigitalInputMask(int %iMask)
	{
		int iMask2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		bRet = m_pHWDeviceOEMPA->CheckRequestDigitalInputMask(iMask2);
		iMask = iMask2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetFilterCoefficient_(csEnumOEMPAFilter eFilter,/*fixed*/WORD *pwScale,/*fixed*/[Out] cli::array<short>^ %wValue/*g_iOEMPAFilterCoefficientMax*/)
	{
		return GetFilterCoefficient(eFilter,pwScale,wValue);
	}
#ifndef COMPIL_DRIVER_OEMPAX
	/*unsafe*/bool csHWDeviceOEMPA::GetFilterCoefficient(csEnumOEMPAFilter eFilter,/*fixed*/WORD *pwScale,/*fixed*/[Out] cli::array<short>^ %wValue/*g_iOEMPAFilterCoefficientMax*/)
	{
		WORD wValue2[g_iOEMPAFilterCoefficientMax];
		bool bRet;
		int maxCoef;

		if(!m_pHWDeviceOEMPA)
			return false;
		if (m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMC2 || m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMCu || m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMCuF)
			maxCoef = g_iOEMMCFilterCoefficientMax;
		else
			maxCoef = g_iOEMPAFilterCoefficientMax;
		wValue = gcnew cli::array<short>(maxCoef);
#ifndef _DRIVER_11XY_
		if (m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMC2 || m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMCu || m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMCuF)
			bRet = CHWDeviceOEMPA::GetFilterCoefficientMC((enumOEMPAFilter)eFilter,*pwScale,wValue2);
		else
			bRet = CHWDeviceOEMPA::GetFilterCoefficient((enumOEMPAFilter)eFilter,*pwScale,wValue2);
#else //_DRIVER_11XY_
		bRet = CHWDeviceOEM::GetFilterCoefficient((enumOEMPAFilter)eFilter,*pwScale,wValue2);
#endif //_DRIVER_11XY_
		for(int i=0;i< maxCoef;i++)
			wValue[i] = wValue2[i];
		return bRet;
	}
#endif //COMPIL_DRIVER_OEMPAX
#ifdef COMPIL_DRIVER_OEMPAX
	/*unsafe*/bool csHWDeviceOEMPA::GetFilterCoefficient(csEnumOEMPAFilterIndex eFilter,/*fixed*/WORD *pwScale,/*fixed*/[Out] cli::array<short>^ %wValue/*g_iOEMPAFilterCoefficientMax*/)
	{
		WORD wValue2[g_iOEMPAFilterCoefficientMax];
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		wValue = gcnew cli::array<short>(g_iOEMPAFilterCoefficientMax);
		bRet = m_pHWDeviceOEMPA->GetFilterCoefficient((enumOEMPAFilterIndex)eFilter,*pwScale,(short*)wValue2);
		for(int i=0;i<g_iOEMPAFilterCoefficientMax;i++)
			wValue[i] = wValue2[i];
		return bRet;
	}
#endif //COMPIL_DRIVER_OEMPAX
	bool csHWDeviceOEMPA::FindFilterCoefficient(WORD %wScale,cli::array<short>^ %wValue/*g_iOEMPAFilterCoefficientMax*/,[Out] csEnumOEMPAFilter^ %eFilter)

	{
		short pwValue2[g_iOEMPAFilterCoefficientMax];
		enumOEMPAFilter eFilter2;
		bool bRet;
		int maxCoef;

		if(!m_pHWDeviceOEMPA)
			return false;
		if (m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMC2 || m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMCu || m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMCuF)
			maxCoef = g_iOEMMCFilterCoefficientMax;
		else
			maxCoef = g_iOEMPAFilterCoefficientMax;
		if(wValue->Length!= maxCoef)
			return false;
		for(int i=0;i< maxCoef;i++)
			pwValue2[i] = wValue[i];
		if (m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMC2 || m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMCu || m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMCuF)
			bRet = m_pHWDeviceOEMPA->FindFilterCoefficientMC(wScale, pwValue2, eFilter2);
		else
			bRet = m_pHWDeviceOEMPA->FindFilterCoefficient(wScale, pwValue2, eFilter2);
		eFilter = (csEnumOEMPAFilter)eFilter2;
		return bRet;
	}
	bool csHWDeviceOEMPA::SetFilter(csEnumOEMPAFilterIndex eFilterIndex,WORD %wScale,cli::array<short>^ %wValue/*g_iOEMPAFilterCoefficientMax*/)
	{
		WORD wScale2;
		short pwValue2[g_iOEMPAFilterCoefficientMax];
		bool bRet;
		int maxCoef;

		if(!m_pHWDeviceOEMPA)
			return false;
		if (m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMC2 || m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMCu || m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMCuF)
			maxCoef = g_iOEMMCFilterCoefficientMax;
		else
			maxCoef = g_iOEMPAFilterCoefficientMax;
		if(wValue->Length!= maxCoef)
			return false;
		wScale2 = wScale;
		for(int i=0;i< maxCoef;i++)
			pwValue2[i] = wValue[i];
		bRet = m_pHWDeviceOEMPA->SetFilter((enumOEMPAFilterIndex)eFilterIndex,wScale2,pwValue2);
		wScale = wScale2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetFilter(csEnumOEMPAFilterIndex eFilterIndex,/*fixed*/WORD *pwScale,/*fixed*/[Out] cli::array<short>^ %wValue/*g_iOEMPAFilterCoefficientMax*/)
	{
		short *pwValue;
		bool bRet;
		int maxCoef;

		if(!m_pHWDeviceOEMPA)
			return false;
		if (m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMC2 || m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMCu || m_pHWDevice->GetSWDevice()->GetHardware() == eOEMMCuF)
			maxCoef = g_iOEMMCFilterCoefficientMax;
		else
			maxCoef = g_iOEMPAFilterCoefficientMax;
		wValue = gcnew cli::array<short>(maxCoef);
		pwValue = (short*)(void*)ListAddObject(wValue);
		bRet = m_pHWDeviceOEMPA->GetFilter((enumOEMPAFilterIndex)eFilterIndex,pwScale,pwValue);
		return bRet;
	}

	bool csHWDeviceOEMPA::SetEncoderWire1(int iEncoderIndex,csEnumDigitalInput %eDigitalInput)//int iEncoderIndex : 0 for first encoder, 1 for second encoder.
	{
		enumDigitalInput eDigitalInput2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eDigitalInput2 = (enumDigitalInput)eDigitalInput;
		bRet = m_pHWDeviceOEMPA->SetEncoderWire1(iEncoderIndex,eDigitalInput2);
		eDigitalInput = (csEnumDigitalInput)eDigitalInput2;
		return bRet;
	}
	bool csHWDeviceOEMPA::SetEncoderWire2(int iEncoderIndex,csEnumDigitalInput %eDigitalInput)
	{
		enumDigitalInput eDigitalInput2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eDigitalInput2 = (enumDigitalInput)eDigitalInput;
		bRet = m_pHWDeviceOEMPA->SetEncoderWire2(iEncoderIndex,eDigitalInput2);
		eDigitalInput = (csEnumDigitalInput)eDigitalInput2;
		return bRet;
	}
	bool csHWDeviceOEMPA::SetExternalTriggerCycle(csEnumDigitalInput %eDigitalInput)
	{
		enumDigitalInput eDigitalInput2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eDigitalInput2 = (enumDigitalInput)eDigitalInput;
		bRet = m_pHWDeviceOEMPA->SetExternalTriggerCycle(eDigitalInput2);
		eDigitalInput = (csEnumDigitalInput)eDigitalInput2;
		return bRet;
	}
	bool csHWDeviceOEMPA::SetExternalTriggerSequence(csEnumDigitalInput %eDigitalInput)
	{
		enumDigitalInput eDigitalInput2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eDigitalInput2 = (enumDigitalInput)eDigitalInput;
		bRet = m_pHWDeviceOEMPA->SetExternalTriggerSequence(eDigitalInput2);
		eDigitalInput = (csEnumDigitalInput)eDigitalInput2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetEncoderWire1(int iEncoderIndex,/*fixed*/[Out] csEnumDigitalInput *peDigitalInput)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEncoderWire1(iEncoderIndex,(enumDigitalInput*)peDigitalInput);
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetEncoderWire2(int iEncoderIndex,/*fixed*/[Out] csEnumDigitalInput *peDigitalInput)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEncoderWire2(iEncoderIndex,(enumDigitalInput*)peDigitalInput);
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetExternalTriggerCycle(/*fixed*/[Out] csEnumDigitalInput *peDigitalInput)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetExternalTriggerCycle((enumDigitalInput*)peDigitalInput);
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetExternalTriggerSequence(/*fixed*/[Out] csEnumDigitalInput *peDigitalInput)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetExternalTriggerSequence((enumDigitalInput*)peDigitalInput);
	}

	bool csHWDeviceOEMPA::SetDigitalOutput(int iIndex,csEnumOEMPAMappingDigitalOutput eMappingDigitalOutput)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->SetDigitalOutput(iIndex,(enumOEMPAMappingDigitalOutput)eMappingDigitalOutput);
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetDigitalOutput(int iIndex,/*fixed*/[Out] csEnumOEMPAMappingDigitalOutput *peMappingDigitalOutput)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetDigitalOutput(iIndex,(enumOEMPAMappingDigitalOutput*)peMappingDigitalOutput);
	}

	bool csHWDeviceOEMPA::SetEncoderDebouncer(double %dValue)
	{
		double dValue2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dValue2 = dValue;
		bRet = m_pHWDeviceOEMPA->SetEncoderDebouncer(dValue2);
		dValue = dValue2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetEncoderDebouncer(/*fixed*/[Out] double *pdValue)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEncoderDebouncer(pdValue);
	}
	bool csHWDeviceOEMPA::CheckEncoderDebouncer(double %dValue)
	{
		double dValue2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dValue2 = dValue;
		bRet = m_pHWDeviceOEMPA->CheckEncoderDebouncer(dValue2);
		dValue = dValue2;
		return bRet;
	}

	bool csHWDeviceOEMPA::SetDigitalDebouncer(double %dValue)
	{
		double dValue2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		bRet = m_pHWDeviceOEMPA->SetDigitalDebouncer(dValue2);
		dValue = dValue2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetDigitalDebouncer(/*fixed*/[Out] double *pdValue)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetDigitalDebouncer(pdValue);
	}
	bool csHWDeviceOEMPA::CheckDigitalDebouncer(double %dValue)
	{
		double dValue2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dValue2 = dValue;
		bRet = m_pHWDeviceOEMPA->CheckDigitalDebouncer(dValue2);
		dValue = dValue2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetFlushTimer(double% dTimer)
	{
		double dTimer2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;

		dTimer2 = dTimer;
		bRet = m_pHWDeviceOEMPA->SetFlushTimer(dTimer2);
		dTimer = dTimer2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::CheckStreamTimer(double% dTimer)
	{
		double dTimer2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;

		dTimer2 = dTimer;
		bRet = m_pHWDeviceOEMPA->CheckStreamTimer(dTimer2);
		dTimer = dTimer2;
		return bRet;
	}

	bool csHWDeviceOEMPA::SetGainDigital(int iCycle,double %dGain)
	{
		double dGain2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dGain2 = dGain;
		bRet = m_pHWDeviceOEMPA->SetGainDigital(iCycle,dGain2);
		dGain = dGain2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetGainDigital(int iCycle,/*fixed*/[Out] double *pdGain)
	{
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		bRet = m_pHWDeviceOEMPA->GetGainDigital(iCycle,pdGain);
		return bRet;
	}
	bool csHWDeviceOEMPA::CheckGainDigital(double %dGain)
	{
		double dGain2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dGain2 = dGain;
		bRet = m_pHWDeviceOEMPA->CheckGainDigital(dGain2);
		dGain = dGain2;
		return bRet;
	}
	bool csDriverOEMPA::csHWDeviceOEMPA::SetGainDigital(int iCycle, int iChannel, double% dGain)
	{
		double dGain2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		dGain2 = dGain;
		bRet = m_pHWDeviceOEMPA->SetGainDigital(iCycle, iChannel, dGain2);
		dGain = dGain2;
		return bRet;
	}
	bool csDriverOEMPA::csHWDeviceOEMPA::GetGainDigital(int iCycle, int iChannel, double* pdGain)
	{
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		bRet = m_pHWDeviceOEMPA->GetGainDigital(iCycle, iChannel, pdGain);
		return bRet;
	}
	void csHWDeviceOEMPA::test()
	{
		structRoot root;
		structCycle cycle;

		OEMPA_ResetStructCycle(&cycle);
		root.iCycleCount = 1;
		cycle.dGainDigital = 12.0;
		gCallbackCustomizedOEM(m_pHWDeviceOEMPA,L"Hello world!",eWriteHW_Enter,&root,&cycle,NULL,NULL);
		gCallbackSystemMessageBoxList(L"Hello world!!");
		csHWDevice::test();
	}
	int csHWDeviceOEMPA::Test()
	{
		return 0;
	}
	bool csHWDeviceOEMPA::SetBeamCorrection(int iCycle,float %fGain)
	{
		float fGain2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		fGain2 = fGain;
		bRet = m_pHWDeviceOEMPA->SetBeamCorrection(iCycle,fGain2);
		fGain = fGain2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetBeamCorrection(int iCycle,float *pfGain)
	{
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		bRet = m_pHWDeviceOEMPA->GetBeamCorrection(iCycle,pfGain);
		return bRet;
	}
	bool csHWDeviceOEMPA::CheckBeamCorrection(float %fGain)
	{
		float fGain2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		fGain2 = fGain;
		bRet = m_pHWDeviceOEMPA->CheckBeamCorrection(fGain2);
		fGain = fGain2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetBeamCorrection(int iCycle, int iChannel, float% fGain)
	{
		float fGain2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		fGain2 = fGain;
		bRet = m_pHWDeviceOEMPA->SetBeamCorrection(iCycle, iChannel, fGain2);
		fGain = fGain2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetBeamCorrection(int iCycle, int iChannel, float* pfGain)
	{
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		bRet = m_pHWDeviceOEMPA->GetBeamCorrection(iCycle, iChannel, pfGain);
		return bRet;
	}

	bool csHWDeviceOEMPA::SetDACSlope(int iCycle,acsDac^ %dac/*int &iCountMax,structCallbackArrayFloatDac &callbackArrayFloatDac*/)
	{
		structCallbackArrayFloatDac callback;
		int iMax=g_iOEMPADACCountMax;
		bool bRet = false;

		if(!m_pHWDeviceOEMPA)
			return false;
		if(dac==nullptr)
			return false;
		if(dac->list==nullptr)
			return false;
		if(!dac->list->Length)
			return false;
		if(dac->list->Length>=iMax)
			return false;
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)dac->GetGcroot();//pdac;
		callback.apParameter[2] = NULL;
		callback.pSetSize = gCallbackSetSizeDac;
		callback.pSetData = gCallbackSetDataDac;
		callback.pGetSize = gCallbackGetSizeDac;
		callback.pGetData = gCallbackGetDataDac;
		iMax = dac->list->Length;
#ifndef _DRIVER_11XY_
		bRet = m_pHWDeviceOEMPA->SetDACSlope(iCycle,iMax,callback);
#endif //_DRIVER_11XY_
		return bRet;
	}
	bool csHWDeviceOEMPA::GetDACSlope(int iCycle,[Out] acsDac^ %dac/*int &iCountMax,structCallbackArrayFloatDac &callbackArrayFloatDac*/)
	{
		structCallbackArrayFloatDac callback;
		int iMax=g_iOEMPADACCountMax;
		bool bRet;
		gcroot<acsDac^> *pdac;

		if(!m_pHWDeviceOEMPA)
			return false;
		if(dac==nullptr)
			dac = gcnew acsDac;
		pdac = dac->GetGcroot();
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)pdac;
		callback.apParameter[2] = NULL;
		callback.pSetSize = gCallbackSetSizeDac;
		callback.pSetData = gCallbackSetDataDac;
		callback.pGetSize = gCallbackGetSizeDac;
		callback.pGetData = gCallbackGetDataDac;
		bRet = m_pHWDeviceOEMPA->GetDACSlope(iCycle,iMax,callback);
		return bRet;
	}
	bool csHWDeviceOEMPA::SetDACGain(bool bAutoStop,int iCycle,acsDac^ %dac/*int &iCountMax,structCallbackArrayFloatDac &callbackArrayFloatDac*/)
	{
		structCallbackArrayFloatDac callback;
		int iMax=g_iOEMPADACCountMax;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if(dac==nullptr)
			return false;
		if(dac->list==nullptr)
			return false;
		if(!dac->list->Length)
			return false;
		if(dac->list->Length>=iMax)
			return false;
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)dac->GetGcroot();//pdac;
		callback.apParameter[2] = (void*)1;
		callback.pSetSize = gCallbackSetSizeDac;
		callback.pSetData = gCallbackSetDataDac;
		callback.pGetSize = gCallbackGetSizeDac;
		callback.pGetData = gCallbackGetDataDac;
		iMax = dac->list->Length;
		bRet = m_pHWDeviceOEMPA->SetDACGain(bAutoStop,iCycle,iMax,callback);
		return bRet;
	}
	bool csHWDeviceOEMPA::GetDACGain(int iCycle,[Out] acsDac^ %dac/*int &iCountMax,structCallbackArrayFloatDac &callbackArrayFloatDac*/)
	{
		structCallbackArrayFloatDac callback;
		int iMax=g_iOEMPADACCountMax;
		bool bRet;
		gcroot<acsDac^> *pdac;

		if(!m_pHWDeviceOEMPA)
			return false;
		if(dac==nullptr)
			dac = gcnew acsDac;
		pdac = dac->GetGcroot();
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)pdac;
		callback.apParameter[2] = (void*)1;
		callback.pSetSize = gCallbackSetSizeDac;
		callback.pSetData = gCallbackSetDataDac;
		callback.pGetSize = gCallbackGetSizeDac;
		callback.pGetData = gCallbackGetDataDac;
		bRet = m_pHWDeviceOEMPA->GetDACGain(iCycle,iMax,callback);
		return bRet;
	}
////////////
	bool csHWDeviceOEMPA::SetEmissionDelays(int iCycle,cli::array<int>^ %aiElementList,acsFloat^ %afDelay)
	{
		int *aiElementList2=NULL;
		structCallbackArrayFloat1D callback;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((aiElementList==nullptr) || !aiElementList->Length)
			return false;
		if(aiElementList->Length)
		{
			aiElementList2 = new int[aiElementList->Length];
			if(!aiElementList2)
				return false;
		}
		for(int i=0;i<aiElementList->Length;i++)
			aiElementList2[i] = aiElementList[i];
		if(afDelay==nullptr)
			return false;
		if(afDelay->list==nullptr)
			return false;
		if(afDelay->list->Length>aiElementList->Length)
			return false;
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)afDelay->GetGcroot();
		callback.pSetSize = gCallbackSetSizeFloat;
		callback.pSetData = gCallbackSetDataFloat;
		callback.pGetSize = gCallbackGetSizeFloat;
		callback.pGetData = gCallbackGetDataFloat;
		bRet = m_pHWDeviceOEMPA->SetEmissionDelays(iCycle,aiElementList->Length,aiElementList2,callback);
		delete aiElementList2;
		return bRet;
	}
	bool csHWDeviceOEMPA::SetEmissionDelays(int iCycle,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/,acsFloat^ %afDelay/*,structCallbackArrayFloat1D &callbackArrayFloat1D*/)
	{
		DWORD adwHWAperture2[g_iOEMPAApertureDWordCount];
		structCallbackArrayFloat1D callback;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((adwHWAperture==nullptr) || (adwHWAperture->Length!=g_iOEMPAApertureDWordCount))
			return false;
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
			adwHWAperture2[i] = adwHWAperture[i];
		if(afDelay==nullptr)
			return false;
		if(afDelay->list==nullptr)
			return false;
		if(afDelay->list->Length>g_iOEMPAApertureElementCountMax)
			return false;
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)afDelay->GetGcroot();//pdac;
		callback.pSetSize = gCallbackSetSizeFloat;
		callback.pSetData = gCallbackSetDataFloat;
		callback.pGetSize = gCallbackGetSizeFloat;
		callback.pGetData = gCallbackGetDataFloat;
		bRet = m_pHWDeviceOEMPA->SetEmissionDelays(iCycle,adwHWAperture2,callback);
		return bRet;
	}
	bool csHWDeviceOEMPA::GetEmissionDelays(int iCycle,/*fixed*/[Out] acsFloat^ %afDelay/*,int &iElementCountMax,structCallbackArrayFloat1D &callbackArrayFloat1D*/)
	{
		structCallbackArrayFloat1D callback;
		int iMax=g_iOEMPAApertureElementCountMax;
		bool bRet;
		gcroot<acsFloat^> *pDelay;

		if(!m_pHWDeviceOEMPA)
			return false;
		if(afDelay==nullptr)
			afDelay = gcnew acsFloat;
		pDelay = afDelay->GetGcroot();
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)pDelay;
		callback.pSetSize = gCallbackSetSizeFloat;
		callback.pSetData = gCallbackSetDataFloat;
		callback.pGetSize = gCallbackGetSizeFloat;
		callback.pGetData = gCallbackGetDataFloat;
		bRet = m_pHWDeviceOEMPA->GetEmissionDelays(iCycle,iMax,callback);
		return bRet;
	}
	bool csDriverOEMPA::csHWDeviceOEMPA::SetEmissionDelay(int iCycle, int iChannel, float% fDelay)
	{
		bool ret;
		float delay;

		if (!m_pHWDeviceOEMPA)
			return false;
		delay = fDelay;
		ret = m_pHWDeviceOEMPA->SetEmissionDelay(iCycle, iChannel, delay);
		fDelay = delay;
		return ret;
	}
	bool csDriverOEMPA::csHWDeviceOEMPA::GetEmissionDelay(int iCycle, int iChannel, float* pfDelay)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEmissionDelay(iCycle, iChannel, pfDelay);
	}
	bool csHWDeviceOEMPA::SetEmissionWidths(int iCycle,cli::array<int>^ %aiElementList,acsFloat^ %afDelay)
	{
		int *aiElementList2=NULL;
		structCallbackArrayFloat1D callback;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((aiElementList==nullptr) || !aiElementList->Length)
			return false;
		if(aiElementList->Length)
		{
			aiElementList2 = new int[aiElementList->Length];
			if(!aiElementList2)
				return false;
		}
		for(int i=0;i<aiElementList->Length;i++)
			aiElementList2[i] = aiElementList[i];
		if(afDelay==nullptr)
			return false;
		if(afDelay->list==nullptr)
			return false;
		if(afDelay->list->Length>aiElementList->Length)
			return false;
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)afDelay->GetGcroot();
		callback.pSetSize = gCallbackSetSizeFloat;
		callback.pSetData = gCallbackSetDataFloat;
		callback.pGetSize = gCallbackGetSizeFloat;
		callback.pGetData = gCallbackGetDataFloat;
		bRet = m_pHWDeviceOEMPA->SetEmissionWidths(iCycle,aiElementList->Length,aiElementList2,callback);
		delete aiElementList2;
		return bRet;
	}
	bool csHWDeviceOEMPA::SetEmissionWidths(int iCycle,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/,acsFloat^ %afWidth/*,structCallbackArrayFloat1D &callbackArrayFloat1D*/)
	{
		DWORD adwHWAperture2[g_iOEMPAApertureDWordCount];
		structCallbackArrayFloat1D callback;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((adwHWAperture==nullptr) || (adwHWAperture->Length!=g_iOEMPAApertureDWordCount))
			return false;
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
			adwHWAperture2[i] = adwHWAperture[i];
		if(afWidth==nullptr)
			return false;
		if(afWidth->list==nullptr)
			return false;
		if(afWidth->list->Length>g_iOEMPAApertureElementCountMax)
			return false;
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)afWidth->GetGcroot();//pdac;
		callback.pSetSize = gCallbackSetSizeFloat;
		callback.pSetData = gCallbackSetDataFloat;
		callback.pGetSize = gCallbackGetSizeFloat;
		callback.pGetData = gCallbackGetDataFloat;
		bRet = m_pHWDeviceOEMPA->SetEmissionWidths(iCycle,adwHWAperture2,callback);
		return bRet;
	}
	bool csHWDeviceOEMPA::GetEmissionWidths(int iCycle,/*fixed*/[Out] acsFloat^ %afWidth/*,int &iElementCountMax,structCallbackArrayFloat1D &callbackArrayFloat1D*/)
	{
		structCallbackArrayFloat1D callback;
		int iMax=g_iOEMPAApertureElementCountMax;
		bool bRet;
		gcroot<acsFloat^> *pWidth;

		if(!m_pHWDeviceOEMPA)
			return false;
		if(afWidth==nullptr)
			afWidth = gcnew acsFloat;
		pWidth = afWidth->GetGcroot();
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)pWidth;
		callback.pSetSize = gCallbackSetSizeFloat;
		callback.pSetData = gCallbackSetDataFloat;
		callback.pGetSize = gCallbackGetSizeFloat;
		callback.pGetData = gCallbackGetDataFloat;
		bRet = m_pHWDeviceOEMPA->GetEmissionWidths(iCycle,iMax,callback);
		return bRet;
	}
	bool csDriverOEMPA::csHWDeviceOEMPA::SetEmissionWidth(int iCycle, int iChannel, float% fWidth)
	{
		bool ret;
		float width;

		if (!m_pHWDeviceOEMPA)
			return false;
		width = fWidth;
		ret = m_pHWDeviceOEMPA->SetEmissionWidth(iCycle, iChannel, width);
		fWidth = width;
		return ret;
	}
	bool csDriverOEMPA::csHWDeviceOEMPA::GetEmissionWidth(int iCycle, int iChannel, float* pfWidth)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEmissionWidth(iCycle, iChannel, pfWidth);
	}
	bool csHWDeviceOEMPA::SetReceptionGains(int iCycle,cli::array<int>^ %aiElementList,acsFloat^ %afDelay)
	{
		int *aiElementList2=NULL;
		structCallbackArrayFloat1D callback;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((aiElementList==nullptr) || !aiElementList->Length)
			return false;
		if(aiElementList->Length)
		{
			aiElementList2 = new int[aiElementList->Length];
			if(!aiElementList2)
				return false;
		}
		for(int i=0;i<aiElementList->Length;i++)
			aiElementList2[i] = aiElementList[i];
		if(afDelay==nullptr)
			return false;
		if(afDelay->list==nullptr)
			return false;
		if(afDelay->list->Length>aiElementList->Length)
			return false;
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)afDelay->GetGcroot();
		callback.pSetSize = gCallbackSetSizeFloat;
		callback.pSetData = gCallbackSetDataFloat;
		callback.pGetSize = gCallbackGetSizeFloat;
		callback.pGetData = gCallbackGetDataFloat;
		bRet = m_pHWDeviceOEMPA->SetReceptionGains(iCycle,aiElementList->Length,aiElementList2,callback);
		delete aiElementList2;
		return bRet;
	}
	bool csHWDeviceOEMPA::SetReceptionGains(int iCycle,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/,acsFloat^ %afGain/*,structCallbackArrayFloat1D &callbackArrayFloat1D*/)
	{
		DWORD adwHWAperture2[g_iOEMPAApertureDWordCount];
		structCallbackArrayFloat1D callback;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((adwHWAperture==nullptr) || (adwHWAperture->Length!=g_iOEMPAApertureDWordCount))
			return false;
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
			adwHWAperture2[i] = adwHWAperture[i];
		if(afGain==nullptr)
			return false;
		if(afGain->list==nullptr)
			return false;
		if(afGain->list->Length>g_iOEMPAApertureElementCountMax)
			return false;
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)afGain->GetGcroot();
		callback.pSetSize = gCallbackSetSizeFloat;
		callback.pSetData = gCallbackSetDataFloat;
		callback.pGetSize = gCallbackGetSizeFloat;
		callback.pGetData = gCallbackGetDataFloat;
		bRet = m_pHWDeviceOEMPA->SetReceptionGains(iCycle,adwHWAperture2,callback);
		return bRet;
	}
	bool csHWDeviceOEMPA::GetReceptionGains(int iCycle,/*fixed*/[Out] acsFloat^ %afGain/*,int &iElementCountMax,structCallbackArrayFloat1D &callbackArrayFloat1D*/)
	{
		structCallbackArrayFloat1D callback;
		int iMax=g_iOEMPAApertureElementCountMax;
		bool bRet;
		gcroot<acsFloat^> *pGain;

		if(!m_pHWDeviceOEMPA)
			return false;
		if(afGain==nullptr)
			afGain = gcnew acsFloat;
		pGain = afGain->GetGcroot();
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)pGain;
		callback.pSetSize = gCallbackSetSizeFloat;
		callback.pSetData = gCallbackSetDataFloat;
		callback.pGetSize = gCallbackGetSizeFloat;
		callback.pGetData = gCallbackGetDataFloat;
		bRet = m_pHWDeviceOEMPA->GetReceptionGains(iCycle,iMax,callback);
		return bRet;
	}
	bool csHWDeviceOEMPA::SetReceptionDelays(int iCycle,cli::array<int>^ %aiElementList,acsDelayReception^ %afDelay)
	{
		int *aiElementList2=NULL;
		structCallbackArrayFloat2D callback;
		int iMax=g_iOEMPAFocalCountMax;//g_iOEMPADACCountMax;
		int iSize1,iSize2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((aiElementList==nullptr) || !aiElementList->Length)
			return false;
		if(aiElementList->Length)
		{
			aiElementList2 = new int[aiElementList->Length];
			if(!aiElementList2)
				return false;
		}
		for(int i=0;i<aiElementList->Length;i++)
			aiElementList2[i] = aiElementList[i];
		if(afDelay==nullptr)
			return false;
		if(afDelay->list==nullptr)
			return false;
		if(afDelay->list->Length>aiElementList->Length)
			return false;
		iSize1 = afDelay->list->GetLength(0);//element count
		iSize2 = afDelay->list->GetLength(1);//focal count
		if(iSize2>iMax)
			return false;
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)afDelay->GetGcroot();
		callback.pSetSize = gCallbackSetSizeDelay2;
		callback.pSetData = gCallbackSetDataDelay2;
		callback.pGetSize = gCallbackGetSizeDelay2;
		callback.pGetData = gCallbackGetDataDelay2;
		bRet = m_pHWDeviceOEMPA->SetReceptionDelays(iCycle,aiElementList->Length,aiElementList2,callback);
		delete aiElementList2;
		return bRet;
	}
	bool csHWDeviceOEMPA::SetReceptionDelays(int iCycle,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/,acsDelayReception^ %afDelay/*,structCallbackArrayFloat2D &callbackArrayFloat2D*/)
	{
		DWORD adwHWAperture2[g_iOEMPAApertureDWordCount];
		structCallbackArrayFloat2D callback;
		int iMax=g_iOEMPAFocalCountMax;//g_iOEMPADACCountMax;
		int iSize1,iSize2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((adwHWAperture==nullptr) || (adwHWAperture->Length!=g_iOEMPAApertureDWordCount))
			return false;
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
			adwHWAperture2[i] = adwHWAperture[i];
		if(afDelay==nullptr)
			return false;
		if(afDelay->list==nullptr)
			return false;
		iSize1 = afDelay->list->GetLength(0);//element count
		iSize2 = afDelay->list->GetLength(1);//focal count
		if(iSize2>iMax)
			return false;
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)afDelay->GetGcroot();//pdac;
		callback.pSetSize = gCallbackSetSizeDelay2;
		callback.pSetData = gCallbackSetDataDelay2;
		callback.pGetSize = gCallbackGetSizeDelay2;
		callback.pGetData = gCallbackGetDataDelay2;
		bRet = m_pHWDeviceOEMPA->SetReceptionDelays(iCycle,adwHWAperture2,callback);
		return bRet;
	}
	bool csHWDeviceOEMPA::GetReceptionDelays(int iCycle,/*fixed*/[Out] acsDelayReception^ %afDelay/*,int &iElementCountMax,int &iFocalCountMax,structCallbackArrayFloat2D &callbackArrayFloat2D*/)
	{
		structCallbackArrayFloat2D callback;
		int iMax1=g_iOEMPAApertureElementCountMax;
		int iMax2=g_iOEMPAFocalCountMax;
		bool bRet;
		gcroot<acsDelayReception^> *pDelay2;

		if(!m_pHWDeviceOEMPA)
			return false;
		if(afDelay==nullptr)
			afDelay = gcnew acsDelayReception;
		pDelay2 = afDelay->GetGcroot();
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)pDelay2;
		callback.pSetSize = gCallbackSetSizeDelay2;
		callback.pSetData = gCallbackSetDataDelay2;
		callback.pGetSize = gCallbackGetSizeDelay2;
		callback.pGetData = gCallbackGetDataDelay2;
		bRet = m_pHWDeviceOEMPA->GetReceptionDelays(iCycle,iMax1,iMax2,callback);
		return bRet;
	}
////////////
	bool csHWDeviceOEMPA::GetApertureOEM(cli::array<int>^ %aiElementList,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/)
	{
		return false;
	}
	bool csHWDeviceOEMPA::GetApertureOEM(cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/,cli::array<int>^ %aiElementList)
	{
		return false;
	}
////////////
	bool csHWDeviceOEMPA::EnableDAC(int iCycle,bool %bEnable)
	{
		bool bEnable2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		bEnable2 = bEnable;
		bRet = m_pHWDeviceOEMPA->EnableDAC(iCycle,bEnable2);
		bEnable = bEnable2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetEnableDAC(int iCycle,bool *pbEnable)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEnableDAC(iCycle,pbEnable);
	}
	bool csDriverOEMPA::csHWDeviceOEMPA::SetDACSlope(int iCycle, int iChannel, acsDac^% dac)
	{
		structCallbackArrayFloatDac callback;
		int iMax = g_iOEMMCDACCountMax;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		if (dac == nullptr)
			return false;
		if (dac->list == nullptr)
			return false;
		if (!dac->list->Length)
			return false;
		if (dac->list->Length >= iMax)
			return false;
		iMax = dac->list->Length;
		/*double pdTime[g_iOEMMCDACCountMax];
		float pfSlope[g_iOEMMCDACCountMax];
		wchar_t error_msg[MAX_PATH];
		for (int iDacIndex = 0; iDacIndex < iMax; iDacIndex++)
		{
			pdTime[iDacIndex] = dac[iDacIndex]->dTime;
			pfSlope[iDacIndex] = dac[iDacIndex]->fSlope;
		}*/
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)dac->GetGcroot();//pdac;
		callback.apParameter[2] = NULL;
		callback.pSetSize = gCallbackSetSizeDac;
		callback.pSetData = gCallbackSetDataDac;
		callback.pGetSize = gCallbackGetSizeDac;
		callback.pGetData = gCallbackGetDataDac;
#ifndef _DRIVER_11XY_
		bRet = m_pHWDeviceOEMPA->SetDACSlope(iCycle, iChannel, iMax, callback);
		//bRet = m_pHWDeviceOEMPA->SetDACSlope(iCycle, iChannel, iMax, pdTime, pfSlope, MAX_PATH, error_msg);
#endif //_DRIVER_11XY_
		return bRet;
	}
	bool csDriverOEMPA::csHWDeviceOEMPA::GetDACSlope(int iCycle, int iChannel, acsDac^% dac)
	{
		int iMax = g_iOEMMCDACCountMax;
		bool bRet;
		gcroot<acsDac^>* pdac;
		structCallbackArrayFloatDac callback;

		if (!m_pHWDeviceOEMPA)
			return false;
		if (dac == nullptr)
			dac = gcnew acsDac;
		pdac = dac->GetGcroot();

		/*g_callbackDAC[iCycle].apParameter[0] = m_pointer;
		g_callbackDAC[iCycle].apParameter[1] = (void*)pdac;
		g_callbackDAC[iCycle].apParameter[2] = NULL;
		g_callbackDAC[iCycle].pSetSize = gCallbackSetSizeDac;
		g_callbackDAC[iCycle].pSetData = gCallbackSetDataDac;
		g_callbackDAC[iCycle].pGetSize = gCallbackGetSizeDac;
		g_callbackDAC[iCycle].pGetData = gCallbackGetDataDac;
		bRet = m_pHWDeviceOEMPA->GetDACSlope(iCycle, iChannel, iMax, g_callbackDAC[iCycle]);*/

		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)pdac;
		callback.apParameter[2] = NULL;
		callback.pSetSize = gCallbackSetSizeDac;
		callback.pSetData = gCallbackSetDataDac;
		callback.pGetSize = gCallbackGetSizeDac;
		callback.pGetData = gCallbackGetDataDac;
		bRet = m_pHWDeviceOEMPA->GetDACSlope(iCycle, iMax, callback);
		return bRet;
	}
	bool csDriverOEMPA::csHWDeviceOEMPA::SetDACGain(bool bAutoStop, int iCycle, int iChannel, acsDac^% dac)
	{
		//int iMax = g_iOEMMCDACCountMax;
		//bool bRet;

		//if (!m_pHWDeviceOEMPA)
		//	return false;
		//if (dac == nullptr)
		//	return false;
		//if (dac->list == nullptr)
		//	return false;
		//if (!dac->list->Length)
		//	return false;
		//if (dac->list->Length >= iMax)
		//	return false;
		//iMax = dac->list->Length;
		//double pdTime[g_iOEMMCDACCountMax];
		//float pfGain[g_iOEMMCDACCountMax];
		//for (int iDacIndex = 0; iDacIndex < iMax; iDacIndex++)
		//{
		//	pdTime[iDacIndex] = dac[iDacIndex]->dTime;
		//	pfGain[iDacIndex] = dac[iDacIndex]->fSlope;
		//}
		//bRet = m_pHWDeviceOEMPA->SetDACGain(bAutoStop, iCycle, iChannel, iMax, pdTime, pfGain);
		//return bRet;

		structCallbackArrayFloatDac callback;
		int iMax = g_iOEMMCDACCountMax;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		if (dac == nullptr)
			return false;
		if (dac->list == nullptr)
			return false;
		if (!dac->list->Length)
			return false;
		if (dac->list->Length >= iMax)
			return false;
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)dac->GetGcroot();//pdac;
		callback.apParameter[2] = (void*)1;
		callback.pSetSize = gCallbackSetSizeDac;
		callback.pSetData = gCallbackSetDataDac;
		callback.pGetSize = gCallbackGetSizeDac;
		callback.pGetData = gCallbackGetDataDac;
		iMax = dac->list->Length;
		bRet = m_pHWDeviceOEMPA->SetDACGain(bAutoStop, iCycle, iChannel, iMax, callback);
		return bRet;
	}
	bool csDriverOEMPA::csHWDeviceOEMPA::GetDACGain(int iCycle, int iChannel, acsDac^% dac)
	{
		int iMax = g_iOEMMCDACCountMax;
		bool bRet;
		gcroot<acsDac^>* pdac;
		structCallbackArrayFloatDac callback;

		if (!m_pHWDeviceOEMPA)
			return false;
		if (dac == nullptr)
			dac = gcnew acsDac;
		pdac = dac->GetGcroot();

		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)pdac;
		callback.apParameter[2] = (void*)1;
		callback.pSetSize = gCallbackSetSizeDac;
		callback.pSetData = gCallbackSetDataDac;
		callback.pGetSize = gCallbackGetSizeDac;
		callback.pGetData = gCallbackGetDataDac;
		bRet = m_pHWDeviceOEMPA->GetDACGain(iCycle, iChannel, iMax, callback);

		/*g_callbackDAC[iCycle].apParameter[0] = m_pointer;
		g_callbackDAC[iCycle].apParameter[1] = (void*)pdac;
		g_callbackDAC[iCycle].apParameter[2] = (void*)1;
		g_callbackDAC[iCycle].pSetSize = gCallbackSetSizeDac;
		g_callbackDAC[iCycle].pSetData = gCallbackSetDataDac;
		g_callbackDAC[iCycle].pGetSize = gCallbackGetSizeDac;
		g_callbackDAC[iCycle].pGetData = gCallbackGetDataDac;
		bRet = m_pHWDeviceOEMPA->GetDACGain(iCycle, iChannel, iMax, g_callbackDAC[iCycle]);*/

		/*double pdTime[g_iOEMMCDACCountMax];
		float pfGain[g_iOEMMCDACCountMax];
		int iCount;*/
		//pdac = dac->GetGcroot();
		//callback.apParameter[0] = m_pointer;
		//callback.apParameter[1] = (void*)pdac;
		//callback.apParameter[2] = (void*)1;
		//callback.pSetSize = gCallbackSetSizeDac;
		//callback.pSetData = gCallbackSetDataDac;
		//callback.pGetSize = gCallbackGetSizeDac;
		//callback.pGetData = gCallbackGetDataDac;
		/*bRet = m_pHWDeviceOEMPA->GetDACGain(iCycle, iChannel, iMax, &iCount, pdTime, pfGain);
		if (bRet)
		{
			Array::Resize(dac->list, iCount);
			for (int iDacIndex = 0; iDacIndex < iCount; iDacIndex++)
			{
				dac->list[iDacIndex]->dTime = pdTime[iDacIndex];
				dac->list[iDacIndex]->fSlope = pfGain[iDacIndex];
			}
		}*/

		return bRet;
	}
	bool csDriverOEMPA::csHWDeviceOEMPA::EnableDAC(int iCycle, int iChannel, bool% bEnable)
	{
		bool bEnable2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		bEnable2 = bEnable;
		bRet = m_pHWDeviceOEMPA->EnableDAC(iCycle, iChannel, bEnable2);
		bEnable = bEnable2;
		return bRet;
	}
	bool csDriverOEMPA::csHWDeviceOEMPA::GetEnableDAC(int iCycle, int iChannel, bool* pbEnable)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEnableDAC(iCycle, iChannel, pbEnable);
	}
	bool csHWDeviceOEMPA::CheckDACSlope(double %dTime,float %fSlope)
	{
		double dTime2;
		float fSlope2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dTime2 = dTime;
		fSlope2 = fSlope;
		bRet = m_pHWDeviceOEMPA->CheckDACSlope(dTime2,fSlope2);
		dTime = dTime2;
		fSlope = fSlope2;
		return bRet;
	}
	bool csHWDeviceOEMPA::CheckDACCount(int %iCount)
	{
		int iCount2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		iCount2 = iCount;
		bRet = m_pHWDeviceOEMPA->CheckDACCount(iCount2);
		iCount = iCount2;
		return bRet;
	}

	bool csHWDeviceOEMPA::SetAscanRectification(int iCycle,csEnumRectification %eRectification)
	{
		enumRectification eRectification2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eRectification2 = (enumRectification)eRectification;
		bRet = m_pHWDeviceOEMPA->SetAscanRectification(iCycle,eRectification2);
		eRectification = (csEnumRectification)eRectification2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetAscanRectification(int iCycle,csEnumRectification *peRectification)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetAscanRectification(iCycle,(enumRectification*)peRectification);
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetAscanRectification(int iCycle, int iChannel, csEnumRectification% eRectification)
	{
		enumRectification eRectification2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		eRectification2 = (enumRectification)eRectification;
		bRet = m_pHWDeviceOEMPA->SetAscanRectification(iCycle, iChannel, eRectification2);
		eRectification = (csEnumRectification)eRectification2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetAscanRectification(int iCycle, int iChannel, csEnumRectification* peRectification)
	{
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		bRet = m_pHWDeviceOEMPA->GetAscanRectification(iCycle, iChannel, (enumRectification*)peRectification);
		return bRet;
	}

	bool csHWDeviceOEMPA::SetAscanStart(int iCycle,double %dStart)
	{
		double dStart2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dStart2 = dStart;
		bRet = m_pHWDeviceOEMPA->SetAscanStart(iCycle,dStart2);
		dStart = dStart2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetAscanStart(int iCycle,double *pdStart)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetAscanStart(iCycle,pdStart);
	}
	bool csHWDeviceOEMPA::CheckAscanStart(double %dStart)
	{
		double dStart2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dStart2 = dStart;
		bRet = m_pHWDeviceOEMPA->CheckAscanStart(dStart2);
		dStart = dStart2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetAscanStart(int iCycle, int iChannel, double% dStart)
	{
		double dStart2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		dStart2 = dStart;
		bRet = m_pHWDeviceOEMPA->SetAscanStart(iCycle, iChannel, dStart2);
		dStart = dStart2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetAscanStart(int iCycle, int iChannel, double* pdStart)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetAscanStart(iCycle, iChannel, pdStart);
	}
	
	bool csHWDeviceOEMPA::SetAscanRange(int iCycle,double %dRange,/*output only*/[Out] csEnumCompressionType %eCompressionType,/*output only*/[Out] long %lPointCount,/*output only*/[Out] long %lPointFactor)
	{
		double dRange2;
		enumCompressionType eCompressionType2;
		long lPointCount2,lPointFactor2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dRange2 = dRange;
		eCompressionType2 = (enumCompressionType)eCompressionType;
		bRet = m_pHWDeviceOEMPA->SetAscanRange(iCycle,dRange2,eCompressionType2,lPointCount2,lPointFactor2);
		dRange = dRange2;
		eCompressionType = (csEnumCompressionType)eCompressionType2;
		lPointCount = lPointCount2;
		lPointFactor = lPointFactor2;
		return bRet;
	}
	bool csHWDeviceOEMPA::SetAscanRangeWithFactor(int iCycle,double %dRange,csEnumCompressionType %eCompressionType,/*in/out*/long %lPointFactor,/*output only*/[Out] long %lPointCount)
	{
		double dRange2;
		enumCompressionType eCompressionType2;
		long lPointCount2,lPointFactor2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dRange2 = dRange;
		eCompressionType2 = (enumCompressionType)eCompressionType;
		lPointFactor2 = lPointFactor;
		bRet = m_pHWDeviceOEMPA->SetAscanRangeWithFactor(iCycle,dRange2,eCompressionType2,lPointFactor2,lPointCount2);
		dRange = dRange2;
		eCompressionType = (csEnumCompressionType)eCompressionType2;
		lPointFactor = lPointFactor2;
		lPointCount = lPointCount2;
		return bRet;
	}
	bool csHWDeviceOEMPA::SetAscanRangeWithCount(int iCycle,double %dRange,/*in/out (check)*/csEnumCompressionType %eCompressionType,/*in/out (check)*/long %lPointCount,/*output only*/[Out] long %lPointFactor)
	{
		double dRange2=dRange;
		enumCompressionType eCompressionType2=(enumCompressionType)eCompressionType;
		long lPointCount2=lPointCount,lPointFactor2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eCompressionType2 = (enumCompressionType)eCompressionType;
		bRet = m_pHWDeviceOEMPA->SetAscanRangeWithCount(iCycle,dRange2,eCompressionType2,lPointCount2,lPointFactor2);
		dRange = dRange2;
		eCompressionType = (csEnumCompressionType)eCompressionType2;
		lPointCount = lPointCount2;
		lPointFactor = lPointFactor2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetAscanRange(int iCycle,double *pdRange,csEnumCompressionType *peCompressionType,long *plPointCount,long *plPointFactor)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetAscanRange(iCycle,pdRange,(enumCompressionType*)peCompressionType,plPointCount,plPointFactor);
	}
	bool csHWDeviceOEMPA::CheckAscanRange(double %dRange)
	{
		double dRange2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dRange2 = dRange;
		bRet = m_pHWDeviceOEMPA->CheckAscanRange(dRange2);
		dRange = dRange2;
		return bRet;
	}
	bool csHWDeviceOEMPA::CheckAscanRangeWithCount(double %dRange,csEnumCompressionType %eCompressionType,long %lPointCount)
	{
		double dRange2;
		enumCompressionType eCompressionType2;
		long lPointCount2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eCompressionType2 = (enumCompressionType)eCompressionType;
		bRet = m_pHWDeviceOEMPA->CheckAscanRangeWithCount(dRange2,eCompressionType2,lPointCount2);
		dRange = dRange2;
		eCompressionType = (csEnumCompressionType)eCompressionType2;
		lPointCount = lPointCount2;
		return bRet;
	}
	bool csHWDeviceOEMPA::GetSamplingFrequency(csEnumCompressionType %eCompressionType,long %lPointFactor,[Out] double %dSamplingFrequency/*Hertz*/)
	{
		double dSamplingFrequency2;
		enumCompressionType eCompressionType2;
		long lPointFactor2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eCompressionType2 = (enumCompressionType)eCompressionType;
		lPointFactor2 = (long)lPointFactor;
		bRet = m_pHWDeviceOEMPA->GetSamplingFrequency(eCompressionType2,lPointFactor2,dSamplingFrequency2);
		dSamplingFrequency = dSamplingFrequency2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetAscanRange(int iCycle, int iChannel, double% dRange, csEnumCompressionType% eCompressionType, LONG% lPointCount, LONG% lPointFactor)
	{
		double dRange2;
		enumCompressionType eCompressionType2;
		long lPointCount2, lPointFactor2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		dRange2 = dRange;
		eCompressionType2 = (enumCompressionType)eCompressionType;
		bRet = m_pHWDeviceOEMPA->SetAscanRange(iCycle, iChannel, dRange2, eCompressionType2, lPointCount2, lPointFactor2);
		dRange = dRange2;
		eCompressionType = (csEnumCompressionType)eCompressionType2;
		lPointCount = lPointCount2;
		lPointFactor = lPointFactor2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetAscanRangeWithFactor(int iCycle, int iChannel, double% dRange, csEnumCompressionType% eCompressionType, LONG% lPointFactor, LONG% lPointCount)
	{
		double dRange2;
		enumCompressionType eCompressionType2;
		long lPointCount2, lPointFactor2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		dRange2 = dRange;
		eCompressionType2 = (enumCompressionType)eCompressionType;
		lPointFactor2 = lPointFactor;
		bRet = m_pHWDeviceOEMPA->SetAscanRangeWithFactor(iCycle, iChannel, dRange2, eCompressionType2, lPointFactor2, lPointCount2);
		dRange = dRange2;
		eCompressionType = (csEnumCompressionType)eCompressionType2;
		lPointFactor = lPointFactor2;
		lPointCount = lPointCount2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetAscanRangeWithCount(int iCycle, int iChannel, double% dRange, csEnumCompressionType% eCompressionType, LONG% lPointCount, LONG% lPointFactor)
	{
		double dRange2 = dRange;
		enumCompressionType eCompressionType2 = (enumCompressionType)eCompressionType;
		long lPointCount2 = lPointCount, lPointFactor2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		eCompressionType2 = (enumCompressionType)eCompressionType;
		bRet = m_pHWDeviceOEMPA->SetAscanRangeWithCount(iCycle, iChannel, dRange2, eCompressionType2, lPointCount2, lPointFactor2);
		dRange = dRange2;
		eCompressionType = (csEnumCompressionType)eCompressionType2;
		lPointCount = lPointCount2;
		lPointFactor = lPointFactor2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetAscanRange(int iCycle, int iChannel, double* pdRange, csEnumCompressionType* peCompressionType, LONG* plPointCount, LONG* plPointFactor)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetAscanRange(iCycle, iChannel, pdRange, (enumCompressionType*)peCompressionType, plPointCount, plPointFactor);
	}

	bool csHWDeviceOEMPA::SetFilterIndex(int iCycle,csEnumOEMPAFilterIndex %eFilterIndex)
	{
		enumOEMPAFilterIndex eFilterIndex2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		eFilterIndex2 = (enumOEMPAFilterIndex)eFilterIndex;
		bRet = m_pHWDeviceOEMPA->SetFilterIndex(iCycle,eFilterIndex2);
		eFilterIndex = (csEnumOEMPAFilterIndex)eFilterIndex2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetFilterIndex(int iCycle,csEnumOEMPAFilterIndex *peFilterIndex)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetFilterIndex(iCycle,(enumOEMPAFilterIndex*)peFilterIndex);
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetFilterIndex(int iCycle, int iChannel, csEnumOEMPAFilterIndex% eFilterIndex)
	{
		enumOEMPAFilterIndex eFilterIndex2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		eFilterIndex2 = (enumOEMPAFilterIndex)eFilterIndex;
		bRet = m_pHWDeviceOEMPA->SetFilterIndex(iCycle, iChannel, eFilterIndex2);
		eFilterIndex = (csEnumOEMPAFilterIndex)eFilterIndex2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetFilterIndex(int iCycle, int iChannel, csEnumOEMPAFilterIndex* peFilterIndex)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetFilterIndex(iCycle, iChannel, (enumOEMPAFilterIndex*)peFilterIndex);
	}
		
	bool csHWDeviceOEMPA::SetTimeSlot(int iCycle,double %dTime)
	{
		double dTime2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dTime2 = dTime;
		bRet = m_pHWDeviceOEMPA->SetTimeSlot(iCycle,dTime2);
		dTime = dTime2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetTimeSlot(int iCycle,double *pdTime)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetTimeSlot(iCycle,pdTime);
	}
	bool csHWDeviceOEMPA::CheckTimeSlot(double %dTime)
	{
		double dTime2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dTime2 = dTime;
		bRet = m_pHWDeviceOEMPA->CheckTimeSlot(dTime2);
		dTime = dTime2;
		return bRet;
	}
	double csHWDeviceOEMPA::vf_GetMinTimeSlotRecovery(long lAscanPointCount, enumBitSize eAscanBitSize)
	{
		double dRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dRet = m_pHWDeviceOEMPA->vf_GetMinTimeSlotRecovery(lAscanPointCount, eAscanBitSize);
		return dRet;
	}
	double csHWDeviceOEMPA::GetAscanThroughput(double dTimeSlot, long lPointCount, enumBitSize eAscanBitSize)
	{
		double dRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dRet = m_pHWDeviceOEMPA->GetAscanThroughput(dTimeSlot, lPointCount, eAscanBitSize);
		return dRet;
	}
	//FMC SubTimeSlot management
	bool csHWDeviceOEMPA::SetFMCSubTimeSlotAcqReplay(int iCycle,double dAscanStart,double dAscanRange,double %dTimeSlot)
	{
		double dTimeSlot2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dTimeSlot2 = dTimeSlot;
		bRet = m_pHWDeviceOEMPA->SetFMCSubTimeSlotAcqReplay(iCycle,dAscanStart,dAscanRange,dTimeSlot2);
		dTimeSlot = dTimeSlot2;
		return bRet;
	}
	bool csHWDeviceOEMPA::GetFMCTimeLimitation(double dAscanStart,double dAscanRange,double dTimeSlot,double %dTimeSlotMin,double %dHWAcqSubTimeSlotMin,double %dReplaySubTimeSlotMin,double %dReplaySubTimeSlotOptimizedForThroughput)
	{
		double _dTimeSlotMin,_dHWAcqSubTimeSlotMin,_dReplayAcqSubTimeSlotMin,_dReplayAcqSubTimeSlotOptimizedForThroughput;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		bRet = m_pHWDeviceOEMPA->GetFMCTimeLimitation(dAscanStart,dAscanRange,dTimeSlot,_dTimeSlotMin,_dHWAcqSubTimeSlotMin,_dReplayAcqSubTimeSlotMin,_dReplayAcqSubTimeSlotOptimizedForThroughput);
		dTimeSlotMin = _dTimeSlotMin;
		dHWAcqSubTimeSlotMin = _dHWAcqSubTimeSlotMin;
		dReplaySubTimeSlotMin = _dReplayAcqSubTimeSlotMin;
		dReplaySubTimeSlotOptimizedForThroughput = _dReplayAcqSubTimeSlotOptimizedForThroughput;
		return bRet;
	}
	int csHWDeviceOEMPA::GetFMCSubTimeSlotCount()
	{
		if(!m_pHWDeviceOEMPA)
			return 0;
		return m_pHWDeviceOEMPA->GetFMCSubTimeSlotCount();
	}
	bool csHWDeviceOEMPA::SetFMCSubTimeSlotAcq(int iCycle,double %dSubTimeSlot)
	{
		double dSubTimeSlot2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dSubTimeSlot2 = dSubTimeSlot;
		bRet = m_pHWDeviceOEMPA->SetFMCSubTimeSlotAcq(iCycle,dSubTimeSlot2);
		dSubTimeSlot = dSubTimeSlot2;
		return bRet;
	}
	bool csHWDeviceOEMPA::SetFMCSubTimeSlotReplay(int iCycle,double %dSubTimeSlot)
	{
		double dSubTimeSlot2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dSubTimeSlot2 = dSubTimeSlot;
		bRet = m_pHWDeviceOEMPA->SetFMCSubTimeSlotReplay(iCycle,dSubTimeSlot2);
		dSubTimeSlot = dSubTimeSlot2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetFMCSubTimeSlotAcq(int iCycle,double *pdSubTimeSlot)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetFMCSubTimeSlotAcq(iCycle,pdSubTimeSlot);
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetFMCSubTimeSlotReplay(int iCycle,double *pdSubTimeSlot)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetFMCSubTimeSlotReplay(iCycle,pdSubTimeSlot);
	}

	bool csHWDeviceOEMPA::SetAscanAcqIdChannelProbe(int iCycle,WORD %wID)
	{
		WORD wID2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		wID2 = wID;
		bRet = m_pHWDeviceOEMPA->SetAscanAcqIdChannelProbe(iCycle,wID2);
		wID = wID2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetAscanAcqIdChannelProbe(int iCycle,WORD *pwID)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetAscanAcqIdChannelProbe(iCycle,pwID);
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetAscanAcqIdChannelProbe(int iCycle, int iChannel, WORD% wID)
	{
		WORD wID2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		wID2 = wID;
		bRet = m_pHWDeviceOEMPA->SetAscanAcqIdChannelProbe(iCycle, iChannel, wID2);
		wID = wID2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetAscanAcqIdChannelProbe(int iCycle, int iChannel, WORD* pwID)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetAscanAcqIdChannelProbe(iCycle, iChannel, pwID);
	}
		
	bool csHWDeviceOEMPA::SetAscanAcqIdChannelScan(int iCycle,WORD %wID)
	{
		WORD wID2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		wID2 = wID;
		bRet = m_pHWDeviceOEMPA->SetAscanAcqIdChannelScan(iCycle,wID2);
		wID = wID2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetAscanAcqIdChannelScan(int iCycle,WORD *pwID)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetAscanAcqIdChannelScan(iCycle,pwID);
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetAscanAcqIdChannelScan(int iCycle, int iChannel, WORD% wID)
	{
		WORD wID2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		wID2 = wID;
		bRet = m_pHWDeviceOEMPA->SetAscanAcqIdChannelScan(iCycle, iChannel, wID2);
		wID = wID2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetAscanAcqIdChannelScan(int iCycle, int iChannel, WORD* pwID)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetAscanAcqIdChannelScan(iCycle, iChannel, pwID);
	}
		
	bool csHWDeviceOEMPA::SetAscanAcqIdChannelCycle(int iCycle,WORD %wID)
	{
		WORD wID2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		wID2 = wID;
		bRet = m_pHWDeviceOEMPA->SetAscanAcqIdChannelCycle(iCycle,wID2);
		wID = wID2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetAscanAcqIdChannelCycle(int iCycle,WORD *pwID)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetAscanAcqIdChannelCycle(iCycle,pwID);
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetAscanAcqIdChannelCycle(int iCycle, int iChannel, WORD% wID)
	{
		WORD wID2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		wID2 = wID;
		bRet = m_pHWDeviceOEMPA->SetAscanAcqIdChannelCycle(iCycle, iChannel, wID2);
		wID = wID2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetAscanAcqIdChannelCycle(int iCycle, int iChannel, WORD* pwID)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetAscanAcqIdChannelCycle(iCycle, iChannel, pwID);
	}
		
	bool csHWDeviceOEMPA::EnableAscanMaximum(int iCycle,bool %bEnable)
	{
		bool bEnable2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		bEnable2 = bEnable;
		bRet = m_pHWDeviceOEMPA->EnableAscanMaximum(iCycle,bEnable2);
		bEnable = bEnable2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetEnableAscanMaximum(int iCycle,bool *pbEnable)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEnableAscanMaximum(iCycle,pbEnable);
	}
		
	bool csHWDeviceOEMPA::EnableAscanMinimum(int iCycle,bool %bEnable)
	{
		bool bEnable2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		bEnable2 = bEnable;
		bRet = m_pHWDeviceOEMPA->EnableAscanMinimum(iCycle,bEnable2);
		bEnable = bEnable2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetEnableAscanMinimum(int iCycle,bool *pbEnable)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEnableAscanMinimum(iCycle,pbEnable);
	}
		
	bool csHWDeviceOEMPA::EnableAscanSaturation(int iCycle,bool %bEnable)
	{
		bool bEnable2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		bEnable2 = bEnable;
		bRet = m_pHWDeviceOEMPA->EnableAscanSaturation(iCycle,bEnable2);
		bEnable = bEnable2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetEnableAscanSaturation(int iCycle,bool *pbEnable)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEnableAscanSaturation(iCycle,pbEnable);
	}
		
	bool csHWDeviceOEMPA::SetGateModeThreshold(int iCycle,int iGate,bool %bEnable,csEnumGateModeAmp %eGateModeAmp,csEnumGateModeTof %eGateModeTof,csEnumRectification %eGateRectification,double %dThresholdPercent)
	{
		bool bEnable2;
		double dThresholdPercent2;
		enumGateModeAmp eGateModeAmp2;
		enumGateModeTof eGateModeTof2;
		enumRectification eGateRectification2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		bEnable2 = bEnable;
		dThresholdPercent2 = dThresholdPercent;
		eGateModeAmp2 = (enumGateModeAmp)eGateModeAmp;
		eGateModeTof2 = (enumGateModeTof)eGateModeTof;
		eGateRectification2 = (enumRectification)eGateRectification;
		bRet = m_pHWDeviceOEMPA->SetGateModeThreshold(iCycle,iGate,bEnable2,eGateModeAmp2,eGateModeTof2,eGateRectification2,dThresholdPercent2);
		bEnable = bEnable2;
		dThresholdPercent = dThresholdPercent2;
		eGateModeAmp = (csEnumGateModeAmp)eGateModeAmp2;
		eGateModeTof = (csEnumGateModeTof)eGateModeTof2;
		eGateRectification = (csEnumRectification)eGateRectification2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetGateModeThreshold(int iCycle,int iGate,bool *pbEnable,csEnumGateModeAmp *peGateModeAmp,csEnumGateModeTof *peGateModeTof,csEnumRectification *peGateRectification,double *pdThresholdPercent)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetGateModeThreshold(iCycle,iGate,pbEnable,(enumGateModeAmp*)peGateModeAmp,(enumGateModeTof*)peGateModeTof,(enumRectification*)peGateRectification,pdThresholdPercent);
	}
	bool csHWDeviceOEMPA::CheckGateModeThreshold(bool %bEnable,csEnumGateModeAmp %eGateModeAmp,csEnumGateModeTof %eGateModeTof,csEnumRectification %eGateRectification,double %dThresholdPercent)
	{
		bool bEnable2;
		double dThresholdPercent2;
		enumGateModeAmp eGateModeAmp2;
		enumGateModeTof eGateModeTof2;
		enumRectification eGateRectification2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		bEnable2 = bEnable;
		dThresholdPercent2 = dThresholdPercent;
		eGateModeAmp2 = (enumGateModeAmp)eGateModeAmp;
		eGateModeTof2 = (enumGateModeTof)eGateModeTof;
		eGateRectification2 = (enumRectification)eGateRectification;
		bRet = m_pHWDeviceOEMPA->CheckGateModeThreshold(bEnable2,eGateModeAmp2,eGateModeTof2,eGateRectification2,dThresholdPercent2);
		bEnable = bEnable2;
		dThresholdPercent = dThresholdPercent2;
		eGateModeAmp = (csEnumGateModeAmp)eGateModeAmp2;
		eGateModeTof = (csEnumGateModeTof)eGateModeTof2;
		eGateRectification = (csEnumRectification)eGateRectification2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetGateModeThreshold(int iCycle, int iChannel, int iGate, bool% bEnable, csEnumGateModeAmp% eGateModeAmp, csEnumGateModeTof% eGateModeTof, csEnumRectification% eGateRectification, double% dThresholdPercent)
	{
		bool bEnable2;
		double dThresholdPercent2;
		enumGateModeAmp eGateModeAmp2;
		enumGateModeTof eGateModeTof2;
		enumRectification eGateRectification2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		bEnable2 = bEnable;
		dThresholdPercent2 = dThresholdPercent;
		eGateModeAmp2 = (enumGateModeAmp)eGateModeAmp;
		eGateModeTof2 = (enumGateModeTof)eGateModeTof;
		eGateRectification2 = (enumRectification)eGateRectification;
		bRet = m_pHWDeviceOEMPA->SetGateModeThreshold(iCycle, iChannel, iGate, bEnable2, eGateModeAmp2, eGateModeTof2, eGateRectification2, dThresholdPercent2);
		bEnable = bEnable2;
		dThresholdPercent = dThresholdPercent2;
		eGateModeAmp = (csEnumGateModeAmp)eGateModeAmp2;
		eGateModeTof = (csEnumGateModeTof)eGateModeTof2;
		eGateRectification = (csEnumRectification)eGateRectification2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetGateModeThreshold(int iCycle, int iChannel, int iGate, bool* pbEnable, csEnumGateModeAmp* peGateModeAmp, csEnumGateModeTof* peGateModeTof, csEnumRectification* peGateRectification, double* pdThresholdPercent)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetGateModeThreshold(iCycle, iChannel, iGate, pbEnable, (enumGateModeAmp*)peGateModeAmp, (enumGateModeTof*)peGateModeTof, (enumRectification*)peGateRectification, pdThresholdPercent);
	}
		
	bool csHWDeviceOEMPA::SetGateStart(int iCycle,int iGate,double %dStart)
	{
		double dStart2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dStart2 = dStart;
		bRet = m_pHWDeviceOEMPA->SetGateStart(iCycle,iGate,dStart2);
		dStart = dStart2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetGateStart(int iCycle,int iGate,double *pdStart)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetGateStart(iCycle,iGate,pdStart);
	}
	bool csHWDeviceOEMPA::CheckGateStart(double %dStart)
	{
		double dStart2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dStart2 = dStart;
		bRet = m_pHWDeviceOEMPA->CheckGateStart(dStart2);
		dStart = dStart2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetGateStart(int iCycle, int iChannel, int iGate, double% dStart)
	{
		double dStart2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		dStart2 = dStart;
		bRet = m_pHWDeviceOEMPA->SetGateStart(iCycle, iChannel, iGate, dStart2);
		dStart = dStart2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetGateStart(int iCycle, int iChannel, int iGate, double* pdStart)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetGateStart(iCycle, iChannel, iGate, pdStart);
	}
		
	bool csHWDeviceOEMPA::CheckGateStartStop(double %dStart,double %dStop)
	{
		double dStart2;
		double dStop2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dStop2 = dStop;
		dStart2 = dStart;
		bRet = m_pHWDeviceOEMPA->CheckGateStartStop(dStart2,dStop2);
		dStart = dStart2;
		dStop = dStop2;
		return bRet;
	}
	bool csHWDeviceOEMPA::SetGateStop(int iCycle,int iGate,double %dStop)
	{
		double dStop2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dStop2 = dStop;
		bRet = m_pHWDeviceOEMPA->SetGateStop(iCycle,iGate,dStop2);
		dStop = dStop2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetGateStop(int iCycle, int iGate, double *pdStop)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetGateStop(iCycle, iGate, pdStop, NULL);
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetGateStop(int iCycle,int iGate,double *pdStop,bool *pbWidth)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetGateStop(iCycle,iGate,pdStop,pbWidth);
	}
	bool csHWDeviceOEMPA::CheckGateStop(double %dStop)
	{
		double dStop2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dStop2 = dStop;
		bRet = m_pHWDeviceOEMPA->CheckGateStop(dStop2);
		dStop = dStop2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetGateStop(int iCycle, int iChannel, int iGate, double% dStop)
	{
		double dStop2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		dStop2 = dStop;
		bRet = m_pHWDeviceOEMPA->SetGateStop(iCycle, iChannel, iGate, dStop2);
		dStop = dStop2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetGateStop(int iCycle, int iChannel, int iGate, double* pdStop)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetGateStop(iCycle, iChannel, iGate, pdStop);
	}
		
	bool csHWDeviceOEMPA::SetGateAcqIDAmp(int iDriverId,int iCycle,WORD %wID)
	{
		WORD wID2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		wID2 = wID;
		bRet = m_pHWDeviceOEMPA->SetGateAcqIDAmp(iDriverId,iCycle,wID2);
		wID = wID2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetGateAcqIDAmp(int iDriverId,int iCycle,WORD *pwID)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetGateAcqIDAmp(iDriverId,iCycle,pwID);
	}
	bool csHWDeviceOEMPA::SetGateAcqIDTof(int iDriverId,int iCycle,WORD %wID)
	{
		WORD wID2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		wID2 = wID;
		bRet = m_pHWDeviceOEMPA->SetGateAcqIDTof(iDriverId,iCycle,wID2);
		wID = wID2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetGateAcqIDTof(int iDriverId,int iCycle,WORD *pwID)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetGateAcqIDTof(iDriverId,iCycle,pwID);
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetGateAcqIDAmp(int iCycle, int iChannel, int iGate, WORD% wID)
	{
		WORD wID2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		wID2 = wID;
		bRet = m_pHWDeviceOEMPA->SetGateAcqIDAmp(iCycle, iChannel, iGate, wID2);
		wID = wID2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetGateAcqIDAmp(int iCycle, int iChannel, int iGate, WORD* pwID)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetGateAcqIDAmp(iCycle, iChannel, iGate, pwID);
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetGateAcqIDTof(int iCycle, int iChannel, int iGate, WORD% wID)
	{
		WORD wID2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		wID2 = wID;
		bRet = m_pHWDeviceOEMPA->SetGateAcqIDTof(iCycle, iChannel, iGate, wID2);
		wID = wID2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetGateAcqIDTof(int iCycle, int iChannel, int iGate, WORD* pwID)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetGateAcqIDTof(iCycle, iChannel, iGate, pwID);
	}

	bool csHWDeviceOEMPA::SetTrackingGateStart(int iCycle,int iGate,bool bEnable,int %iTrackingCycleIndex,int %iTrackingGateIndex)
	{
		int iTrackingCycleIndex2,iTrackingGateIndex2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		iTrackingCycleIndex2 = iTrackingCycleIndex;
		iTrackingGateIndex2 = iTrackingGateIndex;
		bRet = m_pHWDeviceOEMPA->SetTrackingGateStart(iCycle,iGate,bEnable,iTrackingCycleIndex2,iTrackingGateIndex2);
		iTrackingCycleIndex = iTrackingCycleIndex2;
		iTrackingGateIndex = iTrackingGateIndex2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetTrackingGateStart(int iCycle,int iGate,bool *pbEnable,int *piTrackingCycleIndex,int *piTrackingGateIndex)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetTrackingGateStart(iCycle,iGate,pbEnable,piTrackingCycleIndex,piTrackingGateIndex);
	}
	bool csHWDeviceOEMPA::SetTrackingGateStop(int iCycle,int iGate,bool bEnable,int %iTrackingCycleIndex,int %iTrackingGateIndex)
	{
		int iTrackingCycleIndex2,iTrackingGateIndex2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		iTrackingCycleIndex2 = iTrackingCycleIndex;
		iTrackingGateIndex2 = iTrackingGateIndex;
		bRet = m_pHWDeviceOEMPA->SetTrackingGateStop(iCycle,iGate,bEnable,iTrackingCycleIndex2,iTrackingGateIndex2);
		iTrackingCycleIndex = iTrackingCycleIndex2;
		iTrackingGateIndex = iTrackingGateIndex2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetTrackingGateStop(int iCycle,int iGate,bool *pbEnable,int *piTrackingCycleIndex,int *piTrackingGateIndex)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetTrackingGateStop(iCycle,iGate,pbEnable,piTrackingCycleIndex,piTrackingGateIndex);
	}
	bool csHWDeviceOEMPA::SetTrackingAscan(int iCycle,bool bEnable,int %iTrackingCycleIndex,int %iTrackingGateIndex)
	{
		int iTrackingCycleIndex2,iTrackingGateIndex2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		iTrackingCycleIndex2 = iTrackingCycleIndex;
		iTrackingGateIndex2 = iTrackingGateIndex;
		bRet = m_pHWDeviceOEMPA->SetTrackingAscan(iCycle,bEnable,iTrackingCycleIndex2,iTrackingGateIndex2);
		iTrackingCycleIndex = iTrackingCycleIndex2;
		iTrackingGateIndex = iTrackingGateIndex2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetTrackingAscan(int iCycle,bool *pbEnable,int *piTrackingCycleIndex,int *piTrackingGateIndex)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetTrackingAscan(iCycle,pbEnable,piTrackingCycleIndex,piTrackingGateIndex);
	}
	bool csHWDeviceOEMPA::SetTrackingDac(int iCycle,bool bEnable,int %iTrackingCycleIndex,int %iTrackingGateIndex)
	{
		int iTrackingCycleIndex2,iTrackingGateIndex2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		iTrackingCycleIndex2 = iTrackingCycleIndex;
		iTrackingGateIndex2 = iTrackingGateIndex;
		bRet = m_pHWDeviceOEMPA->SetTrackingDac(iCycle,bEnable,iTrackingCycleIndex2,iTrackingGateIndex2);
		iTrackingCycleIndex = iTrackingCycleIndex2;
		iTrackingGateIndex = iTrackingGateIndex2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetTrackingDac(int iCycle,bool *pbEnable,int *piTrackingCycleIndex,int *piTrackingGateIndex)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetTrackingDac(iCycle,pbEnable,piTrackingCycleIndex,piTrackingGateIndex);
	}
	bool csHWDeviceOEMPA::CheckTracking(bool bEnable,int %iTrackingCycleIndex,int %iTrackingGateIndex)
	{
		int iTrackingCycleIndex2,iTrackingGateIndex2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		iTrackingCycleIndex2 = iTrackingCycleIndex;
		iTrackingGateIndex2 = iTrackingGateIndex;
		bRet = m_pHWDeviceOEMPA->CheckTracking(bEnable,iTrackingCycleIndex2,iTrackingGateIndex2);
		iTrackingCycleIndex = iTrackingCycleIndex2;
		iTrackingGateIndex = iTrackingGateIndex2;
		return bRet;
	}
	bool csHWDeviceOEMPA::ResetTrackingTable()//this is called automatically when the IF tracking of one gate is updated.
	{
		return m_pHWDeviceOEMPA->ResetTrackingTable();
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetTrackingGateStart(int iCycle, int iChannel, int iGate, bool bEnable, int% iTrackingCycleIndex, int% iTrackingChannelIndex, int% iTrackingGateIndex)
	{
		int iTrackingCycleIndex2, iTrackingChannelIndex2, iTrackingGateIndex2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		iTrackingCycleIndex2 = iTrackingCycleIndex;
		iTrackingChannelIndex2 = iTrackingChannelIndex;
		iTrackingGateIndex2 = iTrackingGateIndex;
		bRet = m_pHWDeviceOEMPA->SetTrackingGateStart(iCycle, iChannel, iGate, bEnable, iTrackingCycleIndex2, iTrackingChannelIndex2, iTrackingGateIndex2);
		iTrackingCycleIndex = iTrackingCycleIndex2;
		iTrackingChannelIndex = iTrackingChannelIndex2;
		iTrackingGateIndex = iTrackingGateIndex2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetTrackingGateStart(int iCycle, int iChannel, int iGate, bool* pbEnable, int* piTrackingCycleIndex, int* piTrackingChannelIndex, int* piTrackingGateIndex)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetTrackingGateStart(iCycle, iChannel, iGate, pbEnable, piTrackingCycleIndex, piTrackingChannelIndex, piTrackingGateIndex);
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetTrackingGateStop(int iCycle, int iChannel, int iGate, bool bEnable, int% iTrackingCycleIndex, int% iTrackingChannelIndex, int% iTrackingGateIndex)
	{
		int iTrackingCycleIndex2, iTrackingChannelIndex2, iTrackingGateIndex2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		iTrackingCycleIndex2 = iTrackingCycleIndex;
		iTrackingChannelIndex2 = iTrackingChannelIndex;
		iTrackingGateIndex2 = iTrackingGateIndex;
		bRet = m_pHWDeviceOEMPA->SetTrackingGateStop(iCycle, iChannel, iGate, bEnable, iTrackingCycleIndex2, iTrackingChannelIndex2, iTrackingGateIndex2);
		iTrackingCycleIndex = iTrackingCycleIndex2;
		iTrackingChannelIndex = iTrackingChannelIndex2;
		iTrackingGateIndex = iTrackingGateIndex2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetTrackingGateStop(int iCycle, int iChannel, int iGate, bool* pbEnable, int* piTrackingCycleIndex, int* piTrackingChannelIndex, int* piTrackingGateIndex)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetTrackingGateStop(iCycle, iChannel, iGate, pbEnable, piTrackingCycleIndex, piTrackingChannelIndex, piTrackingGateIndex);
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetTrackingAscan(int iCycle, int iChannel, bool bEnable, int% iTrackingCycleIndex, int% iTrackingChannelIndex, int% iTrackingGateIndex)
	{
		int iTrackingCycleIndex2, iTrackingChannelIndex2, iTrackingGateIndex2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		iTrackingCycleIndex2 = iTrackingCycleIndex;
		iTrackingChannelIndex2 = iTrackingChannelIndex;
		iTrackingGateIndex2 = iTrackingGateIndex;
		bRet = m_pHWDeviceOEMPA->SetTrackingAscan(iCycle, iChannel, bEnable, iTrackingCycleIndex2, iTrackingChannelIndex2, iTrackingGateIndex2);
		iTrackingCycleIndex = iTrackingCycleIndex2;
		iTrackingChannelIndex = iTrackingChannelIndex2;
		iTrackingGateIndex = iTrackingGateIndex2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetTrackingAscan(int iCycle, int iChannel, bool* pbEnable, int* piTrackingCycleIndex, int* piTrackingChannelIndex, int* piTrackingGateIndex)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetTrackingAscan(iCycle, iChannel, pbEnable, piTrackingCycleIndex, piTrackingChannelIndex, piTrackingGateIndex);
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetTrackingDac(int iCycle, int iChannel, bool bEnable, int% iTrackingCycleIndex, int% iTrackingChannelIndex, int% iTrackingGateIndex)
	{
		int iTrackingCycleIndex2, iTrackingChannelIndex2, iTrackingGateIndex2;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		iTrackingCycleIndex2 = iTrackingCycleIndex;
		iTrackingChannelIndex2 = iTrackingChannelIndex;
		iTrackingGateIndex2 = iTrackingGateIndex;
		bRet = m_pHWDeviceOEMPA->SetTrackingDac(iCycle, iChannel, bEnable, iTrackingCycleIndex2, iTrackingChannelIndex2, iTrackingGateIndex2);
		iTrackingCycleIndex = iTrackingCycleIndex2;
		iTrackingChannelIndex = iTrackingChannelIndex2;
		iTrackingGateIndex = iTrackingGateIndex2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetTrackingDac(int iCycle, int iChannel, bool* pbEnable, int* piTrackingCycleIndex, int* piTrackingChannelIndex, int* piTrackingGateIndex)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetTrackingDac(iCycle, iChannel, pbEnable, piTrackingCycleIndex, piTrackingChannelIndex, piTrackingGateIndex);
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetHighVoltageBipolar(int% positive, int% negative)
	{
		int positive_, negative_;
		bool ret;

		if (!m_pHWDeviceOEMPA)
			return false;
		positive_ = positive;
		negative_ = negative;
		ret = m_pHWDeviceOEMPA->SetHighVoltageBipolar(positive_, negative_);
		positive = positive_;
		negative = negative_;
		return ret;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetHighVoltageBipolar(int* positive, int* negative)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetHighVoltageBipolar(positive, negative);
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetHighVoltage(int% voltage)
	{
		int voltage_;
		bool ret;

		if (!m_pHWDeviceOEMPA)
			return false;
		voltage_ = voltage;
		ret = m_pHWDeviceOEMPA->SetHighVoltage(voltage_);
		voltage = voltage_;
		return ret;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetHighVoltage(int* voltage)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetHighVoltage(voltage);
	}

	bool csHWDeviceOEMPA::SetGainAnalog(int iCycle,float %fGain)
	{
		float fGain2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		fGain2 = fGain;
		bRet = m_pHWDeviceOEMPA->SetGainAnalog(iCycle,fGain2);
		fGain = fGain2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetGainAnalog(int iCycle,float *pfGain)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetGainAnalog(iCycle,pfGain);
	}
	bool csHWDeviceOEMPA::CheckGainAnalog(float %fGain)
	{
		float fGain2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		fGain2 = fGain;
		bRet = m_pHWDeviceOEMPA->CheckGainAnalog(fGain2);
		fGain = fGain2;
		return bRet;
	}

	bool csHWDeviceOEMPA::SetEmissionWedgeDelay(int iCycle,int iCycleCount,double %dWedgeDelay)
	{
		double dWedgeDelay2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dWedgeDelay2 = dWedgeDelay;
		bRet = m_pHWDeviceOEMPA->SetEmissionWedgeDelay(iCycle,iCycleCount,dWedgeDelay2);
		dWedgeDelay = dWedgeDelay2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetEmissionWedgeDelay(int iCycle,int iCountMax,double *pdWedgeDelay)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEmissionWedgeDelay(iCycle,iCountMax,pdWedgeDelay);
	}
	bool csHWDeviceOEMPA::SetReceptionWedgeDelay(int iCycle,int iCycleCount,double %dWedgeDelay)
	{
		double dWedgeDelay2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dWedgeDelay2 = dWedgeDelay;
		bRet = m_pHWDeviceOEMPA->SetReceptionWedgeDelay(iCycle,iCycleCount,dWedgeDelay2);
		dWedgeDelay = dWedgeDelay2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetReceptionWedgeDelay(int iCycle,int iCountMax,double *pdWedgeDelay)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetReceptionWedgeDelay(iCycle,iCountMax,pdWedgeDelay);
	}
	bool csHWDeviceOEMPA::CheckWedgeDelay(double %dWedgeDelay)
	{
		double dWedgeDelay2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dWedgeDelay2 = dWedgeDelay;
		bRet = m_pHWDeviceOEMPA->CheckWedgeDelay(dWedgeDelay2);
		dWedgeDelay = dWedgeDelay2;
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetEmissionWedgeDelaySingleChannel(int iCycle, int iChannel, double% dWedgeDelay)
	{
		double wedge;
		bool ret;
		if (!m_pHWDeviceOEMPA)
			return false;

		wedge = dWedgeDelay;
		ret = m_pHWDeviceOEMPA->SetEmissionWedgeDelaySingleChannel(iCycle, iChannel, wedge);
		dWedgeDelay = wedge;
		return ret;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetEmissionWedgeDelaySingleChannel(int iCycle, int iChannel, double* pdWedgeDelay)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEmissionWedgeDelaySingleChannel(iCycle, iChannel, pdWedgeDelay);
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetReceptionWedgeDelaySingleChannel(int iCycle, int iChannel, double% dWedgeDelay)
	{
		double wedge;
		bool ret;
		if (!m_pHWDeviceOEMPA)
			return false;

		wedge = dWedgeDelay;
		ret = m_pHWDeviceOEMPA->SetReceptionWedgeDelaySingleChannel(iCycle, iChannel, wedge);
		dWedgeDelay = wedge;
		return ret;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetReceptionWedgeDelaySingleChannel(int iCycle, int iChannel, double* pdWedgeDelay)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetReceptionWedgeDelaySingleChannel(iCycle, iChannel, pdWedgeDelay);
	}

	bool csHWDeviceOEMPA::SetAllElementEnable(bool bEnable,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/)
	{
		DWORD adwHWAperture2[g_iOEMPAApertureDWordCount];
		bool bRet;

		if((adwHWAperture==nullptr) || (adwHWAperture->Length!=g_iOEMPAApertureDWordCount))
			adwHWAperture = gcnew cli::array<DWORD>(g_iOEMPAApertureDWordCount);
#ifndef _DRIVER_11XY_
		bRet = CHWDeviceOEMPA::SetAllElementEnable(bEnable,adwHWAperture->Length,adwHWAperture2);
#else //_DRIVER_11XY_
		bRet = CHWDeviceOEMPA::SetAllElementEnable(bEnable,adwHWAperture2);
#endif //_DRIVER_11XY_
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
			adwHWAperture[i] = adwHWAperture2[i];
		return bRet;
	}
	bool csHWDeviceOEMPA::SetElementEnable(int iElement,bool bEnable,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/)
	{
		DWORD adwHWAperture2[g_iOEMPAApertureDWordCount];
		bool bRet;

		if((adwHWAperture==nullptr) || (adwHWAperture->Length!=g_iOEMPAApertureDWordCount))
			return false;
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
			adwHWAperture2[i] = adwHWAperture[i];
#ifndef _DRIVER_11XY_
		bRet = CHWDeviceOEMPA::SetElementEnable(iElement,bEnable,adwHWAperture->Length,adwHWAperture2);
#else //_DRIVER_11XY_
		bRet = CHWDeviceOEMPA::SetElementEnable(iElement,bEnable,adwHWAperture2);
#endif //_DRIVER_11XY_
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
			adwHWAperture[i] = adwHWAperture2[i];
		return bRet;
	}
	bool csHWDeviceOEMPA::GetElementEnable(int iElement,/*const*/ cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/,bool %bEnable)
	{
		DWORD adwHWAperture2[g_iOEMPAApertureDWordCount];
		bool bEnable2;
		bool bRet;

		if((adwHWAperture==nullptr) || (adwHWAperture->Length!=g_iOEMPAApertureDWordCount))
			return false;
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
			adwHWAperture2[i] = adwHWAperture[i];
#ifndef _DRIVER_11XY_
		bRet = CHWDeviceOEMPA::GetElementEnable(iElement,adwHWAperture->Length,adwHWAperture2,bEnable2);
#else //_DRIVER_11XY_
		bRet = CHWDeviceOEMPA::GetElementEnable(iElement,adwHWAperture2,bEnable2);
#endif //_DRIVER_11XY_
		bEnable = bEnable2;
		return bRet;
	}

	bool csHWDeviceOEMPA::IsMultiplexer()//this function can be used to know if a multiplexer (16:128 or 32:128) is included in the system.
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->IsMultiplexer();
	}
	//Case of a system with a multiplexer (16:128 or 32:128), please use following functions:
	//	in this case same aperture is used for emission and reception
	bool csHWDeviceOEMPA::SetMultiplexerEnable(int iCycle,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/)
	{
		DWORD adwHWAperture2[g_iOEMPAApertureDWordCount];
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((adwHWAperture==nullptr) || (adwHWAperture->Length!=g_iOEMPAApertureDWordCount))
			return false;
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
			adwHWAperture2[i] = adwHWAperture[i];
		bRet = m_pHWDeviceOEMPA->SetMultiplexerEnable(iCycle,adwHWAperture2);
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
			adwHWAperture[i] = adwHWAperture2[i];
		return bRet;
	}
	bool csHWDeviceOEMPA::GetMultiplexerEnable(int iCycle,/*fixed*/[Out] cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/)
	{
		DWORD *pwValue;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		adwHWAperture = gcnew cli::array<DWORD>(g_iOEMPAApertureDWordCount);
		pwValue = (DWORD*)(void*)ListAddObject(adwHWAperture);
		bRet = m_pHWDeviceOEMPA->GetMultiplexerEnable(iCycle,pwValue);
		return bRet;
	}
	bool csHWDeviceOEMPA::SetMultiplexerEnable(int iCycle,cli::array<DWORD>^ %adwHWApertureEmission/*[g_iOEMPAApertureDWordCount]*/,cli::array<DWORD>^ %adwHWApertureReception/*[g_iOEMPAApertureDWordCount]*/)
	{
		DWORD adwHWApertureE2[g_iOEMPAApertureDWordCount];
		DWORD adwHWApertureR2[g_iOEMPAApertureDWordCount];
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((adwHWApertureEmission==nullptr) || (adwHWApertureEmission->Length!=g_iOEMPAApertureDWordCount))
			return false;
		if((adwHWApertureReception==nullptr) || (adwHWApertureReception->Length!=g_iOEMPAApertureDWordCount))
			return false;
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
		{
			adwHWApertureE2[i] = adwHWApertureEmission[i];
			adwHWApertureR2[i] = adwHWApertureReception[i];
		}
		bRet = m_pHWDeviceOEMPA->SetMultiplexerEnable(iCycle,adwHWApertureE2,adwHWApertureR2);
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
		{
			adwHWApertureEmission[i] = adwHWApertureE2[i];
			adwHWApertureReception[i] = adwHWApertureR2[i];
		}
		return bRet;
	}
	bool csHWDeviceOEMPA::GetMultiplexerEnable(int iCycle,/*fixed*/[Out] cli::array<DWORD>^ %adwHWApertureEmission/*[g_iOEMPAApertureDWordCount]*/,/*fixed*/[Out] cli::array<DWORD>^ %adwHWApertureReception/*[g_iOEMPAApertureDWordCount]*/)
	{
		DWORD *pwValueE;
		DWORD *pwValueR;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		adwHWApertureEmission = gcnew cli::array<DWORD>(g_iOEMPAApertureDWordCount);
		if((adwHWApertureEmission==nullptr) || (adwHWApertureEmission->Length!=g_iOEMPAApertureDWordCount))
			return false;
		adwHWApertureReception = gcnew cli::array<DWORD>(g_iOEMPAApertureDWordCount);
		if((adwHWApertureReception==nullptr) || (adwHWApertureReception->Length!=g_iOEMPAApertureDWordCount))
			return false;
		pwValueE = (DWORD*)(void*)ListAddObject(adwHWApertureEmission);
		pwValueR = (DWORD*)(void*)ListAddObject(adwHWApertureReception);
		bRet = m_pHWDeviceOEMPA->GetMultiplexerEnable(iCycle,pwValueE,pwValueR);
		return bRet;
	}
	bool csHWDeviceOEMPA::CheckMultiplexerAperture(cli::array<DWORD>^ %adwHWApertureEmission/*[g_iOEMPAApertureDWordCount]*/,cli::array<DWORD>^ % adwHWApertureReception/*[g_iOEMPAApertureDWordCount]*/)
	{
		DWORD adwHWApertureE2[g_iOEMPAApertureDWordCount];
		DWORD adwHWApertureR2[g_iOEMPAApertureDWordCount];
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((adwHWApertureEmission==nullptr) || (adwHWApertureEmission->Length!=g_iOEMPAApertureDWordCount))
			return false;
		if((adwHWApertureReception==nullptr) || (adwHWApertureReception->Length!=g_iOEMPAApertureDWordCount))
			return false;
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
		{
			adwHWApertureE2[i] = adwHWApertureEmission[i];
			adwHWApertureR2[i] = adwHWApertureReception[i];
		}
		bRet = m_pHWDeviceOEMPA->CheckMultiplexerAperture(adwHWApertureE2,adwHWApertureR2);
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
		{
			adwHWApertureEmission[i] = adwHWApertureE2[i];
			adwHWApertureReception[i] = adwHWApertureR2[i];
		}
		return bRet;
	}
	//Case of a system without a multiplexer (16:128 or 32:128), please use following functions:
	bool csHWDeviceOEMPA::SetEmissionEnable(int iCycle,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/)
	{
		DWORD adwHWAperture2[g_iOEMPAApertureDWordCount];
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((adwHWAperture==nullptr) || (adwHWAperture->Length!=g_iOEMPAApertureDWordCount))
			return false;
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
			adwHWAperture2[i] = adwHWAperture[i];
		bRet = m_pHWDeviceOEMPA->SetEmissionEnable(iCycle,adwHWAperture2);
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
			adwHWAperture[i] = adwHWAperture2[i];
		return bRet;
	}
	bool csHWDeviceOEMPA::GetEmissionEnable(int iCycle,/*fixed*/[Out] cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/)
	{
		DWORD *pwValue;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		adwHWAperture = gcnew cli::array<DWORD>(g_iOEMPAApertureDWordCount);
		pwValue = (DWORD*)(void*)ListAddObject(adwHWAperture);
		bRet = m_pHWDeviceOEMPA->GetEmissionEnable(iCycle,pwValue);
		return bRet;
	}
	bool csHWDeviceOEMPA::SetReceptionEnable(int iCycle,cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/)
	{
		DWORD adwHWAperture2[g_iOEMPAApertureDWordCount];
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((adwHWAperture==nullptr) || (adwHWAperture->Length!=g_iOEMPAApertureDWordCount))
			return false;
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
			adwHWAperture2[i] = adwHWAperture[i];
		bRet = m_pHWDeviceOEMPA->SetReceptionEnable(iCycle,adwHWAperture2);
		for(int i=0;i<g_iOEMPAApertureDWordCount;i++)
			adwHWAperture[i] = adwHWAperture2[i];
		return bRet;
	}
	bool csHWDeviceOEMPA::GetReceptionEnable(int iCycle,/*fixed*/[Out] cli::array<DWORD>^ %adwHWAperture/*[g_iOEMPAApertureDWordCount]*/)
	{
		DWORD *pwValue;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		adwHWAperture = gcnew cli::array<DWORD>(g_iOEMPAApertureDWordCount);
		pwValue = (DWORD*)(void*)ListAddObject(adwHWAperture);
		bRet = m_pHWDeviceOEMPA->GetReceptionEnable(iCycle,pwValue);
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetEmissionEnableBipolar(int iCycle, cli::array<DWORD>^% adwHWAperture)
	{
		DWORD adwHWAperture2[g_iOEMPAApertureDWordCount];
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		if ((adwHWAperture == nullptr) || (adwHWAperture->Length != g_iOEMPAApertureDWordCount))
			return false;
		for (int i = 0; i < g_iOEMPAApertureDWordCount; i++)
			adwHWAperture2[i] = adwHWAperture[i];
		bRet = m_pHWDeviceOEMPA->SetEmissionEnableBipolar(iCycle, adwHWAperture2);
		for (int i = 0; i < g_iOEMPAApertureDWordCount; i++)
			adwHWAperture[i] = adwHWAperture2[i];
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetEmissionEnableBipolar(int iCycle, cli::array<DWORD>^% adwHWAperture)
	{
		DWORD* pwValue;
		bool bRet;

		if (!m_pHWDeviceOEMPA)
			return false;
		adwHWAperture = gcnew cli::array<DWORD>(g_iOEMPAApertureDWordCount);
		pwValue = (DWORD*)(void*)ListAddObject(adwHWAperture);
		bRet = m_pHWDeviceOEMPA->GetEmissionEnableBipolar(iCycle, pwValue);
		return bRet;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::SetEmissionBipolarPulse(int iCycle, int iChannel, int% iPulseCount, float% fPulseWidth, float% fPulsePeriod)
	{
		int count;
		float width, period;
		bool ret;

		if (!m_pHWDeviceOEMPA)
			return false;
		count = iPulseCount;
		width = fPulseWidth;
		period = fPulsePeriod;
		ret = m_pHWDeviceOEMPA->SetEmissionBipolarPulse(iCycle, iChannel, count, width, period);
		iPulseCount = count;
		fPulseWidth = width;
		fPulsePeriod = period;
		return ret;
	}

	bool csDriverOEMPA::csHWDeviceOEMPA::GetEmissionBipolarPulse(int iCycle, int iChannel, int* iPulseCount, float* fPulseWidth, float* fPulsePeriod)
	{
		if (!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEmissionBipolarPulse(iCycle, iChannel, iPulseCount, fPulseWidth, fPulsePeriod);
	}

	bool csHWDeviceOEMPA::SetReceptionFocusing(int iCycle,acsDouble^ %adFocalTof)
	{
		structCallbackArrayDouble1D callback;
		bool bRet;
		int iFocalCount;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((adFocalTof!=nullptr) && (adFocalTof->list!=nullptr) && (adFocalTof->list->Length>g_iOEMPAFocalCountMax))
			return false;
		if((adFocalTof==nullptr) || (adFocalTof->list==nullptr))
		{
			iFocalCount = 0;
			bRet = m_pHWDeviceOEMPA->SetReceptionFocusing(iCycle,iFocalCount,NULL);
			return bRet;
		}
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)adFocalTof->GetGcroot();//pdac;
		callback.pSetSize = gCallbackSetSizeDouble;
		callback.pSetData = gCallbackSetDataDouble;
		callback.pGetSize = gCallbackGetSizeDouble;
		callback.pGetData = gCallbackGetDataDouble;
		bRet = m_pHWDeviceOEMPA->SetReceptionFocusing(iCycle,callback);
		return bRet;
	}
	bool csHWDeviceOEMPA::GetReceptionFocusing(int iCycle,/*fixed*/[Out] acsDouble^ %adFocalTof)
	{
		structCallbackArrayDouble1D callback;
		int iMax=g_iOEMPAApertureElementCountMax;
		bool bRet;
		gcroot<acsDouble^> *pTof;

		if(!m_pHWDeviceOEMPA)
			return false;
		if (iCycle >= 4096)
			return false;
		if(adFocalTof==nullptr)
			adFocalTof = gcnew acsDouble;
		pTof = adFocalTof->GetGcroot();
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)pTof;
		callback.pSetSize = gCallbackSetSizeDouble;
		callback.pSetData = gCallbackSetDataDouble;
		callback.pGetSize = gCallbackGetSizeDouble;
		callback.pGetData = gCallbackGetDataDouble;
		bRet = m_pHWDeviceOEMPA->GetReceptionFocusing(iCycle,iMax, callback);

		/*g_callback[iCycle].apParameter[0] = m_pointer;
		g_callback[iCycle].apParameter[1] = (void*)pTof;
		g_callback[iCycle].pSetSize = gCallbackSetSizeDouble;
		g_callback[iCycle].pSetData = gCallbackSetDataDouble;
		g_callback[iCycle].pGetSize = gCallbackGetSizeDouble;
		g_callback[iCycle].pGetData = gCallbackGetDataDouble;
		bRet = m_pHWDeviceOEMPA->GetReceptionFocusing(iCycle, iMax, g_callback[iCycle]);*/

		return bRet;
	}
	bool csHWDeviceOEMPA::SetReceptionFocusing(int iCycle,acsDouble^ %adFocalTof,float %fCenterDelayE,float %fCenterDelayR)
	{
		structCallbackArrayDouble1D callback;
		bool bRet;
		int iFocalCount;
		float _fCenterDelayE,_fCenterDelayR;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((adFocalTof!=nullptr) && (adFocalTof->list!=nullptr) && (adFocalTof->list->Length>g_iOEMPAFocalCountMax))
			return false;
		if((adFocalTof==nullptr) || (adFocalTof->list==nullptr))
		{
			iFocalCount = 0;
			bRet = m_pHWDeviceOEMPA->SetReceptionFocusing(iCycle,iFocalCount,NULL);
			return bRet;
		}
		_fCenterDelayE = fCenterDelayE;
		_fCenterDelayR = fCenterDelayR;
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)adFocalTof->GetGcroot();//pdac;
		callback.pSetSize = gCallbackSetSizeDouble;
		callback.pSetData = gCallbackSetDataDouble;
		callback.pGetSize = gCallbackGetSizeDouble;
		callback.pGetData = gCallbackGetDataDouble;
		bRet = m_pHWDeviceOEMPA->SetReceptionFocusing(iCycle,callback,_fCenterDelayE,_fCenterDelayR);
		fCenterDelayE = _fCenterDelayE;
		fCenterDelayR = _fCenterDelayR;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetReceptionFocusing(int iCycle,/*fixed*/[Out] acsDouble^ %adFocalTof,[Out] float *pfCenterDelayE,[Out] float *pfCenterDelayR)
	{
		structCallbackArrayDouble1D callback;
		int iMax=g_iOEMPAApertureElementCountMax;
		bool bRet;
		gcroot<acsDouble^> *pTof;

		if(!m_pHWDeviceOEMPA)
			return false;
		if (iCycle >= 4096)
			return false;
		if(adFocalTof==nullptr)
			adFocalTof = gcnew acsDouble;
		pTof = adFocalTof->GetGcroot();
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)pTof;
		callback.pSetSize = gCallbackSetSizeDouble;
		callback.pSetData = gCallbackSetDataDouble;
		callback.pGetSize = gCallbackGetSizeDouble;
		callback.pGetData = gCallbackGetDataDouble;
		bRet = m_pHWDeviceOEMPA->GetReceptionFocusing(iCycle,iMax, callback,pfCenterDelayE,pfCenterDelayR);

		/*g_callback[iCycle].apParameter[0] = m_pointer;
		g_callback[iCycle].apParameter[1] = (void*)pTof;
		g_callback[iCycle].pSetSize = gCallbackSetSizeDouble;
		g_callback[iCycle].pSetData = gCallbackSetDataDouble;
		g_callback[iCycle].pGetSize = gCallbackGetSizeDouble;
		g_callback[iCycle].pGetData = gCallbackGetDataDouble;
		bRet = m_pHWDeviceOEMPA->GetReceptionFocusing(iCycle, iMax, g_callback[iCycle], pfCenterDelayE, pfCenterDelayR);*/

		return bRet;
	}
	bool csHWDeviceOEMPA::EnableDDF(int iCycle,bool %bEnable)
	{
		bool bEnable2;
		bool bRet;
		//int %iFocalCount,double *pdFocalTof

		if(!m_pHWDeviceOEMPA)
			return false;
		bEnable2 = bEnable;
		bRet = m_pHWDeviceOEMPA->EnableDDF(iCycle,bEnable2);
		bEnable = bEnable2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetEnableDDF(int iCycle,bool *pbEnable)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEnableDDF(iCycle,pbEnable);
	}
	bool csHWDeviceOEMPA::EnableDDF(int iCycle,csEnumFocusing %eFocusing)
	{
		enumFocusing eFocusing2;
		bool bRet;
		//int %iFocalCount,double *pdFocalTof

		if(!m_pHWDeviceOEMPA)
			return false;
		eFocusing2 = (enumFocusing)eFocusing;
		bRet = m_pHWDeviceOEMPA->EnableDDF(iCycle,eFocusing2);
		eFocusing = (csEnumFocusing)eFocusing2;
		return bRet;
	}
	bool csHWDeviceOEMPA::GetEnableDDF(int iCycle,csEnumFocusing *peFocusing)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEnableDDF(iCycle,(enumFocusing*)peFocusing);
	}
	void csHWDeviceOEMPA::SetDDFTimeOfFlightMiddle(bool bEnable)
	{
		CHWDeviceOEMPA::SetDDFTimeOfFlightMiddle(bEnable);
	}
	bool csHWDeviceOEMPA::IsDDFTimeOfFlightMiddle()
	{
		return CHWDeviceOEMPA::IsDDFTimeOfFlightMiddle();
	}
	void csHWDeviceOEMPA::SetDDFWaveTrackingCorrection(int iEnable)
	{
		CHWDeviceOEMPA::SetDDFWaveTrackingCorrection(iEnable);
	}
	int csHWDeviceOEMPA::GetDDFWaveTrackingCorrection()
	{
		return CHWDeviceOEMPA::GetDDFWaveTrackingCorrection();
	}
	void csHWDeviceOEMPA::SetFMCReceptionSimplified(bool bEnable)
	{
		CHWDeviceOEMPA::SetFMCReceptionSimplified(bEnable);
	}
	bool csHWDeviceOEMPA::IsFMCReceptionSimplified()
	{
		return CHWDeviceOEMPA::IsFMCReceptionSimplified();
	}
	bool csHWDeviceOEMPA::CheckFocalTimeOfFlight(double %dDelay)
	{
		double dDelay2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dDelay2 = dDelay;
		bRet = m_pHWDeviceOEMPA->CheckFocalTimeOfFlight(dDelay2);
		dDelay = dDelay2;
		return bRet;
	}
	bool csHWDeviceOEMPA::CheckEmissionWidth(float %fWidth)
	{
		float fWidth2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		fWidth2 = fWidth;
		bRet = m_pHWDeviceOEMPA->CheckEmissionWidth(fWidth2);
		fWidth = fWidth2;
		return bRet;
	}
	bool csHWDeviceOEMPA::CheckReceptionGain(float %fGain)
	{
		float fGain2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		fGain2 = fGain;
		bRet = m_pHWDeviceOEMPA->CheckReceptionGain(fGain2);
		fGain = fGain2;
		return bRet;
	}
	bool csHWDeviceOEMPA::CheckEmissionDelay(float %fDelay)
	{
		float fDelay2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		fDelay2 = fDelay;
		bRet = m_pHWDeviceOEMPA->CheckEmissionDelay(fDelay2);
		fDelay = fDelay2;
		return bRet;
	}
	bool csHWDeviceOEMPA::CheckReceptionDelay(float %fDelay)
	{
		float fDelay2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		fDelay2 = fDelay;
		bRet = m_pHWDeviceOEMPA->CheckReceptionDelay(fDelay2);
		fDelay = fDelay2;
		return bRet;
	}
	DWORD csHWDeviceOEMPA::GetSWBaseAddress()
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetSWBaseAddress();
	}

	bool csHWDeviceOEMPA::EnableMultiHWChannelAcquisition(int iCycle,int iCycleCount,bool bEnable)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->EnableMultiHWChannelAcquisition(iCycle,iCycleCount,bEnable);
	}
	bool csHWDeviceOEMPA::GetEnableMultiHWChannelAcquisition(int iCycle,bool *pbEnable)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetEnableMultiHWChannelAcquisition(iCycle,pbEnable);
	}
	bool csHWDeviceOEMPA::SetMultiHWChannelAcqDecimation(int iCycle,acsByte^ %abyData)
	{
		unionOEMMCDecimation decimation;
		unionDecimation *pX;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if((abyData==nullptr) || (abyData->list==nullptr) || (abyData->list->Length>g_iOEMPAApertureElementCountMax))
			return false;
		memset(&decimation,0,sizeof(decimation));
		for(int iIndex=0;iIndex<abyData->list->Length;iIndex++)
		{
			pX = (unionDecimation*)&decimation.byte[iIndex/2];
			if(!(iIndex%2))
				pX->data.lsb = abyData->list[iIndex];
			else
				pX->data.msb = abyData->list[iIndex];
		}
		bRet = m_pHWDeviceOEMPA->SetMultiHWChannelAcqDecimation(iCycle,decimation);
		return bRet;
	}
	bool csHWDeviceOEMPA::GetMultiHWChannelAcqDecimation(int iCycle,[Out] acsByte^ %abyData)
	{
		structCallbackArrayByte1D callback;
		gcroot<acsByte^> *pByte;

		if(!m_pHWDeviceOEMPA)
			return false;
		if(abyData==nullptr)
			abyData = gcnew acsByte;
		pByte = abyData->GetGcroot();
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)pByte;
		callback.pSetSize = gCallbackSetSizeByte1D;
		callback.pSetData = gCallbackSetDataByte1D;
		callback.pGetSize = gCallbackGetSizeByte1D;
		callback.pGetData = gCallbackGetDataByte1D;
		return m_pHWDeviceOEMPA->GetMultiHWChannelAcqDecimation(iCycle,callback);
	}
	bool csHWDeviceOEMPA::SetMultiHWChannelAcqWriteStart(int iCycle,int iAcqElement,int iStartCount,acsFloat^ %afStart/*float *pfStart*/)
	{
		structCallbackArrayFloat1D callback;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		if(afStart==nullptr)
			return false;
		if(afStart->list==nullptr)
			return false;
		if(afStart->list->Length>g_iOEMPAApertureElementCountMax)
			return false;
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)afStart->GetGcroot();
		callback.pSetSize = gCallbackSetSizeFloat;
		callback.pSetData = gCallbackSetDataFloat;
		callback.pGetSize = gCallbackGetSizeFloat;
		callback.pGetData = gCallbackGetDataFloat;
		bRet = m_pHWDeviceOEMPA->SetMultiHWChannelAcqWriteStart(iCycle,iAcqElement,iStartCount,callback);
		return bRet;
	}
	bool csHWDeviceOEMPA::GetMultiHWChannelAcqWriteStart(int iCycle,/*fixed*/[Out] acsFloat^ %afStart/*int &iStartCountMax,int *piStartCount,float *pfStart*/)
	{
		structCallbackArrayFloat1D callback;
		int iMax=g_iOEMPAApertureElementCountMax;
		bool bRet;
		gcroot<acsFloat^> *pStart;

		if(!m_pHWDeviceOEMPA)
			return false;
		if(afStart==nullptr)
			afStart = gcnew acsFloat;
		iMax = m_pHWDeviceOEMPA->GetSWDeviceOEMPA()->GetElementCountMax();//1.1.5.2h
		pStart = afStart->GetGcroot();
		callback.apParameter[0] = m_pointer;
		callback.apParameter[1] = (void*)pStart;
		callback.pSetSize = gCallbackSetSizeFloat;
		callback.pSetData = gCallbackSetDataFloat;
		callback.pGetSize = gCallbackGetSizeFloat;
		callback.pGetData = gCallbackGetDataFloat;
		bRet = m_pHWDeviceOEMPA->GetMultiHWChannelAcqWriteStart(iCycle,iMax,callback);
		return bRet;
	}
	double csHWDeviceOEMPA::GetMultiHWChannelRangeMax()
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetMultiHWChannelRangeMax();
	}
	double csHWDeviceOEMPA::GetFWAscanRecoveryTime()
	{
		if(!m_pHWDeviceOEMPA || !m_pHWDeviceOEMPA->GetSWDeviceOEMPA())
			return false;
		return m_pHWDeviceOEMPA->GetSWDeviceOEMPA()->GetFWAscanRecoveryTime();
	}

	bool csHWDeviceOEMPA::SetSettingId(DWORD dwSettingId)
	{
		DWORD dwSettingId2;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		dwSettingId2 = dwSettingId;
		bRet = m_pHWDeviceOEMPA->SetSettingId(dwSettingId2);
		dwSettingId = dwSettingId2;
		return bRet;
	}
	/*unsafe*/bool csHWDeviceOEMPA::GetSettingId(DWORD *pdwSettingId)
	{
		if(!m_pHWDeviceOEMPA)
			return 0;
		return m_pHWDeviceOEMPA->GetSettingId(pdwSettingId);
	}
	DWORD csHWDeviceOEMPA::swGetSettingId()
	{
		if(!m_pHWDeviceOEMPA || !m_pHWDeviceOEMPA->GetSWDeviceOEMPA())
			return 0;
		return m_pHWDeviceOEMPA->GetSWDeviceOEMPA()->GetSettingId();
	}
	bool csHWDeviceOEMPA::SetTimeOffset(float %fTime)
	{
		float fTime1=fTime;
		bool bRet;

		if(!m_pHWDeviceOEMPA)
			return false;
		bRet = m_pHWDeviceOEMPA->SetTimeOffset(fTime1);
		fTime = fTime1;
		return bRet;
	}
	bool csHWDeviceOEMPA::GetTimeOffset(float *pfTime)
	{
		if(!m_pHWDeviceOEMPA)
			return false;
		return m_pHWDeviceOEMPA->GetTimeOffset(pfTime);
	}

#pragma endregion csHWDeviceOEMPA
////////////////////////////////////////////////////////
////////////////////////////////////////////////////////

}

namespace csDriverOEMPA
{

	bool csRoot::CopyFrom(structRoot *pRoot)
	{
		if(!pRoot)
			return false;
		//root = safe_cast<csRoot^>(Marshal::PtrToStructure((IntPtr)pRoot,csRoot::typeid));
		this->dwRootSize = pRoot->dwRootSize;
		this->ullSavedParameters = pRoot->ullSavedParameters;
		this->uiSavedFilterCount = pRoot->uiSavedFilterCount;
		this->csDefaultHwLink = (csEnumHardwareLink)pRoot->eDefaultHwLink;
		this->bEnableFMC = pRoot->bEnableFMC;
		this->bEnableMultiHWChannel = pRoot->bEnableMultiHWChannel;
		this->bAscanEnable = pRoot->bAscanEnable;
		this->bEnableCscanTof = pRoot->bEnableCscanTof;
		this->csEnableTFM = (csEnumTFMParameters)pRoot->eEnableTFM;
		this->csAscanBitSize = (csEnumBitSize)(pRoot->eAscanBitSize);
		this->csAscanRequest = (csEnumAscanRequest)pRoot->eAscanRequest;
		this->dAscanRequestFrequency = pRoot->dAscanRequestFrequency;
		this->csTriggerMode = (csEnumOEMPATrigger)pRoot->eTriggerMode;
		this->csEncoderDirection = (csEnumOEMPAEncoderDirection)pRoot->eEncoderDirection;
		this->cTemperatureAlarm = pRoot->cTemperatureAlarm;
		this->cTemperatureWarning = pRoot->cTemperatureWarning;
		this->dTriggerEncoderStep = pRoot->dTriggerEncoderStep;
		this->csRequestIO = (csEnumOEMPARequestIO)pRoot->eRequestIO;
		this->iRequestIODigitalInputMaskRising = pRoot->iRequestIODigitalInputMaskRising;
		this->iRequestIODigitalInputMaskFalling = pRoot->iRequestIODigitalInputMaskFalling;
		this->dDebouncerEncoder = pRoot->dDebouncerEncoder;
		this->dDebouncerDigital = pRoot->dDebouncerDigital;
		this->csDigitalOuput = gcnew cli::array<csEnumOEMPAMappingDigitalOutput>(6);
		for(int i=0;i<6;i++)
			this->csDigitalOuput[i] = (csEnumOEMPAMappingDigitalOutput)pRoot->eDigitalOuput[i];
		this->lSWEncoderResolution1 = pRoot->lSWEncoderResolution1;
		this->lSWEncoderResolution2 = pRoot->lSWEncoderResolution2;
		this->dwSWEncoderDivider1 = pRoot->dwSWEncoderDivider1;
		this->dwSWEncoderDivider2 = pRoot->dwSWEncoderDivider2;
		this->csEncoder1A = (csEnumDigitalInput)pRoot->eEncoder1A;
		this->csEncoder1B = (csEnumDigitalInput)pRoot->eEncoder1B;
		this->csEncoder2A = (csEnumDigitalInput)pRoot->eEncoder2A;
		this->csEncoder2B = (csEnumDigitalInput)pRoot->eEncoder2B;
		this->csExternalTriggerCycle = (csEnumDigitalInput)pRoot->eExternalTriggerCycle;
		this->csExternalTriggerSequence = (csEnumDigitalInput)pRoot->eExternalTriggerSequence;
		this->csEncoder1Type = (csEnumEncoderType)pRoot->eEncoder1Type;
		this->csEncoder2Type = (csEnumEncoderType)pRoot->eEncoder2Type;
		this->csKeepAlive = (csEnumKeepAlive)pRoot->eKeepAlive;

		this->iCycleCount = pRoot->iCycleCount;
		this->iDACCountMax = pRoot->iDACCountMax;
		this->iDDFCountMax = pRoot->iDDFCountMax;
		this->aFilter = gcnew cli::array<csFilter^>(g_iEnumOEMPAFilterIndexLast);
		for(int i=0;i<g_iEnumOEMPAFilterIndexLast;i++)
		{
			this->aFilter[i] = gcnew csFilter;
			this->aFilter[i]->aCoefficient = gcnew cli::array<short>(g_iOEMPAFilterCoefficientMax);
			for(int j=0;j<g_iOEMPAFilterCoefficientMax;j++)
				this->aFilter[i]->aCoefficient[j] = pRoot->aFilter[i].aCoefficient[j];
			this->aFilter[i]->wScale = pRoot->aFilter[i].wScale;
			//this->aFilter[i]->pTitle = gcnew String(pRoot->aFilter[i].pTitle);
			if(wcslen(pRoot->aFilter[i].pTitle)<g_iFilterTitleLength)
				this->aFilter[i]->pTitle = Marshal::PtrToStringUni((IntPtr)pRoot->aFilter[i].pTitle);
			else
				this->aFilter[i]->pTitle = gcnew String(L"");
		}
		this->iFMCElementStart = pRoot->iFMCElementStart;
		this->iFMCElementStop = pRoot->iFMCElementStop;
		this->iFMCElementStep = pRoot->iFMCElementStep;

		this->dSpecimenVelocity = pRoot->dSpecimenVelocity;
		this->dDigitizingPeriod = pRoot->dDigitizingPeriod;
		this->dSpecimenRadius = pRoot->dSpecimenRadius;
		this->dSpecimenThickness = pRoot->dSpecimenThickness;
		this->iOEMPAProbeCountMax = pRoot->iOEMPAProbeCountMax;
		this->iOEMPAApertureCountMax = pRoot->iOEMPAApertureCountMax;
		this->iOEMPADDFCountMax = pRoot->iOEMPADDFCountMax;
		this->bUSB3Disable = pRoot->bUSB3Disable;
		this->dMultiHWChannelRangeMax = pRoot->dMultiHWChannelRangeMax;
		this->dFWAscanRecoveryTime = pRoot->dFWAscanRecoveryTime;
		this->dTriggerHighTime = pRoot->dTriggerHighTime;

		this->iSubSequenceEncoderCount = pRoot->iSubSequenceEncoderCount;
		this->iSubSequenceGateCount = pRoot->iSubSequenceGateCount;
		this->aiSubSequenceCycleStart = gcnew cli::array<int>(g_iSubSequenceTableSize);
		this->aiSubSequenceCycleStop = gcnew cli::array<int>(g_iSubSequenceTableSize);
		this->afSubSequenceValue = gcnew cli::array<float>(g_iSubSequenceTableSize);
		for(int iIndex=0;iIndex<g_iSubSequenceTableSize;iIndex++)
		{
			this->aiSubSequenceCycleStart[iIndex] = pRoot->aiSubSequenceCycleStart[iIndex];
			this->aiSubSequenceCycleStop[iIndex] = pRoot->aiSubSequenceCycleStop[iIndex];
			this->afSubSequenceValue[iIndex] = pRoot->afSubSequenceValue[iIndex];
		}
		this->iSubSequenceAverage = pRoot->iSubSequenceAverage;

		this->bAverage = pRoot->bAverage;
		this->bCycleOrderEnable = pRoot->bCycleOrderEnable;
		this->iCycleOrderCount = pRoot->iCycleOrderCount;
		if (!this->aiCycleOrder)
			this->aiCycleOrder = gcnew cli::array<int>(this->iCycleOrderCount);
		for (int iIndex = 0; iIndex < this->iCycleOrderCount; iIndex++)
			this->aiCycleOrder[iIndex] = pRoot->piCycleOrder[iIndex];

		this->eReferenceTimeOfFlight = (csEnumReferenceTimeOfFlight)pRoot->eReferenceTimeOfFlight;

		this->b256Master = pRoot->b256Master;
		this->b256Slave = pRoot->b256Slave;

		this->pHWDeviceOEMPA = pRoot->pHWDeviceOEMPA;
		this->wcFileName = gcnew cli::array<wchar_t>(MAX_PATH);
		for(int i=0;i<MAX_PATH;i++)
			this->wcFileName[i] = pRoot->wcFileName[i];
		if(wcslen(pRoot->wcFileName)>=MAX_PATH)
			this->wcFileName[MAX_PATH-1] = 0;
		return true;
	}
	bool csRoot::CopyTo(structRoot *pRoot)
	{
		wchar_t* y;

		if(!pRoot)
			return false;
		pRoot->dwRootSize = this->dwRootSize;
		pRoot->ullSavedParameters  = this->ullSavedParameters;
		pRoot->uiSavedFilterCount = this->uiSavedFilterCount;
		pRoot->eDefaultHwLink = (enumHardwareLink)this->csDefaultHwLink;
		pRoot->bEnableFMC = this->bEnableFMC;
		pRoot->bEnableMultiHWChannel = this->bEnableMultiHWChannel;
		pRoot->bAscanEnable = this->bAscanEnable;
		pRoot->bEnableCscanTof = this->bEnableCscanTof;
		pRoot->eEnableTFM = (enumTFMParameters)this->csEnableTFM;
		pRoot->eAscanBitSize = (enumBitSize)(this->csAscanBitSize);
		pRoot->eAscanRequest = (enumAscanRequest)this->csAscanRequest;
		pRoot->dAscanRequestFrequency = this->dAscanRequestFrequency;
		pRoot->eTriggerMode = (enumOEMPATrigger)this->csTriggerMode;
		pRoot->eEncoderDirection = (enumOEMPAEncoderDirection)this->csEncoderDirection;
		pRoot->cTemperatureAlarm = this->cTemperatureAlarm;
		pRoot->cTemperatureWarning = this->cTemperatureWarning;
		pRoot->dTriggerEncoderStep = this->dTriggerEncoderStep;
		pRoot->eRequestIO = (enumOEMPARequestIO)this->csRequestIO;
		pRoot->iRequestIODigitalInputMaskRising = this->iRequestIODigitalInputMaskRising;
		pRoot->iRequestIODigitalInputMaskFalling = this->iRequestIODigitalInputMaskFalling;
		pRoot->dDebouncerEncoder = this->dDebouncerEncoder;
		pRoot->dDebouncerDigital = this->dDebouncerDigital;
		if(this->csDigitalOuput!=nullptr)
		for(int i=0;i<6;i++)
			pRoot->eDigitalOuput[i] = (enumOEMPAMappingDigitalOutput)this->csDigitalOuput[i];
		pRoot->lSWEncoderResolution1 = this->lSWEncoderResolution1;
		pRoot->lSWEncoderResolution2 = this->lSWEncoderResolution2;
		pRoot->dwSWEncoderDivider1 = this->dwSWEncoderDivider1;
		pRoot->dwSWEncoderDivider2 = this->dwSWEncoderDivider2;
		pRoot->eEncoder1A = (enumDigitalInput)this->csEncoder1A;
		pRoot->eEncoder1B = (enumDigitalInput)this->csEncoder1B;
		pRoot->eEncoder2A = (enumDigitalInput)this->csEncoder2A;
		pRoot->eEncoder2B = (enumDigitalInput)this->csEncoder2B;
		pRoot->eExternalTriggerCycle = (enumDigitalInput)this->csExternalTriggerCycle;
		pRoot->eExternalTriggerSequence = (enumDigitalInput)this->csExternalTriggerSequence;
		pRoot->eEncoder1Type = (enumEncoderType)this->csEncoder1Type;
		pRoot->eEncoder2Type = (enumEncoderType)this->csEncoder2Type;
		pRoot->eKeepAlive = (enumKeepAlive)this->csKeepAlive;

		pRoot->iCycleCount = this->iCycleCount;
		pRoot->iDACCountMax = this->iDACCountMax;
		pRoot->iDDFCountMax = this->iDDFCountMax;
		if(this->aFilter!=nullptr)
		for(int i=0;i<g_iEnumOEMPAFilterIndexLast;i++)
		{
			if(this->aFilter[i]==nullptr)
				continue;
			if(this->aFilter[i]->aCoefficient!=nullptr)
			for(int j=0;j<g_iOEMPAFilterCoefficientMax;j++)
				pRoot->aFilter[i].aCoefficient[j]  = this->aFilter[i]->aCoefficient[j];
			pRoot->aFilter[i].wScale = this->aFilter[i]->wScale;
			if(this->aFilter[i]->pTitle==nullptr)
				continue;
			y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(this->aFilter[i]->pTitle);
			wcscpy_s(pRoot->aFilter[i].pTitle,MAX_PATH,y);
			Marshal::FreeHGlobal((IntPtr)y);
		}
		pRoot->iFMCElementStart = this->iFMCElementStart;
		pRoot->iFMCElementStop = this->iFMCElementStop;
		pRoot->iFMCElementStep = this->iFMCElementStep;

		pRoot->dSpecimenVelocity  = this->dSpecimenVelocity;
		pRoot->dSpecimenRadius = this->dSpecimenRadius;
		pRoot->dSpecimenThickness = this->dSpecimenThickness;
		pRoot->dDigitizingPeriod  = this->dDigitizingPeriod;
		pRoot->iOEMPAProbeCountMax = this->iOEMPAProbeCountMax;
		pRoot->iOEMPAApertureCountMax = this->iOEMPAApertureCountMax;
		pRoot->iOEMPADDFCountMax = this->iOEMPADDFCountMax;
		pRoot->bUSB3Disable = this->bUSB3Disable;
		pRoot->dMultiHWChannelRangeMax = this->dMultiHWChannelRangeMax;
		pRoot->dFWAscanRecoveryTime = this->dFWAscanRecoveryTime;
		pRoot->dTriggerHighTime = this->dTriggerHighTime;

		pRoot->iSubSequenceEncoderCount = this->iSubSequenceEncoderCount;
		pRoot->iSubSequenceGateCount = this->iSubSequenceGateCount;
		for(int iIndex=0;iIndex<g_iSubSequenceTableSize;iIndex++)
		{
			if(this->aiSubSequenceCycleStart!=nullptr)
				pRoot->aiSubSequenceCycleStart[iIndex] = this->aiSubSequenceCycleStart[iIndex];
			if(this->aiSubSequenceCycleStop!=nullptr)
				pRoot->aiSubSequenceCycleStop[iIndex] = this->aiSubSequenceCycleStop[iIndex];
			if(this->afSubSequenceValue!=nullptr)
				pRoot->afSubSequenceValue[iIndex] = this->afSubSequenceValue[iIndex];
		}
		pRoot->iSubSequenceAverage = this->iSubSequenceAverage;

		pRoot->bAverage = this->bAverage;
		pRoot->bCycleOrderEnable = this->bCycleOrderEnable;
		pRoot->iCycleOrderCount = this->iCycleOrderCount;
		if (pRoot->piCycleOrder)
		{
			delete[] pRoot->piCycleOrder;
			pRoot->piCycleOrder = new int[pRoot->iCycleOrderCount];
		}
		else
			pRoot->piCycleOrder = new int[pRoot->iCycleOrderCount];
		for (int iIndex = 0; iIndex < pRoot->iCycleOrderCount; iIndex++)
			pRoot->piCycleOrder[iIndex] = this->aiCycleOrder[iIndex];

		pRoot->eReferenceTimeOfFlight = (enumReferenceTimeOfFlight)this->eReferenceTimeOfFlight;

		pRoot->b256Master = this->b256Master;
		pRoot->b256Slave = this->b256Slave;

		pRoot->pHWDeviceOEMPA  = (CHWDeviceOEMPA*)this->pHWDeviceOEMPA;
		pRoot->pHWDeviceOEMPA  = (CHWDeviceOEMPA*)this->pHWDeviceOEMPA;
		if(this->wcFileName!=nullptr)
		{
			//y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(this->wcDebugFile1);
			//wcscpy_s(pRoot->wcDebugFile1,MAX_PATH,y);
			//Marshal::FreeHGlobal((IntPtr)y);
			for (int i=0;i<MAX_PATH;i++)
				pRoot->wcFileName[i] = this->wcFileName[i];
		}
		return true;
	}
	bool csCycle::CopyFrom(structCycle *pCycle)
	{
		if(!pCycle)
			return false;
		//1.1.3.2c if gate ON following code crash
		//cycle = safe_cast<csCycle^>(Marshal::PtrToStructure((IntPtr)&m_pCycle[iCycle],csCycle::typeid));
		this->dGainDigital = pCycle->dGainDigital;
		this->fBeamCorrection = pCycle->fBeamCorrection;
		this->dStart = pCycle->dStart;
		this->dRange = pCycle->dRange;
		this->dTimeSlot = pCycle->dTimeSlot;
		this->dFMCSubTimeSlotAcq = pCycle->dFMCSubTimeSlotAcq;
		this->dFMCSubTimeSlotReplay = pCycle->dFMCSubTimeSlotReplay;
		this->lPointCount = pCycle->lPointCount;
		this->lPointFactor = pCycle->lPointFactor;
		this->eCompressionType = (csEnumCompressionType)pCycle->eCompressionType;
		this->eRectification = (csEnumRectification)pCycle->eRectification;
		this->bDACAutoStop = pCycle->bDACAutoStop;
		this->iDACCount = pCycle->iDACCount;
		if(this->iDACCount>0)
		{
			this->adDACTof = gcnew cli::array<double>(g_iOEMPADACCountMax);
			this->afDACPrm = gcnew cli::array<float>(g_iOEMPADACCountMax);
			if(this->adDACTof!=nullptr)
			{
				for(int iIndex=0;iIndex<g_iOEMPADACCountMax;iIndex++)
					this->adDACTof[iIndex] = pCycle->adDACTof[iIndex];
			}
			if(this->afDACPrm!=nullptr)
			{
				for(int iIndex=0;iIndex<g_iOEMPADACCountMax;iIndex++)
					this->afDACPrm[iIndex] = pCycle->afDACPrm[iIndex];
			}
		}
		this->bDACSlope = pCycle->bDACSlope;
		this->bDACEnable = pCycle->bDACEnable;
		this->bMaximum = pCycle->bMaximum;
		this->bMinimum = pCycle->bMinimum;
		this->bSaturation = pCycle->bSaturation;
		this->wAcqIdChannelProbe = pCycle->wAcqIdChannelProbe;
		this->wAcqIdChannelScan = pCycle->wAcqIdChannelScan;
		this->wAcqIdChannelCycle = pCycle->wAcqIdChannelCycle;
		this->fGainAnalog = pCycle->fGainAnalog;
		this->iFilterIndex = pCycle->iFilterIndex;
		this->bTrackingAscanEnable = pCycle->bTrackingAscanEnable;
		this->iTrackingAscanIndexCycle = pCycle->iTrackingAscanIndexCycle;
		this->iTrackingAscanIndexGate = pCycle->iTrackingAscanIndexGate;
		this->bTrackingDacEnable = pCycle->bTrackingDacEnable;
		this->iTrackingDacIndexCycle = pCycle->iTrackingDacIndexCycle;
		this->iTrackingDacIndexGate = pCycle->iTrackingDacIndexGate;
		this->iGateCount = pCycle->iGateCount;
		this->gate = gcnew cli::array<csGate^>(g_iOEMPAGateCountMax);
		for(int iIndex=0;iIndex<g_iOEMPAGateCountMax;iIndex++)
		{
			//this->gate[iIndex] = safe_cast<csGate^>(Marshal::PtrToStructure((IntPtr)&pCycle->gate[iIndex],csGate::typeid));
			this->gate[iIndex] = gcnew csGate;
			this->gate[iIndex]->bEnable = pCycle->gate[iIndex].bEnable;
			this->gate[iIndex]->dStart = pCycle->gate[iIndex].dStart;
			this->gate[iIndex]->dStop = pCycle->gate[iIndex].dStop;
			this->gate[iIndex]->dThreshold = pCycle->gate[iIndex].dThreshold;
			this->gate[iIndex]->eRectification = (csEnumRectification)pCycle->gate[iIndex].eRectification;
			this->gate[iIndex]->eModeAmp = (csEnumGateModeAmp)pCycle->gate[iIndex].eModeAmp;
			this->gate[iIndex]->eModeTof = (csEnumGateModeTof)pCycle->gate[iIndex].eModeTof;
			this->gate[iIndex]->wAcqIDAmp = pCycle->gate[iIndex].wAcqIDAmp;
			this->gate[iIndex]->wAcqIDTof = pCycle->gate[iIndex].wAcqIDTof;
			this->gate[iIndex]->bTrackingStartEnable = pCycle->gate[iIndex].bTrackingStartEnable;
			this->gate[iIndex]->iTrackingStartIndexCycle = pCycle->gate[iIndex].iTrackingStartIndexCycle;
			this->gate[iIndex]->iTrackingStartIndexGate = pCycle->gate[iIndex].iTrackingStartIndexGate;
			this->gate[iIndex]->bTrackingStopEnable = pCycle->gate[iIndex].bTrackingStopEnable;
			this->gate[iIndex]->iTrackingStopIndexCycle = pCycle->gate[iIndex].iTrackingStopIndexCycle;
			this->gate[iIndex]->iTrackingStopIndexGate = pCycle->gate[iIndex].iTrackingStopIndexGate;
		}
		return true;
	}
	bool csCycle::CopyTo(structCycle *pCycle)
	{
		if(!pCycle)
			return false;
		pCycle->dGainDigital = this->dGainDigital;
		pCycle->fBeamCorrection = this->fBeamCorrection;
		pCycle->dStart = this->dStart;
		pCycle->dRange = this->dRange;
		pCycle->dTimeSlot = this->dTimeSlot;
		pCycle->dFMCSubTimeSlotAcq = this->dFMCSubTimeSlotAcq;
		pCycle->dFMCSubTimeSlotReplay = this->dFMCSubTimeSlotReplay;
		pCycle->lPointCount = this->lPointCount;
		pCycle->lPointFactor = this->lPointFactor;
		pCycle->eCompressionType = (enumCompressionType)this->eCompressionType;
		pCycle->eRectification = (enumRectification)this->eRectification;
		pCycle->bDACAutoStop = this->bDACAutoStop;
		pCycle->iDACCount = this->iDACCount;
		if((this->iDACCount>0) && (this->adDACTof!=nullptr) && (this->afDACPrm!=nullptr)
			&& (this->adDACTof->Rank==1) && (this->afDACPrm->Rank==1)
			&& (this->adDACTof->GetLength(0)==g_iOEMPADACCountMax) && (this->afDACPrm->GetLength(0)==g_iOEMPADACCountMax))
		{
			for(int iIndex=0;iIndex<this->iDACCount;iIndex++)
			{
				pCycle->adDACTof[iIndex] = this->adDACTof[iIndex];
				pCycle->afDACPrm[iIndex] = this->afDACPrm[iIndex];
			}
		}
		pCycle->bDACSlope = this->bDACSlope;
		pCycle->bDACEnable = this->bDACEnable;
		pCycle->bMaximum = this->bMaximum;
		pCycle->bMinimum = this->bMinimum;
		pCycle->bSaturation = this->bSaturation;
		pCycle->wAcqIdChannelProbe = this->wAcqIdChannelProbe;
		pCycle->wAcqIdChannelScan = this->wAcqIdChannelScan;
		pCycle->wAcqIdChannelCycle = this->wAcqIdChannelCycle;
		pCycle->fGainAnalog = this->fGainAnalog;
		pCycle->iFilterIndex = this->iFilterIndex;
		pCycle->bTrackingAscanEnable = this->bTrackingAscanEnable;
		pCycle->iTrackingAscanIndexCycle = this->iTrackingAscanIndexCycle;
		pCycle->iTrackingAscanIndexGate = this->iTrackingAscanIndexGate;
		pCycle->bTrackingDacEnable = this->bTrackingDacEnable;
		pCycle->iTrackingDacIndexCycle = this->iTrackingDacIndexCycle;
		pCycle->iTrackingDacIndexGate = this->iTrackingDacIndexGate;
		pCycle->iGateCount = this->iGateCount;
		if((this->gate != nullptr) && (this->gate->Length==g_iOEMPAGateCountMax))
		{
			for(int iIndex=0;iIndex<g_iOEMPAGateCountMax;iIndex++)
			{
				//this->gate[iIndex] = safe_cast<csGate^>(Marshal::PtrToStructure((IntPtr)&pCycle->gate[iIndex],csGate::typeid));
				pCycle->gate[iIndex].bEnable = this->gate[iIndex]->bEnable;
				pCycle->gate[iIndex].dStart = this->gate[iIndex]->dStart;
				pCycle->gate[iIndex].dStop = this->gate[iIndex]->dStop;
				pCycle->gate[iIndex].dThreshold = this->gate[iIndex]->dThreshold;
				pCycle->gate[iIndex].eRectification = (enumRectification)this->gate[iIndex]->eRectification;
				pCycle->gate[iIndex].eModeAmp = (enumGateModeAmp)this->gate[iIndex]->eModeAmp;
				pCycle->gate[iIndex].eModeTof = (enumGateModeTof)this->gate[iIndex]->eModeTof;
				pCycle->gate[iIndex].wAcqIDAmp = this->gate[iIndex]->wAcqIDAmp;
				pCycle->gate[iIndex].wAcqIDTof = this->gate[iIndex]->wAcqIDTof;
				pCycle->gate[iIndex].bTrackingStartEnable = this->gate[iIndex]->bTrackingStartEnable;
				pCycle->gate[iIndex].iTrackingStartIndexCycle = this->gate[iIndex]->iTrackingStartIndexCycle;
				pCycle->gate[iIndex].iTrackingStartIndexGate = this->gate[iIndex]->iTrackingStartIndexGate;
				pCycle->gate[iIndex].bTrackingStopEnable = this->gate[iIndex]->bTrackingStopEnable;
				pCycle->gate[iIndex].iTrackingStopIndexCycle = this->gate[iIndex]->iTrackingStopIndexCycle;
				pCycle->gate[iIndex].iTrackingStopIndexGate = this->gate[iIndex]->iTrackingStopIndexGate;
			}
		}
		return true;
	}
	bool csFocalLaw::CopyFrom(CFocalLaw *pFocalLaw)
	{
		if(!pFocalLaw)
			return false;
		//1.1.5.3k
		//if(bEmission)
		//	this = safe_cast<csFocalLaw^>(Marshal::PtrToStructure((IntPtr)&m_pEmission[iCycle],csFocalLaw::typeid));
		//else
		//	this = safe_cast<csFocalLaw^>(Marshal::PtrToStructure((IntPtr)&m_pReception[iCycle],csFocalLaw::typeid));
		this->dWedgeDelay = pFocalLaw->dWedgeDelay;
		this->iElementCount = pFocalLaw->iElementCount;
		this->adwElement = gcnew cli::array<DWORD>(g_iOEMPAApertureDWordCount);
		for(int iIndex=0; iIndex<g_iOEMPAApertureDWordCount; iIndex++)
			this->adwElement[iIndex] = pFocalLaw->adwElement[iIndex];
		this->csFocusing = (csEnumFocusing)pFocalLaw->eFocusing;
		this->iDelayCount = pFocalLaw->iDelayCount;
		this->iFocalCount = pFocalLaw->iFocalCount;
		this->afDelay = gcnew cli::array<float, 2>(pFocalLaw->iFocalCount, pFocalLaw->iElementCount);
		for (int iFocalIndex = 0; iFocalIndex<pFocalLaw->iFocalCount; iFocalIndex++)
		{
			for(int iElementIndex=0; iElementIndex<pFocalLaw->iElementCount; iElementIndex++)
				this->afDelay[iFocalIndex,iElementIndex] = pFocalLaw->afDelay[iFocalIndex][iElementIndex];
		}
		this->adFocalTimeOfFlight = gcnew cli::array<double>(g_iOEMPAFocalCountMax);
		this->fCenterDelay = pFocalLaw->fCenterDelay;
		for(int iIndex=0; iIndex<pFocalLaw->iFocalCount; iIndex++)
			this->adFocalTimeOfFlight[iIndex] = pFocalLaw->adFocalTimeOfFlight[iIndex];
		this->iPrmCount = pFocalLaw->iPrmCount;
		this->afPrm = gcnew cli::array<float>(g_iOEMPAApertureElementCountMax);
		for(int iIndex=0; iIndex<pFocalLaw->iElementCount; iIndex++)
			this->afPrm[iIndex] = pFocalLaw->afPrm[iIndex];
		this->dSkew = pFocalLaw->dSkew;
		this->dAngle = pFocalLaw->dAngle;
		this->dX = pFocalLaw->dX;
		this->dY = pFocalLaw->dY;
		this->dZ = pFocalLaw->dZ;
		this->dFocalX = pFocalLaw->dFocalX;
		this->dFocalY = pFocalLaw->dFocalY;
		this->dFocalZ = pFocalLaw->dFocalZ;
		this->hwAcqDecimation = gcnew cli::array<BYTE>(g_iOEMPAApertureElementCountMax/2);
		for(int iIndex=0; iIndex<g_iOEMPAApertureElementCountMax/2; iIndex++)
			this->hwAcqDecimation[iIndex] = pFocalLaw->hwAcqDecimation.byte[iIndex];
		return true;
	}
	bool csFocalLaw::CopyTo(CFocalLaw *pFocalLaw)
	{
		pFocalLaw->dWedgeDelay = this->dWedgeDelay;
		pFocalLaw->iElementCount = this->iElementCount;
		pFocalLaw->adwElement.SetAllElementEnable(false);
		for(int iIndex=0; iIndex<this->adwElement->Length; iIndex++)
			pFocalLaw->adwElement[iIndex] = this->adwElement[iIndex];
		pFocalLaw->eFocusing = (enumFocusing)this->csFocusing;
		pFocalLaw->iDelayCount = this->iDelayCount;
		pFocalLaw->iFocalCount = this->iFocalCount;
		for (int iFocalIndex = 0; iFocalIndex<pFocalLaw->iFocalCount; iFocalIndex++)
		{
			for(int iElementIndex=0; iElementIndex<pFocalLaw->iElementCount; iElementIndex++)
				pFocalLaw->afDelay[iFocalIndex][iElementIndex] = this->afDelay[iFocalIndex,iElementIndex];
		}
		pFocalLaw->fCenterDelay = this->fCenterDelay;
		if(this->adFocalTimeOfFlight!=nullptr)
		for(int iIndex=0; iIndex<pFocalLaw->iFocalCount; iIndex++)
			pFocalLaw->adFocalTimeOfFlight[iIndex] = this->adFocalTimeOfFlight[iIndex];
		pFocalLaw->iPrmCount = this->iPrmCount;
		if(this->afPrm!=nullptr)
		for(int iIndex=0; iIndex<pFocalLaw->iElementCount; iIndex++)
			pFocalLaw->afPrm[iIndex] = this->afPrm[iIndex];
		pFocalLaw->dSkew = this->dSkew;
		pFocalLaw->dAngle = this->dAngle;
		pFocalLaw->dX = this->dX;
		pFocalLaw->dY = this->dY;
		pFocalLaw->dZ = this->dZ;
		pFocalLaw->dFocalX = this->dFocalX;
		pFocalLaw->dFocalY = this->dFocalY;
		pFocalLaw->dFocalZ = this->dFocalZ;
		if(this && (this->hwAcqDecimation!=nullptr))
		{
			for(int iIndex=0; iIndex<this->hwAcqDecimation->GetLength(0); iIndex++)
				pFocalLaw->hwAcqDecimation.byte[iIndex] = this->hwAcqDecimation[iIndex];
		}
		return true;
	}

}

#pragma region callback
	UINT WINAPI gAcquisitionAscan_0x00010103(void *pAcquisitionParameter,structAcqInfoEx &acqInfo,const CStream_0x0001 *pStreamHeader,const CSubStreamAscan_0x0103 *pAscanHeader,const void *pBufferMax,const void *pBufferMin,const void *pBufferSat)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pAcquisitionParameter);
		if(!pointer)
			return 1;
		return (*pointer)->AcquisitionAscan_0x00010103(acqInfo,pStreamHeader,pAscanHeader,pBufferMax,pBufferMin,pBufferSat);
	}
	UINT WINAPI gAcquisitionAscan_0x00020203(void* pAcquisitionParameter, const CStream_0x0002* pStreamHeader, const CSubStreamAscan_0x0203* pAscanHeader, const void* pBufferMax, const void* pBufferMin, const void* pBufferSat)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pAcquisitionParameter);
		if (!pointer)
			return 1;
		return (*pointer)->AcquisitionAscan_0x00020203(pStreamHeader, pAscanHeader, pBufferMax, pBufferMin, pBufferSat);
	}
	UINT WINAPI gAcquisitionCscan_0x00010X02(void *pAcquisitionParameter,structAcqInfoEx &acqInfo,const CStream_0x0001 *pStreamHeader,const CSubStreamCscan_0x0X02 *pCscanHeader,const structCscanAmp_0x0102 *pBufferAmp, const structCscanAmpTof_0x0202 *pBufferAmpTof)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pAcquisitionParameter);
		if(!pointer)
			return 1;
		return (*pointer)->AcquisitionCscan_0x00010X02(acqInfo,pStreamHeader,pCscanHeader,pBufferAmp,pBufferAmpTof);
	}
	UINT WINAPI gAcquisitionCscan_0x00020402(void* pAcquisitionParameter, structAcqInfoEx& acqInfo, const CStream_0x0002* pStreamHeader, const CSubStreamCscan_0x0402* pCscanHeader, const structCscanAmpTof_0x0402* pBufferAmpTof)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pAcquisitionParameter);
		if (!pointer)
			return 1;
		return (*pointer)->AcquisitionCscan_0x00020402(acqInfo, pStreamHeader, pCscanHeader, pBufferAmpTof);
	}
	UINT WINAPI gAcquisitionIO_0x00010101(void *pAcquisitionParameter,const CStream_0x0001 *pStreamHeader,const CSubStreamIO_0x0101 *pIOHeader)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pAcquisitionParameter);
		if(!pointer)
			return 1;
		return (*pointer)->AcquisitionIO_0x00010101(pStreamHeader,pIOHeader);
	}
	UINT WINAPI gAcquisitionIO_1x00010101(void *pAcquisitionParameter,structAcqInfoEx &acqInfo,const CStream_0x0001 *pStreamHeader,const CSubStreamIO_0x0101 *pIOHeader)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pAcquisitionParameter);
		if(!pointer)
			return 1;
		return (*pointer)->AcquisitionIO_1x00010101(acqInfo,pStreamHeader,pIOHeader);
	}
	UINT WINAPI gAcquisitionInfo(void *pAcquisitionParameter,const wchar_t *pInfo)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pAcquisitionParameter);
		if(!pointer)
			return 1;
		return (*pointer)->AcquisitionInfo(pInfo);
	}

#ifndef _DRIVER_11XY_
	structCorrectionOEMPA* WINAPI gCallbackCustomizedOEM(CHWDeviceOEMPA *pHWDeviceOEMPA,const wchar_t *pFileName,enumStepCustomizedAPI eStep,structRoot *pRoot,structCycle *pCycle,CFocalLaw *pEmission,CFocalLaw *pReception)
#else //_DRIVER_11XY_
	structCorrectionOEMPA* WINAPI gCallbackCustomizedOEM(void *pHWDeviceOEMPA,const wchar_t *pFileName,enumStepCustomizedAPI eStep,structRoot *pRoot,structCycle *pCycle,CFocalLaw *pEmission,CFocalLaw *pReception)
#endif //_DRIVER_11XY_
	{
		CHWDeviceOEMPA *pOEMPA=(CHWDeviceOEMPA*)pHWDeviceOEMPA;
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		void *pAcquisitionParameter2;

		pAcquisitionParameter2 = pOEMPA->GetAcquisitionParameter();
		if(!pAcquisitionParameter2)
			return NULL;
		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pAcquisitionParameter2);
		(*pointer)->CallbackCustomizedAPI(pFileName,eStep,pRoot,pCycle,pEmission,pReception);
		return NULL;//we dont need correction in the current example.
	}

	static gcroot<csDriverOEMPA::csMsgBox^> g_pStaticKernelAPI;
	void WINAPI gCallbackSystemMessageBox(const wchar_t *pMsg)
	{
		g_pStaticKernelAPI->CallbackSystemMessageBox(pMsg);
	}
	void WINAPI gCallbackSystemMessageBoxList(const wchar_t *pMsg)
	{
		g_pStaticKernelAPI->CallbackSystemMessageBoxList(pMsg);
	}
	UINT WINAPI gCallbackSystemMessageBoxButtons(const wchar_t *pMsg,const wchar_t *pTitle,UINT nType)
	{
		return g_pStaticKernelAPI->CallbackSystemMessageBoxButtons(pMsg,pTitle,nType);
	}
	int WINAPI gCallbackOempaApiMessageBox(HWND hWnd,LPCTSTR lpszText,LPCTSTR lpszCaption,UINT nType)
	{
		return g_pStaticKernelAPI->CallbackOempaApiMessageBox(hWnd,lpszText,lpszCaption,nType);
	}

	bool WINAPI gCallbackSetSizeDouble(struct structCallbackArrayDouble1D *pCallbackArray,int iSize)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsDouble^>* csDouble;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csDouble = (gcroot<csDriverOEMPA::acsDouble^>*)(pCallbackArray->apParameter[1]);
		if(!csDouble)
			return false;
		(*csDouble)->list = gcnew cli::array<double>(iSize);
		return true;
	}
	bool WINAPI gCallbackSetDataDouble(struct structCallbackArrayDouble1D *pCallbackArray,int iIndex,double fData)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsDouble^>* csDouble;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csDouble = (gcroot<csDriverOEMPA::acsDouble^>*)(pCallbackArray->apParameter[1]);
		if(!csDouble || ((*csDouble)->list==nullptr))
			return false;
		if(iIndex<0)
			return false;
		if(iIndex>=(*csDouble)->list->Length)
			return true;
		(*csDouble)->list[iIndex] = fData;
		return true;
	}
	bool WINAPI gCallbackGetSizeDouble(struct structCallbackArrayDouble1D *pCallbackArray,int &iSize)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsDouble^>* csDouble;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csDouble = (gcroot<csDriverOEMPA::acsDouble^>*)(pCallbackArray->apParameter[1]);
		if(!csDouble || ((*csDouble)->list==nullptr))
			return false;
		iSize = (*csDouble)->list->Length;
		return true;
	}
	bool WINAPI gCallbackGetDataDouble(struct structCallbackArrayDouble1D *pCallbackArray,int iIndex,double &fData)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsDouble^>* csDouble;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csDouble = (gcroot<csDriverOEMPA::acsDouble^>*)(pCallbackArray->apParameter[1]);
		if(!csDouble || ((*csDouble)->list==nullptr))
			return false;
		if(iIndex<0)
			return false;
		if(iIndex>=(*csDouble)->list->Length)
			return true;
		fData = (*csDouble)->list[iIndex];
		return true;
	}

	bool WINAPI gCallbackSetSizeFloat(struct structCallbackArrayFloat1D *pCallbackArray,int iSize)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsFloat^>* csFloat;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csFloat = (gcroot<csDriverOEMPA::acsFloat^>*)(pCallbackArray->apParameter[1]);
		if(!csFloat)
			return false;
		(*csFloat)->list = gcnew cli::array<float>(iSize);
		return true;
	}
	bool WINAPI gCallbackSetDataFloat(struct structCallbackArrayFloat1D *pCallbackArray,int iIndex,float fData)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsFloat^>* csFloat;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csFloat = (gcroot<csDriverOEMPA::acsFloat^>*)(pCallbackArray->apParameter[1]);
		if(!csFloat || ((*csFloat)->list==nullptr))
			return false;
		if(iIndex<0)
			return false;
		if(iIndex>=(*csFloat)->list->Length)
			return true;
		(*csFloat)->list[iIndex] = fData;
		return true;
	}
	bool WINAPI gCallbackGetSizeFloat(struct structCallbackArrayFloat1D *pCallbackArray,int &iSize)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsFloat^>* csFloat;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csFloat = (gcroot<csDriverOEMPA::acsFloat^>*)(pCallbackArray->apParameter[1]);
		if(!csFloat || ((*csFloat)->list==nullptr))
			return false;
		iSize = (*csFloat)->list->Length;
		return true;
	}
	bool WINAPI gCallbackGetDataFloat(struct structCallbackArrayFloat1D *pCallbackArray,int iIndex,float &fData)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsFloat^>* csFloat;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csFloat = (gcroot<csDriverOEMPA::acsFloat^>*)(pCallbackArray->apParameter[1]);
		if(!csFloat || ((*csFloat)->list==nullptr))
			return false;
		if(iIndex<0)
			return false;
		if(iIndex>=(*csFloat)->list->Length)
			return true;
		fData = (*csFloat)->list[iIndex];
		return true;
	}

	bool WINAPI gCallbackSetSizeDelay2(struct structCallbackArrayFloat2D *pCallbackArray,int iSize1,int iSize2)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsDelayReception^>* csDelay;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csDelay = (gcroot<csDriverOEMPA::acsDelayReception^>*)(pCallbackArray->apParameter[1]);
		if(!csDelay)
			return false;
		(*csDelay)->list = gcnew cli::array<float,2>(iSize1,iSize2);
		return true;
	}
	bool WINAPI gCallbackSetDataDelay2(struct structCallbackArrayFloat2D *pCallbackArray,int iIndex1,int iIndex2,float fData)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsDelayReception^>* csDelay;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csDelay = (gcroot<csDriverOEMPA::acsDelayReception^>*)(pCallbackArray->apParameter[1]);
		if(!csDelay || ((*csDelay)->list==nullptr))
			return false;
		if((*csDelay)->list->Rank!=2)
			return false;
		if(iIndex1<0)
			return false;
		if(iIndex1>=(*csDelay)->list->GetLength(0))
			return true;
		if(iIndex2<0)
			return false;
		if(iIndex2>=(*csDelay)->list->GetLength(1))
			return true;
		(*csDelay)->list[iIndex1,iIndex2] = fData;
		return true;
	}
	bool WINAPI gCallbackGetSizeDelay2(struct structCallbackArrayFloat2D *pCallbackArray,int &iSize1,int &iSize2)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsDelayReception^>* csDelay;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csDelay = (gcroot<csDriverOEMPA::acsDelayReception^>*)(pCallbackArray->apParameter[1]);
		if(!csDelay || ((*csDelay)->list==nullptr))
			return false;
		iSize1 = (*csDelay)->list->GetLength(0);
		iSize2 = (*csDelay)->list->GetLength(1);
		return true;
	}
	bool WINAPI gCallbackGetDataDelay2(struct structCallbackArrayFloat2D *pCallbackArray,int iIndex1,int iIndex2,float &fData)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsDelayReception^>* csDelay;
		int iSize1,iSize2;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csDelay = (gcroot<csDriverOEMPA::acsDelayReception^>*)(pCallbackArray->apParameter[1]);
		if(!csDelay || ((*csDelay)->list==nullptr))
			return false;
		if((*csDelay)->list->Rank!=2)
			return false;
		iSize1 = (*csDelay)->list->GetLength(0);
		iSize2 = (*csDelay)->list->GetLength(1);
		if(iIndex1<0)
			return false;
		if(iIndex1>=iSize1)
			return true;
		if(iIndex2<0)
			return false;
		if(iIndex2>=iSize2)
			return true;
		fData = (*csDelay)->list[iIndex1,iIndex2];
		return true;
	}

	bool WINAPI gCallbackSetSizeDac(struct structCallbackArrayFloatDac *pCallbackArray,int iSize)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsDac^>* dac;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		dac = (gcroot<csDriverOEMPA::acsDac^>*)(pCallbackArray->apParameter[1]);
		if(!dac || !iSize)
			return false;
		(*dac)->list = gcnew cli::array<csDriverOEMPA::csDac^>(iSize);
		for(int i=0;i<iSize;i++)
			(*dac)->list[i] = gcnew csDriverOEMPA::csDac(0.0,0.0f);
		return true;
	}
	bool WINAPI gCallbackSetDataDac(struct structCallbackArrayFloatDac *pCallbackArray,int iIndex,double dTime,float fSlope)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsDac^>* dac;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		dac = (gcroot<csDriverOEMPA::acsDac^>*)(pCallbackArray->apParameter[1]);
		if(!dac)
			return false;
		if(iIndex<0)
			return false;
		if(iIndex>=(*dac)->list->Length)
			return true;
		((*dac)->list[iIndex])->dTime = dTime;
		((*dac)->list[iIndex])->fSlope = fSlope;
		return true;
	}
	bool WINAPI gCallbackGetSizeDac(struct structCallbackArrayFloatDac *pCallbackArray,int &iSize)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsDac^>* dac;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		dac = (gcroot<csDriverOEMPA::acsDac^>*)(pCallbackArray->apParameter[1]);
		if(!dac)
			return false;
		iSize = (*dac)->list->Length;
		return true;
	}
	bool WINAPI gCallbackGetDataDac(struct structCallbackArrayFloatDac *pCallbackArray,int iIndex,double &dTime,float &fSlope)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsDac^>* dac;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		dac = (gcroot<csDriverOEMPA::acsDac^>*)(pCallbackArray->apParameter[1]);
		if(!dac)
			return false;
		if(iIndex<0)
			return false;
		if(iIndex>=(*dac)->list->Length)
			return false;
		dTime = ((*dac)->list[iIndex])->dTime;
		fSlope = ((*dac)->list[iIndex])->fSlope;
		return true;
	}

	bool WINAPI gCallbackSetSizeByte1D(struct structCallbackArrayByte1D *pCallbackArray,int iSize)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsByte^>* csByte;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csByte = (gcroot<csDriverOEMPA::acsByte^>*)(pCallbackArray->apParameter[1]);
		if(!csByte)
			return false;
		(*csByte)->list = gcnew cli::array<BYTE>(iSize);
		return true;
	}
	bool WINAPI gCallbackSetDataByte1D(struct structCallbackArrayByte1D *pCallbackArray,int iIndex,BYTE byData)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsByte^>* csByte;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csByte = (gcroot<csDriverOEMPA::acsByte^>*)(pCallbackArray->apParameter[1]);
		if(!csByte || ((*csByte)->list==nullptr))
			return false;
		if(iIndex<0)
			return false;
		if(iIndex>=(*csByte)->list->Length)
			return true;
		(*csByte)->list[iIndex] = byData;
		return true;
	}
	bool WINAPI gCallbackGetSizeByte1D(struct structCallbackArrayByte1D *pCallbackArray,int &iSize)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsByte^>* csByte;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csByte = (gcroot<csDriverOEMPA::acsByte^>*)(pCallbackArray->apParameter[1]);
		if(!csByte || ((*csByte)->list==nullptr))
			return false;
		iSize = (*csByte)->list->Length;
		return true;
	}
	bool WINAPI gCallbackGetDataByte1D(struct structCallbackArrayByte1D *pCallbackArray,int iIndex,BYTE &byData)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		gcroot<csDriverOEMPA::acsByte^>* csByte;

		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pCallbackArray->apParameter[0]);
		csByte = (gcroot<csDriverOEMPA::acsByte^>*)(pCallbackArray->apParameter[1]);
		if(!csByte || ((*csByte)->list==nullptr))
			return false;
		if(iIndex<0)
			return false;
		if(iIndex>=(*csByte)->list->Length)
			return true;
		byData = (*csByte)->list[iIndex];
		return true;
	}
	void gCallbackHWMemory(CHWDevice *pHWDevice, DWORD addr, DWORD data, int size)
	{
		gcroot<csDriverOEMPA::csHWDeviceOEMPA^>* pointer;
		void *pAcquisitionParameter2;
		bool bMaster=true;

restart:
		if(!pHWDevice)
			return;
		switch(pHWDevice->GetMatchedDeviceHwLink())
		{
		case eUnlinked:	break;
		case eMaster:	break;
		case eSlave:	if(!bMaster)
							return;//error
						bMaster = false;
						pHWDevice = pHWDevice->GetMatchedDevice();
						goto restart;
						break;
		}
		pAcquisitionParameter2 = pHWDevice->GetAcquisitionParameter();
		if(!pAcquisitionParameter2)
			return;
		pointer = (gcroot<csDriverOEMPA::csHWDeviceOEMPA^>*)(pAcquisitionParameter2);
		(*pointer)->CallbackHWMemory(bMaster, addr, data, size);
	}

#pragma endregion callback

#pragma region init
	void init(structAcqInfoEx &acqInfo)
	{
		acqInfo.lEncoder[0] = 0x12345678;
		acqInfo.lEncoder[1] = 0x89ABCDEF;
		acqInfo.dEncoder[0] = 1.2345678;
		acqInfo.dEncoder[1] = 1.2345678;
		acqInfo.dwDigitalInputs = 0x12345678;
		acqInfo.lDeviceId = 0x89ABCDEF;
		acqInfo.bMultiChannel = 0x12345678;
		acqInfo.bFullMatrixCapture = 0;
		acqInfo.lFMCElementIndex = 0x12345678;
	};

	void init(CStream_0x0001 &StreamHeader)
	{
		StreamHeader.start = 0x12345678;
		StreamHeader.size = 0x89ABCDEF;
		StreamHeader.frameId = 0x12345678;
		StreamHeader.settingId = 0x89ABCDEF;
		StreamHeader.subStreamCount = 0x1234;
		StreamHeader.version = 0x89AB;
	};

	void init(CSubStreamIO_0x0101 &ioHeader)
	{
		ioHeader.id = 0x12;
		ioHeader.version = 0x89;
		ioHeader.size = 0x1234;
		ioHeader.timeStampLow = 0x89ABCDEF;
		ioHeader.timeStampHigh = 0x12345678;
		ioHeader.cycle = 0x89AB;
		ioHeader.padding0 = 0x1234;
		ioHeader.seqLow = 0x89ABCDEF;
		ioHeader.seqHigh = 0x12345678;
		ioHeader.inputs = 0x89ABCDEF;
		ioHeader.edges = 0x12345678;
		ioHeader.encoder1 = 0x89ABCDEF;
		ioHeader.encoder2 = 0x12345678;
	};

	void init(structCscanAmp_0x0102 &bufferAmp)
	{
		bufferAmp.byAmp = 0x12;
		bufferAmp.gateId = 0x89;
		bufferAmp.wAcqId = 0x1234;
	};

	void init(structCscanAmpTof_0x0202 &bufferAmpTof)
	{
		bufferAmpTof.byAmp = 0x12;
		bufferAmpTof.gateId = 0x89;
		bufferAmpTof.wAcqIdAmp = 0x1234;
		bufferAmpTof.wTof = 0x89AB;
		bufferAmpTof.wAcqIdTof = 0x1234;
	};

	void init(CSubStreamCscan_0x0X02 &cscanHeader)
	{
		cscanHeader.id = 0x12;
		cscanHeader.version = 0x89;
		cscanHeader.size = 0x1234;
		cscanHeader.timeStampLow = 0x89ABCDEF;
		cscanHeader.timeStampHigh = 0x12345678;
		cscanHeader.cycle = 0x89AB;
		cscanHeader.count = 0x1234;
		cscanHeader.seqLow = 0x89ABCDEF;
		cscanHeader.seqHigh = 0x12345678;
	};

	void init(CSubStreamAscan_0x0103 &AscanHeader)
	{
		AscanHeader.id = 0x12;
		AscanHeader.version = 0x89;
		AscanHeader.size = 0x1234;
		AscanHeader.timeStampLow = 0x89ABCDEF;
		AscanHeader.timeStampHigh = 0x12345678;
		AscanHeader.cycle = 0x89AB;
		AscanHeader.dataCount = 0x1234;
		//AscanHeader.ascan_byte1 = 0x89;
		AscanHeader.src = 1;
		AscanHeader.dst = 0;
		AscanHeader.type = 0;
		AscanHeader.error = 1;
		AscanHeader.dataSize = 0x8;//size of one data (in bytes), take a look also to member "bitSize".
		//AscanHeader.ascan_byte2 = 0x12;
		AscanHeader.align = 2;
		AscanHeader.max = 1;//maximum buffer is valid.
		AscanHeader.min = 0;//minimum buffer is valid.
		AscanHeader.sat = 0;//saturation buffer is valid.
		AscanHeader.sign = 0;//sign of maximum and minimum buffer data.
		//AscanHeader.ascan_word1 = 0x89AB;
		AscanHeader.bitSize = 3;//see enumBitSize
		//AscanHeader.seqLost = 0;//encoder speed too high.
		AscanHeader.dacIFOldReference = 0;
		AscanHeader.dacIFNotInitialized = 0;
		AscanHeader.IFOldReference = 0;
		AscanHeader.IFNotInitialized = 0;
		AscanHeader.padding0 = 0x1135;
		AscanHeader.FWAcqIdChannelCycle = 0x1234;
		AscanHeader.FWAcqIdChannelScan = 0x89AB;
		AscanHeader.FWAcqIdChannelProbe = 0x1234;
		AscanHeader.padding1 = 0x89AB;
		AscanHeader.seqLow = 0x12345678;
		AscanHeader.seqHigh = 0x89ABCDEF;
	};

#pragma endregion init
//////////////////////////////////////////////////////////////////////////////
