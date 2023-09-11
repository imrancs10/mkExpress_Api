using System;


public class cateries
{
    public decimal? cat_id { get; set; }

    public string cat_name { get; set; }

    public string Barcode { get; set; }

    public decimal? Unit { get; set; }

    public decimal? Cost_price { get; set; }

    public decimal? Sale_price { get; set; }

    public string S_PATH { get; set; }

    public string Acc_ID { get; set; }

    public string status { get; set; }

    public string Brand { get; set; }

    public string Description { get; set; }

    public decimal? Size { get; set; }

    public decimal? SUP_ID { get; set; }

    public decimal? Buffer_Stock { get; set; }
}

public class Chart_Of_Account
{
    public string Gl_account_Code { get; set; }

    public string Description { get; set; }

    public short? Account_level { get; set; }

    public short? Account_Type { get; set; }

    public string Formatted_Code { get; set; }

    public string Parent_Code { get; set; }

    public int? Opening_Baln { get; set; }
}

public class Child
{
    public decimal? C_id { get; set; }

    public string Ch_name { get; set; }

    public string Ch_Contact1 { get; set; }

    public string Acc_id { get; set; }

    public decimal? Cus_id { get; set; }

    public string Relation { get; set; }
}

public class Combo
{
    public decimal? Option_Id { get; set; }

    public string Option_Value { get; set; }

    public string Option_Path { get; set; }
}

public class CrystalUsed
{
    public decimal? Ord_Cry_Id { get; set; }

    public string Ord_no_Detail { get; set; }

    public string ord_No { get; set; }

    public float? Ord_Qty { get; set; }

    public decimal? Ord_Voucher { get; set; }

    public string Ord_Cry_name { get; set; }

    public decimal? Ord_Cry_N_ID { get; set; }

    public DateTime? Crystal_date { get; set; }

    public string Cry_Shape { get; set; }

    public string Cry_Size { get; set; }

    public string Branch { get; set; }
}


public class Cuttinrder
{
    public decimal Co_id { get; set; }

    public string Orderno { get; set; }

    public decimal? emp_id { get; set; }

    public string EmpName { get; set; }

    public DateTime? Deldate { get; set; }

    public DateTime? Cutdate { get; set; }

    public float? Quantity { get; set; }

    public float? Amount { get; set; }

    public decimal? Status { get; set; }

    public string CutBy { get; set; }

    public string Ord_no { get; set; }

    public decimal? Work_Sheet_ID { get; set; }

    public decimal? Work_Type_ID { get; set; }

    public string Note { get; set; }

    public string Grade { get; set; }

    public float? AlterAmount { get; set; }

    public float? ExtraAmount { get; set; }

    public DateTime? Asign_Date { get; set; }

    public string Design { get; set; }

    public string ModelNo { get; set; }

    public string Inch { get; set; }

    public float? SS16PlusCrystal { get; set; }

    public string Final_Check { get; set; }
}

public class Del_allert
{
    public decimal? Del_id { get; set; }

    public string Del_Order { get; set; }

    public string Del_Customer { get; set; }

    public DateTime? Del_Req_date { get; set; }

    public decimal? Quantity { get; set; }

    public string Del_status { get; set; }
}

public class DeliverdOrders
{
    public decimal? Del_Ord_ID { get; set; }

    public string Del_Ord_No { get; set; }

    public decimal? Del_Ord_Qty { get; set; }

    public string Del_Ord_Cus_Name { get; set; }

    public decimal? Del_Ord_Due_Amount { get; set; }

    public decimal? Del_Ord_Paid_Amount { get; set; }

    public DateTime? Del_Ord_Date { get; set; }

    public string Del_Ord_Note { get; set; }
}

public class DeliveryReport
{
    public decimal inv_no { get; set; }

    public string ord_no { get; set; }

    public string Cus_name { get; set; }

    public string Cus_contact { get; set; }

    public DateTime? inv_date { get; set; }

    public decimal? inv_amount { get; set; }

    public decimal? inv_advance { get; set; }

    public decimal? inv_bal { get; set; }

    public string Master { get; set; }

    public DateTime? DateDate { get; set; }

    public string CashType { get; set; }

    public decimal? Qty { get; set; }

    public decimal? VAT_P { get; set; }

