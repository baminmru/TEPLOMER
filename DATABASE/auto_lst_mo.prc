create or replace procedure auto_lst_mo
-- ��������� ������� ���. �� ����� �� ���� �����.
AUTHID CURRENT_USER
IS
s_date  varchar2(24);   -- ���� ���������
e_date  varchar2(24);   -- ���� ��������
begin

     s_date := to_char(add_months(sysdate,-1),'DD.MM.YYYY');
     e_date := to_char(sysdate-1,'DD.MM.YYYY');
     counters.lst_full(s_date,e_date);

end;
/

