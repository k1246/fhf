#include "stdafx.h"
#include <gcroot.h>

#pragma managed(push, off)
#include "UTDriverOEMPA.h"
#include "UTDriverOEMMC.h"
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
using namespace csDriverOEMPA;

namespace csDriverOEMMC
{
//////////////////////////////////////////////////////////////////////////
#pragma region csSWDeviceOEMMC
	public ref class csSWDeviceOEMMC : public csSWDeviceOEMPA
    {
	private:
		CHWDeviceOEMMC *m_pHWDeviceOEMMC;
		CSWDeviceOEMMC *m_pSWDeviceOEMMC;
	public:
		csSWDeviceOEMMC();
		~csSWDeviceOEMMC();
		void Constructor(CHWDeviceOEMMC *pHWDeviceOEMMC,CSWDeviceOEMMC *pSWDeviceOEMMC);
		void Free();

	//ethernet
		bool SetIPAddress(String ^pValue);
		bool GetIPAddress([Out] String^ %pValue);

		bool SetPort(unsigned short usValue);
		bool _SetPort(unsigned short usValue);//same than "SetPort", required because of the preprocessor definition in "WinSpool.h": "#define SetPort SetPortW".
		bool GetPort(unsigned short %usValue);

	protected:
		!csSWDeviceOEMMC();
	};
#pragma endregion csSWDeviceOEMMC

#pragma region csHWDeviceOEMMC
	public ref class csHWDeviceOEMMC:	public csHWDeviceOEMPA
    {
	private:
#ifdef _DRIVER_11XY_
		csCustomizedAPI ^m_csCustomizedAPI;
#endif //_DRIVER_11XY_
		csSWDeviceOEMMC ^m_csSWDeviceOEMMC;
		CHWDeviceOEMMC *m_pHWDeviceOEMMC;
		CHWDevice *m_pHWDevice;
		CSWDeviceOEMMC *m_pSWDeviceOEMMC;
	public:
		csHWDeviceOEMMC(csEnumHardware hwType);
		~csHWDeviceOEMMC();
		void Free();
		//csSWDeviceOEMMC^ GetSWDeviceOEMPAmini();
		csSWDeviceOEMMC^ GetSWDeviceOEMMC();
		csHWDevice ^GetHWDevice();
		csSWDeviceOEMMC ^GetSWDeviceOEMPA();

		/*unsafe*/bool GetApertureCountMax(int *piCount);//to get the maximum element count of an aperture.
		/*unsafe*/bool GetElementCountMax(int *piCount);//to get the maximum element count of the system (in case of mux).
			//Output parameters
			//	piCount : maximum aperture size.
	
		void/*CHWDeviceOEMMC*/ *GetHWDeviceOEMMC();
	protected:
		!csHWDeviceOEMMC();
	};
	public ref class csHWDeviceOEMMC2 : public csHWDeviceOEMMC
	{
	public:
		csHWDeviceOEMMC2();
	};
	public ref class csHWDeviceOEMMCu : public csHWDeviceOEMMC
	{
	public:
		csHWDeviceOEMMCu();
	};
	public ref class csHWDeviceOEMMCuF : public csHWDeviceOEMMC
	{
	public:
		csHWDeviceOEMMCuF();
	};
#pragma endregion hwDriverOEMMC
//////////////////////////////////////////////////////////////////////////

};

