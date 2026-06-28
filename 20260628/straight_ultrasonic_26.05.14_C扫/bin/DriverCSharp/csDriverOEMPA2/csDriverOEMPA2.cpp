#include "stdafx.h"
#include <gcroot.h>

#pragma managed(push, off)
#ifdef _DRIVER_11XY_
#include "UTKernelDriver.h"
#include "UTKernelDriverOEMPA.h"
#else //_DRIVER_11XY_
#include "UTDriverOEMPA.h"
#include "UTDriverOEMPA2.h"
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
	ref class csHWDeviceOEMPA2;

	public ref class csCustomizedAPI : public csCustomizedOEMPA
	{
	private:
		CHWDeviceOEMPA2 *m_pHWDeviceOEMPA2;
	public:
		csCustomizedAPI(CHWDeviceOEMPA2 *pHWDeviceOEMPA2);
		~csCustomizedAPI();
		void Free();

		bool WriteHW(csHWDeviceOEMPA2^ %pHWDeviceOEMPA,csRoot^ %root,cli::array<csCycle^>^ %cycle,cli::array<csFocalLaw^>^ %emission,cli::array<csFocalLaw^>^ %reception,csEnumAcquisitionState eAcqState);
	protected:
		!csCustomizedAPI();
	};

};

namespace csDriverOEMPA
#else //_DRIVER_11XY_
using namespace csDriverOEMPA;

namespace csDriverOEMPA2
#endif //_DRIVER_11XY_
{

//////////////////////////////////////////////////////////////////////////

#pragma region csSWDeviceOEMPA2
	public ref class csSWDeviceOEMPA2 :
#ifndef _DRIVER_11XY_
										public csSWDeviceOEMPA
#else //_DRIVER_11XY_
										public csSWDeviceOEM
#endif //_DRIVER_11XY_
    {
	private:
		CHWDeviceOEMPA2 *m_pHWDeviceOEMPA2;
		CSWDeviceOEMPA2 *m_pSWDeviceOEMPA2;
	public:
		csSWDeviceOEMPA2();
		~csSWDeviceOEMPA2();
		void Constructor(CHWDeviceOEMPA2 *pHWDeviceOEMPA2,CSWDeviceOEMPA2 *pSWDeviceOEMPA2);
		void Free();

	//ethernet
		bool SetIPAddress(String ^pValue);
		bool GetIPAddress([Out] String^ %pValue);

		bool SetPort(unsigned short usValue);
		bool _SetPort(unsigned short usValue);//same than "SetPort", required because of the preprocessor definition in "WinSpool.h": "#define SetPort SetPortW".
		bool GetPort(unsigned short %usValue);

	protected:
		!csSWDeviceOEMPA2();
	};
#pragma endregion csSWDeviceOEMPA2

#pragma region csHWDeviceOEMPA2
#ifndef _DRIVER_11XY_
	public ref class csHWDeviceOEMPA2 :	public csHWDeviceOEMPA
#else //_DRIVER_11XY_
	public ref class csHWDeviceOEMPA:	public csHWDeviceOEM
#endif //_DRIVER_11XY_
    {
	private:
#ifdef _DRIVER_11XY_
		csCustomizedAPI ^m_csCustomizedAPI;
#endif //_DRIVER_11XY_
		csSWDeviceOEMPA2 ^m_csSWDeviceOEMPA2;
		CHWDeviceOEMPA2 *m_pHWDeviceOEMPA2;
		CHWDevice *m_pHWDevice;
		CSWDeviceOEMPA2 *m_pSWDeviceOEMPA2;
	public:
		csHWDeviceOEMPA2();
		~csHWDeviceOEMPA2();
		void Free();
		csSWDeviceOEMPA2^ GetSWDeviceOEMPA2();
		csHWDevice ^GetHWDevice();
		csSWDeviceOEMPA2 ^GetSWDeviceOEMPA();
#ifdef _DRIVER_11XY_
		csCustomizedAPI ^GetCustomizedAPI();
#endif //_DRIVER_11XY_

		/*unsafe*/bool GetApertureCountMax(int *piCount);//to get the maximum element count of an aperture.
		/*unsafe*/bool GetElementCountMax(int *piCount);//to get the maximum element count of the system (in case of mux).
			//Output parameters
			//	piCount : maximum aperture size.
	
		void/*CHWDeviceOEMPA2*/ *GetHWDeviceOEMPA2();
	protected:
		!csHWDeviceOEMPA2();
	};
#pragma endregion csDriverOEMPA2
//////////////////////////////////////////////////////////////////////////

};

