#include "stdafx.h"
#include <gcroot.h>

#pragma managed(push, off)
#ifdef _DRIVER_11XY_
#include "UTKernelDriver.h"
#include "UTKernelDriverOEMPA.h"
#else //_DRIVER_11XY_
#include "UTDriverOEMPA.h"
#include "UTDriverOEMPAmax.h"
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
	ref class csHWDeviceOEMPAmax;

	public ref class csCustomizedAPI : public csCustomizedOEMPA
	{
	private:
		CHWDeviceOEMPAmax *m_pHWDeviceOEMPAmax;
	public:
		csCustomizedAPI(CHWDeviceOEMPAmax *pHWDeviceOEMPAmax);
		~csCustomizedAPI();
		void Free();

		bool WriteHW(csHWDeviceOEMPAmax^ %pHWDeviceOEMPA,csRoot^ %root,cli::array<csCycle^>^ %cycle,cli::array<csFocalLaw^>^ %emission,cli::array<csFocalLaw^>^ %reception,csEnumAcquisitionState eAcqState);
	protected:
		!csCustomizedAPI();
	};

};

namespace csDriverOEMPA
#else //_DRIVER_11XY_

namespace csDriverOEMPAmax
#endif //_DRIVER_11XY_
{
//////////////////////////////////////////////////////////////////////////
#pragma region csSWDeviceOEMPAmax
	public ref class csSWDeviceOEMPAmax :
#ifndef _DRIVER_11XY_
										public csSWDeviceOEMPA
#else //_DRIVER_11XY_
										public csSWDeviceOEM
#endif //_DRIVER_11XY_
    {
	private:
		CHWDeviceOEMPAmax *m_pHWDeviceOEMPAmax;
		CSWDeviceOEMPAmax *m_pSWDeviceOEMPAmax;
	public:
		csSWDeviceOEMPAmax();
		~csSWDeviceOEMPAmax();
		void Constructor(CHWDeviceOEMPAmax *pHWDeviceOEMPAmax,CSWDeviceOEMPAmax *pSWDeviceOEMPAmax);
		void Free();

	//ethernet
		bool SetIPAddress(String ^pValue);
		bool GetIPAddress([Out] String^ %pValue);

		bool SetPort(unsigned short usValue);
		bool _SetPort(unsigned short usValue);//same than "SetPort", required because of the preprocessor definition in "WinSpool.h": "#define SetPort SetPortW".
		bool GetPort(unsigned short %usValue);

	protected:
		!csSWDeviceOEMPAmax();
	};
#pragma endregion csSWDeviceOEMPAmax

#pragma region csHWDeviceOEMPAmax
#ifndef _DRIVER_11XY_
	public ref class csHWDeviceOEMPAmax:	public csHWDeviceOEMPA
#else //_DRIVER_11XY_
	public ref class csHWDeviceOEMPA:		public csHWDeviceOEM
#endif //_DRIVER_11XY_
    {
	private:
#ifdef _DRIVER_11XY_
		csCustomizedAPI ^m_csCustomizedAPI;
#endif //_DRIVER_11XY_
		csSWDeviceOEMPAmax ^m_csSWDeviceOEMPAmax;
		CHWDeviceOEMPAmax *m_pHWDeviceOEMPAmax;
		CHWDevice *m_pHWDevice;
		CSWDeviceOEMPAmax *m_pSWDeviceOEMPAmax;
	public:
		csHWDeviceOEMPAmax();
		~csHWDeviceOEMPAmax();
		void Free();
		//csSWDeviceOEMPAmax^ GetSWDeviceOEMPAmini();
		csSWDeviceOEMPAmax^ GetSWDeviceOEMPAmax();
		csHWDevice ^GetHWDevice();
		csSWDeviceOEMPAmax ^GetSWDeviceOEMPA();
#ifdef _DRIVER_11XY_
		csCustomizedAPI ^GetCustomizedAPI();
#endif //_DRIVER_11XY_

		/*unsafe*/bool GetApertureCountMax(int *piCount);//to get the maximum element count of an aperture.
		/*unsafe*/bool GetElementCountMax(int *piCount);//to get the maximum element count of the system (in case of mux).
			//Output parameters
			//	piCount : maximum aperture size.
	
		void/*CHWDeviceOEMPAmax*/ *GetHWDeviceOEMPAmax();
	protected:
		!csHWDeviceOEMPAmax();
	};
#pragma endregion hwDriverOEMPAmax
//////////////////////////////////////////////////////////////////////////

};

