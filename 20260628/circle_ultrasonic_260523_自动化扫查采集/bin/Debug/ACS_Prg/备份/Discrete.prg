#/ Controller version = 3.14.01
#/ Date = 12/12/2024 2:47 PM
#/ User remarks = 
#3
!PNAME=
!PDESC=
ENABLE(X,Z)
int k=0

loop MyMatrixRowsCount
ptp/ev(X,Z),MyMatrix(k)(0)+PosXOffset,MyMatrix(k)(1),ScanSpeed
TILL (MST(X).#MOVE=0)&(MST(Z).#MOVE=0)
if(Trigger0_0=1)
OUT(0).0=1
end
WAIT(WaitTime)
if(Trigger0_0=1)
OUT(0).0=0
end
k=k+1
end
!ptp/ev(X,Z),0,0,ScanSpeed
!WAIT(WaitTime)
!DISP "Over!"
!TILL (MST(X).#MOVE=0)&(MST(Z).#MOVE=0)
STOP
#4
!PNAME=
!PDESC=
ENABLE(X,Y,Z)

DISP "Step0:Start Out0.1=1"
OUT(0).1=1	!Electromagnet action

DISP "Step1:PreRun!"
JOG/v X,AngleJogSpeed	!JOG for a short time
WAIT AngleJogDelayTime
!HALT X
!TILL (MST(X).#MOVE=0)

DISP "Step2:Run"
PTP/v X,abs(AnglePosTarget-FPOS(Y)),AnglePosSpeed
TILL (MST(X).#MOVE=0)

DISP "Step3:Over!"
STOP
#A
!PNAME=
!PDESC=
!axisdef X=0,Y=1,Z=2,T=3,A=4,B=5,C=6,D=7
!axisdef x=0,y=1,z=2,t=3,a=4,b=5,c=6,d=7
global int I(100),I0,I1,I2,I3,I4,I5,I6,I7,I8,I9,I90,I91,I92,I93,I94,I95,I96,I97,I98,I99
global real V(100),V0,V1,V2,V3,V4,V5,V6,V7,V8,V9,V90,V91,V92,V93,V94,V95,V96,V97,V98,V99
AXISDEF X=0,Y=1,Z=2
GLOBAL real PosXOffset=0,PosYOffset=0,PosZOffset=0
GLOBAL real MyMatrix(25000)(3)
GLOBAL REAL arrZ(100)
GLOBAL REAL ScanSpeed
GLOBAL INT MyMatrixRowsCount,WaitTime,Trigger0_0,Trigger0_1
GLOBAL REAL AngleJogDelayTime=0,AngleJogSpeed=0,AnglePosTarget=0,AnglePosSpeed=0	!Angle setting parameter

