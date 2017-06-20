prompt PL/SQL Developer import file
prompt Created on 29 Ìàðò 2016 ã. by bami
set feedback off
set define off
prompt Disabling triggers for COLORS...
alter table COLORS disable all triggers;
prompt Disabling triggers for DEVCLASSES...
alter table DEVCLASSES disable all triggers;
prompt Disabling triggers for DATATYPE...
alter table DATATYPE disable all triggers;
prompt Disabling triggers for DEVICES...
alter table DEVICES disable all triggers;
prompt Disabling triggers for PARAMTYPE...
alter table PARAMTYPE disable all triggers;
prompt Disabling foreign key constraints for DATATYPE...
alter table DATATYPE disable constraint DATATYPE_CLASS;
prompt Disabling foreign key constraints for DEVICES...
alter table DEVICES disable constraint DEVICE_DEVCLASS;
prompt Disabling foreign key constraints for PARAMTYPE...
alter table PARAMTYPE disable constraint PARAMTYPE_CLASS;
prompt Deleting PARAMTYPE...
delete from PARAMTYPE;
commit;
prompt Deleting DEVICES...
delete from DEVICES;
commit;
prompt Deleting DATATYPE...
delete from DATATYPE;
commit;
prompt Deleting DEVCLASSES...
delete from DEVCLASSES;
commit;
prompt Deleting COLORS...
delete from COLORS;
commit;
prompt Loading COLORS...
insert into COLORS (colorid, color)
values (1, 13808780);
insert into COLORS (colorid, color)
values (2, 16444375);
insert into COLORS (colorid, color)
values (3, 16773083);
insert into COLORS (colorid, color)
values (4, 15654860);
insert into COLORS (colorid, color)
values (5, 13484208);
insert into COLORS (colorid, color)
values (6, 9143160);
insert into COLORS (colorid, color)
values (7, 8388564);
insert into COLORS (colorid, color)
values (8, 7794374);
insert into COLORS (colorid, color)
values (9, 6737322);
insert into COLORS (colorid, color)
values (10, 4557684);
insert into COLORS (colorid, color)
values (11, 15794175);
insert into COLORS (colorid, color)
values (12, 14741230);
insert into COLORS (colorid, color)
values (13, 12701133);
insert into COLORS (colorid, color)
values (14, 8620939);
insert into COLORS (colorid, color)
values (15, 16119260);
insert into COLORS (colorid, color)
values (16, 16770244);
insert into COLORS (colorid, color)
values (17, 15652279);
insert into COLORS (colorid, color)
values (18, 13481886);
insert into COLORS (colorid, color)
values (19, 9141611);
insert into COLORS (colorid, color)
values (20, 0);
insert into COLORS (colorid, color)
values (21, 16772045);
insert into COLORS (colorid, color)
values (22, 255);
insert into COLORS (colorid, color)
values (23, 238);
insert into COLORS (colorid, color)
values (24, 205);
insert into COLORS (colorid, color)
values (25, 139);
insert into COLORS (colorid, color)
values (26, 9055202);
insert into COLORS (colorid, color)
values (27, 10824234);
insert into COLORS (colorid, color)
values (28, 16728128);
insert into COLORS (colorid, color)
values (29, 15612731);
insert into COLORS (colorid, color)
values (30, 13447987);
insert into COLORS (colorid, color)
values (31, 9118499);
insert into COLORS (colorid, color)
values (32, 14596231);
insert into COLORS (colorid, color)
values (33, 16765851);
insert into COLORS (colorid, color)
values (34, 15648145);
insert into COLORS (colorid, color)
values (35, 13478525);
insert into COLORS (colorid, color)
values (36, 9139029);
insert into COLORS (colorid, color)
values (37, 6266528);
insert into COLORS (colorid, color)
values (38, 10024447);
insert into COLORS (colorid, color)
values (39, 9364974);
insert into COLORS (colorid, color)
values (40, 8046029);
insert into COLORS (colorid, color)
values (41, 5473931);
insert into COLORS (colorid, color)
values (42, 8388352);
insert into COLORS (colorid, color)
values (43, 7794176);
insert into COLORS (colorid, color)
values (44, 6737152);
insert into COLORS (colorid, color)
values (45, 4557568);
insert into COLORS (colorid, color)
values (46, 13789470);
insert into COLORS (colorid, color)
values (47, 16744228);
insert into COLORS (colorid, color)
values (48, 15627809);
insert into COLORS (colorid, color)
values (49, 13461021);
insert into COLORS (colorid, color)
values (50, 9127187);
insert into COLORS (colorid, color)
values (51, 16744272);
insert into COLORS (colorid, color)
values (52, 16740950);
insert into COLORS (colorid, color)
values (53, 15624784);
insert into COLORS (colorid, color)
values (54, 13458245);
insert into COLORS (colorid, color)
values (55, 9125423);
insert into COLORS (colorid, color)
values (56, 6591981);
insert into COLORS (colorid, color)
values (57, 16775388);
insert into COLORS (colorid, color)
values (58, 15657165);
insert into COLORS (colorid, color)
values (59, 13486257);
insert into COLORS (colorid, color)
values (60, 9144440);
insert into COLORS (colorid, color)
values (61, 65535);
insert into COLORS (colorid, color)
values (62, 61166);
insert into COLORS (colorid, color)
values (63, 52685);
insert into COLORS (colorid, color)
values (64, 35723);
insert into COLORS (colorid, color)
values (65, 139);
insert into COLORS (colorid, color)
values (66, 35723);
insert into COLORS (colorid, color)
values (67, 12092939);
insert into COLORS (colorid, color)
values (68, 16759055);
insert into COLORS (colorid, color)
values (69, 15641870);
insert into COLORS (colorid, color)
values (70, 13473036);
insert into COLORS (colorid, color)
values (71, 9135368);
insert into COLORS (colorid, color)
values (72, 25600);
insert into COLORS (colorid, color)
values (73, 11119017);
insert into COLORS (colorid, color)
values (74, 12433259);
insert into COLORS (colorid, color)
values (75, 9109643);
insert into COLORS (colorid, color)
values (76, 5597999);
insert into COLORS (colorid, color)
values (77, 13303664);
insert into COLORS (colorid, color)
values (78, 12381800);
insert into COLORS (colorid, color)
values (79, 10669402);
insert into COLORS (colorid, color)
values (80, 7244605);
insert into COLORS (colorid, color)
values (81, 16747520);
insert into COLORS (colorid, color)
values (82, 16744192);
insert into COLORS (colorid, color)
values (83, 15627776);
insert into COLORS (colorid, color)
values (84, 13460992);
insert into COLORS (colorid, color)
values (85, 9127168);
insert into COLORS (colorid, color)
values (86, 10040012);
insert into COLORS (colorid, color)
values (87, 12533503);
insert into COLORS (colorid, color)
values (88, 11680494);
insert into COLORS (colorid, color)
values (89, 10105549);
insert into COLORS (colorid, color)
values (90, 6824587);
insert into COLORS (colorid, color)
values (91, 9109504);
insert into COLORS (colorid, color)
values (92, 15308410);
insert into COLORS (colorid, color)
values (93, 9419919);
insert into COLORS (colorid, color)
values (94, 12713921);
insert into COLORS (colorid, color)
values (95, 11857588);
insert into COLORS (colorid, color)
values (96, 10210715);
insert into COLORS (colorid, color)
values (97, 6916969);
insert into COLORS (colorid, color)
values (98, 4734347);
insert into COLORS (colorid, color)
values (99, 3100495);
insert into COLORS (colorid, color)
values (100, 9961471);
commit;
prompt 100 records committed...
insert into COLORS (colorid, color)
values (101, 9301742);
insert into COLORS (colorid, color)
values (102, 7982541);
insert into COLORS (colorid, color)
values (103, 5409675);
insert into COLORS (colorid, color)
values (104, 52945);
insert into COLORS (colorid, color)
values (105, 9699539);
insert into COLORS (colorid, color)
values (106, 16716947);
insert into COLORS (colorid, color)
values (107, 15602313);
insert into COLORS (colorid, color)
values (108, 13439094);
insert into COLORS (colorid, color)
values (109, 9112144);
insert into COLORS (colorid, color)
values (110, 49151);
insert into COLORS (colorid, color)
values (111, 45806);
insert into COLORS (colorid, color)
values (112, 39629);
insert into COLORS (colorid, color)
values (113, 26763);
insert into COLORS (colorid, color)
values (114, 6908265);
insert into COLORS (colorid, color)
values (115, 2003199);
insert into COLORS (colorid, color)
values (116, 1869550);
insert into COLORS (colorid, color)
values (117, 1602765);
insert into COLORS (colorid, color)
values (118, 1068683);
insert into COLORS (colorid, color)
values (119, 11674146);
insert into COLORS (colorid, color)
values (120, 16724016);
insert into COLORS (colorid, color)
values (121, 15608876);
insert into COLORS (colorid, color)
values (122, 13444646);
insert into COLORS (colorid, color)
values (123, 9116186);
insert into COLORS (colorid, color)
values (124, 16775920);
insert into COLORS (colorid, color)
values (125, 2263842);
insert into COLORS (colorid, color)
values (126, 14474460);
insert into COLORS (colorid, color)
values (127, 16316671);
insert into COLORS (colorid, color)
values (128, 16766720);
insert into COLORS (colorid, color)
values (129, 15649024);
insert into COLORS (colorid, color)
values (130, 13479168);
insert into COLORS (colorid, color)
values (131, 9139456);
insert into COLORS (colorid, color)
values (132, 14329120);
insert into COLORS (colorid, color)
values (133, 16761125);
insert into COLORS (colorid, color)
values (134, 15643682);
insert into COLORS (colorid, color)
values (135, 13474589);
insert into COLORS (colorid, color)
values (136, 9136404);
insert into COLORS (colorid, color)
values (137, 65280);
insert into COLORS (colorid, color)
values (138, 60928);
insert into COLORS (colorid, color)
values (139, 52480);
insert into COLORS (colorid, color)
values (140, 35584);
insert into COLORS (colorid, color)
values (141, 11403055);
insert into COLORS (colorid, color)
values (142, 12500670);
insert into COLORS (colorid, color)
values (143, 1842204);
insert into COLORS (colorid, color)
values (144, 3552822);
insert into COLORS (colorid, color)
values (145, 5197647);
insert into COLORS (colorid, color)
values (146, 6908265);
insert into COLORS (colorid, color)
values (147, 8553090);
insert into COLORS (colorid, color)
values (148, 10263708);
insert into COLORS (colorid, color)
values (149, 11908533);
insert into COLORS (colorid, color)
values (150, 13619151);
insert into COLORS (colorid, color)
values (151, 15263976);
insert into COLORS (colorid, color)
values (152, 15794160);
insert into COLORS (colorid, color)
values (153, 14741216);
insert into COLORS (colorid, color)
values (154, 12701121);
insert into COLORS (colorid, color)
values (155, 8620931);
insert into COLORS (colorid, color)
values (156, 16738740);
insert into COLORS (colorid, color)
values (157, 16740020);
insert into COLORS (colorid, color)
values (158, 15624871);
insert into COLORS (colorid, color)
values (159, 13459600);
insert into COLORS (colorid, color)
values (160, 9124450);
insert into COLORS (colorid, color)
values (161, 13458524);
insert into COLORS (colorid, color)
values (162, 16738922);
insert into COLORS (colorid, color)
values (163, 15623011);
insert into COLORS (colorid, color)
values (164, 13456725);
insert into COLORS (colorid, color)
values (165, 9124410);
insert into COLORS (colorid, color)
values (166, 16777200);
insert into COLORS (colorid, color)
values (167, 15658720);
insert into COLORS (colorid, color)
values (168, 13487553);
insert into COLORS (colorid, color)
values (169, 9145219);
insert into COLORS (colorid, color)
values (170, 16774799);
insert into COLORS (colorid, color)
values (171, 15656581);
insert into COLORS (colorid, color)
values (172, 13485683);
insert into COLORS (colorid, color)
values (173, 9143886);
insert into COLORS (colorid, color)
values (174, 15132410);
insert into COLORS (colorid, color)
values (175, 16773365);
insert into COLORS (colorid, color)
values (176, 15655141);
insert into COLORS (colorid, color)
values (177, 13484485);
insert into COLORS (colorid, color)
values (178, 9143174);
insert into COLORS (colorid, color)
values (179, 8190976);
insert into COLORS (colorid, color)
values (180, 16775885);
insert into COLORS (colorid, color)
values (181, 15657407);
insert into COLORS (colorid, color)
values (182, 13486501);
insert into COLORS (colorid, color)
values (183, 9144688);
insert into COLORS (colorid, color)
values (184, 11393254);
insert into COLORS (colorid, color)
values (185, 12578815);
insert into COLORS (colorid, color)
values (186, 11722734);
insert into COLORS (colorid, color)
values (187, 10141901);
insert into COLORS (colorid, color)
values (188, 6849419);
insert into COLORS (colorid, color)
values (189, 15761536);
insert into COLORS (colorid, color)
values (190, 14745599);
insert into COLORS (colorid, color)
values (191, 13758190);
insert into COLORS (colorid, color)
values (192, 11849165);
insert into COLORS (colorid, color)
values (193, 8031115);
insert into COLORS (colorid, color)
values (194, 15654274);
insert into COLORS (colorid, color)
values (195, 16772235);
insert into COLORS (colorid, color)
values (196, 15654018);
insert into COLORS (colorid, color)
values (197, 13483632);
insert into COLORS (colorid, color)
values (198, 9142604);
insert into COLORS (colorid, color)
values (199, 16444375);
insert into COLORS (colorid, color)
values (200, 16773083);
commit;
prompt 200 records committed...
insert into COLORS (colorid, color)
values (201, 15654860);
insert into COLORS (colorid, color)
values (202, 13484208);
insert into COLORS (colorid, color)
values (203, 9143160);
insert into COLORS (colorid, color)
values (204, 8388564);
insert into COLORS (colorid, color)
values (205, 7794374);
insert into COLORS (colorid, color)
values (206, 6737322);
insert into COLORS (colorid, color)
values (207, 4557684);
insert into COLORS (colorid, color)
values (208, 15794175);
insert into COLORS (colorid, color)
values (209, 14741230);
insert into COLORS (colorid, color)
values (210, 12701133);
insert into COLORS (colorid, color)
values (211, 8620939);
insert into COLORS (colorid, color)
values (212, 16119260);
insert into COLORS (colorid, color)
values (213, 16770244);
insert into COLORS (colorid, color)
values (214, 15652279);
insert into COLORS (colorid, color)
values (215, 13481886);
insert into COLORS (colorid, color)
values (216, 9141611);
insert into COLORS (colorid, color)
values (217, 0);
insert into COLORS (colorid, color)
values (218, 16772045);
insert into COLORS (colorid, color)
values (219, 255);
insert into COLORS (colorid, color)
values (220, 238);
insert into COLORS (colorid, color)
values (221, 205);
insert into COLORS (colorid, color)
values (222, 139);
insert into COLORS (colorid, color)
values (223, 9055202);
insert into COLORS (colorid, color)
values (224, 10824234);
insert into COLORS (colorid, color)
values (225, 16728128);
insert into COLORS (colorid, color)
values (226, 15612731);
insert into COLORS (colorid, color)
values (227, 13447987);
insert into COLORS (colorid, color)
values (228, 9118499);
insert into COLORS (colorid, color)
values (229, 14596231);
insert into COLORS (colorid, color)
values (230, 16765851);
insert into COLORS (colorid, color)
values (231, 15648145);
insert into COLORS (colorid, color)
values (232, 13478525);
insert into COLORS (colorid, color)
values (233, 9139029);
insert into COLORS (colorid, color)
values (234, 6266528);
insert into COLORS (colorid, color)
values (235, 10024447);
insert into COLORS (colorid, color)
values (236, 9364974);
insert into COLORS (colorid, color)
values (237, 8046029);
insert into COLORS (colorid, color)
values (238, 5473931);
insert into COLORS (colorid, color)
values (239, 8388352);
insert into COLORS (colorid, color)
values (240, 7794176);
insert into COLORS (colorid, color)
values (241, 6737152);
insert into COLORS (colorid, color)
values (242, 4557568);
insert into COLORS (colorid, color)
values (243, 13789470);
insert into COLORS (colorid, color)
values (244, 16744228);
insert into COLORS (colorid, color)
values (245, 15627809);
insert into COLORS (colorid, color)
values (246, 13461021);
insert into COLORS (colorid, color)
values (247, 9127187);
insert into COLORS (colorid, color)
values (248, 16744272);
insert into COLORS (colorid, color)
values (249, 16740950);
insert into COLORS (colorid, color)
values (250, 15624784);
insert into COLORS (colorid, color)
values (251, 13458245);
insert into COLORS (colorid, color)
values (252, 9125423);
insert into COLORS (colorid, color)
values (253, 6591981);
insert into COLORS (colorid, color)
values (254, 16775388);
insert into COLORS (colorid, color)
values (255, 15657165);
insert into COLORS (colorid, color)
values (256, 13486257);
insert into COLORS (colorid, color)
values (257, 9144440);
insert into COLORS (colorid, color)
values (258, 65535);
insert into COLORS (colorid, color)
values (259, 61166);
insert into COLORS (colorid, color)
values (260, 52685);
insert into COLORS (colorid, color)
values (261, 35723);
insert into COLORS (colorid, color)
values (262, 139);
insert into COLORS (colorid, color)
values (263, 35723);
insert into COLORS (colorid, color)
values (264, 12092939);
insert into COLORS (colorid, color)
values (265, 16759055);
insert into COLORS (colorid, color)
values (266, 15641870);
insert into COLORS (colorid, color)
values (267, 13473036);
insert into COLORS (colorid, color)
values (268, 9135368);
insert into COLORS (colorid, color)
values (269, 25600);
insert into COLORS (colorid, color)
values (270, 11119017);
insert into COLORS (colorid, color)
values (271, 12433259);
insert into COLORS (colorid, color)
values (272, 9109643);
insert into COLORS (colorid, color)
values (273, 5597999);
insert into COLORS (colorid, color)
values (274, 13303664);
insert into COLORS (colorid, color)
values (275, 12381800);
insert into COLORS (colorid, color)
values (276, 10669402);
insert into COLORS (colorid, color)
values (277, 7244605);
insert into COLORS (colorid, color)
values (278, 16747520);
insert into COLORS (colorid, color)
values (279, 16744192);
insert into COLORS (colorid, color)
values (280, 15627776);
insert into COLORS (colorid, color)
values (281, 13460992);
insert into COLORS (colorid, color)
values (282, 9127168);
insert into COLORS (colorid, color)
values (283, 10040012);
insert into COLORS (colorid, color)
values (284, 12533503);
insert into COLORS (colorid, color)
values (285, 11680494);
insert into COLORS (colorid, color)
values (286, 10105549);
insert into COLORS (colorid, color)
values (287, 6824587);
insert into COLORS (colorid, color)
values (288, 9109504);
insert into COLORS (colorid, color)
values (289, 15308410);
insert into COLORS (colorid, color)
values (290, 9419919);
insert into COLORS (colorid, color)
values (291, 12713921);
insert into COLORS (colorid, color)
values (292, 11857588);
insert into COLORS (colorid, color)
values (293, 10210715);
insert into COLORS (colorid, color)
values (294, 6916969);
insert into COLORS (colorid, color)
values (295, 4734347);
insert into COLORS (colorid, color)
values (296, 3100495);
insert into COLORS (colorid, color)
values (297, 9961471);
insert into COLORS (colorid, color)
values (298, 9301742);
insert into COLORS (colorid, color)
values (299, 7982541);
insert into COLORS (colorid, color)
values (300, 5409675);
commit;
prompt 300 records committed...
insert into COLORS (colorid, color)
values (301, 52945);
insert into COLORS (colorid, color)
values (302, 9699539);
insert into COLORS (colorid, color)
values (303, 16716947);
insert into COLORS (colorid, color)
values (304, 15602313);
insert into COLORS (colorid, color)
values (305, 13439094);
insert into COLORS (colorid, color)
values (306, 9112144);
insert into COLORS (colorid, color)
values (307, 49151);
insert into COLORS (colorid, color)
values (308, 45806);
insert into COLORS (colorid, color)
values (309, 39629);
insert into COLORS (colorid, color)
values (310, 26763);
insert into COLORS (colorid, color)
values (311, 6908265);
insert into COLORS (colorid, color)
values (312, 2003199);
insert into COLORS (colorid, color)
values (313, 1869550);
insert into COLORS (colorid, color)
values (314, 1602765);
insert into COLORS (colorid, color)
values (315, 1068683);
insert into COLORS (colorid, color)
values (316, 11674146);
insert into COLORS (colorid, color)
values (317, 16724016);
insert into COLORS (colorid, color)
values (318, 15608876);
insert into COLORS (colorid, color)
values (319, 13444646);
insert into COLORS (colorid, color)
values (320, 9116186);
insert into COLORS (colorid, color)
values (321, 16775920);
insert into COLORS (colorid, color)
values (322, 2263842);
insert into COLORS (colorid, color)
values (323, 14474460);
insert into COLORS (colorid, color)
values (324, 16316671);
insert into COLORS (colorid, color)
values (325, 16766720);
insert into COLORS (colorid, color)
values (326, 15649024);
insert into COLORS (colorid, color)
values (327, 13479168);
insert into COLORS (colorid, color)
values (328, 9139456);
insert into COLORS (colorid, color)
values (329, 14329120);
insert into COLORS (colorid, color)
values (330, 16761125);
insert into COLORS (colorid, color)
values (331, 15643682);
insert into COLORS (colorid, color)
values (332, 13474589);
insert into COLORS (colorid, color)
values (333, 9136404);
insert into COLORS (colorid, color)
values (334, 65280);
insert into COLORS (colorid, color)
values (335, 60928);
insert into COLORS (colorid, color)
values (336, 52480);
insert into COLORS (colorid, color)
values (337, 35584);
insert into COLORS (colorid, color)
values (338, 11403055);
insert into COLORS (colorid, color)
values (339, 12500670);
insert into COLORS (colorid, color)
values (340, 1842204);
insert into COLORS (colorid, color)
values (341, 3552822);
insert into COLORS (colorid, color)
values (342, 5197647);
insert into COLORS (colorid, color)
values (343, 6908265);
insert into COLORS (colorid, color)
values (344, 8553090);
insert into COLORS (colorid, color)
values (345, 10263708);
insert into COLORS (colorid, color)
values (346, 11908533);
insert into COLORS (colorid, color)
values (347, 13619151);
insert into COLORS (colorid, color)
values (348, 15263976);
insert into COLORS (colorid, color)
values (349, 15794160);
insert into COLORS (colorid, color)
values (350, 14741216);
insert into COLORS (colorid, color)
values (351, 12701121);
insert into COLORS (colorid, color)
values (352, 8620931);
insert into COLORS (colorid, color)
values (353, 16738740);
insert into COLORS (colorid, color)
values (354, 16740020);
insert into COLORS (colorid, color)
values (355, 15624871);
insert into COLORS (colorid, color)
values (356, 13459600);
insert into COLORS (colorid, color)
values (357, 9124450);
insert into COLORS (colorid, color)
values (358, 13458524);
insert into COLORS (colorid, color)
values (359, 16738922);
insert into COLORS (colorid, color)
values (360, 15623011);
insert into COLORS (colorid, color)
values (361, 13456725);
insert into COLORS (colorid, color)
values (362, 9124410);
insert into COLORS (colorid, color)
values (363, 16777200);
insert into COLORS (colorid, color)
values (364, 15658720);
insert into COLORS (colorid, color)
values (365, 13487553);
insert into COLORS (colorid, color)
values (366, 9145219);
insert into COLORS (colorid, color)
values (367, 16774799);
insert into COLORS (colorid, color)
values (368, 15656581);
insert into COLORS (colorid, color)
values (369, 13485683);
insert into COLORS (colorid, color)
values (370, 9143886);
insert into COLORS (colorid, color)
values (371, 15132410);
insert into COLORS (colorid, color)
values (372, 16773365);
insert into COLORS (colorid, color)
values (373, 15655141);
insert into COLORS (colorid, color)
values (374, 13484485);
insert into COLORS (colorid, color)
values (375, 9143174);
insert into COLORS (colorid, color)
values (376, 8190976);
insert into COLORS (colorid, color)
values (377, 16775885);
insert into COLORS (colorid, color)
values (378, 15657407);
insert into COLORS (colorid, color)
values (379, 13486501);
insert into COLORS (colorid, color)
values (380, 9144688);
insert into COLORS (colorid, color)
values (381, 11393254);
insert into COLORS (colorid, color)
values (382, 12578815);
insert into COLORS (colorid, color)
values (383, 11722734);
insert into COLORS (colorid, color)
values (384, 10141901);
insert into COLORS (colorid, color)
values (385, 6849419);
insert into COLORS (colorid, color)
values (386, 15761536);
insert into COLORS (colorid, color)
values (387, 14745599);
insert into COLORS (colorid, color)
values (388, 13758190);
insert into COLORS (colorid, color)
values (389, 11849165);
insert into COLORS (colorid, color)
values (390, 8031115);
insert into COLORS (colorid, color)
values (391, 15654274);
insert into COLORS (colorid, color)
values (392, 16772235);
insert into COLORS (colorid, color)
values (393, 15654018);
insert into COLORS (colorid, color)
values (394, 13483632);
insert into COLORS (colorid, color)
values (395, 9142604);
insert into COLORS (colorid, color)
values (396, 16448210);
insert into COLORS (colorid, color)
values (397, 9498256);
insert into COLORS (colorid, color)
values (398, 13882323);
insert into COLORS (colorid, color)
values (399, 16758465);
insert into COLORS (colorid, color)
values (400, 16756409);
commit;
prompt 400 records committed...
insert into COLORS (colorid, color)
values (401, 15639213);
insert into COLORS (colorid, color)
values (402, 13470869);
insert into COLORS (colorid, color)
values (403, 9133925);
insert into COLORS (colorid, color)
values (404, 16752762);
insert into COLORS (colorid, color)
values (405, 15635826);
insert into COLORS (colorid, color)
values (406, 13468002);
insert into COLORS (colorid, color)
values (407, 9131842);
insert into COLORS (colorid, color)
values (408, 2142890);
insert into COLORS (colorid, color)
values (409, 8900346);
insert into COLORS (colorid, color)
values (410, 11592447);
insert into COLORS (colorid, color)
values (411, 10802158);
insert into COLORS (colorid, color)
values (412, 9287373);
insert into COLORS (colorid, color)
values (413, 6323083);
insert into COLORS (colorid, color)
values (414, 8679679);
insert into COLORS (colorid, color)
values (415, 7833753);
insert into COLORS (colorid, color)
values (416, 11584734);
insert into COLORS (colorid, color)
values (417, 13296127);
insert into COLORS (colorid, color)
values (418, 12374766);
insert into COLORS (colorid, color)
values (419, 10663373);
insert into COLORS (colorid, color)
values (420, 7240587);
insert into COLORS (colorid, color)
values (421, 16777184);
insert into COLORS (colorid, color)
values (422, 15658705);
insert into COLORS (colorid, color)
values (423, 13487540);
insert into COLORS (colorid, color)
values (424, 9145210);
insert into COLORS (colorid, color)
values (425, 3329330);
insert into COLORS (colorid, color)
values (426, 16445670);
insert into COLORS (colorid, color)
values (427, 16711935);
insert into COLORS (colorid, color)
values (428, 15597806);
insert into COLORS (colorid, color)
values (429, 13435085);
insert into COLORS (colorid, color)
values (430, 9109643);
insert into COLORS (colorid, color)
values (431, 11546720);
insert into COLORS (colorid, color)
values (432, 16725171);
insert into COLORS (colorid, color)
values (433, 15610023);
insert into COLORS (colorid, color)
values (434, 13445520);
insert into COLORS (colorid, color)
values (435, 9116770);
insert into COLORS (colorid, color)
values (436, 6737322);
insert into COLORS (colorid, color)
values (437, 205);
insert into COLORS (colorid, color)
values (438, 12211667);
insert into COLORS (colorid, color)
values (439, 14706431);
insert into COLORS (colorid, color)
values (440, 13721582);
insert into COLORS (colorid, color)
values (441, 11817677);
insert into COLORS (colorid, color)
values (442, 8009611);
insert into COLORS (colorid, color)
values (443, 9662683);
insert into COLORS (colorid, color)
values (444, 11240191);
insert into COLORS (colorid, color)
values (445, 10451438);
insert into COLORS (colorid, color)
values (446, 9005261);
insert into COLORS (colorid, color)
values (447, 6113163);
insert into COLORS (colorid, color)
values (448, 3978097);
insert into COLORS (colorid, color)
values (449, 8087790);
insert into COLORS (colorid, color)
values (450, 4772300);
insert into COLORS (colorid, color)
values (451, 13047173);
insert into COLORS (colorid, color)
values (452, 64154);
insert into COLORS (colorid, color)
values (453, 1644912);
insert into COLORS (colorid, color)
values (454, 16121850);
insert into COLORS (colorid, color)
values (455, 16770273);
insert into COLORS (colorid, color)
values (456, 15652306);
insert into COLORS (colorid, color)
values (457, 13481909);
insert into COLORS (colorid, color)
values (458, 9141627);
insert into COLORS (colorid, color)
values (459, 16770229);
insert into COLORS (colorid, color)
values (460, 16768685);
insert into COLORS (colorid, color)
values (461, 15650721);
insert into COLORS (colorid, color)
values (462, 13480843);
insert into COLORS (colorid, color)
values (463, 9140574);
insert into COLORS (colorid, color)
values (464, 128);
insert into COLORS (colorid, color)
values (465, 16643558);
insert into COLORS (colorid, color)
values (466, 7048739);
insert into COLORS (colorid, color)
values (467, 12648254);
insert into COLORS (colorid, color)
values (468, 11791930);
insert into COLORS (colorid, color)
values (469, 10145074);
insert into COLORS (colorid, color)
values (470, 6916898);
insert into COLORS (colorid, color)
values (471, 16753920);
insert into COLORS (colorid, color)
values (472, 15636992);
insert into COLORS (colorid, color)
values (473, 13468928);
insert into COLORS (colorid, color)
values (474, 9132544);
insert into COLORS (colorid, color)
values (475, 16729344);
insert into COLORS (colorid, color)
values (476, 15613952);
insert into COLORS (colorid, color)
values (477, 13448960);
insert into COLORS (colorid, color)
values (478, 9118976);
insert into COLORS (colorid, color)
values (479, 14315734);
insert into COLORS (colorid, color)
values (480, 16745466);
insert into COLORS (colorid, color)
values (481, 15629033);
insert into COLORS (colorid, color)
values (482, 13461961);
insert into COLORS (colorid, color)
values (483, 9127817);
insert into COLORS (colorid, color)
values (484, 15657130);
insert into COLORS (colorid, color)
values (485, 10025880);
insert into COLORS (colorid, color)
values (486, 10157978);
insert into COLORS (colorid, color)
values (487, 9498256);
insert into COLORS (colorid, color)
values (488, 8179068);
insert into COLORS (colorid, color)
values (489, 5540692);
insert into COLORS (colorid, color)
values (490, 11529966);
insert into COLORS (colorid, color)
values (491, 12320767);
insert into COLORS (colorid, color)
values (492, 9883085);
insert into COLORS (colorid, color)
values (493, 6720395);
insert into COLORS (colorid, color)
values (494, 14381203);
insert into COLORS (colorid, color)
values (495, 16745131);
insert into COLORS (colorid, color)
values (496, 15628703);
insert into COLORS (colorid, color)
values (497, 13461641);
insert into COLORS (colorid, color)
values (498, 9127773);
insert into COLORS (colorid, color)
values (499, 16773077);
insert into COLORS (colorid, color)
values (500, 16767673);
commit;
prompt 500 records committed...
insert into COLORS (colorid, color)
values (501, 15649709);
insert into COLORS (colorid, color)
values (502, 13479829);
insert into COLORS (colorid, color)
values (503, 9140069);
insert into COLORS (colorid, color)
values (504, 13468991);
insert into COLORS (colorid, color)
values (505, 16761035);
insert into COLORS (colorid, color)
values (506, 16758213);
insert into COLORS (colorid, color)
values (507, 15641016);
insert into COLORS (colorid, color)
values (508, 13472158);
insert into COLORS (colorid, color)
values (509, 9134956);
insert into COLORS (colorid, color)
values (510, 14524637);
insert into COLORS (colorid, color)
values (511, 16759807);
insert into COLORS (colorid, color)
values (512, 15642350);
insert into COLORS (colorid, color)
values (513, 13473485);
insert into COLORS (colorid, color)
values (514, 9135755);
insert into COLORS (colorid, color)
values (515, 11591910);
insert into COLORS (colorid, color)
values (516, 10494192);
insert into COLORS (colorid, color)
values (517, 10170623);
insert into COLORS (colorid, color)
values (518, 9514222);
insert into COLORS (colorid, color)
values (519, 8201933);
insert into COLORS (colorid, color)
values (520, 5577355);
insert into COLORS (colorid, color)
values (521, 16711680);
insert into COLORS (colorid, color)
values (522, 15597568);
insert into COLORS (colorid, color)
values (523, 13434880);
insert into COLORS (colorid, color)
values (524, 9109504);
insert into COLORS (colorid, color)
values (525, 12357519);
insert into COLORS (colorid, color)
values (526, 16761281);
insert into COLORS (colorid, color)
values (527, 15643828);
insert into COLORS (colorid, color)
values (528, 13474715);
insert into COLORS (colorid, color)
values (529, 9136489);
insert into COLORS (colorid, color)
values (530, 4286945);
insert into COLORS (colorid, color)
values (531, 4749055);
insert into COLORS (colorid, color)
values (532, 4419310);
insert into COLORS (colorid, color)
values (533, 3825613);
insert into COLORS (colorid, color)
values (534, 2572427);
insert into COLORS (colorid, color)
values (535, 9127187);
insert into COLORS (colorid, color)
values (536, 16416882);
insert into COLORS (colorid, color)
values (537, 16747625);
insert into COLORS (colorid, color)
values (538, 15630946);
insert into COLORS (colorid, color)
values (539, 13463636);
insert into COLORS (colorid, color)
values (540, 9129017);
insert into COLORS (colorid, color)
values (541, 16032864);
insert into COLORS (colorid, color)
values (542, 3050327);
insert into COLORS (colorid, color)
values (543, 5570463);
insert into COLORS (colorid, color)
values (544, 5172884);
insert into COLORS (colorid, color)
values (545, 4443520);
insert into COLORS (colorid, color)
values (546, 3050327);
insert into COLORS (colorid, color)
values (547, 16774638);
insert into COLORS (colorid, color)
values (548, 15656414);
insert into COLORS (colorid, color)
values (549, 13485503);
insert into COLORS (colorid, color)
values (550, 9143938);
insert into COLORS (colorid, color)
values (551, 10506797);
insert into COLORS (colorid, color)
values (552, 16745031);
insert into COLORS (colorid, color)
values (553, 15628610);
insert into COLORS (colorid, color)
values (554, 13461561);
insert into COLORS (colorid, color)
values (555, 9127718);
insert into COLORS (colorid, color)
values (556, 8900331);
insert into COLORS (colorid, color)
values (557, 8900351);
insert into COLORS (colorid, color)
values (558, 8306926);
insert into COLORS (colorid, color)
values (559, 7120589);
insert into COLORS (colorid, color)
values (560, 4878475);
insert into COLORS (colorid, color)
values (561, 6970061);
insert into COLORS (colorid, color)
values (562, 8613887);
insert into COLORS (colorid, color)
values (563, 8021998);
insert into COLORS (colorid, color)
values (564, 6904269);
insert into COLORS (colorid, color)
values (565, 4668555);
insert into COLORS (colorid, color)
values (566, 7372944);
insert into COLORS (colorid, color)
values (567, 13034239);
insert into COLORS (colorid, color)
values (568, 12178414);
insert into COLORS (colorid, color)
values (569, 10467021);
insert into COLORS (colorid, color)
values (570, 7109515);
insert into COLORS (colorid, color)
values (571, 16775930);
insert into COLORS (colorid, color)
values (572, 15657449);
insert into COLORS (colorid, color)
values (573, 13486537);
insert into COLORS (colorid, color)
values (574, 9144713);
insert into COLORS (colorid, color)
values (575, 65407);
insert into COLORS (colorid, color)
values (576, 61046);
insert into COLORS (colorid, color)
values (577, 52582);
insert into COLORS (colorid, color)
values (578, 35653);
insert into COLORS (colorid, color)
values (579, 4620980);
insert into COLORS (colorid, color)
values (580, 6535423);
insert into COLORS (colorid, color)
values (581, 6073582);
insert into COLORS (colorid, color)
values (582, 5215437);
insert into COLORS (colorid, color)
values (583, 3564683);
insert into COLORS (colorid, color)
values (584, 13808780);
insert into COLORS (colorid, color)
values (585, 16753999);
insert into COLORS (colorid, color)
values (586, 15637065);
insert into COLORS (colorid, color)
values (587, 13468991);
insert into COLORS (colorid, color)
values (588, 9132587);
insert into COLORS (colorid, color)
values (589, 14204888);
insert into COLORS (colorid, color)
values (590, 16769535);
insert into COLORS (colorid, color)
values (591, 15651566);
insert into COLORS (colorid, color)
values (592, 13481421);
insert into COLORS (colorid, color)
values (593, 9141131);
insert into COLORS (colorid, color)
values (594, 16737095);
insert into COLORS (colorid, color)
values (595, 15621186);
insert into COLORS (colorid, color)
values (596, 13455161);
insert into COLORS (colorid, color)
values (597, 9123366);
insert into COLORS (colorid, color)
values (598, 4251856);
insert into COLORS (colorid, color)
values (599, 62975);
insert into COLORS (colorid, color)
values (600, 58862);
commit;
prompt 600 records committed...
insert into COLORS (colorid, color)
values (601, 50637);
insert into COLORS (colorid, color)
values (602, 34443);
insert into COLORS (colorid, color)
values (603, 15631086);
insert into COLORS (colorid, color)
values (604, 13639824);
insert into COLORS (colorid, color)
values (605, 16727702);
insert into COLORS (colorid, color)
values (606, 15612556);
insert into COLORS (colorid, color)
values (607, 13447800);
insert into COLORS (colorid, color)
values (608, 9118290);
insert into COLORS (colorid, color)
values (609, 16113331);
insert into COLORS (colorid, color)
values (610, 16771002);
insert into COLORS (colorid, color)
values (611, 15653038);
insert into COLORS (colorid, color)
values (612, 13482646);
insert into COLORS (colorid, color)
values (613, 9141862);
insert into COLORS (colorid, color)
values (614, 16777215);
insert into COLORS (colorid, color)
values (615, 16119285);
insert into COLORS (colorid, color)
values (616, 16776960);
insert into COLORS (colorid, color)
values (617, 15658496);
insert into COLORS (colorid, color)
values (618, 13487360);
insert into COLORS (colorid, color)
values (619, 9145088);
insert into COLORS (colorid, color)
values (620, 10145074);
insert into COLORS (colorid, color)
values (621, 16444375);
insert into COLORS (colorid, color)
values (622, 16773083);
insert into COLORS (colorid, color)
values (623, 15654860);
insert into COLORS (colorid, color)
values (624, 13484208);
insert into COLORS (colorid, color)
values (625, 9143160);
insert into COLORS (colorid, color)
values (626, 8388564);
insert into COLORS (colorid, color)
values (627, 7794374);
insert into COLORS (colorid, color)
values (628, 6737322);
insert into COLORS (colorid, color)
values (629, 4557684);
insert into COLORS (colorid, color)
values (630, 15794175);
insert into COLORS (colorid, color)
values (631, 14741230);
insert into COLORS (colorid, color)
values (632, 12701133);
insert into COLORS (colorid, color)
values (633, 8620939);
insert into COLORS (colorid, color)
values (634, 16119260);
insert into COLORS (colorid, color)
values (635, 16770244);
insert into COLORS (colorid, color)
values (636, 15652279);
insert into COLORS (colorid, color)
values (637, 13481886);
insert into COLORS (colorid, color)
values (638, 9141611);
insert into COLORS (colorid, color)
values (639, 0);
insert into COLORS (colorid, color)
values (640, 16772045);
insert into COLORS (colorid, color)
values (641, 255);
insert into COLORS (colorid, color)
values (642, 238);
insert into COLORS (colorid, color)
values (643, 205);
insert into COLORS (colorid, color)
values (644, 139);
insert into COLORS (colorid, color)
values (645, 9055202);
insert into COLORS (colorid, color)
values (646, 10824234);
insert into COLORS (colorid, color)
values (647, 16728128);
insert into COLORS (colorid, color)
values (648, 15612731);
insert into COLORS (colorid, color)
values (649, 13447987);
insert into COLORS (colorid, color)
values (650, 9118499);
insert into COLORS (colorid, color)
values (651, 14596231);
insert into COLORS (colorid, color)
values (652, 16765851);
insert into COLORS (colorid, color)
values (653, 15648145);
insert into COLORS (colorid, color)
values (654, 13478525);
insert into COLORS (colorid, color)
values (655, 9139029);
insert into COLORS (colorid, color)
values (656, 6266528);
insert into COLORS (colorid, color)
values (657, 10024447);
insert into COLORS (colorid, color)
values (658, 9364974);
insert into COLORS (colorid, color)
values (659, 8046029);
insert into COLORS (colorid, color)
values (660, 5473931);
insert into COLORS (colorid, color)
values (661, 8388352);
insert into COLORS (colorid, color)
values (662, 7794176);
insert into COLORS (colorid, color)
values (663, 6737152);
insert into COLORS (colorid, color)
values (664, 4557568);
insert into COLORS (colorid, color)
values (665, 13789470);
insert into COLORS (colorid, color)
values (666, 16744228);
insert into COLORS (colorid, color)
values (667, 15627809);
insert into COLORS (colorid, color)
values (668, 13461021);
insert into COLORS (colorid, color)
values (669, 9127187);
insert into COLORS (colorid, color)
values (670, 16744272);
insert into COLORS (colorid, color)
values (671, 16740950);
insert into COLORS (colorid, color)
values (672, 15624784);
insert into COLORS (colorid, color)
values (673, 13458245);
insert into COLORS (colorid, color)
values (674, 9125423);
insert into COLORS (colorid, color)
values (675, 6591981);
insert into COLORS (colorid, color)
values (676, 16775388);
insert into COLORS (colorid, color)
values (677, 15657165);
insert into COLORS (colorid, color)
values (678, 13486257);
insert into COLORS (colorid, color)
values (679, 9144440);
insert into COLORS (colorid, color)
values (680, 65535);
insert into COLORS (colorid, color)
values (681, 61166);
insert into COLORS (colorid, color)
values (682, 52685);
insert into COLORS (colorid, color)
values (683, 35723);
insert into COLORS (colorid, color)
values (684, 139);
insert into COLORS (colorid, color)
values (685, 35723);
insert into COLORS (colorid, color)
values (686, 12092939);
insert into COLORS (colorid, color)
values (687, 16759055);
insert into COLORS (colorid, color)
values (688, 15641870);
insert into COLORS (colorid, color)
values (689, 13473036);
insert into COLORS (colorid, color)
values (690, 9135368);
insert into COLORS (colorid, color)
values (691, 25600);
insert into COLORS (colorid, color)
values (692, 11119017);
insert into COLORS (colorid, color)
values (693, 12433259);
insert into COLORS (colorid, color)
values (694, 9109643);
insert into COLORS (colorid, color)
values (695, 5597999);
insert into COLORS (colorid, color)
values (696, 13303664);
insert into COLORS (colorid, color)
values (697, 12381800);
insert into COLORS (colorid, color)
values (698, 10669402);
insert into COLORS (colorid, color)
values (699, 7244605);
insert into COLORS (colorid, color)
values (700, 16747520);
commit;
prompt 700 records committed...
insert into COLORS (colorid, color)
values (701, 16744192);
insert into COLORS (colorid, color)
values (702, 15627776);
insert into COLORS (colorid, color)
values (703, 13460992);
insert into COLORS (colorid, color)
values (704, 9127168);
insert into COLORS (colorid, color)
values (705, 10040012);
insert into COLORS (colorid, color)
values (706, 12533503);
insert into COLORS (colorid, color)
values (707, 11680494);
insert into COLORS (colorid, color)
values (708, 10105549);
insert into COLORS (colorid, color)
values (709, 6824587);
insert into COLORS (colorid, color)
values (710, 9109504);
insert into COLORS (colorid, color)
values (711, 15308410);
insert into COLORS (colorid, color)
values (712, 9419919);
insert into COLORS (colorid, color)
values (713, 12713921);
insert into COLORS (colorid, color)
values (714, 11857588);
insert into COLORS (colorid, color)
values (715, 10210715);
insert into COLORS (colorid, color)
values (716, 6916969);
insert into COLORS (colorid, color)
values (717, 4734347);
insert into COLORS (colorid, color)
values (718, 3100495);
insert into COLORS (colorid, color)
values (719, 9961471);
insert into COLORS (colorid, color)
values (720, 9301742);
insert into COLORS (colorid, color)
values (721, 7982541);
insert into COLORS (colorid, color)
values (722, 5409675);
insert into COLORS (colorid, color)
values (723, 52945);
insert into COLORS (colorid, color)
values (724, 9699539);
insert into COLORS (colorid, color)
values (725, 16716947);
insert into COLORS (colorid, color)
values (726, 15602313);
insert into COLORS (colorid, color)
values (727, 13439094);
insert into COLORS (colorid, color)
values (728, 9112144);
insert into COLORS (colorid, color)
values (729, 49151);
insert into COLORS (colorid, color)
values (730, 45806);
insert into COLORS (colorid, color)
values (731, 39629);
insert into COLORS (colorid, color)
values (732, 26763);
insert into COLORS (colorid, color)
values (733, 6908265);
insert into COLORS (colorid, color)
values (734, 2003199);
insert into COLORS (colorid, color)
values (735, 1869550);
insert into COLORS (colorid, color)
values (736, 1602765);
insert into COLORS (colorid, color)
values (737, 1068683);
insert into COLORS (colorid, color)
values (738, 11674146);
insert into COLORS (colorid, color)
values (739, 16724016);
insert into COLORS (colorid, color)
values (740, 15608876);
insert into COLORS (colorid, color)
values (741, 13444646);
insert into COLORS (colorid, color)
values (742, 9116186);
insert into COLORS (colorid, color)
values (743, 16775920);
insert into COLORS (colorid, color)
values (744, 2263842);
insert into COLORS (colorid, color)
values (745, 14474460);
insert into COLORS (colorid, color)
values (746, 16316671);
insert into COLORS (colorid, color)
values (747, 16766720);
insert into COLORS (colorid, color)
values (748, 15649024);
insert into COLORS (colorid, color)
values (749, 13479168);
insert into COLORS (colorid, color)
values (750, 9139456);
insert into COLORS (colorid, color)
values (751, 14329120);
insert into COLORS (colorid, color)
values (752, 16761125);
insert into COLORS (colorid, color)
values (753, 15643682);
insert into COLORS (colorid, color)
values (754, 13474589);
insert into COLORS (colorid, color)
values (755, 9136404);
insert into COLORS (colorid, color)
values (756, 65280);
insert into COLORS (colorid, color)
values (757, 60928);
insert into COLORS (colorid, color)
values (758, 52480);
insert into COLORS (colorid, color)
values (759, 35584);
insert into COLORS (colorid, color)
values (760, 11403055);
insert into COLORS (colorid, color)
values (761, 12500670);
insert into COLORS (colorid, color)
values (762, 1842204);
insert into COLORS (colorid, color)
values (763, 3552822);
insert into COLORS (colorid, color)
values (764, 5197647);
insert into COLORS (colorid, color)
values (765, 6908265);
insert into COLORS (colorid, color)
values (766, 8553090);
insert into COLORS (colorid, color)
values (767, 10263708);
insert into COLORS (colorid, color)
values (768, 11908533);
insert into COLORS (colorid, color)
values (769, 13619151);
insert into COLORS (colorid, color)
values (770, 15263976);
insert into COLORS (colorid, color)
values (771, 15794160);
insert into COLORS (colorid, color)
values (772, 14741216);
insert into COLORS (colorid, color)
values (773, 12701121);
insert into COLORS (colorid, color)
values (774, 8620931);
insert into COLORS (colorid, color)
values (775, 16738740);
insert into COLORS (colorid, color)
values (776, 16740020);
insert into COLORS (colorid, color)
values (777, 15624871);
insert into COLORS (colorid, color)
values (778, 13459600);
insert into COLORS (colorid, color)
values (779, 9124450);
insert into COLORS (colorid, color)
values (780, 13458524);
insert into COLORS (colorid, color)
values (781, 16738922);
insert into COLORS (colorid, color)
values (782, 15623011);
insert into COLORS (colorid, color)
values (783, 13456725);
insert into COLORS (colorid, color)
values (784, 9124410);
insert into COLORS (colorid, color)
values (785, 16777200);
insert into COLORS (colorid, color)
values (786, 15658720);
insert into COLORS (colorid, color)
values (787, 13487553);
insert into COLORS (colorid, color)
values (788, 9145219);
insert into COLORS (colorid, color)
values (789, 16774799);
insert into COLORS (colorid, color)
values (790, 15656581);
insert into COLORS (colorid, color)
values (791, 13485683);
insert into COLORS (colorid, color)
values (792, 9143886);
insert into COLORS (colorid, color)
values (793, 15132410);
insert into COLORS (colorid, color)
values (794, 16773365);
insert into COLORS (colorid, color)
values (795, 15655141);
insert into COLORS (colorid, color)
values (796, 13484485);
insert into COLORS (colorid, color)
values (797, 9143174);
insert into COLORS (colorid, color)
values (798, 8190976);
insert into COLORS (colorid, color)
values (799, 16775885);
insert into COLORS (colorid, color)
values (800, 15657407);
commit;
prompt 800 records committed...
insert into COLORS (colorid, color)
values (801, 13486501);
insert into COLORS (colorid, color)
values (802, 9144688);
insert into COLORS (colorid, color)
values (803, 11393254);
insert into COLORS (colorid, color)
values (804, 12578815);
insert into COLORS (colorid, color)
values (805, 11722734);
insert into COLORS (colorid, color)
values (806, 10141901);
insert into COLORS (colorid, color)
values (807, 6849419);
insert into COLORS (colorid, color)
values (808, 15761536);
insert into COLORS (colorid, color)
values (809, 14745599);
insert into COLORS (colorid, color)
values (810, 13758190);
insert into COLORS (colorid, color)
values (811, 11849165);
insert into COLORS (colorid, color)
values (812, 8031115);
insert into COLORS (colorid, color)
values (813, 15654274);
insert into COLORS (colorid, color)
values (814, 16772235);
insert into COLORS (colorid, color)
values (815, 15654018);
insert into COLORS (colorid, color)
values (816, 13483632);
insert into COLORS (colorid, color)
values (817, 9142604);
insert into COLORS (colorid, color)
values (818, 16448210);
insert into COLORS (colorid, color)
values (819, 9498256);
insert into COLORS (colorid, color)
values (820, 13882323);
insert into COLORS (colorid, color)
values (821, 16758465);
insert into COLORS (colorid, color)
values (822, 16756409);
insert into COLORS (colorid, color)
values (823, 15639213);
insert into COLORS (colorid, color)
values (824, 13470869);
insert into COLORS (colorid, color)
values (825, 9133925);
insert into COLORS (colorid, color)
values (826, 16752762);
insert into COLORS (colorid, color)
values (827, 15635826);
insert into COLORS (colorid, color)
values (828, 13468002);
insert into COLORS (colorid, color)
values (829, 9131842);
insert into COLORS (colorid, color)
values (830, 2142890);
insert into COLORS (colorid, color)
values (831, 8900346);
insert into COLORS (colorid, color)
values (832, 11592447);
insert into COLORS (colorid, color)
values (833, 10802158);
insert into COLORS (colorid, color)
values (834, 9287373);
insert into COLORS (colorid, color)
values (835, 6323083);
insert into COLORS (colorid, color)
values (836, 8679679);
insert into COLORS (colorid, color)
values (837, 7833753);
insert into COLORS (colorid, color)
values (838, 11584734);
insert into COLORS (colorid, color)
values (839, 13296127);
insert into COLORS (colorid, color)
values (840, 12374766);
insert into COLORS (colorid, color)
values (841, 10663373);
insert into COLORS (colorid, color)
values (842, 7240587);
insert into COLORS (colorid, color)
values (843, 16777184);
insert into COLORS (colorid, color)
values (844, 15658705);
insert into COLORS (colorid, color)
values (845, 13487540);
insert into COLORS (colorid, color)
values (846, 9145210);
insert into COLORS (colorid, color)
values (847, 3329330);
insert into COLORS (colorid, color)
values (848, 16445670);
insert into COLORS (colorid, color)
values (849, 16711935);
insert into COLORS (colorid, color)
values (850, 15597806);
insert into COLORS (colorid, color)
values (851, 13435085);
insert into COLORS (colorid, color)
values (852, 9109643);
insert into COLORS (colorid, color)
values (853, 11546720);
insert into COLORS (colorid, color)
values (854, 16725171);
insert into COLORS (colorid, color)
values (855, 15610023);
insert into COLORS (colorid, color)
values (856, 13445520);
insert into COLORS (colorid, color)
values (857, 9116770);
insert into COLORS (colorid, color)
values (858, 6737322);
insert into COLORS (colorid, color)
values (859, 205);
insert into COLORS (colorid, color)
values (860, 12211667);
insert into COLORS (colorid, color)
values (861, 14706431);
insert into COLORS (colorid, color)
values (862, 13721582);
insert into COLORS (colorid, color)
values (863, 11817677);
insert into COLORS (colorid, color)
values (864, 8009611);
insert into COLORS (colorid, color)
values (865, 9662683);
insert into COLORS (colorid, color)
values (866, 11240191);
insert into COLORS (colorid, color)
values (867, 10451438);
insert into COLORS (colorid, color)
values (868, 9005261);
insert into COLORS (colorid, color)
values (869, 6113163);
insert into COLORS (colorid, color)
values (870, 3978097);
insert into COLORS (colorid, color)
values (871, 8087790);
insert into COLORS (colorid, color)
values (872, 4772300);
insert into COLORS (colorid, color)
values (873, 13047173);
insert into COLORS (colorid, color)
values (874, 64154);
insert into COLORS (colorid, color)
values (875, 1644912);
insert into COLORS (colorid, color)
values (876, 16121850);
insert into COLORS (colorid, color)
values (877, 16770273);
insert into COLORS (colorid, color)
values (878, 15652306);
insert into COLORS (colorid, color)
values (879, 13481909);
insert into COLORS (colorid, color)
values (880, 9141627);
insert into COLORS (colorid, color)
values (881, 16770229);
insert into COLORS (colorid, color)
values (882, 16768685);
insert into COLORS (colorid, color)
values (883, 15650721);
insert into COLORS (colorid, color)
values (884, 13480843);
insert into COLORS (colorid, color)
values (885, 9140574);
insert into COLORS (colorid, color)
values (886, 128);
insert into COLORS (colorid, color)
values (887, 16643558);
insert into COLORS (colorid, color)
values (888, 7048739);
insert into COLORS (colorid, color)
values (889, 12648254);
insert into COLORS (colorid, color)
values (890, 11791930);
insert into COLORS (colorid, color)
values (891, 10145074);
insert into COLORS (colorid, color)
values (892, 6916898);
insert into COLORS (colorid, color)
values (893, 16753920);
insert into COLORS (colorid, color)
values (894, 15636992);
insert into COLORS (colorid, color)
values (895, 13468928);
insert into COLORS (colorid, color)
values (896, 9132544);
insert into COLORS (colorid, color)
values (897, 16729344);
insert into COLORS (colorid, color)
values (898, 15613952);
insert into COLORS (colorid, color)
values (899, 13448960);
insert into COLORS (colorid, color)
values (900, 9118976);
commit;
prompt 900 records committed...
insert into COLORS (colorid, color)
values (901, 14315734);
insert into COLORS (colorid, color)
values (902, 16745466);
insert into COLORS (colorid, color)
values (903, 15629033);
insert into COLORS (colorid, color)
values (904, 13461961);
insert into COLORS (colorid, color)
values (905, 9127817);
insert into COLORS (colorid, color)
values (906, 15657130);
insert into COLORS (colorid, color)
values (907, 10025880);
insert into COLORS (colorid, color)
values (908, 10157978);
insert into COLORS (colorid, color)
values (909, 9498256);
insert into COLORS (colorid, color)
values (910, 8179068);
insert into COLORS (colorid, color)
values (911, 5540692);
insert into COLORS (colorid, color)
values (912, 11529966);
insert into COLORS (colorid, color)
values (913, 12320767);
insert into COLORS (colorid, color)
values (914, 9883085);
insert into COLORS (colorid, color)
values (915, 6720395);
insert into COLORS (colorid, color)
values (916, 14381203);
insert into COLORS (colorid, color)
values (917, 16745131);
insert into COLORS (colorid, color)
values (918, 15628703);
insert into COLORS (colorid, color)
values (919, 13461641);
insert into COLORS (colorid, color)
values (920, 9127773);
insert into COLORS (colorid, color)
values (921, 16773077);
insert into COLORS (colorid, color)
values (922, 16767673);
insert into COLORS (colorid, color)
values (923, 15649709);
insert into COLORS (colorid, color)
values (924, 13479829);
insert into COLORS (colorid, color)
values (925, 9140069);
insert into COLORS (colorid, color)
values (926, 13468991);
insert into COLORS (colorid, color)
values (927, 16761035);
insert into COLORS (colorid, color)
values (928, 16758213);
insert into COLORS (colorid, color)
values (929, 15641016);
insert into COLORS (colorid, color)
values (930, 13472158);
insert into COLORS (colorid, color)
values (931, 9134956);
insert into COLORS (colorid, color)
values (932, 14524637);
insert into COLORS (colorid, color)
values (933, 16759807);
insert into COLORS (colorid, color)
values (934, 15642350);
insert into COLORS (colorid, color)
values (935, 13473485);
insert into COLORS (colorid, color)
values (936, 9135755);
insert into COLORS (colorid, color)
values (937, 11591910);
insert into COLORS (colorid, color)
values (938, 10494192);
insert into COLORS (colorid, color)
values (939, 10170623);
insert into COLORS (colorid, color)
values (940, 9514222);
insert into COLORS (colorid, color)
values (941, 8201933);
insert into COLORS (colorid, color)
values (942, 5577355);
insert into COLORS (colorid, color)
values (943, 16711680);
insert into COLORS (colorid, color)
values (944, 15597568);
insert into COLORS (colorid, color)
values (945, 13434880);
insert into COLORS (colorid, color)
values (946, 9109504);
insert into COLORS (colorid, color)
values (947, 12357519);
insert into COLORS (colorid, color)
values (948, 16761281);
insert into COLORS (colorid, color)
values (949, 15643828);
insert into COLORS (colorid, color)
values (950, 13474715);
insert into COLORS (colorid, color)
values (951, 9136489);
insert into COLORS (colorid, color)
values (952, 4286945);
insert into COLORS (colorid, color)
values (953, 4749055);
insert into COLORS (colorid, color)
values (954, 4419310);
insert into COLORS (colorid, color)
values (955, 3825613);
insert into COLORS (colorid, color)
values (956, 2572427);
insert into COLORS (colorid, color)
values (957, 9127187);
insert into COLORS (colorid, color)
values (958, 16416882);
insert into COLORS (colorid, color)
values (959, 16747625);
insert into COLORS (colorid, color)
values (960, 15630946);
insert into COLORS (colorid, color)
values (961, 13463636);
insert into COLORS (colorid, color)
values (962, 9129017);
insert into COLORS (colorid, color)
values (963, 16032864);
insert into COLORS (colorid, color)
values (964, 3050327);
insert into COLORS (colorid, color)
values (965, 5570463);
insert into COLORS (colorid, color)
values (966, 5172884);
insert into COLORS (colorid, color)
values (967, 4443520);
insert into COLORS (colorid, color)
values (968, 3050327);
insert into COLORS (colorid, color)
values (969, 16774638);
insert into COLORS (colorid, color)
values (970, 15656414);
insert into COLORS (colorid, color)
values (971, 13485503);
insert into COLORS (colorid, color)
values (972, 9143938);
insert into COLORS (colorid, color)
values (973, 10506797);
insert into COLORS (colorid, color)
values (974, 16745031);
insert into COLORS (colorid, color)
values (975, 15628610);
insert into COLORS (colorid, color)
values (976, 13461561);
insert into COLORS (colorid, color)
values (977, 9127718);
insert into COLORS (colorid, color)
values (978, 8900331);
insert into COLORS (colorid, color)
values (979, 8900351);
insert into COLORS (colorid, color)
values (980, 8306926);
insert into COLORS (colorid, color)
values (981, 7120589);
insert into COLORS (colorid, color)
values (982, 4878475);
insert into COLORS (colorid, color)
values (983, 6970061);
insert into COLORS (colorid, color)
values (984, 8613887);
insert into COLORS (colorid, color)
values (985, 8021998);
insert into COLORS (colorid, color)
values (986, 6904269);
insert into COLORS (colorid, color)
values (987, 4668555);
insert into COLORS (colorid, color)
values (988, 7372944);
insert into COLORS (colorid, color)
values (989, 13034239);
insert into COLORS (colorid, color)
values (990, 12178414);
insert into COLORS (colorid, color)
values (991, 10467021);
insert into COLORS (colorid, color)
values (992, 7109515);
insert into COLORS (colorid, color)
values (993, 16775930);
insert into COLORS (colorid, color)
values (994, 15657449);
insert into COLORS (colorid, color)
values (995, 13486537);
insert into COLORS (colorid, color)
values (996, 9144713);
insert into COLORS (colorid, color)
values (997, 65407);
insert into COLORS (colorid, color)
values (998, 61046);
insert into COLORS (colorid, color)
values (999, 52582);
insert into COLORS (colorid, color)
values (1000, 35653);
commit;
prompt 1000 records committed...
insert into COLORS (colorid, color)
values (1001, 4620980);
insert into COLORS (colorid, color)
values (1002, 6535423);
insert into COLORS (colorid, color)
values (1003, 6073582);
insert into COLORS (colorid, color)
values (1004, 5215437);
insert into COLORS (colorid, color)
values (1005, 3564683);
insert into COLORS (colorid, color)
values (1006, 13808780);
insert into COLORS (colorid, color)
values (1007, 16753999);
insert into COLORS (colorid, color)
values (1008, 15637065);
insert into COLORS (colorid, color)
values (1009, 13468991);
insert into COLORS (colorid, color)
values (1010, 9132587);
insert into COLORS (colorid, color)
values (1011, 14204888);
insert into COLORS (colorid, color)
values (1012, 16769535);
insert into COLORS (colorid, color)
values (1013, 15651566);
insert into COLORS (colorid, color)
values (1014, 13481421);
insert into COLORS (colorid, color)
values (1015, 9141131);
insert into COLORS (colorid, color)
values (1016, 16737095);
insert into COLORS (colorid, color)
values (1017, 15621186);
insert into COLORS (colorid, color)
values (1018, 13455161);
insert into COLORS (colorid, color)
values (1019, 9123366);
insert into COLORS (colorid, color)
values (1020, 4251856);
insert into COLORS (colorid, color)
values (1021, 62975);
insert into COLORS (colorid, color)
values (1022, 58862);
insert into COLORS (colorid, color)
values (1023, 50637);
insert into COLORS (colorid, color)
values (1024, 34443);
insert into COLORS (colorid, color)
values (1025, 15631086);
insert into COLORS (colorid, color)
values (1026, 13639824);
insert into COLORS (colorid, color)
values (1027, 16727702);
insert into COLORS (colorid, color)
values (1028, 15612556);
insert into COLORS (colorid, color)
values (1029, 13447800);
insert into COLORS (colorid, color)
values (1030, 9118290);
insert into COLORS (colorid, color)
values (1031, 16113331);
insert into COLORS (colorid, color)
values (1032, 16771002);
insert into COLORS (colorid, color)
values (1033, 15653038);
insert into COLORS (colorid, color)
values (1034, 13482646);
insert into COLORS (colorid, color)
values (1035, 9141862);
insert into COLORS (colorid, color)
values (1036, 16777215);
insert into COLORS (colorid, color)
values (1037, 16119285);
insert into COLORS (colorid, color)
values (1038, 16776960);
insert into COLORS (colorid, color)
values (1039, 15658496);
insert into COLORS (colorid, color)
values (1040, 13487360);
insert into COLORS (colorid, color)
values (1041, 9145088);
insert into COLORS (colorid, color)
values (1042, 10145074);
commit;
prompt 1042 records loaded
prompt Loading DEVCLASSES...
insert into DEVCLASSES (id_class, classname)
values (1, 'ÒÅÏËÎÑ×ÅÒ×ÈÊ');
commit;
prompt 1 records loaded
prompt Loading DATATYPE...
insert into DATATYPE (id_class, id_type, ctype)
values (1, 10, '941_s_float');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 11, '941_s_TO');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 12, '941_s_DO');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 13, '941_s_DS');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 14, '941_s_DW');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 1, 'INTEGER');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 2, 'LONG');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 3, 'SINGLE');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 4, 'DOUBLE');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 5, 'DATE');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 6, 'STRING');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 7, 'BYTE');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 8, 'VARIANT');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 9, 'HC');
commit;
prompt 14 records loaded
prompt Loading DEVICES...
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (15, 'VKT4', 'ÂÊÒ-4', null, 'DrvVKT4', 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (7, 'SPT943', 'ÑÏÒ943', 1, 'Drv943', 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (1, 'MT200116', 'MT200116', 1, 'DrvMT', 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (2, 'SPT941', 'ÑÏÒ941', 1, 'Drv941', 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (3, 'SPT942', 'ÑÏÒ942', 1, 'Drv942', 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (4, 'SPT960', 'ÑÏÒ960', 1, null, 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (5, 'SPT961', 'ÑÏÒ961', 1, null, 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (6, 'VKT2M_II', 'ÂÊÒ-2M II', 1, null, 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (11, 'VKT2M', 'VKT2M', 1, null, 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (8, 'SPT92_01', 'SPT92_01', 1, null, 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (9, 'SPT92_02', 'SPT92_02', 1, null, 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (10, 'SPT940', 'SPT940', 1, null, 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (12, 'VKT7', 'ÂÊÒ-7', 1, 'DrvVKT7', 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (14, 'TEM104', 'ÒÝÌ-104', 1, 'DrvTEM104', 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (13, 'TSRV03', 'ÒÑÐÂ-03', 1, 'DrvTSRV', 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (19, 'VKT5', 'ÂÊÒ-5', null, 'DrvVKT5', 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (17, 'TSRV02', 'ÒÑÐÂ-02', null, 'DrvTSRV02', 'M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (16, 'MAGIKA', 'ÌÀÃÈÊÀ', null, 'DrvMagika', 'M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (18, 'MC601', 'Ìóëüòèêàë 601', null, 'DrvMC601', 'V1;M1;Q1;');
insert into DEVICES (id_dev, cdevname, cdevdesc, id_class, dllname, verifycols)
values (20, 'DAN300', 'DANFOSS200-300', null, 'DrvDAN300', 'V1;M1;Q1;');
commit;
prompt 20 records loaded
prompt Loading PARAMTYPE...
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 7, 'Äàòà ñ÷åò÷èêà, åäèíèöû èçìåðåíèÿ è ò.ä.');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 0, 'Ñèñòåìíûå');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 1, 'Ìãíîâåííûå');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 2, 'Èòîãîâûå');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 3, '×àñîâûå Àðõèâíûå');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 4, 'Ñóòî÷íûå Àðõèâû');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 5, 'Íåøòàòíàÿ Ñèòóàöèÿ');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 99, 'Íåèçâåñòíûé');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 10, '10???');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 6, 'Ðàñ÷åòíûé');
commit;
prompt 10 records loaded
prompt Enabling foreign key constraints for DATATYPE...
alter table DATATYPE enable constraint DATATYPE_CLASS;
prompt Enabling foreign key constraints for DEVICES...
alter table DEVICES enable constraint DEVICE_DEVCLASS;
prompt Enabling foreign key constraints for PARAMTYPE...
alter table PARAMTYPE enable constraint PARAMTYPE_CLASS;
prompt Enabling triggers for COLORS...
alter table COLORS enable all triggers;
prompt Enabling triggers for DEVCLASSES...
alter table DEVCLASSES enable all triggers;
prompt Enabling triggers for DATATYPE...
alter table DATATYPE enable all triggers;
prompt Enabling triggers for DEVICES...
alter table DEVICES enable all triggers;
prompt Enabling triggers for PARAMTYPE...
alter table PARAMTYPE enable all triggers;
set feedback on
set define on
prompt Done.
