// Assembly PmDataAccess, Version 1.0.0.0

[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_OrgDi__OrgId__31B8F9AA", "PT_d_bm", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.PT_d_bm), "Bud_OrgDiaryCost", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_OrgDiaryCost), true)]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__PT_PrjInf__PrjGu__0313E4B1", "PT_PrjInfo_ZTB_Detail", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(cn.justwin.DAL.PT_PrjInfo_ZTB_Detail), "PT_PrjInfo_Kind", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.PT_PrjInfo_Kind), true)]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK_plus_report", "plus_report", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.plus_report), "plus_reportDetail", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.plus_reportDetail), true)]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "用户_部门", "PT_d_bm", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.PT_d_bm), "PT_yhmc", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.PT_yhmc), true)]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__PT_PrjInf__PrjGu__06E47595", "PT_PrjInfo_ZTB_Detail", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(cn.justwin.DAL.PT_PrjInfo_ZTB_Detail), "PT_PrjInfo_Rank", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.PT_PrjInfo_Rank), true)]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_ConsT__ConsR__61DC42C1", "Bud_ConsReport", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Bud_ConsReport), "Bud_ConsTask", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_ConsTask), true)]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_ConsT__ConsT__65ACD3A5", "Bud_ConsTask", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(cn.justwin.DAL.Bud_ConsTask), "Bud_ConsTaskRes", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_ConsTaskRes), true)]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_ConsT__Resou__66A0F7DE", "Res_Resource", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(cn.justwin.DAL.Res_Resource), "Bud_ConsTaskRes", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_ConsTaskRes), true)]
[assembly: System.Diagnostics.Debuggable(System.Diagnostics.DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: System.Runtime.CompilerServices.CompilationRelaxations(8)]
[assembly: System.Runtime.CompilerServices.RuntimeCompatibility(WrapNonExceptionThrows=true)]
[assembly: System.Runtime.Versioning.TargetFramework(".NETFramework,Version=v4.0", FrameworkDisplayName=".NET Framework 4")]
[assembly: System.Reflection.AssemblyTitle("PmDataAccess")]
[assembly: System.Reflection.AssemblyDescription("")]
[assembly: System.Reflection.AssemblyConfiguration("")]
[assembly: System.Reflection.AssemblyCompany("justwin")]
[assembly: System.Reflection.AssemblyProduct("PmDataAccess")]
[assembly: System.Reflection.AssemblyCopyright("版权所有 (C) justwin 2010")]
[assembly: System.Reflection.AssemblyTrademark("")]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: System.Runtime.InteropServices.Guid("77e33095-687d-497b-a7a9-ea3ba3c553f1")]
[assembly: System.Reflection.AssemblyFileVersion("1.0.0.0")]
[assembly: System.Data.Objects.DataClasses.EdmSchema]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Res_Attri__Resou__1A4AB916", "Res_ResourceType", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Res_ResourceType), "Res_Attribute", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Res_Attribute))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Res_Price__Price__176E4C6B", "Res_PriceType", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(cn.justwin.DAL.Res_PriceType), "Res_Price", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Res_Price), true)]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Res_Price__Resou__167A2832", "Res_Resource", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(cn.justwin.DAL.Res_Resource), "Res_Price", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Res_Price), true)]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Res_Resou__Resou__10C14EDC", "Res_ResourceType", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Res_ResourceType), "Res_Resource", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Res_Resource))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Res_Resour__Unit__11B57315", "Res_Unit", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Res_Unit), "Res_Resource", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Res_Resource))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Res_Resou__Paren__0DE4E231", "Res_ResourceType", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Res_ResourceType), "Res_ResourceType1", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Res_ResourceType))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Res_Resou__Attri__7D446614", "Res_Attribute", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Res_Attribute), "Res_Resource", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Res_Resource))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Res_Templ__Templ__61674175", "Res_Template", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Res_Template), "Res_TemplateItem", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Res_TemplateItem))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_Templ__Templ__2611717D", "Bud_TemplateType", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Bud_TemplateType), "Bud_Template", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_Template))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_TaskR__TaskI__269B8162", "Bud_Task", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Bud_Task), "Bud_TaskResource", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_TaskResource))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_TaskR__Resou__278FA59B", "Res_Resource", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Res_Resource), "Bud_TaskResource", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_TaskResource))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_Templ__Templ__247E2EC6", "Bud_Template", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Bud_Template), "Bud_TemplateItem", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_TemplateItem))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_Templ__Resou__338B682C", "Res_Resource", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Res_Resource), "Bud_TemplateResource", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_TemplateResource))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_Templ__Templ__6DB809C1", "Bud_TemplateItem", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Bud_TemplateItem), "Bud_TemplateResource", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_TemplateResource))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_Contr__Resou__705F6C42", "Res_Resource", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Res_Resource), "Bud_ContractResource", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_ContractResource))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_Contr__TaskI__6F6B4809", "Bud_ContractTask", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Bud_ContractTask), "Bud_ContractResource", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_ContractResource))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Res_Resou__TaskI__63F9955D", "Bud_Task", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Bud_Task), "Res_ResourceTemp", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Res_ResourceTemp))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Res_Resou__Resou__63057124", "Res_Resource", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Res_Resource), "Res_ResourceTemp", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Res_ResourceTemp))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_Indir__Indir__0801EBA9", "Bud_IndirectBudget", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(cn.justwin.DAL.Bud_IndirectBudget), "Bud_IndirectMonthBudget", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_IndirectMonthBudget))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_Indir__InDia__0D85BAD5", "Bud_IndirectDiaryCost", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Bud_IndirectDiaryCost), "Bud_IndirectDiaryDetails", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_IndirectDiaryDetails))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_Organ__Organ__69D26A44", "Bud_OrganizationBudget", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Bud_OrganizationBudget), "Bud_OrganizationMonthBudget", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_OrganizationMonthBudget))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_OrgDi__OrgDi__48C67C34", "Bud_OrgDiaryCost", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.Bud_OrgDiaryCost), "Bud_OrgDiaryDetails", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_OrgDiaryDetails))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Basic_Cod__TypeC__37A6DD2A", "Basic_CodeType", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(cn.justwin.DAL.Basic_CodeType), "Basic_CodeList", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Basic_CodeList), true)]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__PT_PrjMem__Membe__40071901", "PT_yhmc", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.PT_yhmc), "PT_PrjMember", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.PT_PrjMember))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__PT_Prj_Co__Input__5EE0A5DC", "PT_yhmc", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.PT_yhmc), "PT_Prj_Completed", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.PT_Prj_Completed))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__PT_Prj_Co__PrjCo__6A525888", "PT_Prj_Completed", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.PT_Prj_Completed), "PT_Prj_Completed_Detail", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.PT_Prj_Completed_Detail))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__PT_Prj_Co__Input__6B467CC1", "PT_yhmc", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.PT_yhmc), "PT_Prj_Completed_Detail", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.PT_Prj_Completed_Detail))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__PT_Prj_Co__Detai__32CD1974", "PT_Prj_Completed_Detail", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.PT_Prj_Completed_Detail), "PT_Prj_Completed_Annex", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.PT_Prj_Completed_Annex))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK_plus_progress_InputUser", "PT_yhmc", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.PT_yhmc), "plus_progress", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.plus_progress))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK_privilege_ProgressId", "plus_progress", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(cn.justwin.DAL.plus_progress), "plus_progress_privilege", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.plus_progress_privilege))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK_progress_version_ProgressId", "plus_progress", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(cn.justwin.DAL.plus_progress), "plus_progress_version", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.plus_progress_version))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK_privilege_UserCode", "PT_yhmc", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(cn.justwin.DAL.PT_yhmc), "plus_progress_privilege", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.plus_progress_privilege))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK_progress_version_InputUser", "PT_yhmc", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.PT_yhmc), "plus_progress_version", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.plus_progress_version))]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK__Bud_TaskC__Input__292EA0A1", "PT_yhmc", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.PT_yhmc), "Bud_TaskChange", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.Bud_TaskChange), true)]
[assembly: System.Data.Objects.DataClasses.EdmRelationship("Pm2Model", "FK_plus_progress_version", "plus_progress_version", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(cn.justwin.DAL.plus_progress_version), "plus_report", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(cn.justwin.DAL.plus_report), true)]