    public DateTime? Del_Date { get; set; }
}

public class Deposit
{
    public decimal? D_No { get; set; }

    public string D_Description { get; set; }

    public DateTime? D_Date { get; set; }

    public decimal? Bank_Id { get; set; }

    public decimal? D_Deposit { get; set; }
}

public class EmpAdvanceReport
{
    public decimal? Emp_Id { get; set; }

    public string Emp_Name { get; set; }

    public string Emp_Status { get; set; }

    public decimal? Emp_Tot_Advance { get; set; }

    public decimal? Emp_Given_Amount { get; set; }

    public decimal? Emp_Bal_Amount { get; set; }

    public DateTime? Emp_Date { get; set; }
}

public class EmpOSale
{
    public string Name { get; set; }

    public decimal? Emp_id { get; set; }

    public decimal? Qty { get; set; }

    public decimal? Amount { get; set; }

    public decimal? UsedUnit { get; set; }

    public DateTime? T_date { get; set; }
}

public class EmpSummery
{
    public string Emp_Name { get; set; }

    public string Emp_Status { get; set; }

    public decimal? Emp_Qty { get; set; }

    public decimal? Emp_Amount { get; set; }

    public decimal? Emp_id { get; set; }

    public string Emp_month { get; set; }

    public decimal? Emp_P { get; set; }

    public float? Emp_Average { get; set; }

    public decimal? BasicSalary { get; set; }

    public decimal? NetSalary { get; set; }

    public decimal? AvanceSalary { get; set; }

    public decimal? GrossSalary { get; set; }
}

public class EntityRentElectric
{
    public decimal? EntityID { get; set; }

    public string EntityName { get; set; }

    public string EntityNO { get; set; }

    public string MeterNo { get; set; }

    public decimal? RentAmount { get; set; }
}

public class ExpCatery
{
    public decimal? Exp_Id { get; set; }

    public string Exp_name { get; set; }

    public decimal? Exp_ParID { get; set; }
}




public class FabricOrders
{
    public string Item_code { get; set; }

    public decimal? Item_Ord_Unit { get; set; }

    public DateTime? Item_ord_Date { get; set; }

    public string Item_ord_Number { get; set; }

    public decimal? Status { get; set; }
}

public class Gl_Account_Type
{
    public int? Account_type { get; set; }

    public string Description { get; set; }

    public string Short_Name { get; set; }
}

public class Gl_Ledger
{
    public string Account_Code { get; set; }

    public string Account_Desc { get; set; }

    public float? Open_Amount { get; set; }

    public float? Close_Amount { get; set; }

    public string Formated_Account { get; set; }
}

public class Gl_Ledger_Detail
{
    public string Account_Code { get; set; }

    public string Voucher_no { get; set; }

    public DateTime? Voucher_Date { get; set; }

    public string Narration { get; set; }

    public float? Amount { get; set; }

    public float? Running_Balance { get; set; }

    public string Short_Name { get; set; }

    public string Voucher_status { get; set; }

    public float? DrAmount { get; set; }

    public float? CrAmount { get; set; }

    public string Chk_dd_tt { get; set; }
}

public class GlPeriod
{
    public short? Gl_Period { get; set; }

    public string Gl_year_id { get; set; }

    public DateTime? Period_From { get; set; }

    public DateTime? Period_To { get; set; }

    public string Period_Name { get; set; }

    public string Active { get; set; }
}

public class Gl_Voucher
{
    public double? Serial_number { get; set; }

    public string Gl_year_id { get; set; }

    public int? Gl_period { get; set; }

    public short? Voucher_Type_id { get; set; }

    public string Voucher_Short_Name { get; set; }

    public int? Voucher_no { get; set; }

    public DateTime? Voucher_date { get; set; }

    public string Remarks { get; set; }

    public string Status { get; set; }

    public string Chk_dd_tt { get; set; }

    public string Issued_Received { get; set; }

    public string Narration { get; set; }
}

public class Gl_Voucher_Detail
{
    public string Gl_year_id { get; set; }

    public int? Gl_period { get; set; }

    public int? Voucher_Type_id { get; set; }

    public int? Voucher_no { get; set; }

    public int? Voucher_Detail_id { get; set; }

    public string Gl_Account_code { get; set; }

    public float Amount { get; set; }