#ifndef _DRIVER_11XY_
namespace csDriverOEMPAmax
#else //_DRIVER_11XY_
namespace csDriverOEMPA
#endif //_DRIVER_11XY_
{
////////////////////////////////////////////////////////
#pragma region csSWDeviceOEMPAmax
	csSWDeviceOEMPAmax::csSWDeviceOEMPAmax()
	{
		Free();
	}
	csSWDeviceOEMPAmax::~csSWDeviceOEMPAmax()
	{
		this->!csSWDeviceOEMPAmax();
	}
	csSWDeviceOEMPAmax::!csSWDeviceOEMPAmax()
	{
		Free();
	}
	void csSWDeviceOEMPAmax::Constructor(CHWDeviceOEMPAmax *pHWDeviceOEMPAmax,CSWDeviceOEMPAmax *pSWDeviceOEMPAmax)
	{
		m_pHWDeviceOEMPAmax = pHWDeviceOEMPAmax;
		m_pSWDeviceOEMPAmax = pSWDeviceOEMPAmax;
	}
	void csSWDeviceOEMPAmax::Free()
	{
		m_pHWDeviceOEMPAmax = NULL;
		m_pSWDeviceOEMPAmax = NULL;
	}

//ethernet
	bool csSWDeviceOEMPAmax::SetIPAddress(String ^pValue)
	{
		wchar_t* y;
		bool bRet;

		if(!m_pSWDeviceOEMPAmax)
			return false;
		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pValue);
		bRet = m_pSWDeviceOEMPAmax->SetIPAddress(y);
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}
	bool csSWDeviceOEMPAmax::GetIPAddress([Out] String^ %pValue)
	{
		wchar_t pAux[MAX_PATH];

		if(!m_pSWDeviceOEMPAmax)
			return false;
		if(!m_pSWDeviceOEMPAmax->GetIPAddress(pAux,MAX_PATH))
			return false;
		pValue = Marshal::PtrToStringUni((IntPtr)pAux);
		return true;
	}

	bool csSWDeviceOEMPAmax::SetPort(unsigned short usValue)
	{
		return _SetPort(usValue);
	}
	bool csSWDeviceOEMPAmax::_SetPort(unsigned short usValue)
	{
		if(!m_pSWDeviceOEMPAmax)
			return false;
		return m_pSWDeviceOEMPAmax->_SetPort(usValue);//same than "SetPort", required because of the preprocessor definition in "WinSpool.h": "#define SetPort SetPortW".
	}
	bool csSWDeviceOEMPAmax::GetPort(unsigned short %usValue)
	{
		unsigned short usValue2;
		bool bRet;

		if(!m_pSWDeviceOEMPAmax)
			return false;
		bRet = m_pSWDeviceOEMPAmax->GetPort(usValue2);
		usValue = usValue2;
		return bRet;
	}

	//int csSWDeviceOEMPAmax::GetApertureCountMax()//to get the maximum element count of an aperture.
	//{
	//	if(!m_pSWDeviceOEMPAmax)
	//		return 0;
	//	return m_pSWDeviceOEMPAmax->GetApertureCountMax();
	//}
	//int csSWDeviceOEMPAmax::GetElementCountMax()//to get the maximum element count of the system (in case of mux).
	//{
	//	if(!m_pSWDeviceOEMPAmax)
	//		return 0;
	//	return m_pSWDeviceOEMPAmax->GetElementCountMax();
	//}

	//void csSWDeviceOEMPAmax::AlignmentCfgUpdated()
	//{
	//	CSWDeviceOEMPAmax::AlignmentCfgUpdated();
	//}
	//void csSWDeviceOEMPAmax::SetCalibrationParameters(float fWidth,float fStart,float fRange,float fGainAnalog,double dGainDigital)
	//{
	//	if(!m_pSWDeviceOEMPAmax)
	//		return;
	//	return m_pSWDeviceOEMPAmax->SetCalibrationParameters(fWidth,fStart,fRange,fGainAnalog,dGainDigital);
	//}
	//void csSWDeviceOEMPAmax::GetCalibrationParameters(float &fWidth,float &fStart,float &fRange,float &fGainAnalog,double &dGainDigital)
	//{
	//	if(!m_pSWDeviceOEMPAmax)
	//		return;
	//	return m_pSWDeviceOEMPAmax->GetCalibrationParameters(fWidth,fStart,fRange,fGainAnalog,dGainDigital);
	//}
	//bool csSWDeviceOEMPAmax::IsCalibrationPerformed()
	//{
	//	return IsAlignmentPerformed();
	//}
	//bool csSWDeviceOEMPAmax::IsAlignmentPerformed()
	//{
	//	if(!m_pSWDeviceOEMPAmax)
	//		return false;
	//	return m_pSWDeviceOEMPAmax->IsAlignmentPerformed();
	//}
	//bool csSWDeviceOEMPAmax::EnableAlignment(bool bEnable)
	//{
	//	if(!m_pSWDeviceOEMPAmax)
	//		return false;
	//	return m_pSWDeviceOEMPAmax->EnableAlignment(bEnable);
	//}
	//float csSWDeviceOEMPAmax::GetCalibrationAlignment()
	//{
	//	if(!m_pSWDeviceOEMPAmax)
	//		return 0.0f;
	//	return m_pSWDeviceOEMPAmax->GetCalibrationAlignment();
	//}
	//float csSWDeviceOEMPAmax::GetCalibrationOffset()
	//{
	//	if(!m_pSWDeviceOEMPAmax)
	//		return 0.0f;
	//	return m_pSWDeviceOEMPAmax->GetCalibrationOffset();
	//}
	//bool csSWDeviceOEMPAmax::IsAlignmentEnabled()
	//{
	//	if(!m_pSWDeviceOEMPAmax)
	//		return false;
	//	return m_pSWDeviceOEMPAmax->IsAlignmentEnabled();
	//}
	//bool csSWDeviceOEMPAmax::ResetAlignment()
	//{
	//	if(!m_pSWDeviceOEMPAmax)
	//		return false;
	//	return m_pSWDeviceOEMPAmax->ResetAlignment();
	//}
	//bool csSWDeviceOEMPAmax::SetCalibrationFileReport(String ^pFileReport)
	//{
	//	wchar_t* y;

	//	if(!m_pSWDeviceOEMPAmax)
	//		return false;
	//	y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pFileReport);
	//	m_pSWDeviceOEMPAmax->SetCalibrationFileReport(y);
	//	Marshal::FreeHGlobal((IntPtr)y);
	//	return true;
	//}
	//bool csSWDeviceOEMPAmax::GetCalibrationFileReport([Out] String^ %pFileReport)
	//{
	//	wchar_t pAux[MAX_PATH];

	//	if(!m_pSWDeviceOEMPAmax)
	//		return false;
	//	if(!m_pSWDeviceOEMPAmax->GetCalibrationFileReport(MAX_PATH,pAux))
	//		return false;
	//	pFileReport = Marshal::PtrToStringUni((IntPtr)pAux);
	//	return true;
	//}
