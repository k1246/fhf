#include "stdafx.h"
#define _USE_MATH_DEFINES
#include <gcroot.h>
#include <math.h>

#pragma managed(push, off)
#ifdef _DRIVER_11XY_
#include "UTKernelDriver.h"
#include "UTKernelDriverOEMPA.h"
#include "CustomizedDriverAPI.h"
#include "CustomizedWizardAPI.h"
#else //_DRIVER_11XY_
#include "UTDriverOEMPA.h"
#include "UTDriverOEMPA1.h"
#include "UTDriverOEMPA2.h"
#include "UTDriverOEMPAmini.h"
#include "CustomizedDriverOEMPA.h"
#include "CustomizedWizardOEMPA.h"
#endif //_DRIVER_11XY_
#include "UTKernelAPI.h"
DllImport void UTKernel_EnableEventUpdate(bool bEnable);
DllImport bool WINAPI OEMPA_WriteHWWizard(CHWDeviceOEMPA *pHWDeviceOEMPA,	int iWizardSystemId,int iWizardChannelId);
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

#ifndef _DRIVER_11XY_
namespace WizardOEMPA
#else //_DRIVER_11XY_
namespace WizardTemplate
#endif //_DRIVER_11XY_
{

#pragma region enumerations
	public enum class csSpecimen {csPlane=ePlane,
							csDisk=eDisk};
	public enum class csWave {csLongitudinal=eLongitudinal,
							csTransverse=eTransverse};
	public enum class csRectification{
							csSigned=eSigned,			//Rectification: RF --> not rectified, signed value,
							csUnsigned=eUnsigned,			//FW --> rectified, unsigned
							csUnsignedPositive=eUnsignedPositive,	//HWP --> only positive, unsigned,
							csUnsignedNegative=eUnsignedNegative	//HWN --> only negative
							};
	public enum class csGateModeAmp{csAmpAbsolute=eAmpAbsolute,
							csAmpMaximum=eAmpMaximum,
							csAmpMinimum=eAmpMinimum,
							csAmpPeakToPeak=eAmpPeakToPeak};
	public enum class csGateModeTof{csTofAmplitudeDetection=eTofAmplitudeDetection,//"AMP's TOF" : where the AMP result has been found, for Peak-Peak--> where Max has been found
								csTofThresholdCross=eTofThresholdCross,//"TH cross": first cross of the THRESHOLD  
								csTofZeroFirstAfterThresholdCross=eTofZeroFirstAfterThresholdCross,//"ZrA": first time crossed 0 after crossing THRESHOLD
								csTofZeroLastBeforeThresholdCross=eTofZeroLastBeforeThresholdCross//"ZrB": last time crossed 0 before crossing THRESHOLD
								};
	public enum class csEnumDepthMode{csTrueDepth=eStandardDepth,csSoundPath=eSoundPath,csTrueDepthBigBar=eDepthBigBar};
	public enum class csEnumPitchCatchDefinition{csLinear,csSector,csCycleByCycle};
#pragma endregion enumerations
///////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//# TEMPLATE - SINGLE-CHANNEL - LINEAR
	public ref class csWizardSpecimen
    {
	private:
		csSpecimen m_eSpecimen;
		double m_dVelocity;
		csWave m_eWave;
		double m_dRadius;

	public:
		csWizardSpecimen();
		~csWizardSpecimen();

		property double Velocity{
			double get()
			{
				return m_dVelocity;
			} 
			void set(double value)
			{
				m_dVelocity = value;
			}
		}

		property csWave Wave{
			csWave get()
			{
				return m_eWave;
			} 
			void set(csWave eWave)
			{
				m_eWave = eWave;
			}
		}

		property double Radius{
			double get()
			{
				if(m_eSpecimen==csSpecimen::csDisk)
					return m_dRadius;
				else
					return 0.0;
			} 
			void set(double value)
			{
				if(!value)
					m_eSpecimen = csSpecimen::csPlane;
				else
					m_eSpecimen = csSpecimen::csDisk;
				m_dRadius = value;
			}
		}

		property csSpecimen Specimen{
			csSpecimen get()
			{
				return m_eSpecimen;
			} 
			void set(csSpecimen value)
			{
				m_eSpecimen = value;
				if(m_eSpecimen == csSpecimen::csPlane)
					m_dRadius = 0.0;
			}
		}
		
		static property array<String^>^ Waves
		{
			array<String^>^ get()
			{
				array<String^>^ values = gcnew array<String^> {"Longitudinal", "Transverse"};
				return values;
			}
		}
		static property array<String^>^ Specimens
		{
			array<String^>^ get()
			{
				array<String^>^ values = gcnew array<String^> {"Plane", "Disk"};
				return values;
			}
		}
	protected:
		!csWizardSpecimen();
		void Free();
	};

	public ref class csWizardWedge
    {
	private:
		bool m_bEnable;
		double m_dVelocity;
		double m_dHeight;
		double m_dAngle;

	public:
		csWizardWedge();
		~csWizardWedge();

		property double Velocity{
			double get()
			{
				return m_dVelocity;
			} 
			void set(double value)
			{
				m_dVelocity = value;
			}
		}

		property bool Enable{
			bool get()
			{
				return m_bEnable;
			} 
			void set(bool bEnable)
			{
				m_bEnable = bEnable;
			}
		}

		property double Height{
			double get()
			{
				return m_dHeight;
			} 
			void set(double value)
			{
				m_dHeight = value;
			}
		}

		property double Angle{
			double get()
			{
				return m_dAngle;
			} 
			void set(double value)
			{
				m_dAngle = value;
			}
		}

	protected:
		!csWizardWedge();
		void Free();
	};

	public ref class csWizardProbe
    {
	private:
		int m_iElementOffset;
		int m_iElementCount;
		double m_dPitch;
		double m_dFrequency;
		double m_dRadius;

	public:
		csWizardProbe();
		~csWizardProbe();

		property int ElementOffset {
			int get()
			{
				return m_iElementOffset;
			}
			void set(int value)
			{
				m_iElementOffset = value;
			}
		}

		property int ElementCount{
			int get()
			{
				return m_iElementCount;
			} 
			void set(int value)
			{
				m_iElementCount = value;
			}
		}

		property double Pitch{
			double get()
			{
				return m_dPitch;
			} 
			void set(double value)
			{
				m_dPitch = value;
			}
		}

		property double Frequency{
			double get()
			{
				return m_dFrequency;
			} 
			void set(double value)
			{
				m_dFrequency = value;
			}
		}

		property double Radius{
			double get()
			{
				return m_dRadius;
			} 
			void set(double value)
			{
				m_dRadius = value;
			}
		}
	protected:
		!csWizardProbe();
		void Free();
	};

	public ref class csWizardScan
    {
	private:
		int m_iElementCount;
		double m_dDepthEmission;
		double m_dAngleStart;
		double m_dAngleStop;
		double m_dAngleStep;
		int m_iElementStart;
		int m_iElementStop;
		int m_iElementStep;
		bool m_bLinear;
		csEnumDepthMode m_DepthMode;

	public:
		csWizardScan();
		~csWizardScan();

		property bool Linear{
			bool get()
			{
				return m_bLinear;
			} 
			void set(bool value)
			{
				m_bLinear = value;
			}
		}

		property int ElementCount{
			int get()
			{
				return m_iElementCount;
			} 
			void set(int value)
			{
				m_iElementCount = value;
			}
		}

		property double DepthEmission{
			double get()
			{
				return m_dDepthEmission;
			} 
			void set(double value)
			{
				m_dDepthEmission = value;
			}
		}

		array<double>^ DepthReception;

		property double AngleStart{
			double get()
			{
				return m_dAngleStart;
			} 
			void set(double value)
			{
				m_dAngleStart = value;
			}
		}

		property double AngleStop{
			double get()
			{
				if(!m_bLinear)
					return m_dAngleStop;
				else
					return m_dAngleStart;
			} 
			void set(double value)
			{
				if(!m_bLinear)
				m_dAngleStop = value;
			}
		}

		property double AngleStep{
			double get()
			{
				if(!m_bLinear)
					return m_dAngleStep;
				else
					return M_PI / 180.0;
			}
			void set(double value)
			{
				if(!m_bLinear)
					m_dAngleStep = value;
			}
		}

		property int ElementStart{
			int get()
			{
				return m_iElementStart;
			} 
			void set(int value)
			{
				m_iElementStart = value;
			}
		}

		property int ElementStop{
			int get()
			{
				if(m_bLinear)
					return m_iElementStop;
				else
					return m_iElementStart;
			} 
			void set(int value)
			{
				if(m_bLinear)
					m_iElementStop = value;
			}
		}

		property int ElementStep{
			int get()
			{
				if(m_bLinear)
					return m_iElementStep;
				else
					return 1;
			} 
			void set(int value)
			{
				if(m_bLinear)
				m_iElementStep = value;
			}
		}

		property csEnumDepthMode DepthMode{
			csEnumDepthMode get()
			{
				return m_DepthMode;
			} 
			void set(csEnumDepthMode depthMode)
			{
				m_DepthMode = depthMode;
			}
		}

	protected:
		!csWizardScan();
		void Free();
	};

	public ref class csWizardGateAscan
    {
	private:
		double m_dStart;
		double m_dStop;
		double m_dTimeSlot;

	public:
		csWizardGateAscan();
		~csWizardGateAscan();

		property double Start{
			double get()
			{
				return m_dStart;
			} 
			void set(double value)
			{
				m_dStart = value;
			}
		}

		property double Stop{
			double get()
			{
				return m_dStop;
			} 
			void set(double value)
			{
				m_dStop = value;
			}
		}

		property double Range{
			double get()
			{
				return m_dStop-m_dStart;
			} 
			void set(double value)
			{
				m_dStop = m_dStart+value;
			}
		}

		property double TimeSlot{
			double get()
			{
				return m_dTimeSlot;
			} 
			void set(double value)
			{
				m_dTimeSlot = value;
			}
		}
	protected:
		!csWizardGateAscan();
		void Free();
	};

	public ref class csWizardGateCscan
    {
	private:
		bool m_bEnable;
		double m_dStart;
		double m_dStop;
		double m_dThreshold;
		csRectification m_eRectification;
		csGateModeAmp m_eGateModeAmp;
		csGateModeTof m_eGateModeTof;

	public:
		csWizardGateCscan();
		~csWizardGateCscan();

		void Init();

		property bool Enable{
			bool get()
			{
				return m_bEnable;
			} 
			void set(bool value)
			{
				m_bEnable = value;
			}
		}

		property double Start{
			double get()
			{
				return m_dStart;
			} 
			void set(double value)
			{
				m_dStart = value;
			}
		}

		property double Stop{
			double get()
			{
				return m_dStop;
			} 
			void set(double value)
			{
				m_dStop = value;
			}
		}

		property double Range{
			double get()
			{
				return m_dStop-m_dStart;
			} 
		}

		property double Threshold{
			double get()
			{
				return m_dThreshold;
			} 
			void set(double value)
			{
				m_dThreshold = value;
			}
		}

		property csRectification Rectification{
			csRectification get()
			{
				return m_eRectification;
			} 
			void set(csRectification value)
			{
				m_eRectification = value;
			}
		}

		property csGateModeAmp ModeAmplitude{
			csGateModeAmp get()
			{
				return m_eGateModeAmp;
			} 
			void set(csGateModeAmp value)
			{
				m_eGateModeAmp = value;
			}
		}

		property csGateModeTof ModeTimeOfFlight{
			csGateModeTof get()
			{
				return m_eGateModeTof;
			} 
			void set(csGateModeTof value)
			{
				m_eGateModeTof = value;
			}
		}

		static property array<String^>^ Rectifications
		{
			array<String^>^ get()
			{
				array<String^>^ values = gcnew array<String^> {"Signed", "Unsigned", "UnsignedPositive", "UnsignedNegative"};
				return values;
			}
		}

		static property array<String^>^ GateModeAmps
		{
			array<String^>^ get()
			{
				array<String^>^ values = gcnew array<String^> {"Absolute", "Maximum", "Minimum", "PeakToPeak"};
				return values;
			}
		}

		static property array<String^>^ GateModeTofs
		{
			array<String^>^ get()
			{
				array<String^>^ values = gcnew array<String^> {"AmplitudeDetection", "ThresholdCross", "ZeroFirstAfterThresholdCross", "ZeroLastBeforeThresholdCross"};
				return values;
			}
		}
	protected:
		!csWizardGateCscan();
		void Free();
	};

	public ref class csWizardTemplate
    {
	protected:
		CUTWizardSystem *m_pWizardSystem;
		CUTChannels *m_pChannel;
		BOOL m_bCheckWizardEnable;
		int m_iDeviceId;

	public:
		csWizardTemplate(int iDeviceId);
		~csWizardTemplate();

		csWizardSpecimen^ Specimen;
		csWizardWedge^ Wedge;
		csWizardProbe^ Probe;
		csWizardScan^ Scan;
		array<csWizardScan^>^ aScan;//multiple scan management.
		csWizardGateAscan^ GateAscan;
		array<csWizardGateCscan^>^ aGateCscan;

		bool SetScanCount(int iScanCount);
		void TemplateEdit(String^ filename,bool bCloseWaiting);
		bool TemplateToWizard();
		bool WizardUpdateScan(int *piErrorChannelProbe,int *piErrorChannelScan);
		bool WizardToFile(csHWDeviceOEMPA^ hwDeviceOEMPA,String^ file);
		bool WizardToHw(csHWDeviceOEMPA^ hwDeviceOEMPA);
		bool ReadWizard(csHWDeviceOEMPA^ hwDeviceOEMPA,[Out] csRoot^ %root,[Out] array<csCycle^>^ %cycle,[Out] array<csFocalLaw^>^ %emission,[Out] array<csFocalLaw^>^ %reception);
		bool WriteFile(csHWDeviceOEMPA^ hwDeviceOEMPA,csRoot^ %root,array<csCycle^>^ %cycle,array<csFocalLaw^>^ %emission,array<csFocalLaw^>^ %reception,String^ file);

		static int GateCsanCountMax();
		void ReallocGateCscan();
		void DesallocGateCscan();
		void EditFile(String^ filename,bool bCloseWaiting);
		static bool GetWizardFolder(String^ %filename);

		static void EnableEventUpdate(bool bEnable)
		{
			UTKernel_EnableEventUpdate(bEnable);
		}
	protected:
		!csWizardTemplate();
		void Free();
		bool WizardDelete();
		bool WizardNew();
	};

//////////////////////////////////////////////////////////////////////////

};