    public string Narration { get; set; }

    public double? Serial_Number { get; set; }
}

public class Gl_Voucher_Type
{
    public short Voucher_type_id { get; set; }

    public string Voucher_short_name { get; set; }

    public string Entry_Side { get; set; }

    public string Voucher_name { get; set; }

    public string Voucher_Reset { get; set; }
}

public class Gl_Year
{
    public string Gl_year_id { get; set; }

    public DateTime? Year_From { get; set; }

    public DateTime? Year_To { get; set; }

    public string Next_Year { get; set; }

    public string Pl_account { get; set; }

    public string Active { get; set; }
}

public class Gl_Year_Amount
{
    public string Gl_year_id { get; set; }

    public string gl_Account_code { get; set; }

    public string Parent_Code { get; set; }

    public float? Opening_Amount { get; set; }
}

public class GrandSummary
{
    public string Summary_Month { get; set; }

    public decimal? Booking_Quantity { get; set; }

    public decimal? Booking_Amount { get; set; }

    public decimal? Booking_Cash_Adv { get; set; }

    public decimal? Booking_Card_Adv { get; set; }

    public decimal? Booking_Chq_Adv { get; set; }

    public decimal? Total_booking_Adv { get; set; }

    public decimal? Total_Del_Quantity { get; set; }

    public decimal? Total_Del_Amount { get; set; }

    public decimal? Del_Cash_Amount { get; set; }

    public decimal? Del_Card_Amount { get; set; }

    public decimal? Del_Chq_Amount { get; set; }

    public decimal? Total_Cash_Amount { get; set; }

    public decimal? Total_Card_Amount { get; set; }

    public decimal? Total_Chq_Amount { get; set; }

    public decimal? Total_Grand_Sale { get; set; }

    public decimal? Total_Expences { get; set; }

    public decimal? Total_Net_Sale { get; set; }

    public decimal? Total_Cancel_amount { get; set; }

    public decimal? Total_Cancel_Qty { get; set; }

    public decimal? Total_Cancel_Adv { get; set; }

    public decimal? Total_Vat { get; set; }
}

public class InvDetail
{
    public decimal? inv_no { get; set; }

    public string Items { get; set; }

    public decimal? Qty { get; set; }

    public decimal? UnitPrice_Dhs { get; set; }

    public decimal? Total_Dhs { get; set; }

    public string Model_No { get; set; }

    public decimal? Vat { get; set; }

    public decimal? UnitPriceWOVat { get; set; }

    public string DelStatus { get; set; }

    public string OrderNo { get; set; }
}

public class invmaster
{
    public decimal inv_no { get; set; }

    public string ord_no { get; set; }

    public string Cus_name { get; set; }

    public string Cus_contact { get; set; }

    public DateTime? inv_date { get; set; }

    public decimal? inv_amount { get; set; }

    public decimal? inv_advance { get; set; }

    public decimal? inv_descount { get; set; }

    public decimal? inv_bal { get; set; }

    public DateTime? inv_rdate { get; set; }

    public string Cus_Acc_Id { get; set; }

    public string Master { get; set; }

    public string Mesurstatus { get; set; }

    public string Order_status { get; set; }

    public string CashType { get; set; }

    public decimal? Qty { get; set; }

    public decimal? VAT_P { get; set; }

    public decimal? VAT_T { get; set; }

    public decimal? VAT_B { get; set; }

    public decimal? G_T_AMOUNT { get; set; }

    public decimal? G_T_PAID { get; set; }

    public decimal? G_T_BAL { get; set; }
}

public class K_Type
{
    public decimal? K_Type_Id { get; set; }

    public string K_Type_Name { get; set; }
}

public class Kandoora
{
    public decimal oid { get; set; }

    public decimal? O_id { get; set; }

    public decimal? Ordn { get; set; }

    public string KType { get; set; }

    public DateTime? Deldate { get; set; }

    public DateTime? Odate { get; set; }

    public decimal? FabricUnit { get; set; }

    public decimal? status { get; set; }

    public decimal? Cataries { get; set; }
}

public class MachineEMbWork
{
    public decimal? COL_ID { get; set; }

    public string COL_NAME { get; set; }

    public decimal? COL_VALUE { get; set; }
}

public class Main_option
{
    public int? main_Lo_one { get; set; }

