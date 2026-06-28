#include "stdafx.h"
#include <gcroot.h>

#pragma managed(push, off)
#ifdef _DRIVER_11XY_
#include "UTKernelDriver.h"
#include "UTKernelDriverOEMPA.h"
#else //_DRIVER_11XY_
#include "UTDriverOEMPA.h"
#include "UTDriverOEMPAmini.h"
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
	ref class csHWDeviceOEMPAmini;

	public ref class csCustomizedAPI : public csCustomizedOEMPA
	{
	private:
		CHWDeviceOEMPAmini *m_pHWDeviceOEMPAmini;
	public:
		csCustomizedAPI(CHWDeviceOEMPAmini *pHWDeviceOEMPAmini);
		~csCustomizedAPI();
		void Free();

		bool WriteHW(csHWDeviceOEMPAmini^ %pHWDeviceOEMPA,csRoot^ %root,cli::array<csCycle^>^ %cycle,cli::array<csFocalLaw^>^ %emission,cli::array<csFocalLaw^>^ %reception,csEnumAcquisitionState eAcqState);
	protected:
		!csCustomizedAPI();
	};

};

namespace csDriverOEMPA
#else //_DRIVER_11XY_

namespace csDriverOEMPAmini
#endif //_DRIVER_11XY_
{
//////////////////////////////////////////////////////////////////////////
#pragma region csSWDeviceOEMPAmini
	public ref class csSWDeviceOEMPAmini :
#ifndef _DRIVER_11XY_
										public csSWDeviceOEMPA
#else //_DRIVER_11XY_
										public csSWDeviceOEM
#endif //_DRIVER_11XY_
    {
	private:
		CHWDeviceOEMPAmini *m_pHWDeviceOEMPAmini;
		CSWDeviceOEMPAmini *m_pSWDeviceOEMPAmini;
	public:
		csSWDeviceOEMPAmini();
		~csSWDeviceOEMPAmini();
		void Constructor(CHWDeviceOEMPAmini *pHWDeviceOEMPAmini,CSWDeviceOEMPAmini *pSWDeviceOEMPAmini);
		void Free();

	//ethernet
		bool SetIPAddress(String ^pValue);
		bool GetIPAddress([Out] String^ %pValue);

		bool SetPort(unsigned short usValue);
		bool _SetPort(unsigned short usValue);//same than "SetPort", required because of the preprocessor definition in "WinSpool.h": "#define SetPort SetPortW".
		bool GetPort(unsigned short %usValue);

	protected:
		!csSWDeviceOEMPAmini();
	};
#pragma endregion csSWDeviceOEMPAmini

#pragma region csHWDeviceOEMPAmini
#ifndef _DRIVER_11XY_
	public ref class csHWDeviceOEMPAmini:	public csHWDeviceOEMPA
#else //_DRIVER_11XY_
	public ref class csHWDeviceOEMPA:		public csHWDeviceOEM
#endif //_DRIVER_11XY_
    {
	private:
#ifdef _DRIVER_11XY_
		csCustomizedAPI ^m_csCustomizedAPI;
#endif //_DRIVER_11XY_
		csSWDeviceOEMPAmini ^m_csSWDeviceOEMPAmini;
		CHWDeviceOEMPAmini *m_pHWDeviceOEMPAmini;
		CHWDevice *m_pHWDevice;
		CSWDeviceOEMPAmini *m_pSWDeviceOEMPAmini;
	public:
		csHWDeviceOEMPAmini();
		csHWDeviceOEMPAmini(bool bTCP);
		~csHWDeviceOEMPAmini();
		void Free();
		csSWDeviceOEMPAmini^ GetSWDeviceOEMPAmini();
		csSWDeviceOEMPAmini^ GetSWDeviceOEMPAmax();
		csHWDevice ^GetHWDevice();
		csSWDeviceOEMPAmini ^GetSWDeviceOEMPA();
#ifdef _DRIVER_11XY_
		csCustomizedAPI ^GetCustomizedAPI();
#endif //_DRIVER_11XY_

		/*unsafe*/bool GetApertureCountMax(int *piCount);//to get the maximum element count of an aperture.
		/*unsafe*/bool GetElementCountMax(int *piCount);//to get the maximum element count of the system (in case of mux).
			//Output parameters
			//	piCount : maximum aperture size.
	
		void/*CHWDeviceOEMPAmini*/ *GetHWDeviceOEMPAmini();
	protected:
		!csHWDeviceOEMPAmini();
	};
#pragma endregion hwDriverOEMPAmini
//////////////////////////////////////////////////////////////////////////

};

