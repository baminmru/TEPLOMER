CREATE OR REPLACE PROCEDURE CHEK_DAY_ARCHIVE
( pid_bd number, pdcounter date ) is
begin
  declare
    sV1 number(18,6);
    sV2 number(18,6);
    sM1 number(18,6);
    sM2 number(18,6);
  
    dV1 number(18,6);
    dV2 number(18,6);
    dM1 number(18,6);
    dM2 number(18,6);
    hCNT number(4);
    result varchar2(2);
    presult varchar2(2);
    dStart date;
    dEnd date;
  
  begin

      dStart:=TO_DATE(TO_CHAR(pdcounter,'YYYY/MM/DD') || ' 00:00:00','YYYY/MM/DD HH24:MI:SS');
      dEnd:=TO_DATE(TO_CHAR(pdcounter,'YYYY/MM/DD')||' 23:59:59','YYYY/MM/DD  HH24:MI:SS');
      SELECT COUNT(*) into hCNT  FROM  DATACURR WHERE ID_BD= pid_bd and id_ptype=3 and dcounter>= dStart and dcounter <= dEnd;

      if hCNT <24 then
        result := '3';
      end if;

      if hCNT = 24 then
         SELECT nvl((V1),0), nvl((V2),0), nvl((M1),0), nvl((M2),0),nvl(d_eql_24,'0') into dV1,dV2,dM1,dM2,presult
         FROM  DATACURR WHERE ID_BD= pid_bd and id_ptype=4 and dcounter>= dStart and dcounter<=dEnd;


         SELECT nvl(sum(V1),0), nvl(sum(V2),0), nvl(sum(M1),0), nvl(sum(M2),0) into sV1,sV2,sM1,sM2
         FROM  DATACURR WHERE ID_BD=pid_bd and id_ptype=3 and dcounter>= dStart and dcounter<=dEnd;

         result :=presult;

         if  dV1<>0 and ABS(dV1- sV1)> 0.5 then
             result := '2';
         end if;

         if  dV2<>0 and ABS(dV2- sV2)> 0.5 then
             result := '2';
         end if;


         if  dM1<>0 and ABS(dM1- sM1)> 0.5 then
             result:= '2';
         end if;

         if  dM2<>0 and ABS(dM2- sM2)> 0.5 then
             result := '2';
         end if;

         if  result = 0 then
               result := '1';
         end if;

      end if;

      update  DATACURR set d_eql_24 = result WHERE ID_BD = pid_bd and id_ptype=4 and dcounter>= dStart and dcounter<=dEnd;



      Commit;
  end ;
end ;
/