#pragma endregion csSWDeviceOEMPAmax
////////////////////////////////////////////////////////
#pragma region csHWDeviceOEMPAmax
	csHWDeviceOEMPAmax::csHWDeviceOEMPAmax() :
#ifndef _DRIVER_11XY_
											csHWDeviceOEMPA(csEnumHardware::csOEMPAmax)
#else //_DRIVER_11XY_
											csHWDeviceOEM(csEnumHardware::csOEMPAmax,true, false)
#endif //_DRIVER_11XY_
	{
		bool bCreate;
		int iValue = CSWDevice::ReadCfgInt(L"UTKernel" LS L"Device",L"csDriverOEMPAmaxCount",0,bCreate);
		iValue++;
		CSWDevice::WriteCfgInt(L"UTKernel" LS L"Device",L"csDriverOEMPAmaxCount",iValue);
		m_pHWDeviceOEMPAmax = dynamic_cast<CHWDeviceOEMPAmax*>((CHWDeviceOEMPA*)cGetHWDeviceOEMPA());//new CHWDeviceOEMPAmax();
#ifdef _DRIVER_11XY_
		m_csCustomizedAPI = gcnew csCustomizedAPI(m_pHWDeviceOEMPAmax);
		csHWDeviceOEM::SetCustomizedOEMPA(dynamic_cast<csCustomizedOEMPA^>(m_csCustomizedAPI));
#endif //_DRIVER_11XY_
		m_pHWDevice = dynamic_cast<CHWDevice*>(m_pHWDeviceOEMPAmax);
		m_pSWDeviceOEMPAmax = m_pHWDeviceOEMPAmax->GetSWDeviceOEMPA();
		csHWDevice::Constructor(m_pHWDeviceOEMPAmax,m_pHWDevice);
		csHWDevice::Constructor(GetCustomizedOEMPA());
		m_csSWDeviceOEMPAmax = gcnew csSWDeviceOEMPAmax();
		m_csSWDeviceOEMPAmax->Constructor(m_pHWDeviceOEMPAmax,m_pSWDeviceOEMPAmax);
		if(m_pHWDevice)
			m_pHWDevice->SetDerivedClass(L"csHWDeviceOEMPAmax",NULL);
	}
	csHWDeviceOEMPAmax::~csHWDeviceOEMPAmax()
	{
		this->!csHWDeviceOEMPAmax();
	}
	csHWDeviceOEMPAmax::!csHWDeviceOEMPAmax()
	{
		Free();
	}
	void csHWDeviceOEMPAmax::Free()
	{
		m_pSWDeviceOEMPAmax = NULL;
		m_pHWDeviceOEMPAmax = NULL;
	}
	//csSWDeviceOEMPAmax^ csHWDeviceOEMPAmax::GetSWDeviceOEMPAmini()
	//{
	//	return m_csSWDeviceOEMPAmax;
	//}
	csSWDeviceOEMPAmax^ csHWDeviceOEMPAmax::GetSWDeviceOEMPAmax()
	{
		return m_csSWDeviceOEMPAmax;
	}
	csSWDeviceOEMPAmax^ csHWDeviceOEMPAmax::GetSWDeviceOEMPA()
	{
		return m_csSWDeviceOEMPAmax;
	}

	/*unsafe*/bool csHWDeviceOEMPAmax::GetApertureCountMax(int *piCount)
	{
		if(!m_pHWDeviceOEMPAmax)
			return false;
		return m_pHWDeviceOEMPAmax->GetApertureCountMax(piCount);
	}
	/*unsafe*/bool csHWDeviceOEMPAmax::GetElementCountMax(int *piCount)
	{
		if(!m_pHWDeviceOEMPAmax)
			return false;
		return m_pHWDeviceOEMPAmax->GetElementCountMax(piCount);
	}

	void/*CHWDeviceOEMPAmax*/ *csHWDeviceOEMPAmax::GetHWDeviceOEMPAmax()
	{
		return m_pHWDeviceOEMPAmax;
	}

	csHWDevice ^csHWDeviceOEMPAmax::GetHWDevice()
	{
		return this;
	}
#pragma endregion csHWDeviceOEMPAmax
////////////////////////////////////////////////////////
////////////////////////////////////////////////////////

}