#ifndef _DRIVER_11XY_
namespace csDriverOEMPAmini
#else //_DRIVER_11XY_
namespace csDriverOEMPA
#endif //_DRIVER_11XY_
{
////////////////////////////////////////////////////////
#pragma region csSWDeviceOEMPAmini
	csSWDeviceOEMPAmini::csSWDeviceOEMPAmini()
	{
		Free();
	}
	csSWDeviceOEMPAmini::~csSWDeviceOEMPAmini()
	{
		this->!csSWDeviceOEMPAmini();
	}
	csSWDeviceOEMPAmini::!csSWDeviceOEMPAmini()
	{
		Free();
	}
	void csSWDeviceOEMPAmini::Constructor(CHWDeviceOEMPAmini *pHWDeviceOEMPAmini,CSWDeviceOEMPAmini *pSWDeviceOEMPAmini)
	{
		m_pHWDeviceOEMPAmini = pHWDeviceOEMPAmini;
		m_pSWDeviceOEMPAmini = pSWDeviceOEMPAmini;
	}
	void csSWDeviceOEMPAmini::Free()
	{
		m_pHWDeviceOEMPAmini = NULL;
		m_pSWDeviceOEMPAmini = NULL;
	}

//ethernet
	bool csSWDeviceOEMPAmini::SetIPAddress(String ^pValue)
	{
		wchar_t* y;
		bool bRet;

		if(!m_pSWDeviceOEMPAmini)
			return false;
		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pValue);
		bRet = m_pSWDeviceOEMPAmini->SetIPAddress(y);
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}
	bool csSWDeviceOEMPAmini::GetIPAddress([Out] String^ %pValue)
	{
		wchar_t pAux[MAX_PATH];

		if(!m_pSWDeviceOEMPAmini)
			return false;
		if(!m_pSWDeviceOEMPAmini->GetIPAddress(pAux,MAX_PATH))
			return false;
		pValue = Marshal::PtrToStringUni((IntPtr)pAux);
		return true;
	}

	bool csSWDeviceOEMPAmini::SetPort(unsigned short usValue)
	{
		return _SetPort(usValue);
	}
	bool csSWDeviceOEMPAmini::_SetPort(unsigned short usValue)
	{
		if(!m_pSWDeviceOEMPAmini)
			return false;
		return m_pSWDeviceOEMPAmini->_SetPort(usValue);//same than "SetPort", required because of the preprocessor definition in "WinSpool.h": "#define SetPort SetPortW".
	}
	bool csSWDeviceOEMPAmini::GetPort(unsigned short %usValue)
	{
		unsigned short usValue2;
		bool bRet;

		if(!m_pSWDeviceOEMPAmini)
			return false;
		bRet = m_pSWDeviceOEMPAmini->GetPort(usValue2);
		usValue = usValue2;
		return bRet;
	}

	//int csSWDeviceOEMPAmini::GetApertureCountMax()//to get the maximum element count of an aperture.
	//{
	//	if(!m_pSWDeviceOEMPAmini)
	//		return 0;
	//	return m_pSWDeviceOEMPAmini->GetApertureCountMax();
	//}
	//int csSWDeviceOEMPAmini::GetElementCountMax()//to get the maximum element count of the system (in case of mux).
	//{
	//	if(!m_pSWDeviceOEMPAmini)
	//		return 0;
	//	return m_pSWDeviceOEMPAmini->GetElementCountMax();
	//}

	//void csSWDeviceOEMPAmini::AlignmentCfgUpdated()
	//{
	//	CSWDeviceOEMPAmini::AlignmentCfgUpdated();
	//}
	//void csSWDeviceOEMPAmini::SetCalibrationParameters(float fWidth,float fStart,float fRange,float fGainAnalog,double dGainDigital)
	//{
	//	if(!m_pSWDeviceOEMPAmini)
	//		return;
	//	return m_pSWDeviceOEMPAmini->SetCalibrationParameters(fWidth,fStart,fRange,fGainAnalog,dGainDigital);
	//}
	//void csSWDeviceOEMPAmini::GetCalibrationParameters(float &fWidth,float &fStart,float &fRange,float &fGainAnalog,double &dGainDigital)
	//{
	//	if(!m_pSWDeviceOEMPAmini)
	//		return;
	//	return m_pSWDeviceOEMPAmini->GetCalibrationParameters(fWidth,fStart,fRange,fGainAnalog,dGainDigital);
	//}
	//bool csSWDeviceOEMPAmini::IsCalibrationPerformed()
	//{
	//	return IsAlignmentPerformed();
	//}
	//bool csSWDeviceOEMPAmini::IsAlignmentPerformed()
	//{
	//	if(!m_pSWDeviceOEMPAmini)
	//		return false;
	//	return m_pSWDeviceOEMPAmini->IsAlignmentPerformed();
	//}
	//bool csSWDeviceOEMPAmini::EnableAlignment(bool bEnable)
	//{
	//	if(!m_pSWDeviceOEMPAmini)
	//		return false;
	//	return m_pSWDeviceOEMPAmini->EnableAlignment(bEnable);
	//}
	//float csSWDeviceOEMPAmini::GetCalibrationAlignment()
	//{
	//	if(!m_pSWDeviceOEMPAmini)
	//		return 0.0f;
	//	return m_pSWDeviceOEMPAmini->GetCalibrationAlignment();
	//}
	//float csSWDeviceOEMPAmini::GetCalibrationOffset()
	//{
	//	if(!m_pSWDeviceOEMPAmini)
	//		return 0.0f;
	//	return m_pSWDeviceOEMPAmini->GetCalibrationOffset();
	//}
	//bool csSWDeviceOEMPAmini::IsAlignmentEnabled()
	//{
	//	if(!m_pSWDeviceOEMPAmini)
	//		return false;
	//	return m_pSWDeviceOEMPAmini->IsAlignmentEnabled();
	//}
	//bool csSWDeviceOEMPAmini::ResetAlignment()
	//{
	//	if(!m_pSWDeviceOEMPAmini)
	//		return false;
	//	return m_pSWDeviceOEMPAmini->ResetAlignment();
	//}
	//bool csSWDeviceOEMPAmini::SetCalibrationFileReport(String ^pFileReport)
	//{
	//	wchar_t* y;

	//	if(!m_pSWDeviceOEMPAmini)
	//		return false;
	//	y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pFileReport);
	//	m_pSWDeviceOEMPAmini->SetCalibrationFileReport(y);
	//	Marshal::FreeHGlobal((IntPtr)y);
	//	return true;
	//}
	//bool csSWDeviceOEMPAmini::GetCalibrationFileReport([Out] String^ %pFileReport)
	//{
	//	wchar_t pAux[MAX_PATH];

	//	if(!m_pSWDeviceOEMPAmini)
	//		return false;
	//	if(!m_pSWDeviceOEMPAmini->GetCalibrationFileReport(MAX_PATH,pAux))
	//		return false;
	//	pFileReport = Marshal::PtrToStringUni((IntPtr)pAux);
	//	return true;
	//}
