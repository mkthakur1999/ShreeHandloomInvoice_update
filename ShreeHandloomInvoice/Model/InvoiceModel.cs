using ShreeHandloomInvoice.ViewModel;
using System;
using System.Collections.ObjectModel;

public class InvoiceModel : BaseViewModel
{
    public string ShopName { get; set; } = "SHREE HANDLOOM HOUSE";
    public string ShopAddress { get; set; } =
        "Shop no. 17 E/2, Highway Park CHS. Ltd., Kandivali (E), Mumbai – 400 101";

    public string GSTIN { get; set; } = "27BNHPS9363R1ZR";

    private string _invoiceNo = "";
    public string InvoiceNo
    {
        get => _invoiceNo;
        set => SetProperty(ref _invoiceNo, value);
    }

    private DateTime _invoiceDate = DateTime.Now;
    public DateTime InvoiceDate
    {
        get => _invoiceDate;
        set => SetProperty(ref _invoiceDate, value);
    }

    public string OrderNo { get; set; }
    public DateTime? OrderDate { get; set; }
    public string DispatchThrough { get; set; }
    public string VehicleNo { get; set; }
    public string Destination { get; set; }

    public string BuyerName { get; set; }
    public string ConsigneeName { get; set; }
    public string BuyerGSTIN { get; set; }
    public string BuyerAddress { get; set; }
    public string ConsigneeAddress { get; set; }

    public ObservableCollection<InvoiceItemModel> Items { get; set; }

    private double _totalAmount;
    public double TotalAmount
    {
        get => _totalAmount;
        set => SetProperty(ref _totalAmount, value);
    }

    private string _amountInWords = "";
    public string AmountInWords
    {
        get => _amountInWords;
        set => SetProperty(ref _amountInWords, value);
    }
}
