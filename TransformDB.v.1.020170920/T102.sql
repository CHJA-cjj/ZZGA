
set heading off;
set term off  ;
set feedback off;
set linesize 1000;
set pagesize 30000;
set trimout on;
set trimspool on;



--侵財
spool D:\HRV\TransformDB.v.1.0\V_DLP_QCLJQ.V_DLP_QCLJQ.csv; 
select trim(时间||'|'||抢劫||'|'||抢夺||'|'||诈骗||'|'||盗窃||'|'||敲诈勒索||'|'||网络诈骗)  from  dsecs.V_DLP_QCLJQ;


-- 重大警情||'|'||新建的表
spool D:\HRV\TransformDB.v.1.0\V_DLP_ZDJQ.V_DLP_ZDJQ.csv
SELECT  trim(接警员||'|'||to_char(报警时间,'YYYY-MM-DD')||'|'||to_char(报警时间,'HH24:mi:ss')||'|'||内容||'|'||案由) from  dsecs.V_DLP_ZDJQ; 

-- 公告信息
spool D:\HRV\TransformDB.v.1.0\V_DLP_GGXX.V_DLP_GGXX.csv
SELECT  trim(标题||'|'||内容||'|'||to_char(发布时间,'YYYY-MM-DD')||'|'||to_char(发布时间,'HH24:mi:ss')||'|'||发布人||'|'||失效时间) from dsecs.V_DLP_GGXX;


-- 接警席位 -------------------------------- 
spool d:\HRV\TransformDB.v.1.0\V_DLP_CZLS.V_DLP_CZLS.csv; 
select trim(坐席号||'|'||接警员姓名||'|'||操作类型||'|'||操作时间) from  dsecs.V_DLP_CZLS; 



-- 刑事警情, 交通事故分布
spool d:\HRV\TransformDB.v.1.0\V_DLP_QYAYFB.V_DLP_QYAYFB.csv; 
select trim(所属分局名称||'|'||案由类别||'|'||to_char(count(*),'99999')) from dsecs.V_DLP_QYAYFB ;


-- 今日警情分布 -------------------------------- 
spool d:\HRV\TransformDB.v.1.0\V_DLP_JJXQ.V_DLP_JJXQ.csv; 
select trim(市区总量||'|'||天元分局||'|'||芦淞分局||'|'||荷塘分局||'|'||石峰分局||'|'||董家段分局||'|'||田心分局||'|'||株洲县公安局||'|'||醴陵市公安局||'|'||攸县公安局||'|'||茶陵县公安局||'|'||炎陵县公安局) from  dsecs.V_DLP_JJXQ;


spool off;
exit;



