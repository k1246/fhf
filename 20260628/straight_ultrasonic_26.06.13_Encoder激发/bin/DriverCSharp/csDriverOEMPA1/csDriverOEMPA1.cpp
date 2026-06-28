#include "stdafx.h"
#include <gcroot.h>

#pragma managed(push, off)
#ifdef _DRIVER_11XY_
#include "UTKernelDriver.h"
#include "UTKernelDriverOEMPA.h"
#else //_DRIVER_11XY_
#include "UTDriverOEMPA.h"
#include "UTDriverOEMPA1.h"
#endif //_DRIVER_11XY_
#ifndef _CONST_VOID_
#define _CONST_VOID_
typedef union constVoid{
	const void *pcVoid;
	void *pVoid;
}constVoid;
#endif //_CONST_VOID_

#pragma managed(push, on)

using System::String;
using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::Collections::Generic;
#ifndef _DRIVER_11XY_
using namespace csDriverOEMPA;
#else //_DRIVER_11XY_
using namespace csDriverOEM;
#endif //_DRIVER_11XY_

#ifdef _DRIVER_11XY_
namespace csDriverOEMPA
{
	ref class csHWDeviceOEMPA1;

	public ref class csCustomizedAPI : public csCustomizedOEMPA
	{
	private:
		CHWDeviceOEMPA1 *m_pHWDeviceOEMPA1;
	public:
		csCustomizedAPI(CHWDeviceOEMPA1 *pHWDeviceOEMPA1);
		~csCustomizedAPI();
		void Free();

		bool WriteHW(csHWDeviceOEMPA1^ %pHWDeviceOEMPA,csRoot^ %root,cli::array<csCycle^>^ %cycle,cli::array<csFocalLaw^>^ %emission,cli::array<csFocalLaw^>^ %reception,csEnumAcquisitionState eAcqState);
	protected:
		!csCustomizedAPI();
	};

};

namespace csDriverOEMPA
#else //_DRIVER_11XY_
using namespace csDriverOEMPA;

namespace csDriverOEMPA1
#endif //_DRIVER_11XY_
{

//////////////////////////////////////////////////////////////////////////

#pragma region csSWDeviceOEMPA1
	public ref class csSWDeviceOEMPA1 :
#ifndef _DRIVER_11XY_
										public csSWDeviceOEMPA
#else //_DRIVER_11XY_
										public csSWDeviceOEM
#endif //_DRIVER_11XY_
    {
	private:
		CHWDeviceOEMPA1 *m_pHWDeviceOEMPA1;
		CSWDeviceOEMPA1 *m_pSWDeviceOEMPA1;
	public:
		csSWDeviceOEMPA1();
		~csSWDeviceOEMPA1();
		void Constructor(CHWDeviceOEMPA1 *pHWDeviceOEMPA1,CSWDeviceOEMPA1 *pSWDeviceOEMPA1);
		void Free();

	//ethernet
		bool SetIPAddress(String ^pValue);
		bool GetIPAddress([Out] String^ %pValue);

		bool SetPort(unsigned short usValue);
		bool _SetPort(unsigned short usValue);//same than "SetPort", required because of the preprocessor definition in "WinSpool.h": "#define SetPort SetPortW".
		bool GetPort(unsigned short %usValue);

		//static void CfgKernelUpdate();
	protected:
		!csSWDeviceOEMPA1();
	};
#pragma endregion csSWDeviceOEMPA1

#pragma region csHWDeviceOEMPA1
#ifndef _DRIVER_11XY_
	public ref class csHWDeviceOEMPA1 :	public csHWDeviceOEMPA
#else //_DRIVER_11XY_
	public ref class csHWDeviceOEMPA:	public csHWDeviceOEM
#endif //_DRIVER_11XY_
    {
	private:
#ifdef _DRIVER_11XY_
		csCustomizedAPI ^m_csCustomizedAPI;
#endif //_DRIVER_11XY_
		csSWDeviceOEMPA1 ^m_csSWDeviceOEMPA1;
		CHWDeviceOEMPA1 *m_pHWDeviceOEMPA1;
		CHWDevice *m_pHWDevice;
		CSWDeviceOEMPA1 *m_pSWDeviceOEMPA1;
	public:
		csHWDeviceOEMPA1();
		~csHWDeviceOEMPA1();
		void Free();
		csSWDeviceOEMPA1^ GetSWDeviceOEMPA1();
		csHWDevice ^GetHWDevice();
		csSWDeviceOEMPA1 ^GetSWDeviceOEMPA();
#ifdef _DRIVER_11XY_
		csCustomizedAPI ^GetCustomizedAPI();
#endif //_DRIVER_11XY_

		/*unsafe*/bool GetApertureCountMax(int *piCount);//to get the maximum element count of an aperture.
		/*unsafe*/bool GetElementCountMax(int *piCount);//to get the maximum element count of the system (in case of mux).
			//Output parameters
			//	piCount : maximum aperture size.
	
		//calibration management
		bool PerformCalibration([Out] float %fDelayMax,[Out] float %fCorrectionOffset);//In case of calibration error you can display the reporting file (see "GetCalibrationFileReport").
				//output parameter "fDelayMax" maximum correction delay in second
				//output parameter "fCalibrationOffset" offset time of all elements in second, call "SetTimeOffset" with this value to correct it.
		bool UpdateCalibration();
		void/*CHWDeviceOEMPA1*/ *GetHWDeviceOEMPA1();
	protected:
		!csHWDeviceOEMPA1();
	};
#pragma endregion hwDriverOEMPA1
//////////////////////////////////////////////////////////////////////////

};