    public int? K_id { get; set; }

    public string main_m_name { get; set; }

    public string main_value { get; set; }

    public int? option_Value { get; set; }

    public string Textbox { get; set; }

    public string Combo { get; set; }

    public string Picture { get; set; }

    public decimal? Cost_Price { get; set; }

    public decimal? Sale_Price { get; set; }

    public decimal? Crystal_U { get; set; }
}

public class MainAccount
{
    public decimal? T_No { get; set; }

    public string Description { get; set; }

    public DateTime? T_date { get; set; }

    public string Order_NUmber { get; set; }

    public string User_Name { get; set; }

    public decimal? Cre_Amount { get; set; }

    public decimal? Dre_Amount { get; set; }

    public string Cus_Acc { get; set; }

    public decimal? Qty { get; set; }

    public decimal? Cus_ID { get; set; }

    public decimal? Child_Id { get; set; }

    public decimal? Master_Acc { get; set; }

    public DateTime? B_P_TIme { get; set; }

    public decimal? B_P_ID_2 { get; set; }

    public decimal? B_P_ID_3 { get; set; }

    public decimal? B_P_ID_4 { get; set; }

    public string B_P_ID_5 { get; set; }

    public string B_P_ID_1 { get; set; }
}

public class Majer_Expense
{
    public string Column_0 { get; set; }

    public string Column_1 { get; set; }
}

public class MajorExpCatery
{
    public decimal? MEex_ID { get; set; }

    public string MExp_Name { get; set; }
}

public class Managerial_report_Detail
{
    public string Account_Code { get; set; }

    public string Voucher_no { get; set; }

    public DateTime? Voucher_Date { get; set; }

    public string Narration { get; set; }

    public float? Amount { get; set; }

    public float? Running_Balance { get; set; }

    public string Short_Name { get; set; }

    public string Voucher_status { get; set; }

    public float? DrAmount { get; set; }

    public float? CrAmount { get; set; }

    public string Chk_dd_tt { get; set; }

    public string TransMonth { get; set; }

    public string Account_Description { get; set; }
}

public class Managerial_report_master
{
    public string Account_Code { get; set; }

    public string Account_Desc { get; set; }

    public float? Open_Amount { get; set; }

    public float? Close_Amount { get; set; }

    public string Formated_Account { get; set; }
}

public class Measurment
{
    public decimal? Cus_Mea_ID { get; set; }

    public string Cus_Order_No { get; set; }

    public string Length { get; set; }

    public string Hip { get; set; }

    public string Saleev { get; set; }

    public string Shoulder { get; set; }

    public string Neck { get; set; }

    public string Extra { get; set; }

    public string Qchest { get; set; }

    public string Qbottom { get; set; }

    public string Sleev_L { get; set; }

    public string Deep { get; set; }

    public string BackD { get; set; }

    public string Extra2 { get; set; }
}



public class Mreport
{
    public decimal? E_ID { get; set; }

    public string voucher { get; set; }

    public string Description { get; set; }

    public DateTime? E_date { get; set; }

    public decimal? Amount { get; set; }

    public decimal? Status { get; set; }
}

public class NotAssignYet
{
    public string OrderNo { get; set; }

    public string Design { get; set; }

    public string ModelNo { get; set; }

    public string Type { get; set; }

    public string Sign { get; set; }

    public DateTime? Booking_Date { get; set; }

    public DateTime? Delivery_Date { get; set; }

    public string Size { get; set; }

    public decimal? Amount { get; set; }

    public string Work_Type { get; set; }

    public string Saleman { get; set; }
}

public class Notes
{
    public string Gl_year_id { get; set; }

    public short? Notes_id { get; set; }

    public short? Bal_Pl { get; set; }

    public string Gl_Account_code { get; set; }

    public short? Note_no { get; set; }
}

public class Option_Work
{
    public decimal? Work_ID { get; set; }

    public string Work_Name { get; set; }

    public decimal? Option_ID { get; set; }
}

public class Options
{
    public int? K_id { get; set; }

    public string k_name { get; set; }

    public string K_Description { get; set; }

    public string K_path { get; set; }

    public DateTime? K_Date { get; set; }

    public int? K_price { get; set; }
}

public class Options_Work
{
    public decimal? Work_ID { get; set; }