#pragma endregion csSWDeviceOEMPAmini
////////////////////////////////////////////////////////
#pragma region csHWDeviceOEMPAmini
	csHWDeviceOEMPAmini::csHWDeviceOEMPAmini() :
#ifndef _DRIVER_11XY_
											csHWDeviceOEMPA(csEnumHardware::csOEMPAmini)
#else //_DRIVER_11XY_
											csHWDeviceOEM(csEnumHardware::csOEMPAmini,true, false)
#endif //_DRIVER_11XY_
	{
		bool bCreate;
		int iValue = CSWDevice::ReadCfgInt(L"UTKernel" LS L"Device",L"csDriverOEMPAminiCount",0,bCreate);
		iValue++;
		CSWDevice::WriteCfgInt(L"UTKernel" LS L"Device",L"csDriverOEMPAminiCount",iValue);
		m_pHWDeviceOEMPAmini = dynamic_cast<CHWDeviceOEMPAmini*>((CHWDeviceOEMPA*)cGetHWDeviceOEMPA());//new CHWDeviceOEMPAmini();
#ifdef _DRIVER_11XY_
		m_csCustomizedAPI = gcnew csCustomizedAPI(m_pHWDeviceOEMPAmini);
		csHWDeviceOEM::SetCustomizedOEMPA(dynamic_cast<csCustomizedOEMPA^>(m_csCustomizedAPI));
#endif //_DRIVER_11XY_
		m_pHWDevice = dynamic_cast<CHWDevice*>(m_pHWDeviceOEMPAmini);
		m_pSWDeviceOEMPAmini = m_pHWDeviceOEMPAmini->GetSWDeviceOEMPA();
		csHWDevice::Constructor(m_pHWDeviceOEMPAmini,m_pHWDevice);
		csHWDevice::Constructor(GetCustomizedOEMPA());
		m_csSWDeviceOEMPAmini = gcnew csSWDeviceOEMPAmini();
		m_csSWDeviceOEMPAmini->Constructor(m_pHWDeviceOEMPAmini,m_pSWDeviceOEMPAmini);
		if(m_pHWDevice)
			m_pHWDevice->SetDerivedClass(L"csHWDeviceOEMPAmini",NULL);
	}
	csHWDeviceOEMPAmini::csHWDeviceOEMPAmini(bool bTCP) :
