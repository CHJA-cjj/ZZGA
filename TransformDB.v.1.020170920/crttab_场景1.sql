
--月度警情同比
DROP TABLE V_DLP_YDJQTB;
CREATE TABLE V_DLP_YDJQTB(
rq   VARCHAR(50),
xsjq   int,
zajq   int,
jtsg   int,
qzqz   int
);

--今日警情
DROP TABLE V_DLP_TODAY_ZZ;
CREATE TABLE V_DLP_TODAY_ZZ(
hrzs   int,
jjzs   int,
yxjq   int,
xsjq   int,
zajq   int,
jtsg   int,
qzqz   int
);

--警情类型占比
DROP TABLE V_DLP_JRJQLX;
CREATE TABLE V_DLP_JRJQLX(
lxmc   VARCHAR(50),
jjzs   int
);

--两枪一盗辖区分布
DROP TABLE V_DLP_LQYDFB;
CREATE TABLE V_DLP_LQYDFB(
dwmc   VARCHAR(50),
volume   int 
);



-- 两抢一盗 -
DROP TABLE V_DLP_LQYD;
CREATE TABLE V_DLP_LQYD(
time   VARCHAR(50),
jdc   int ,
mtc   int ,
rs   int ,
bqjq   int ,
daoqie   int ,
qiangjie   int ,
qiangduo   int ,
lqydzs   int 
);
 

DROP VIEW V_DLP_LQYDBL_TOTAL_VIEW;
CREATE VIEW V_DLP_LQYDBL_TOTAL_VIEW as select sum(lqydzs) as sum from V_DLP_LQYD;

set names gbk; 
DROP VIEW V_DLP_LQYDBL_VIEW;
CREATE VIEW V_DLP_LQYDBL_VIEW as
select 1 as xh,'抢劫' as type, sum(qiangjie) as  casenum, sum(qiangjie)*100/sum as bl from V_DLP_LQYD T16, V_DLP_LQYDBL_TOTAL_VIEW T26
union
select 2 as xh,'抢夺' as type, sum(qiangduo) as  casenum, sum(qiangduo)*100/sum as bl from V_DLP_LQYD T17, V_DLP_LQYDBL_TOTAL_VIEW T27
union
select 3 as xh,'机动车' as type, sum(jdc) as  casenum, sum(jdc)*100/sum as bl from V_DLP_LQYD T18, V_DLP_LQYDBL_TOTAL_VIEW T28
union
select 4 as xh,'摩托车' as type, sum(mtc) as  casenum, sum(mtc)*100/sum as bl from V_DLP_LQYD T19, V_DLP_LQYDBL_TOTAL_VIEW T29
union
select 5 as xh,'入室' as type, sum(rs) as  casenum, sum(rs)*100/sum as bl from V_DLP_LQYD T20, V_DLP_LQYDBL_TOTAL_VIEW T30
union
select 6 as xh,'扒窃' as type, sum(bqjq) as  casenum, sum(bqjq)*100/sum as bl from V_DLP_LQYD T21, V_DLP_LQYDBL_TOTAL_VIEW T31;



DROP VIEW V_DLP_LQYD_VIEW;
CREATE VIEW V_DLP_LQYD_VIEW as select type, casenum, bl from V_DLP_LQYDBL_VIEW order by xh;




--今日值班
DROP TABLE V_DLP_JRZB;
CREATE TABLE V_DLP_JRZB(
zbrq   VARCHAR(50),
zbdw   VARCHAR(50),
zbjz   VARCHAR(50),
zbzr   VARCHAR(50),
zbz   VARCHAR(50),
xh   int,
type   CHAR(5)
);


DROP VIEW V_DLP_JRZB_VIEW;
CREATE VIEW V_DLP_JRZB_VIEW as
select zbdw, zbjz, zbzr, zbz from V_DLP_JRZB;



-- 刑事警情类型---------------------------- 
DROP TABLE V_DLP_XSJQLX;
CREATE TABLE V_DLP_XSJQLX(
	type  VARCHAR(50),
	volume  int
);
-- 用这个建新指标给刑事组件使用
DROP VIEW V_DLP_XSJQLX_VIEW;
CREATE VIEW V_DLP_XSJQLX_VIEW as
select type,volume*100/sum from V_DLP_XSJQLX T12 ,(select sum(volume)as sum from V_DLP_XSJQLX) T22；
 