    public string Work_Name { get; set; }

    public decimal? Option_ID { get; set; }
}



public class OrderKandoora
{
    public decimal? Co_id { get; set; }

    public string K_type { get; set; }

    public decimal? k_Amount { get; set; }

    public string K_Number { get; set; }

    public string K_Size { get; set; }
}

public class OrderKN
{
    public decimal? Co_id { get; set; }

    public string K_type { get; set; }

    public decimal? k_Amount { get; set; }

    public string K_Number { get; set; }

    public string K_Size { get; set; }

    public string Requirment { get; set; }

    public decimal? Product_ID { get; set; }

    public decimal? UnitUsed { get; set; }

    public string Length { get; set; }

    public string Shoulder { get; set; }

    public string Sleeves { get; set; }

    public string Chest { get; set; }

    public string Neck { get; set; }

    public string Bottom { get; set; }

    public string Type { get; set; }

    public decimal? Status { get; set; }

    public string Path { get; set; }

    public string Fabric { get; set; }

    public string WorkType { get; set; }
}

public class List
{
    public decimal? Order_no { get; set; }

    public decimal? Balance_Amount { get; set; }

    public string Contact { get; set; }

    public DateTime? Date { get; set; }

    public string Status { get; set; }

    public decimal? Kn_Id { get; set; }

    public string Note { get; set; }

    public string PackedOption { get; set; }

    public string Note2 { get; set; }
}

public class PaymentReport
{
    public DateTime? VOUCHERDATE { get; set; }

    public string NARRATION { get; set; }

    public string CHECKNO { get; set; }

    public string REFNO { get; set; }

    public string ACCOUNTNO { get; set; }

    public string DESCRIPTION { get; set; }

    public int? AMOUNT { get; set; }

    public string ACCOUNTCODE { get; set; }

    public string TITLE { get; set; }

    public float? DRAMOUNT { get; set; }

    public float? CRAMOUNT { get; set; }
}

public class Period_Closing
{
    public string gl_Account_code { get; set; }

    public string Gl_year_id { get; set; }

    public short? Gl_period { get; set; }

    public float? Balance { get; set; }

    public float? Pl_Balance { get; set; }
}

public class Pictures
{
    public decimal? Pic_d { get; set; }

    public string Pic_Sam { get; set; }

    public string Pic_Price { get; set; }

    public string Pic_Design { get; set; }

    public decimal? Pic_Cat_Id { get; set; }

    public string Pic_PaperSize { get; set; }

    public string Pic_Prefix { get; set; }

    public string Pc_Type { get; set; }

    public string Pc_Inch { get; set; }
}

public class POSTBACKUP
{
    public decimal? POST_B_ID { get; set; }

    public decimal? P_B_ID_2 { get; set; }

    public decimal? P_B_ID_3 { get; set; }

    public decimal? P_B_ID_4 { get; set; }

    public decimal? P_B_ID_5 { get; set; }
}

public class ProductionExp
{
    public decimal? Exp_ID { get; set; }

    public decimal? Metrial_Exp { get; set; }

    public decimal? Telephone_Exp { get; set; }

    public decimal? Mechine_Exp { get; set; }

    public decimal? ShopRent_Exp { get; set; }

    public decimal? WorkShop_Exp { get; set; }

    public decimal? Electricity_Exp { get; set; }

    public decimal? Medical_Exp { get; set; }

    public decimal? Visa_Exp { get; set; }

    public decimal? AC_Exp { get; set; }

    public decimal? Transport_Exp { get; set; }

    public decimal? CrystalPacket_Exp { get; set; }

    public decimal? Pooth { get; set; }

    public decimal? Rashim { get; set; }

    public decimal? Silk { get; set; }

    public decimal? Safoon { get; set; }

    public decimal? Toor { get; set; }

    public decimal? TotalExp { get; set; }

    public decimal? DesignExp { get; set; }

    public decimal? CuttingExp { get; set; }

    public decimal? Machine_Emb { get; set; }

    public decimal? Hand_Emb { get; set; }

    public decimal? Stitch_Exp { get; set; }

    public decimal? RoomExp { get; set; }

    public decimal? SalaryStaff { get; set; }

    public decimal? Kafalet { get; set; }

    public decimal? License { get; set; }