#ifndef _DRIVER_11XY_
namespace csDriverOEMPA1
#else //_DRIVER_11XY_
namespace csDriverOEMPA
#endif //_DRIVER_11XY_
{
////////////////////////////////////////////////////////
#pragma region csSWDeviceOEMPA1
	csSWDeviceOEMPA1::csSWDeviceOEMPA1()
	{
		Free();
	}
	csSWDeviceOEMPA1::~csSWDeviceOEMPA1()
	{
		this->!csSWDeviceOEMPA1();
	}
	csSWDeviceOEMPA1::!csSWDeviceOEMPA1()
	{
		Free();
	}
	void csSWDeviceOEMPA1::Constructor(CHWDeviceOEMPA1 *pHWDeviceOEMPA1,CSWDeviceOEMPA1 *pSWDeviceOEMPA1)
	{
		m_pHWDeviceOEMPA1 = pHWDeviceOEMPA1;
		m_pSWDeviceOEMPA1 = pSWDeviceOEMPA1;
	}
	void csSWDeviceOEMPA1::Free()
	{
		m_pHWDeviceOEMPA1 = NULL;
		m_pSWDeviceOEMPA1 = NULL;
	}

//ethernet
	bool csSWDeviceOEMPA1::SetIPAddress(String ^pValue)
	{
		wchar_t* y;
		bool bRet;

		if(!m_pSWDeviceOEMPA1)
			return false;
		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pValue);
		bRet = m_pSWDeviceOEMPA1->SetIPAddress(y);
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}
	bool csSWDeviceOEMPA1::GetIPAddress([Out] String^ %pValue)
	{
		wchar_t pAux[MAX_PATH];

		if(!m_pSWDeviceOEMPA1)
			return false;
		if(!m_pSWDeviceOEMPA1->GetIPAddress(pAux,MAX_PATH))
			return false;
		pValue = Marshal::PtrToStringUni((IntPtr)pAux);
		return true;
	}

	bool csSWDeviceOEMPA1::SetPort(unsigned short usValue)
	{
		return _SetPort(usValue);
	}
	bool csSWDeviceOEMPA1::_SetPort(unsigned short usValue)
	{
		if(!m_pSWDeviceOEMPA1)
			return false;
		return m_pSWDeviceOEMPA1->_SetPort(usValue);//same than "SetPort", required because of the preprocessor definition in "WinSpool.h": "#define SetPort SetPortW".
	}
	bool csSWDeviceOEMPA1::GetPort(unsigned short %usValue)
	{
		unsigned short usValue2;
		bool bRet;

		if(!m_pSWDeviceOEMPA1)
			return false;
		bRet = m_pSWDeviceOEMPA1->GetPort(usValue2);
		usValue = usValue2;
		return bRet;
	}

	//void csSWDeviceOEMPA1::CfgKernelUpdate()
	//{
	//	CHWDeviceOEMPA1::CfgKernelUpdate();
	//}

	//int csSWDeviceOEMPA1::GetApertureCountMax()//to get the maximum element count of an aperture.
	//{
	//	if(!m_pSWDeviceOEMPA1)
	//		return 0;
	//	return m_pSWDeviceOEMPA1->GetApertureCountMax();
	//}
	//int csSWDeviceOEMPA1::GetElementCountMax()//to get the maximum element count of the system (in case of mux).
	//{
	//	if(!m_pSWDeviceOEMPA1)
	//		return 0;
	//	return m_pSWDeviceOEMPA1->GetElementCountMax();
	//}

	//void csSWDeviceOEMPA1::AlignmentCfgUpdated()
	//{
	//	CSWDeviceOEMPA1::AlignmentCfgUpdated();
	//}
	//void csSWDeviceOEMPA1::SetCalibrationParameters(float fWidth,float fStart,float fRange,float fGainAnalog,double dGainDigital)
	//{
	//	if(!m_pSWDeviceOEMPA1)
	//		return;
	//	return m_pSWDeviceOEMPA1->SetCalibrationParameters(fWidth,fStart,fRange,fGainAnalog,dGainDigital);
	//}
	//void csSWDeviceOEMPA1::GetCalibrationParameters(float &fWidth,float &fStart,float &fRange,float &fGainAnalog,double &dGainDigital)
	//{
	//	if(!m_pSWDeviceOEMPA1)
	//		return;
	//	return m_pSWDeviceOEMPA1->GetCalibrationParameters(fWidth,fStart,fRange,fGainAnalog,dGainDigital);
	//}
	//bool csSWDeviceOEMPA1::IsCalibrationPerformed()
	//{
	//	return IsAlignmentPerformed();
	//}
	//bool csSWDeviceOEMPA1::IsAlignmentPerformed()
	//{
	//	if(!m_pSWDeviceOEMPA1)
	//		return false;
	//	return m_pSWDeviceOEMPA1->IsAlignmentPerformed();
	//}
	//bool csSWDeviceOEMPA1::EnableAlignment(bool bEnable)
	//{
	//	if(!m_pSWDeviceOEMPA1)
	//		return false;
	//	return m_pSWDeviceOEMPA1->EnableAlignment(bEnable);
	//}
	//float csSWDeviceOEMPA1::GetCalibrationAlignment()
	//{
	//	if(!m_pSWDeviceOEMPA1)
	//		return 0.0f;
	//	return m_pSWDeviceOEMPA1->GetCalibrationAlignment();
	//}
	//float csSWDeviceOEMPA1::GetCalibrationOffset()
	//{
	//	if(!m_pSWDeviceOEMPA1)
	//		return 0.0f;
	//	return m_pSWDeviceOEMPA1->GetCalibrationOffset();
	//}
	//bool csSWDeviceOEMPA1::IsAlignmentEnabled()
	//{
	//	if(!m_pSWDeviceOEMPA1)
	//		return false;
	//	return m_pSWDeviceOEMPA1->IsAlignmentEnabled();
	//}
	//bool csSWDeviceOEMPA1::ResetAlignment()
	//{
	//	if(!m_pSWDeviceOEMPA1)
	//		return false;
	//	return m_pSWDeviceOEMPA1->ResetAlignment();
	//}
	//bool csSWDeviceOEMPA1::SetCalibrationFileReport(String ^pFileReport)
	//{
	//	wchar_t* y;

	//	if(!m_pSWDeviceOEMPA1)
	//		return false;
	//	y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pFileReport);
	//	m_pSWDeviceOEMPA1->SetCalibrationFileReport(y);
	//	Marshal::FreeHGlobal((IntPtr)y);
	//	return true;
	//}
	//bool csSWDeviceOEMPA1::GetCalibrationFileReport([Out] String^ %pFileReport)
	//{
	//	wchar_t pAux[MAX_PATH];

	//	if(!m_pSWDeviceOEMPA1)
	//		return false;
	//	if(!m_pSWDeviceOEMPA1->GetCalibrationFileReport(MAX_PATH,pAux))
	//		return false;
	//	pFileReport = Marshal::PtrToStringUni((IntPtr)pAux);
	//	return true;
	//}
