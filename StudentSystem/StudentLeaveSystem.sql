create database studentLeaveSystem
on(
name='studentLeaveSystem',
filename='D:\MyDB\studentLeaveSystem.mdf',
size=5mb
)
go
use studentLeaveSystem
go
create table Roles--角色表
(
RoleId int primary key identity(1,1),
RoleName nvarchar(50) not null,--角色名
)
go
insert into Roles values('学工助理')
insert into Roles values('班主任')
insert into Roles values('管理员')
go
--select *from Roles
go
create table Admins--管理员表
(
 Aid int primary key identity(1,1),
 AdminName nvarchar(50) not null,--管理员姓名
 AdminPwd nvarchar(50) not null,--管理员密码 
 RoleId int foreign key references Roles(RoleId)
)
go
update Admins set AdminPwd='12345' where AdminName='5'
go
insert into Admins values('李瑞清','123456',3)
insert into Admins values('李清','123456',1)
--select * from Admins
go
create table Gnb--功能表
(
  GnId int  primary key identity(1,1),
  GnName nvarchar(50) not null,--功能名称
)
go
insert into Gnb values('Left')
insert into Gnb values('Left1')
go
create table Qxb--权限表
(
  Qid int primary key identity(1,1),
  RoleId int foreign key references Roles(RoleId),
  GnId int foreign key references Gnb(GnId)
)
go
insert into Qxb values(1,2)
insert into Qxb values(2,2)
insert into Qxb values(3,1)

--select * from Admins,Roles,Qxb,Gnb where 
Admins.RoleId=Roles.RoleId and Qxb.GnId=Gnb.GnId and Roles.RoleId=Qxb.RoleId  
go
create table Grade--年级级别表
(
Gid int primary key identity(1,1),
GNum nvarchar(20),--级别名
)

go
--select * from Grade
go
create table Class --班级表
(
Cid int primary key identity(1,1),
CName nvarchar(50),--年级名
Gid int foreign key references Grade(Gid)--级别
)
--GO
--select GNum,CName from Grade,Class where Grade.Gid=Class.Gid and Class.Gid=1 and Cid=1
go
create table Student--学生表
(
  StuId int primary key identity(1,1),
  StuNum nvarchar(50) not null,--学号
  StuName nvarchar(50) not null,--姓名
  Phone nvarchar(30) not null,--手机号
  Gid int foreign key references  Grade(Gid),--级别
  Cid int foreign key references  Class(Cid),--班级
)

--select * from Student,Grade,Class where Student.Cid=Class.Cid  and Grade.Gid=Class.Cid
go
create table LeaveInfo--请假信息表
(
Lid int primary key identity(1,1),
StuNum nvarchar(50) not null,--学号,
StuName nvarchar(50) not null,--姓名
Phone nvarchar(30) not null,--手机号
GNum nvarchar(20) not null,--级别
CName nvarchar(20) not null,--班级
BeginDate date not null,--开始时间
EndDate date not null,--结束时间
Addresss nvarchar(50) not null,--地点
Reason nvarchar(200) not null,--原因
Principal nvarchar(10) not null,--负责人
Statu int ,--状态
)

--select top 0 * from LeaveInfo where Lid not in
--(select top 0  Lid from LeaveInfo)
--update LeaveInfo set Statu=1 where StuNum='41916078'

--select top 5 Lid, StuNum,StuName,Phone,GNum,CName,Date(date,BeginDate,23) BeginDate,
--CONVERT(varchar(100),EndDate,23) EndDate,Addresss,Reason,Principal,Statu
--from LeaveInfo where Lid not in(select top ((0*5)) Lid from LeaveInfo)

--select top 5 StuNum,StuName,Phone,GNum,CName
-- from Student,Grade,Class  where StuId not in(select top (1*5) Student.StuId from Student) and
-- Student.Gid=Grade.Gid and Grade.Gid=Class.Cid

select count(*) total from LeaveInfo where DateDiff(dd,BeginDate,getdate())=0

select *
from LeaveInfo where DateDiff(dd,BeginDate,getdate())=0 


--select top 5 *  from LeaveInfo where Lid not in
--(select top (1) Lid from LeaveInfo where  DateDiff(dd,BeginDate,getdate())=0 and GNum='2015' and CName='NET1') 
--and DateDiff(dd,BeginDate,getdate())=0 and GNum='2015' and CName='NET1'

--(select count(*) from LeaveInfo where DateDiff(dd,BeginDate,getdate())=1 ) zt,
--(select count(*) from LeaveInfo where DateDiff(dd,BeginDate,getdate())<=7 ) bz,
--(select count(*) from LeaveInfo where DateDiff(mm,BeginDate,getdate())=0 ) byue


