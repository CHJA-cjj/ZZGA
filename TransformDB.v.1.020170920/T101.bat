
sqlplus led/led@dsdb  @D:\HRV\TransformDB.v.1.0\T101.sql


D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_YDJQTB;"
D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_YDJQTB.V_DLP_YDJQTB.csv

D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_TODAY_ZZ;"
D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_TODAY_ZZ.V_DLP_TODAY_ZZ.csv

D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_JRJQLX;"
D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_JRJQLX.V_DLP_JRJQLX.csv

D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_LQYDFB;"
D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_LQYDFB.V_DLP_LQYDFB.csv

D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_LQYD;"
D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_LQYD.V_DLP_LQYD.csv

D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_JRZB;"
D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_JRZB.V_DLP_JRZB.csv


rem D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_BDYZBL;"
rem D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_BDYZBL.V_DLP_BDYZBL.csv



D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_XSJQLX;"
D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_XSJQLX.V_DLP_XSJQLX.csv