--刑事
DROP TABLE V_DLP_BDYZBL;
CREATE TABLE V_DLP_BDYZBL(
	sj    int,
	gysr    int,
	gysh    int,
	qj    int,
	qjie    int,
	fmdp    int,
	fh    int,
	bz    int,
	td    int,
	zs    int
);

set names gbk; 
DROP VIEW V_DLP_BDYZBLBL_VIEW;
CREATE VIEW V_DLP_BDYZBLBL_VIEW as
select '故意杀人' as type,sum(gysr)*100/sum(zs) as bl from V_DLP_BDYZBL
union 
select '故意伤害' as type,sum(gysh)*100/sum(zs) as bl from V_DLP_BDYZBL
union 
select '强奸' as type,sum(qj)*100/sum(zs) as bl from V_DLP_BDYZBL
union 
select '抢劫' as type,sum(qjie)*100/sum(zs) as bl from V_DLP_BDYZBL
union 
select '贩卖毒品' as type,sum(fmdp)*100/sum(zs) as bl from V_DLP_BDYZBL
union 
select '放火' as type,sum(fh)*100/sum(zs) as bl from V_DLP_BDYZBL
union 
select '爆炸' as type,sum(bz)*100/sum(zs) as bl from V_DLP_BDYZBL
union 
select '投毒' as type,sum(td)*100/sum(zs) as bl from V_DLP_BDYZBL;



--侵財
DROP TABLE V_DLP_QCLJQ;
CREATE TABLE V_DLP_QCLJQ(
	sj    int,
	qiangjie    int,
	qiangduo    int,
	zhapian    int,
	daoqie    int,
	qzls    int,
	wlzp    int
);
 
DROP VIEW V_DLP_QCLJQ_TOTAL_VIEW;
CREATE VIEW V_DLP_QCLJQ_TOTAL_VIEW as 
select sum(qiangjie)+sum(qiangduo)+sum(zhapian)+sum(daoqie)+sum(qzls)+sum(wlzp) as sum from V_DLP_QCLJQ;

set names gbk; 
DROP VIEW V_DLP_QCLJQBL_VIEW;
CREATE VIEW V_DLP_QCLJQBL_VIEW as
select '抢劫' as type,sum(qiangjie)*100/sum as volume from V_DLP_QCLJQ T11 ,V_DLP_QCLJQ_TOTAL_VIEW T21
union 
select '抢夺' as type,sum(qiangduo)*100/sum as volume from V_DLP_QCLJQ T12 ,V_DLP_QCLJQ_TOTAL_VIEW T22
union 
select '诈骗' as type,sum(zhapian)*100/sum as volume from V_DLP_QCLJQ T13 ,V_DLP_QCLJQ_TOTAL_VIEW T23
union 
select '盗窃' as type,sum(daoqie)*100/sum as volume from V_DLP_QCLJQ T14 ,V_DLP_QCLJQ_TOTAL_VIEW T24
union 
select '敲诈勒索' as type,sum(qzls)*100/sum as volume from V_DLP_QCLJQ T15 ,V_DLP_QCLJQ_TOTAL_VIEW T25
union 
select '网络诈骗' as type,sum(wlzp)*100/sum as volume from V_DLP_QCLJQ T16 ,V_DLP_QCLJQ_TOTAL_VIEW T26;

 
DROP TABLE V_DLP_QCLJQBL;
CREATE TABLE V_DLP_QCLJQBL(
	type  VARCHAR(50),
	volume  int
);

DROP TABLE V_DLP_BDYZBLBL;
CREATE TABLE V_DLP_BDYZBLBL(
	type  VARCHAR(50),
	volume  int
);

 
DROP TABLE V_DLP_LQYDBL;
CREATE TABLE V_DLP_LQYDBL(
	type  VARCHAR(50),
	volume  int
);

----重大警情和公告信息

DROP TABLE V_DLP_ZDJQ;
CREATE TABLE V_DLP_ZDJQ(
	jjy    varchar(50),
	jjrq    varchar(50),
	jjsj    varchar(50),
	content    varchar(200),
	anyou    varchar(50)
);


