using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using SharpArtileri.Services;
using SharpArtileri.Services.ViewModels;
using SharpArtileri.Web.Base;
using SharpArtileri.Web.Helpers;
using Telerik.Web.UI;
using System.Data;


namespace SharpArtileri.Web
{
    public partial class Receiving : BaseForm
    {
        [Inject]
        public ManagementProvider ManagementService { get; set; }

        [Inject]
        public SecurityProvider SecurityService { get; set; }

        [Inject]
        public BranchProvider BranchService { get; set; }

        [Inject]
        public ItemProvider ItemService { get; set; }

        [Inject]
        public SupplierProvider SupplierService { get; set; }

        [Inject]
        public EmployeeProvider EmployeeService { get; set; }

        [Inject]
        public PurchaseOrderProvider PurchaseOrderService { get; set; }

        [Inject]
        public ReceivingProvider ReceiveService { get; set; }

        public List<ReceivingDetailViewModel> Detail
        {
            get
            {
                if (ViewState["Detail"] == null)
                    ViewState["Detail"] = new List<ReceivingDetailViewModel>();
                return ViewState["Detail"] as List<ReceivingDetailViewModel>;
            }
            set { ViewState["Detail"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            sdsMaster.ConnectionString = ManagementService.GetConnectionString(this.GetCurrentCompanyCode());
            if (!Page.IsPostBack)
            {
                mvwForm.SetActiveView(viwRead);
                RadHelper.SetUpGrid(grdMaster);
                FillDropDown();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            var privilege = SecurityService.GetPrivilege(
                ManagementService.GetConnectionString(this.GetCurrentCompanyCode()),
                CurrentPageName);

            if (mvwForm.GetActiveView() == viwRead)
            {
                btnVoid.Enabled = privilege.AllowDelete;
                if (RowID == 0)
                    lnbAddNew.Enabled = privilege.AllowAddNew;
            }
            else
            {
                if (btnSave.Enabled)
                    btnSave.Enabled = RowID == 0 ? privilege.AllowAddNew : privilege.AllowUpdate;
            }
        }

        private void FillDropDown()
        {
            ddlFindBranch.DataSource = BranchService.GetBranches();
            ddlFindBranch.DataTextField = "Name";
            ddlFindBranch.DataValueField = "ID";
            ddlFindBranch.DataBind();
        }

        private void LoadData()
        {
            var receiveHeader = ReceiveService.GetReceiving(RowID);
            Detail = ReceiveService.GetreceiveDetail(RowID).ToList();
            receivingDate.SelectedDate = receiveHeader.Date;
            txtDocumentNo.Text = receiveHeader.DocumentNo;
            txtDocumentPO.Text = receiveHeader.PurchaseOrderHeader.DocumentNo;
            hidPOID.Value = Convert.ToString(receiveHeader.PurchaseOrderHeader.ID);
            //ddlPurchaseOrder.SelectedValue = Convert.ToString(receiveHeader.PurchaseOrderID);
            txtGoodIssueNo.Text = receiveHeader.GoodIssueNo;
            txtFreightInfo.Text = receiveHeader.FreightInfo;
            txtNotes.Text = receiveHeader.Notes;
            btnVoid.Enabled = true;
            btnPrint.Enabled = true;
            RefreshDetail();

            if (receiveHeader.VoidDate.HasValue)
            {
                btnSave.Enabled = false;
                btnPrint.Enabled = false;
                btnVoid.Enabled = false;
            }
        }


        private void RefreshDetail()
        {
            grdDetail.DataSource = Detail;
            grdDetail.DataBind();

            grdDetail.Columns[0].Visible = false;
            grdDetail.Columns[1].Visible = false;
        }


        #region Control Event
        protected void lnbAddNew_Click(object sender, EventArgs e)
        {
            mvwForm.SetActiveView(viwAddEdit);
            ViewState["BranchID"] = Convert.ToInt32(ddlFindBranch.SelectedValue);
            RefreshDetail();
            txtDocumentNo.Text = "(Auto)";
            receivingDate.SelectedDate = DateTime.Today;
            //lblBranch.Text = ddlFindBranch.SelectedItem.Text;
            btnVoid.Enabled = false;
            btnPrint.Enabled = false;
        }

        protected void lnbDelete_Click(object sender, EventArgs e)
        {

        }

        protected void grdMaster_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                mvwForm.SetActiveView(viwAddEdit);
                RowID = Convert.ToInt32(e.CommandArgument);
                LoadData();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ReloadCurrentPage();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("AddEdit");
            if (Page.IsValid)
            {
                try
                {
                    if (Detail.Count == 0)
                    {
                        WebFormHelper.SetLabelTextWithCssClass(lblStatusAddEdit, "Cannot save empty detail", LabelStyleNames.ErrorMessage);
                        return;
                    }

                    foreach (GridItem row in grdDetail.Items)
                    {
                        int itemID = Convert.ToInt32(((Label)row.FindControl("lblQtyPO")).Text);
                        ReceivingDetailViewModel detail = Detail.Single(x => x.ItemID == itemID);
                        detail.QtyReceived = Convert.ToDecimal(((RadNumericTextBox)row.FindControl("txtQtyReceived")).Value);
                        detail.Notes = ((RadTextBox)row.FindControl("txtNotes")).Text;
                    }

//                    if (ReceiveService.CheckItemIsFullReceive(RowID, Convert.ToInt32(hidPOID.Value), Detail))
//                    {
//                        WebFormHelper.SetLabelTextWithCssClass(lblStatusAddEdit,
//                            @"Item receiving is not valid in qty or does not exist in 
//                        Purchase Order or surpass the item quantity in Purchase Order",
//                            LabelStyleNames.ErrorMessage);
//                        return;
//                    }


                    string result = ReceiveService.AddOrUpdateReceiving(
                        RowID,
                       Convert.ToInt32(hidPOID.Value),
                        receivingDate.SelectedDate.GetValueOrDefault(),
                        txtGoodIssueNo.Text,
                        txtFreightInfo.Text,
                        txtNotes.Text,
                        Detail);

                    btnPrint.Enabled = true;

                    //if (result == "F")
                    //{
                    //    btnSave.Enabled = false;
                    //    btnVoid.Enabled = false;
                    //}
                    //ScriptManager.RegisterStartupScript(this,
                    //    this.GetType(),
                    //    "save",
                    //    String.Format("alert('Receiving has been saved');"),
                    //    true);

                    ReloadCurrentPage();
                }
                catch (Exception ex)
                {
                    WebFormHelper.SetLabelTextWithCssClass(lblStatusAddEdit, ex.Message, LabelStyleNames.ErrorMessage);
                }
            }
        }

        //protected void ddlPurchaseOrder_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        //{
        //    Detail = ReceiveService.GetUnReceivingPO(Convert.ToInt32(ddlPurchaseOrder.SelectedValue), RowID);
        //    RefreshDetail();
        //}

        //protected void btnAddDetail_Click(object sender, EventArgs e)
        //{
        //    Page.Validate("AddDetail");
        //    if (Page.IsValid)
        //    {
        //        if (Convert.ToInt32(ntbQty.Value) == 0)
        //            ntbQty.Value = 1;

        //        try
        //        {

        //            var detailLine = new ReceivingDetailViewModel();
        //            var item = ItemService.GetItem(Convert.ToInt32(cboItem.SelectedValue));

        //            detailLine.ItemID = item.ID;
        //            detailLine.ItemCode = item.Code;
        //            detailLine.ItemName = item.Name;
        //            detailLine.UnitName = ddlUnit.SelectedItem.Text;
        //            detailLine.QtyReceived = Convert.ToInt32(ntbQty.Value);
        //            detailLine.Notes = txtNoteDetail.Text;
        //            Detail.Add(detailLine);

        //            RefreshDetail();

        //            cboItem.Text = "";
        //            ntbQty.Value = 1;
        //            ddlUnit.SelectedIndex = -1;


        //            cboItem.Focus();
        //        }
        //        catch (Exception ex)
        //        {
        //            WebFormHelper.SetLabelTextWithCssClass(lblStatusAddEdit, ex.Message, LabelStyleNames.ErrorMessage);
        //        }

        //    }
        //}

        protected void btnProcessVoid_Click(object sender, EventArgs e)
        {
            try
            {
                ReceiveService.VoidReceiving(RowID, txtVoidReason.Text);
                LoadData();
                WebFormHelper.SetLabelTextWithCssClass(lblStatusAddEdit, String.Format("Purchase Order {0} has been marked as void", string.Empty), LabelStyleNames.AlternateMessage);

                btnSave.Enabled = false;
                btnVoid.Enabled = false;
            }
            catch (Exception ex)
            {
                WebFormHelper.SetLabelTextWithCssClass(lblStatusAddEdit, ex.Message, LabelStyleNames.ErrorMessage);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdMaster.DataBind();
        }

        protected void sdsMaster_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@BranchID"].Value = Convert.ToInt32(ddlFindBranch.SelectedValue);
            e.Command.Parameters["@DateFrom"].Value = dtpFindDateFrom.SelectedDate ?? new DateTime(1980, 1, 1);
            e.Command.Parameters["@DateTo"].Value = dtpFindDateTo.SelectedDate ?? new DateTime(2099, 12, 31);
            e.Command.Parameters["@DocumentNo"].Value = txtFindDocumentNo.Text;
        }

        #endregion

        protected void radBtnRefreshPO_Click(object sender, EventArgs e)
        {
            Detail = ReceiveService.GetUnReceivingPO(Convert.ToInt32(hidPOID.Value), RowID);
            RefreshDetail();
        }
    }
}