    public decimal? ShoppingBag { get; set; }

    public decimal? StitchMat { get; set; }

    public decimal? ExtraExp { get; set; }

    public decimal? TotalCostL { get; set; }
}

public class ProductionSummery
{
    public string OrderNo { get; set; }

    public DateTime? OrderDate { get; set; }

    public string Kandoora_No { get; set; }

    public string Name { get; set; }

    public decimal? Amount { get; set; }

    public string ModelNo { get; set; }

    public decimal? Designer { get; set; }

    public decimal? Cutting { get; set; }

    public decimal? MechEmb { get; set; }

    public decimal? Crystal { get; set; }

    public decimal? HotFixing { get; set; }

    public decimal? handEmb { get; set; }

    public decimal? Stitching { get; set; }

    public decimal? MaterialExp { get; set; }

    public decimal? AllExp { get; set; }

    public decimal? TotalExp { get; set; }

    public decimal? Profit { get; set; }

    public decimal? ProfitPercentage { get; set; }

    public decimal? CutWork { get; set; }

    public decimal? PatchWork { get; set; }

    public string Design { get; set; }
}



public class PROFITANDLOSS
{
    public float? SERVICES { get; set; }

    public float? COSTOFPATIENT { get; set; }

    public float? Amount { get; set; }

    public float? ADMINISTRATION { get; set; }

    public float? PROMOTIONAL { get; set; }

    public float? FINANCIAL { get; set; }

    public float? SumofFinandProm { get; set; }

    public float? OTHERINCOME { get; set; }

    public float? PROFITORLOSS { get; set; }

    public float? GenExp { get; set; }

    public float? RentalIncome { get; set; }
}





public class ReceiptReport
{
    public DateTime? VOUCHERDATE { get; set; }

    public string NARRATION { get; set; }

    public string CHECKNO { get; set; }

    public string REFNO { get; set; }

    public string ACCOUNTNO { get; set; }

    public string DESCRIPTION { get; set; }

    public int? AMOUNT { get; set; }

    public string ACCOUNTCODE { get; set; }

    public string TITLE { get; set; }

    public float? DRAMOUNT { get; set; }

    public float? CRAMOUNT { get; set; }
}

public class RptChartofAccount
{
    public string Account_code { get; set; }

    public string Description { get; set; }

    public short? Account_Level { get; set; }

    public string parent_code { get; set; }

    public string parent_name { get; set; }

    public string Main { get; set; }

    public string Main_name { get; set; }

    public short? Account_type { get; set; }

    public int? DR { get; set; }

    public int? CR { get; set; }
}

public class RptOpeningBalances
{
    public string Account_code { get; set; }

    public string Description { get; set; }

    public short? Account_Level { get; set; }

    public string parent_code { get; set; }

    public string parent_name { get; set; }

    public string Main { get; set; }

    public string Main_name { get; set; }

    public short? Account_type { get; set; }

    public float? DR { get; set; }

    public float? CR { get; set; }
}

public class Saleman_Comm
{
    public string Order_No { get; set; }

    public string Customer_Name { get; set; }

    public decimal? Qty { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? Advance { get; set; }

    public decimal? Balance { get; set; }

    public string Status { get; set; }

    public decimal? Ever_kan_Profit { get; set; }

    public decimal? Sale_Com { get; set; }

    public DateTime? Order_Date { get; set; }

    public string Saleman { get; set; }

    public string OrderNo { get; set; }
}

public class SalePurchaseReport
{
    public DateTime? VoucherDate { get; set; }

    public string Narration { get; set; }

    public string Refno { get; set; }

    public string Billno { get; set; }

    public string Maincode { get; set; }

    public string Maindescription { get; set; }

    public string Subcode { get; set; }

    public string Subdescription { get; set; }

    public float? DrAmount { get; set; }

    public float? CrAmount { get; set; }
}
public class Sheet2
{
    public decimal? SR_NO { get; set; }

    public decimal? ORDER_NO { get; set; }
}

public class SUMERY1
{
    public string SHOPNAME { get; set; }

    public decimal? TOTAL_GRANDTOTAL { get; set; }

    public decimal? TOTAL_CREDIT { get; set; }

    public decimal? TOTAL_CHEQUE { get; set; }

    public decimal? TOTAL_NETSALE { get; set; }

