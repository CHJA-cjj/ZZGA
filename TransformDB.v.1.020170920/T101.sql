
set heading off;
set term off  ;
set feedback off;
set linesize 1000;
set pagesize 30000;
set trimout on;
set trimspool on;



--月度警情同比
spool D:\HRV\TransformDB.v.1.0\V_DLP_YDJQTB.V_DLP_YDJQTB.csv; 
select trim(日期||'|'||刑事警情||'|'||治安警情||'|'||交通事故||'|'||群众求助) from dsecs.V_DLP_YDJQTB;

--今日警情
spool D:\HRV\TransformDB.v.1.0\V_DLP_TODAY_ZZ.V_DLP_TODAY_ZZ.csv; 
select trim(呼入总数||'|'||有效警情数||'|'||刑事警情||'|'||治安警情||'|'||交通事故||'|'||群众求助) from  dsecs.V_DLP_TODAY_ZZ;


--警情类型占比
spool D:\HRV\TransformDB.v.1.0\V_DLP_JRJQLX.v_dlp_jrjqlx.csv;
select trim(类型名称||'|'||接警总数 ) from dsecs.v_dlp_jrjqlx;

--两枪一盗辖区分布
spool D:\HRV\TransformDB.v.1.0\V_DLP_LQYDFB.V_DLP_LQYDFB.csv; 
select trim(单位名称||'|'||数量 ) from  dsecs.V_DLP_LQYDFB ;

 
--两枪一盗
spool D:\HRV\TransformDB.v.1.0\V_DLP_LQYD.V_DLP_LQYD.csv; 
select trim(时间||'|'||机动车||'|'||摩托车||'|'||入室||'|'|| 扒窃警情||'|'|| 盗窃||'|'||抢劫||'|'|| 抢夺||'|'|| 两抢一盗总数)  from  dsecs.V_DLP_LQYD  ;



--今日值班
spool D:\HRV\TransformDB.v.1.0\V_DLP_JRZB.V_DLP_JRZB.csv; 
select trim(值班日期||'|'||值班单位||'|'||值班局长||'|'||值班主任||'|'||值班长||'|'||序号||'|'||类别 ) from  dsecs.V_DLP_JRZB where 值班单位 in ('茶陵县公安局','董家段分局','荷塘分局','芦淞分局','石峰分局','天元分局','田心分局','炎陵县公安局','株洲县公安局','攸县公安局','醴陵市公安局'); 


--刑事
-- spool D:\HRV\TransformDB.v.1.0\V_DLP_BDYZBL.V_DLP_BDYZBL.csv; 
-- select trim(时间||'|'||故意杀人||'|'||故意伤害||'|'||强奸||'|'|| 抢劫||'|'||贩卖毒品||'|'||放火||'|'||爆炸||'|'|| 投毒||'|'||刑事总数)  from  dsecs.V_DLP_BDYZBL;

--刑事 新
spool D:\HRV\TransformDB.v.1.0\V_DLP_XSJQLX.V_DLP_XSJQLX.csv; 
select trim(类型名称||'|'||接警总数)  from  dsecs.V_DLP_XSJQLX;



spool off;
exit;