#ifndef _DRIVER_11XY_
namespace csDriverOEMPA2
#else //_DRIVER_11XY_
namespace csDriverOEMPA
#endif //_DRIVER_11XY_
{
////////////////////////////////////////////////////////
#pragma region csSWDeviceOEMPA2
	csSWDeviceOEMPA2::csSWDeviceOEMPA2()
	{
		Free();
	}
	csSWDeviceOEMPA2::~csSWDeviceOEMPA2()
	{
		this->!csSWDeviceOEMPA2();
	}
	csSWDeviceOEMPA2::!csSWDeviceOEMPA2()
	{
		Free();
	}
	void csSWDeviceOEMPA2::Constructor(CHWDeviceOEMPA2 *pHWDeviceOEMPA2,CSWDeviceOEMPA2 *pSWDeviceOEMPA2)
	{
		m_pHWDeviceOEMPA2 = pHWDeviceOEMPA2;
		m_pSWDeviceOEMPA2 = pSWDeviceOEMPA2;
	}
	void csSWDeviceOEMPA2::Free()
	{
		m_pHWDeviceOEMPA2 = NULL;
		m_pSWDeviceOEMPA2 = NULL;
	}

//ethernet
	bool csSWDeviceOEMPA2::SetIPAddress(String ^pValue)
	{
		wchar_t* y;
		bool bRet;

		if(!m_pSWDeviceOEMPA2)
			return false;
		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pValue);
		bRet = m_pSWDeviceOEMPA2->SetIPAddress(y);
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}
	bool csSWDeviceOEMPA2::GetIPAddress([Out] String^ %pValue)
	{
		wchar_t pAux[MAX_PATH];

		if(!m_pSWDeviceOEMPA2)
			return false;
		if(!m_pSWDeviceOEMPA2->GetIPAddress(pAux,MAX_PATH))
			return false;
		pValue = Marshal::PtrToStringUni((IntPtr)pAux);
		return true;
	}

	bool csSWDeviceOEMPA2::SetPort(unsigned short usValue)
	{
		return _SetPort(usValue);
	}
	bool csSWDeviceOEMPA2::_SetPort(unsigned short usValue)
	{
		if(!m_pSWDeviceOEMPA2)
			return false;
		return m_pSWDeviceOEMPA2->_SetPort(usValue);//same than "SetPort", required because of the preprocessor definition in "WinSpool.h": "#define SetPort SetPortW".
	}
	bool csSWDeviceOEMPA2::GetPort(unsigned short %usValue)
	{
		unsigned short usValue2;
		bool bRet;

		if(!m_pSWDeviceOEMPA2)
			return false;
		bRet = m_pSWDeviceOEMPA2->GetPort(usValue2);
		usValue = usValue2;
		return bRet;
	}

	//int csSWDeviceOEMPA2::GetApertureCountMax()//to get the maximum element count of an aperture.
	//{
	//	if(!m_pSWDeviceOEMPA2)
	//		return 0;
	//	return m_pSWDeviceOEMPA2->GetApertureCountMax();
	//}
	//int csSWDeviceOEMPA2::GetElementCountMax()//to get the maximum element count of the system (in case of mux).
	//{
	//	if(!m_pSWDeviceOEMPA2)
	//		return 0;
	//	return m_pSWDeviceOEMPA2->GetElementCountMax();
	//}

	//void csSWDeviceOEMPA2::AlignmentCfgUpdated()
	//{
	//	CSWDeviceOEMPA2::AlignmentCfgUpdated();
	//}
	//void csSWDeviceOEMPA2::SetCalibrationParameters(float fWidth,float fStart,float fRange,float fGainAnalog,double dGainDigital)
	//{
	//	if(!m_pSWDeviceOEMPA2)
	//		return;
	//	return m_pSWDeviceOEMPA2->SetCalibrationParameters(fWidth,fStart,fRange,fGainAnalog,dGainDigital);
	//}
	//void csSWDeviceOEMPA2::GetCalibrationParameters(float &fWidth,float &fStart,float &fRange,float &fGainAnalog,double &dGainDigital)
	//{
	//	if(!m_pSWDeviceOEMPA2)
	//		return;
	//	return m_pSWDeviceOEMPA2->GetCalibrationParameters(fWidth,fStart,fRange,fGainAnalog,dGainDigital);
	//}
	//bool csSWDeviceOEMPA2::IsCalibrationPerformed()
	//{
	//	return IsAlignmentPerformed();
	//}
	//bool csSWDeviceOEMPA2::IsAlignmentPerformed()
	//{
	//	if(!m_pSWDeviceOEMPA2)
	//		return false;
	//	return m_pSWDeviceOEMPA2->IsAlignmentPerformed();
	//}
	//bool csSWDeviceOEMPA2::EnableAlignment(bool bEnable)
	//{
	//	if(!m_pSWDeviceOEMPA2)
	//		return false;
	//	return m_pSWDeviceOEMPA2->EnableAlignment(bEnable);
	//}
	//float csSWDeviceOEMPA2::GetCalibrationAlignment()
	//{
	//	if(!m_pSWDeviceOEMPA2)
	//		return 0.0f;
	//	return m_pSWDeviceOEMPA2->GetCalibrationAlignment();
	//}
	//float csSWDeviceOEMPA2::GetCalibrationOffset()
	//{
	//	if(!m_pSWDeviceOEMPA2)
	//		return 0.0f;
	//	return m_pSWDeviceOEMPA2->GetCalibrationOffset();
	//}
	//bool csSWDeviceOEMPA2::IsAlignmentEnabled()
	//{
	//	if(!m_pSWDeviceOEMPA2)
	//		return false;
	//	return m_pSWDeviceOEMPA2->IsAlignmentEnabled();
	//}
	//bool csSWDeviceOEMPA2::ResetAlignment()
	//{
	//	if(!m_pSWDeviceOEMPA2)
	//		return false;
	//	return m_pSWDeviceOEMPA2->ResetAlignment();
	//}
	//bool csSWDeviceOEMPA2::SetCalibrationFileReport(String ^pFileReport)
	//{
	//	wchar_t* y;

	//	if(!m_pSWDeviceOEMPA2)
	//		return false;
	//	y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pFileReport);
	//	m_pSWDeviceOEMPA2->SetCalibrationFileReport(y);
	//	Marshal::FreeHGlobal((IntPtr)y);
	//	return true;
	//}
	//bool csSWDeviceOEMPA2::GetCalibrationFileReport([Out] String^ %pFileReport)
	//{
	//	wchar_t pAux[MAX_PATH];

	//	if(!m_pSWDeviceOEMPA2)
	//		return false;
	//	if(!m_pSWDeviceOEMPA2->GetCalibrationFileReport(MAX_PATH,pAux))
	//		return false;
	//	pFileReport = Marshal::PtrToStringUni((IntPtr)pAux);
	//	return true;
	//}