#ifndef _DRIVER_11XY_
		csHWDeviceOEMPA(csEnumHardware::csOEMPAmini, false, bTCP)
#else //_DRIVER_11XY_
		csHWDeviceOEM(csEnumHardware::csOEMPAmini, true, bTCP)
#endif //_DRIVER_11XY_
	{
		bool bCreate;
		int iValue = CSWDevice::ReadCfgInt(L"UTKernel" LS L"Device", L"csDriverOEMPAminiCount", 0, bCreate);
		iValue++;
		CSWDevice::WriteCfgInt(L"UTKernel" LS L"Device", L"csDriverOEMPAminiCount", iValue);
		m_pHWDeviceOEMPAmini = dynamic_cast<CHWDeviceOEMPAmini*>((CHWDeviceOEMPA*)cGetHWDeviceOEMPA());//new CHWDeviceOEMPAmini();
#ifdef _DRIVER_11XY_
		m_csCustomizedAPI = gcnew csCustomizedAPI(m_pHWDeviceOEMPAmini);
		csHWDeviceOEM::SetCustomizedOEMPA(dynamic_cast<csCustomizedOEMPA^>(m_csCustomizedAPI));
#endif //_DRIVER_11XY_
		m_pHWDevice = dynamic_cast<CHWDevice*>(m_pHWDeviceOEMPAmini);
		m_pSWDeviceOEMPAmini = m_pHWDeviceOEMPAmini->GetSWDeviceOEMPA();
		csHWDevice::Constructor(m_pHWDeviceOEMPAmini, m_pHWDevice);
		csHWDevice::Constructor(GetCustomizedOEMPA());
		m_csSWDeviceOEMPAmini = gcnew csSWDeviceOEMPAmini();
		m_csSWDeviceOEMPAmini->Constructor(m_pHWDeviceOEMPAmini, m_pSWDeviceOEMPAmini);
		if (m_pHWDevice)
			m_pHWDevice->SetDerivedClass(L"csHWDeviceOEMPAmini", NULL);
	}
	csHWDeviceOEMPAmini::~csHWDeviceOEMPAmini()
	{
		this->!csHWDeviceOEMPAmini();
	}
	csHWDeviceOEMPAmini::!csHWDeviceOEMPAmini()
	{
		Free();
	}
	void csHWDeviceOEMPAmini::Free()
	{
		m_pSWDeviceOEMPAmini = NULL;
		m_pHWDeviceOEMPAmini = NULL;
	}
	csSWDeviceOEMPAmini^ csHWDeviceOEMPAmini::GetSWDeviceOEMPAmini()
	{
		return m_csSWDeviceOEMPAmini;
	}
	csSWDeviceOEMPAmini^ csHWDeviceOEMPAmini::GetSWDeviceOEMPAmax()
	{
		return m_csSWDeviceOEMPAmini;
	}
	csSWDeviceOEMPAmini^ csHWDeviceOEMPAmini::GetSWDeviceOEMPA()
	{
		return m_csSWDeviceOEMPAmini;
	}

	/*unsafe*/bool csHWDeviceOEMPAmini::GetApertureCountMax(int *piCount)
	{
		if(!m_pHWDeviceOEMPAmini)
			return false;
		return m_pHWDeviceOEMPAmini->GetApertureCountMax(piCount);
	}
	/*unsafe*/bool csHWDeviceOEMPAmini::GetElementCountMax(int *piCount)
	{
		if(!m_pHWDeviceOEMPAmini)
			return false;
		return m_pHWDeviceOEMPAmini->GetElementCountMax(piCount);
	}

	void/*CHWDeviceOEMPAmini*/ *csHWDeviceOEMPAmini::GetHWDeviceOEMPAmini()
	{
		return m_pHWDeviceOEMPAmini;
	}

	csHWDevice ^csHWDeviceOEMPAmini::GetHWDevice()
	{
		return this;
	}
#pragma endregion csHWDeviceOEMPAmini
////////////////////////////////////////////////////////
////////////////////////////////////////////////////////

}
