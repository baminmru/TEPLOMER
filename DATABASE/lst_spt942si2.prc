create or replace procedure lst_spt942si2(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst('Голикова 76 5968 ТЦ2',
                               'Голикова 76 5952 ТЦ1'
                               );
BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP
                counters.si2spt942(node_name(x),sdate,edate);
              END LOOP;
END;
/