#ifndef _DRIVER_11XY_
namespace csDriverOEMMC
#else //_DRIVER_11XY_
namespace csDriverOEMPA
#endif //_DRIVER_11XY_
{
////////////////////////////////////////////////////////
#pragma region csSWDeviceOEMMC
	csSWDeviceOEMMC::csSWDeviceOEMMC()
	{
		Free();
	}
	csSWDeviceOEMMC::~csSWDeviceOEMMC()
	{
		this->!csSWDeviceOEMMC();
	}
	csSWDeviceOEMMC::!csSWDeviceOEMMC()
	{
		Free();
	}
	void csSWDeviceOEMMC::Constructor(CHWDeviceOEMMC *pHWDeviceOEMMC,CSWDeviceOEMMC *pSWDeviceOEMMC)
	{
		m_pHWDeviceOEMMC = pHWDeviceOEMMC;
		m_pSWDeviceOEMMC = pSWDeviceOEMMC;
	}
	void csSWDeviceOEMMC::Free()
	{
		m_pHWDeviceOEMMC = NULL;
		m_pSWDeviceOEMMC = NULL;
	}

//ethernet
	bool csSWDeviceOEMMC::SetIPAddress(String ^pValue)
	{
		wchar_t* y;
		bool bRet;

		if(!m_pSWDeviceOEMMC)
			return false;
		y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pValue);
		bRet = m_pSWDeviceOEMMC->SetIPAddress(y);
		Marshal::FreeHGlobal((IntPtr)y);
		return bRet;
	}
	bool csSWDeviceOEMMC::GetIPAddress([Out] String^ %pValue)
	{
		wchar_t pAux[MAX_PATH];

		if(!m_pSWDeviceOEMMC)
			return false;
		if(!m_pSWDeviceOEMMC->GetIPAddress(pAux,MAX_PATH))
			return false;
		pValue = Marshal::PtrToStringUni((IntPtr)pAux);
		return true;
	}

	bool csSWDeviceOEMMC::SetPort(unsigned short usValue)
	{
		return _SetPort(usValue);
	}
	bool csSWDeviceOEMMC::_SetPort(unsigned short usValue)
	{
		if(!m_pSWDeviceOEMMC)
			return false;
		return m_pSWDeviceOEMMC->_SetPort(usValue);//same than "SetPort", required because of the preprocessor definition in "WinSpool.h": "#define SetPort SetPortW".
	}
	bool csSWDeviceOEMMC::GetPort(unsigned short %usValue)
	{
		unsigned short usValue2;
		bool bRet;

		if(!m_pSWDeviceOEMMC)
			return false;
		bRet = m_pSWDeviceOEMMC->GetPort(usValue2);
		usValue = usValue2;
		return bRet;
	}

	//int csSWDeviceOEMMC::GetApertureCountMax()//to get the maximum element count of an aperture.
	//{
	//	if(!m_pSWDeviceOEMMC)
	//		return 0;
	//	return m_pSWDeviceOEMMC->GetApertureCountMax();
	//}
	//int csSWDeviceOEMMC::GetElementCountMax()//to get the maximum element count of the system (in case of mux).
	//{
	//	if(!m_pSWDeviceOEMMC)
	//		return 0;
	//	return m_pSWDeviceOEMMC->GetElementCountMax();
	//}

	//void csSWDeviceOEMMC::AlignmentCfgUpdated()
	//{
	//	CSWDeviceOEMMC::AlignmentCfgUpdated();
	//}
	//void csSWDeviceOEMMC::SetCalibrationParameters(float fWidth,float fStart,float fRange,float fGainAnalog,double dGainDigital)
	//{
	//	if(!m_pSWDeviceOEMMC)
	//		return;
	//	return m_pSWDeviceOEMMC->SetCalibrationParameters(fWidth,fStart,fRange,fGainAnalog,dGainDigital);
	//}
	//void csSWDeviceOEMMC::GetCalibrationParameters(float &fWidth,float &fStart,float &fRange,float &fGainAnalog,double &dGainDigital)
	//{
	//	if(!m_pSWDeviceOEMMC)
	//		return;
	//	return m_pSWDeviceOEMMC->GetCalibrationParameters(fWidth,fStart,fRange,fGainAnalog,dGainDigital);
	//}
	//bool csSWDeviceOEMMC::IsCalibrationPerformed()
	//{
	//	return IsAlignmentPerformed();
	//}
	//bool csSWDeviceOEMMC::IsAlignmentPerformed()
	//{
	//	if(!m_pSWDeviceOEMMC)
	//		return false;
	//	return m_pSWDeviceOEMMC->IsAlignmentPerformed();
	//}
	//bool csSWDeviceOEMMC::EnableAlignment(bool bEnable)
	//{
	//	if(!m_pSWDeviceOEMMC)
	//		return false;
	//	return m_pSWDeviceOEMMC->EnableAlignment(bEnable);
	//}
	//float csSWDeviceOEMMC::GetCalibrationAlignment()
	//{
	//	if(!m_pSWDeviceOEMMC)
	//		return 0.0f;
	//	return m_pSWDeviceOEMMC->GetCalibrationAlignment();
	//}
	//float csSWDeviceOEMMC::GetCalibrationOffset()
	//{
	//	if(!m_pSWDeviceOEMMC)
	//		return 0.0f;
	//	return m_pSWDeviceOEMMC->GetCalibrationOffset();
	//}
	//bool csSWDeviceOEMMC::IsAlignmentEnabled()
	//{
	//	if(!m_pSWDeviceOEMMC)
	//		return false;
	//	return m_pSWDeviceOEMMC->IsAlignmentEnabled();
	//}
	//bool csSWDeviceOEMMC::ResetAlignment()
	//{
	//	if(!m_pSWDeviceOEMMC)
	//		return false;
	//	return m_pSWDeviceOEMMC->ResetAlignment();
	//}
	//bool csSWDeviceOEMMC::SetCalibrationFileReport(String ^pFileReport)
	//{
	//	wchar_t* y;

	//	if(!m_pSWDeviceOEMMC)
	//		return false;
	//	y = (wchar_t*)(void*)Marshal::StringToHGlobalUni(pFileReport);
	//	m_pSWDeviceOEMMC->SetCalibrationFileReport(y);
	//	Marshal::FreeHGlobal((IntPtr)y);
	//	return true;
	//}
	//bool csSWDeviceOEMMC::GetCalibrationFileReport([Out] String^ %pFileReport)
	//{
	//	wchar_t pAux[MAX_PATH];

	//	if(!m_pSWDeviceOEMMC)
	//		return false;
	//	if(!m_pSWDeviceOEMMC->GetCalibrationFileReport(MAX_PATH,pAux))
	//		return false;
	//	pFileReport = Marshal::PtrToStringUni((IntPtr)pAux);
	//	return true;
	//}
