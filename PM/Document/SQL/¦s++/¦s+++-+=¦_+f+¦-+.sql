--清空资金计划主表
DELETE FROM [Fund_Plan_MonthMain]
--清空资金计划从表
DELETE FROM [Fund_Plan_MonthDetail]
--清空资金计划上报主表
DELETE FROM [Fund_Plan_Summary_Main]
--清空资金计划上报从表
DELETE FROM  [Fund_Plan_Summary_Detail]
--清空资金账户表
DELETE FROM  [Fund_Prj_Accoun]
--清空资金收入入账表
DELETE FROM  [Fund_Prj_Accoun_Income]
--清空资金支出入账表
DELETE FROM  [Fund_Prj_Accoun_Payout]
--清空资金借款表
DELETE FROM  [Fund_Prj_Loan]
--清空资金还款款表
DELETE FROM  [Fund_Prj_Loan_Return]
--参数设置表，此表初始化