    public DateTime? START_DATE { get; set; }
}

public class Summary
{
    public DateTime? Report_Date { get; set; }

    public decimal? Order_book { get; set; }

    public decimal? Order_Book_Amount { get; set; }

    public decimal? Order_Delivery { get; set; }

    public decimal? Order_Delivery_Amount { get; set; }

    public decimal? W_Des_Qty { get; set; }

    public decimal? W_Des_Amount { get; set; }

    public decimal? W_Cut_Qty { get; set; }

    public decimal? W_CUt_Amount { get; set; }

    public decimal? W_Emb_Qty { get; set; }

    public decimal? W_Emb_Amount { get; set; }

    public decimal? W_Crys_Qty { get; set; }

    public decimal? W_Crys_Amount { get; set; }

    public decimal? W_Hand_Qty { get; set; }

    public decimal? W_Hand_Amount { get; set; }

    public decimal? W_Ste_Qty { get; set; }

    public decimal? W_Ste_Amount { get; set; }

    public decimal? Total_Expences { get; set; }

    public decimal? Total_Vat { get; set; }
}

public class Summery
{
    public DateTime? Report_Date { get; set; }

    public decimal? Order_book { get; set; }

    public decimal? Order_Book_Amount { get; set; }

    public decimal? Order_Delivery { get; set; }

    public decimal? Order_Delivery_Amount { get; set; }

    public decimal? W_Des_Qty { get; set; }

    public decimal? W_Des_Amount { get; set; }

    public decimal? W_Cut_Qty { get; set; }

    public decimal? W_CUt_Amount { get; set; }

    public decimal? W_Emb_Qty { get; set; }

    public decimal? W_Emb_Amount { get; set; }

    public decimal? W_Crys_Qty { get; set; }

    public decimal? W_Crys_Amount { get; set; }

    public decimal? W_Hand_Qty { get; set; }

    public decimal? W_Hand_Amount { get; set; }

    public decimal? W_Ste_Qty { get; set; }

    public decimal? W_Ste_Amount { get; set; }

    public decimal? Total_Expences { get; set; }

    public decimal? Total_Vat { get; set; }
}

public class SumReceiptPayment
{
    public string Accountcode { get; set; }

    public string cashlocation { get; set; }

    public float? openingBaln { get; set; }

    public float? Receipt { get; set; }

    public float? Payments { get; set; }

    public float? ClosingBaln { get; set; }
}



public class temp
{
    public int? id { get; set; }

    public string name { get; set; }
}

public class TrialBalnReport
{
    public string Account_code { get; set; }

    public string Description { get; set; }

    public float? drAmount { get; set; }

    public float? CrAmount { get; set; }

    public int? Account_Level { get; set; }

    public string Gl_Year_id { get; set; }

    public int? GL_period { get; set; }

    public string parent_code { get; set; }

    public string parent_name { get; set; }

    public string Main { get; set; }

    public string Main_name { get; set; }

    public short? Account_type { get; set; }
}

public class UndeliverdSummary
{
    public string Order_No { get; set; }

    public DateTime? Delivery_Date { get; set; }

    public decimal? Total_Qty { get; set; }

    public decimal? Deliverd_Qty { get; set; }

    public decimal? Balance_Qty { get; set; }

    public decimal? Total_Amount { get; set; }

    public decimal? Total_Advance { get; set; }

    public decimal? Total_Balance { get; set; }
}

public class UsedCrystal_Summary
{
    public string Crystal_name { get; set; }

    public string Crystal_Size { get; set; }

    public decimal? Used_Packes { get; set; }

    public decimal? Crystal_Purchase { get; set; }

    public decimal? Crystal_Hold { get; set; }

    public decimal? Crystal_left { get; set; }

    public string Brand { get; set; }
}

public class UsedStock
{
    public decimal Us_ID { get; set; }

    public string Cateries { get; set; }

    public decimal? Cat_id { get; set; }

    public decimal? unit { get; set; }

    public DateTime? Us_date { get; set; }

    public decimal? Status { get; set; }

    public string Orderno { get; set; }
}

public class users
{
    public decimal? Emp_id { get; set; }

    public string Emp_Name { get; set; }

    public string Position { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }
}

public class VoucherReport
{
    public string Voucherno { get; set; }