#pragma endregion csSWDeviceOEMMC
////////////////////////////////////////////////////////
#pragma region csHWDeviceOEMMC
	csHWDeviceOEMMC::csHWDeviceOEMMC(csEnumHardware hwType) :
#ifndef _DRIVER_11XY_
																csHWDeviceOEMPA(hwType)
#else //_DRIVER_11XY_
																csHWDeviceOEM(csEnumHardware::csOEMMC, true, false)
#endif //_DRIVER_11XY_
	{
		bool bCreate;
		int iValue = CSWDevice::ReadCfgInt(L"UTKernel" LS L"Device", L"csDriverOEMMCCount", 0, bCreate);
		iValue++;
		CSWDevice::WriteCfgInt(L"UTKernel" LS L"Device", L"csDriverOEMMCCount", iValue);
		m_pHWDeviceOEMMC = dynamic_cast<CHWDeviceOEMMC*>((CHWDeviceOEMPA*)cGetHWDeviceOEMPA());//new CHWDeviceOEMMC();
#ifdef _DRIVER_11XY_
		m_csCustomizedAPI = gcnew csCustomizedAPI(m_pHWDeviceOEMMC);
		csHWDeviceOEM::SetCustomizedOEMPA(dynamic_cast<csCustomizedOEMPA^>(m_csCustomizedAPI));
#endif //_DRIVER_11XY_
		m_pHWDevice = dynamic_cast<CHWDevice*>(m_pHWDeviceOEMMC);
		m_pSWDeviceOEMMC = m_pHWDeviceOEMMC->GetSWDeviceOEMPA();
		csHWDevice::Constructor(m_pHWDeviceOEMMC, m_pHWDevice);
		csHWDevice::Constructor(GetCustomizedOEMPA());
		m_csSWDeviceOEMMC = gcnew csSWDeviceOEMMC();
		m_csSWDeviceOEMMC->Constructor(m_pHWDeviceOEMMC, m_pSWDeviceOEMMC);
		if (m_pHWDevice)
			m_pHWDevice->SetDerivedClass(L"csHWDeviceOEMMC", NULL);
	}
	csHWDeviceOEMMC2::csHWDeviceOEMMC2() : csHWDeviceOEMMC(csEnumHardware::csOEMMC2)
	{
	};
	csHWDeviceOEMMCu::csHWDeviceOEMMCu() : csHWDeviceOEMMC(csEnumHardware::csOEMMCu)
	{
	};
	csHWDeviceOEMMCuF::csHWDeviceOEMMCuF() : csHWDeviceOEMMC(csEnumHardware::csOEMMCuF)
	{
	};
	csHWDeviceOEMMC::~csHWDeviceOEMMC()
	{
		this->!csHWDeviceOEMMC();
	}
	csHWDeviceOEMMC::!csHWDeviceOEMMC()
	{
		Free();
	}
	void csHWDeviceOEMMC::Free()
	{
		m_pSWDeviceOEMMC = NULL;
		m_pHWDeviceOEMMC = NULL;
	}
	csSWDeviceOEMMC^ csHWDeviceOEMMC::GetSWDeviceOEMMC()
	{
		return m_csSWDeviceOEMMC;
	}
	csSWDeviceOEMMC^ csHWDeviceOEMMC::GetSWDeviceOEMPA()
	{
		return m_csSWDeviceOEMMC;
	}

	/*unsafe*/bool csHWDeviceOEMMC::GetApertureCountMax(int *piCount)
	{
		if(!m_pHWDeviceOEMMC)
			return false;
		return m_pHWDeviceOEMMC->GetApertureCountMax(piCount);
	}
	/*unsafe*/bool csHWDeviceOEMMC::GetElementCountMax(int *piCount)
	{
		if(!m_pHWDeviceOEMMC)
			return false;
		return m_pHWDeviceOEMMC->GetElementCountMax(piCount);
	}

	void/*CHWDeviceOEMMC*/ *csHWDeviceOEMMC::GetHWDeviceOEMMC()
	{
		return m_pHWDeviceOEMMC;
	}

	csHWDevice ^csHWDeviceOEMMC::GetHWDevice()
	{
		return this;
	}
#pragma endregion csHWDeviceOEMMC
////////////////////////////////////////////////////////
////////////////////////////////////////////////////////

}