#pragma endregion csSWDeviceOEMPA2
////////////////////////////////////////////////////////
#pragma region csHWDeviceOEMPA2
	csHWDeviceOEMPA2::csHWDeviceOEMPA2() :
#ifndef _DRIVER_11XY_
											csHWDeviceOEMPA(csEnumHardware::csOEMPA2,false, false)
#else //_DRIVER_11XY_
											csHWDeviceOEM(csEnumHardware::csOEMPA2,true, false)
#endif //_DRIVER_11XY_
	{
		bool bCreate;
		int iValue = CSWDevice::ReadCfgInt(L"UTKernel" LS L"Device",L"csDriverOEMPA2Count",0,bCreate);

		iValue++;
		CSWDevice::WriteCfgInt(L"UTKernel" LS L"Device",L"csDriverOEMPA2Count",iValue);
		m_pHWDeviceOEMPA2 = dynamic_cast<CHWDeviceOEMPA2*>((CHWDeviceOEMPA*)cGetHWDeviceOEMPA());//new CHWDeviceOEMPA2();
#ifdef _DRIVER_11XY_
		m_csCustomizedAPI = gcnew csCustomizedAPI(m_pHWDeviceOEMPA2);
		csHWDeviceOEM::SetCustomizedOEMPA(dynamic_cast<csCustomizedOEMPA^>(m_csCustomizedAPI));
#endif //_DRIVER_11XY_
		m_pHWDevice = dynamic_cast<CHWDevice*>(m_pHWDeviceOEMPA2);
		m_pSWDeviceOEMPA2 = m_pHWDeviceOEMPA2->GetSWDeviceOEMPA();
		csHWDevice::Constructor(m_pHWDeviceOEMPA2,m_pHWDevice);
		csHWDevice::Constructor(GetCustomizedOEMPA());
		m_csSWDeviceOEMPA2 = gcnew csSWDeviceOEMPA2();
		m_csSWDeviceOEMPA2->Constructor(m_pHWDeviceOEMPA2,m_pSWDeviceOEMPA2);
		if(m_pHWDevice)
			m_pHWDevice->SetDerivedClass(L"csHWDeviceOEMPA2",NULL);
	}
	csHWDeviceOEMPA2::~csHWDeviceOEMPA2()
	{
		this->!csHWDeviceOEMPA2();
	}
	csHWDeviceOEMPA2::!csHWDeviceOEMPA2()
	{
		Free();
	}
	void csHWDeviceOEMPA2::Free()
	{
		m_pSWDeviceOEMPA2 = NULL;
		m_pHWDeviceOEMPA2 = NULL;
	}
	csSWDeviceOEMPA2^ csHWDeviceOEMPA2::GetSWDeviceOEMPA2()
	{
		return m_csSWDeviceOEMPA2;
	}
	csSWDeviceOEMPA2^ csHWDeviceOEMPA2::GetSWDeviceOEMPA()
	{
		return m_csSWDeviceOEMPA2;
	}

	/*unsafe*/bool csHWDeviceOEMPA2::GetApertureCountMax(int *piCount)
	{
		if(!m_pHWDeviceOEMPA2)
			return false;
		return m_pHWDeviceOEMPA2->GetApertureCountMax(piCount);
	}
	/*unsafe*/bool csHWDeviceOEMPA2::GetElementCountMax(int *piCount)
	{
		if(!m_pHWDeviceOEMPA2)
			return false;
		return m_pHWDeviceOEMPA2->GetElementCountMax(piCount);
	}

	void/*CHWDeviceOEMPA2*/ *csHWDeviceOEMPA2::GetHWDeviceOEMPA2()
	{
		return m_pHWDeviceOEMPA2;
	}

	csHWDevice ^csHWDeviceOEMPA2::GetHWDevice()
	{
		return this;
	}
#pragma endregion csHWDeviceOEMPA2
////////////////////////////////////////////////////////
////////////////////////////////////////////////////////

}