select count(*)  from LeaveInfo where DateDiff(dd,BeginDate,getdate())=0 --今天

select count(*) from LeaveInfo where DateDiff(dd,BeginDate,getdate())=1--昨天
select count(*) from LeaveInfo where DateDiff(dd,BeginDate,getdate())<=7--7天内的所有数据
select count(*) from LeaveInfo where DateDiff(dd,BeginDate,getdate())<=30--30天内的所有数据
select count(*) from LeaveInfo where DateDiff(mm,BeginDate,getdate())=0--本月
select count(*) from LeaveInfo where DateDiff(yy,BeginDate,getdate())=0--本年

--select  a.CName,a.GNum from (select *  from LeaveInfo where DateDiff(dd,BeginDate,getdate())=0 ) as a,
--(select *  from LeaveInfo where DateDiff(dd,BeginDate,getdate())=1 ) b,LeaveInfo 
--WHERE a.GNum=LeaveInfo.GNum  and b.GNum=a.GNum

SELECT A.CName AS 班级名称, A.GNum AS 年级,
    (SELECT COUNT(*) FROM [dbo].[LeaveInfo] AS B 
      WHERE B.CName = A.CName AND B.GNum = A.GNum AND B.[Statu] = 1
        AND B.BeginDate >= DATEADD(DD,DATEDIFF(DD,0,GETDATE()),0) 
        AND B.EndDate < DATEADD(DD,DATEDIFF(DD,0,GETDATE()),1)) AS 今日请假,
    (SELECT COUNT(*) FROM [dbo].[LeaveInfo] AS B 
      WHERE B.CName = A.CName AND B.GNum = A.GNum AND B.[Statu] <= 1 
        AND B.BeginDate >= DATEADD(DD,DATEDIFF(DD,0,GETDATE()),-1) 
        AND B.EndDate < DATEADD(DD,DATEDIFF(DD,0,GETDATE()),0)) AS 昨日请假,
    (SELECT COUNT(*) FROM [dbo].[LeaveInfo] AS B 
      WHERE B.CName = A.CName AND B.GNum = A.GNum AND B.[Statu] = 1 
        AND B.BeginDate >= DATEADD(WK,DATEDIFF(WK,0,GETDATE()),0) 
        AND B.EndDate < DATEADD(WK,DATEDIFF(WK,0,GETDATE()),7)) AS 本周请假,
    (SELECT COUNT(*) FROM [dbo].[LeaveInfo] AS B 
      WHERE B.CName = A.CName AND B.GNum = A.GNum AND B.[Statu] = 1 
        AND B.BeginDate >= DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0) 
        AND B.EndDate < DATEADD(MM,DATEDIFF(MM,0,GETDATE()) + 1,0)) AS 本月请假
FROM [dbo].[LeaveInfo] AS A 
GROUP BY A.CName, A.GNum

SELECT A.CName AS 班级名称, A.GNum AS 年级,
    (SELECT COUNT(*) FROM [dbo].[LeaveInfo] AS B 
      WHERE B.CName = A.CName AND B.GNum = A.GNum 
        AND DateDiff(dd,BeginDate,getdate())=0) AS 今日请假,
    (SELECT COUNT(*) FROM [dbo].[LeaveInfo] AS B 
      WHERE B.CName = A.CName AND B.GNum = A.GNum 
         
        AND DateDiff(dd,BeginDate,getdate())=1) AS 昨日请假,
    (SELECT COUNT(*) FROM [dbo].[LeaveInfo] AS B 
      WHERE B.CName = A.CName AND B.GNum = A.GNum 
        
        AND DateDiff(dd,BeginDate,getdate())<=7) AS 本周请假,
    (SELECT COUNT(*) FROM [dbo].[LeaveInfo] AS B 
      WHERE B.CName = A.CName AND B.GNum = A.GNum 
        AND B.BeginDate >= DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0) 
        AND B.EndDate < DATEADD(MM,DATEDIFF(MM,0,GETDATE()) + 1,0)) AS 本月请假
FROM [dbo].[LeaveInfo] AS A 
GROUP BY A.CName, A.GNum
 
 select count(*) xjTotal from LeaveInfo where Statu=1

 
select count(*) xjTotal  from LeaveInfo where DateDiff(dd,EndDate,getdate())=1 and Statu=1
 --select CName,count(*) 本月  from LeaveInfo where DateDiff(mm,BeginDate,getdate())=0 group by CName