#pragma endregion csSWDeviceOEMPA1
////////////////////////////////////////////////////////
#pragma region csHWDeviceOEMPA1
	csHWDeviceOEMPA1::csHWDeviceOEMPA1() :
#ifndef _DRIVER_11XY_
											csHWDeviceOEMPA(csEnumHardware::csOEMPA1,false,false)
#else //_DRIVER_11XY_
											csHWDeviceOEM(csEnumHardware::csOEMPA1,true, false)
#endif //_DRIVER_11XY_
	{
		bool bCreate;
		int iValue = CSWDevice::ReadCfgInt(L"UTKernel" LS L"Device",L"csDriverOEMPA1Count",0,bCreate);

		iValue++;
		CSWDevice::WriteCfgInt(L"UTKernel" LS L"Device",L"csDriverOEMPA1Count",iValue);
		m_pHWDeviceOEMPA1 = dynamic_cast<CHWDeviceOEMPA1*>((CHWDeviceOEMPA*)cGetHWDeviceOEMPA());//new CHWDeviceOEMPA1();
#ifdef _DRIVER_11XY_
		m_csCustomizedAPI = gcnew csCustomizedAPI(m_pHWDeviceOEMPA1);
		csHWDeviceOEM::SetCustomizedOEMPA(dynamic_cast<csCustomizedOEMPA^>(m_csCustomizedAPI));
#endif //_DRIVER_11XY_
		m_pHWDevice = dynamic_cast<CHWDevice*>(m_pHWDeviceOEMPA1);
		m_pSWDeviceOEMPA1 = m_pHWDeviceOEMPA1->GetSWDeviceOEMPA();
		csHWDevice::Constructor(m_pHWDeviceOEMPA1,m_pHWDevice);
		csHWDevice::Constructor(GetCustomizedOEMPA());
		m_csSWDeviceOEMPA1 = gcnew csSWDeviceOEMPA1();
		m_csSWDeviceOEMPA1->Constructor(m_pHWDeviceOEMPA1,m_pSWDeviceOEMPA1);
		if(m_pHWDevice)
			m_pHWDevice->SetDerivedClass(L"csHWDeviceOEMPA1",NULL);
	}
	csHWDeviceOEMPA1::~csHWDeviceOEMPA1()
	{
		this->!csHWDeviceOEMPA1();
	}
	csHWDeviceOEMPA1::!csHWDeviceOEMPA1()
	{
		Free();
	}
	void csHWDeviceOEMPA1::Free()
	{
		m_pSWDeviceOEMPA1 = NULL;
		m_pHWDeviceOEMPA1 = NULL;
	}
	csSWDeviceOEMPA1^ csHWDeviceOEMPA1::GetSWDeviceOEMPA1()
	{
		return m_csSWDeviceOEMPA1;
	}
	csSWDeviceOEMPA1^ csHWDeviceOEMPA1::GetSWDeviceOEMPA()
	{
		return m_csSWDeviceOEMPA1;
	}

	/*unsafe*/bool csHWDeviceOEMPA1::GetApertureCountMax(int *piCount)
	{
		if(!m_pHWDeviceOEMPA1)
			return false;
		return m_pHWDeviceOEMPA1->GetApertureCountMax(piCount);
	}
	/*unsafe*/bool csHWDeviceOEMPA1::GetElementCountMax(int *piCount)
	{
		if(!m_pHWDeviceOEMPA1)
			return false;
		return m_pHWDeviceOEMPA1->GetElementCountMax(piCount);
	}

	void/*CHWDeviceOEMPA1*/ *csHWDeviceOEMPA1::GetHWDeviceOEMPA1()
	{
		return m_pHWDeviceOEMPA1;
	}

	bool csHWDeviceOEMPA1::PerformCalibration([Out] float %fDelayMax,[Out] float %fCorrectionOffset)
	{
		float fDelayMax1,fCorrectionOffset1;
		bool bRet;

		if(!m_pHWDeviceOEMPA1)
			return false;
		bRet = m_pHWDeviceOEMPA1->PerformCalibration(fDelayMax1,fCorrectionOffset1);
		fDelayMax = fDelayMax1;
		fCorrectionOffset = fCorrectionOffset1;
		return bRet;
	}
	bool csHWDeviceOEMPA1::UpdateCalibration()
	{
		if(!m_pHWDeviceOEMPA1)
			return false;
		return m_pHWDeviceOEMPA1->UpdateCalibration();
	}
	csHWDevice ^csHWDeviceOEMPA1::GetHWDevice()
	{
		return this;
	}
#pragma endregion csHWDeviceOEMPA1
////////////////////////////////////////////////////////
////////////////////////////////////////////////////////

}
