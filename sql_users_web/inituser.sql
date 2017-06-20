delete from chartsettings;
commit;

insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,3,CFLD,1,100*sequence,1, 200*sequence,300*sequence,1  from bdevices join masks
on bdevices.id_mask_HOUR  = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
where cfld like 'T_' or cfld like 'TAIR';
commit;


insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,4,CFLD,1,100*sequence,1, 200*sequence,300*sequence,1  from bdevices join masks
on bdevices.id_mask_24  = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
where cfld like 'T_' or cfld like 'TAIR';
commit;



insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,1,CFLD,1,100*sequence,1, 200*sequence,300*sequence,1  from bdevices join masks
on bdevices.id_mask_curr = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
where cfld like 'T_' or cfld like 'TAIR';
commit;

------------------------- M

insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,3,CFLD,2,100*sequence,1, 200*sequence,300*sequence,1  from bdevices join masks
on bdevices.id_mask_HOUR  = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
where cfld like 'M%';
commit;


insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,4,CFLD,2,100*sequence,1, 200*sequence,300*sequence,1  from bdevices join masks
on bdevices.id_mask_24  = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
where cfld like 'M%';
commit;



insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,1,CFLD,2,100*sequence,1, 200*sequence,300*sequence,1  from bdevices join masks
on bdevices.id_mask_curr = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
where cfld like 'M%';
commit;

---------------- Q

insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,3,CFLD,0,100*sequence,1, 200*sequence,300*sequence,1  from bdevices join masks
on bdevices.id_mask_HOUR  = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
where cfld like 'Q%';
commit;


insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,4,CFLD,0,100*sequence,1, 200*sequence,300*sequence,1  from bdevices join masks
on bdevices.id_mask_24  = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
where cfld like 'Q%';
commit;



insert into chartsettings(id_bd,ptype,pname,chartnum,COLOR,ENABLE,COLORMIN,COLORMAX,VARNUM)
select id_bd,1,CFLD,0,100*sequence,1, 200*sequence,300*sequence,1  from bdevices join masks
on bdevices.id_mask_curr = masks.id_mask
join masksline on masksline.id_mask= masks.id_mask
where cfld like 'Q%';
commit;