DROP TABLE V_DLP_GGXX;
CREATE TABLE V_DLP_GGXX(
	title    varchar(50),
	content    varchar(200),
	fbrq    varchar(50),
	fbsj    varchar(50),
	fbr    varchar(50),
	sxsj    varchar(50)
);


DROP VIEW V_DLP_ZDJQ_VIEW;
CREATE VIEW V_DLP_ZDJQ_VIEW as
select jjrq, jjsj, anyou, content from V_DLP_ZDJQ order by jjrq desc, jjsj desc ;

DROP VIEW V_DLP_GGXX_VIEW;
CREATE VIEW V_DLP_GGXX_VIEW as
select fbrq, fbsj, title, content from V_DLP_GGXX order by fbrq desc, fbsj desc ;
  

-- 接警席位 -------------------------------- 
DROP TABLE V_DLP_CZLS;
CREATE TABLE V_DLP_CZLS(
zxh   VARCHAR(50),
jjyxm   VARCHAR(50) ,
czlx   VARCHAR(50) ,
操作时间   VARCHAR(50) 
); 



DROP VIEW V_DLP_CZLS_VIEW;
CREATE VIEW V_DLP_CZLS_VIEW as
select zxh,czlx from V_DLP_CZLS T1 ,(select sum(volume)as sum from V_DLP_XSJQLX) T22；


-- 今日警情分布 -------------------------------- 

DROP TABLE V_DLP_JJXQ;
CREATE TABLE V_DLP_JJXQ(
       sqzl  int,
       tyfj  int,
       lsfj  int,
       htfj  int,
       sffj  int,
       djdfj int,
       txfj  int,
       zzxgaj int,
       llsgaj int,
       yxgaj  int,
       clxgaj int,
       ylxgaj int
);


DROP VIEW V_DLP_JJXQ_VIEW;
CREATE VIEW V_DLP_JJXQ_VIEW
(
	GAJG, JQSL
) as
select '市区总量', sqzl from V_DLP_JJXQ T11
union 
select '天元分局', tyfj from V_DLP_JJXQ T12
union 
select '芦淞分局', lsfj from V_DLP_JJXQ T13
union 
select '荷塘分局', htfj from V_DLP_JJXQ T14
union 
select '石峰分局', sffj from V_DLP_JJXQ T15
union 
select '董家段分局', djdfj from V_DLP_JJXQ T16
union 
select '田心分局', txfj from V_DLP_JJXQ T17
union 
select '株洲县公安局', zzxgaj from V_DLP_JJXQ T18
union 
select '醴陵市公安局', llsgaj from V_DLP_JJXQ T19
union 
select '攸县公安局', yxgaj from V_DLP_JJXQ T20
union 
select '茶陵县公安局', clxgaj from V_DLP_JJXQ T21
union 
select '炎陵县公安局	', ylxgaj from V_DLP_JJXQ T22;



-- 刑事警情, 交通事故分布-------------------------------- 
DROP TABLE V_DLP_QYAYFB;
CREATE TABLE V_DLP_QYAYFB(
	GAJG          varchar(50),
	ANYOU        varchar(50),
	CASENUM      int 
);




 -- 涉疆 ----------------------------
DROP TABLE V_DLP_ZZSJRY;
CREATE TABLE V_DLP_ZZSJRY(
	HDFSDDSSGAJG varchar(50),
	ZZSJRYCNT    int ,
	RKSJ    varchar(50)
);



-- 地图分布-------------------------------- 
DROP VIEW V_DLP_JJXQ_VIEW;
CREATE VIEW V_DLP_JJXQ_VIEW  as
select 
     T0.GAJG,    
     T0.JQSL ,
     T1.JL ,
     T2.ZZSJRYCNT ,
     T0.JQSL ,
from V_DLP_JJXQ_VIEW T0, V_DLP_QWXX T1, V_DLP_ZZSJRY T2, 
(select GAJG, CASENUM  from V_DLP_QYAYFB T13 where ANYOU	= '刑事警情') T3,
(select GAJG, CASENUM  from V_DLP_QYAYFB T14 where ANYOU	= '交通事故') T4
where T0.GAJG= T1.ORG_NAME
and T0.GAJG= T2.HDFSDDSSGAJG
and T0.GAJG= T3.GAJG
and T0.GAJG= T4.GAJG;
 
 
