SELECT * FROM FLOW_SETTING_MAIN fsm -- �y�{�]�w�D��
JOIN FLOW_STAGE_SETTING fss ON fsm.FLOW_ID = fss.FLOW_ID -- �y�{����
JOIN FLOW_ROLES fr ON fr.ROLE_ID = fss.ROLE_ID -- �y�{���d�����]�w��
-- ���d�]�w��

-- �W�U���p
-- SIGN_FORM_MAIN.FLOW_ID = FLOW_STAGE_SETTING.FLOW_ID
-- SERVICE_CODE.FLOW_NO = FLOW_SETTING_MAIN.FLOW_NO 

SELECT * FROM SIGN_FORM_MAIN sfm -- ñ�֪��D��
JOIN PAM_SYSTEM_SERVICES pss ON sfm.SERVICE_CODE = pss.SERVICE_CODE -- �t�κ��@ - ñ�֬y�{�˪O���@
JOIN SIGN_STAGE ss ON sfm.SIGN_FORM_ID = ss.SIGN_FORM_ID -- ñ�����d
-- �]�w�᪺���d
