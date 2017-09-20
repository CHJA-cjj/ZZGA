
sqlplus led/led@dsdb  @D:\HRV\TransformDB.v.1.0\T102.sql

sqlplus asit_zhzx/asit_zhzx @zzqbxtdb1  @D:\HRV\TransformDB.v.1.0\T111.sql


D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_QCLJQ;"
D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_QCLJQ.V_DLP_QCLJQ.csv


D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_ZDJQ;"
D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_ZDJQ.V_DLP_ZDJQ.csv

D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_GGXX;"
D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_GGXX.V_DLP_GGXX.csv



D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_CZLS;"
D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_CZLS.V_DLP_CZLS.csv



D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_QYAYFB;"
D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_QYAYFB.V_DLP_QYAYFB.csv

D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_JJXQ;"
D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_JJXQ.V_DLP_JJXQ.csv

 
D:\HRV_2.0\DataBase\bin\mysql -h localhost -port=13406 --user=root --password=11111111   shenzhengongan -e "truncate V_DLP_ZZSJRY;"
D:\HRV_2.0\DataBase\bin\mysqlimport --host=localhost  --port=13406 -u root --password=11111111 shenzhengongan --ignore-lines=1 --fields-terminated-by="|" --lines-terminated-by="\n" -L -i D:\HRV\TransformDB.v.1.0\V_DLP_ZZSJRY.V_DLP_ZZSJRY.csv
 
