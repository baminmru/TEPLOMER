delete from chartsettings;
commit;

insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,3,CFLD,1,c1.color,1, c2.color,c3.color,1  from bdevices join masks
on bdevices.id_mask_HOUR  = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
join colors c1 on c1.colorid=sequence*10
join colors c2 on c2.colorid=sequence*10+100
join colors c3 on c3.colorid=sequence*10+200
where cfld like 'T_' or cfld like 'TAIR';
commit;


insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,4,CFLD,1,c1.color,1, c2.color,c3.color,1  from bdevices join masks
on bdevices.id_mask_24  = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
join colors c1 on c1.colorid=sequence*10
join colors c2 on c2.colorid=sequence*10+100
join colors c3 on c3.colorid=sequence*10+200
where cfld like 'T_' or cfld like 'TAIR';
commit;



insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,1,CFLD,1,c1.color,1, c2.color,c3.color,1  from bdevices join masks
on bdevices.id_mask_curr = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
join colors c1 on c1.colorid=sequence*10
join colors c2 on c2.colorid=sequence*10+100
join colors c3 on c3.colorid=sequence*10+200
where cfld like 'T_' or cfld like 'TAIR';
commit;

------------------------- M

insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,3,CFLD,2,c1.color,1, c2.color,c3.color,1  from bdevices join masks
on bdevices.id_mask_HOUR  = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
join colors c1 on c1.colorid=sequence*10
join colors c2 on c2.colorid=sequence*10+100
join colors c3 on c3.colorid=sequence*10+200
where cfld like 'M%';
commit;


insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,4,CFLD,2,c1.color,1, c2.color,c3.color,1  from bdevices join masks
on bdevices.id_mask_24  = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
join colors c1 on c1.colorid=sequence*10
join colors c2 on c2.colorid=sequence*10+100
join colors c3 on c3.colorid=sequence*10+200
where cfld like 'M%';
commit;



insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,1,CFLD,2,c1.color,1, c2.color,c3.color,1  from bdevices join masks
on bdevices.id_mask_curr = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
join colors c1 on c1.colorid=sequence*10
join colors c2 on c2.colorid=sequence*10+100
join colors c3 on c3.colorid=sequence*10+200
where cfld like 'M%';
commit;

---------------- Q

insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,3,CFLD,0,c1.color,1, c2.color,c3.color,1  from bdevices join masks
on bdevices.id_mask_HOUR  = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
join colors c1 on c1.colorid=sequence*10
join colors c2 on c2.colorid=sequence*10+100
join colors c3 on c3.colorid=sequence*10+200
where cfld like 'Q%';
commit;


insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,4,CFLD,0,c1.color,1, c2.color,c3.color,1  from bdevices join masks
on bdevices.id_mask_24  = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
join colors c1 on c1.colorid=sequence*10
join colors c2 on c2.colorid=sequence*10+100
join colors c3 on c3.colorid=sequence*10+200
where cfld like 'Q%';
commit;



insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,1,CFLD,0,c1.color,1, c2.color,c3.color,1  from bdevices join masks
on bdevices.id_mask_curr = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
join colors c1 on c1.colorid=sequence*10
join colors c2 on c2.colorid=sequence*10+100
join colors c3 on c3.colorid=sequence*10+200
where cfld like 'Q%';
commit;