    public DateTime? VoucherDate { get; set; }

    public string AccountCode { get; set; }

    public string Description { get; set; }

    public float? DrAmount { get; set; }

    public float? Cramount { get; set; }

    public string Narration { get; set; }

    public string Checkno { get; set; }
}

public class Work_Assign
{
    public decimal? co_id { get; set; }

    public DateTime? deldate { get; set; }

    public DateTime? cutdate { get; set; }

    public string quantity { get; set; }

    public decimal? amount { get; set; }

    public string status { get; set; }

    public string CutBy { get; set; }

    public string Ord_no { get; set; }
}

public class Work_assign_Detail
{
    public decimal? Co_id { get; set; }

    public string K_type { get; set; }

    public decimal? k_Amount { get; set; }

    public string K_Number { get; set; }

    public string K_Size { get; set; }

    public string Requirment { get; set; }

    public string Product_ID { get; set; }

    public string UnitUsed { get; set; }

    public string Length { get; set; }

    public string Chest { get; set; }

    public string Hipps { get; set; }

    public string Bottom { get; set; }

    public string Saleevs { get; set; }

    public string Saleevloosing { get; set; }

    public string shoulder { get; set; }

    public string Deep { get; set; }

    public string Neck { get; set; }

    public string BackDown { get; set; }

    public string Extra { get; set; }

    public decimal? Status { get; set; }

    public string M_name { get; set; }

    public string M_W_T { get; set; }

    public string CrystalUsed { get; set; }

    public string PicPath { get; set; }

    public string Waist { get; set; }

    public string Note { get; set; }

    public string Saleman { get; set; }

    public string Inches { get; set; }

    public string Emb_Text1 { get; set; }

    public string Emb_Text2 { get; set; }

    public string Emb_Text3 { get; set; }

    public string Emb_Text4 { get; set; }

    public string Emb_Text5 { get; set; }

    public string Emb_Text6 { get; set; }

    public string Emb_Text7 { get; set; }

    public string Cry_Text1 { get; set; }

    public string Cry_Text2 { get; set; }

    public string Cry_Text3 { get; set; }

    public string Cry_Text4 { get; set; }

    public string Cry_Text5 { get; set; }

    public string Folak_Text1 { get; set; }

    public string Cut_work_Text1 { get; set; }

    public string Patching_Text1 { get; set; }

    public string Stitch_Text1 { get; set; }
}

public class Work_Sheet
{
    public decimal? Work_Sheet_ID { get; set; }

    public DateTime? Work_Date { get; set; }

    public decimal? Work_Tot_Amount { get; set; }

    public string Work_Order_Number { get; set; }

    public string Work_Kandoora_No { get; set; }

    public string Work_Cus_Name { get; set; }

    public decimal? Cut_S_ID { get; set; }
}

public class WorkDesign
{
    public decimal? W_D_ID { get; set; }

    public string W_D_Name { get; set; }

    public string W_D_Value { get; set; }
}

public class WORKER_EMB
{
    public decimal? M_E_Work_Id { get; set; }

    public decimal? Emp_Id { get; set; }

    public DateTime? Date { get; set; }

    public string Kandoora_No { get; set; }

    public string Model { get; set; }

    public string Emp_Name { get; set; }

    public string NECK { get; set; }

    public string BIG_NECK_905_805 { get; set; }

    public string BIG_NECK_1005_805 { get; set; }

    public string V_NECK_14_NORMAL { get; set; }

    public string V_NECK_14_FULL_APPLIQ { get; set; }

    public string M_NECK { get; set; }

    public string NO_STITCH { get; set; }

    public string SLEEVS { get; set; }

    public string BIG_SLEEVS { get; set; }

    public string N_APPLIQ { get; set; }

    public string FULL_APPLIQ { get; set; }

    public string DOUBLE_APPLIQ { get; set; }

    public string BADLA_PANI { get; set; }

    public string BAHRAI_JALI { get; set; }

    public string BACK { get; set; }

    public string SOAB { get; set; }

    public string BAIL { get; set; }

    public string ONLY_OUTING { get; set; }

    public decimal? TOTAL { get; set; }
}

public class workerEmb
{
    public string Column_0 { get; set; }

    public string Column_1 { get; set; }

    public string Column_2 { get; set; }
}