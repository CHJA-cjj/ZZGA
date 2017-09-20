
set heading off;
set term off  ;
set feedback off;
set linesize 1000;
set pagesize 30000;
set trimout on;
set trimspool on;

-- 涉疆
spool d:\HRV\TransformDB.v.1.0\V_DLP_ZZSJRY.V_DLP_ZZSJRY.csv; 
select trim(HDFSDDSSGAJG||'|'||to_char(count(ZDRYBH),'99999')||'|'||RKSJ) from zzqbxtdb1.ZZSJRY where substr(RKSJ,1,8) = to_char(sysdate,'YYYYMMDD') group by HDFSDDSSGAJG, RKSJ
 


spool off;
exit;