#ifndef _DRIVER_11XY_
namespace WizardOEMPA
#else //_DRIVER_11XY_
namespace WizardTemplate
#endif //_DRIVER_11XY_
{
////////////////////////////////////////////////////////
#pragma region csWizardTemplateImplement
	csWizardSpecimen::csWizardSpecimen()
	{
		double dVelocity=6300.0;
		double dRadius=0.0;//default if Plane 100.0e-3;

		m_dVelocity = dVelocity;
		m_eWave = csWave::csLongitudinal;
		m_dRadius = dRadius;
	}
	csWizardSpecimen::~csWizardSpecimen()
	{
		this->!csWizardSpecimen();
	}
	csWizardSpecimen::!csWizardSpecimen()
	{
		Free();
	}
	void csWizardSpecimen::Free()
	{
	}

	csWizardWedge::csWizardWedge()
	{
		double dVelocity=1480.0;
		double dHeight=20.0e-3;
		double dAngle=0.0;

		m_bEnable = false;
		m_dVelocity = dVelocity;
		m_dHeight = dHeight;
		m_dAngle = dAngle;
	}
	csWizardWedge::~csWizardWedge()
	{
		this->!csWizardWedge();
	}
	csWizardWedge::!csWizardWedge()
	{
		Free();
	}
	void csWizardWedge::Free()
	{
	}

	csWizardProbe::csWizardProbe()
	{
		double dPitch=0.6e-3;
		double dFrequency=5e6;
		double dRadius=150.0e-3;

		m_iElementOffset = 0;
		m_iElementCount = 128;
		m_dPitch = dPitch;
		m_dFrequency = dFrequency;
		m_dRadius = dRadius;
	}
	csWizardProbe::~csWizardProbe()
	{
		this->!csWizardProbe();
	}
	csWizardProbe::!csWizardProbe()
	{
		Free();
	}
	void csWizardProbe::Free()
	{
	}

	csWizardScan::csWizardScan()
	{
		double dDepthEmission=100.0e-3;
		m_iElementCount = 8;
		m_dDepthEmission = dDepthEmission;
		DepthReception = gcnew array<double>{100.0e-3};
		m_dAngleStart = 0.0;
		m_dAngleStop = 0.0;
		m_dAngleStep = 0.0;
		m_iElementStart = 0;
		m_iElementStop = 0;
		m_iElementStep = 1;
		m_bLinear = true;
		m_DepthMode = csEnumDepthMode::csTrueDepth;
		//DepthReception = gcnew array<double>(iSize);
	}
	csWizardScan::~csWizardScan()
	{
		this->!csWizardScan();
	}
	csWizardScan::!csWizardScan()
	{
		Free();
	}
	void csWizardScan::Free()
	{
	}

	csWizardGateAscan::csWizardGateAscan()
	{
		double dStop=1.0e-6;
		double dTimeSlot=2000.0e-6;

		m_dStart = 0.0;
		m_dStop = dStop;
		m_dTimeSlot = dTimeSlot;
	}
	csWizardGateAscan::~csWizardGateAscan()
	{
		this->!csWizardGateAscan();
	}
	csWizardGateAscan::!csWizardGateAscan()
	{
		Free();
	}
	void csWizardGateAscan::Free()
	{
	}

	csWizardGateCscan::csWizardGateCscan()
	{
		Init();
	}
	void csWizardGateCscan::Init()
	{
		double dStop=1.0e-6;
		double dThreshold=50.0;

		m_dStart = 0.0;
		m_dStop = dStop;
		m_dThreshold = dThreshold;
		m_eRectification = csRectification::csSigned;
		m_eGateModeAmp = csGateModeAmp::csAmpAbsolute;
		m_eGateModeTof = csGateModeTof::csTofAmplitudeDetection;
	}
	csWizardGateCscan::~csWizardGateCscan()
	{
		this->!csWizardGateCscan();
	}
	csWizardGateCscan::!csWizardGateCscan()
	{
		Free();
	}
	void csWizardGateCscan::Free()
	{
	}

	bool csWizardTemplate::SetScanCount(int iScanCount)
	{
		if(iScanCount<=0)
		{
			if(aScan!=nullptr)
			{
				for(int iScanIndex=0;iScanIndex<aScan->GetLength(0);iScanIndex++)
				{
					if(aScan[iScanIndex]!=nullptr)
						delete aScan[iScanIndex];
					aScan[iScanIndex] = nullptr;
				}
				delete aScan;
			}
			aScan = nullptr;
			Scan = nullptr;
			return true;
		}
		aScan = gcnew array<csWizardScan^>(iScanCount);
		for(int iScanIndex=0;iScanIndex<iScanCount;iScanIndex++)
		{
			aScan[iScanIndex] = gcnew csWizardScan();
		}
		Scan = aScan[0];
		return true;
	}
	csWizardTemplate::csWizardTemplate(int iDeviceId)
	{
		Specimen = gcnew csWizardSpecimen();
		Wedge = gcnew csWizardWedge();
		Probe = gcnew csWizardProbe();
		aScan = nullptr;
		Scan = nullptr;
		GateAscan = gcnew csWizardGateAscan();
		aGateCscan = nullptr;
		ReallocGateCscan();
		SetScanCount(1);
		m_pWizardSystem = NULL;
		m_pChannel = NULL;
		m_bCheckWizardEnable = FALSE;
		m_iDeviceId = iDeviceId;
		WizardNew();
	}
	csWizardTemplate::~csWizardTemplate()
	{
		this->!csWizardTemplate();
	}
	csWizardTemplate::!csWizardTemplate()
	{
		delete Specimen;
		delete Wedge;
		delete Probe;
		delete GateAscan;
		delete aGateCscan;
		Specimen = nullptr;
		Wedge = nullptr;
		Probe = nullptr;
		Scan = nullptr;
		GateAscan = nullptr;
		aGateCscan = nullptr;
		if(aScan!=nullptr)
		{
			for(int iIndex=0;iIndex<aScan->Length;iIndex++)
			{
				if(aScan[iIndex]!=nullptr)
					delete aScan[iIndex];
				aScan[iIndex] = nullptr;
			}
			delete aScan;
		}
		aScan = nullptr;
		Free();
	}
	void csWizardTemplate::Free()
	{
		WizardDelete();
	}
	int csWizardTemplate::GateCsanCountMax()
	{
		return g_iOEMPAGateCountMax;
	}
	void csWizardTemplate::ReallocGateCscan()
	{
		bool bRet=true;
		csWizardGateCscan ^gate0=nullptr;
		csWizardGateCscan ^gate1=nullptr,^gate2=nullptr,^gate3=nullptr;

		if(aGateCscan && (0<aGateCscan->Length))
			gate0 = aGateCscan[0];
		if(aGateCscan && (1<aGateCscan->Length))
			gate1 = aGateCscan[1];
		if(aGateCscan && (0<aGateCscan->Length))
			gate2 = aGateCscan[2];
		if(aGateCscan && (0<aGateCscan->Length))
			gate3 = aGateCscan[3];
		if(!aGateCscan)
			aGateCscan = gcnew array<csWizardGateCscan^>(g_iOEMPAGateCountMax);
		if(!aGateCscan)
			return;
		if(gate0==nullptr)
			aGateCscan[0] = gcnew csWizardGateCscan();
		else
			aGateCscan[0] = gate0;
		if(gate1==nullptr)
			aGateCscan[1] = gcnew csWizardGateCscan();
		else
			aGateCscan[1] = gate1;
		if(gate2==nullptr)
			aGateCscan[2] = gcnew csWizardGateCscan();
		else
			aGateCscan[2] = gate2;
		if(gate3==nullptr)
			aGateCscan[3] = gcnew csWizardGateCscan();
		else
			aGateCscan[3] = gate3;
	}
	void csWizardTemplate::DesallocGateCscan()
	{
		if(aGateCscan)
		{
			for(int iIndex=0;iIndex<aGateCscan->Length;iIndex++)
			{
				delete aGateCscan[iIndex];
				aGateCscan[iIndex] = nullptr;
			}
			delete aGateCscan;
			aGateCscan = nullptr;
		}
	}
	bool csWizardTemplate::WizardDelete()
	{
		bool bRet=true;

		if(!m_pWizardSystem)
			return false;
		if(!m_pChannel)
			return false;
		if(!CUTKernelFile::DeleteObject(m_pChannel,eRootTypeChannels))
			bRet = false;
		m_pChannel = NULL;
		if(!CUTKernelFile::DeleteObject(m_pWizardSystem,eRootTypeWizardSystem))
			bRet = false;
		m_pWizardSystem = NULL;
		m_bCheckWizardEnable = FALSE;
		return bRet;
	}

	bool csWizardTemplate::WizardNew()
	{
		wchar_t pAux[MAX_PATH];
		CUTKernelRoot *ptr;
		bool bRet=true;
		int iWizardProbeIndex=0;
		bool bCreate;

		if(m_bCheckWizardEnable)
		{
			WizardDelete();
			m_bCheckWizardEnable = FALSE;
		}
		swprintf(pAux,MAX_PATH,L"OEMPA_system_%d", m_iDeviceId);
		ptr = CUTKernelRoot::SafeNewObject(eRootTypeWizardSystem,pAux,bCreate);
		m_pWizardSystem = dynamic_cast<CUTWizardSystem*>(ptr);
		if(!m_pWizardSystem)
		{
			WizardDelete();
			return false;
		}
		swprintf(pAux,g_iMaxNameLength+1,L"OEMPA_channel_%d", m_iDeviceId);
		ptr = CUTKernelRoot::SafeNewObject(eRootTypeChannels,pAux,bCreate);
		m_pChannel = dynamic_cast<CUTChannels*>(ptr);
		if(!m_pChannel)
		{
			WizardDelete();
			return false;
		}
		if(m_pChannel->LinkWizardProbe(0,m_pWizardSystem,iWizardProbeIndex))
			m_bCheckWizardEnable = TRUE;
		else
			WizardDelete();
		return bRet;
	}

	bool csWizardTemplate::TemplateToWizard()
	{
		double dSpecimenRadius=Specimen->Radius;
		double adCscanStart[g_iOEMPAGateCountMax],adCscanRange[g_iOEMPAGateCountMax],adCscanThreshold[g_iOEMPAGateCountMax];
		enumRectification aeRectification[g_iOEMPAGateCountMax];
		enumGateModeAmp aeModeAmp[g_iOEMPAGateCountMax];
		enumGateModeTof aeModeTof[g_iOEMPAGateCountMax];
		bool bRet;
		int iCscanCount;
		enumDepthMode eDepthMode;

		if(!m_bCheckWizardEnable)
			return false;
		iCscanCount = aGateCscan->Length;
		if(aGateCscan->Length>g_iOEMPAGateCountMax)
			return false;
		if((!dSpecimenRadius && m_pWizardSystem->Specimen().SetSpecimen(ePlane)) ||
			((dSpecimenRadius>0.0) && m_pWizardSystem->Specimen().SetSpecimen(eDisk)))
			return false;
		if((dSpecimenRadius>0.0) && m_pWizardSystem->Specimen().SetRadius(dSpecimenRadius))
			return false;
		if(m_pWizardSystem->Probe(0).SetRadius(Probe->Radius))
			return false;
		if(aScan->GetLength(0)<=0)
			return false;
		if (m_pChannel->Probe(0).SetFirst(Probe->ElementOffset))
			return false;
		if(m_pChannel->Probe(0).SetScanCount(aScan->GetLength(0)))
			return false;
		for(int iIndex=0;iIndex<aGateCscan->Length;iIndex++)
		{
			if(!aGateCscan[iIndex]->Enable)
			{
				iCscanCount = iIndex;
				break;
			}
			adCscanStart[iIndex] = aGateCscan[iIndex]->Start;
			adCscanRange[iIndex] = aGateCscan[iIndex]->Range;
			adCscanThreshold[iIndex] = aGateCscan[iIndex]->Threshold;
			aeRectification[iIndex] = (enumRectification)aGateCscan[iIndex]->Rectification;
			aeModeAmp[iIndex] = (enumGateModeAmp)aGateCscan[iIndex]->ModeAmplitude;
			aeModeTof[iIndex] = (enumGateModeTof)aGateCscan[iIndex]->ModeTimeOfFlight;
		}
		//pin_ptr<double> adScanDepthReception = &Scan->DepthReception[0];
		for(int iScanIndex=0;iScanIndex<aScan->GetLength(0);iScanIndex++)
		{
			double* adScanDepthReception = new double[aScan[iScanIndex]->DepthReception->Length];
			if(!adScanDepthReception)
				return false;
			for(int iIndex=0;iIndex<aScan[iScanIndex]->DepthReception->Length;iIndex++)
				adScanDepthReception[iIndex] = aScan[iScanIndex]->DepthReception[iIndex];
			eDepthMode = (enumDepthMode)aScan[iScanIndex]->DepthMode;
			if(aScan[iScanIndex]->Linear)
				bRet = OEMPA_WriteWizardMultipleChannel(0,iScanIndex,
							m_pWizardSystem,m_pChannel,
							Specimen->Wave==csWave::csLongitudinal,Specimen->Velocity,
							Probe->ElementCount,Probe->Pitch,Probe->Frequency,
							Wedge->Enable,
							Wedge->Height,Wedge->Angle,Wedge->Velocity,
							aScan[iScanIndex]->DepthEmission,adScanDepthReception,aScan[iScanIndex]->DepthReception->Length,
							aScan[iScanIndex]->ElementCount,
							true,aScan[iScanIndex]->ElementStart,aScan[iScanIndex]->ElementStop,aScan[iScanIndex]->ElementStep,
							false,aScan[iScanIndex]->AngleStart,aScan[iScanIndex]->AngleStart,0.01,
							GateAscan->Start,GateAscan->Range,GateAscan->TimeSlot,
							iCscanCount/*aGateCscan->Length*/,adCscanStart,adCscanRange,adCscanThreshold,
							aeRectification,aeModeAmp,aeModeTof,
							eDepthMode);
			else
				bRet = OEMPA_WriteWizardMultipleChannel(0,iScanIndex,
							m_pWizardSystem,m_pChannel,
							Specimen->Wave==csWave::csLongitudinal,Specimen->Velocity,
							Probe->ElementCount,Probe->Pitch,Probe->Frequency,
							Wedge->Enable,
							Wedge->Height,Wedge->Angle,Wedge->Velocity,
							aScan[iScanIndex]->DepthEmission,adScanDepthReception,aScan[iScanIndex]->DepthReception->Length,
							aScan[iScanIndex]->ElementCount,
							false,aScan[iScanIndex]->ElementStart,aScan[iScanIndex]->ElementStart,1,
							true,aScan[iScanIndex]->AngleStart,aScan[iScanIndex]->AngleStop,aScan[iScanIndex]->AngleStep,
							GateAscan->Start,GateAscan->Range,GateAscan->TimeSlot,
							iCscanCount/*aGateCscan->Length*/,adCscanStart,adCscanRange,adCscanThreshold,
							aeRectification,aeModeAmp,aeModeTof,
							eDepthMode);
			delete adScanDepthReception;
			adScanDepthReception = NULL;
		}
		return bRet;
	}

	bool csWizardTemplate::WizardUpdateScan(int *piErrorChannelProbe,int *piErrorChannelScan)
	{
		CUTProbe *pProbe;
		CUTScan *pScan;
		int iProbeCount,iScanCount;
		bool bRet=true;

		if(piErrorChannelProbe)
			*piErrorChannelProbe = -1;
		if(piErrorChannelScan)
			*piErrorChannelScan = -1;
		if(!m_pChannel)
			return false;
		if(!CUTChannels::IsDefaultMultiChannels())
		{
			if(!(*m_pChannel).Probe().Scan().SetScanStatusOutOfDate())
				return false;
			return (*m_pChannel).Probe().Scan().UpdateScan();//UpdateScan
		}else{
			for(int iStep=0;iStep<2;iStep++)
			{
				iProbeCount = m_pChannel->GetProbeCount();
				for(int iProbeIndex=0;iProbeIndex<iProbeCount;iProbeIndex++)
				{
					pProbe = &m_pChannel->Probe(iProbeIndex);
					iScanCount = pProbe->GetScanCount();
					for(int iScanIndex=0;iScanIndex<iScanCount;iScanIndex++)
					{
						pScan = &pProbe->Scan(iScanIndex);
						if((iStep==0) && !pScan->SetScanStatusOutOfDate())
							bRet = false;
						if((iStep==1) && !pScan->UpdateScan())
						{
							bRet = false;
							if(piErrorChannelProbe && (*piErrorChannelProbe==-1))
								*piErrorChannelProbe = iProbeIndex;
							if(piErrorChannelScan && (*piErrorChannelScan==-1))
								*piErrorChannelScan = iScanIndex;
						}
					}
				}
			}
		}
		return bRet;
	}

	bool csWizardTemplate::WizardToFile(csHWDeviceOEMPA^ hwDeviceOEMPA,String^ file)
	{
		CHWDeviceOEMPA *pHWDeviceOEMPA;
		wchar_t *pFile;
		bool bRet;

		if(!m_bCheckWizardEnable)
			return false;
		if(hwDeviceOEMPA==nullptr)
			return false;
		pHWDeviceOEMPA = (CHWDeviceOEMPA*)hwDeviceOEMPA->cGetHWDeviceOEMPA();
		if(!pHWDeviceOEMPA)
			return false;
		pFile = (wchar_t*)(void*)Marshal::StringToHGlobalUni(file);
		bRet = OEMPA_ReadWizardWriteFile(pHWDeviceOEMPA,m_pWizardSystem,m_pChannel,pFile);
		Marshal::FreeHGlobal((IntPtr)pFile);
		return bRet;
	}

	bool csWizardTemplate::WizardToHw(csHWDeviceOEMPA^ hwDeviceOEMPA)
	{
		CHWDeviceOEMPA *pHWDeviceOEMPA;
		int iWizardSystemId,iWizardChannelId;

		if(!m_bCheckWizardEnable)
			return false;
		if(hwDeviceOEMPA==nullptr)
			return false;
		if(!m_pWizardSystem->GetID(iWizardSystemId))
			return false;
		if(!m_pChannel->GetID(iWizardChannelId))
			return false;
		pHWDeviceOEMPA = (CHWDeviceOEMPA*)hwDeviceOEMPA->cGetHWDeviceOEMPA();
		if(!pHWDeviceOEMPA)
			return false;
		return OEMPA_WriteHWWizard(pHWDeviceOEMPA,iWizardSystemId,iWizardChannelId);
	}

	bool csWizardTemplate::ReadWizard(csHWDeviceOEMPA^ hwDeviceOEMPA,[Out] csRoot^ %root,[Out] array<csCycle^>^ %cycle,[Out] array<csFocalLaw^>^ %emission,[Out] array<csFocalLaw^>^ %reception)
	{
		CHWDeviceOEMPA *pHWDeviceOEMPA;
		bool bRet=true;
		structRoot Root;
		structCycle *pCycle=NULL;
		CFocalLaw *pEmission=NULL,*pReception=NULL;

		if(!m_bCheckWizardEnable)
			return false;
		if(hwDeviceOEMPA==nullptr)
			return false;
		pHWDeviceOEMPA = (CHWDeviceOEMPA*)hwDeviceOEMPA->cGetHWDeviceOEMPA();
		if(!pHWDeviceOEMPA)
			return false;
		bRet = OEMPA_ReadWizard(pHWDeviceOEMPA,m_pWizardSystem,m_pChannel,Root,pCycle,pEmission,pReception);
		if(bRet)
		{
			root = gcnew csRoot;
			if(!root || !root->vCopyFrom(&Root))
				bRet = false;
			if(Root.iCycleCount>0)
			{
				cycle = gcnew array<csCycle^>(Root.iCycleCount);
				if(cycle)
				{
					for(int iCycle=0;iCycle<Root.iCycleCount;iCycle++)
					{
						cycle[iCycle] = gcnew csCycle;
						if(!cycle[iCycle]->vCopyFrom(&pCycle[iCycle]))
							bRet = false;
					}
				}
				emission = gcnew array<csFocalLaw^>(Root.iCycleCount);
				if(emission)
				{
					for(int iCycle=0;iCycle<Root.iCycleCount;iCycle++)
					{
						emission[iCycle] = gcnew csFocalLaw;
						if(!emission[iCycle]->vCopyFrom(&pEmission[iCycle]))
							bRet = false;
					}
				}
				reception = gcnew array<csFocalLaw^>(Root.iCycleCount);
				if(reception)
				{
					for(int iCycle=0;iCycle<Root.iCycleCount;iCycle++)
					{
						reception[iCycle] = gcnew csFocalLaw;
						if(!reception[iCycle]->vCopyFrom(&pReception[iCycle]))
							bRet = false;
					}
				}
			}
		}
		return bRet;
	}

	bool csWizardTemplate::WriteFile(csHWDeviceOEMPA^ hwDeviceOEMPA,csRoot^ %root,array<csCycle^>^ %cycle,array<csFocalLaw^>^ %emission,array<csFocalLaw^>^ %reception,String^ file)
	{
		CHWDeviceOEMPA *pHWDeviceOEMPA;
		wchar_t *pFile;
		bool bRet=true;
		structRoot Root;
		structCycle *pCycle=NULL;
		CFocalLaw *pEmission=NULL,*pReception=NULL;

		if(!m_bCheckWizardEnable)
			return false;
		if(hwDeviceOEMPA==nullptr)
			return false;
		pHWDeviceOEMPA = (CHWDeviceOEMPA*)hwDeviceOEMPA->cGetHWDeviceOEMPA();
		if(!pHWDeviceOEMPA)
			return false;

		if(!root->vCopyTo(&Root))
			bRet = false;

		if(Root.iCycleCount>0)
		{
			pCycle = OEMPA_AllocCycle(Root, Root.iCycleCount);
			pEmission = OEMPA_AllocFocalLaw(Root.iCycleCount);
			pReception = OEMPA_AllocFocalLaw(Root.iCycleCount);
			if(!pCycle || !pEmission || !pReception)
				return false;
			OEMPA_ResetArrayFocalLaw(Root.iCycleCount,pEmission);
			OEMPA_ResetArrayFocalLaw(Root.iCycleCount,pReception);

			if(pCycle)
			{
				for(int iCycle=0;iCycle<Root.iCycleCount;iCycle++)
				{
					if(!cycle[iCycle]->vCopyTo(&pCycle[iCycle]))
						bRet = false;
				}
			}
			if(pEmission)
			{
				for(int iCycle=0;iCycle<Root.iCycleCount;iCycle++)
				{
					if(!emission[iCycle]->vCopyTo(&pEmission[iCycle]))
						bRet = false;
				}
			}
			if(pReception)
			{
				for(int iCycle=0;iCycle<Root.iCycleCount;iCycle++)
				{
					if(!reception[iCycle]->vCopyTo(&pReception[iCycle]))
						bRet = false;
				}
			}
		}

		if(bRet)
			pFile = (wchar_t*)(void*)Marshal::StringToHGlobalUni(file);
		if(!OEMPA_WriteFileText(	pFile,&Root,pCycle,pEmission,pReception))
			bRet = false;
		Marshal::FreeHGlobal((IntPtr)pFile);

		if(Root.iCycleCount>0)
		{
			OEMPA_DesallocCycle(pCycle);
			OEMPA_DesallocFocalLaw(pEmission);
			OEMPA_DesallocFocalLaw(pReception);
		}

		return bRet;
	}

	bool csWizardTemplate::GetWizardFolder(String^ %filename)
	{
		filename = gcnew String(GetRootProgramDataFolder());
		return true;
	}

	void csWizardTemplate::TemplateEdit(String ^file,bool bCloseWaiting)
	{
		EditFile(file,bCloseWaiting);
	}

	void csWizardTemplate::EditFile(String^ file,bool bCloseWaiting)
	{
		SHELLEXECUTEINFO execinfo;
		wchar_t *pFile;

		pFile = (wchar_t*)(void*)Marshal::StringToHGlobalUni(file);
		memset(&execinfo, 0, sizeof(execinfo));
		execinfo.lpFile = pFile;
		execinfo.cbSize = sizeof(execinfo);
		execinfo.lpVerb = L"open";
		execinfo.fMask = SEE_MASK_NOCLOSEPROCESS;
		execinfo.nShow = SW_SHOWDEFAULT;
		execinfo.lpParameters = 0;
		ShellExecuteEx(&execinfo);
		if(bCloseWaiting)
			WaitForSingleObject(execinfo.hProcess, INFINITE);
		Marshal::FreeHGlobal((IntPtr)pFile);
	}

	//void CDlgSetupFiles::OnBnClickedButtonToolbox()
	//{
	//	g_RunToolbox.Run(GetSafeHwnd(),CDlgSetupFiles::CallbackThreadToolbox);
	//}

	/////////////////////////////////////////////////////////////////////////
	public ref class csWizardPitchCatchScanCycle
	{
	private:
		int m_iElementCountEmission;
		int m_iElementCountReception;
		double m_dDepthEmission;
		double m_dAngleStartEmission;
		double m_dAngleStartReception;
		int m_iElementStartEmission;
		int m_iElementStartReception;
		csEnumDepthMode m_eDepthModeEmission;
		csEnumDepthMode m_eDepthModeReception;

	public:
		csWizardPitchCatchScanCycle();
		~csWizardPitchCatchScanCycle();

		property int ElementCountEmission {
			int get()
			{
				return m_iElementCountEmission;
			}
			void set(int value)
			{
				m_iElementCountEmission = value;
			}
		}

		property int ElementCountReception {
			int get()
			{
				return m_iElementCountReception;
			}
			void set(int value)
			{
				m_iElementCountReception = value;
			}
		}

		property double DepthEmission {
			double get()
			{
				return m_dDepthEmission;
			}
			void set(double value)
			{
				m_dDepthEmission = value;
			}
		}

		array<double>^ DepthReception;

		property double AngleEmission {
			double get()
			{
				return m_dAngleStartEmission;
			}
			void set(double value)
			{
				m_dAngleStartEmission = value;
			}
		}

		property double AngleReception {
			double get()
			{
				return m_dAngleStartReception;
			}
			void set(double value)
			{
				m_dAngleStartReception = value;
			}
		}

		property int ElementStartEmission {
			int get()
			{
				return m_iElementStartEmission;
			}
			void set(int value)
			{
				m_iElementStartEmission = value;
			}
		}

		property int ElementStartReception {
			int get()
			{
				return m_iElementStartReception;
			}
			void set(int value)
			{
				m_iElementStartReception = value;
			}
		}

		property csEnumDepthMode DepthModeEmission {
			csEnumDepthMode get()
			{
				return m_eDepthModeEmission;
			}
			void set(csEnumDepthMode value)
			{
				m_eDepthModeEmission = value;
			}
		}

		property csEnumDepthMode DepthModeReception {
			csEnumDepthMode get()
			{
				return m_eDepthModeReception;
			}
			void set(csEnumDepthMode value)
			{
				m_eDepthModeReception = value;
			}
		}

	protected:
		!csWizardPitchCatchScanCycle();
		void Free();
	};

	csWizardPitchCatchScanCycle::csWizardPitchCatchScanCycle()
	{
		m_iElementCountEmission = 8;
		m_iElementCountReception = 8;
		m_dDepthEmission = 100.0e-3;
		m_dAngleStartEmission = 0.0;
		m_dAngleStartReception = 0.0;
		m_iElementStartEmission = 0;
		m_iElementStartReception = 0;
		DepthReception = gcnew array<double>{100.0e-3};
		DepthModeEmission = csEnumDepthMode::csTrueDepth;
		DepthModeReception = csEnumDepthMode::csTrueDepth;
	}
	csWizardPitchCatchScanCycle::~csWizardPitchCatchScanCycle()
	{
		this->!csWizardPitchCatchScanCycle();
	}
	csWizardPitchCatchScanCycle::!csWizardPitchCatchScanCycle()
	{
		Free();
	}
	void csWizardPitchCatchScanCycle::Free()
	{
	}
	
	public ref class csWizardPitchCatchScan
	{
	private:
		int m_iElementCountE;
		int m_iElementCountR;
		double m_dDepthE;
		double m_dAngleStartE;
		double m_dAngleStartR;
		double m_dAngleStopE;
		double m_dAngleStopR;
		double m_dAngleStepE;
		double m_dAngleStepR;
		int m_iElementStartE;
		int m_iElementStartR;
		int m_iElementStopE;
		int m_iElementStopR;
		int m_iElementStepE;
		int m_iElementStepR;
		csEnumDepthMode m_DepthModeE;
		csEnumDepthMode m_DepthModeR;
		csEnumPitchCatchDefinition m_ePitchCatchDefinition;

	public:
		array<csWizardPitchCatchScanCycle^>^ Cycles;//multiple scan management.
		csWizardPitchCatchScan();
		~csWizardPitchCatchScan();
		bool SetScanCount(int iScanCount);

		property csEnumPitchCatchDefinition PitchCatchDefinition {
			csEnumPitchCatchDefinition get()
			{
				return m_ePitchCatchDefinition;
			}
			void set(csEnumPitchCatchDefinition value)
			{
				m_ePitchCatchDefinition = value;
			}
		}

		property int ElementCountEmission {
			int get()
			{
				return m_iElementCountE;
			}
			void set(int value)
			{
				m_iElementCountE = value;
			}
		}

		property int ElementCountReception {
			int get()
			{
				return m_iElementCountR;
			}
			void set(int value)
			{
				m_iElementCountR = value;
			}
		}

		property double DepthEmission {
			double get()
			{
				return m_dDepthE;
			}
			void set(double value)
			{
				m_dDepthE = value;
			}
		}

		array<double>^ DepthReception;

		property double AngleStartEmission {
			double get()
			{
				return m_dAngleStartE;
			}
			void set(double value)
			{
				m_dAngleStartE = value;
			}
		}

		property double AngleStartReception {
			double get()
			{
				return m_dAngleStartR;
			}
			void set(double value)
			{
				m_dAngleStartR = value;
			}
		}

		property double AngleStopEmission {
			double get()
			{
				if(m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csSector)
					return m_dAngleStopE;
				else
					return m_dAngleStartE;

			}
			void set(double value)
			{
				if(m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csSector)
					m_dAngleStopE = value;
			}
		}

		property double AngleStopReception {
			double get()
			{
				if (m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csSector)
					return m_dAngleStopR;
				else
					return m_dAngleStartR;

			}
			void set(double value)
			{
				if (m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csSector)
					m_dAngleStopR = value;
			}
		}

		property double AngleStepEmission {
			double get()
			{
				if (m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csSector)
					return m_dAngleStepE;
				else
					return M_PI / 180.0;
			}
			void set(double value)
			{
				if (m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csSector)
					m_dAngleStepE = value;
			}
		}

		property double AngleStepReception {
			double get()
			{
				if (m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csSector)
					return m_dAngleStepR;
				else
					return M_PI / 180.0;
			}
			void set(double value)
			{
				if (m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csSector)
					m_dAngleStepR = value;
			}
		}

		property int ElementStartEmission {
			int get()
			{
				return m_iElementStartE;
			}
			void set(int value)
			{
				m_iElementStartE = value;
			}
		}

		property int ElementStartReception {
			int get()
			{
				return m_iElementStartR;
			}
			void set(int value)
			{
				m_iElementStartR = value;
			}
		}

		property int ElementStopEmission {
			int get()
			{
				if (m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csLinear)
					return m_iElementStopE;
				else
					return m_iElementStartE;
			}
			void set(int value)
			{
				if (m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csLinear)
					m_iElementStopE = value;
			}
		}

		property int ElementStopReception {
			int get()
			{
				if (m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csLinear)
					return m_iElementStopR;
				else
					return m_iElementStartR;
			}
			void set(int value)
			{
				if (m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csLinear)
					m_iElementStopR = value;
			}
		}

		property int ElementStepEmission {
			int get()
			{
				if (m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csLinear)
					return m_iElementStepE;
				else
					return 1;
			}
			void set(int value)
			{
				if (m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csLinear)
					m_iElementStepE = value;
			}
		}

		property int ElementStepReception {
			int get()
			{
				if (m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csLinear)
					return m_iElementStepR;
				else
					return 1;
			}
			void set(int value)
			{
				if (m_ePitchCatchDefinition == csEnumPitchCatchDefinition::csLinear)
					m_iElementStepR = value;
			}
		}

		property csEnumDepthMode DepthModeEmission {
			csEnumDepthMode get()
			{
				return m_DepthModeE;
			}
			void set(csEnumDepthMode depthMode)
			{
				m_DepthModeE= depthMode;
			}
		}

		property csEnumDepthMode DepthModeReception {
			csEnumDepthMode get()
			{
				return m_DepthModeR;
			}
			void set(csEnumDepthMode depthMode)
			{
				m_DepthModeR = depthMode;
			}
		}

	protected:
		!csWizardPitchCatchScan();
		void Free();
	};

	csWizardPitchCatchScan::csWizardPitchCatchScan()
	{
		m_iElementCountE = 8;
		m_iElementCountR = 8;
		m_dDepthE = 100.0e-3;
		DepthReception = gcnew array<double>{100.0e-3};
		m_dAngleStartE = 0.0;
		m_dAngleStartR = 0.0;
		m_dAngleStopE = 0.0;
		m_dAngleStopR = 0.0;
		m_dAngleStepE = 1.0 * M_PI / 180.0;
		m_dAngleStepR = 1.0 * M_PI / 180.0;
		m_iElementStartE = 0;
		m_iElementStartR = 0;
		m_iElementStopE = 0;
		m_iElementStopR = 0;
		m_iElementStepE = 1;
		m_iElementStepR = 1;
		DepthModeEmission = csEnumDepthMode::csTrueDepth;
		DepthModeReception = csEnumDepthMode::csTrueDepth;
		m_ePitchCatchDefinition = csEnumPitchCatchDefinition::csLinear;
		Cycles = nullptr;
	}
	csWizardPitchCatchScan::~csWizardPitchCatchScan()
	{
		if (Cycles != nullptr)
		{
			for (int iIndex = 0; iIndex < Cycles->Length; iIndex++)
			{
				if (Cycles[iIndex] != nullptr)
					delete Cycles[iIndex];
				Cycles[iIndex] = nullptr;
			}
			delete Cycles;
		}
		Cycles = nullptr;
		this->!csWizardPitchCatchScan();
	}
	csWizardPitchCatchScan::!csWizardPitchCatchScan()
	{
		if (Cycles != nullptr)
		{
			for (int iIndex = 0; iIndex < Cycles->Length; iIndex++)
			{
				if (Cycles[iIndex] != nullptr)
					delete Cycles[iIndex];
				Cycles[iIndex] = nullptr;
			}
			delete Cycles;
		}
		Cycles = nullptr;
		Free();
	}
	void csWizardPitchCatchScan::Free()
	{
	}
	bool csWizardPitchCatchScan::SetScanCount(int iScanCount)
	{
		if (iScanCount <= 0)
		{
			if (Cycles != nullptr)
			{
				for (int iScanIndex = 0; iScanIndex < Cycles->GetLength(0); iScanIndex++)
				{
					if (Cycles[iScanIndex] != nullptr)
						delete Cycles[iScanIndex];
					Cycles[iScanIndex] = nullptr;
				}
				delete Cycles;
			}
			Cycles = nullptr;
			return true;
		}
		Cycles = gcnew array<csWizardPitchCatchScanCycle^>(iScanCount);
		for (int iScanIndex = 0; iScanIndex < iScanCount; iScanIndex++)
		{
			Cycles[iScanIndex] = gcnew csWizardPitchCatchScanCycle();
		}
		return true;
	}

	public ref class csWizardPitchCatchTemplate : public csWizardTemplate
	{
	public:
		csWizardPitchCatchTemplate(csHWDeviceOEMPA^ device);
		~csWizardPitchCatchTemplate();

		csWizardPitchCatchScan^ Scan;
		int m_iElementStopEmission;
		int m_iElementStopReception;

		void TemplateEdit(String^ filename, bool bCloseWaiting);
		bool TemplateToWizard();
		bool ReadWizard([Out] csRoot^% root, [Out] array<csCycle^>^% cycle, [Out] array<csFocalLaw^>^% emission, [Out] array<csFocalLaw^>^% reception);
		bool WizardToFile(String^ file) { return wizardToFileOrHW(file); };
		bool WizardToHw() { return wizardToFileOrHW(nullptr); };
		property csHWDeviceOEMPA^ HWDeviceOEMPA {
			csHWDeviceOEMPA^ get()
			{
				return hwDeviceOEMPA;
			}
			void set(csHWDeviceOEMPA^ value)
			{
				hwDeviceOEMPA = value;
			}
		}

		void EditFile(String^ filename, bool bCloseWaiting);
		static bool GetWizardFolder(String^% filename);

		static void EnableEventUpdate(bool bEnable)
		{
			UTKernel_EnableEventUpdate(bEnable);
		}
	protected:
		!csWizardPitchCatchTemplate();

	private://local variable of "CDlgSetupFiles::WizardFileSamplesPitchCatch1"
		bool wizardToFileOrHW(String^ file);
		bool WizardUpdateScan();
		bool Alloc(int iCycleCount);
		bool Desalloc();

		//struct for final results (allocated only one time)
		csRoot^ root;
		array<csCycle^>^ cycle;
		array<csFocalLaw^>^ emission;
		array<csFocalLaw^>^ reception;
		csHWDeviceOEMPA^ hwDeviceOEMPA;
	};
	csWizardPitchCatchTemplate::csWizardPitchCatchTemplate(csHWDeviceOEMPA^ device) :
		csWizardTemplate(device->GetDeviceId())
	{
		Specimen = gcnew csWizardSpecimen();
		Wedge = gcnew csWizardWedge();
		Probe = gcnew csWizardProbe();
		GateAscan = gcnew csWizardGateAscan();
		hwDeviceOEMPA = device;
		Scan = gcnew csWizardPitchCatchScan();
		Scan->SetScanCount(1);
		m_iDeviceId = device->GetDeviceId();

		//struct for final results (allocated only one time)
		root = nullptr;
		cycle = nullptr;
		emission = nullptr;
		reception = nullptr;
	}
	csWizardPitchCatchTemplate::~csWizardPitchCatchTemplate()
	{
		this->!csWizardPitchCatchTemplate();
	}
	csWizardPitchCatchTemplate::!csWizardPitchCatchTemplate()
	{
		delete Specimen;
		delete Wedge;
		delete Probe;
		delete GateAscan;
		Specimen = nullptr;
		Wedge = nullptr;
		Probe = nullptr;
		GateAscan = nullptr;
		Free();
	}

	bool csWizardPitchCatchTemplate::TemplateToWizard()
	{
		const int iMaxCycleCount = 4096;
		CHWDeviceOEMPA* pHWDeviceOEMPA = (CHWDeviceOEMPA*)hwDeviceOEMPA->cGetHWDeviceOEMPA();
		double dSpecimenRadius = Specimen->Radius;
		bool bRet = true, bMakeCycles = false;
		int iCountE = 0, iCountR = 0, iCount;
		int iElementStartE[iMaxCycleCount]{ 0 };
		int iElementStartR[iMaxCycleCount]{ 0 };
		double AngleE[iMaxCycleCount]{ 0 };
		double AngleR[iMaxCycleCount]{ 0 };

		switch (Scan->PitchCatchDefinition)
		{
		case csEnumPitchCatchDefinition::csLinear:
			bMakeCycles = true;
			if (!Scan->ElementStepEmission || !Scan->ElementStepReception)
			{
				UTKernel_SystemMessageBox(L"Too many cycles or bad step");
				return false;
			}
			iCountE = (Scan->ElementStopEmission - Scan->ElementStartEmission) / Scan->ElementStepEmission;
			iCountR = (Scan->ElementStopReception - Scan->ElementStartReception) / Scan->ElementStepReception;
			iCount = MIN(iCountE, iCountR);
			if (iCount > iMaxCycleCount || iCount <= 0)
			{
				UTKernel_SystemMessageBox(L"Too many cycles or bad step");
				return false;
			}
			for (int i = 0; i < iCount; i++)
			{
				iElementStartE[i] = Scan->ElementStartEmission + i * Scan->ElementStepEmission;
				iElementStartR[i] = Scan->ElementStartReception + i * Scan->ElementStepReception;
				AngleE[i] = Scan->AngleStartEmission;
				AngleR[i] = Scan->AngleStartReception;
			}
			break;

		case csEnumPitchCatchDefinition::csSector:
			bMakeCycles = true;
			if (!Scan->AngleStepEmission || !Scan->AngleStepReception)
			{
				UTKernel_SystemMessageBox(L"Too many cycles or bad step");
				return false;
			}
			iCountE = int(Math::Round((Scan->AngleStopEmission - Scan->AngleStartEmission) / Scan->AngleStepEmission));
			iCountR = int(Math::Round((Scan->AngleStopReception - Scan->AngleStartReception) / Scan->AngleStepReception));
			iCount = MIN(iCountE, iCountR);
			if (iCount > iMaxCycleCount)
			{
				UTKernel_SystemMessageBox(L"Too many cycles or bad step");
				return false;
			}
			for (int i = 0; i < iCount; i++)
			{
				AngleE[i] = Scan->AngleStartEmission + i * Scan->AngleStepEmission;
				AngleR[i] = Scan->AngleStartReception + i * Scan->AngleStepReception;
				iElementStartE[i] = Scan->ElementStartEmission;
				iElementStartR[i] = Scan->ElementStartReception;
			}
			break;
		}
		if (bMakeCycles)
		{
			Scan->SetScanCount(iCount);
			for (int i = 0; i < iCount; i++)
			{
				Scan->Cycles[i]->AngleEmission = AngleE[i];
				Scan->Cycles[i]->AngleReception = AngleR[i];
				Scan->Cycles[i]->ElementStartEmission = iElementStartE[i];
				Scan->Cycles[i]->ElementStartReception = iElementStartR[i];
				Scan->Cycles[i]->ElementCountEmission = Scan->ElementCountEmission;
				Scan->Cycles[i]->ElementCountReception = Scan->ElementCountReception;
				Scan->Cycles[i]->DepthEmission = Scan->DepthEmission;
				Scan->Cycles[i]->DepthReception = Scan->DepthReception;
				Scan->Cycles[i]->DepthModeEmission = Scan->DepthModeEmission;
				Scan->Cycles[i]->DepthModeReception = Scan->DepthModeReception;
			}
		}
		
		double adDepthReception[g_iOEMDACCountMax];
		double adCscanStart[g_iOEMPAGateCountMax], adCscanRange[g_iOEMPAGateCountMax], adCscanThreshold[g_iOEMPAGateCountMax];
		enumRectification aeRectification[g_iOEMPAGateCountMax];
		enumGateModeAmp aeModeAmp[g_iOEMPAGateCountMax];
		enumGateModeTof aeModeTof[g_iOEMPAGateCountMax];
		double dAscanStart = 0.0, dAscanRange = 10.0e-6, dTimeSlot = 1000.0e-3;
		double dSpecimenVelocity, dProbePitch, dProbeFrequency, dWedgeHeight, dWedgeAngle, dWedgeVelocity;
		bool bLongitudinalWave, bWedgeEnable;
		int iProbeElementCount;
		int iCscanCount = 0;
		enumDepthMode eDepthMode;
		structRoot rootE;
		structCycle* pCycleE = NULL;
		CFocalLaw* pEmissionE = NULL;
		CFocalLaw* pReceptionE = NULL;
		structRoot rootR;
		structCycle* pCycleR = NULL;
		CFocalLaw* pEmissionR = NULL;
		CFocalLaw* pReceptionR = NULL;

		if (!m_bCheckWizardEnable)
			return false;
		if (!m_pWizardSystem)
			return false;
		if (!m_pChannel)
			return false;
		if (hwDeviceOEMPA == nullptr)
		{
			UTKernel_SystemMessageBox(L"hwDeviceOEMPA not set prior to TemplateToWizard!");
			bRet = false; goto end;
		}
		if (Scan->Cycles == nullptr)
		{
			UTKernel_SystemMessageBox(L"No cycles defined prior to TemplateToWizard!");
			bRet = false; goto end;
		}
		int iCycleCount = Scan->Cycles->Length;
		if (iCycleCount > cOEMPA_GetCycleCountMax(pHWDeviceOEMPA))
		{
			UTKernel_SystemMessageBox(L"Too Many Cycles");
			bRet = false; goto end;
		}
		if ((!dSpecimenRadius && m_pWizardSystem->Specimen().SetSpecimen(ePlane)) ||
			((dSpecimenRadius > 0.0) && m_pWizardSystem->Specimen().SetSpecimen(eDisk)))
			return false;
		if ((dSpecimenRadius > 0.0) && m_pWizardSystem->Specimen().SetRadius(dSpecimenRadius))
			return false;
		if (m_pWizardSystem->Probe(0).SetRadius(Probe->Radius))
			return false;
		if (Scan->Cycles->GetLength(0) <= 0)
			return false;
		if (m_pChannel->Probe(0).SetFirst(Probe->ElementOffset))
			return false;
		if (m_pChannel->Probe(0).SetScanCount(1))//Cycles->GetLength(0)))
			return false;
		iCscanCount = aGateCscan->Length;
		if (aGateCscan->Length > g_iOEMPAGateCountMax)
			return false;

		if (Specimen->Wave == csWave::csLongitudinal)
			bLongitudinalWave = true;
		else
			bLongitudinalWave = false;
		bWedgeEnable = Wedge->Enable;
		dSpecimenVelocity = Specimen->Velocity;
		dProbePitch = Probe->Pitch;
		dProbeFrequency = Probe->Frequency;
		dWedgeHeight = Wedge->Height;
		dWedgeAngle = Wedge->Angle;
		dWedgeVelocity = Wedge->Velocity;
		iProbeElementCount = Probe->ElementCount;
		pHWDeviceOEMPA = (CHWDeviceOEMPA*)hwDeviceOEMPA->cGetHWDeviceOEMPA();

		for (int iIndex = 0; iIndex < aGateCscan->Length; iIndex++)
		{
			if (!aGateCscan[iIndex]->Enable)
			{
				iCscanCount = iIndex;
				break;
			}
			adCscanStart[iIndex] = aGateCscan[iIndex]->Start;
			adCscanRange[iIndex] = aGateCscan[iIndex]->Range;
			adCscanThreshold[iIndex] = aGateCscan[iIndex]->Threshold;
			aeRectification[iIndex] = (enumRectification)aGateCscan[iIndex]->Rectification;
			aeModeAmp[iIndex] = (enumGateModeAmp)aGateCscan[iIndex]->ModeAmplitude;
			aeModeTof[iIndex] = (enumGateModeTof)aGateCscan[iIndex]->ModeTimeOfFlight;
		}

		//alloc see "CDlgSetupFiles::WizardFileSamplesPitchCatch1"
		Desalloc();
		if (!Alloc(iCycleCount))
		{
			bRet = false; goto end;
		}
		for (int iCycleIndex = 0; iCycleIndex < iCycleCount; iCycleIndex++)
		{//see "CDlgSetupFiles::WizardFileSamplesPitchCatch1"
			for(int i = 0; i < Scan->Cycles[iCycleIndex]->DepthReception->Length; i++)
				adDepthReception[i] = Scan->Cycles[iCycleIndex]->DepthReception[i];

			//emission
			switch (Scan->Cycles[iCycleIndex]->DepthModeEmission)
			{
			case csEnumDepthMode::csTrueDepth: eDepthMode = eStandardDepth; break;
			case csEnumDepthMode::csSoundPath: eDepthMode = eSoundPath; break;
			case csEnumDepthMode::csTrueDepthBigBar: eDepthMode = eDepthBigBar; break;
			}
			if (!OEMPA_WriteWizard(m_pWizardSystem, m_pChannel,
				bLongitudinalWave, dSpecimenVelocity,
				iProbeElementCount, dProbePitch, dProbeFrequency,
				bWedgeEnable,
				dWedgeHeight, dWedgeAngle, dWedgeVelocity,
				Scan->Cycles[iCycleIndex]->DepthEmission, adDepthReception,
				1/*iScanDepthReception*/,
				Scan->Cycles[iCycleIndex]->ElementCountEmission,
				true/*bLinear*/, Scan->Cycles[iCycleIndex]->ElementStartEmission, Scan->Cycles[iCycleIndex]->ElementStartEmission, 1,
				false/*!bLinear*/, Scan->Cycles[iCycleIndex]->AngleEmission, Scan->Cycles[iCycleIndex]->AngleEmission, 1.0,
				GateAscan->Start, GateAscan->Range, GateAscan->TimeSlot,
				iCscanCount, adCscanStart, adCscanRange, adCscanThreshold,
				aeRectification, aeModeAmp, aeModeTof,
				eDepthMode))
			{
				//AfxMessageBox(L"Error to update emission wizard!");
				bRet = false; goto end;
			}
			if (!WizardUpdateScan())
			{
				//AfxMessageBox(L"Error to update emission scan!");
				bRet = false; goto end;
			}
			if (!OEMPA_ReadWizard(pHWDeviceOEMPA, m_pWizardSystem, m_pChannel, rootE, pCycleE, pEmissionE, pReceptionE))
			{
				//AfxMessageBox(L"Error to read emission wizard results!");
				bRet = false; goto end;
			}
			//reception
			switch (Scan->Cycles[iCycleIndex]->DepthModeReception)
			{
			case csEnumDepthMode::csTrueDepth: eDepthMode = eStandardDepth; break;
			case csEnumDepthMode::csSoundPath: eDepthMode = eSoundPath; break;
			case csEnumDepthMode::csTrueDepthBigBar: eDepthMode = eDepthBigBar; break;
			}
			if (!OEMPA_WriteWizard(m_pWizardSystem, m_pChannel,
				bLongitudinalWave, dSpecimenVelocity,
				iProbeElementCount, dProbePitch, dProbeFrequency,
				bWedgeEnable,
				dWedgeHeight, dWedgeAngle, dWedgeVelocity,
				Scan->Cycles[iCycleIndex]->DepthReception[0], adDepthReception,
				Scan->Cycles[iCycleIndex]->DepthReception->Length/*iScanDepthReception*/,
				Scan->Cycles[iCycleIndex]->ElementCountReception,
				true/*bLinear*/, Scan->Cycles[iCycleIndex]->ElementStartReception, Scan->Cycles[iCycleIndex]->ElementStartReception, 1,
				false/*!bLinear*/, Scan->Cycles[iCycleIndex]->AngleReception, Scan->Cycles[iCycleIndex]->AngleReception, 1.0,
				GateAscan->Start, GateAscan->Range, GateAscan->TimeSlot,
				iCscanCount, adCscanStart, adCscanRange, adCscanThreshold,
				aeRectification, aeModeAmp, aeModeTof,
				eDepthMode))
			{
				//AfxMessageBox(L"Error to update reception wizard!");
				bRet = false; goto end;
			}
			if (!WizardUpdateScan())
			{
				//AfxMessageBox(L"Error to update reception scan!");
				bRet = false; goto end;
			}
			if (!OEMPA_ReadWizard(pHWDeviceOEMPA, m_pWizardSystem, m_pChannel, rootR, pCycleR, pEmissionR, pReceptionR))
			{
				//AfxMessageBox(L"Error to read reception wizard results!");
				bRet = false; goto end;
			}

			//merge
			//memcpy(&pCycle[iCycleIndex], pCycleE, sizeof(structCycle));
			//pEmission[iCycleIndex] = *pEmissionE;
			//pReception[iCycleIndex] = *pReceptionR;
			if (!cycle[iCycleIndex]->vCopyFrom(pCycleE))
			{
				bRet = false; goto end;
			}
			if (!emission[iCycleIndex]->vCopyFrom(pEmissionE))
			{
				bRet = false; goto end;
			}
			if (!reception[iCycleIndex]->vCopyFrom(pReceptionR))
			{
				bRet = false; goto end;
			}

			//desalloc emission
			OEMPA_DesallocCycle(pCycleE);
			pCycleE = NULL;
			if (pEmissionE)
				OEMPA_DesallocFocalLaw(pEmissionE);
			pEmissionE = NULL;
			if (pReceptionE)
				OEMPA_DesallocFocalLaw(pReceptionE);
			//desalloc reception
			OEMPA_DesallocCycle(pCycleR);
			pCycleR = NULL;
			if (pEmissionR)
				OEMPA_DesallocFocalLaw(pEmissionR);
			pEmissionR = NULL;
			if (pReceptionR)
				OEMPA_DesallocFocalLaw(pReceptionR);
		}
		root->vCopyFrom(&rootE);
		root->iCycleCount = iCycleCount;
	end:
		//desalloc see "CDlgSetupFiles::WizardFileSamplesPitchCatch1"
		//desalloc emission
		OEMPA_DesallocCycle(pCycleE);
		pCycleE = NULL;
		if (pEmissionE)
			OEMPA_DesallocFocalLaw(pEmissionE);
		pEmissionE = NULL;
		if (pReceptionE)
			OEMPA_DesallocFocalLaw(pReceptionE);
		//desalloc reception
		OEMPA_DesallocCycle(pCycleR);
		pCycleR = NULL;
		if (pEmissionR)
			OEMPA_DesallocFocalLaw(pEmissionR);
		pEmissionR = NULL;
		if (pReceptionR)
			OEMPA_DesallocFocalLaw(pReceptionR);

		return bRet;
	}

	bool csWizardPitchCatchTemplate::WizardUpdateScan()
	{
		//see bool CDlgSetupFiles::WizardUpdateScan()
		if (!m_pChannel)
			return false;
		//	return (*m_pChannel).GetProbe(0)->Scan(0).UpdateScan();
		//	//1.1.3.2c return true;
		//}
		//bool CDlgHW::WizardUpdateScan()
		//{
		CUTScan* pScan;
		int iScanCount;

		if (!m_pChannel)
			return false;
		if (!m_pChannel->IsMultiChannels())
		{
			pScan = &m_pChannel->Probe().Scan();//single channel
			return pScan->UpdateScan();
		}
		else {
			iScanCount = m_pChannel->Probe(0).GetScanCount();
			if (!iScanCount)
				return false;
			for (int iScanIndex = 0; iScanIndex < iScanCount; iScanIndex++)
			{
				pScan = &m_pChannel->Probe(0).Scan(iScanIndex);//multi channel
				if (!pScan->UpdateScan())
					return false;
			}
			return true;
		}
	}

	bool csWizardPitchCatchTemplate::ReadWizard([Out] csRoot^% _root, [Out] array<csCycle^>^% _cycle, [Out] array<csFocalLaw^>^% _emission, [Out] array<csFocalLaw^>^% _reception)
	{
		if (!m_bCheckWizardEnable)
			return false;
		if ((root != nullptr) && (cycle != nullptr) && (emission != nullptr) && (reception != nullptr))
		{
			_root = root;
			_cycle = cycle;
			_emission = emission;
			_reception = reception;
			return true;
		}
		else
			return false;
	}

	bool csWizardPitchCatchTemplate::wizardToFileOrHW(String^ file)
	{
		CHWDeviceOEMPA* pHWDeviceOEMPA;
		wchar_t* pFile;
		bool bRet = true;
		structRoot Root;
		structCycle* pCycle = NULL;
		CFocalLaw* pEmission = NULL, * pReception = NULL;

		if (!m_bCheckWizardEnable)
			return false;
		if (hwDeviceOEMPA == nullptr)
			return false;
		pHWDeviceOEMPA = (CHWDeviceOEMPA*)hwDeviceOEMPA->cGetHWDeviceOEMPA();
		if (!pHWDeviceOEMPA)
			return false;

		if (!root->vCopyTo(&Root))
			bRet = false;
		Root.eHW = (enumHardware)hwDeviceOEMPA->GetSWDevice()->GetHardware();
		if (Root.iCycleCount > 0)
		{
			pCycle = OEMPA_AllocCycle(Root, Root.iCycleCount);
			pEmission = OEMPA_AllocFocalLaw(Root.iCycleCount);
			pReception = OEMPA_AllocFocalLaw(Root.iCycleCount);
			if (!pCycle || !pEmission || !pReception)
				return false;
			OEMPA_ResetArrayFocalLaw(Root.iCycleCount, pEmission);
			OEMPA_ResetArrayFocalLaw(Root.iCycleCount, pReception);

			if (pCycle)
			{
				for (int iCycle = 0; iCycle < Root.iCycleCount; iCycle++)
				{
					if (!cycle[iCycle]->vCopyTo(&pCycle[iCycle]))
						bRet = false;
				}
			}
			if (pEmission)
			{
				for (int iCycle = 0; iCycle < Root.iCycleCount; iCycle++)
				{
					if (!emission[iCycle]->vCopyTo(&pEmission[iCycle]))
						bRet = false;
				}
			}
			if (pReception)
			{
				for (int iCycle = 0; iCycle < Root.iCycleCount; iCycle++)
				{
					if (!reception[iCycle]->vCopyTo(&pReception[iCycle]))
						bRet = false;
				}
			}
		}

		if (file)
		{
			pFile = (wchar_t*)(void*)Marshal::StringToHGlobalUni(file);
			bRet = bRet && OEMPA_WriteFileText(pFile, &Root, pCycle, pEmission, pReception);
			Marshal::FreeHGlobal((IntPtr)pFile);
		}
		else
			bRet = bRet && OEMPA_WriteHW(pHWDeviceOEMPA, Root, pCycle, pEmission, pReception);

		if (Root.iCycleCount > 0)
		{
			OEMPA_DesallocCycle(pCycle);
			OEMPA_DesallocFocalLaw(pEmission);
			OEMPA_DesallocFocalLaw(pReception);
		}

		return bRet;
	}

	bool csWizardPitchCatchTemplate::GetWizardFolder(String^% filename)
	{
		filename = gcnew String(GetRootProgramDataFolder());
		return true;
	}

	void csWizardPitchCatchTemplate::TemplateEdit(String^ file, bool bCloseWaiting)
	{
		EditFile(file, bCloseWaiting);
	}

	void csWizardPitchCatchTemplate::EditFile(String^ file, bool bCloseWaiting)
	{
		SHELLEXECUTEINFO execinfo;
		wchar_t* pFile;

		pFile = (wchar_t*)(void*)Marshal::StringToHGlobalUni(file);
		memset(&execinfo, 0, sizeof(execinfo));
		execinfo.lpFile = pFile;
		execinfo.cbSize = sizeof(execinfo);
		execinfo.lpVerb = L"open";
		execinfo.fMask = SEE_MASK_NOCLOSEPROCESS;
		execinfo.nShow = SW_SHOWDEFAULT;
		execinfo.lpParameters = 0;
		ShellExecuteEx(&execinfo);
		if (bCloseWaiting)
			WaitForSingleObject(execinfo.hProcess, INFINITE);
		Marshal::FreeHGlobal((IntPtr)pFile);
	}

	bool csWizardPitchCatchTemplate::Alloc(int iCycleCount)
	{
		bool bRet = true;
		root = gcnew csRoot();
		cycle = gcnew array<csCycle^>(iCycleCount);
		if (cycle)
		{
			for (int iCycle = 0; iCycle < iCycleCount; iCycle++)
			{
				cycle[iCycle] = gcnew csCycle;
			}
		}
		emission = gcnew array<csFocalLaw^>(iCycleCount);
		if (emission)
		{
			for (int iCycle = 0; iCycle < iCycleCount; iCycle++)
			{
				emission[iCycle] = gcnew csFocalLaw;
			}
		}
		reception = gcnew array<csFocalLaw^>(iCycleCount);
		if (reception)
		{
			for (int iCycle = 0; iCycle < iCycleCount; iCycle++)
			{
				reception[iCycle] = gcnew csFocalLaw;
			}
		}
		return bRet;
	}

	bool csWizardPitchCatchTemplate::Desalloc()
	{
		if (root)
			delete root;
		root = nullptr;
		if (cycle)
		{
			for (int iCycle = 0; iCycle < cycle->Length; iCycle++)
			{
				if (cycle[iCycle] != nullptr)
					delete cycle[iCycle];
				cycle[iCycle] = nullptr;
			}
			if (cycle)
				delete cycle;
			cycle = nullptr;
		}
		if (emission)
		{
			for (int iCycle = 0; iCycle < emission->Length; iCycle++)
			{
				if (emission[iCycle] != nullptr)
					delete emission[iCycle];
				emission[iCycle] = nullptr;
			}
			if (emission)
				delete emission;
			emission = nullptr;
		}
		if (reception)
		{
			for (int iCycle = 0; iCycle < reception->Length; iCycle++)
			{
				if (reception[iCycle] != nullptr)
					delete reception[iCycle];
				reception[iCycle] = nullptr;
			}
			if (reception)
				delete reception;
			reception = nullptr;
		}
		return true;
	}
	/////////////////////////////////////////////////////////////////////////

#pragma endregion csWizardTemplateImplement
////////////////////////////////////////////////////////
////////////////////////////////////////////////////////

}

