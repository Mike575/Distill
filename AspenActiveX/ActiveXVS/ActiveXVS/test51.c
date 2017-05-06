
#include <stdio.h>
//#include <intrins.h>
#include <math.h>
#include<stdlib.h>

#define PI                      3.1415926
#define EARTH_RADIUS            6378.137        //地球近似半径

unsigned char  flag_rec=0;
unsigned char num_rec=0;
   
unsigned char code kaijihuamian[]="HLJU-505";
unsigned char code receiving[]="Receiving!";
unsigned char code nodata[]="No BD data!";

char code TIME_AREA= 8;		//时区
unsigned char flag_data;	//数据标志位

//GPS数据存储数组

unsigned char JD[10];		//经度
unsigned char JD_a;		//经度方向
unsigned char WD[9];		//纬度
unsigned char WD_a;		//纬度方向
unsigned char date[6];		//日期
unsigned char time[6];		//时间
unsigned char time1[6];		//时间
unsigned char speed[5]={'0','0','0','0','0'};		//速度
unsigned char high[6];		//高度
unsigned char angle[5];		//方位角
unsigned char use_sat[2];	//使用的卫星数
unsigned char total_sat[2];	//天空中总卫星数
unsigned char lock;			//定位状态

//串口中断需要的变量
unsigned char seg_count;	//逗号计数器
unsigned char dot_count;	//小数点计数器
unsigned char byte_count;	//位数计数器
unsigned char cmd_number;	//命令类型
unsigned char mode;			//0：结束模式，1：命令模式，2：数据模式
unsigned char buf_full;		//1：整句接收完成，相应数据有效。0：缓存数据无效。
unsigned char cmd[5];		//命令类型存储数组

sbit rs	= P1^0;		//
sbit rw = P1^1;
sbit ep = P2^5;
/*****************************************table declare*************************************/

int radian(int d);
int get_distance(int lat1, int lng1, int lat2, int lng2);
int JD_int( );
int WD_int();
void get_distance_chars();
/*****************************************calculate  distance**********************************/


// 求弧度
int radian(int d)
{
    return d * PI / 180.0;   //角度1? = π / 180
}

//计算距离
int get_distance(int lat1, int lng1, int lat2, int lng2)
{
    int radLat1 = radian(lat1);
    int radLat2 = radian(lat2);
    int a = radLat1 - radLat2;
    int b = radian(lng1) - radian(lng2);

    int dst = 2 * asin((sqrt(pow(sin(a / 2), 2) + cos(radLat1) * cos(radLat2) * pow(sin(b / 2), 2) )));

    dst = dst * EARTH_RADIUS;
   // dst= round(dst * 10000) / 10000;
    return dst;
}



//JD[10]字符串数组变换成int,ddd.ddddd
	
int JD_int( )
{
	char *P_JD=&JD[0];
	int JDshu=atof(P_JD);
	int JDshuW=JDshu/10000%10;
	int JDshuQ=JDshu/1000%10;
	int JDshuB=JDshu/100%10;
	int JDshuS=JDshu/10%10;
	int JDshuG=JDshu/1%10;
	int JDshuX1=JDshu*10%10;
	int JDshuX2=JDshu*100%10;
	int JDshuX3=JDshu*1000%10;
	int JDshuX4=JDshu*10000%10;
	
	
	JDshu=JDshuW*100+JDshuQ*10+JDshuB*1+(JDshuS*10+JDshuG*1+JDshuX1*0.1+JDshuX2*0.01+JDshuX3*0.001+JDshuX4*0.0001)/60.0;		
	//60.0添加小数点避免取整，
	//dddmm.mmmm 
	return JDshu;
}

//WD[9]字符串数组变换成int,dd.dddddd

int WD_int()
{

	char *P_WD=&WD[0];
	int WDshu=atof(P_WD);
	int WDshuQ=WDshu/1000%10;
	int WDshuB=WDshu/100%10;
	int WDshuS=WDshu/10%10;
	int WDshuG=WDshu/1%10;
	int WDshuX1=WDshu*10%10;			//小数点后第一位
	int WDshuX2=WDshu*100%10;
	int WDshuX3=WDshu*1000%10;
	int WDshuX4=WDshu*10000%10;
	WDshu=atof(WD);
	WDshu=(WDshuQ/1000%10)*10+(WDshuB/100%10)*1+(WDshu-((WDshuQ/1000%10)*1000+(WDshuB/100%10)*100))/60.0;	
	//60.0添加小数点避免取整，
	//ddmm.mmmm
	return WDshu;
}

//获取distance字符串数组

unsigned char Distance[8];			//距离
unsigned char code Unit[]="Km";		//单位
int distance_int; 
void get_distance_chars()
{
	int lat2 = 39.90744; //纬度2
	int lng2 = 0.0;//经度2
	int lat1;
	int lng1;
	lat1=WD_int();
	lng1=JD_int();
	distance_int=get_distance(lat1,lng1,lat2,lng2);
	
	Distance[0]='0'+distance_int/10000%10;	//万位	
	Distance[1]='0'+distance_int/1000%10;
	Distance[2]='0'+distance_int/100%10;
	Distance[3]='0'+distance_int/10%10;
	Distance[4]='0'+distance_int/1%10;
	Distance[5]='.';
	Distance[6]='0'+distance_int * 10 % 10;		//小数第一位
	Distance[7]='0'+distance_int * 100 % 10;		//小数第二位

}



//----------------------------------------------------------------------------
void main (void) 
{	
	unsigned char i;


				while(JD[i] != '\0')
				{
					lcd_wdat(JD[i]);	// 显示字符经度
					i++;
				}
				delayms(10);
				i=0	 ;


		/*		lcd_pos(0x40);			// 设置显示位置
				while(WD[i] != '\0')
				{
					lcd_wdat(WD[i]);	// 显示字符 纬度
					i++;
				}
				delayms(10);
				i=0	 ;*/
				
				get_distance_chars();
				lcd_pos(0x40);			// 设置显示位置
				while(Distance[i] != '\0')
				{
					lcd_wdat(Distance[i]);	// 显示字符 距离
					//unsigned char code Unit[]="Km";		//单位
					i++;
				}
				delayms(10);
				i=0	 ;



 
}

