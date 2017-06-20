create or replace procedure exptotext
IS
fd utl_file.file_type;
v_in varchar2(100);
begin
 fd:= utl_file.fopen('\\zoom\database\utl','exptotext.txt', 'w');
 utl_file.put_line(fd,'Номер договора теплоснабжения:'||chr(9)||'$$$1$$$'||'data');
 utl_file.put_line(fd,'Код схемы измерений:'||chr(9)||'$$$2$$$'||'data');
 utl_file.put_line(fd,'Код типа тепловычеслителя:'||chr(9)||'$$$3$$$'||'data');
 utl_file.put_line(fd,'Номер тепловычеслителя:'||chr(9)||'$$$4$$$'||'data');
 utl_file.put_line(fd,chr(10));
 utl_file.put_line(fd,'Канал измерения массы(объема):'||chr(9)||'$$$11$$$'||'M1'||chr(9)||'V1'||chr(9)||'M2'||chr(9)||'V2'||chr(9)||'M1гв');
 utl_file.put_line(fd,chr(10));
 utl_file.put_line(fd,'$$$data_start$$$');
 utl_file.put_line(fd,chr(13));
/* Вставка данных*/
 utl_file.put_line(fd,'------------------------------------------------------');

 utl_file.put_line(fd,chr(10));
 utl_file.put_line(fd,'$$$data_end$$$');
 utl_file.fclose(fd);
end;
/