//DWORD WINAPI CallbackThreadToolbox(HWND hWnd,CRunToolbox *pRunToolbox)
//{
//	int iRootId;
//	CWaitCursor wait;
//
//	if(!UTKernel_IsToolboxRunning())
//	{
//		pRunToolbox->ResetPostFeedback();
//		UTKernel_ToolboxRun(false,hWnd,g_uiUTEventMessage,WPARAM_RUN_TOOLBOX,LPARAM_RUN_TOOLBOX_OK);
//		if(!pRunToolbox->WaitPostFeedback(5000,g_uiUTEventMessage,WPARAM_RUN_TOOLBOX,LPARAM_RUN_TOOLBOX_ERROR))
//			return 1;
//		pRunToolbox->ResetPostFeedback();
//		UTKernel_ToolboxDisplayCloseAll(hWnd,g_uiUTEventMessage,WPARAM_RUN_TOOLBOX,LPARAM_RUN_TOOLBOX_OK);
//		if(!pRunToolbox->WaitPostFeedback(5000,g_uiUTEventMessage,WPARAM_RUN_TOOLBOX,LPARAM_RUN_TOOLBOX_ERROR))
//			return 2;
//	}
//	if(m_bCheckWizardEnable)
//	{
//		if(m_pWizardSystem && m_pWizardSystem->GetID(iRootId))
//		{
//			pRunToolbox->ResetPostFeedback();
//			UTKernel_ToolboxDisplay(iRootId,hWnd,g_uiUTEventMessage,WPARAM_RUN_TOOLBOX,LPARAM_RUN_TOOLBOX_OK);
//			if(!pRunToolbox->WaitPostFeedback(5000,g_uiUTEventMessage,WPARAM_RUN_TOOLBOX,LPARAM_RUN_TOOLBOX_ERROR))
//				return 3;
//		}
//		if(m_pChannel && m_pChannel->GetID(iRootId))
//		{
//			pRunToolbox->ResetPostFeedback();
//			UTKernel_ToolboxDisplay(iRootId,hWnd,g_uiUTEventMessage,WPARAM_RUN_TOOLBOX,LPARAM_RUN_TOOLBOX_OK);
//			if(!pRunToolbox->WaitPostFeedback(5000,g_uiUTEventMessage,WPARAM_RUN_TOOLBOX,LPARAM_RUN_TOOLBOX_ERROR))
//				return 4;
//		}
//	}
//	return 0;//no error
//}
//
